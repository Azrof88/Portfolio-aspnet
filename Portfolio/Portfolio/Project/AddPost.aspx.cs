using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace Portfolio.Project
{
    public partial class AddPost : System.Web.UI.Page
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
            string title = txtTitle.Text.Trim();
            string slug = CreateSlug(title); // Create a URL-friendly version of the title

            string connectionString = ConfigurationManager.AppSettings["DbConnectionString"];
            string query = @"INSERT INTO BlogPosts 
                             (Title, Slug, Content, PublishDate, Excerpt) 
                             VALUES 
                             (@Title, @Slug, @Content, @PublishDate, @Excerpt)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Slug", slug);
                    cmd.Parameters.AddWithValue("@Content", txtContent.Text);
                    cmd.Parameters.AddWithValue("@PublishDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Excerpt", txtExcerpt.Text);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        Response.Redirect("Admin.aspx");
                    }
                    catch (Exception ex)
                    {
                        lblStatus.Text = "Error saving post: " + ex.Message;
                    }
                }
            }
        }

        // Helper function to create a URL-friendly "slug" from a title
        private string CreateSlug(string title)
        {
            string slug = title.ToLowerInvariant();
            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", ""); // Remove invalid chars
            slug = Regex.Replace(slug, @"\s+", " ").Trim(); // Condense whitespace
            slug = slug.Substring(0, slug.Length <= 45 ? slug.Length : 45).Trim(); // Cut to length
            slug = Regex.Replace(slug, @"\s", "-"); // Replace spaces with hyphens
            return slug;
        }
    }
}
