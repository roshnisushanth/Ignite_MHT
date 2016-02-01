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
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Data.SqlTypes;
using Newtonsoft.Json.Linq;
using System.ServiceModel.Web;
using IGNITE_MODEL.AutherizedUser;
using IGNITE_MODEL.SessionNote;
using Dal.Encryption;

namespace Hick.Authorized
{
    [ServiceContract(Namespace = "")] 
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AuthorizedService
    {
        EncryptDecryptUtil ecd = new EncryptDecryptUtil();
        [OperationContract]
        public AutherizedUsersList GetAutherizedUsers(string action, long userId, long authorizedUserId)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("@action",action),
                 new SqlParameter("@UserId",userId),
                  new SqlParameter("@AutherizedUserId",authorizedUserId)
            };

            var list = new List<IGNITE_MODEL.AutherizedUser.AutherizedUser>();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "sp_hick_GetAuthorizedUser", sqlParms))
            {
                while (sqlobj.Read())
                {
                    var autherobj = new IGNITE_MODEL.AutherizedUser.AutherizedUser();
                    autherobj.AutherizedUserId = DBHelper.getInt64(sqlobj, "AuthorizedUserId");
                    //autherobj.AutherizedUserId = Convert.ToInt64(ecd.DecryptData( DBHelper.getString(sqlobj, "AuthorizedUserId"),ecd.GetEncryptType()));                    
                    autherobj.DOB = ecd.DecryptData(DBHelper.getString(sqlobj, "dateofbirth"), ecd.GetEncryptType());                   
                    autherobj.Email =  ecd.DecryptData(DBHelper.getString(sqlobj, "EmailID"),ecd.GetEncryptType());
                    autherobj.FirstName = ecd.DecryptData(DBHelper.getString(sqlobj, "Firstname"),ecd.GetEncryptType());
                    autherobj.LastName = ecd.DecryptData(DBHelper.getString(sqlobj, "Lastname"), ecd.GetEncryptType());
                    autherobj.Passcode = ecd.DecryptData(DBHelper.getString(sqlobj, "Password"),ecd.GetEncryptType());

                    autherobj.Relationship = DBHelper.getString(sqlobj, "Relationship");
                    autherobj.RelationshipOther = DBHelper.getString(sqlobj, "OtherRelationship");
                    autherobj.AccessHistory = (DBHelper.getDateTime(sqlobj, "LastAccessedIn")).ToString("MM/dd/yyyy").Replace("01/01/0001","N/A");
                    autherobj.AccessLevel = "Advanced";
                    list.Add(autherobj);
                }
            }

            return new AutherizedUsersList
            {
                AutherizedUsers = list
            };

        }



        [OperationContract]
        public AutherizedUsersList GetAutherizedUsersFor(long userId)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("@AutherizedUserId",userId)
            };

            var list = new List<IGNITE_MODEL.AutherizedUser.AutherizedUser>();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "sp_hick_GetAuthorizedUsersFor", sqlParms))
            {
                while (sqlobj.Read())
                {
                    var autherobj = new IGNITE_MODEL.AutherizedUser.AutherizedUser();
                    autherobj.UserId = DBHelper.getInt64(sqlobj, "UserId");
                    autherobj.ReferenceId= DBHelper.getInt64(sqlobj, "ReferenceId");
                    autherobj.DOB = ecd.DecryptData(DBHelper.getString(sqlobj, "dateofbirth"),ecd.GetEncryptType());
                    autherobj.Email = ecd.DecryptData(DBHelper.getString(sqlobj, "EmailID"), ecd.GetEncryptType());
                    autherobj.FirstName = ecd.DecryptData(DBHelper.getString(sqlobj, "Firstname"), ecd.GetEncryptType());
                    autherobj.LastName = ecd.DecryptData(DBHelper.getString(sqlobj, "Lastname"), ecd.GetEncryptType());
                    autherobj.Passcode = ecd.DecryptData(DBHelper.getString(sqlobj, "Password"), ecd.GetEncryptType());

                    autherobj.Relationship = DBHelper.getString(sqlobj, "Relationship");
                    autherobj.RelationshipOther = DBHelper.getString(sqlobj, "OtherRelationship");
                    autherobj.AccessLevel = "Advanced";
                    list.Add(autherobj);
                }
            }

            return new AutherizedUsersList
            {
                AutherizedUsers = list
            };
        }



        [OperationContract]
        public string AddAutherizedUsers(string action, long userid, long authorizeduserid, string firstname, string lastname, string dob, string relationship, string otherrelationship, string email, string password)
        {
            EncryptDecryptUtil enc = new EncryptDecryptUtil();

            SqlParameter[] sqlParms = new SqlParameter[]{


             //new SqlParameter("@Action",action),
             // new SqlParameter("@UserId",userid),
             //  new SqlParameter("@Autherizeduserid",authorizeduserid),
             //   new SqlParameter("@FirstName",firstname),
             //    new SqlParameter("@LastName",lastname),
             //     new SqlParameter("@Email",email),
             //      new SqlParameter("@DOB",dob),
             //       new SqlParameter("@Password",password),
             //        new SqlParameter("@Relationship",relationship),
             //         new SqlParameter("@OtherRelationship",otherrelationship),
             //          new SqlParameter("@CreatedDate",DateTime.UtcNow)
             

               new SqlParameter("@Action",action),
                new SqlParameter("@UserId",userid),
                  new SqlParameter("@Autherizeduserid",authorizeduserid),
                    new SqlParameter("@FirstName",firstname),
                     new SqlParameter("@LastName",lastname),
                      new SqlParameter("@Email",email),
                       new SqlParameter("@DOB",dob),
                        new SqlParameter("@Password",password),
                         new SqlParameter("@Relationship",relationship),
                          new SqlParameter("@OtherRelationship",otherrelationship),
                            new SqlParameter("@CreatedDate",DateTime.UtcNow)


            };

            var obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "sp_hick_InsertAuthorizedUser", sqlParms);
            return (string)obj;
        }


        [OperationContract]
        public int RevokeUsers(string action, long userid, long authorizeduserid)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
             new SqlParameter("@Action",action),
              new SqlParameter("@UserId",userid),
               new SqlParameter("@Autherizeduserid",authorizeduserid)

            };

            var obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "sp_hick_DeleteAuthorizedUser", sqlParms);
            if ((int)obj == 1)
            {
                return 1;
            }
            return (int)obj;
        }

        /// <summary>
        /// inserting audit log 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="actionType"></param>
        /// <param name="informationType"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <param name="actionDate"></param>
        /// <returns></returns>
        public string AddAuditLog(int userId, int actionType, string informationType, string oldValue, string newValue, DateTime actionDate)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
              new SqlParameter("@UserId",userId),
               new SqlParameter("@ActionType",actionType),
               new SqlParameter("@InformationType",informationType),
               new SqlParameter("@OldValue",oldValue),
               new SqlParameter("@NewValue",newValue),
               new SqlParameter("@ActionDate",actionDate)
            };

            var obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "sp_hick_InsertAuditLog", sqlParms);
            return obj.ToString();
        }

        public List<SessionNote> GetSessionNote(string userid,int peerId,int PageIndex,int PageSize)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("@UserId",userid),
                new SqlParameter("@PeerId",peerId)
            };
            var sessionNoteList = new List<SessionNote>();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "sp_hick_GetSessionNote", sqlParms))
            {
                while (sqlobj.Read())
                {
                    var sessionNote = new IGNITE_MODEL.SessionNote.SessionNote();
                    sessionNote.Id = DBHelper.getInt(sqlobj, "Id");
                    sessionNote.Date = DBHelper.getDateTime(sqlobj, "Date").ToString("MMM dd,yyyy");
                    sessionNote.Category = GetActionTypeString(DBHelper.getInt(sqlobj, "Category"));
                    sessionNote.Note = DBHelper.getString(sqlobj, "Note");
                    sessionNote.StartTime = string.Format("{0:hh:mm tt}", DBHelper.getDateTime(sqlobj, "StartTime"));
                    sessionNote.EndTime = string.Format("{0:hh:mm tt}", DBHelper.getDateTime(sqlobj, "EndTime"));
                    TimeSpan totaltime = (DBHelper.getDateTime(sqlobj, "EndTime").Subtract(DBHelper.getDateTime(sqlobj, "StartTime")));
                    sessionNote.TotalTime = ToLongString(totaltime);
                    sessionNoteList.Add(sessionNote);
                }
            }
            return sessionNoteList;
        }

        public SessionNote SessionNoteModification(string action, int id, string note)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("@action",action),
                new SqlParameter("@id",id),
                new SqlParameter("@note",note)
            };
            var sessionNote = new IGNITE_MODEL.SessionNote.SessionNote();
            using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "sp_hick_SessionNote_Modification", sqlParms))
            {
                while (sqlobj.Read())
                {
                    sessionNote.Id = DBHelper.getInt(sqlobj, "Id");
                    sessionNote.Date = DBHelper.getDateTime(sqlobj, "Date").ToString("MMMM dd, yyyy");
                    sessionNote.Category = GetActionTypeString(DBHelper.getInt16(sqlobj, "Category"));
                    sessionNote.Note = DBHelper.getString(sqlobj, "Note");
                    sessionNote.StartTime = DBHelper.getDateTime(sqlobj, "StartTime").ToShortTimeString();
                    sessionNote.EndTime = DBHelper.getDateTime(sqlobj, "EndTime").ToShortTimeString();
                    TimeSpan totaltime = (DBHelper.getDateTime(sqlobj, "EndTime").Subtract(DBHelper.getDateTime(sqlobj, "StartTime")));
                    sessionNote.TotalTime = ToLongString(totaltime);
                }
            }
            return sessionNote;
        }


        public string ToLongString(TimeSpan time)
        {
            string output = String.Empty;


            output += (time.Days > 0) ? time.Days + " d " + " " : 0 + "d"+" ";

            output += ((time.Days == 0 || time.Days == 1) && time.Hours > 0) ? time.Hours + " h " + " " : 0 + "h"+" ";

            output += (time.Days == 0 && time.Minutes > 0) ? time.Minutes + " m " + " " : 0 + "m"+" ";

            output += (output.Length == 0) ? time.Seconds + " s" : 0 + "s";

            if (output.Contains("-"))
            {
                output = 0 + "d" + " " + 0 + "h" + " " + 0 + "m" + " " + 0 + "s";
            }
            return output.Trim();
        }

        private string GetActionTypeString(int v)
        {
            switch (v)
            {
                case 2:
                    return "Audio Call";
                case 3:
                    return "Video Call";
                default:
                    return "Chat";
            }
        }
    }
}