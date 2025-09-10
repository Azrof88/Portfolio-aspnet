using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portfolio.Project
{
    // CORRECTED: Inherits from MasterPage and class name is AdminMaster
    public partial class AdminMaster : System.Web.UI.MasterPage 
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // This is the correct place for the logout button's code
        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            if (Request.Cookies["AdminAuth"] != null)
            {
                // Expire the cookie by setting its expiration date to the past
                HttpCookie authCookie = new HttpCookie("AdminAuth");
                authCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(authCookie);
            }
            Response.Redirect("Admin.aspx");
        }
    }
}