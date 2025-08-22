using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace Portfolio.Project
{
    public partial class AddTestimonial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IsAdmin"] == null || (bool)Session["IsAdmin"] == false)
            {
                Response.Redirect("Admin.aspx");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["DbConnectionString"];
            string query = @"INSERT INTO Testimonials 
                             (Quote, AuthorName, AuthorTitle, AuthorImageURL) 
                             VALUES 
                             (@Quote, @AuthorName, @AuthorTitle, @AuthorImageURL)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Quote", txtQuote.Text);
                    cmd.Parameters.AddWithValue("@AuthorName", txtAuthorName.Text);
                    cmd.Parameters.AddWithValue("@AuthorTitle", txtAuthorTitle.Text);
                    cmd.Parameters.AddWithValue("@AuthorImageURL", txtAuthorImageURL.Text);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        Response.Redirect("Admin.aspx");
                    }
                    catch (Exception ex)
                    {
                        lblStatus.Text = "Error saving testimonial: " + ex.Message;
                    }
                }
            }
        }
    }
}
