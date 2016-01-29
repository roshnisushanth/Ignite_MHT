using Hick.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Web;
using System.Data;
using IGNITE_MODEL;
using IGNITE_BLL;
using IGNITE.DBUtility;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Dal.Encryption;
using System.Globalization;

namespace Hick
{
    public enum MessageTypes
    {
        Text = 1,
        Video = 2,
        File = 3
    }
    public enum ReadStatus
    {
        /*Text Messages*/
        Read = 1,
        UnRead = 2,
        /* File Transfer*/
        Transfered = 3,
        Declined = 4,
        Received = 5,
        /*Video Call*/
        CallInitiated = 6,
        CallEnded = 7
    }
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "HickChatEngine" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select HickChatEngine.svc or HickChatEngine.svc.cs at the Solution Explorer and start debugging.
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class HickChatEngine
    {
        EncryptDecryptUtil ecd = new EncryptDecryptUtil();
        [OperationContract]
        public void UpdateConsented(long patientId,string fileExt)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                         new SqlParameter("Id",patientId),
                         new SqlParameter("Ext",fileExt)
                    };

            SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "UpdateConsented", sqlParms.ToArray());
        }

        [OperationContract]
        public List<ConsentedUsers> GetConsentedUsers(long currentuserid, int favouriteUsers, int PageIndex, int PageSize, string usertype)
        {
            try
            {

                string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                List<ConsentedUsers> userlist = new List<ConsentedUsers>();

                using (SqlConnection conn = new SqlConnection())
                {

                    conn.ConnectionString = constr;
                    conn.Open();
                    StringBuilder sbu = new StringBuilder();

                    using (SqlCommand command = new SqlCommand("IgniteGetConsentReportUsers", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@PageIndex", SqlDbType.Int).Value = PageIndex;
                        command.Parameters.Add("@PageSize", SqlDbType.Int).Value = PageSize;

                        command.Parameters.Add("@CurrentUserId", SqlDbType.VarChar).Value = currentuserid.ToString();
                        command.Parameters.Add("@FavouriteUsers", SqlDbType.SmallInt).Value = favouriteUsers;
                        command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = usertype;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ConsentedUsers user = new ConsentedUsers();
                                //User=reader["asd"]  }
                                user.Username = ecd.DecryptData(DBHelper.getString(reader, "Username"), ecd.GetEncryptType());
                                user.Firstname = ecd.DecryptData(DBHelper.getString(reader, "Firstname"), ecd.GetEncryptType());
                                user.Status = DBHelper.getInt(reader, "Status");
                                user.Lastname = ecd.DecryptData(DBHelper.getString(reader, "LastName"), ecd.GetEncryptType());
                                user.ID = DBHelper.getInt(reader, "Id") == 0 ? Convert.ToInt32(reader["Id"]) : DBHelper.getInt(reader, "Id");
                                user.StatusMessage = DBHelper.getString(reader, "StatusMessage");
                                user.Lastloggedin = DBHelper.getString(reader, "LastLoggedIN");
                                user.Image = DBHelper.getString(reader, "Image");
                                user.Password = ecd.DecryptData(DBHelper.getString(reader, "Password"), ecd.GetEncryptType());
                                user.ReferenceID = DBHelper.getInt64(reader, "ReferenceID");
                                user.Favorites = DBHelper.getInt(reader, "fav_status");
                                user.PhoneNumber = DBHelper.getString(reader, "phone_number");
                                user.FileExt = DBHelper.getString(reader, "ConsentFormExt");
                                user.DateOfBirth = ecd.DecryptData(DBHelper.getString(reader, "dateofbirth"), ecd.GetEncryptType());
                                user.ConsentFormUploaded = DBHelper.getBool(reader, "HasFormUploaded");
                                user.showConsentButton = !DBHelper.getBool(reader, "HasFormUploaded") ? "block" : "none";
                                user.showDownloadButton = DBHelper.getBool(reader, "HasFormUploaded") ? "block" : "none";
                                userlist.Add(user);


                                //user.Username = DBHelper.getString(reader, "Username");
                                //user.Firstname = DBHelper.getString(reader, "Firstname");
                                //user.Status = DBHelper.getInt(reader, "Status");
                                //user.Lastname = DBHelper.getString(reader, "LastName");
                                //user.ID = DBHelper.getInt(reader, "Id") == 0 ? Convert.ToInt32(reader["Id"]) : DBHelper.getInt(reader, "Id");
                                //user.StatusMessage = DBHelper.getString(reader, "StatusMessage");
                                //user.Lastloggedin = DBHelper.getString(reader, "LastLoggedIN");
                                //user.Image = DBHelper.getString(reader, "Image");
                                //user.Password = DBHelper.getString(reader, "Password");
                                //user.ReferenceID = DBHelper.getInt64(reader, "ReferenceID");
                                //user.Favorites = DBHelper.getInt(reader, "fav_status");
                                //user.PhoneNumber = DBHelper.getString(reader, "phone_number");
                                //user.FileExt = DBHelper.getString(reader, "ConsentFormExt");
                                //user.DateOfBirth = DBHelper.getDateTime(reader, "dateofbirth").ToString(Utility.GlobalDateFormat);
                                //user.ConsentFormUploaded = DBHelper.getBool(reader, "HasFormUploaded");
                                //user.showConsentButton = !DBHelper.getBool(reader, "HasFormUploaded") ? "block" : "none";
                                //user.showDownloadButton = DBHelper.getBool(reader, "HasFormUploaded") ? "block" : "none";
                                //userlist.Add(user);
                            }
                        }
                    }

                    return userlist;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        [OperationContract]
        public Wraper GetUsers(long currentuserid, int favouriteUsers, int PageIndex, int PageSize, string usertype)
        {
            try
            {

                string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                Wraper objwp = new Wraper();
                List<Users> userlist = new List<Users>();
                Users user = null;

                using (SqlConnection conn = new SqlConnection())
                {

                    conn.ConnectionString = constr;
                    conn.Open();
                    StringBuilder sbu = new StringBuilder();

                    //if (favouriteUsers == 0)
                    //{
                    //    sbu.AppendLine("select U.ID,U.Username,Firstname,Status,Lastname,StatusMessage,LastLoggedIN,Image,Password,fav_status,phone_number,(select top 1 ConversationDate from Hick_user_Conversation where (Initiator=" + currentuserid + " and Answerer=U.ID) OR (Initiator=U.ID and Answerer=" + currentuserid + ") order by ConversationDate DESC) as ConvDate ");
                    //    sbu.AppendLine("FROM Hick_Users as U ");
                    //    sbu.AppendLine("LEFT OUTER JOIN hick_favourite_contacts as FC on U.ID=FC.fav_contact_id and FC.user_id=" + currentuserid + "");
                    //    sbu.AppendLine("WHERE U.ID<>" + currentuserid + "");
                    //    sbu.AppendLine("ORDER BY Status DESC,ConvDate DESC,Firstname ASC");
                    //}
                    //else
                    //{
                    //    sbu.AppendLine("select * from Hick_Users hu inner join hick_favourite_contacts hf on hf.fav_contact_id=hu.ID and hf.fav_status=1 where hf.user_id=" + currentuserid + "");
                    //}
                    //using (SqlCommand command = new SqlCommand(sbu.ToString(), conn))
                    using (SqlCommand command = new SqlCommand("hickFetchUsersByCriteria", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@PageIndex", SqlDbType.Int).Value = PageIndex;
                        command.Parameters.Add("@PageSize", SqlDbType.Int).Value = PageSize;

                        command.Parameters.Add("@CurrentUserId", SqlDbType.VarChar).Value = currentuserid.ToString();
                        command.Parameters.Add("@FavouriteUsers", SqlDbType.SmallInt).Value = favouriteUsers;
                        command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = usertype;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //ConsentedUsers user1 = new ConsentedUsers();
                                user = new Users();
                                //User=reader["asd"] }

                                user.Username = ecd.DecryptData(DBHelper.getString(reader, "Username"), ecd.GetEncryptType());
                                user.Firstname = ecd.DecryptData(DBHelper.getString(reader, "Firstname"), ecd.GetEncryptType());
                                user.Status = DBHelper.getInt(reader, "Status");
                                user.Lastname = ecd.DecryptData(DBHelper.getString(reader, "LastName"), ecd.GetEncryptType());
                                user.ID = DBHelper.getInt(reader, "Id") == 0 ? Convert.ToInt32(reader["Id"]) : DBHelper.getInt(reader, "Id");
                                user.StatusMessage = DBHelper.getString(reader, "StatusMessage");
                                user.Lastloggedin = DBHelper.getString(reader, "LastLoggedIN");
                                user.Image = DBHelper.getString(reader, "Image");
                                user.Password = ecd.DecryptData(DBHelper.getString(reader, "Password"), ecd.GetEncryptType());
                                user.ReferenceID = DBHelper.getInt64(reader, "ReferenceID");
                                user.Favorites = DBHelper.getInt16(reader, "fav_status");
                                user.PhoneNumber = DBHelper.getString(reader, "phone_number");
                                userlist.Add(user);



                                //user.Username = DBHelper.getString(reader, "Username");
                                //user.Firstname = DBHelper.getString(reader, "Firstname");
                                //user.Status = DBHelper.getInt(reader, "Status");
                                //user.Lastname = DBHelper.getString(reader, "LastName");
                                //user.ID = DBHelper.getInt(reader, "Id") == 0 ? Convert.ToInt32(reader["Id"]) : DBHelper.getInt(reader, "Id");
                                //user.StatusMessage = DBHelper.getString(reader, "StatusMessage");
                                //user.Lastloggedin = DBHelper.getString(reader, "LastLoggedIN");
                                //user.Image = DBHelper.getString(reader, "Image");
                                //user.Password = DBHelper.getString(reader, "Password");
                                //user.ReferenceID = DBHelper.getInt64(reader, "ReferenceID");
                                //user.Favorites = DBHelper.getInt(reader, "fav_status");
                                //user.PhoneNumber = DBHelper.getString(reader, "phone_number");
                                //user.FileExt = DBHelper.getString(reader, "ConsentFormExt");
                                //user.DateOfBirth = DBHelper.getDateTime(reader, "dateofbirth").ToString(Utility.GlobalDateFormat);
                                // user.ConsentFormUploaded = DBHelper.getBool(reader, "HasFormUploaded");
                                //user.showConsentButton = !DBHelper.getBool(reader, "HasFormUploaded") ? "block" : "none";
                                //user.showDownloadButton = DBHelper.getBool(reader, "HasFormUploaded") ? "block" : "none";
                                //userlist.Add(user);



                            }
                        }
                    }
                    if (userlist.Count > 0)
                    {
                        for (int i = 0; i < userlist.Count; i++)
                        {
                            int peerid = userlist[i].ID;

                            //StringBuilder sb = new StringBuilder();
                            //sb.AppendLine("select UC.ID from Hick_user_Conversation UC");
                            //sb.AppendLine("INNER JOIN Hick_Conversation_log CL on UC.ID=CL.Conversation_Id");
                            //sb.AppendLine("where ((UC.Initiator=" + currentuserid + " and UC.Answerer=" + peerid + ") or (UC.Initiator=" + peerid + " and UC.Answerer=" + currentuserid + ")) and UC.ConversationDate=CONVERT(date, @date)");
                            //sb.AppendLine("AND CL.peerid=" + peerid + " and CL.Read_Status=" + (int)ReadStatus.UnRead + " and CL.Message_Type=" + (int)MessageTypes.Text + "");

                            using (SqlCommand command = new SqlCommand("sp_hick_FetchUnreadMsgSts", conn))
                            {
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.AddWithValue("@CurUsr", currentuserid);
                                command.Parameters.AddWithValue("@PeerId", peerid);
                                command.Parameters.AddWithValue("@date", DateTime.UtcNow);
                                command.Parameters.AddWithValue("@ReadStatusUnread", (int)ReadStatus.UnRead);
                                command.Parameters.AddWithValue("@MsgTypeTxt", (int)MessageTypes.Text);
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        userlist[i].UnReadMessages = true;
                                    }
                                }
                            }
                            //sb = null;
                            //sb = new StringBuilder();
                            //sb.AppendLine("select UC.ID,CL.PeerId from Hick_user_Conversation UC");
                            //sb.AppendLine("INNER JOIN Hick_VideoConversation_Log CL on UC.ID=CL.ConversationId");
                            //sb.AppendLine("where ((UC.Initiator=" + currentuserid + " and UC.Answerer=" + peerid + ") or (UC.Initiator=" + peerid + " and UC.Answerer=" + currentuserid + ")) and UC.ConversationDate=CONVERT(date, @date)");
                            //sb.AppendLine("and CL.Status=" + (int)ReadStatus.CallInitiated + "");
                            using (SqlCommand command = new SqlCommand("sp_hick_IncomingCall", conn))
                            {
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.AddWithValue("@CurUsrId", currentuserid);
                                command.Parameters.AddWithValue("@PeerId", peerid);
                                command.Parameters.AddWithValue("@date", DateTime.UtcNow);
                                command.Parameters.AddWithValue("@CallInitiateSts", (int)ReadStatus.CallInitiated);
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            if (currentuserid != Convert.ToInt32(reader["PeerId"]))
                                            {
                                                userlist[i].IncomingCall = true;
                                            }
                                        }
                                    }
                                }
                            }

                            string ChatDuration = TxtChatDuration(currentuserid, peerid).Trim();
                            string VidDuration = VideoDuration(currentuserid, peerid).Trim();
                            string TaskAssignDuration = TaskDuration(currentuserid, peerid).Trim();
                            string AudioDuration = AudioCallDuration(peerid).Trim();

                            TimeSpan TotalDuration = new TimeSpan();
                            //if (!string.IsNullOrEmpty(ChatDuration) && !string.IsNullOrEmpty(VidDuration))
                            //{
                            //    TotalDuration = TimeSpan.Parse(ChatDuration) + TimeSpan.Parse(VidDuration) + TimeSpan.Parse(TaskAssignDuration);
                            //}
                            //else if (string.IsNullOrEmpty(ChatDuration) && !string.IsNullOrEmpty(VidDuration))
                            //{
                            //    TotalDuration = TimeSpan.Parse(VidDuration);
                            //}
                            //else if (string.IsNullOrEmpty(VidDuration) && !string.IsNullOrEmpty(ChatDuration))
                            //{
                            //    TotalDuration = TimeSpan.Parse(ChatDuration);
                            //}

                            if (!string.IsNullOrEmpty(ChatDuration))
                            {
                                TotalDuration = TotalDuration.Add(TimeSpan.Parse(ChatDuration));
                            }
                            if (!string.IsNullOrEmpty(VidDuration))
                            {
                                TotalDuration = TotalDuration.Add(TimeSpan.Parse(VidDuration));
                            }
                            if (!string.IsNullOrEmpty(TaskAssignDuration))
                            {
                                TotalDuration = TotalDuration.Add(TimeSpan.Parse(TaskAssignDuration));
                            }
                            if (!string.IsNullOrEmpty(AudioDuration))
                            {
                                TotalDuration = TotalDuration.Add(TimeSpan.Parse(AudioDuration));
                            }

                            if (TotalDuration.Hours > 0 || TotalDuration.Minutes > 0)
                            {
                                userlist[i].VideoCallDuration = string.Format("{0:00}:{1:00}", TotalDuration.Hours, TotalDuration.Minutes);
                            }
                            else
                            {
                                userlist[i].VideoCallDuration = "";
                            }
                        }

                    }

                    objwp.UsersColl = userlist;
                    if (favouriteUsers == 0)
                    {
                        objwp.GroupsColl = GetGroups(currentuserid);
                    }
                    return objwp;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }


        [OperationContract]
        public string VideoDuration(long currentuserid, int peerid)
        {
            string VideoCallDuration = "";
            TimeSpan duration = new TimeSpan();
            string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                conn.Open();
                //StringBuilder sb = new StringBuilder();
                //sb.AppendLine("declare @status int;");
                //sb.AppendLine("DECLARE @sm DATETIME;");
                //sb.AppendLine("SET @sm = DATEADD(DAY, 1-DAY(@date), DATEDIFF(DAY, 0, @date));");
                //sb.AppendLine("set @status=7;");
                //sb.AppendLine("select ConversationDate, ConversationEndTime,");
                //sb.AppendLine("DateDiff(s, ConversationDate, ConversationEndTime)/3600 as hrs,DateDiff(s, ConversationDate, ConversationEndTime)%3600/60 as mts,(DateDiff(s, ConversationDate, ConversationEndTime)%60) as sec");
                //sb.AppendLine(", DateDiff(s,ConversationDate , ConversationEndTime) as [Log Seconds]");
                //sb.AppendLine("into #temptable");
                //sb.AppendLine("from Hick_VideoConversation_Log");
                //sb.AppendLine("where PeerId='" + currentuserid + "' and Status=@status and ParentVideoId in (select VideoId from Hick_VideoConversation_Log where peerid=" + peerid + ")and (ConversationDate IS NOT NULL) and (ConversationEndTime IS NOT NULL) and ConversationEndTime >= @sm");
                //sb.AppendLine("select STR(SUM([Log Seconds])/3600) + RIGHT(CONVERT(char(8),DATEADD(s,SUM([Log Seconds]),0),108),6) as TotalDuration from #temptable");
                //sb.AppendLine("drop table  #temptable");
                using (SqlCommand command = new SqlCommand("sp_hick_TotalVideoCallDurationForGivenUsers", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CurrentUserId", currentuserid);
                    command.Parameters.AddWithValue("@PeerId", peerid);
                    command.Parameters.AddWithValue("@Date", DateTime.UtcNow);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Utility.CalculateTimeSpan(ref duration,Convert.ToString(reader["TotalDuration"]));
                                VideoCallDuration = Convert.ToString(duration);
                            }
                        }
                    }
                }
            }
            return VideoCallDuration;
        }

        

        [OperationContract]
        public AuditWrapper GetAudit_log(long currentuserid, string usertype,long peerid, int PageIndex, int PageSize)
        {
            if (usertype == "admin")
            {
                currentuserid = peerid;
            }
            SqlParameter[] sqlParms = new SqlParameter[] { new SqlParameter("UserId", currentuserid) };

            List<IGNITE_MODEL.Audit> auditList = new List<IGNITE_MODEL.Audit>();

            using (SqlDataReader sqlGetAudits = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "sp_hick_GetAllAudits", sqlParms))
            {
                while (sqlGetAudits.Read())
                {
                    var audit = new IGNITE_MODEL.Audit();
                    audit.ActionType = GetActionTypeString(DBHelper.getInt16(sqlGetAudits, "Type"));
                    //audit.Email = DBHelper.getString(sqlGetAudits, "Username");

                    audit.Email =ecd.DecryptData(DBHelper.getString(sqlGetAudits, "Email"),ecd.GetEncryptType());
                    audit.Date = DBHelper.getDateTime(sqlGetAudits, "ActionDate").ToString("MM/dd/yyyy HH:mm:ss");
                    audit.InformationType = DBHelper.getString(sqlGetAudits, "InformationType");
                    //audit.NewValue = DBHelper.getString(sqlGetAudits, "NewValue");
                    //audit.OldValue = DBHelper.getString(sqlGetAudits, "OldValue");
                    auditList.Add(audit);
                }

                return new AuditWrapper()
                {
                    AuditColl = auditList
                };
            }
            //return new AuditBLL().GetAllAudits(currentuserid, usertype, PageIndex, PageSize);
        }

        [OperationContract]
        public Wraper GetUsers_log(long currentuserid, string usertype, int PageIndex, int PageSize)
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                Wraper objwp = new Wraper();
                List<Users> userlist = new List<Users>();
                Users user = null;

                using (SqlConnection conn = new SqlConnection())
                {

                    conn.ConnectionString = constr;
                    conn.Open();

                    StringBuilder sbu = new StringBuilder();

                    //sbu.AppendLine("select *,(select top 1 ConversationDate from Hick_user_Conversation where (Initiator=" + currentuserid + " and Answerer=U.ID) OR (Initiator=U.ID and Answerer=" + currentuserid + ") order by ConversationDate DESC) as ConvDate ");
                    //sbu.AppendLine("FROM Hick_Users as U ");
                    //sbu.AppendLine("WHERE U.ID<>" + currentuserid + " AND LOWER(U.user_type)<>CASE WHEN LOWER('" + usertype + "')='patient' THEN 'patient' ELSE '' END");
                    //sbu.AppendLine("ORDER BY Status DESC,ConvDate DESC,Firstname ASC");

                    using (SqlCommand command = new SqlCommand("hickFetchUsersByCriteria", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@PageIndex", SqlDbType.Int).Value = PageIndex;
                        command.Parameters.Add("@PageSize", SqlDbType.Int).Value = PageSize;

                        command.Parameters.Add("@CurrentUserId", SqlDbType.VarChar).Value = currentuserid.ToString();
                        command.Parameters.Add("@FavouriteUsers", SqlDbType.SmallInt).Value = 0;
                        command.Parameters.Add("@UserType", SqlDbType.VarChar).Value = usertype;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                user = new Users();
                                //User=reader["asd"]                        }

                                user.Username = ecd.DecryptData((reader["Username"].ToString()), ecd.GetEncryptType());
                                user.Firstname = ecd.DecryptData((reader["Firstname"].ToString()), ecd.GetEncryptType());
                                user.Status = Convert.ToInt32(reader["Status"]);
                                user.Lastname = ecd.DecryptData((reader["Lastname"].ToString()),ecd.GetEncryptType());
                                user.ID = Convert.ToInt32(reader["ID"]);
                                user.StatusMessage = reader["StatusMessage"].ToString();
                                user.Lastloggedin = reader["LastLoggedIN"].ToString();
                                user.Image = reader["Image"].ToString();
                                user.Password = ecd.DecryptData((reader["Password"].ToString()), ecd.GetEncryptType());
                                userlist.Add(user);
                            }

                        }
                    }

                }
                objwp.UsersColl = userlist;
                objwp.GroupsColl = GetGroups(currentuserid);
                return objwp;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [OperationContract]
        public List<UserConversation> InitiateChat(long currentid, long peerid, long groupid)
        {
            try
            {
                List<UserConversation> objcoll = new List<UserConversation>();
                UserConversation objconversation = null;
                if (currentid != 0 && peerid != 0)
                {
                    string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = constr;
                        conn.Open();
                        bool isexist = false;

                        //using (SqlCommand command = new SqlCommand("select * from Hick_user_Conversation where ((Initiator=" + currentid + " and Answerer=" + peerid + ") or (Initiator=" + peerid + " and Answerer=" + currentid + ")) and ConversationDate=CONVERT(date, @date)", conn))
                        using (SqlCommand command = new SqlCommand("sp_hick_FetchUserConversationForToday", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@CurrentUserId", currentid);
                            command.Parameters.AddWithValue("@PeerId", peerid);
                            command.Parameters.AddWithValue("@Date", DateTime.UtcNow);
                            command.Parameters.AddWithValue("@GroupId", 0);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    isexist = true;
                                    while (reader.Read())
                                    {
                                        objconversation = new UserConversation();
                                        objconversation.Id = Convert.ToInt32(reader["ID"]);
                                        if (reader["ischatOn"] != System.DBNull.Value)
                                        {
                                            objconversation.IsChatOn = Convert.ToInt32(reader["ischatOn"]);
                                        }
                                        else
                                        {
                                            objconversation.IsChatOn = 0;
                                        }

                                        if (reader["total_txtchat_dauration"] != System.DBNull.Value)
                                        {
                                            objconversation.Total_Txtchat_Dauration = Convert.ToString(reader["total_txtchat_dauration"]);
                                        }
                                        else
                                        {
                                            objconversation.Total_Txtchat_Dauration = string.Empty;
                                        }
                                        objcoll.Add(objconversation);
                                    }
                                }
                            }
                        }
                        if (!isexist)
                        {
                            //var cmd = "INSERT INTO Hick_user_Conversation (Initiator,Answerer,ConversationDate) VALUES (@curuser,@peeruser,@date);SELECT CAST(scope_identity() AS bigint)";
                            using (SqlCommand command = new SqlCommand("sp_hick_InsertHickUserConversation", conn))
                            {
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.AddWithValue("@Curuser", currentid);
                                command.Parameters.AddWithValue("@Peeruser", peerid);
                                command.Parameters.AddWithValue("@Date", DateTime.UtcNow);
                                objconversation = new UserConversation();
                                objconversation.Id = (long)command.ExecuteScalar();
                                objcoll.Add(objconversation);

                            }
                        }

                        UpdateReadStatus(objcoll[0].Id, currentid, (int)ReadStatus.Read, (int)MessageTypes.Text);
                    }
                }
                else if (groupid != 0)
                {
                    objconversation = new UserConversation();
                    objconversation.Id = InitiateGroupChat(groupid);
                    objcoll.Add(objconversation);

                    UpdateGroupMessageState(groupid, currentid);
                }

                return objcoll;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [OperationContract]
        public List<ConversationLog> GetMessages(string conversationid, long currentuserid, string timezone, long peeruserid, long groupid)
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                List<ConversationLog> objlogColl = new List<ConversationLog>();
                ConversationLog objlog = null;

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = constr;
                    conn.Open();
                    
                    using (SqlCommand command = new SqlCommand("sp_hick_GetMessages", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ConvId", conversationid);
                        command.Parameters.AddWithValue("@CurUsrId", currentuserid);
                        command.Parameters.AddWithValue("@PeerId", peeruserid);
                        command.Parameters.AddWithValue("@GroupId", groupid);
                        command.Parameters.AddWithValue("@MsgTypeTxt", (int)MessageTypes.Text);
                        command.Parameters.AddWithValue("@MagTypeFile", (int)MessageTypes.File);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                objlog = new ConversationLog();
                                objlog.Id = Convert.ToInt32(reader["Id"]);
                                objlog.ConversationId = Convert.ToInt32(reader["Conversation_Id"]);
                                objlog.Conversation = Convert.ToString(reader["Conversation_log"]);

                                objlog.PeerName = Convert.ToString(ecd.DecryptData(reader["peeruserF"].ToString(), ecd.GetEncryptType()));
                                objlog.PeerName= objlog.PeerName+" "+ Convert.ToString(ecd.DecryptData(reader["peeruserL"].ToString(), ecd.GetEncryptType()));
                                objlog.PeerUserName = Convert.ToString(ecd.DecryptData(reader["peerusername"].ToString(),ecd.GetEncryptType())); 
                                objlog.MessageType = Convert.ToInt32(reader["Message_Type"]);
                                if (reader["Message_To"] != System.DBNull.Value)
                                {
                                    objlog.MessageTo = Convert.ToInt32(reader["Message_To"]);
                                }
                                objlog.ReadStatus = Convert.ToInt32(reader["Read_Status"]);

                                objlog.TotalChatDuration = Convert.ToString(reader["totaltextchat"]);

                                DateTime dtraw = Convert.ToDateTime(reader["Conversation_date"]);
                                objlog.ConversationDate = Convert.ToString(Utility.ConvertDateToLocal(timezone, dtraw).ToShortDateString());

                                DateTime dt = Utility.ConvertDateToLocal(timezone, dtraw);

                                objlog.Time = objlog.ConversationDate != DateTime.Now.ToShortDateString() ? dt.ToString("MMM dd") + ", " + dt.ToString("hh:mm tt") : dt.ToString("hh:mm tt");
                                objlog.PeerID = Convert.ToInt32(reader["peerid"]);

                                /* creating the path for the received file */
                                if (objlog.MessageType == (int)MessageTypes.File && objlog.ReadStatus == (int)ReadStatus.Received)
                                {
                                    string filname = objlog.Conversation;
                                    var imageExtensions = Convert.ToString(ConfigurationManager.AppSettings["SendPictureAllowedExtensions"]).Split(',');
                                    bool fileIsImage = false;
                                    foreach (string extension in imageExtensions)
                                    {
                                        if (filname.ToLower().EndsWith("." + extension.ToLower()))
                                        {
                                            fileIsImage = true;
                                            break;
                                        }
                                    }
                                    string sourcefilepath = string.Empty;

                                    if (fileIsImage)
                                    {
                                        sourcefilepath = "UserFiles/" + objlog.PeerUserName.ToLower() + "/pictures";
                                    }
                                    else
                                    {
                                        sourcefilepath = "UserFiles/" + objlog.PeerUserName.ToLower() + "/documents";
                                    }
                                    string filename = filname;
                                    string isssl = HttpContext.Current.Request.ServerVariables["HTTPS"];

                                    string fileUrl = String.Format("{0}{1}{2}", isssl == "off" ? "http://" : "https://", HttpContext.Current.Request.Url.Host, VirtualPathUtility.ToAbsolute(String.Format("~/{0}", System.IO.Path.Combine(sourcefilepath, filename))));

                                    objlog.ReceivedImagePath = fileUrl;
                                }

                                objlogColl.Add(objlog);
                            }


                        }
                    }
                    if (objlogColl.Count > 0 && groupid == 0)
                    {
                        //var cmd = "UPDATE Hick_Conversation_log SET Read_Status=@readstatus WHERE Conversation_Id=@convestnid AND Message_To=@currentuser AND Message_Type=@mesgtyp";
                        //using (SqlCommand command = new SqlCommand(cmd, conn))
                        //{
                        //    command.Parameters.AddWithValue("@convestnid", conversationid);
                        //    command.Parameters.AddWithValue("@currentuser", currentuserid);
                        //    command.Parameters.AddWithValue("@readstatus", (int)ReadStatus.Read);
                        //    command.Parameters.AddWithValue("@mesgtyp", (int)MessageTypes.Text);
                        //    command.ExecuteNonQuery();
                        //}
                        if (conversationid.Contains(","))
                        {
                            for (int i = 0; i < conversationid.Split(',').Length; i++)
                            {
                                UpdateReadStatus(long.Parse(conversationid.Split(',')[i]), currentuserid, (int)ReadStatus.Read, (int)MessageTypes.Text);
                            }
                        }
                        else
                        {
                            UpdateReadStatus(long.Parse(conversationid), currentuserid, (int)ReadStatus.Read, (int)MessageTypes.Text);
                        }

                    }
                    else if (objlogColl.Count > 0 && groupid != 0)
                    {
                        UpdateGroupMessageState(groupid, currentuserid);

                    }
                }
                return objlogColl;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [OperationContract]
        public void SaveMessage(long conversationid, string message, long curentuserid, long peeruserid, int messagetype, long groupid)
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = constr;
                    conn.Open();
                    int res = -1;
                    StringBuilder sb = new StringBuilder();
                    //if (groupid == 0)
                    //{
                    //    sb.AppendLine("INSERT INTO Hick_Conversation_log (Conversation_Id,Conversation_log,Conversation_date,peerid,Message_Type,Message_To,Read_Status) VALUES (@conid,@msg,@date,@curntuserid,@messagetype,@messageto,@readstatus)");
                    //}
                    //else
                    //{
                    //    sb.AppendLine("INSERT INTO Hick_Conversation_log (Conversation_Id,Conversation_log,Conversation_date,peerid,Message_Type,Read_Status,group_id) VALUES (@conid,@msg,@date,@curntuserid,@messagetype,@readstatus,@groupid)");
                    //}

                    using (SqlCommand command = new SqlCommand("sp_hick_InsertConversationLog", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@conid", conversationid);
                        command.Parameters.AddWithValue("@msg", message);
                        command.Parameters.AddWithValue("@date", DateTime.UtcNow);
                        command.Parameters.AddWithValue("@curntuserid", curentuserid);
                        command.Parameters.AddWithValue("@messagetype", messagetype);
                        command.Parameters.AddWithValue("@messageto", peeruserid);
                        if (groupid != 0)
                        {
                            command.Parameters.AddWithValue("@groupid", groupid);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@groupid", string.Empty);
                        }
                        if (messagetype == (int)MessageTypes.File)
                        {
                            command.Parameters.AddWithValue("@readstatus", (int)ReadStatus.Transfered);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@readstatus", (int)ReadStatus.UnRead);
                        }

                        res = command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public VideoChatSettings InitiateVideoChat(long conversationid, string message, long peerid, int messagetype)
        {
            try
            {
                int res = -1;
                List<VideoChatSettings> objcoll = new List<VideoChatSettings>();
                VideoChatSettings objvideo = null;
                string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = constr;
                    conn.Open();

                    //using (SqlCommand command = new SqlCommand("select * from Hick_VideoConversation_Log as l where ConversationId=" + conversationid + " and l.PeerId=" + peerid + " and l.MessageType=" + (int)MessageTypes.Video + " and l.Status=" + (int)ReadStatus.CallInitiated + "", conn))
                    using (SqlCommand command = new SqlCommand("sp_hick_FetchVideoConvToInitiate", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ConvId", conversationid);
                        command.Parameters.AddWithValue("@PeerId", peerid);
                        command.Parameters.AddWithValue("@MsgType", (int)MessageTypes.Video);
                        command.Parameters.AddWithValue("@Status", (int)ReadStatus.CallInitiated);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    //var cmd = "UPDATE Hick_Conversation_log set Conversation_log=@msg, Conversation_date=@date where Conversation_Id=" + conversationid + " and peerid=" + peerid + " and Message_Type=" + (int)MessageTypes.Video + "";
                                    //using (SqlCommand command1 = new SqlCommand(cmd, conn))
                                    //{
                                    //    command1.Parameters.AddWithValue("@msg", message);
                                    //    command1.Parameters.AddWithValue("@date", DateTime.Now);
                                    //    res = command1.ExecuteNonQuery();
                                    //}
                                    objvideo = new VideoChatSettings();
                                    objvideo.EnableVideoChat = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableVideoChat"]);
                                    objvideo.FlashServerType = Convert.ToString(ConfigurationManager.AppSettings["FlashServerType"]).Trim();
                                    objvideo.FlashMediaServer = Convert.ToString(ConfigurationManager.AppSettings["FlashMediaServer"]).Trim();
                                    objvideo.BroadcastVideoWidth = Convert.ToString(ConfigurationManager.AppSettings["BroadcastVideoWidth"]).Trim();
                                    objvideo.BroadcastVideoheight = Convert.ToString(ConfigurationManager.AppSettings["BroadcastVideoheight"]).Trim();
                                    objvideo.BroadcastVideoGuid = Convert.ToString(reader["VideoId"]);
                                    objvideo.ParentVideoId = Convert.ToString(reader["ParentVideoId"]);
                                    objcoll.Add(objvideo);
                                }

                            }
                            if (objcoll.Count > 0)
                            {
                                for (int i = 0; i < objcoll.Count; i++)
                                {
                                    UpdateVideoCallStatus(conversationid, objcoll[i].BroadcastVideoGuid, String.IsNullOrEmpty(objcoll[i].ParentVideoId) ? string.Empty : objcoll[i].ParentVideoId, (int)ReadStatus.CallEnded, (int)MessageTypes.Video, 0);
                                }
                            }

                            //var cmd = "INSERT INTO Hick_VideoConversation_Log (ConversationId,VideoId,ConversationDate,PeerId,MessageType,Status) VALUES (@conid,@msg,@date,@curntuserid,@messagetype,@status)";
                            using (SqlCommand command2 = new SqlCommand("sp_hick_InsertVideoConversationLog", conn))
                            {
                                command2.CommandType = CommandType.StoredProcedure;

                                command2.Parameters.AddWithValue("@conid", conversationid);
                                command2.Parameters.AddWithValue("@msg", message);
                                command2.Parameters.AddWithValue("@date", DateTime.Now);
                                command2.Parameters.AddWithValue("@curntuserid", peerid);
                                command2.Parameters.AddWithValue("@messagetype", messagetype);
                                command2.Parameters.AddWithValue("@status", (int)ReadStatus.CallInitiated);
                                res = command2.ExecuteNonQuery();
                                if (res >= 0)
                                {
                                    objvideo = new VideoChatSettings();
                                    objvideo.EnableVideoChat = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableVideoChat"]);
                                    objvideo.FlashServerType = Convert.ToString(ConfigurationManager.AppSettings["FlashServerType"]).Trim();
                                    objvideo.FlashMediaServer = Convert.ToString(ConfigurationManager.AppSettings["FlashMediaServer"]).Trim();
                                    objvideo.BroadcastVideoWidth = Convert.ToString(ConfigurationManager.AppSettings["BroadcastVideoWidth"]).Trim();
                                    objvideo.BroadcastVideoheight = Convert.ToString(ConfigurationManager.AppSettings["BroadcastVideoheight"]).Trim();
                                    objvideo.BroadcastVideoGuid = message;
                                }
                            }
                        }
                    }


                }
                return objvideo;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [OperationContract]
        public void InsertSessionNote(long conversationid, long currentid, long peerid, long groupid, string session, string sessionNote,int category,string StartDateTime,string EndDateTime)
        {
            string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                conn.Open();
                int active = (session == "Yes") ? 1: 0;
                using (SqlCommand command = new SqlCommand("sp_hick_InsertSessionNote", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FromId", currentid);
                    command.Parameters.AddWithValue("@Note", sessionNote);
                    command.Parameters.AddWithValue("@ConverstionId", conversationid);
                    command.Parameters.AddWithValue("@Category", category);
                    command.Parameters.AddWithValue("@IsActive", active);
                    command.Parameters.AddWithValue("@StartDateTime", Convert.ToDateTime(StartDateTime));
                    command.Parameters.AddWithValue("@EndDateTime", Convert.ToDateTime(EndDateTime));
                    if (groupid != 0)
                    {
                        command.Parameters.AddWithValue("@ToId", groupid);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ToId", peerid);
                    }

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (session == "Yes")
                                {
                                    var peerRefId= GetReferenceId(peerid);

                                    var name = Convert.ToString(ecd.DecryptData((reader["FirstName"]).ToString(), ecd.GetEncryptType())) + " " + Convert.ToString(ecd.DecryptData((reader["LastName"]).ToString(), ecd.GetEncryptType()));

                                    var currentUserRefId = (reader["ReferenceId"] != null && reader["ReferenceId"] != DBNull.Value) ? Convert.ToInt32(reader["ReferenceId"]) : currentid;
                                    var type = (category == 1) ? "Chat" : (category == 2) ? "Audio Call" : "Video Call";

                                    var uri = Utility.GetServiceUrl("updateencounter");

                                    IgJObject jobj = new IgJObject();
                                    jobj.Add("PatientID", peerRefId);
                                    jobj.Add("DoctorName", name);
                                    jobj.Add("VisitReason", type);
                                    jobj.Add("DoctorVisitId", 0);
                                    jobj.Add("Visitdate", DateTime.Now.ToString());
                                    jobj.Add("UserId", currentUserRefId);
                                    jobj.Add("VisitDiagnosis", "CCM");
                                    //jobj.Add("CCM", string.Empty);
                                    var res = Utility.PostRequestForSave(uri, jobj.ToString(Formatting.None));
                                }
                            }
                        }
                    }
                }
            }
        }

        public long GetReferenceId(long peerid)
        {
            string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
            long referenceId = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                conn.Open();

                using (SqlCommand command = new SqlCommand("sp_hick_FetchUserByID", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", peerid);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                referenceId = Convert.ToInt64(reader["ReferenceId"]);
                            }
                        }
                    }
                }
            }
            return referenceId;
         }

        [OperationContract]
        public List<ConversationLog> GetChatLog(long currentid, long peerid, string logdate, string timezone, long groupid)
        {
            try
            {
                List<ConversationLog> objcoll = new List<ConversationLog>();
                if (currentid != 0)
                {

                    string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();

                    ConversationLog objlog = null;

                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = constr;
                        conn.Open();
                        bool isexist = false;
                        //string selcmd = string.Empty;
                        //if (groupid == 0)
                        //{
                        //    selcmd = "select * from Hick_user_Conversation where ((Initiator=" + currentid + " and Answerer=" + peerid + ") or (Initiator=" + peerid + " and Answerer=" + currentid + ")) and ConversationDate='" + Utility.ConvertDateToUTC(timezone, Convert.ToDateTime(logdate)) + "'";
                        //}
                        //else
                        //{
                        //    selcmd = "select * from Hick_user_Conversation where group_id=" + groupid + " and ConversationDate='" + Utility.ConvertDateToUTC(timezone, Convert.ToDateTime(logdate)) + "'";
                        //}
                        using (SqlCommand command = new SqlCommand("sp_hick_GetChatLog", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@CurrentId", currentid);
                            command.Parameters.AddWithValue("@PeerId", peerid);
                            
                            command.Parameters.AddWithValue("@ConvDate", Utility.ConvertDateToUTC(timezone, DateTime.ParseExact(logdate, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture)));
                            if (groupid != 0)
                            {
                                command.Parameters.AddWithValue("@GroupId", groupid);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@GroupId", string.Empty);
                            }

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    isexist = true;
                                    while (reader.Read())
                                    {
                                        objlog = new ConversationLog();
                                        objlog.Id = Convert.ToInt32(reader["ID"]);
                                        //objcoll.Add(objlog);
                                    }
                                }
                            }
                        }
                        if (isexist == true)
                        {
                            //StringBuilder cmd = new StringBuilder();
                            //if (groupid == 0)
                            //{
                            //    cmd.AppendLine("select *,peeruser=(select Username from Hick_Users where ID=l.peerid) from Hick_Conversation_log as l where Conversation_Id=" + objlog.Id + " order by l.Id asc");
                            //}
                            //else
                            //{
                            //    cmd.AppendLine("select *,peeruser=(select Username from Hick_Users where ID=l.peerid) from Hick_Conversation_log as l");
                            //    cmd.AppendLine("where (l.Conversation_Id=" + objlog.Id + " OR l.Conversation_Id=(select top 1 conversation_id from hick_previous_log where group_id=" + groupid + " and l.Conversation_date<=created_date and Convert(date,created_date)=Convert(date,'" + Utility.ConvertDateToUTC(timezone, Convert.ToDateTime(logdate)) + "')))");

                            //    cmd.AppendLine("order by l.Id asc");
                            //}


                            using (SqlCommand command = new SqlCommand("sp_hick_FetchConvLog", conn))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@ConverId", objlog.Id);
                                if (groupid != 0)
                                {
                                    command.Parameters.AddWithValue("@GroupId", groupid);
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@GroupId", string.Empty);
                                }
                                command.Parameters.AddWithValue("@Date", Utility.ConvertDateToUTC(timezone, DateTime.ParseExact(logdate, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture)));

                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        objlog = new ConversationLog();
                                        objlog.Id = Convert.ToInt32(reader["Id"]);
                                        objlog.ConversationId = Convert.ToInt32(reader["Conversation_Id"]);
                                        objlog.Conversation = Convert.ToString(reader["Conversation_log"]);

                                        objlog.PeerName = ecd.DecryptData((reader["peeruser"]).ToString(), ecd.GetEncryptType());

                                        objlog.MessageType = Convert.ToInt32(reader["Message_Type"]);
                                        objlog.ReadStatus = reader["Read_Status"] != DBNull.Value ? Convert.ToInt32(reader["Read_Status"]) : 0;

                                        DateTime dtraw = Convert.ToDateTime(reader["Conversation_date"]);
                                        objlog.ConversationDate = Convert.ToString(Utility.ConvertDateToLocal(timezone, dtraw));
                                        DateTime dt = Utility.ConvertDateToLocal(timezone, dtraw);

                                        objlog.Time = dt.ToString("MMM dd") + ", " + dt.ToString("hh:mm tt");
                                        objlog.PeerID = Convert.ToInt32(reader["peerid"]);

                                        objcoll.Add(objlog);
                                    }

                                    //return objcoll;
                                }
                            }
                        }
                    }
                }
                return objcoll;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [OperationContract]
        public VideoChatSettings BroadcastVideo(long conversationid, long peerid)
        {

            VideoChatSettings objvideo = new VideoChatSettings();
            try
            {
                string guid = Convert.ToString(Guid.NewGuid());
                objvideo = InitiateVideoChat(conversationid, guid, peerid, (int)MessageTypes.Video);

            }
            catch (Exception)
            {
                throw;
            }
            return objvideo;
        }

        [OperationContract]
        public VideoChatSettings ReceiveVideo(long conversationid, long peerid, long currentuserid)
        {

            VideoChatSettings objvideo = new VideoChatSettings();
            try
            {

                string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = constr;
                    conn.Open();

                    //using (SqlCommand command = new SqlCommand("select top 1 * from Hick_VideoConversation_Log as l where ConversationId=" + conversationid + " and l.PeerId=" + peerid + " and l.MessageType=" + (int)MessageTypes.Video + " and l.Status=" + (int)ReadStatus.CallInitiated + " order by l.ConversationDate desc", conn))
                    using (SqlCommand command = new SqlCommand("sp_hick_FetchTopVidConvLog", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ConvId", conversationid);
                        command.Parameters.AddWithValue("@UserId", peerid);
                        command.Parameters.AddWithValue("@MsgType", (int)MessageTypes.Video);
                        command.Parameters.AddWithValue("@Status", (int)ReadStatus.CallInitiated);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    objvideo.ReceiveVideoGuid = Convert.ToString(reader["VideoId"]);
                                    objvideo.EnableVideoChat = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableVideoChat"]);
                                    objvideo.FlashServerType = Convert.ToString(ConfigurationManager.AppSettings["FlashServerType"]).Trim();
                                    objvideo.FlashMediaServer = Convert.ToString(ConfigurationManager.AppSettings["FlashMediaServer"]).Trim();
                                    objvideo.BroadcastVideoWidth = Convert.ToString(ConfigurationManager.AppSettings["BroadcastVideoWidth"]).Trim();
                                    objvideo.BroadcastVideoheight = Convert.ToString(ConfigurationManager.AppSettings["BroadcastVideoheight"]).Trim();

                                }
                            }

                        }
                    }
                    //using (SqlCommand command = new SqlCommand("select top 1 * from Hick_VideoConversation_Log as l where ConversationId=" + conversationid + " and l.PeerId=" + currentuserid + " and l.MessageType=" + (int)MessageTypes.Video + " and l.Status=" + (int)ReadStatus.CallInitiated + " order by l.ConversationDate desc", conn))
                    using (SqlCommand command = new SqlCommand("sp_hick_FetchTopVidConvLog", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ConvId", conversationid);
                        command.Parameters.AddWithValue("@UserId", currentuserid);
                        command.Parameters.AddWithValue("@MsgType", (int)MessageTypes.Video);
                        command.Parameters.AddWithValue("@Status", (int)ReadStatus.CallInitiated);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    objvideo.BroadcastVideoGuid = Convert.ToString(reader["VideoId"]);
                                }
                            }
                        }
                    }
                    if (objvideo != null)
                    {
                        if (!String.IsNullOrEmpty(objvideo.BroadcastVideoGuid) && !String.IsNullOrEmpty(objvideo.ReceiveVideoGuid))
                        {

                            UpdateParentVedioId(conversationid, objvideo.BroadcastVideoGuid, objvideo.ReceiveVideoGuid);
                            UpdateParentVedioId(conversationid, objvideo.ReceiveVideoGuid, objvideo.BroadcastVideoGuid);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return objvideo;
        }

        [OperationContract]
        public void StopVideoChat(long conversationid, string broadcastvideoid, string receivedvideoid, long groupid)
        {

            try
            {
                //string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                //Object thisLock = new Object();
                //lock (thisLock)
                //{
                //    using (SqlConnection conn = new SqlConnection())
                //    {
                //        conn.ConnectionString = constr;
                //        conn.Open();

                //        using (SqlCommand command = new SqlCommand("delete Hick_Conversation_log where Conversation_Id=" + conversationid + " and Message_Type=" + (int)MessageTypes.Video + "", conn))
                //        {
                //            command.ExecuteNonQuery();
                //        }
                //    }
                //}
                UpdateVideoCallStatus(conversationid, broadcastvideoid, receivedvideoid, (int)ReadStatus.CallEnded, (int)MessageTypes.Video, groupid);

            }
            catch (Exception)
            {
                throw;
            }
        }

        [OperationContract]
        public void UpdateFileReceivedStatus(long conversationid, long curentuserid, long logid, int status)
        {
            string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();

            using (SqlConnection conn = new SqlConnection(constr))
            {
                //var cmd1 = "UPDATE Hick_Conversation_log SET Read_Status=@readstatus WHERE Conversation_Id=@convestnid AND Message_To=@currentuser AND Message_Type=@mesgtyp AND Id=@id";
                using (SqlCommand command = new SqlCommand("sp_hick_UpdateFileReceivedStatus", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    command.Parameters.AddWithValue("@convestnid", conversationid);
                    command.Parameters.AddWithValue("@currentuser", curentuserid);
                    command.Parameters.AddWithValue("@readstatus", status);
                    command.Parameters.AddWithValue("@mesgtyp", (int)MessageTypes.File);
                    command.Parameters.AddWithValue("@id", logid);
                    command.ExecuteNonQuery();
                }
            }
        }

        [OperationContract]
        public string ClearChatLog(string conversationid, long currentuserid)
        {
            string result = string.Empty;
            try
            {

                if (currentuserid != 0 && conversationid != string.Empty)
                {
                    string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = constr;
                        conn.Open();
                        string[] strAry = conversationid.Split(',');
                        for (int i = 0; i < strAry.Length; i++)
                        {
                            ClearChatLog(Convert.ToInt64(strAry[i]), currentuserid, conn);
                        }
                    }
                }
                else
                {
                    result = "error";
                }


            }
            catch (Exception)
            {

                result = "error";
            }
            return result;
        }


        public void UpdateReadStatus(long conversationid, long userid, int readstatus, int mesgtype)
        {
            string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();

            using (SqlConnection conn = new SqlConnection(constr))
            {
                //var cmd1 = "UPDATE Hick_Conversation_log SET Read_Status=@readstatus WHERE Conversation_Id=@convestnid AND Message_To=@currentuser AND Message_Type=@mesgtyp";
                using (SqlCommand command = new SqlCommand("sp_hick_UpdateReadStatus", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    command.Parameters.AddWithValue("@convestnid", conversationid);
                    command.Parameters.AddWithValue("@currentuser", userid);
                    command.Parameters.AddWithValue("@readstatus", readstatus);
                    command.Parameters.AddWithValue("@mesgtyp", mesgtype);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateVideoCallStatus(long conversationid, string videoid, string parentvideoid, int readstatus, int messagetype, long groupid)
        {
            string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();

            using (SqlConnection conn = new SqlConnection(constr))
            {

                //StringBuilder cmd1 = new StringBuilder();

                //if (groupid == 0)
                //{
                //    if (parentvideoid == string.Empty)
                //    {
                //        cmd1.AppendLine("UPDATE Hick_VideoConversation_Log SET Status=@readstatus");
                //        cmd1.AppendLine("WHERE ConversationId=@conversationid AND VideoId like @videoid AND ParentVideoId IS NULL");
                //    }
                //    else
                //    {
                //        cmd1.AppendLine("UPDATE Hick_VideoConversation_Log SET Status=@readstatus,ConversationDate=(SELECT MAX(ConversationDate) FROM Hick_VideoConversation_Log WHERE ConversationId=@conversationid),ConversationEndTime=@endtime");
                //        cmd1.AppendLine("WHERE ConversationId=@conversationid AND ((VideoId like @videoid AND ParentVideoId like @parentvideoid) OR (VideoId like @parentvideoid AND ParentVideoId like @videoid))");
                //    }

                //    cmd1.AppendLine("AND MessageType=@mesgtyp");

                //    using (SqlCommand command = new SqlCommand(cmd1.ToString(), conn))
                //    {
                //        conn.Open();
                //        command.Parameters.AddWithValue("@conversationid", conversationid);
                //        command.Parameters.AddWithValue("@videoid", videoid);
                //        command.Parameters.AddWithValue("@parentvideoid", parentvideoid);
                //        command.Parameters.AddWithValue("@readstatus", readstatus);
                //        command.Parameters.AddWithValue("@mesgtyp", messagetype);
                //        command.Parameters.AddWithValue("@endtime", DateTime.UtcNow);
                //        command.ExecuteNonQuery();
                //    }
                //}
                //else
                //{
                //    cmd1.AppendLine("UPDATE Hick_VideoConversation_Log SET Status=@readstatus,ConversationEndTime=@endtime");
                //    cmd1.AppendLine("WHERE ConversationId=@conversationid");
                //    using (SqlCommand command = new SqlCommand(cmd1.ToString(), conn))
                //    {
                //        conn.Open();
                //        command.Parameters.AddWithValue("@conversationid", conversationid);
                //        command.Parameters.AddWithValue("@readstatus", readstatus);
                //        command.Parameters.AddWithValue("@endtime", DateTime.UtcNow);
                //        command.ExecuteNonQuery();
                //    }
                //}

                using (SqlCommand command = new SqlCommand("sp_hick_UpdateVideoCallStatus", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@conversationid", conversationid);
                    command.Parameters.AddWithValue("@videoid", videoid);
                    command.Parameters.AddWithValue("@parentvideoid", parentvideoid);
                    command.Parameters.AddWithValue("@readstatus", readstatus);
                    command.Parameters.AddWithValue("@mesgtyp", messagetype);
                    command.Parameters.AddWithValue("@endtime", DateTime.Now);
                    command.Parameters.AddWithValue("@groupId", groupid);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateParentVedioId(long conversationid, string videoid, string parentvideoid)
        {
            string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();

            using (SqlConnection conn = new SqlConnection(constr))
            {

                //StringBuilder cmd1 = new StringBuilder();
                //cmd1.AppendLine("UPDATE Hick_VideoConversation_Log SET ParentVideoId=@parentvideoid");
                //cmd1.AppendLine("WHERE ConversationId=@conversationid AND VideoId like @videoid");

                using (SqlCommand command = new SqlCommand("sp_hick_UpdateParentVedioId", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    command.Parameters.AddWithValue("@conversationid", conversationid);
                    command.Parameters.AddWithValue("@videoid", videoid);
                    command.Parameters.AddWithValue("@parentvideoid", parentvideoid);

                    command.ExecuteNonQuery();
                }
            }
        }

        [OperationContract]
        public List<VideoChatLog> GetVideoChatLog(long currentid, long peerid, string logdate, string timezone, long groupid)
        {
            try
            {
                List<VideoChatLog> objvideologcoll = new List<VideoChatLog>();
                List<VideoChatLog> objvideologcollnew = new List<VideoChatLog>();
                if (currentid != 0)
                {

                    string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();

                    ConversationLog objlog = null;

                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = constr;
                        conn.Open();
                        bool isexist = false;
                        //string selcmd = string.Empty;
                        //if (groupid == 0)
                        //{
                        //    selcmd = "select * from Hick_user_Conversation where ((Initiator=" + currentid + " and Answerer=" + peerid + ") or (Initiator=" + peerid + " and Answerer=" + currentid + ")) and ConversationDate='" + Utility.ConvertDateToUTC(timezone, Convert.ToDateTime(logdate)) + "'";
                        //}
                        //else
                        //{
                        //    selcmd = "select * from Hick_user_Conversation where group_id=" + groupid + " and ConversationDate='" + Utility.ConvertDateToUTC(timezone, Convert.ToDateTime(logdate)) + "'";
                        //}
                        using (SqlCommand command = new SqlCommand("sp_hick_FetchUserConversationForToday", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@CurrentUserId", currentid);
                            command.Parameters.AddWithValue("@PeerId", peerid);
                            command.Parameters.AddWithValue("@Date", Utility.ConvertDateToUTC(timezone, DateTime.ParseExact(logdate, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture)));
                            command.Parameters.AddWithValue("@GroupId", groupid);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    isexist = true;
                                    while (reader.Read())
                                    {
                                        objlog = new ConversationLog();
                                        objlog.Id = Convert.ToInt32(reader["ID"]);
                                        //objcoll.Add(objlog);
                                    }
                                }
                            }
                        }
                        if (isexist == true)
                        {
                            /* fetching video log history*/
                            //using (SqlCommand command = new SqlCommand("select *,peeruser=(select Username from Hick_Users where ID=l.peerid) from Hick_VideoConversation_Log as l where ConversationId=" + objlog.Id + " order by l.ConversationDate asc", conn))
                            using (SqlCommand command = new SqlCommand("sp_hick_GetVideoChatLog", conn))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@ConvId", objlog.Id);

                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            VideoChatLog objvideolog = new VideoChatLog();
                                            objvideolog.ConversationId = Convert.ToInt32(reader["ConversationId"]);
                                            objvideolog.VideoId = Convert.ToString(reader["VideoId"]);
                                            objvideolog.ParentVideoId = Convert.ToString(reader["ParentVideoId"]);
                                            // objvideolog.ConversationDate = Convert.ToString(reader["ConversationDate"]);
                                            objvideolog.PeerName = Convert.ToString(reader["peeruser"]);
                                            objvideolog.MessageType = Convert.ToInt32(reader["MessageType"]);
                                            objvideolog.Status = Convert.ToInt32(reader["Status"]);
                                            objvideolog.PeerID = Convert.ToInt32(reader["PeerId"]);

                                            DateTime dtraw = DateTime.ParseExact(reader["ConversationDate"].ToString(), "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);
                                            objvideolog.ConversationDate = Convert.ToString(Utility.ConvertDateToLocal(timezone, dtraw));
                                            
                                            objvideolog.ConversationEndTime = reader["ConversationEndTime"] != DBNull.Value ? Convert.ToString(Utility.ConvertDateToLocal(timezone, DateTime.ParseExact(reader["ConversationEndTime"].ToString(), "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture))) : string.Empty;
                                            DateTime dt = Utility.ConvertDateToLocal(timezone, dtraw);

                                            objvideolog.Time = dt.ToString("MMM dd") + ", " + dt.ToString("hh:mm tt");


                                            objvideologcoll.Add(objvideolog);
                                        }
                                    }


                                    //return objcoll;
                                }
                            }


                        }

                    }

                    if (objvideologcoll.Count > 0)
                    {

                        for (int i = 0; i < objvideologcoll.Count; i++)
                        {
                            var isexist = objvideologcollnew.Where(m => m.ParentVideoId == objvideologcoll[i].VideoId).FirstOrDefault();
                            if (isexist == null)
                            {
                                if (String.IsNullOrEmpty(objvideologcoll[i].ParentVideoId))
                                {
                                    objvideologcoll[i].Duration = "00:00:00";

                                }
                                else
                                {
                                    DateTime dt1 = Convert.ToDateTime(objvideologcoll[i].ConversationDate).ToLocalTime();
                                    var objprnt = objvideologcoll.Where(m => m.VideoId == objvideologcoll[i].ParentVideoId).FirstOrDefault();
                                    if (objprnt != null)
                                    {
                                        //DateTime dt2 = Convert.ToDateTime(objprnt.ConversationDate).ToLocalTime();
                                        if (!String.IsNullOrEmpty(objprnt.ConversationEndTime))
                                        {
                                            DateTime dt2 = Convert.ToDateTime(objprnt.ConversationEndTime).ToLocalTime();
                                            TimeSpan duration = new TimeSpan(dt2.Ticks - dt1.Ticks);
                                            objvideologcoll[i].Duration = Convert.ToString(duration).Replace("-", "");
                                        }
                                        else
                                        {
                                            objvideologcoll[i].Duration = "00:00:00";
                                        }
                                    }
                                }

                                objvideologcollnew.Add(objvideologcoll[i]);
                            }

                        }
                    }

                }
                return objvideologcollnew;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [OperationContract]
        public List<ExportChatLog> GetChatLogForExport(long currentuserid, string conversationid, string flag, string timezone, long groupid, string logdate)
        {
            try
            {
                HttpContext.Current.Session["ExportChatColl"] = null;
                string _filnam = string.Empty;
                string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                List<ExportChatLog> objlogColl = new List<ExportChatLog>();
                ExportChatLog objlog = null;

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = constr;
                    conn.Open();
                    //StringBuilder sb = new StringBuilder();
                    if (!String.IsNullOrEmpty(flag) && flag.Equals("yes"))
                    {
                        _filnam = "IgniteChat" + String.Format("{0}", DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString()) + DateTime.Now.Day + DateTime.Now.Year + ".txt";
                        //sb.AppendLine("select *,peeruser=(select top 1 Username from Hick_Users where ID=l.peerid) from Hick_Conversation_log as l");
                        //sb.AppendLine("left outer join Hick_Clear_Log as cl on l.Conversation_Id=cl.Conversation_Id and cl.UserId=" + currentuserid + "");
                        //if (groupid != 0)
                        //{
                        //    sb.AppendLine("left outer join hick_previous_log as Prlog on l.Conversation_Id=Prlog.conversation_id");
                        //}

                        //sb.AppendLine("where l.Conversation_Id IN (" + conversationid + ")");
                        //sb.AppendLine("and CONVERT(TIME, l.Conversation_date,108)>= CASE WHEN  cl.Cleared_Date IS NULL THEN");
                        //sb.AppendLine("CONVERT(TIME, l.Conversation_date,108) ELSE CONVERT(Time,cl.Cleared_Date,108) END");
                        //sb.AppendLine("and (l.Message_Type=" + (int)MessageTypes.Text + " OR l.Message_Type=" + (int)MessageTypes.File + ")");
                        //if (groupid != 0)
                        //{
                        //    sb.AppendLine("and ISNULL(Prlog.created_date,l.Conversation_date)>=l.Conversation_date");
                        //    sb.AppendLine("and ISNULL(Prlog.group_id,0)=Case when Prlog.group_id IS NULL Then 0 Else " + groupid + " end");
                        //}
                        //sb.AppendLine("order by l.Id asc");
                    }
                    else
                    {

                        _filnam = "IgniteChatLog" + String.Format("{0}", DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString()) + DateTime.Now.Day + DateTime.Now.Year + ".txt";
                        //    sb.AppendLine("select *,peeruser=(select top 1 Username from Hick_Users where ID=l.peerid) from Hick_Conversation_log as l");

                        //    sb.AppendLine("where (l.Conversation_Id IN (" + conversationid + ")");
                        //    sb.AppendLine("OR l.Conversation_Id=(select top 1 conversation_id from hick_previous_log where group_id=" + groupid + " and l.Conversation_date<=created_date and Convert(date,created_date)=Convert(date,'" + Utility.ConvertDateToUTC(timezone, Convert.ToDateTime(logdate)) + "')))");
                        //    sb.AppendLine("and (l.Message_Type=" + (int)MessageTypes.Text + " OR l.Message_Type=" + (int)MessageTypes.File + ")");

                        //    sb.AppendLine("order by l.Id asc");

                    }
                    using (SqlCommand command = new SqlCommand("sp_hick_GetChatLogForExport", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CurrentUId", currentuserid);
                        command.Parameters.AddWithValue("@GroupId", groupid);
                        command.Parameters.AddWithValue("@ConvId", conversationid);
                        command.Parameters.AddWithValue("@MsgTypeText", (int)MessageTypes.Text);
                        command.Parameters.AddWithValue("@MsgTypeFile", (int)MessageTypes.File);
                        command.Parameters.AddWithValue("@Flag", flag);
                        if (!String.IsNullOrEmpty(flag) && flag.Equals("yes"))
                        {
                            command.Parameters.AddWithValue("@Date", DateTime.UtcNow);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Date", Utility.ConvertDateToUTC(timezone, Convert.ToDateTime(logdate)));
                        }

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                objlog = new ExportChatLog();
                                objlog.Conversation = Convert.ToString(reader["Conversation_log"]);
                                objlog.Name = Convert.ToString(ecd.DecryptData(reader["peeruser"].ToString(),ecd.GetEncryptType()));
                                DateTime dtraw = Convert.ToDateTime(reader["Conversation_date"]);
                                DateTime dt = Utility.ConvertDateToLocal(timezone, dtraw);
                                //objlog.Time = conversationid.Contains(',') ? dt.ToString("MMM dd") + ", " + dt.ToString("hh:mm tt") : dt.ToString("hh:mm tt");
                                objlog.Time = dt.ToString("MMM dd") + ", " + dt.ToString("hh:mm tt");
                                objlog.MessageType = Convert.ToInt32(reader["Message_Type"]);
                                objlogColl.Add(objlog);
                            }

                        }
                    }
                }
                return objlogColl;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [OperationContract]
        public string GetUsersTimeZone(long ofsetminutes)
        {
            if (ofsetminutes > 0)
            {
                ofsetminutes = -ofsetminutes;
            }
            else
            {
                ofsetminutes = Math.Abs(ofsetminutes);
            }
            string tid = TimeZoneInfo.GetSystemTimeZones().Where(m => m.BaseUtcOffset.TotalMinutes == (double)ofsetminutes).Select(m => m.Id).FirstOrDefault();
            return tid;
        }

        [OperationContract]
        public void UpdateFavorites(long currentid, long peerid)
        {
            try
            {
                if (currentid != 0 && peerid != 0)
                {

                    string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();



                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = constr;
                        conn.Open();
                        bool isexist = false;

                        //using (SqlCommand command = new SqlCommand("select * from hick_favourite_contacts where ((user_id=" + currentid + " and fav_contact_id=" + peerid + "))", conn))
                        using (SqlCommand command = new SqlCommand("sp_hick_FetchAllFavouriteContacts", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@CurrentId", currentid);
                            command.Parameters.AddWithValue("@PeerId", peerid);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    isexist = true;
                                    string _updateCmd;
                                    while (reader.Read())
                                    {
                                        //if (Convert.ToInt32(reader["fav_status"]) == 1)
                                        //{
                                        //    _updateCmd = "Update hick_favourite_contacts set fav_status=2 where ((user_id=" + currentid + " and fav_contact_id=" + peerid + "))";
                                        //}
                                        //else
                                        //{
                                        //    _updateCmd = "Update hick_favourite_contacts set fav_status= '1' where ((user_id=" + currentid + " and fav_contact_id=" + peerid + "))";
                                        //}
                                        using (SqlCommand updatecmd = new SqlCommand("sp_hick_UpdateFavouriteContacts", conn))
                                        {
                                            updatecmd.CommandType = CommandType.StoredProcedure;
                                            updatecmd.Parameters.AddWithValue("@CurrentId", currentid);
                                            updatecmd.Parameters.AddWithValue("@PeerId", peerid);
                                            if (Convert.ToInt32(reader["fav_status"]) == 1)
                                            {
                                                updatecmd.Parameters.AddWithValue("@FavStatus", 2);
                                            }
                                            else
                                            {
                                                updatecmd.Parameters.AddWithValue("@FavStatus", 1);
                                            }

                                            SqlDataReader updateReader = updatecmd.ExecuteReader();
                                        }
                                    }
                                }
                            }
                        }
                        if (!isexist)
                        {
                            //var cmd = "INSERT INTO hick_favourite_contacts (user_id,fav_contact_id,fav_status) VALUES (@curuser,@peeruser,@status)";
                            using (SqlCommand command = new SqlCommand("sp_hick_InsertFavouriteContacts", conn))
                            {
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.AddWithValue("@curuser", currentid);
                                command.Parameters.AddWithValue("@peeruser", peerid);
                                command.Parameters.AddWithValue("@status", 1);
                                command.ExecuteReader();

                            }
                        }

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [OperationContract]
        public void AddGroup(long userid, string groupuserids, string groupname)
        {
            try
            {
                groupuserids = groupuserids + ',' + userid.ToString();
                string uniqueGroupId = GetUniqueGroupId("", groupuserids);
                HickGroups existingGroup = GetGroupByUniqueId(uniqueGroupId);

                if (existingGroup != null)
                {
                    // Group with selected users set already rexist;
                }
                else
                {
                    string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();

                    using (SqlConnection conn = new SqlConnection(constr))
                    {
                        conn.ConnectionString = constr;
                        conn.Open();
                        long groupid;
                        //var cmd = "INSERT INTO hick_groups (group_name,created_by,created_date,group_status,group_unique_key) VALUES (@grpName,@createdBy,@createdDate,@grpStatus,@group_unique_key);SELECT CAST(scope_identity() AS bigint)";
                        using (SqlCommand command = new SqlCommand("sp_hick_InsertGroups", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            //command.Parameters.AddWithValue("@grpName", groupname);
                            command.Parameters.AddWithValue("@createdBy", userid);
                            command.Parameters.AddWithValue("@createdDate", DateTime.UtcNow.ToShortDateString());
                            //command.Parameters.AddWithValue("@grpStatus", 1);
                            command.Parameters.AddWithValue("@group_unique_key", uniqueGroupId);
                            groupid = (long)command.ExecuteScalar();
                        }


                        //var insertGroupUsers = "insert into hick_group_users (group_id,user_id,grp_user_status) values (@grpId,@userId,@grpStatus)";
                        string[] grpIds = groupuserids.Split(',');
                        for (int i = 0; i < grpIds.Length; i++)
                        {
                            using (SqlCommand insertCommand = new SqlCommand("sp_hick_InsertGroupUsers", conn))
                            {
                                insertCommand.CommandType = CommandType.StoredProcedure;

                                insertCommand.Parameters.AddWithValue("@grpId", groupid);
                                insertCommand.Parameters.AddWithValue("@userId", grpIds[i]);
                                insertCommand.Parameters.AddWithValue("@grpStatus", 1);
                                insertCommand.ExecuteScalar();
                            }
                        }

                    }
                }
                //string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                ////groupuserids = groupuserids + ',' + userid.ToString();
                //string[] grpIds = groupuserids.Split(',');

                ////int len = grpIds.Length;
                ////grpIds[len+1]=userid.ToString();
                //int[] Ids = new int[grpIds.Length];
                //for (int i = 0; i < grpIds.Length; i++)
                //{
                //    Ids[i] = Convert.ToInt32(grpIds[i]);
                //}
                //Array.Sort(Ids);
                //for (int i = 0; i < Ids.Length; i++)
                //{
                //    grpIds[i] = Ids[i].ToString();
                //    grpUniqueId = grpUniqueId + "|" + grpIds[i];
                //}

                //using (SqlConnection conn = new SqlConnection(constr))
                //{
                //    conn.ConnectionString = constr;
                //    conn.Open();
                //    long groupid;
                //    var cmd = "INSERT INTO hick_groups (group_name,created_by,created_date,group_status) VALUES (@grpName,@createdBy,@createdDate,@grpStatus);SELECT CAST(scope_identity() AS bigint)";
                //    using (SqlCommand command = new SqlCommand(cmd, conn))
                //    {
                //        command.Parameters.AddWithValue("@grpName", groupname);
                //        command.Parameters.AddWithValue("@createdBy", userid);
                //        command.Parameters.AddWithValue("@createdDate", DateTime.UtcNow.ToShortDateString());
                //        command.Parameters.AddWithValue("@grpStatus", 1);
                //        groupid = (long)command.ExecuteScalar();
                //    }


                //    var insertGroupUsers = "insert into hick_group_users (group_id,user_id,grp_user_status) values (@grpId,@userId,@grpStatus)";
                //    for (int i = 0; i < grpIds.Length; i++)
                //    {
                //        using (SqlCommand insertCommand = new SqlCommand(insertGroupUsers, conn))
                //        {
                //            insertCommand.Parameters.AddWithValue("@grpId", groupid);
                //            insertCommand.Parameters.AddWithValue("@userId", grpIds[i]);
                //            insertCommand.Parameters.AddWithValue("@grpStatus", 1);
                //            insertCommand.ExecuteScalar();
                //        }
                //    }
                //}
            }
            catch (Exception e)
            {

            }
        }
        private string GetUniqueGroupId(string userId, string groupUsers)
        {
            string groupSeperator = "-";
            List<int> usersList = new List<int>();

            if (!string.IsNullOrEmpty(userId))
            {
                usersList.Add(int.Parse(userId));

                foreach (string user in groupUsers.Split(','))
                    usersList.Add(int.Parse(user));

                usersList.Sort();

                return string.Join(groupSeperator, usersList.ToArray());
            }
            else
            {
                foreach (string user in groupUsers.Split(','))
                    usersList.Add(int.Parse(user));

                usersList.Sort();

                return string.Join(groupSeperator, usersList.ToArray());
            }
        }

        private HickGroups GetGroupByUniqueId(string uniqueId)
        {
            HickGroups group = null;
            string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
            using (SqlConnection conn = new SqlConnection(constr))
            {

                conn.Open();
                //string query = @"SELECT  * FROM hick_groups WHERE group_unique_key='{0}'";
                //query = string.Format(query, uniqueId);

                using (SqlCommand command = new SqlCommand("sp_hick_GetGroupByUniqueId", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UniqueId", uniqueId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                group = new HickGroups();
                                group.Id = Convert.ToInt32(reader["id"]);
                                return group;
                            }
                        }
                    }
                }
            }
            return group;

        }
        [OperationContract]
        public UserConversation AddUserToGroup(long currentuserid, long groupid, long selecteduser, long peerid, long conversationid)
        {

            try
            {
                string uniqueGroupId = string.Empty;
                long _groupid = 0;
                UserConversation objuc = new UserConversation();
                string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    conn.ConnectionString = constr;
                    conn.Open();
                    StringBuilder sb = new StringBuilder();

                    if (groupid == 0)
                    {
                        /* create new group*/
                        //if (selecteduser != peerid)
                        //{
                        //    string userId = currentuserid.ToString();
                        //    string groupUsers = selecteduser.ToString() + ',' + peerid.ToString();

                        //    uniqueGroupId = GetUniqueGroupId(userId, groupUsers);
                        //}
                        //else
                        //{
                        //    uniqueGroupId = GetUniqueGroupId(currentuserid.ToString(), selecteduser.ToString());
                        //}
                        if (peerid != 0)
                        {
                            uniqueGroupId = GetUniqueGroupId(currentuserid.ToString(), peerid.ToString());
                        }
                        else if (peerid == 0)
                        {
                            uniqueGroupId = GetUniqueGroupId(currentuserid.ToString(), selecteduser.ToString());
                        }

                        HickGroups existingGroup = GetGroupByUniqueId(uniqueGroupId);
                        if (existingGroup == null)
                        {
                            //sb.AppendLine("INSERT INTO hick_groups (created_by,created_date,group_unique_key) VALUES (@createdBy,@createdDate,@group_unique_key);SELECT CAST(scope_identity() AS bigint)");
                            using (SqlCommand command = new SqlCommand("sp_hick_InsertGroups", conn))
                            {
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.AddWithValue("@createdBy", currentuserid);
                                command.Parameters.AddWithValue("@createdDate", DateTime.UtcNow);
                                command.Parameters.AddWithValue("@group_unique_key", uniqueGroupId);
                                _groupid = (long)command.ExecuteScalar();
                            }
                            if (_groupid != 0)
                            {
                                /* adding loggedin user to the created group */
                                //sb = new StringBuilder();
                                //sb.AppendLine("INSERT INTO hick_group_users (group_id,user_id,created_date) VALUES (@groupid,@userid,@createddate)");
                                //if (peerid != 0)
                                //{
                                //    sb.AppendLine("INSERT INTO hick_group_users (group_id,user_id,created_date) VALUES (@groupid,@peerid,@createddate)");
                                //    //sb.AppendLine("UPDATE Hick_Conversation_log SET group_id=@groupid WHERE Conversation_Id=@converstnid");
                                //    sb.AppendLine("INSERT INTO hick_previous_log (group_id,conversation_id,created_date) VALUES (@groupid,@converstnid,@createddate)");

                                //}
                                //else if (peerid == 0)
                                //{
                                //    sb.AppendLine("INSERT INTO hick_group_users (group_id,user_id,created_date) VALUES (@groupid,@selecteduser,@createddate)");
                                //}
                                using (SqlCommand command = new SqlCommand("sp_hik_AddUsersToGroup", conn))
                                {
                                    command.CommandType = CommandType.StoredProcedure;

                                    command.Parameters.AddWithValue("@groupid", _groupid);
                                    command.Parameters.AddWithValue("@userid", currentuserid);
                                    command.Parameters.AddWithValue("@createddate", DateTime.UtcNow);
                                    //if (peerid != 0)
                                    //{
                                    //    command.Parameters.AddWithValue("@peerid", peerid);
                                    //    command.Parameters.AddWithValue("@converstnid", conversationid);
                                    //}
                                    //else if (peerid == 0)
                                    //{
                                    //    command.Parameters.AddWithValue("@selecteduser", selecteduser);
                                    //}

                                    command.Parameters.AddWithValue("@peerid", peerid);
                                    command.Parameters.AddWithValue("@converstnid", conversationid);
                                    command.Parameters.AddWithValue("@selecteduser", selecteduser);
                                    command.ExecuteNonQuery();
                                }

                            }
                        }
                        else
                        {
                            _groupid = existingGroup.Id;
                        }
                    }
                    else
                    {
                        _groupid = groupid;
                    }
                    if (_groupid != 0)
                    {
                        #region
                        /* Initiate group chat */
                        objuc.Id = InitiateGroupChat(_groupid);
                        objuc.GroupId = _groupid;

                        /* checking for the user already exist */
                        bool isuserExist = false;
                        //sb = new StringBuilder();
                        //sb.AppendLine("SELECT * FROM hick_group_users WHERE group_id=@groupid AND user_id=@userid");
                        using (SqlCommand command = new SqlCommand("sp_hick_FetchAllGroupUsers", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@groupid", _groupid);
                            command.Parameters.AddWithValue("@userid", selecteduser);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    isuserExist = true;
                                }
                            }
                        }
                        if (!isuserExist)
                        {
                            /* adding selected user to the created group */
                            string uniqueKey = string.Empty;
                            bool userExists = false;
                            List<string> usersList = new List<string>();
                            //string cmdSelectGroup = string.Empty;

                            //cmdSelectGroup = "select * from hick_groups where id=" + _groupid;
                            using (SqlCommand command = new SqlCommand("sp_hick_FetchAllGroups", conn))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@GroupId", _groupid);

                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            uniqueKey = Convert.ToString(reader["group_unique_key"]);
                                        }
                                    }
                                }
                            }
                            string[] groupUsers = uniqueKey.Split('-');
                            for (int i = 0; i < groupUsers.Length; i++)
                            {
                                if (groupUsers[i] == selecteduser.ToString())
                                {
                                    //selected user already exists;
                                    userExists = true;
                                }
                                usersList.Add(groupUsers[i]);
                            }

                            if (userExists == false)
                            {
                                string seperator = ",";
                                usersList.Add(selecteduser.ToString());
                                string users = string.Join(seperator, usersList.ToArray());
                                uniqueGroupId = GetUniqueGroupId("", users);

                                HickGroups existingGroup = GetGroupByUniqueId(uniqueGroupId);

                                if (existingGroup != null)
                                {
                                    // Group with selected users set already rexist;
                                }
                                else
                                {
                                    //update existing group
                                    //sb = new StringBuilder();
                                    //sb.AppendLine("UPDATE hick_groups SET group_unique_key='" + uniqueGroupId + "', created_date='" + DateTime.UtcNow + "' where id=" + _groupid + "");
                                    //sb.AppendLine("INSERT INTO hick_group_users (group_id,user_id,created_date) values (@grpId,@userId,@createddate)");
                                    // var cmd = "UPDATE hick_groups SET group_unique_key='" + uniqueGroupId + "', created_date='" + DateTime.UtcNow + "' where id=" + groupid;
                                    using (SqlCommand command = new SqlCommand("sp_hick_UpdGroupsInsGroupUsers", conn))
                                    {
                                        command.CommandType = CommandType.StoredProcedure;

                                        command.Parameters.AddWithValue("@grpId", _groupid);
                                        command.Parameters.AddWithValue("@userId", selecteduser);
                                        command.Parameters.AddWithValue("@createddate", DateTime.UtcNow);
                                        command.Parameters.AddWithValue("@uniqueId", uniqueGroupId);
                                        command.ExecuteNonQuery();
                                    }

                                    //var insertGroupUsers = "insert into hick_group_users (group_id,user_id,created_date) values (@grpId,@userId,@createddate)";
                                    //using (SqlCommand insertCommand = new SqlCommand(insertGroupUsers, conn))
                                    //{
                                    //    insertCommand.Parameters.AddWithValue("@grpId", groupid);
                                    //    insertCommand.Parameters.AddWithValue("@userId", selecteduser);
                                    //    insertCommand.Parameters.AddWithValue("@createddate", DateTime.UtcNow);
                                    //    //insertCommand.Parameters.AddWithValue("@grpStatus", 1);
                                    //    insertCommand.ExecuteNonQuery();
                                    //}
                                }


                            }
                            else
                            {
                                //user already exists
                            }


                            //sb = new StringBuilder();
                            //sb.AppendLine("INSERT INTO hick_group_users (group_id,user_id,created_date) VALUES (@groupid,@userid,@createddate)");

                            //using (SqlCommand command = new SqlCommand(sb.ToString(), conn))
                            //{
                            //    command.Parameters.AddWithValue("@groupid", _groupid);
                            //    command.Parameters.AddWithValue("@userid", selecteduser);
                            //    command.Parameters.AddWithValue("@createddate", DateTime.UtcNow);
                            //    command.ExecuteNonQuery();
                            //}
                        }
                        #endregion

                    }
                }
                return objuc;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [OperationContract]
        public List<HickGroups> GetGroups(long currentuserid)
        {
            try
            {
                List<HickGroups> objgroupColl = new List<HickGroups>();
                string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                StringBuilder sb = new StringBuilder();


                //sb.Append(@"select * from hick_groups where id in (select Distinct id  from hick_groups where created_by={0}) ");
                //sb.Append(@"OR id in (select Distinct group_id  from hick_group_users where user_id={0}) ; ");
                //string query = sb.ToString();
                //query = string.Format(query, currentuserid);
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    conn.ConnectionString = constr;
                    conn.Open();

                    using (SqlCommand command = new SqlCommand("sp_hick_FetchGroupsForCurUsr", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UsrId", currentuserid);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    HickGroups objgrp = new HickGroups();
                                    objgrp.Id = Convert.ToInt32(reader["id"]);
                                    objgroupColl.Add(objgrp);
                                }

                            }
                        }
                    }
                    if (objgroupColl.Count > 0)
                    {
                        for (int i = 0; i < objgroupColl.Count; i++)
                        {
                            long groupid = objgroupColl[i].Id;
                            //sb = new StringBuilder();
                            //sb.AppendLine("SELECT * FROM hick_group_users AS GU");
                            //sb.AppendLine("INNER JOIN Hick_Users AS U ON GU.user_id=U.ID");
                            //sb.AppendLine("WHERE GU.group_id=" + objgroupColl[i].Id + "");
                            using (SqlCommand command = new SqlCommand("sp_hick_FetchUsersOfGroup", conn))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@GroupId", objgroupColl[i].Id);

                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        objgroupColl[i].GroupUsersColl = new List<HickGroupUsers>();
                                        while (reader.Read())
                                        {
                                            HickGroupUsers objgrpuser = new HickGroupUsers();
                                            objgrpuser.GroupId = Convert.ToInt32(reader["group_id"]);
                                            objgrpuser.UserId = Convert.ToInt32(reader["user_id"]);

                                            objgrpuser.FullName = Convert.ToString(ecd.DecryptData((reader["Firstname"]).ToString(), ecd.GetEncryptType())) + " " + Convert.ToString(ecd.DecryptData((reader["Lastname"]).ToString(), ecd.GetEncryptType()));

                                            if (objgrpuser.UserId == currentuserid && reader["message_status"] != DBNull.Value && Convert.ToInt32(reader["message_status"]) == (int)ReadStatus.UnRead)
                                            {
                                                objgroupColl[i].IsUnreadMessage = true;
                                            }
                                            objgroupColl[i].GroupUsersColl.Add(objgrpuser);
                                        }

                                    }
                                }
                            }
                            sb = null;
                            //sb = new StringBuilder();
                            //sb.AppendLine("select * from Hick_user_Conversation UC");
                            //sb.AppendLine("INNER JOIN Hick_VideoConversation_Log CL on UC.ID=CL.ConversationId");
                            //sb.AppendLine("where UC.group_id=" + groupid + " and UC.ConversationDate=CONVERT(date, @date)");
                            //sb.AppendLine("and CL.Status=" + (int)ReadStatus.CallInitiated + "");
                            using (SqlCommand command = new SqlCommand("sp_hick_FetchVideoCallStatus", conn))
                            {
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.AddWithValue("@groupId", groupid);
                                command.Parameters.AddWithValue("@status", (int)ReadStatus.CallInitiated);
                                command.Parameters.AddWithValue("@date", DateTime.UtcNow);
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            if (currentuserid != Convert.ToInt32(reader["PeerId"]))
                                            {
                                                objgroupColl[i].IncomingCall = true;
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                }


                return objgroupColl;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public long InitiateGroupChat(long groupid)
        {

            try
            {
                long conversationid = 0;
                string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                StringBuilder sb = null;
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    conn.ConnectionString = constr;
                    conn.Open();
                    //sb = new StringBuilder();
                    bool isexist = false;
                    //sb.AppendLine("SELECT * FROM Hick_user_Conversation WHERE group_id=@groupid AND ConversationDate=CONVERT(date, @date)");
                    using (SqlCommand command = new SqlCommand("sp_hick_FetchGroupUsrConv", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@groupid", groupid);
                        command.Parameters.AddWithValue("@date", DateTime.UtcNow);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                isexist = true;
                                while (reader.Read())
                                {
                                    conversationid = Convert.ToInt32(reader["ID"]);
                                }
                            }
                        }
                    }
                    if (!isexist)
                    {
                        //sb = new StringBuilder();
                        //sb.AppendLine("INSERT INTO Hick_user_Conversation (ConversationDate,group_id) VALUES (@date,@groupid);SELECT CAST(scope_identity() AS bigint)");

                        using (SqlCommand command = new SqlCommand("sp_hick_InsertUserConv", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@groupid", groupid);
                            command.Parameters.AddWithValue("@date", DateTime.UtcNow);
                            conversationid = (long)command.ExecuteScalar();
                        }
                    }
                }
                return conversationid;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [OperationContract]
        public List<HickGroupUsers> FetchUsersByGroupId(string groupID)
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                List<HickGroupUsers> objlst = new List<HickGroupUsers>();
                HickGroupUsers objGroupUsers = null;
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = constr;
                    conn.Open();

                    StringBuilder sb = new StringBuilder();
                    //sb.AppendLine("select * from hick_group_users where group_id=" + groupID);
                    //sb.AppendLine("select gu.user_id,u.Firstname,u.Lastname from hick_group_users as gu inner join Hick_Users as u on u.ID=gu.user_id where gu.group_id=" + groupID);
                    using (SqlCommand command = new SqlCommand("sp_hick_FetchUsersByGroupId", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@GroupId", groupID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                objGroupUsers = new HickGroupUsers();
                                objGroupUsers.UserId = Convert.ToInt32(reader["user_id"]);
                               
                                objGroupUsers.FullName = Convert.ToString(ecd.DecryptData((reader["Firstname"].ToString()), ecd.GetEncryptType())) + " " + Convert.ToString(ecd.DecryptData((reader["Lastname"]).ToString(), ecd.GetEncryptType()));
                                
                                objlst.Add(objGroupUsers);
                            }
                        }
                    }
                }
                return objlst;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [OperationContract]
        public VideoChatSettings BroadcastGroupVideo(long conversationid, string peerid, string groupId)
        {

            VideoChatSettings objvideo = new VideoChatSettings();
            //try
            //{
            //    string guid = Convert.ToString(Guid.NewGuid());
            //    //objvideo = InitiateVideoChat(conversationid, guid, peerid, (int)MessageTypes.Video);

            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            return objvideo;
        }

        public void ClearChatLog(long conversationid, long currentuserid, SqlConnection conn)
        {
            bool isexist = false;
            ClearLog objlog = null;
            //string selcmd = string.Empty;


            //selcmd = "SELECT TOP 1 * FROM Hick_Clear_Log WHERE Conversation_Id=" + conversationid + " AND UserId=" + currentuserid + "";


            using (SqlCommand command = new SqlCommand("sp_hick_SelectTopClearLog", conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ConvId", conversationid);
                command.Parameters.AddWithValue("@CurUsrId", currentuserid);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        isexist = true;
                        while (reader.Read())
                        {
                            objlog = new ClearLog();
                            objlog.Id = Convert.ToInt32(reader["Id"]);
                        }
                    }
                }
            }
            if (!isexist)
            {
                //string cmd = string.Empty;

                //cmd = "INSERT INTO Hick_Clear_Log(Conversation_Id,UserId,Cleared_Date) VALUES(@conversationid,@userid,@date);";

                //if (groupid != 0)
                //{
                //   cmd = cmd + " UPDATE hick_previous_log SET created_date=@date WHERE group_id=@groupid";
                //}

                using (SqlCommand command = new SqlCommand("sp_hick_InsertClearLog", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@conversationid", conversationid);
                    command.Parameters.AddWithValue("@userid", currentuserid);
                    command.Parameters.AddWithValue("@date", DateTime.UtcNow);
                    //if (groupid != 0) {
                    //    command.Parameters.AddWithValue("@groupid", groupid);
                    //}
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                if (objlog != null)
                {
                    //var cmd = "UPDATE Hick_Clear_Log SET Cleared_Date=@date WHERE Id=@id;";


                    //if (groupid != 0) {
                    //    cmd = cmd + "UPDATE hick_previous_log SET created_date=@date WHERE group_id=@groupid";
                    //}                              
                    using (SqlCommand command = new SqlCommand("sp_hick_UpdateClearLog", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", objlog.Id);
                        command.Parameters.AddWithValue("@date", DateTime.UtcNow);
                        //if (groupid != 0)
                        //{
                        //    command.Parameters.AddWithValue("@groupid", groupid);
                        //}
                        command.ExecuteNonQuery();
                    }
                }
                //else
                //{
                //    result = "error";
                //}

            }

        }

        [OperationContract]
        public string CustomerName()
        {
            //string Customername = string.Empty;
            return Utility.CustomerName;
        }


        [OperationContract]
        public void TrackUsers(long currentuserid)
        {
            var users = new Dictionary<long, DateTime>(Hick.Models.Utility.TrackLoggedInUsers);

            if (users.Count > 0)
            {
                var a = users.Where(m => m.Key == currentuserid).Select(m => m).ToList();
                if (a.Count > 0)
                {
                    users[currentuserid] = DateTime.Now;
                }
                else
                {
                    users.Add(currentuserid, DateTime.Now);
                }
            }
            else
            {
                users.Add(currentuserid, DateTime.Now);
            }

            Hick.Models.Utility.TrackLoggedInUsers = users;
        }

        [OperationContract]
        public string TxtChatDuration(long currentuserid, int peerid)
        {
            try
            {
                string ChatDuration = "";
                TimeSpan duration = new TimeSpan();
                string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = constr;
                    conn.Open();
                    //StringBuilder sb = new StringBuilder();
                    //sb.AppendLine("select total_txtchat_dauration from Hick_user_Conversation where ((Initiator=" + currentuserid + " and Answerer=" + peerid + ") or (Initiator=" + peerid + " and Answerer=" + currentuserid + ")) and ConversationDate between DATEADD(DAY, 1-DAY(@date), DATEDIFF(DAY, 0, @date)) and @date");

                    using (SqlCommand command = new SqlCommand("sp_hick_TotalTextChatDurationForGivenUsers", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CurrentUserId", currentuserid);
                        command.Parameters.AddWithValue("@PeerId", peerid);
                        command.Parameters.AddWithValue("@Date", DateTime.UtcNow);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    if (reader["total_txtchat_dauration"] != System.DBNull.Value)
                                    {
                                        //duration = duration + TimeSpan.Parse(reader["total_txtchat_dauration"].ToString());
                                        Utility.CalculateTimeSpan(ref duration, Convert.ToString(reader["total_txtchat_dauration"]));
                                        ChatDuration = Convert.ToString(duration);
                                    }

                                }
                            }
                        }
                    }
                }

                return ChatDuration;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public string TaskDuration(long currentuserid, int peerid)
        {
            try
            {
                string duration = "";
                TimeSpan totaltime = new TimeSpan();
                string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = constr;
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_hick_TotalTaskDurationForGivenUsers", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CurrentUserId", currentuserid);
                        cmd.Parameters.AddWithValue("@PeerId", peerid);
                        cmd.Parameters.AddWithValue("@Date", DateTime.UtcNow);

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                string s = sdr["total_time"].ToString();
                                Utility.CalculateTimeSpan(ref totaltime, Convert.ToString(sdr["total_time"]));
                                duration = Convert.ToString(totaltime);
                            }
                        }
                    }
                }
                return duration;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string AudioCallDuration(int peerid)
        {
            try
            {
                string totalTime = string.Empty;
                TimeSpan duration = new TimeSpan();
                string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = constr;
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_hick_TotalAudioCallDurationForGivenPatient", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PeerId", peerid);
                        cmd.Parameters.AddWithValue("@Date", DateTime.UtcNow);

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                Utility.CalculateTimeSpan(ref duration, Convert.ToString(sdr["call_duration"]));
                                totalTime = Convert.ToString(duration);

                            }
                        }
                    }
                }
                return totalTime;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdateGroupMessageState(long groupId, long currentId)
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = constr;
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_hick_UpdateGroupMsgState", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@GroupId", groupId);
                        cmd.Parameters.AddWithValue("@CurId", currentId);
                        cmd.Parameters.AddWithValue("@Status", (int)ReadStatus.Read);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        private string GetActionTypeString(int v)
        {
            switch (v)
            {
                case 0:
                    return "View";
                case 1:
                    return "Download";
                case 2:
                    return "Transmit";
                default:
                    return "View";
            }
        }
    }
}
