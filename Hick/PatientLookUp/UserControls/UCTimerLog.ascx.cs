using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using Hick.Models;
using IGNITE.DBUtility;
using System.Drawing;

namespace Hick.PatientLookUp.UserControls
{
    public partial class UCTimerLog : System.Web.UI.UserControl
    {
        string patientid = "";
        string userid = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = constr;
            conn.Open();

            hdnPatientId.Value = Convert.ToString(Session["patientid"]);
            hdnUserId.Value = Convert.ToString(Session["userid"]);

            SqlCommand command = new SqlCommand("sp_hick_timer_patientdetails", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@patient_id", hdnPatientId.Value);


            SqlDataReader sdr1 = command.ExecuteReader();
            while (sdr1.Read())
            {

                lblfname.Text = sdr1["FirstName"].ToString();
                lbllname.Text = sdr1["LastName"].ToString();

            }

            lblfname.ForeColor = Color.Red;
            lbllname.ForeColor = Color.Red;
            patientid = hdnPatientId.Value;
            SqlCommand command1 = new SqlCommand("select task_date from hick_tasks where patient_id=" + hdnPatientId.Value, conn);
            SqlDataReader sdr2 = command1.ExecuteReader();
            while (sdr2.Read())
            {
                lblmonth.Text = sdr2["task_date"].ToString();

            }

            ////lblmonth.Text = Convert.ToDateTime(lblmonth.Text).ToString("MM");
            string temp = Convert.ToDateTime(lblmonth.Text).ToString("MM-dd-yyyy");
            //var temp1=temp
            //  lblmonth.Text = temp;
            string tem = Convert.ToDateTime(lblmonth.Text).ToString("MMM");
            // string mon = temp.ToString("MMM");
            lblmonth.Text = tem.ToString();
            userid = hdnUserId.Value;
            conn.Close();
            lblmonth.ForeColor = Color.Red;
            BindTaskDetails();
        }

        
        public void BindTaskDetails()
        {
            TimeSpan totaltime = new TimeSpan();
            string totalduration = "";
            string[] totaltimesplit;

            Int32 currentuserid = Int32.Parse(userid);
            Int32 peerid = Int32.Parse(patientid);

            DataTable dt = new DataTable();
            
            dt.Columns.Add("Date");
            dt.Columns.Add("Category");
            dt.Columns.Add("StartTime");
            dt.Columns.Add("EndTime");
            dt.Columns.Add("TotalTime");
            dt.Columns.Add("TaskDetails");
            dt.Columns.Add("taskid");
            dt.Columns.Add("tasktype");

            string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                conn.Open();

                //StringBuilder cmd = new StringBuilder();
                //cmd.AppendLine("select CONVERT(varchar(10),ConversationDate,101) as ConversationDate,");
                //cmd.AppendLine("CONVERT(varchar(10),ConversationDate,108) as StartTime,");
                //cmd.AppendLine("CONVERT(varchar(10),ConversationEndTime,108) as EndTime,");
                //cmd.AppendLine("DateDiff(s,ConversationDate , ConversationEndTime) as [Log Seconds]");
                //cmd.AppendLine("into #temp");
                //cmd.AppendLine("from Hick_VideoConversation_Log where ConversationDate between DATEADD(day,(1-DAY('" + DateTime.Now + "')),datediff(DAY,0,'" + DateTime.Now + "')) and DATEADD (dd, -1, DATEADD(mm, DATEDIFF(mm, 0, '" + DateTime.Now + "') + 1, 0)) and PeerId='" + patientid + "'");
                //cmd.AppendLine("select ConversationDate,StartTime,EndTime,[Log Seconds],");
                //cmd.AppendLine("STR(([Log Seconds])/3600) + RIGHT(CONVERT(char(8),DATEADD(s,([Log Seconds]),0),108),7) as TotalDuration from #temp");
                //cmd.AppendLine("drop table #temp");

                using (SqlCommand command = new SqlCommand("sp_hick_TotalVideoCallDurationForGivenUsers", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CurrentUserId", currentuserid);
                    command.Parameters.AddWithValue("@PeerId", peerid);
                    command.Parameters.AddWithValue("@Date", DateTime.UtcNow);

                    using (SqlDataReader sdr = command.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            DataRow dr = dt.NewRow();
                            DateTime dateval = new DateTime();

                            dateval = DateTime.Parse(DBHelper.getString(sdr,"ConversationDate"));
                            dr[0] = dateval.ToString(Utility.GlobalDateMonthDayYearFormat);
                            dr[1] = "System";

                            if (!string.IsNullOrEmpty(DBHelper.getString(sdr,"StartTime")))
                            {
                                TimeSpan stime = TimeSpan.Parse(DBHelper.getString(sdr,"StartTime"));
                                DateTime st = DateTime.Today.Add(stime);
                                dr[2] = st.ToString("hh:mm tt");
                            }
                            else
                            {
                                dr[2] = "--";
                            }

                            if (!string.IsNullOrEmpty(DBHelper.getString(sdr,"EndTime")))
                            {
                                TimeSpan etime = TimeSpan.Parse(DBHelper.getString(sdr,"EndTime"));
                                DateTime et = DateTime.Today.Add(etime);
                                dr[3] = et.ToString("hh:mm tt");
                            }
                            else
                            {
                                dr[3] = "--";
                            }

                            string t = Convert.ToString(sdr["TotalDuration"]).Trim();

                            if (!string.IsNullOrEmpty(t))
                            {
                                TimeSpan ts = new TimeSpan();
                                Utility.CalculateTimeSpan(ref ts, t);
                                dr[4] = string.Format("{0}d {1}h {2}m {3}s", ts.Days ,ts.Hours ,ts.Minutes , ts.Seconds);
                                totaltime = totaltime.Add(ts);
                            }
                            else
                            {
                                dr[4] = "--";
                            }
                            dr[5] = "Video Call";
                            dr[6] = Convert.ToString(sdr["taskid"]);
                            dr[7] = Convert.ToString(sdr["tasktype"]);
                            if (!string.IsNullOrEmpty(t))
                            {
                                dt.Rows.Add(dr);
                            }
                            
                        }
                    }
                }

                //StringBuilder cmd1 = new StringBuilder();
                //cmd1.AppendLine("select CONVERT(varchar(10),ConversationDate,101) as ConversationDate,total_txtchat_dauration from Hick_user_Conversation");
                //cmd1.AppendLine("where ConversationDate between DATEADD(day,1-day('" + DateTime.Now + "'),DATEDIFF(day,0,'" + DateTime.Now + "')) and DATEADD (dd, -1, DATEADD(mm, DATEDIFF(mm, 0, '" + DateTime.Now + "') + 1, 0)) and Initiator='" + patientid + "'");

                using (SqlCommand command1 = new SqlCommand("sp_hick_TotalTextChatDurationForGivenUsers", conn))
                {
                    command1.CommandType = CommandType.StoredProcedure;
                    command1.Parameters.AddWithValue("@CurrentUserId", currentuserid);
                    command1.Parameters.AddWithValue("@PeerId", peerid);
                    command1.Parameters.AddWithValue("@Date", DateTime.Now);

                    using (SqlDataReader sdr1 = command1.ExecuteReader())
                    {
                        while (sdr1.Read())
                        {
                            DataRow dr = dt.NewRow();
                            DateTime dateval = new DateTime();
                            if (!string.IsNullOrEmpty(DBHelper.getString(sdr1,"ConversationDate")))
                            {
                                dateval = DateTime.Parse(DBHelper.getString(sdr1,"ConversationDate"));
                                dr[0] = dateval.ToString(Utility.GlobalDateMonthDayYearFormat);
                            }
                            else
                            {
                                dr[0] = "--";
                            }
                            dr[1] = "System";
                            if (!string.IsNullOrEmpty(DBHelper.getString(sdr1,"StartTime")))
                            {
                                //TimeSpan stime = TimeSpan.Parse(Convert.ToString(sdr1["StartTime"]));
                                //DateTime st = DateTime.Today.Add(stime);
                                dr[2] = string.Format("{0:hh:mm tt}", DBHelper.getDateTime(sdr1,"StartTime"));//st.ToString("hh:mm tt");
                            }
                            else
                            {
                                dr[2] = "--";
                            }

                            if (!string.IsNullOrEmpty(DBHelper.getString(sdr1,"EndTime")))
                            {
                                //TimeSpan etime = TimeSpan.Parse(Convert.ToString(sdr1["EndTime"]));
                                //DateTime et = DateTime.Today.Add(etime);
                                dr[3] = string.Format("{0:hh:mm tt}", DBHelper.getDateTime(sdr1,"EndTime"));//et.ToString("hh:mm tt");
                            }
                            else
                            {
                                dr[3] = "--";
                            }
                            //dr[4] = TimeSpan.Parse(Convert.ToString(sdr1["total_txtchat_dauration"]));
                            //string t = Convert.ToString(sdr1["total_txtchat_dauration"]).Trim();
                            //TimeSpan stime = TimeSpan.Parse(Convert.ToString(sdr1["StartTime"]));
                            string t = (Convert.ToDateTime(DBHelper.getDateTime(sdr1,"EndTime"))).Subtract(Convert.ToDateTime(DBHelper.getDateTime(sdr1,"StartTime"))).ToString();
                            //string t = ToLongString(totaltime);
                            

                            if (!string.IsNullOrEmpty(t))
                            {
                                TimeSpan ts = new TimeSpan();
                                Utility.CalculateTimeSpan(ref ts, t);
                                dr[4] = string.Format("{0}d {1}h {2}m {3}s", ts.Days, ts.Hours, ts.Minutes, ts.Seconds);
                                totaltime = totaltime.Add(ts);
                            }
                            else
                            {
                                dr[4] = "--";
                            }
                            dr[5] = "Chat";
                            dr[6] = DBHelper.getString(sdr1,"taskid");
                            dr[7] = DBHelper.getString(sdr1,"tasktype");
                            if (!string.IsNullOrEmpty(t))
                            {
                                dt.Rows.Add(dr);
                            }
                            
                        }
                    }
                }

                //StringBuilder cmd2 = new StringBuilder();
                //cmd2.AppendLine("select task,CONVERT(varchar(10),task_date,101) as conversationdate,category,start_time,end_time,total_time from hick_tasks");
                //cmd2.AppendLine("where task_date between DATEADD(day,1-day('" + DateTime.Now + "'),DATEDIFF(day,0,'" + DateTime.Now + "')) and DATEADD (dd, -1, DATEADD(mm, DATEDIFF(mm, 0, '" + DateTime.Now + "') + 1, 0)) and patient_id='" + patientid + "'");

                using (SqlCommand command2 = new SqlCommand("sp_hick_TotalTaskDurationForGivenUsers", conn))
                {
                    command2.CommandType = CommandType.StoredProcedure;
                    command2.Parameters.AddWithValue("@CurrentUserId", currentuserid);
                    command2.Parameters.AddWithValue("@PeerId", peerid);
                    command2.Parameters.AddWithValue("@Date", DateTime.UtcNow);

                    using (SqlDataReader sdr2 = command2.ExecuteReader())
                    {
                        while (sdr2.Read())
                        {
                            DataRow dr = dt.NewRow();
                            DateTime dateval = new DateTime();

                            if (!string.IsNullOrEmpty(Convert.ToString(sdr2["ConversationDate"])))
                            {
                                dateval = DateTime.Parse(Convert.ToString(sdr2["ConversationDate"]));
                                dr[0] = dateval.ToString(Utility.GlobalDateMonthDayYearFormat);
                            }
                            else
                            {
                                dr[0] = "--";
                            }

                            if (!string.IsNullOrEmpty(Convert.ToString(sdr2["category"])))
                            {
                                if (Convert.ToInt32(sdr2["category"]) == 1)
                                {
                                    dr[1] = "System";
                                }
                                else
                                {
                                    dr[1] = "Chart Review";
                                }
                            }
                            else
                            {
                                dr[1] = "--";
                            }

                            //string stime = Convert.ToString(sdr2["start_time"]);

                            if (!string.IsNullOrEmpty(Convert.ToString(sdr2["start_time"])))
                            {
                                //TimeSpan stime = TimeSpan.Parse(Convert.ToString(sdr2["start_time"]));
                                //DateTime st = DateTime.Today.Add(stime);
                                //dr[2] = st.ToString("hh:mm tt");
                                dr[2] = Convert.ToString(sdr2["start_time"]);
                            }
                            else
                            {
                                dr[2] = "--";
                            }

                            //dr[2] = Convert.ToString(sdr2["start_time"]);

                            if (!string.IsNullOrEmpty(Convert.ToString(sdr2["end_time"])))
                            {
                                //TimeSpan etime = TimeSpan.Parse(Convert.ToString(sdr2["end_time"]));
                                //DateTime et = DateTime.Today.Add(etime);
                                //dr[3] = et.ToString("hh:mm tt");

                                dr[3] = Convert.ToString(sdr2["end_time"]);
                            }
                            else
                            {
                                dr[3] = "";
                            }

                            //dr[3] = Convert.ToString(sdr2["end_time"]);

                            string t = Convert.ToString(sdr2["total_time"]).Trim();
                            if (!string.IsNullOrEmpty(t))
                            {
                                TimeSpan ts = new TimeSpan();
                                Utility.CalculateTimeSpan(ref ts, t);
                                dr[4] = string.Format("{0}d {1}h {2}m {3}s", ts.Days, ts.Hours, ts.Minutes, ts.Seconds);
                                totaltime = totaltime.Add(ts);
                            }
                            else
                            {
                                dr[4] = "--";
                            }
                            dr[5] = Convert.ToString(sdr2["task"]);
                            dr[6] = Convert.ToString(sdr2["taskid"]);
                            dr[7] = Convert.ToString(sdr2["tasktype"]);
                            if (!string.IsNullOrEmpty(t))
                            {
                                dt.Rows.Add(dr);
                            }
                        }
                    }
                    Session["exceldata"] = dt;
                }

                //StringBuilder cmd3 = new StringBuilder();
                //cmd3.AppendLine("select conversation_date,CONVERT(varchar(10),start_time,108) as start_time,CONVERT(varchar(10),end_time,108) as end_time,call_duration from hick_audiocall_log");
                //cmd3.AppendLine("where conversation_date between DATEADD(day,1-day('" + DateTime.Now + "'),DATEDIFF(day,0,'" + DateTime.Now + "')) and DATEADD (dd, -1, DATEADD(mm, DATEDIFF(mm, 0, '" + DateTime.Now + "') + 1, 0)) and initiator=" + patientid + "");

                using (SqlCommand command3 = new SqlCommand("sp_hick_TotalAudioCallDurationForGivenPatient", conn))
                {
                    command3.CommandType = CommandType.StoredProcedure;
                    command3.Parameters.AddWithValue("@PeerId", peerid);
                    command3.Parameters.AddWithValue("@Date", DateTime.UtcNow);

                    using(SqlDataReader sdr3=command3.ExecuteReader())
                    {
                        while(sdr3.Read())
                        {
                            DataRow dr = dt.NewRow();
                            DateTime dateval = new DateTime();

                            if (!string.IsNullOrEmpty(Convert.ToString(sdr3["conversation_date"])))
                            {
                                dateval = DateTime.Parse(Convert.ToString(sdr3["conversation_date"]));
                                dr[0] = dateval.ToString(Utility.GlobalDateMonthDayYearFormat);
                            }
                            else
                            {
                                dr[0] = "--";
                            }

                            dr[1] = "System";

                            if (!string.IsNullOrEmpty(Convert.ToString(sdr3["start_time"])))
                            {
                                TimeSpan stime = TimeSpan.Parse(Convert.ToString(sdr3["start_time"]));
                                DateTime st = DateTime.Today.Add(stime);
                                dr[2] = st.ToString("hh:mm tt");
                            }
                            else
                            {
                                dr[2] = "--";
                            }

                            if (!string.IsNullOrEmpty(Convert.ToString(sdr3["end_time"])))
                            {
                                TimeSpan etime = TimeSpan.Parse(Convert.ToString(sdr3["end_time"]));
                                DateTime et = DateTime.Today.Add(etime);
                                dr[3] = et.ToString("hh:mm tt");
                            }
                            else
                            {
                                dr[3] = "--";
                            }

                            string t = Convert.ToString(sdr3["call_duration"]).Trim();
                            if (!string.IsNullOrEmpty(t))
                            {
                                TimeSpan ts = new TimeSpan();
                                Utility.CalculateTimeSpan(ref ts, t);
                                dr[4] = string.Format("{0}d {1}h {2}m {3}s", ts.Days, ts.Hours, ts.Minutes, ts.Seconds);
                                totaltime = totaltime.Add(ts);
                            }
                            else
                            {
                                dr[4] = "--";
                            }

                            dr[5] = "Audio Call";
                            dr[6] = Convert.ToString(sdr3["taskid"]);
                            dr[7] = Convert.ToString(sdr3["tasktype"]);
                            if (!string.IsNullOrEmpty(t))
                            {
                                dt.Rows.Add(dr);
                            }
                        }
                    }
                }
            }
            
            DataView view = dt.DefaultView;
            view.Sort = "Date ASC";
            DataTable sortedDate = view.ToTable();

            //lbldays.Text = Convert.ToString(totaltime.Days);
            lblhours.Text = Convert.ToString( totaltime.Hours);
            lblmins.Text = Convert.ToString(totaltime.Minutes);
            lblsecs.Text = Convert.ToString(totaltime.Seconds);


            taskdetailsrepeater.DataSource = sortedDate;
            taskdetailsrepeater.DataBind();
        }

        


        protected void Taskdetailsrepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName=="edit")
            {
                string arg = e.CommandArgument.ToString();
                string[] values = arg.Split(':');
                if (values.GetUpperBound(0) >= 1)
                {
                    Response.Redirect("AddTask.aspx?TaskID=" + values[0] + "&TaskType=" + values[1]);
                }
            }
            else if(e.CommandName=="delete")
            {
                string arg = e.CommandArgument.ToString();
                string[] values = arg.Split(':');
                Session["TaskID"] = values[0];
                Session["TaskType"] = values[1];
                btnYes.Visible = true;
                btnNo.Visible = true;
                btnOk.Visible = false;
                lblprompt.Text = "Are you sure you want to delete this task?";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function",
                        "Showoverlay();", true);
            }
        }

        private void ExportToExcel(DataTable dt, string fileName, string worksheetName)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition",
                                                   "attachment;filename=" + fileName.Replace(" ", "_") + ".xls");
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            var stringWriter = new StringWriter();
            var htmlWrite = new HtmlTextWriter(stringWriter);
            var dataExportExcel = new DataGrid();
            dataExportExcel.DataSource = dt;
            dataExportExcel.DataBind();
            dataExportExcel.RenderControl(htmlWrite);
            var sbResponseString = new StringBuilder();
            sbResponseString.Append("<html xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" xmlns=\"https://www.w3.org/TR/REC-html40\"> <head><meta http-equiv=\"Content-Type\" content=\"text/html;charset=windows-1252\"><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>" + worksheetName + "</x:Name><x:WorksheetOptions><x:Panes></x:Panes></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head> <body>");
            sbResponseString.Append(stringWriter + "</body></html>");
            HttpContext.Current.Response.Write(sbResponseString.ToString());
            HttpContext.Current.Response.End();
        }

        protected void btnaddtask_click(object sender, EventArgs e)
        {
            Response.Redirect("AddTask.aspx");
        }

        protected void btnprinttask_click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function",
                        "Print();", true);
        }

        protected void btnexporttask_click(object sender, EventArgs e)
        {
            DataTable dts = ((DataTable)Session["exceldata"]).Copy();
            dts.Columns.Remove("taskid");
            dts.Columns.Remove("tasktype");

            //ExportToExcel(dts, Guid.NewGuid().ToString().Replace("-", "") , "Timer Log");
            ExportToExcel(dts, "Timer_Log" + " " + DateTime.Now.ToString(), "Timer Log");
        }
        protected void btnbillingtask_click(object sender, EventArgs e)
        {
            Response.Redirect("AddTask.aspx");
        }

        protected void btnYes_click(object sender, EventArgs e)
        {

            try
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function",
                "Hideoverlay();", true);
                string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = constr;
                    conn.Open();

                    using (SqlCommand command = new SqlCommand("sp_deletetasks", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TaskID", Session["TaskID"].ToString());
                        command.Parameters.AddWithValue("@TaskType", Session["TaskType"].ToString());

                        object obj = command.ExecuteScalar();
                    }
                }
                lblprompt.Text = "Delete Sucessful";
                btnYes.Visible = false;
                btnNo.Visible = false;
                btnOk.Visible = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function",
                "Showoverlay();", true);
                BindTaskDetails();
            }
            catch(Exception ex)
            {
                lblprompt.Text = "Error occured while deleting. Please contact your administrator";
                btnYes.Visible = false;
                btnNo.Visible = false;
                btnOk.Visible = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function",
                "Showoverlay();", true);
            }
        }

        protected void btnNo_click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function",
                        "Hideoverlay();", true);
        }
        protected void btnOk_click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function",
                        "Hideoverlay();", true);
        }
    }
}