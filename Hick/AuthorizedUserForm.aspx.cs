using Hick.Authorized;
using Hick.Models;
using IGNITE.DBUtility;
using IGNITE_MODEL.AutherizedUser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick
{
    public partial class AuthorizedUserForm : System.Web.UI.Page
    {
        AuthorizedService service = new AuthorizedService();
        protected void Page_Load(object sender, EventArgs e)
        {
          

            if (!Page.IsPostBack)
            {
                hdncurrentuser.Value = Session["userid"].ToString();
                BindAutherizedUserFor();
            }

        }

        public void BindAutherizedUserFor()
        {
            if(Session["userid"]!=null)
            {
                AutherizedUsersList AutherizedUsers = new AutherizedUsersList();
                AutherizedUsers = service.GetAutherizedUsersFor(Convert.ToInt32(Session["userid"].ToString()));
                gdautherizeduserfor.DataSource = AutherizedUsers.AutherizedUsers;
                gdautherizeduserfor.DataBind();
            }
            else
            {
                Server.Transfer("Index.aspx", true);
            }
        }

        [WebMethod(EnableSession = true)]
        public static void Signout(string userid)
        {
            string constr = Utility.DBConnectionString;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                conn.Open();

                //string updateString = @"update Hick_Users set Status='0', StatusMessage='Offline' where ID='" + userid + "' ";

                SqlCommand cmd = new SqlCommand("sp_hick_UpdateUserOffline", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userid);
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.UpdateCommand = cmd;
                adp.UpdateCommand.ExecuteNonQuery();
                HttpContext.Current.Session["username"] = null;

            }
        }

        protected void download(object sender, CommandEventArgs e)
        {
                var userId = e.CommandArgument.ToString();
                var auditresult = service.AddAuditLog(Convert.ToInt32(Session["userid"].ToString()), 1, "PHP", "Not Applicable", "Not Applicable", DateTime.Now);
                WebClient myClient = new WebClient();
                myClient.UseDefaultCredentials = true;
                string MyUrl = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port;
                MyUrl = MyUrl + Request.Url.AbsolutePath.Replace("AuthorizedUserForm.aspx", "");
                string currentPageUrl = MyUrl + "PatientLookUp/ASPX/ViewPHPSummery.aspx?id="+ userId;
                HttpContext.Current.Response.ContentType = "text/html";
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=PHPReport.html");
                string htmlstring = myClient.DownloadString(currentPageUrl);
                HttpContext.Current.Response.Write(htmlstring);
                HttpContext.Current.Response.End();

        }

    }
}