using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using System.IO;
using Hick.Models;
using System.Web.Script.Serialization;
using System.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data.SqlClient;
using IGNITE.DBUtility;
using System.Data;

namespace Hick.PatientLookUp.UserControls
{
    public partial class UCPatientSearch : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btn_lookup_Click(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;
            //Session["PatientID"] = null;
            FetchPatientDetails();

        }
        protected void btn_createpatient_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PatientLookUp/ASPX/CreatePatient.aspx", false);
        }
        public void FetchPatientDetails()
        {
            List<PatientInfo> objColl = null;
            try
            {

                SqlParameter[] sqlParams = new SqlParameter[] { };
                List<long> provIds = new List<long>();
                string sqlQuery = "";
                if(Session["UserType"].ToString()== "admin")
                {
                    sqlQuery = "select distinct providerid from [Ignite_PhysicianProviderMapping]";
                }
                else
                {
                    sqlQuery = "select providerid from [Ignite_PhysicianProviderMapping] where physicianid={0}";
                }
                using (SqlDataReader sqlobj = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, string.Format(sqlQuery, Session["userid"])))
                {
                    while (sqlobj.Read())
                    {
                        provIds.Add(DBHelper.getlong(sqlobj, "providerid"));
                    }
                }

                var pr = string.Join(",", provIds);

                string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/patientdetails";
                var uri = Utility.GetServiceUrl("patientdetails");
              //  var postData = "{\"LastName\":\"" + lastname.Text + "\",\"FirstName\":\"" + firstname.Text + "\",\"DOB\":\"" + dob.Text + "\",\"Pin\":\"" + PinValue + "\",\"PhysicianId\":\"" + Session["PhysicianID"] + "\"}";
                JObject jo = new JObject();
                jo.Add("LastName", lastname.Text);
                jo.Add("FirstName", firstname.Text);
                jo.Add("DOB", dob.Text);
                jo.Add("Pin", PinValue);
                jo.Add("PhysicianId", pr);


                objColl = Utility.PostRequest<PatientInfo>(uri, jo.ToString(Formatting.None));

                int datacount = objColl.Count;
                Cache["PatientData"] = objColl;

              

                if (datacount == 1)
                {
                    var pat = objColl[0];
                    if (string.IsNullOrWhiteSpace(pat.FirstName))
                    {
                        LoadNoRecordsFound();
                    }
                    else
                    {
                        div_patlist.Style.Add("display", "block");
                        //Response.Redirect("~/PatientLookUp/ASPX/PatientList.aspx",false);
                        var data = objColl;

                        patientList.DataSource = data;
                        patientList.DataBind();
                        // Response.Redirect("~/PatientLookUp/ASPX/PhpSummary.aspx?ptid=" + objColl[0].PatientId + "", false);
                    }
                }
                else if (datacount > 1)
                {
                    div_patlist.Style.Add("display", "block");
                    //Response.Redirect("~/PatientLookUp/ASPX/PatientList.aspx",false);
                    var data = Cache["PatientData"];

                    patientList.DataSource = data;
                    patientList.DataBind();
                }
                else
                {
                    LoadNoRecordsFound();
                }

            }
            catch (Exception exc)
            {
                lblmessage.Text = exc.Message;
            }

        }

        private void LoadNoRecordsFound()
        {
            lblmessage.Text = "No records found.";
            patientList.DataSource = null;
            patientList.DataBind();
        }
    }
}