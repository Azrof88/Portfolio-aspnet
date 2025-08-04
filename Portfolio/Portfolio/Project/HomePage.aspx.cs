using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace Portfolio.Project
{
    
    public partial class HomePage : System.Web.UI.Page
    {
        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Website.Text)) return; // Spam check

            string toEmail = "hmazrof@email.com"; // Change to your email
            string fromEmail = Email.Text.Trim();
            string subject = $"New Inquiry - {Subject.SelectedValue}";
            string body = $@"
                <strong>Name:</strong> {Name.Text}<br/>
                <strong>Email:</strong> {fromEmail}<br/>
                <strong>Phone:</strong> {Phone.Text}<br/>
                <strong>Message:</strong><br/>{Message.Text.Replace("\n", "<br/>")}";

            try
            {
                // --- Read all settings from Web.config ---
                string smtpUser = ConfigurationManager.AppSettings["SmtpUser"];
                string smtpPass = ConfigurationManager.AppSettings["SmtpPass"];
                string smtpHost = ConfigurationManager.AppSettings["SmtpHost"];
                int smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]);
                string adminEmail = ConfigurationManager.AppSettings["AdminEmail"];

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(smtpUser); // Send FROM your own email
                mail.To.Add(adminEmail);               // Send TO your own email
                mail.ReplyToList.Add(new MailAddress(fromEmail)); // Add the user's email here
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient(smtpHost, smtpPort); // Use variables from Web.config
                smtp.Credentials = new NetworkCredential(smtpUser, smtpPass); // Use variables from Web.config
                smtp.EnableSsl = true;
                smtp.Send(mail);

                StatusLabel.Text = "Message sent successfully!";
                StatusLabel.CssClass += " success";
            }
            catch (Exception ex)
            {
                StatusLabel.Text = "Something went wrong. Please try again.";
                StatusLabel.CssClass += " error";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Add your image paths here
                string[] imagePaths = {
                "Azrof.jpg",
                "Azrof.jpg"
                
            };

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
            }
            
    }
    }
}