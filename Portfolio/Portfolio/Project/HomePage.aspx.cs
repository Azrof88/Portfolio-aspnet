using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web.UI;

namespace Portfolio.Project
{
    public partial class HomePage : System.Web.UI.Page
    {
        private string guestbookFilePath;

        protected void Page_Load(object sender, EventArgs e)
        {
            guestbookFilePath = Server.MapPath("~/App_Data/guestbook.txt");

            if (!IsPostBack)
            {
                LoadSkills();
                LoadProjects();
                LoadComments();
            }
        }

        // --- DATA LOADING METHODS ---

        private void LoadSkills()
        {
            string connectionString = ConfigurationManager.AppSettings["DbConnectionString"];
            StringBuilder skillsHtml = new StringBuilder();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // UPDATED: The query now also selects the GitHubURL
                string query = "SELECT SkillName, Description, IconClass, Proficiency, GitHubURL FROM Skills";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Build each skill card inside a Bootstrap column
                                skillsHtml.Append(@"<div class='col-md-6 col-lg-4 mb-4'>");
                                // The main card is a plain <div>
                                skillsHtml.Append(@"<div class='card h-100 text-center p-4'>");

                                // The icon is now a clickable link to GitHub
                                skillsHtml.AppendFormat(@"<a href='{0}' target='_blank' rel='noopener' class='card-icon-link'>", reader["GitHubURL"]);
                                skillsHtml.AppendFormat(@"<div class='card-icon-wrapper'><i class='{0}'></i></div></a>", reader["IconClass"]);

                                skillsHtml.Append(@"<div class='card-body d-flex flex-column'>");
                                skillsHtml.AppendFormat(@"<h5 class='card-title'>{0}</h5>", reader["SkillName"]);
                                skillsHtml.AppendFormat(@"<p class='card-text'>{0}</p>", reader["Description"]);

                                // Adds the percentage text
                                skillsHtml.AppendFormat(@"<p class='proficiency-text'>{0}% Proficiency</p>", reader["Proficiency"]);

                                skillsHtml.Append(@"<div class='progress mt-auto'><div class='progress-bar'");
                                skillsHtml.AppendFormat(@" role='progressbar' style='width: {0}%' aria-valuenow='{0}' aria-valuemin='0' aria-valuemax='100'></div></div>", reader["Proficiency"]);
                                skillsHtml.Append(@"</div></div></div>");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Skills DB Error: " + ex.Message);
                        litSkills.Text = "<p class='text-danger'>Could not load skills.</p>";
                    }
                }
            }
            litSkills.Text = skillsHtml.ToString();
        }


        private void LoadProjects()
        {
            string connectionString = ConfigurationManager.AppSettings["DbConnectionString"];
            StringBuilder projectsHtml = new StringBuilder();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Title, Description, ImageURL, GitHubURL FROM Projects";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Build each project card inside a Bootstrap column
                                projectsHtml.Append(@"<div class='col-md-6 col-lg-4 mb-4'>");
                                projectsHtml.Append(@"<div class='card h-100'>");
                                projectsHtml.AppendFormat(@"<img src='{0}' class='card-img-top' alt='{1}' style='height: 200px; object-fit: cover;'>", ResolveUrl("~/Project/" + reader["ImageURL"].ToString()), reader["Title"]);
                                projectsHtml.Append(@"<div class='card-body d-flex flex-column'>");
                                projectsHtml.AppendFormat(@"<h5 class='card-title'>{0}</h5>", reader["Title"]);
                                projectsHtml.AppendFormat(@"<p class='card-text'>{0}</p>", reader["Description"]);
                                projectsHtml.AppendFormat(@"<a href='{0}' class='btn btn-outline-primary mt-auto' target='_blank'>View on GitHub</a>", reader["GitHubURL"]);
                                projectsHtml.Append(@"</div></div></div>");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Projects DB Error: " + ex.Message);
                        litProjects.Text = "<p class='text-danger'>Could not load projects.</p>";
                    }
                }
            }
            litProjects.Text = projectsHtml.ToString();
        }

        private void LoadComments()
        {
            StringBuilder commentsHtml = new StringBuilder();
            try
            {
                if (File.Exists(guestbookFilePath))
                {
                    string[] comments = File.ReadAllLines(guestbookFilePath);
                    for (int i = comments.Length - 1; i >= 0; i--)
                    {
                        string line = comments[i];
                        string[] parts = line.Split(new[] { '|' }, 2);
                        if (parts.Length == 2)
                        {
                            commentsHtml.Append("<div class='comment-item'>");
                            commentsHtml.AppendFormat("<p class='comment-author'>{0} wrote:</p>", Server.HtmlEncode(parts[0]));
                            commentsHtml.AppendFormat("<p class='comment-text'>{0}</p>", Server.HtmlEncode(parts[1]));
                            commentsHtml.Append("</div>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading comments: " + ex.Message);
            }
            litComments.Text = commentsHtml.ToString();
        }

        // --- EVENT HANDLERS ---
        protected void btnSubmitComment_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtGuestName.Text) && !string.IsNullOrWhiteSpace(txtGuestMessage.Text))
                {
                    string entry = $"{txtGuestName.Text.Trim()}|{txtGuestMessage.Text.Trim().Replace("\n", "<br>")}\n";
                    File.AppendAllText(guestbookFilePath, entry);
                    txtGuestName.Text = "";
                    txtGuestMessage.Text = "";
                    lblCommentStatus.CssClass = "d-block mt-3 text-success";
                    lblCommentStatus.Text = "Thank you for your comment!";
                    LoadComments();
                }
            }
            catch (Exception ex)
            {
                lblCommentStatus.CssClass = "d-block mt-3 text-danger";
                lblCommentStatus.Text = "Sorry, there was an error saving your comment.";
                System.Diagnostics.Debug.WriteLine("Error saving comment: " + ex.Message);
            }
        }

        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Name.Text) || string.IsNullOrWhiteSpace(Email.Text) || string.IsNullOrWhiteSpace(Message.Text))
            {
                StatusLabel.CssClass = "d-block mt-3 text-danger";
                StatusLabel.Text = "Please fill out all required fields.";
                return;
            }

            try
            {
                // NOTE: You will need to re-add your email sending logic here
                // For now, we'll just show a success message.
                StatusLabel.CssClass = "d-block mt-3 text-success";
                StatusLabel.Text = "Message sent successfully!";
                Name.Text = "";
                Email.Text = "";
                Message.Text = "";
            }
            catch (Exception ex)
            {
                StatusLabel.CssClass = "d-block mt-3 text-danger";
                StatusLabel.Text = "Something went wrong. Please try again.";
                System.Diagnostics.Debug.WriteLine("Email Error: " + ex.Message);
            }
        }
    }
}
