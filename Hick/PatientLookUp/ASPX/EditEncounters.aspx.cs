using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Hick.Models;
using Newtonsoft.Json;

namespace Hick.PatientLookUp.ASPX
{
    public partial class EditEncounters : System.Web.UI.Page
    {
        public long PatientID
        {
            get
            {

                return Convert.ToInt64(ViewState["PatientID"]);
            }
            set
            {
                ViewState["PatientID"] = value;
            }
        }
        public long EncounterID
        {
            get
            {

                return Convert.ToInt64(ViewState["EncounterID"]);
            }
            set
            {
                ViewState["EncounterID"] = value;
            }
        }
        public string PIN
        {
            get
            {

                return ViewState["PIN"].ToString();
            }
            set
            {
                ViewState["PIN"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PatientID = Session["PatientID"] != null ? Convert.ToInt64(Session["PatientID"]) : 0;
                PIN = ConfigurationManager.AppSettings["Bridge_Base"];
                EncounterID = !String.IsNullOrEmpty(Request.QueryString["cid"]) ? Convert.ToInt64(Request.QueryString["cid"]) : 0;

                if (PatientID != 0 && EncounterID != 0)
                {
                    BindDetails();
                }

            }
        }
        public void BindDetails()
        {
            Hick.Models.HealthLogInfo obj = HttpContext.Current.Cache["EditEncounters"] as Hick.Models.HealthLogInfo;
            if (obj != null)
            {
               txt_date.Text = obj.Visitdate.ToShortDateString();
               txt_doctor.Text = obj.DoctorName;
               txt_Reason.Text = obj.VisitReason;
               txt_Diagnoses.Text = obj.VisitDiagnosis;
            }

        }
        protected void btnSaveCondition_Click(object sender, EventArgs e)
        {
            SaveDetails();
        }

        public void SaveDetails()
        {
            try
            {
                var uri = Utility.GetServiceUrl("updateencounter");

                IgJObject postData = new IgJObject();
                postData.Add("PatientID", PatientID);
                postData.Add("Pin", PIN);
                postData.Add("DoctorName", txt_doctor.Text);
                postData.Add("VisitReason", txt_Reason.Text);
                postData.Add("DoctorVisitId", EncounterID);
                postData.Add("Visitdate", txt_date.Text);
                postData.Add("UserId", Session["PhysicianID"].ToString());
                postData.Add("VisitDiagnosis", txt_Diagnoses.Text);

                var res = Utility.PostRequestForSave(uri, postData.ToString(Formatting.None));

                //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/updateencounter";
                //var postData = "{\"PatientID\":\"" + PatientID + "\",\"Pin\":\"" + PIN + "\",\"DoctorName\":\"" + txt_doctor.Text + "\",
                //    \"VisitReason\":\"" + txt_Reason.Text + "\",\"DoctorVisitId\":\"" + EncounterID + "\",\"Visitdate\":\"" + txt_date.Text + "\",\"UserId\":\"" + Session["PhysicianID"] + "\",
                //    \"VisitDiagnosis\":\"" +txt_Diagnoses.Text + "\"}";
                //var res = Utility.PostRequestForSave(uri, postData);
                ClientScript.RegisterStartupScript(GetType(), "scrpt", "parent.window.location.href=parent.window.location.href;", true);

            }
            catch (Exception exc)
            {

            }

        }
    }
}