using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Serialization;
using Hick.Models;
using System.Data;
using Dal.Encryption;

namespace Hick
{
    public partial class Chat_log : Hick.Base.BasePage
    {
        EncryptDecryptUtil ecd = new EncryptDecryptUtil();
        public List<ExportChatLog> ExportChatList
        {
            get;
            set;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //lblversion.Text = Utility.AppVersion;
            /* For query string as guid(after hick is integrated)*/

            string sessionid = Convert.ToString(Request.QueryString["ssid"]);
            if (!String.IsNullOrEmpty(sessionid))
            {
                AuthenticateBridgeUser(sessionid);
            }

            /* For Query string as username(for demo purpose)*/
            //string name = Convert.ToString(Request.QueryString["username"]);           
            //if (!String.IsNullOrEmpty(name))
            //{
            //    AuthenticateBridgeUser(name.ToLower());

            //}

            if (Session["username"] != null)
            {
                loginname.InnerHtml = Session["username"].ToString();
                Userid.InnerHtml = Session["userid"].ToString();

                hdnusertype.InnerHtml = Convert.ToString(Session["UserType"]).ToLower().Trim();
                //DateTime dt = String.IsNullOrEmpty(Convert.ToString(Session["LastLoggedIN"])) ? DateTime.Now : Convert.ToDateTime(Session["LastLoggedIN"]);
                //datetimepicker.InnerHtml = dt.ToString("hh:mm tt");
            }
            else
            {
                Response.Redirect(Utility.LogOutUrl);
            }
        }

        [WebMethod]
        public static void Signout(string userid)
        {
            string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
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

            }
        }
        //public void AuthenticateBridgeUser(string username)
        //{

        //    try
        //    {
        //        Session.Abandon();
        //        string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
        //        using (SqlConnection conn = new SqlConnection())
        //        {
        //            conn.ConnectionString = constr;
        //            conn.Open();

        //            using (SqlCommand command = new SqlCommand("select top 1 * from Hick_Users where LOWER(Username)='" + username + "'", conn))
        //            {
        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    if (reader.HasRows)
        //                    {
        //                        while (reader.Read())
        //                        {
        //                            Session["userid"] = reader["ID"];
        //                            Session["username"] = reader["Username"];
        //                            Session["LastLoggedIN"] = Convert.ToString(reader["LastLoggedIN"]);

        //                            string updateString = @"update Hick_Users set Status='1',LastLoggedIN=GETDATE() where ID='" + Convert.ToInt32(reader["ID"]) + "'";
        //                            SqlCommand cmd = new SqlCommand(updateString, conn);
        //                            SqlDataAdapter adp = new SqlDataAdapter();
        //                            adp.UpdateCommand = cmd;
        //                            adp.UpdateCommand.ExecuteNonQuery();
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Response.Redirect(Utility.LogOutUrl);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {

        //       Response.Redirect(Utility.LogOutUrl);
        //    }
        //}

        [WebMethod(EnableSession = true)]
        public static void ModifyChatForExport(string strjson)
        {
            if (!String.IsNullOrEmpty(strjson))
            {
                HttpContext.Current.Session["ExportChatColl"] = null;
                List<ExportChatLog> objcoll = new JavaScriptSerializer().Deserialize<List<ExportChatLog>>(strjson);
                HttpContext.Current.Session["ExportChatColl"] = objcoll;


            }
        }
        [WebMethod(EnableSession = true)]
        public static void StoreTimeZoneToSession(string timezone)
        {
            if (!String.IsNullOrEmpty(timezone))
            {
                HttpContext.Current.Session["TimeZone"] = null;
                HttpContext.Current.Session["TimeZone"] = timezone;


            }
        }
        public void AuthenticateBridgeUser(string sessionid)
        {

            try
            {
                Session.Clear();
                long _userid = long.Parse(Utility.DecryptText(sessionid.Split('-')[0].Trim()));
                string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = constr;
                    conn.Open();

                    //using (SqlCommand command = new SqlCommand("select * from Hick_Users where ID='" + _userid + "'", conn))
                    using (SqlCommand command = new SqlCommand("sp_hick_FetchUserByID", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId",_userid);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Session["userid"] = reader["ID"];
                                    Session["username"] = ecd.DecryptData((reader["Username"].ToString()), ecd.GetEncryptType());
                                    Session["LastLoggedIN"] = Convert.ToString(reader["LastLoggedIN"]);
                                    Session["UserType"] = Convert.ToString(reader["user_type"]);

                                    //string updateString = @"update Hick_Users set Status='1',LastLoggedIN='"+DateTime.UtcNow+"', StatusMessage='Online' where ID='" + _userid + "'";
                                    SqlCommand cmd = new SqlCommand("sp_hickUpdateUserStatusMessageOnline", conn);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    //cmd.Parameters.AddWithValue("@UserName", "");
                                    cmd.Parameters.AddWithValue("@UserId", _userid);
                                    cmd.Parameters.AddWithValue("@LastLoggedIN", DateTime.UtcNow);
                                    cmd.Parameters.AddWithValue("@TcAcceptDate", string.Empty);

                                    //SqlDataAdapter adp = new SqlDataAdapter();
                                    //adp.UpdateCommand = cmd;
                                    //adp.UpdateCommand.ExecuteNonQuery();

                                    cmd.ExecuteScalar();
                                }
                            }
                            else
                            {
                                Response.Redirect(Utility.LogOutUrl);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect(Utility.LogOutUrl);
            }
        }

        [WebMethod(EnableSession = true)]
        public static string GetLastLoggedInTime()
        {
            return Utility.LastLoggedInTime;
        }
    }
}

