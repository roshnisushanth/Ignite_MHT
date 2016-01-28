using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hick.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Hick
{
    public partial class TermsConditions : Hick.Base.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAccept_click(object sender, EventArgs e)
        {
            Int32 userid = Int32.Parse(Session["userid"].ToString());
            string name = Convert.ToString(HttpContext.Current.Session["username"]);
            string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                conn.Open();

                //string updateString = @"update Hick_Users set Status='1',StatusMessage='Online',LastLoggedIN='" + DateTime.UtcNow + "', tc_accepteddate='" + DateTime.UtcNow + "' where Username='" + name + "'; select LastLoggedIN from Hick_Users where Username='" + name + "'";
                SqlCommand cmd = new SqlCommand("sp_hickUpdateUserStatusMessageOnline", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LastLoggedIN",DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@UserId",userid);
                cmd.Parameters.AddWithValue("@TcAcceptDate",DateTime.UtcNow);

                //SqlDataAdapter adp = new SqlDataAdapter();
                //adp.UpdateCommand = cmd;
                //adp.UpdateCommand.ExecuteNonQuery();
                HttpContext.Current.Session["LastLoggedIN"] = cmd.ExecuteScalar();
            }

            Response.Redirect("Chat.aspx");
        }

        protected void btnCancel_click(object sender, EventArgs e)
        {
            Response.Redirect(Utility.LogOutUrl);
        }
    }
}