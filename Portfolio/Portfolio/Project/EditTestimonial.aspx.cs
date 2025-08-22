using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace Portfolio.Project
{
    public partial class EditTestimonial : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.AppSettings["DbConnectionString"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IsAdmin"] == null || (bool)Session["IsAdmin"] == false)
            {
                Response.Redirect("Admin.aspx");
            }

            if (!IsPostBack)
            {
                // Only load the testimonial data the first time the page is visited.
                LoadTestimonialData();
            }
        }

        // This method fetches the data for the selected testimonial and fills the form.
        private void LoadTestimonialData()
        {
            if (Request.QueryString["id"] != null)
            {
                string testimonialId = Request.QueryString["id"];
                string query = "SELECT * FROM Testimonials WHERE ID = @ID";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ID", testimonialId);
                        try
                        {
                            con.Open();
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Populate the form fields with data from the database
                                    txtQuote.Text = reader["Quote"].ToString();
                                    txtAuthorName.Text = reader["AuthorName"].ToString();
                                    txtAuthorTitle.Text = reader["AuthorTitle"].ToString();
                                    txtAuthorImageURL.Text = reader["AuthorImageURL"].ToString();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            lblStatus.Text = "Error loading testimonial data: " + ex.Message;
                        }
                    }
                }
            }
        }

        // This method saves the changes back to the database.
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                string testimonialId = Request.QueryString["id"];
                string query = @"UPDATE Testimonials SET 
                                 Quote = @Quote, 
                                 AuthorName = @AuthorName, 
                                 AuthorTitle = @AuthorTitle, 
                                 AuthorImageURL = @AuthorImageURL 
                                 WHERE ID = @ID";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Quote", txtQuote.Text);
                        cmd.Parameters.AddWithValue("@AuthorName", txtAuthorName.Text);
                        cmd.Parameters.AddWithValue("@AuthorTitle", txtAuthorTitle.Text);
                        cmd.Parameters.AddWithValue("@AuthorImageURL", txtAuthorImageURL.Text);
                        cmd.Parameters.AddWithValue("@ID", testimonialId);

                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            Response.Redirect("Admin.aspx");
                        }
                        catch (Exception ex)
                        {
                            lblStatus.Text = "Error updating testimonial: " + ex.Message;
                        }
                    }
                }
            }
        }
    }
}
