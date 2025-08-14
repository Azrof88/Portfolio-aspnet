using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace Portfolio.Project
{
    public partial class AddProject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Security Check: If the user is not logged in, redirect them away.
            if (Session["IsAdmin"] == null || (bool)Session["IsAdmin"] == false)
            {
                Response.Redirect("Admin.aspx");
            }
        }

        // RENAMED this method to match the OnClick event in the .aspx file
        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Get the connection string from Web.config
            string connectionString = ConfigurationManager.AppSettings["DbConnectionString"];

            // Define the SQL INSERT command
            string query = @"INSERT INTO Projects 
                     (Title, Category, ImageURL, Description, GitHubURL, TechStack) 
                     VALUES 
                     (@Title, @Category, @ImageURL, @Description, @GitHubURL, @TechStack)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameters to prevent SQL injection
                    cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                    cmd.Parameters.AddWithValue("@Category", txtCategory.Text);
                    cmd.Parameters.AddWithValue("@ImageURL", txtImageURL.Text);
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@GitHubURL", txtGitHubURL.Text);
                    cmd.Parameters.AddWithValue("@TechStack", txtTechStack.Text);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery(); // Execute the command

                        // Redirect back to the main admin page after a successful save
                        Response.Redirect("Admin.aspx");
                    }
                    catch (Exception ex)
                    {
                        // Show an error message if something goes wrong
                        lblStatus.ForeColor = System.Drawing.Color.Red;
                        lblStatus.Text = "Error saving project: " + ex.Message;
                    }
                }
            }
        }
    }
}
