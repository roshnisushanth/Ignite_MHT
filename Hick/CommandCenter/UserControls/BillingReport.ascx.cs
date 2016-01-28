using Hick.Models;
using IGNITE.DBUtility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.CommandCenter.UserControls
{
    public partial class BillingReport : System.Web.UI.UserControl
    {
        protected string startDate = Utility.FirstDayOfMonth(DateTime.Now).ToString(Utility.GlobalDateFormat);
        protected string endDate = Utility.LastDayOfMonth(DateTime.Now).ToString(Utility.GlobalDateFormat);
        protected string currentMonth = DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture);
        protected string currentYear = DateTime.Now.ToString("yyyy", CultureInfo.InvariantCulture);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<PatientDetails> patientList = new List<PatientDetails>();
                var users = GetLogs();
                if (users.Count > 0)
                {
                    patientList = GetPatientList(users.ToArray());
                }

                List<PatientDetails> tableList = new List<PatientDetails>();

                foreach (var patient in users)
                {
                    var result = patientList.Where(p => p.PatientId == Convert.ToInt64(patient)).FirstOrDefault<PatientDetails>();
                    if (result != null)
                    {
                        var s = string.Join(",", patientList.Where(p => p.PatientId == Convert.ToInt64(patient))
                                     .Select(p => p.ICDCode.ToString()));


                        result.ICDCode = s;
                        tableList.Add(result);
                    }
                }

                gdconditions.DataSource = tableList;
                gdconditions.DataBind();

            }
        }

        private List<PatientDetails> GetPatientList(string[] patids)
        {

            SqlParameter[] sqlParams = new SqlParameter[] {
                                                // comma separated list
                                                new SqlParameter("PatientId", string.Join(",", patids))
                                            };

            List<PatientDetails> iPatient = new List<PatientDetails>();
            using (SqlDataReader sqldrPatType = SqlHelper.ExecuteReader(Utility.DBBridgeConnectionString, CommandType.StoredProcedure, "sp_getPatientDetailsByPatientIdList", sqlParams))
            {
                while (sqldrPatType.Read())
                {
                    PatientDetails oPatientInfoModel = new PatientDetails();

                    oPatientInfoModel.PatientId = DBHelper.getInt(sqldrPatType, "PatientId");
                    oPatientInfoModel.PhysicianId = DBHelper.getString(sqldrPatType, "PhysicianId");

                    oPatientInfoModel.LastName = DBHelper.getString(sqldrPatType, "Lastname");
                    oPatientInfoModel.FirstName = DBHelper.getString(sqldrPatType, "FirstName");
                    oPatientInfoModel.EmailId = DBHelper.getString(sqldrPatType, "EmailId");
                    oPatientInfoModel.DOB = DBHelper.getDateTime(sqldrPatType, "DateOfBirth").ToString(Utility.GlobalDateFormat);

                    oPatientInfoModel.Gender = DBHelper.getString(sqldrPatType, "Gender");
                    oPatientInfoModel.PhoneNumber = DBHelper.getString(sqldrPatType, "PhoneNumber");
                    oPatientInfoModel.Last4SSN = DBHelper.getString(sqldrPatType, "Last4SSN");
                    oPatientInfoModel.Address1 = DBHelper.getString(sqldrPatType, "Addres");
                    oPatientInfoModel.HICN = DBHelper.getString(sqldrPatType, "HICN");
                    oPatientInfoModel.ICDCode = DBHelper.getString(sqldrPatType, "icdcode");
                    iPatient.Add(oPatientInfoModel);
                }
            }
            return iPatient;



        }

        private List<string> GetLogs()
        {
            string constr = Utility.DBConnectionString;
            List<string> billableUsers = new List<string>();
            if (Session["userid"] != null)
            {
                using (SqlConnection conn = new SqlConnection())
                {

                    conn.ConnectionString = constr;
                    conn.Open();
          
                   
                    using (SqlCommand command = new SqlCommand("sp_HickPatientList", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CurrentUserId", Convert.ToInt64(Session["userid"]));
                        command.Parameters.AddWithValue("@CurrentDate", DateTime.UtcNow);
                        command.Parameters.AddWithValue("@Flag", 1);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            GetBillableUsers(billableUsers, reader);
                        }
                    }
                }

            }
            return billableUsers;
        }

        private static void GetBillableUsers(List<string> billableUsers, SqlDataReader reader)
        {
            while (reader.Read())
            {
                TimeSpan totalDuration = new TimeSpan();
                //var duration = DBHelper.getInt(reader, "TotalDuration");
                Utility.CalculateTimeSpan(ref totalDuration, Convert.ToString(reader["TotalDuration"]));
                if (totalDuration.TotalSeconds >= Utility.CutOffSeconds)
                {
                    billableUsers.Add(Convert.ToString( DBHelper.getInt64(reader, "ReferenceId")));
                }
            }
        }

        public DateTime FirstDayOfMonth(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }


        public DateTime LastDayOfMonth(DateTime dateTime)
        {
            return FirstDayOfMonth(dateTime).AddMonths(1).AddDays(-1);
        }

        protected void btnexportpatientlist_Click(object sender, EventArgs e)
        {
            ExportGridToExcel();
        }

        private void ExportGridToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Billing_Report" + DateTime.Now.ToString(Utility.GlobalDateFormat) + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            gdconditions.GridLines = GridLines.Both;
            gdconditions.HeaderStyle.Font.Bold = true;
            gdconditions.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }
    }
}