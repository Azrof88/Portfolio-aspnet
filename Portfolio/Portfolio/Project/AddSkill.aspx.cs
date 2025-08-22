using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace Portfolio.Project
{
    public partial class AddSkill : System.Web.UI.Page
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
            string query = @"INSERT INTO Skills 
                             (SkillName, Description, IconClass, Proficiency, GitHubURL) 
                             VALUES 
                             (@SkillName, @Description, @IconClass, @Proficiency, @GitHubURL)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@SkillName", txtSkillName.Text);
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@IconClass", txtIconClass.Text);
                    cmd.Parameters.AddWithValue("@Proficiency", Convert.ToInt32(txtProficiency.Text));
                    cmd.Parameters.AddWithValue("@GitHubURL", txtGitHubURL.Text);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        Response.Redirect("Admin.aspx");
                    }
                    catch (Exception ex)
                    {
                        lblStatus.Text = "Error saving skill: " + ex.Message;
                    }
                }
            }
        }
    }
}
