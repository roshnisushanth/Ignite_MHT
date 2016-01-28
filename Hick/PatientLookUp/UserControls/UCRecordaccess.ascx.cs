 using Hick.Authorized;
using IGNITE.DBUtility;
using IGNITE_MODEL.AutherizedUser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.PatientLookUp.UserControls
{
    public partial class UCRecordaccess : System.Web.UI.UserControl
    {
        AuthorizedService service = new AuthorizedService();
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hdncurrentuser.Value = Session["userid"].ToString();
                BindAutherizedUser();
                BindAutherizedUserFor();
                
            }
        }

        public void BindAutherizedUser()
        {
            AutherizedUsersList AutherizedUsers = new AutherizedUsersList();
            AutherizedUsers = service.GetAutherizedUsers("SelectAll", Convert.ToInt32(hdncurrentuser.Value), 0);
            gdautherizeduser.DataSource = AutherizedUsers.AutherizedUsers;
            gdautherizeduser.DataBind();
        }
        public void BindAutherizedUserFor()
        {
            AutherizedUsersList AutherizedUsers = new AutherizedUsersList();
            AutherizedUsers = service.GetAutherizedUsersFor(Convert.ToInt32(hdncurrentuser.Value));
            gdautherizeduserfor.DataSource = AutherizedUsers.AutherizedUsers;
            gdautherizeduserfor.DataBind();
        }

        protected void download(object sender, CommandEventArgs e)
        {
           var userId = e.CommandArgument.ToString();
                    string result = CheckOnetimePassword(Session["userid"].ToString(), userId);
           if (result == "Sucessful")
           {
                var auditresult = service.AddAuditLog(Convert.ToInt32(Session["userid"].ToString()), 1, "PHP", "Not Applicable", "Not Applicable", DateTime.Now);

                WebClient myClient = new WebClient();
                myClient.UseDefaultCredentials = true;
                string MyUrl = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port;
                MyUrl = MyUrl + Request.Url.AbsolutePath.Replace("Recordaccess.aspx", "");
                string currentPageUrl = MyUrl + "ViewPHPSummery.aspx?id="+hdnReferenceId.Value;
                HttpContext.Current.Response.ContentType = "text/html";
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=PHPReport.html");
                string htmlstring = myClient.DownloadString(currentPageUrl);
                HttpContext.Current.Response.Write(htmlstring);
                HttpContext.Current.Response.End();


            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function",
               "ShowPasswordPopup();", true);
            }
      // view 0 , download 1, transmit 2

        }

        public static string CheckOnetimePassword(string userid, string authorizeduserid)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
              new SqlParameter("@UserId",userid),
               new SqlParameter("@Autherizeduserid",authorizeduserid)

            };

            var obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "sp_hick_GetOneTimeAccessCodeStatus", sqlParms);
            if ((int)obj == 1)
            {
                return "Sucessful";
            }
            else
            {
                return "UnSucessful";
            }


        }
    }
}