using Hick.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Dal.Encryption;

namespace Hick
{
    public partial class Chat : Hick.Base.BasePage
    {
        EncryptDecryptUtil ecd = new EncryptDecryptUtil();

        string currentuser;
        public string userType;
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
                currentuser = Session["username"].ToString();
                string userid = Session["userid"].ToString();
                Userid.InnerHtml = userid;
                loginname.InnerHtml = Session["username"].ToString();
                userType = Convert.ToString(Session["UserType"]).ToLower().Trim();
                hdnusertype.InnerHtml = userType;

                //ShowTermsPopUp(Convert.ToString(Session["LastLoggedIN"]), Convert.ToInt32(userid),false);

                //DateTime dt = String.IsNullOrEmpty(Convert.ToString(Session["LastLoggedIN"])) ? DateTime.Now : Convert.ToDateTime(Session["LastLoggedIN"]);
                //datetimepicker.InnerHtml = dt.ToString("hh:mm tt");

            }
            else
            {
                Response.Redirect(Utility.LogOutUrl);
            }


        }
        
        [WebMethod]
        public static List<Users> getusers()
        {
            EncryptDecryptUtil ecd = new EncryptDecryptUtil();
            try
            {
                List<Users> userlist = new List<Users>();
                Users user = null;

                using (SqlConnection conn = new SqlConnection())
                {
                    //conn.ConnectionString = "Server=its-ba-dc02\\MSSQL2008R2_DEV;Database=hick_poc;Trusted_Connection=false;User Id=itsdev;Password=itsdev";
                    //"Server=its-ba-dc02\\MSSQL2008R2_DEV;Database=hick_poc;Trusted_Connection=false;User Id=itsdev;Password=itsdev";
                    conn.ConnectionString = Utility.DBConnectionString; 
                    conn.Open();

                    using (SqlCommand command = new SqlCommand(
            "select * from Hick_Users where Status=1 ",
            conn))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                user = new Users();
                                //User=reader["asd"]                        }
                                user.Username = ecd.DecryptData((reader["Username"].ToString()), ecd.GetEncryptType());
                                user.Firstname = ecd.DecryptData((reader["Firstname"].ToString()), ecd.GetEncryptType());
                                user.Status = Convert.ToInt32(reader["Status"]);
                                user.Lastname = ecd.DecryptData((reader["Lastname"].ToString()), ecd.GetEncryptType());
                                user.ID = Convert.ToInt32(reader["ID"]);
                                user.StatusMessage = reader["StatusMessage"].ToString();
                                user.Lastloggedin = reader["LastLoggedIN"].ToString();
                                userlist.Add(user);
                            }

                            return userlist;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [WebMethod]
        public static List<UserConversation> insertpeerconversation(string currentid, string peerid)
        {
            List<UserConversation> conversationlog = new List<UserConversation>();
            UserConversation conver = null;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = Utility.DBConnectionString;
                conn.Open();
                using (SqlCommand command = new SqlCommand(
            "select * from Hick_user_Conversation where (Initiator='" + currentid + "' or Answerer='" + currentid + "')and (Initiator='" + peerid + "' or Answerer='" + peerid + "')and (ConversationDate=convert(date, getdate()))", conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows == true)
                        {
                            while (reader.Read())
                            {
                                conver = new UserConversation();
                                conver.Id = Convert.ToInt64(reader["ID"]);
                                conver.Answerer = Convert.ToInt64(reader["Answerer"]);
                                conver.Initiator = Convert.ToInt64(reader["Initiator"]);
                                // conver.Date = Convert.ToInt64(reader["ConversationDate"]);
                                conversationlog.Add(conver);
                            }
                        }
                        else
                        {
                            string insertstring = @"insert into Hick_user_Conversation values('" + currentid + "','" + peerid + "',GETDATE()) ";
                            SqlCommand cmd = new SqlCommand(insertstring, conn);
                            SqlDataAdapter adp = new SqlDataAdapter();
                            adp.InsertCommand = cmd;
                            adp.InsertCommand.ExecuteNonQuery();
                        }

                        return conversationlog;
                    }
                }
            }
        }

        [WebMethod]
        public static List<ConversationLog> GetMessages(string conversationid)
        {
            try
            {
                string constr = Utility.DBConnectionString;
                List<ConversationLog> objlogColl = new List<ConversationLog>();
                ConversationLog objlog = null;

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = constr;
                    conn.Open();

                    using (SqlCommand command = new SqlCommand("select *,peeruser=(select Username from Hick_Users where ID=l.peerid) from Hick_Conversation_log as l where Conversation_date=CONVERT(date,GETDATE())", conn))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                objlog = new ConversationLog();
                                objlog.Id = Convert.ToInt32(reader["Id"]);
                                objlog.ConversationId = Convert.ToInt32(reader["Conversation_Id"]);
                                objlog.Conversation = Convert.ToString(reader["Conversation_log"]);
                                //objlog.ConversationDate = Convert.ToInt64(reader["Conversation_date"]);
                                objlogColl.Add(objlog);
                            }

                            return objlogColl;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
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
                cmd.Parameters.AddWithValue("@UserId",userid);
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.UpdateCommand = cmd;
                adp.UpdateCommand.ExecuteNonQuery();
                HttpContext.Current.Session["username"] = null;

            }
        }

        [WebMethod]
        public static void ExportChat(string conversationid)
        {
            try
            {
                string constr = Utility.DBConnectionString;
                List<ConversationLog> objlogColl = new List<ConversationLog>();
                ConversationLog objlog = null;

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = constr;
                    conn.Open();
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("select *,peeruser=(select top 1 Username from Hick_Users where ID=l.peerid) from Hick_Conversation_log as l");
                    sb.AppendLine("where l.Conversation_Id=" + conversationid + "");
                    sb.AppendLine("and (l.Message_Type=" + (int)MessageTypes.Text + " OR l.Message_Type=" + (int)MessageTypes.File + ")");
                    sb.AppendLine("order by l.Id asc");

                    using (SqlCommand command = new SqlCommand(Convert.ToString(sb), conn))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                objlog = new ConversationLog();
                                objlog.Id = Convert.ToInt32(reader["Id"]);
                                objlog.ConversationId = Convert.ToInt32(reader["Conversation_Id"]);
                                objlog.Conversation = Convert.ToString(reader["Conversation_log"]);
                                objlog.ConversationDate = Convert.ToString(reader["Conversation_date"]);
                                objlog.PeerName = Convert.ToString(reader["peeruser"]);
                                objlog.MessageType = Convert.ToInt32(reader["Message_Type"]);
                                objlog.ReadStatus = Convert.ToInt32(reader["Read_Status"]);

                                DateTime dt = Convert.ToDateTime(reader["Conversation_date"]);
                                objlog.Time = dt.ToString("hh:mm tt");
                                objlog.PeerID = Convert.ToInt32(reader["peerid"]);
                                objlogColl.Add(objlog);
                            }


                        }
                    }
                    if (objlogColl.Count > 0)
                    {
                        JavaScriptSerializer jss = new JavaScriptSerializer();
                        string output = jss.Serialize(objlogColl);
                        byte[] buffer = System.Text.Encoding.ASCII.GetBytes(output);
                        string _filnam = "Chat(" + DateTime.Now.ToShortDateString() + ").txt";
                        //context.Response.ContentType = "text/plain";
                        //context.Response.AddHeader("Content-Disposition", "attachment;filename=" + _filnam + "");
                        //context.Response.OutputStream.Write(buffer, 0, buffer.Length);                       
                        //HttpContext.Current.ApplicationInstance.CompleteRequest();
                        var callingPage = HttpContext.Current;
                        callingPage.Response.AddHeader("Content-Type", "text/plain");
                        callingPage.Response.Charset = "";
                        callingPage.Response.AppendHeader("Content-disposition", "attachment; filename=" + _filnam);
                        callingPage.Response.Write(output);
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                }

            }
            catch (Exception)
            {

                throw;
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

        //         Response.Redirect(Utility.LogOutUrl);
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
                string constr = Utility.DBConnectionString;
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = constr;
                    conn.Open();

                    using (SqlCommand command = new SqlCommand("select * from Hick_Users where ID='" + _userid + "'", conn))
                    {
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

                                    if (!string.IsNullOrEmpty(Convert.ToString(reader["LastLoggedIN"])))
                                    {
                                        string constring = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                                        using (SqlConnection connection = new SqlConnection())
                                        {
                                            connection.ConnectionString = constring;
                                            connection.Open();

                                            string updateString = @"update Hick_Users set Status='1',StatusMessage='Online', LastLoggedIN='" + DateTime.UtcNow + "' where ID='" + _userid + "'";
                                            SqlCommand cmd = new SqlCommand(updateString, connection);
                                            SqlDataAdapter adp = new SqlDataAdapter();
                                            adp.UpdateCommand = cmd;
                                            adp.UpdateCommand.ExecuteNonQuery();
                                        }
                                    }
                                    else
                                    {
                                        Response.Redirect("TermsConditions.aspx", false);
                                    }
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
            catch (Exception ex)
            {

                Response.Redirect(Utility.LogOutUrl);
            }
        }

        [WebMethod(EnableSession = true)]
        public static string GetLastLoggedInTime()
        {
            return Utility.LastLoggedInTime;
        }

        [WebMethod]
        public static void StoreTotalTxtChatDuration(string totaltxtchat, string txtchatduration, string conversationId)
        {
            TimeSpan totaltime = new TimeSpan();
            try
            {
                if (!string.IsNullOrEmpty(txtchatduration) && !string.IsNullOrEmpty(totaltxtchat))
                {
                    totaltime = TimeSpan.Parse(totaltxtchat) + TimeSpan.Parse(txtchatduration);
                }
                else if (string.IsNullOrEmpty(txtchatduration) && !string.IsNullOrEmpty(totaltxtchat))
                {
                    totaltime = TimeSpan.Parse(totaltxtchat);
                }
                else if (!string.IsNullOrEmpty(txtchatduration) && string.IsNullOrEmpty(totaltxtchat))
                {
                    totaltime = TimeSpan.Parse(txtchatduration);
                }

                if (!string.IsNullOrEmpty(conversationId) && (totaltime.Seconds > 0))
                {
                    totaltxtchat = totaltime.ToString();
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = Utility.DBConnectionString;
                        conn.Open();

                        //string updatetime = @"update Hick_user_Conversation set ischatOn=0, total_txtchat_dauration='" + totaltxtchat + "' where ID='" + conversationId + "' and ischatOn=1 and ConversationDate=convert(varchar(10), getdate(), 21)";
                        SqlCommand cmd = new SqlCommand("sp_hick_UpdateTxtChatDuration", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Duration", totaltxtchat);
                        cmd.Parameters.AddWithValue("@ConversationId", conversationId);
                        cmd.Parameters.AddWithValue("@Date", DateTime.UtcNow);

                        SqlDataAdapter adp = new SqlDataAdapter();
                        adp.UpdateCommand = cmd;
                        adp.UpdateCommand.ExecuteNonQuery();
                    }
                }

            }

            catch (Exception ex)
            {
                throw;
            }
        }

        [WebMethod]
        public static string UpdateIsChatOn(string conversationid)
        {
            try
            {
                string response = string.Empty;
                if (!string.IsNullOrEmpty(conversationid))
                {
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = Utility.DBConnectionString; 
                        conn.Open();

                        //string updatetime = @"update Hick_user_Conversation set ischatOn=1 where ID='" + conversationid + "' and (ischatOn=0 OR ischatOn IS NULL)";
                        SqlCommand cmd = new SqlCommand("sp_hick_UpdateIsChatOn", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ConversationId",conversationid);

                        SqlDataAdapter adp = new SqlDataAdapter();
                        adp.UpdateCommand = cmd;
                        int res = -1;
                        res = adp.UpdateCommand.ExecuteNonQuery();
                        if (res > 0)
                        {
                            response = "1";
                        }

                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [WebMethod]
        public static int SaveAudioCallLog(string conversationid, long initiator, long answerer, string phoneno, string starttime)
        {
            int res = -1;
            var igniteLogFile = Path.Combine(Utility.GetBaseDirectory(), "ignitelog.txt");
            try
            {
                if (!String.IsNullOrEmpty(starttime))
                {
                    if (!File.Exists(igniteLogFile))
                    {
                        File.Create(igniteLogFile).Close();
                    }

                    System.IO.File.AppendAllText(igniteLogFile, starttime);
                    DateTime dt = DateTime.Parse(starttime);
                    
                    if (dt != null && dt != DateTime.MinValue)
                    {
                        TimeSpan duration = DateTime.Now - dt;
                        string hrs = duration.Hours.ToString();
                        string mts = duration.Minutes.ToString();
                        string secs = duration.Seconds.ToString();
                        string constr = Utility.DBConnectionString;
                        using (SqlConnection conn = new SqlConnection())
                        {
                            System.IO.File.AppendAllText(igniteLogFile, constr);
                            conn.ConnectionString = constr;
                            conn.Open();


                            //StringBuilder sb = new StringBuilder();
                            //sb.AppendLine("INSERT INTO hick_audiocall_log (initiator,phone_number,conversation_date,start_time,end_time,call_duration)");
                            //sb.AppendLine("VALUES (@initiator,@pno,@date,@stime,@etime,@duration)");

                            using (SqlCommand cmd = new SqlCommand("sp_hick_SaveAudioCallLog", conn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;

                                //cmd.Parameters.AddWithValue("@conversationid", conversationid);
                                cmd.Parameters.AddWithValue("@Initiater", initiator);
                                cmd.Parameters.AddWithValue("@Answerer", answerer);
                                cmd.Parameters.AddWithValue("@PhoneNo", phoneno);
                                cmd.Parameters.AddWithValue("@Date", dt.ToShortDateString());
                                cmd.Parameters.AddWithValue("@StartTime", dt);
                                cmd.Parameters.AddWithValue("@EndTime", DateTime.Now);
                                cmd.Parameters.AddWithValue("@Duration", hrs + ":" + mts + ":" + secs);

                                res = cmd.ExecuteNonQuery();

                            }


                        }
                    }
                }

            }
            catch (Exception exe)
            {
                System.IO.File.AppendAllText(igniteLogFile, exe.Message);
            }
            return res;

        }
    }

}


