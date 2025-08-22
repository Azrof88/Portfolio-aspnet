using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace Portfolio.Project
{
    public partial class EditTimelineEvent : System.Web.UI.Page
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
                LoadEventData();
            }
        }

        private void LoadEventData()
        {
            if (Request.QueryString["id"] != null)
            {
                string eventId = Request.QueryString["id"];
                string query = "SELECT * FROM TimelineEvents WHERE ID = @ID";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ID", eventId);
                        try
                        {
                            con.Open();
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    ddlEventType.SelectedValue = reader["EventType"].ToString();
                                    txtTitle.Text = reader["Title"].ToString();
                                    txtInstitution.Text = reader["Institution"].ToString();
                                    txtDateRange.Text = reader["DateRange"].ToString();
                                    txtDescription.Text = reader["Description"].ToString();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            lblStatus.Text = "Error loading event data: " + ex.Message;
                        }
                    }
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                string eventId = Request.QueryString["id"];
                string query = @"UPDATE TimelineEvents SET 
                                 EventType = @EventType, 
                                 Title = @Title, 
                                 Institution = @Institution, 
                                 DateRange = @DateRange, 
                                 Description = @Description 
                                 WHERE ID = @ID";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@EventType", ddlEventType.SelectedValue);
                        cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                        cmd.Parameters.AddWithValue("@Institution", txtInstitution.Text);
                        cmd.Parameters.AddWithValue("@DateRange", txtDateRange.Text);
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                        cmd.Parameters.AddWithValue("@ID", eventId);

                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            Response.Redirect("Admin.aspx");
                        }
                        catch (Exception ex)
                        {
                            lblStatus.Text = "Error updating event: " + ex.Message;
                        }
                    }
                }
            }
        }
    }
}
