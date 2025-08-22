using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;

namespace Portfolio.Project
{
    public partial class Post : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["slug"] != null)
            {
                LoadPost(Request.QueryString["slug"]);
            }
            else
            {
                litPostContent.Text = "<p class='text-danger'>No post specified.</p>";
            }
        }

        private void LoadPost(string slug)
        {
            string connectionString = ConfigurationManager.AppSettings["DbConnectionString"];
            StringBuilder postHtml = new StringBuilder();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Title, Content, PublishDate FROM BlogPosts WHERE Slug = @Slug";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Slug", slug);
                    try
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Set the page's title to the post title
                                this.Title = reader["Title"].ToString();

                                postHtml.AppendFormat("<h1>{0}</h1>", reader["Title"]);
                                postHtml.AppendFormat("<p class='text-muted mb-4'>Published on {0:MMMM dd, yyyy}</p>", (DateTime)reader["PublishDate"]);
                                postHtml.Append("<hr/>");
                                // The content is rendered directly as HTML
                                postHtml.AppendFormat("<div class='post-body mt-4'>{0}</div>", reader["Content"]);
                            }
                            else
                            {
                                postHtml.Append("<p class='text-danger'>Post not found.</p>");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        postHtml.Append("<p class='text-danger'>Error loading post.</p>");
                    }
                }
            }
            litPostContent.Text = postHtml.ToString();
        }
    }
}
