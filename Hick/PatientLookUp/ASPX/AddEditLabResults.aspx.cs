using Hick.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.PatientLookUp.ASPX
{
    public partial class AddEditLabResults : System.Web.UI.Page
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
        public long ResultId
        {
            get
            {

                return Convert.ToInt64(ViewState["ResultId"]);
            }
            set
            {
                ViewState["ResultId"] = value;
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
                ResultId = !String.IsNullOrEmpty(Request.QueryString["cid"]) ? Convert.ToInt64(Request.QueryString["cid"]) : 0;

                if (PatientID != 0 && ResultId != 0)
                {
                    BindDetails();
                    buttontype.Value = "Update";
                }

            }


        }

        public void BindDetails()
        {
            Hick.Models.LabImaging obj = HttpContext.Current.Cache["EditLabResults"] as Hick.Models.LabImaging;
            if (obj != null)
            {
                date.Text = obj.Date;
                testtype.Text = obj.TestType;
                Results.Text = obj.Results;
                doctor.Text = obj.RequestingDoctor;
                admin.Text = obj.AdministeredBy;
            }

        }
        protected void saveResult(object sender, EventArgs e)
        {
            SaveDetails();
        }

        public void SaveDetails()
        {
            try
            {
                string action = "";
                if (buttontype.Value != "Update")
                {
                    action = "INSERT";
                }
                else
                {
                    action = "UPDATE";
                }
                var uri = Utility.GetServiceUrl("insertupdatelabimaging");

                IgJObject postData = new IgJObject();
                postData.Add("PatientID", PatientID);
                postData.Add("UserId", Convert.ToString(Session["PhysicianID"]));
                postData.Add("LabImagingId", ResultId);
                postData.Add("TestType", testtype.Text);
                postData.Add("Results", Results.Text);
                postData.Add("RequestingDoctor", doctor.Text);
                postData.Add("AdministeredBy", admin.Text);
                postData.Add("ActionItem", action);

                var res = Utility.PostRequestForSave(uri, postData.ToString(Formatting.None));
                ClientScript.RegisterStartupScript(GetType(), "scrpt", "parent.window.location.href=parent.window.location.href;", true);
            }
            catch (Exception exc)
            {

            }
        }
    }
}