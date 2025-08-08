using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient; // Required for SQL database access
using System.IO;             // Required for File I/O
using System.Net;
using System.Net.Mail;
using System.Text;           // Required for StringBuilder
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Portfolio.Project
{
    // This class represents a single project from our database
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
        // Define the path for our guestbook text file
        private string guestbookFilePath;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Set the file path every time the page loads.
            guestbookFilePath = Server.MapPath("~/App_Data/guestbook.txt");

            // --- THIS LOGIC NOW RUNS ON EVERY PAGE LOAD, NOT JUST THE FIRST ONE ---
            // This will prevent your content from disappearing after a button click.

            // --- Slider logic ---
            string[] imagePaths = { "Azrof.jpg", "Azrof.jpg" };

            // IMPORTANT: Clear existing controls to prevent duplicates on postback
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
                // Only set the first dot as active on the initial page load
                if (i == 0 && !IsPostBack)
                {
                    dot.CssClass += " active";
                }
                dotsContainer.Controls.Add(dot);
            }

            // --- Load projects from the database ---
            LoadProjectsFromDatabase();

            // --- Load existing comments from the guestbook file ---
            LoadComments();
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

        // --- NEW GUESTBOOK METHODS START HERE ---

        private void LoadComments()
        {
            // This method READS from the text file.
            StringBuilder commentsHtml = new StringBuilder();
            try
            {
                // Check if the file exists before trying to read it.
                if (File.Exists(guestbookFilePath))
                {
                    // Read all lines from the file into an array.
                    string[] comments = File.ReadAllLines(guestbookFilePath);
                    // Loop through the lines in reverse to show the newest comments first.
                    for (int i = comments.Length - 1; i >= 0; i--)
                    {
                        string line = comments[i];
                        // We expect the format to be "Name|Message"
                        string[] parts = line.Split(new[] { '|' }, 2);
                        if (parts.Length == 2)
                        {
                            string name = Server.HtmlEncode(parts[0]); // Encode to prevent HTML injection
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

            // Display the generated HTML in our literal control
            litComments.Text = commentsHtml.ToString();
        }

        protected void btnSubmitComment_Click(object sender, EventArgs e)
        {
            // This method WRITES to the text file.
            try
            {
                string name = txtGuestName.Text.Trim();
                string message = txtGuestMessage.Text.Trim();

                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(message))
                {
                    // Format the entry with a pipe character as a separator.
                    // Replace newlines in the message to store it properly.
                    string entry = $"{name}|{message.Replace("\n", "<br>")}\n";

                    // Use AppendAllText to add the new comment to the end of the file.
                    // This will create the file if it doesn't exist.
                    File.AppendAllText(guestbookFilePath, entry);

                    // Clear the form and show a success message
                    txtGuestName.Text = "";
                    txtGuestMessage.Text = "";
                    lblCommentStatus.Text = "Thank you for your comment!";

                    // Reload the comments to show the new one immediately
                    LoadComments();
                }
            }
            catch (Exception ex)
            {
                lblCommentStatus.Text = "Sorry, there was an error saving your comment.";
                System.Diagnostics.Debug.WriteLine("Error saving comment: " + ex.Message);
            }
        }

        // --- NEW GUESTBOOK METHODS END HERE ---

        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            // --- Spam check is preserved ---
            if (!string.IsNullOrEmpty(Website.Text)) return;

            // --- NEW: Add server-side validation to check for empty required fields ---
            if (string.IsNullOrWhiteSpace(Name.Text) ||
                string.IsNullOrWhiteSpace(Email.Text) ||
                string.IsNullOrWhiteSpace(Message.Text))
            {
                // If any required field is empty, show a specific error and stop.
                StatusLabel.Text = "Please fill out all required fields.";
                StatusLabel.CssClass = "form-status error"; // Make sure your CSS has a style for this
                return; // Stop the function from proceeding further
            }
            // --- End of new validation ---

            // The rest of your email logic will only run if the validation passes.
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
                StatusLabel.CssClass = "form-status success"; // Use '=' instead of '+=' to reset classes
                                                              // --- ADD THIS NEW BLOCK OF CODE ---
                                                              // This will clear the form fields after a successful submission.
                Name.Text = "";
                Email.Text = "";
                Phone.Text = "";
                Message.Text = "";
                Subject.SelectedIndex = 0; // Resets the dropdown to the first item
                                           // --- END OF NEW CODE ---
            }
            catch (Exception ex)
            {
                StatusLabel.Text = "Something went wrong. Please try again.";
                StatusLabel.CssClass = "form-status error"; // Use '=' instead of '+=' to reset classes
                System.Diagnostics.Debug.WriteLine("Email Error: " + ex.Message);
            }
        }

    }
}
