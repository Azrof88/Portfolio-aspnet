using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portfolio.Project
{
    public partial class Admin : System.Web.UI.Page
    {
        // Handles the "Add New Blog Post" button click
        protected void btnAddNewPost_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddPost.aspx");
        }

        // Handles the "Edit" link click in the blog grid
        protected void gvBlogPosts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string postId = gvBlogPosts.DataKeys[e.NewEditIndex].Value.ToString();
            Response.Redirect("EditPost.aspx?id=" + postId);
        }

        // Handles the "Delete" link click in the blog grid
        protected void gvBlogPosts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int postId = Convert.ToInt32(gvBlogPosts.DataKeys[e.RowIndex].Value);
            string connectionString = ConfigurationManager.AppSettings["DbConnectionString"];
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM BlogPosts WHERE PostID = @PostID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@PostID", postId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            BindData(); // Refresh the grid
        }
        // Handles the "Add New Testimonial" button click
        protected void btnAddNewTestimonial_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddTestimonial.aspx");
        }

        // Handles the "Edit" link click in the testimonials grid
        protected void gvTestimonials_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string testimonialId = gvTestimonials.DataKeys[e.NewEditIndex].Value.ToString();
            Response.Redirect("EditTestimonial.aspx?id=" + testimonialId);
        }

        // Handles the "Delete" link click in the testimonials grid
        protected void gvTestimonials_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int testimonialId = Convert.ToInt32(gvTestimonials.DataKeys[e.RowIndex].Value);
            string connectionString = ConfigurationManager.AppSettings["DbConnectionString"];
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Testimonials WHERE ID = @ID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", testimonialId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            BindData(); // Refresh the grid
        }
        // Handles the "Add New Skill" button click
        protected void btnAddNewSkill_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddSkill.aspx");
        }

        // Handles the "Edit" link click in the skills grid
        protected void gvSkills_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string skillId = gvSkills.DataKeys[e.NewEditIndex].Value.ToString();
            Response.Redirect("EditSkill.aspx?id=" + skillId);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IsAdmin"] == null || (bool)Session["IsAdmin"] == false)
            {
                // ...check if they have a "Remember Me" cookie.
                if (Request.Cookies["AdminAuth"] != null)
                {
                    // If the cookie has the correct value, log them in by setting the session.
                    if (Request.Cookies["AdminAuth"]["IsAdmin"] == "true")
                    {
                        Session["IsAdmin"] = true;
                    }
                }
            }

            if (Session["IsAdmin"] != null && (bool)Session["IsAdmin"] == true)
            {
                LoginPanel.Visible = false;
                AdminContentPanel.Visible = true;
                if (!IsPostBack)
                {
                    BindData();
                }
            }
            else
            {
                LoginPanel.Visible = true;
                AdminContentPanel.Visible = false;
                // ADD THIS LINE
                (this.Master.FindControl("pageWrapper") as System.Web.UI.HtmlControls.HtmlGenericControl).Attributes["class"] += " login-mode";
            }
        }

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

                    // 3. NEW: Fetch and bind Timeline Events
                    SqlDataAdapter timelineAdapter = new SqlDataAdapter("SELECT ID, EventType, Title FROM TimelineEvents ORDER BY ID DESC", con);
                    DataTable timelineTable = new DataTable();
                    timelineAdapter.Fill(timelineTable);
                    gvTimeline.DataSource = timelineTable;
                    gvTimeline.DataBind();

                    // NEW: Fetch and bind Testimonials
                    SqlDataAdapter testimonialsAdapter = new SqlDataAdapter("SELECT ID, Quote, AuthorName FROM Testimonials", con);
                    DataTable testimonialsTable = new DataTable();
                    testimonialsAdapter.Fill(testimonialsTable);
                    gvTestimonials.DataSource = testimonialsTable;
                    gvTestimonials.DataBind();

                    // NEW: Fetch and bind Blog Posts
                    SqlDataAdapter blogAdapter = new SqlDataAdapter("SELECT PostID, Title, PublishDate FROM BlogPosts ORDER BY PublishDate DESC", con);
                    DataTable blogTable = new DataTable();
                    blogAdapter.Fill(blogTable);
                    gvBlogPosts.DataSource = blogTable;
                    gvBlogPosts.DataBind();

                    // Inside the BindData() method, within the try block...

                    // NEW: Fetch and bind Contact Messages
                    SqlDataAdapter messagesAdapter = new SqlDataAdapter("SELECT Id, Name, Email, Message, DateSent FROM ContactMessages ORDER BY DateSent DESC", con);
                    DataTable messagesTable = new DataTable();
                    messagesAdapter.Fill(messagesTable);
                    gvMessages.DataSource = messagesTable;
                    gvMessages.DataBind();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Admin Data Load Error: " + ex.Message);
                }
            }
        }

        // --- EVENT HANDLERS FOR BUTTON CLICKS ---
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string adminUser = "admin";
            string adminPass = "password123";
            if (txtUsername.Text == adminUser && txtPassword.Text == adminPass)
            {
                Session["IsAdmin"] = true;
                // --- ADD THIS COOKIE LOGIC ---
                if (chkRememberMe.Checked)
                {
                    // Create a new cookie
                    HttpCookie authCookie = new HttpCookie("AdminAuth");
                    // Add a value to it (for this example, a simple true)
                    authCookie.Values["IsAdmin"] = "true";
                    // Set the cookie to expire in 14 days
                    authCookie.Expires = DateTime.Now.AddDays(14);
                    // Add the cookie to the browser
                    Response.Cookies.Add(authCookie);
                }
                // ----
                Response.Redirect("Admin.aspx");
            }
            else
            {
                lblError.Text = "Invalid username or password.";
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Admin.aspx");
        }

        protected void btnAddNewProject_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddProject.aspx");
        }

        // NEW: Event handler for adding a new timeline event
        protected void btnAddNewEvent_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddTimelineEvent.aspx");
        }


        // --- EVENT HANDLERS FOR GRIDVIEW ACTIONS ---
        protected void gvProjects_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string projectId = gvProjects.DataKeys[e.NewEditIndex].Value.ToString();
            Response.Redirect("EditProject.aspx?id=" + projectId);
        }

        protected void gvProjects_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int projectId = Convert.ToInt32(gvProjects.DataKeys[e.RowIndex].Value);
            string connectionString = ConfigurationManager.AppSettings["DbConnectionString"];
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Projects WHERE ID = @ID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", projectId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            BindData();
        }

        protected void gvSkills_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int skillId = Convert.ToInt32(gvSkills.DataKeys[e.RowIndex].Value);
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
            BindData();
        }

        // NEW: Event handlers for the timeline GridView
        protected void gvTimeline_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Get the ID of the event to edit from the GridView row.
            string eventId = gvTimeline.DataKeys[e.NewEditIndex].Value.ToString();

            // Redirect the user to the EditTimelineEvent page, passing the ID in the URL.
            Response.Redirect("EditTimelineEvent.aspx?id=" + eventId);
        }

        protected void gvTimeline_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int eventId = Convert.ToInt32(gvTimeline.DataKeys[e.RowIndex].Value);
            string connectionString = ConfigurationManager.AppSettings["DbConnectionString"];
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM TimelineEvents WHERE ID = @ID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", eventId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            BindData();
        }
    }
}
