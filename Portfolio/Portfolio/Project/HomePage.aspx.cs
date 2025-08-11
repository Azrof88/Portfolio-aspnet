using Newtonsoft.Json; // REQUIRED: Add this using statement for JSON functionality
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Portfolio.Project
{
    // Represents a single project from the database


    // Represents a single skill from the database


    public partial class HomePage : System.Web.UI.Page
    {
        private string guestbookFilePath;

        protected void Page_Load(object sender, EventArgs e)
        {
            guestbookFilePath = Server.MapPath("~/App_Data/guestbook.txt");

            // --- THIS LOGIC RUNS ON EVERY PAGE LOAD TO PREVENT CONTENT DISAPPEARING ---

            // --- Slider logic ---
            string[] imagePaths = { "Azrof.jpg", "Azrof.jpg" };
            sliderContainer.Controls.Clear();
            dotsContainer.Controls.Clear();
            foreach (string imgPath in imagePaths)
            {
                Panel slide = new Panel();
                slide.CssClass = "slide";
                slide.Style.Add("background-image", $"url({ResolveUrl(imgPath)})");
                sliderContainer.Controls.Add(slide);
            }
            for (int i = 0; i < imagePaths.Length; i++)
            {
                Panel dot = new Panel();
                dot.CssClass = "dot";
                if (i == 0 && !IsPostBack) dot.CssClass += " active";
                dotsContainer.Controls.Add(dot);
            }

            // --- Load all dynamic content ---
            LoadProjectsFromDatabase();
            LoadComments();

            if (!IsPostBack)
            {
                // This runs only on the first page load to populate the database
                PopulateSkillsIfEmpty();
            }
            LoadSkillsFromDatabase();
        }

        // --- SKILLS METHODS ---
        private void PopulateSkillsIfEmpty()
        {
            string connectionString = ConfigurationManager.AppSettings["DbConnectionString"];
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand countCmd = new SqlCommand("SELECT COUNT(*) FROM Skills", con);
                    int count = (int)countCmd.ExecuteScalar();

                    if (count == 0)
                    {
                        string jsonFilePath = Server.MapPath("~/App_Data/skills.json");
                        string jsonData = File.ReadAllText(jsonFilePath);
                        List<Skill> skills = JsonConvert.DeserializeObject<List<Skill>>(jsonData);

                        foreach (var skill in skills)
                        {
                            string insertQuery = "INSERT INTO Skills (SkillName, Description, IconClass, Proficiency, GitHubURL) VALUES (@SkillName, @Description, @IconClass, @Proficiency, @GitHubURL)";
                            using (SqlCommand insertCmd = new SqlCommand(insertQuery, con))
                            {
                                insertCmd.Parameters.AddWithValue("@SkillName", skill.SkillName);
                                insertCmd.Parameters.AddWithValue("@Description", skill.Description);
                                insertCmd.Parameters.AddWithValue("@IconClass", skill.IconClass);
                                insertCmd.Parameters.AddWithValue("@Proficiency", skill.Proficiency);
                                insertCmd.Parameters.AddWithValue("@GitHubURL", skill.GitHubURL);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error populating skills from JSON: " + ex.Message);
                }
            }
        }

        private void LoadSkillsFromDatabase()
        {
            List<Skill> skills = new List<Skill>();
            string connectionString = ConfigurationManager.AppSettings["DbConnectionString"];
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT SkillName, Description, IconClass, Proficiency, GitHubURL FROM Skills";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                skills.Add(new Skill
                                {
                                    SkillName = reader["SkillName"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    IconClass = reader["IconClass"].ToString(),
                                    Proficiency = (int)reader["Proficiency"],
                                    GitHubURL = reader["GitHubURL"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Skills DB Error: " + ex.Message);
                skillsGrid.Controls.Add(new LiteralControl("<p style='color:red;'>Could not load skills.</p>"));
                return;
            }

            StringBuilder skillsHtml = new StringBuilder();
            foreach (var skill in skills)
            {
                skillsHtml.AppendFormat(@"
        <a href='{0}' class='skill-card' target='_blank' rel='noopener'>
            <div class='card-icon'><i class='{1}'></i></div>
            <h3>{2}</h3>
            <p>{3}</p>
            
            <!-- THIS IS THE NEW LINE FOR THE PERCENTAGE -->
            <p class='proficiency-text'>{4}% Proficiency</p> 
            
            <div class='progress-bar-container'>
                <div class='progress-bar-fill' style='width: {4}%;'></div>
            </div>
        </a>",
                    skill.GitHubURL, skill.IconClass, skill.SkillName, skill.Description, skill.Proficiency);
            }
            skillsGrid.Controls.Clear();
            skillsGrid.Controls.Add(new LiteralControl(skillsHtml.ToString()));
        }

        // --- PROJECTS METHODS ---
        private void LoadProjectsFromDatabase()
        {
            List<Project> projects = new List<Project>();
            string connectionString = ConfigurationManager.AppSettings["DbConnectionString"];
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT Title, Category, ImageURL, Description, GitHubURL, TechStack FROM Projects";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                projects.Add(new Project
                                {
                                    Title = reader["Title"].ToString(),
                                    Category = reader["Category"].ToString(),
                                    ImageURL = reader["ImageURL"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    GitHubURL = reader["GitHubURL"].ToString(),
                                    TechStack = reader["TechStack"].ToString().Split(',')
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Projects DB Error: " + ex.Message);
                carouselTrack.Controls.Add(new LiteralControl("<p style='color:red; text-align:center;'>Could not load projects at this time.</p>"));
                return;
            }

            StringBuilder projectHtml = new StringBuilder();
            foreach (var project in projects)
            {
                projectHtml.AppendFormat(@"
                    <div class='project-card' data-category='{0}'>
                        <img src='{1}' alt='{2}' class='project-image'>
                        <h3>{2}</h3>
                        <p>{3}</p>
                        <div class='tech-stack'>",
                    project.Category, project.ImageURL, project.Title, project.Description);

                foreach (var tech in project.TechStack)
                {
                    projectHtml.AppendFormat("<span>{0}</span>", tech.Trim());
                }

                projectHtml.AppendFormat(@"
                        </div>
                        <a href='{0}' class='github-link' target='_blank'>
                            <i class='fab fa-github'></i>
                        </a>
                    </div>", project.GitHubURL);
            }
            carouselTrack.Controls.Clear();
            carouselTrack.Controls.Add(new LiteralControl(projectHtml.ToString()));
        }

        // --- GUESTBOOK METHODS ---
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
                            string name = Server.HtmlEncode(parts[0]);
                            string message = Server.HtmlEncode(parts[1]);

                            commentsHtml.Append("<div class='comment-item'>");
                            commentsHtml.AppendFormat("<p class='comment-author'>{0} wrote:</p>", name);
                            commentsHtml.AppendFormat("<p class='comment-text'>{0}</p>", message);
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

        protected void btnSubmitComment_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtGuestName.Text.Trim();
                string message = txtGuestMessage.Text.Trim();

                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(message))
                {
                    string entry = $"{name}|{message.Replace("\n", "<br>")}\n";
                    File.AppendAllText(guestbookFilePath, entry);
                    txtGuestName.Text = "";
                    txtGuestMessage.Text = "";
                    lblCommentStatus.Text = "Thank you for your comment!";
                    LoadComments();
                }
            }
            catch (Exception ex)
            {
                lblCommentStatus.Text = "Sorry, there was an error saving your comment.";
                System.Diagnostics.Debug.WriteLine("Error saving comment: " + ex.Message);
            }
        }

        // --- CONTACT FORM SUBMISSION ---
        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Website.Text)) return;

            if (string.IsNullOrWhiteSpace(Name.Text) ||
                string.IsNullOrWhiteSpace(Email.Text) ||
                string.IsNullOrWhiteSpace(Message.Text))
            {
                StatusLabel.Text = "Please fill out all required fields.";
                StatusLabel.CssClass = "form-status error";
                return;
            }

            string fromEmail = Email.Text.Trim();
            string subject = $"New Inquiry - {Subject.SelectedValue}";
            string body = $@"
                <strong>Name:</strong> {Name.Text}<br/>
                <strong>Email:</strong> {fromEmail}<br/>
                <strong>Phone:</strong> {Phone.Text}<br/>
                <strong>Message:</strong><br/>{Message.Text.Replace("\n", "<br/>")}";

            try
            {
                string smtpUser = ConfigurationManager.AppSettings["SmtpUser"];
                string smtpPass = ConfigurationManager.AppSettings["SmtpPass"];
                string smtpHost = ConfigurationManager.AppSettings["SmtpHost"];
                int smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]);
                string adminEmail = ConfigurationManager.AppSettings["AdminEmail"];

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(smtpUser);
                mail.To.Add(adminEmail);
                mail.ReplyToList.Add(new MailAddress(fromEmail));
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient(smtpHost, smtpPort);
                smtp.Credentials = new NetworkCredential(smtpUser, smtpPass);
                smtp.EnableSsl = true;
                smtp.Send(mail);

                StatusLabel.Text = "Message sent successfully!";
                StatusLabel.CssClass = "form-status success";

                Name.Text = "";
                Email.Text = "";
                Phone.Text = "";
                Message.Text = "";
                Subject.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                StatusLabel.Text = "Something went wrong. Please try again.";
                StatusLabel.CssClass = "form-status error";
                System.Diagnostics.Debug.WriteLine("Email Error: " + ex.Message);
            }
        }
    }
}
