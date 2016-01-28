using Hick.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Data;

namespace Hick
{
    /// <summary>
    /// Summary description for ExportChat
    /// </summary>
    public class ExportChat : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {


        public void ProcessRequest(HttpContext context)
        {



            long conversationid = Convert.ToInt32(context.Request.QueryString["conversationid"]);
            string flag = Convert.ToString(context.Request.QueryString["ischatwindow"]);
            long currentuserid = Convert.ToInt32(context.Request.QueryString["currentuserid"]);
            string msgtyp = Convert.ToString(context.Request.QueryString["messagetype"]);

            string _filnam = string.Empty;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                List<ExportChatLog> objlogColl = new List<ExportChatLog>();
                ExportChatLog objlog = null;

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = constr;
                    conn.Open();
                    StringBuilder sb = new StringBuilder();

                    /* check for video chat */
                    if (!String.IsNullOrEmpty(msgtyp) && msgtyp.Equals("2"))
                    {
                        _filnam = "IgniteVideoChatLog" + String.Format("{0}", DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString()) + DateTime.Now.Day + DateTime.Now.Year + ".txt";
                        //sb.AppendLine("select *,peeruser=(select top 1 Username from Hick_Users where ID=l.peerid) from Hick_VideoConversation_Log as l");
                        //sb.AppendLine("where l.ConversationId=" + conversationid + "");
                        //sb.AppendLine("and (l.MessageType=" + (int)MessageTypes.Video + ")");
                        //sb.AppendLine("order by l.ConversationDate asc");
                    }


                    if (!String.IsNullOrEmpty(msgtyp) && msgtyp.Equals("2"))
                    {
                        List<VideoChatLog> objvideologcoll = new List<VideoChatLog>();
                        List<VideoChatLog> objvideologcollnew = new List<VideoChatLog>();
                        string timezone = Convert.ToString(context.Session["TimeZone"]);
                        /* fetching video log history*/
                        //using (SqlCommand command = new SqlCommand(sb.ToString(), conn))
                        using (SqlCommand command = new SqlCommand("sp_hick_FetchVideoConversationLog", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@ConversationId", conversationid);
                            command.Parameters.AddWithValue("@MessageType", (int)MessageTypes.Video);

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
                                        DateTime dtraw = Convert.ToDateTime(reader["ConversationDate"]);
                                        objvideolog.ConversationDate = Convert.ToString(Utility.ConvertDateToLocal(timezone, dtraw));
                                        objvideolog.ConversationEndTime = reader["ConversationEndTime"] != DBNull.Value ? Convert.ToString(Utility.ConvertDateToLocal(timezone, Convert.ToDateTime(reader["ConversationEndTime"]))) : string.Empty;
                                        DateTime dt = Utility.ConvertDateToLocal(timezone, dtraw);
                                        objvideolog.Time = dt.ToString("MMM dd") + ", " + dt.ToString("hh:mm tt");

                                        objvideologcoll.Add(objvideolog);
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
                        if (objvideologcollnew.Count > 0)
                        {
                            for (int i = 0; i < objvideologcollnew.Count; i++)
                            {
                                objlog = new ExportChatLog();
                                objlog.Conversation = objvideologcollnew[i].Duration;
                                objlog.Name = objvideologcollnew[i].PeerName;
                                objlog.Time = objvideologcollnew[i].Time;
                                objlogColl.Add(objlog);
                            }
                        }
                    }
                    else
                    {
                        if (context.Session["ExportChatColl"] != null)
                        {
                            if (!String.IsNullOrEmpty(flag) && flag.Equals("yes"))
                            {
                                _filnam = "IgniteChat" + String.Format("{0}", DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString()) + DateTime.Now.Day + DateTime.Now.Year + ".txt";
                            }
                            else
                            {
                                _filnam = "IgniteChatLog" + String.Format("{0}", DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString()) + DateTime.Now.Day + DateTime.Now.Year + ".txt";
                            }
                            objlogColl = context.Session["ExportChatColl"] as List<ExportChatLog>;
                        }

                    }

                    if (objlogColl.Count > 0)
                    {
                        StringBuilder jsonsb = new StringBuilder();
                        for (int i = 0; i < objlogColl.Count; i++)
                        {
                            //jsonsb.AppendLine("{" + objlogColl[i].Name + ":" + objlogColl[i].Conversation + ":" + objlogColl[i].Time + ":" + objlogColl[i].MessageType + "}");
                            jsonsb.AppendLine(objlogColl[i].Name + ":" + objlogColl[i].Conversation + "," + objlogColl[i].Time);
                        }


                        //JavaScriptSerializer jss = new JavaScriptSerializer();
                        //StringBuilder jsonsb = new StringBuilder();
                        //string output = jss.Serialize(objlogColl);
                        //string[] strary = output.Split(new string[] { "}," }, StringSplitOptions.None);
                        //if (strary.Length > 0)
                        //{
                        //    /*arranging the each json message in new line */
                        //    for (int i = 0; i < strary.Length; i++)
                        //    {
                        //        jsonsb.AppendLine(i == strary.Length - 1 ? strary[i] : strary[i] + "},");
                        //    }
                        //}

                        byte[] buffer = System.Text.Encoding.ASCII.GetBytes(jsonsb.ToString());
                        context.Response.Clear();
                        context.Response.ContentType = "text/plain";
                        context.Response.AddHeader("Content-Disposition", String.Format("attachment; filename=\"{0}\"", _filnam));
                        context.Response.AddHeader("Content-Length", buffer.Length.ToString());
                        context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                        context.Response.Flush();
                        HttpContext.Current.ApplicationInstance.CompleteRequest();

                    }

                }

            }
            catch (Exception)
            {

                throw;
            }



            /* Old Code*/

            //long conversationid = Convert.ToInt32(context.Request.QueryString["conversationid"]);
            //string flag = Convert.ToString(context.Request.QueryString["ischatwindow"]);
            //long currentuserid = Convert.ToInt32(context.Request.QueryString["currentuserid"]);
            //string msgtyp = Convert.ToString(context.Request.QueryString["messagetype"]);

            //string _filnam = string.Empty;
            //try
            //{
            //    string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
            //    List<ExportChatLog> objlogColl = new List<ExportChatLog>();
            //    ExportChatLog objlog = null;

            //    using (SqlConnection conn = new SqlConnection())
            //    {
            //        conn.ConnectionString = constr;
            //        conn.Open();
            //        StringBuilder sb = new StringBuilder();
            //        if (!String.IsNullOrEmpty(flag) && flag.Equals("yes"))
            //        {
            //            _filnam = "HickChat" + DateTime.Now.Year + String.Format("{0}", DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString()) + DateTime.Now.Day + ".txt";
            //            sb.AppendLine("select *,peeruser=(select top 1 Username from Hick_Users where ID=l.peerid) from Hick_Conversation_log as l");
            //            sb.AppendLine("left outer join Hick_Clear_Log as cl on l.Conversation_Id=cl.Conversation_Id and cl.UserId=" + currentuserid + "");
            //            sb.AppendLine("where l.Conversation_Id=" + conversationid + "");
            //            sb.AppendLine("and CONVERT(TIME, l.Conversation_date,108)>= CASE WHEN  cl.Cleared_Date IS NULL THEN");
            //            sb.AppendLine("CONVERT(TIME, l.Conversation_date,108) ELSE CONVERT(Time,cl.Cleared_Date,108) END");
            //            sb.AppendLine("and (l.Message_Type=" + (int)MessageTypes.Text + " OR l.Message_Type=" + (int)MessageTypes.File + ")");
            //            sb.AppendLine("order by l.Id asc");
            //        }
            //        else
            //        {
            //            /* check for video chat */
            //            if (!String.IsNullOrEmpty(msgtyp) && msgtyp.Equals("2"))
            //            {
            //                _filnam = "HickVideoChatLog" + DateTime.Now.Year + String.Format("{0}", DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString()) + DateTime.Now.Day + ".txt";
            //                sb.AppendLine("select *,peeruser=(select top 1 Username from Hick_Users where ID=l.peerid) from Hick_VideoConversation_Log as l");
            //                sb.AppendLine("where l.ConversationId=" + conversationid + "");
            //                sb.AppendLine("and (l.MessageType=" + (int)MessageTypes.Video + ")");
            //                sb.AppendLine("order by l.ConversationDate asc");
            //            }
            //            else
            //            {
            //                _filnam = "HickChatLog" + DateTime.Now.Year + String.Format("{0}", DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString()) + DateTime.Now.Day + ".txt";
            //                sb.AppendLine("select *,peeruser=(select top 1 Username from Hick_Users where ID=l.peerid) from Hick_Conversation_log as l");
            //                sb.AppendLine("where l.Conversation_Id=" + conversationid + "");
            //                sb.AppendLine("and (l.Message_Type=" + (int)MessageTypes.Text + " OR l.Message_Type=" + (int)MessageTypes.File + ")");
            //                sb.AppendLine("order by l.Id asc");
            //            }
            //        }

            //        if (!String.IsNullOrEmpty(msgtyp) && msgtyp.Equals("2"))
            //        {
            //            List<VideoChatLog> objvideologcoll = new List<VideoChatLog>();
            //            List<VideoChatLog> objvideologcollnew = new List<VideoChatLog>();
            //            /* fetching video log history*/
            //            using (SqlCommand command = new SqlCommand(sb.ToString(), conn))
            //            {
            //                using (SqlDataReader reader = command.ExecuteReader())
            //                {
            //                    if (reader.HasRows)
            //                    {
            //                        while (reader.Read())
            //                        {
            //                            VideoChatLog objvideolog = new VideoChatLog();
            //                            objvideolog.ConversationId = Convert.ToInt32(reader["ConversationId"]);
            //                            objvideolog.VideoId = Convert.ToString(reader["VideoId"]);
            //                            objvideolog.ParentVideoId = Convert.ToString(reader["ParentVideoId"]);
            //                            objvideolog.ConversationDate = Convert.ToString(reader["ConversationDate"]);
            //                            objvideolog.PeerName = Convert.ToString(reader["peeruser"]);
            //                            objvideolog.MessageType = Convert.ToInt32(reader["MessageType"]);
            //                            objvideolog.Status = Convert.ToInt32(reader["Status"]);
            //                            objvideolog.PeerID = Convert.ToInt32(reader["PeerId"]);
            //                            DateTime dt = Convert.ToDateTime(reader["ConversationDate"]);
            //                            objvideolog.Time = dt.ToString("hh:mm tt");

            //                            objvideologcoll.Add(objvideolog);
            //                        }
            //                    }
            //                }
            //            }
            //            if (objvideologcoll.Count > 0)
            //            {
            //                for (int i = 0; i < objvideologcoll.Count; i++)
            //                {
            //                    var isexist = objvideologcollnew.Where(m => m.ParentVideoId == objvideologcoll[i].VideoId).FirstOrDefault();
            //                    if (isexist == null)
            //                    {
            //                        if (String.IsNullOrEmpty(objvideologcoll[i].ParentVideoId))
            //                        {
            //                            objvideologcoll[i].Duration = "00:00:00";

            //                        }
            //                        else
            //                        {
            //                            DateTime dt1 = Convert.ToDateTime(objvideologcoll[i].ConversationDate);
            //                            var objprnt = objvideologcoll.Where(m => m.VideoId == objvideologcoll[i].ParentVideoId).FirstOrDefault();
            //                            if (objprnt != null)
            //                            {
            //                                DateTime dt2 = Convert.ToDateTime(objprnt.ConversationDate);
            //                                TimeSpan duration = new TimeSpan(dt2.Ticks - dt1.Ticks);
            //                                objvideologcoll[i].Duration = Convert.ToString(duration).Replace("-", "");
            //                            }
            //                        }
            //                        objvideologcollnew.Add(objvideologcoll[i]);
            //                    }

            //                }
            //            }
            //            if (objvideologcollnew.Count > 0)
            //            {
            //                for (int i = 0; i < objvideologcollnew.Count; i++)
            //                {
            //                    objlog = new ExportChatLog();
            //                    objlog.Conversation = objvideologcollnew[i].Duration;
            //                    objlog.Name = objvideologcollnew[i].PeerName;
            //                    objlog.Time = objvideologcollnew[i].Time;
            //                    objlogColl.Add(objlog);
            //                }
            //            }
            //        }
            //        else
            //        {
            //            using (SqlCommand command = new SqlCommand(Convert.ToString(sb), conn))
            //            {
            //                using (SqlDataReader reader = command.ExecuteReader())
            //                {
            //                    while (reader.Read())
            //                    {
            //                        objlog = new ExportChatLog();

            //                        objlog.Conversation = Convert.ToString(reader["Conversation_log"]);
            //                        objlog.Name = Convert.ToString(reader["peeruser"]);
            //                        DateTime dt = Convert.ToDateTime(reader["Conversation_date"]);
            //                        objlog.Time = dt.ToString("hh:mm tt");
            //                        objlogColl.Add(objlog);
            //                    }


            //                }
            //            }
            //        }

            //        if (objlogColl.Count > 0)
            //        {
            //            JavaScriptSerializer jss = new JavaScriptSerializer();
            //            string output = jss.Serialize(objlogColl);
            //            byte[] buffer = System.Text.Encoding.ASCII.GetBytes(output);

            //            context.Response.Clear();
            //            context.Response.ContentType = "text/plain";
            //            context.Response.AddHeader("Content-Disposition", String.Format("attachment; filename=\"{0}\"", _filnam));
            //            context.Response.AddHeader("Content-Length", buffer.Length.ToString());
            //            context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            //            context.Response.Flush();
            //            HttpContext.Current.ApplicationInstance.CompleteRequest();

            //        }

            //    }

            //}
            //catch (Exception)
            //{

            //    throw;
            //}

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}