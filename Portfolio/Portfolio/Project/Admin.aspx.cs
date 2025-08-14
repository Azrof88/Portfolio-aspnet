using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portfolio.Project
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void gvProjects_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Get the ID of the project to edit from the GridView row.
            string projectId = gvProjects.DataKeys[e.NewEditIndex].Value.ToString();

            // Redirect the user to the EditProject page, passing the ID in the URL.
            Response.Redirect("EditProject.aspx?id=" + projectId);
        }
        protected void btnAddNewProject_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddProject.aspx");
        }
        protected void gvProjects_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // 1. Get the ID of the project to delete from the GridView row.
            int projectId = Convert.ToInt32(gvProjects.DataKeys[e.RowIndex].Value);

            // 2. Set up the connection and the DELETE command.
            string connectionString = ConfigurationManager.AppSettings["DbConnectionString"];
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Use a parameterized query to prevent SQL injection attacks.
                string query = "DELETE FROM Projects WHERE ID = @ID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", projectId);
                    con.Open();
                    cmd.ExecuteNonQuery(); // Execute the command
                }
            }

            // 3. Re-bind the data to the grid to show the updated list.
            BindData();
        }

        protected void gvSkills_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // 1. Get the ID of the skill to delete.
            int skillId = Convert.ToInt32(gvSkills.DataKeys[e.RowIndex].Value);

            // 2. Set up the connection and the DELETE command.
            string connectionString = ConfigurationManager.AppSettings["DbConnectionString"];
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Skills WHERE ID = @ID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", skillId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            // 3. Re-bind the data to refresh the grid.
            BindData();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is already logged in by looking for the Session variable.
            if (Session["IsAdmin"] != null && (bool)Session["IsAdmin"] == true)
            {
                // If they are logged in:
                LoginPanel.Visible = false;
                AdminContentPanel.Visible = true;

                // NEW: Load and bind data to the grids, but only on the initial page load.
                if (!IsPostBack)
                {
                    BindData();
                }
            }
            else
            {
                // If they are NOT logged in:
                LoginPanel.Visible = true;
                AdminContentPanel.Visible = false;
            }
        }

        // --- NEW METHOD TO BIND DATABASE DATA ---
        private void BindData()
        {
            string connectionString = ConfigurationManager.AppSettings["DbConnectionString"];
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // 1. Fetch and bind Projects
                    SqlDataAdapter projectsAdapter = new SqlDataAdapter("SELECT ID, Title, Category FROM Projects", con);
                    DataTable projectsTable = new DataTable();
                    projectsAdapter.Fill(projectsTable);
                    gvProjects.DataSource = projectsTable;
                    gvProjects.DataBind();

                    // 2. Fetch and bind Skills
                    SqlDataAdapter skillsAdapter = new SqlDataAdapter("SELECT ID, SkillName, Proficiency FROM Skills", con);
                    DataTable skillsTable = new DataTable();
                    skillsAdapter.Fill(skillsTable);
                    gvSkills.DataSource = skillsTable;
                    gvSkills.DataBind();
                }
                catch (Exception ex)
                {
                    // Handle any database errors
                    System.Diagnostics.Debug.WriteLine("Admin Data Load Error: " + ex.Message);
                    // You could show an error message on the page here
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string adminUser = "admin";
            string adminPass = "password123";

            if (txtUsername.Text == adminUser && txtPassword.Text == adminPass)
            {
                Session["IsAdmin"] = true;
                Response.Redirect("Admin.aspx");
            }
            else
            {
                lblError.Text = "Invalid username or password.";
            }
        }

        // --- NEW METHOD FOR LOGOUT BUTTON ---
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Clear the session and redirect to the login page
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Admin.aspx");
        }
    }
}
