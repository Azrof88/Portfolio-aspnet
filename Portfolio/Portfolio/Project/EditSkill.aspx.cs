using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace Portfolio.Project
{
    public partial class EditSkill : System.Web.UI.Page
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
                // Only load the skill data the first time the page is visited.
                LoadSkillData();
            }
        }

        // This method fetches the data for the selected skill and fills the form.
        private void LoadSkillData()
        {
            if (Request.QueryString["id"] != null)
            {
                string skillId = Request.QueryString["id"];
                string query = "SELECT * FROM Skills WHERE ID = @ID";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ID", skillId);
                        try
                        {
                            con.Open();
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Populate the form fields with data from the database
                                    txtSkillName.Text = reader["SkillName"].ToString();
                                    txtDescription.Text = reader["Description"].ToString();
                                    txtIconClass.Text = reader["IconClass"].ToString();
                                    txtProficiency.Text = reader["Proficiency"].ToString();
                                    txtGitHubURL.Text = reader["GitHubURL"].ToString();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            lblStatus.Text = "Error loading skill data: " + ex.Message;
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
                string skillId = Request.QueryString["id"];
                string query = @"UPDATE Skills SET 
                                 SkillName = @SkillName, 
                                 Description = @Description, 
                                 IconClass = @IconClass, 
                                 Proficiency = @Proficiency, 
                                 GitHubURL = @GitHubURL 
                                 WHERE ID = @ID";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@SkillName", txtSkillName.Text);
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                        cmd.Parameters.AddWithValue("@IconClass", txtIconClass.Text);
                        cmd.Parameters.AddWithValue("@Proficiency", Convert.ToInt32(txtProficiency.Text));
                        cmd.Parameters.AddWithValue("@GitHubURL", txtGitHubURL.Text);
                        cmd.Parameters.AddWithValue("@ID", skillId);

                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            Response.Redirect("Admin.aspx");
                        }
                        catch (Exception ex)
                        {
                            lblStatus.Text = "Error updating skill: " + ex.Message;
                        }
                    }
                }
            }
        }
    }
}
