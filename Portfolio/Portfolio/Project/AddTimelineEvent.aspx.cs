using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portfolio.Project
{
    public partial class AddTimelineEvent : System.Web.UI.Page
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
            string query = @"INSERT INTO TimelineEvents 
                             (EventType, Title, Institution, DateRange, Description) 
                             VALUES 
                             (@EventType, @Title, @Institution, @DateRange, @Description)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@EventType", ddlEventType.SelectedValue);
                    cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                    cmd.Parameters.AddWithValue("@Institution", txtInstitution.Text);
                    cmd.Parameters.AddWithValue("@DateRange", txtDateRange.Text);
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        Response.Redirect("Admin.aspx");
                    }
                    catch (Exception ex)
                    {
                        lblStatus.Text = "Error saving event: " + ex.Message;
                    }
                }
            }
        }
    }
}