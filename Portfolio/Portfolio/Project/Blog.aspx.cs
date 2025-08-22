using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;

namespace Portfolio.Project
{
    public partial class Blog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadBlogPosts();
            }
        }

        private void LoadBlogPosts()
        {
            string connectionString = ConfigurationManager.AppSettings["DbConnectionString"];
            StringBuilder postsHtml = new StringBuilder();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Title, Slug, Excerpt, PublishDate FROM BlogPosts ORDER BY PublishDate DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                postsHtml.Append("<div class='col-md-8 offset-md-2 mb-4'>");
                                postsHtml.Append("<div class='card blog-card'>");
                                postsHtml.Append("<div class='card-body'>");
                                postsHtml.AppendFormat("<h3 class='card-title'>{0}</h3>", reader["Title"]);
                                postsHtml.AppendFormat("<p class='card-subtitle mb-2 text-muted'>Published on {0:MMMM dd, yyyy}</p>", (DateTime)reader["PublishDate"]);
                                postsHtml.AppendFormat("<p class='card-text'>{0}</p>", reader["Excerpt"]);
                                postsHtml.AppendFormat("<a href='Post.aspx?slug={0}' class='btn btn-outline-primary'>Read More</a>", reader["Slug"]);
                                postsHtml.Append("</div></div></div>");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        litBlogPosts.Text = "<p class='text-danger'>Could not load blog posts at this time.</p>";
                    }
                }
            }
            litBlogPosts.Text = postsHtml.ToString();
        }
    }
}
