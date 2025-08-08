using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient; // Required for SQL database access
using System.Net;
using System.Net.Mail;
using System.Text;          // Required for StringBuilder
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Portfolio.Project
{
    // This new class represents a single project from our database
    public class Project
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public string GitHubURL { get; set; }
        public string[] TechStack { get; set; }
    }

    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // --- Your existing slider logic is preserved ---
                string[] imagePaths = { "Azrof.jpg", "Azrof.jpg" };

                // Create slides
                foreach (string imgPath in imagePaths)
                {
                    Panel slide = new Panel();
                    slide.CssClass = "slide";
                    slide.Style.Add("background-image", $"url({ResolveUrl(imgPath)})");
                    sliderContainer.Controls.Add(slide);
                }

                // Create dots
                for (int i = 0; i < imagePaths.Length; i++)
                {
                    Panel dot = new Panel();
                    dot.CssClass = "dot";
                    if (i == 0) dot.CssClass += " active";
                    dotsContainer.Controls.Add(dot);
                }
                // --- End of existing slider logic ---


                // --- NEW: Load projects from the database ---
                LoadProjectsFromDatabase();
            }
        }

        private void LoadProjectsFromDatabase()
        {
            List<Project> projects = new List<Project>();
            // Reads the connection string from Web.config
            string connectionString = ConfigurationManager.AppSettings["DbConnectionString"];

            // Use a try-catch block for robust database operations
            try
            {
                // The 'using' statement ensures the connection is properly closed
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT Title, Category, ImageURL, Description, GitHubURL, TechStack FROM Projects";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open(); // Open the connection to the database
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Loop through each row returned by the query
                            while (reader.Read())
                            {
                                // Create a new Project object and fill it with data from the row
                                projects.Add(new Project
                                {
                                    Title = reader["Title"].ToString(),
                                    Category = reader["Category"].ToString(),
                                    ImageURL = reader["ImageURL"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    GitHubURL = reader["GitHubURL"].ToString(),
                                    // Split the comma-separated string from the DB into an array
                                    TechStack = reader["TechStack"].ToString().Split(',')
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any database errors to the debug console for troubleshooting
                System.Diagnostics.Debug.WriteLine("Database Error: " + ex.Message);
                // Display a friendly error message to the user on the page
                carouselTrack.Controls.Add(new LiteralControl("<p style='color:red; text-align:center;'>Could not load projects at this time.</p>"));
                return;
            }

            // Use StringBuilder for efficient string concatenation
            StringBuilder projectHtml = new StringBuilder();
            // Loop through the list of projects we fetched from the database
            foreach (var project in projects)
            {
                // Build the HTML for one project card
                projectHtml.AppendFormat(@"
                    <div class='project-card' data-category='{0}'>
                        <img src='{1}' alt='{2}' class='project-image'>
                        <h3>{2}</h3>
                        <p>{3}</p>
                        <div class='tech-stack'>",
                    project.Category, project.ImageURL, project.Title, project.Description);

                // Add the tech stack spans
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

            // Add the complete block of generated HTML to the page
            carouselTrack.Controls.Add(new LiteralControl(projectHtml.ToString()));
        }


        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            // --- Your existing, corrected email logic is preserved ---
            if (!string.IsNullOrEmpty(Website.Text)) return; // Spam check

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
                StatusLabel.CssClass += " success";
            }
            catch (Exception ex)
            {
                StatusLabel.Text = "Something went wrong. Please try again.";
                StatusLabel.CssClass += " error";
                System.Diagnostics.Debug.WriteLine("Email Error: " + ex.Message);
            }
        }
    }
}
