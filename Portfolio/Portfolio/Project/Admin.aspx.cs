using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portfolio.Project
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is already logged in by looking for the Session variable.
            if (Session["IsAdmin"] != null && (bool)Session["IsAdmin"] == true)
            {
                // If they are logged in:
                // 1. Hide the login form.
                LoginPanel.Visible = false;
                // 2. Show the secure admin content.
                AdminContentPanel.Visible = true;

                // We will add the code to load projects here later.
            }
            else
            {
                // If they are NOT logged in:
                // 1. Show the login form.
                LoginPanel.Visible = true;
                // 2. Hide the secure admin content.
                AdminContentPanel.Visible = false;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // For simplicity, we are hardcoding the admin credentials.
            // In a real application, these would be checked against a database.
            string adminUser = "admin";
            string adminPass = "password123";

            // Check if the entered username and password are correct.
            if (txtUsername.Text == adminUser && txtPassword.Text == adminPass)
            {
                // If credentials are correct:
                // 1. Create a Session variable to mark the user as logged in.
                //    ASP.NET automatically handles creating a secure cookie for this session.
                Session["IsAdmin"] = true;

                // 2. Reload the page. The Page_Load event will now see the Session
                //    variable and show the secure admin content.
                Response.Redirect("Admin.aspx");
            }
            else
            {
                // If credentials are incorrect, show an error message.
                lblError.Text = "Invalid username or password.";
            }
        }
    }
}
