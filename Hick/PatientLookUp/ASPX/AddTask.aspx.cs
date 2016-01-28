using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Hick.PatientLookUp.ASPX
{
    public partial class AddTask : Hick.Base.BasePage
    {
        static string patientid = "";
        static string userid = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                hdnPatientId.Value = Request.QueryString["PatientId"];
                hdnUserId.Value = Request.QueryString["UserId"];

                patientid = hdnPatientId.Value;
                userid = hdnUserId.Value;
                if (!string.IsNullOrEmpty(patientid) && !string.IsNullOrEmpty(userid))
                {
                    Session["patientid"] = patientid;
                    Session["userid"] = userid;
                }
            }
        }
        [WebMethod(EnableSession = true)]
        public static void GetTaskDetails(string tasktype, string taskid)
        {
            string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                conn.Open();

                using (SqlCommand command = new SqlCommand("sp_GetTask", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TaskType", tasktype);
                    command.Parameters.AddWithValue("@TaskID", taskid);

                    using (SqlDataReader sdr = command.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                        }
                    }
                }
            }
        }
        [WebMethod(EnableSession = true)]
        public static void AddTaskDetails(string taskid, string tasktype, string task, string taskdate, string category, string starttime, string endtime, string totaltime)
        {
            if (taskdate.Length > 0)
            {
                string patientID = Convert.ToString(HttpContext.Current.Session["patientid"]);
                string userID = Convert.ToString(HttpContext.Current.Session["userid"]);

                string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    conn.ConnectionString = constr;
                    conn.Open();
                    //var cmd = "Insert into hick_tasks (patient_id,task,task_date,category,start_time,end_time,total_time,created_by,created_date) values (@patientid,@task,@taskdate,@category,@starttime,@endtime,@totaltime,@createdby,@createddate)";
                    using (SqlCommand command = new SqlCommand("sp_hick_InsertHickTasks", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        if (taskid.ToLower() == "undefined" || tasktype.ToLower() == "undefined")
                        {
                            taskid = "";
                            tasktype = "";
                        }
                        command.Parameters.AddWithValue("@TaskID", taskid);
                        command.Parameters.AddWithValue("@TaskType", tasktype);
                        command.Parameters.AddWithValue("@patientid", Convert.ToInt32(patientID));
                        command.Parameters.AddWithValue("@task", task);
                        command.Parameters.AddWithValue("@taskdate", DateTime.Parse(taskdate));
                        command.Parameters.AddWithValue("@category", Convert.ToInt32(category));
                        command.Parameters.AddWithValue("@starttime", starttime);
                        command.Parameters.AddWithValue("@endtime", endtime);
                        command.Parameters.AddWithValue("@totaltime", totaltime);
                        command.Parameters.AddWithValue("@createdby", Convert.ToInt32(userID));
                        command.Parameters.AddWithValue("@createddate", DateTime.UtcNow);
                        command.ExecuteScalar();
                    }
                }
            }
        }
    }
}