using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;

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
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(fromEmail);
                mail.To.Add(toEmail);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587); // change to your SMTP
                smtp.Credentials = new NetworkCredential("hmazrof@gmail.com", "yxik qxvq rton njld");
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