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

namespace Hick.PatientLookUp.UserControls
{
    public partial class UCAddTask : System.Web.UI.UserControl
    {
        string strtaskid = "";
        string strtasktype = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //hdnPatientId.Value = Request.QueryString["PatientId"];
            //hdnUserId.Value = Request.QueryString["UserId"];
            
            if (Request.QueryString["taskid"] != null && Request.QueryString["tasktype"] != null)
            {
                strtaskid = Request.QueryString["taskid"].ToString();
                strtasktype = Request.QueryString["tasktype"].ToString();
                GetTaskDetails(strtaskid, strtasktype);
                taskid.Value = strtaskid;
                tasktype.Value = strtasktype;
                this.Page.Title = "Edit Tasks";
                lblheader.Text = "Edit Task";
                
            }
            //taskid.Visible = false;
            //tasktype.Visible = false;
        }
        public void GetTaskDetails(string staskid, string stasktype)
        {
            string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                conn.Open();

                using (SqlCommand command = new SqlCommand("sp_GetTask", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TaskType", stasktype);
                    command.Parameters.AddWithValue("@TaskID", staskid);

                    using (SqlDataReader sdr = command.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            taskdate.Value = DateTime.Parse(Convert.ToString(sdr["ConversationDate"])).ToString(Models.Utility.GlobalDateMonthDayYearFormat);
                            dropdowncategory.Value = sdr["category"].ToString();
                            string[] strstarttime = sdr["start_time"].ToString().Split(' ');
                            string[] strendtime = sdr["end_time"].ToString().Split(' ');
                            starttime.Value = strstarttime[0];
                            endtime.Value = strendtime[0];
                            string[] strtotaltime = sdr["total_time"].ToString().Split(':');
                            hours.Value = strtotaltime[0];
                            mins.Value = strtotaltime[1];
                            task.Value = sdr["task"].ToString();
                            taskid.Value = staskid;
                            tasktype.Value = stasktype;
                        }
                    }
                }
            }
        }

    }
}