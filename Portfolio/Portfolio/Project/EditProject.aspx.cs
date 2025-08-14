using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace Portfolio.Project
{
    public partial class EditProject : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.AppSettings["DbConnectionString"];

        protected void Page_Load(object sender, EventArgs e)
        {
            // Security Check: If the user is not logged in, redirect them away.
            if (Session["IsAdmin"] == null || (bool)Session["IsAdmin"] == false)
            {
                Response.Redirect("Admin.aspx");
            }

            if (!IsPostBack)
            {
                // Only load the project data the first time the page is visited.
                LoadProjectData();
            }
        }

        private void LoadProjectData()
        {
            // Get the project ID from the URL (e.g., EditProject.aspx?id=5)
            if (Request.QueryString["id"] != null)
            {
                string projectId = Request.QueryString["id"];
                string query = "SELECT * FROM Projects WHERE ID = @ID";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ID", projectId);
                        try
                        {
                            con.Open();
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Populate the form fields with data from the database
                                    txtTitle.Text = reader["Title"].ToString();
                                    txtCategory.Text = reader["Category"].ToString();
                                    txtImageURL.Text = reader["ImageURL"].ToString();
                                    txtDescription.Text = reader["Description"].ToString();
                                    txtGitHubURL.Text = reader["GitHubURL"].ToString();
                                    txtTechStack.Text = reader["TechStack"].ToString();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            lblStatus.ForeColor = System.Drawing.Color.Red;
                            lblStatus.Text = "Error loading project data: " + ex.Message;
                        }
                    }
                }
            }
        }

        // RENAMED this method to match the OnClick event in the .aspx file
        // This method runs when the "Update Project" button is clicked on the EditProject page.
        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Check if we have a project ID from the URL
            if (Request.QueryString["id"] != null)
            {
                string projectId = Request.QueryString["id"];

                // Define the SQL UPDATE command
                string query = @"UPDATE Projects SET 
                         Title = @Title, 
                         Category = @Category, 
                         ImageURL = @ImageURL, 
                         Description = @Description, 
                         GitHubURL = @GitHubURL, 
                         TechStack = @TechStack 
                         WHERE ID = @ID";

                string connectionString = ConfigurationManager.AppSettings["DbConnectionString"];
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add all parameters to prevent SQL injection
                        cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                        cmd.Parameters.AddWithValue("@Category", txtCategory.Text);
                        cmd.Parameters.AddWithValue("@ImageURL", txtImageURL.Text);
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                        cmd.Parameters.AddWithValue("@GitHubURL", txtGitHubURL.Text);
                        cmd.Parameters.AddWithValue("@TechStack", txtTechStack.Text);
                        cmd.Parameters.AddWithValue("@ID", projectId);

                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery(); // Execute the UPDATE command

                            // Redirect back to the main admin page after a successful update
                            Response.Redirect("Admin.aspx");
                        }
                        catch (Exception ex)
                        {
                            // Show an error message if something goes wrong
                            lblStatus.ForeColor = System.Drawing.Color.Red;
                            lblStatus.Text = "Error updating project: " + ex.Message;
                        }
                    }
                }
            }
        }

    }
}
