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
    public partial class EditImmunizations : System.Web.UI.Page
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
        public long ImmunizationID
        {
            get
            {

                return Convert.ToInt64(ViewState["ImmunizationID"]);
            }
            set
            {
                ViewState["ImmunizationID"] = value;
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
                ImmunizationID = !String.IsNullOrEmpty(Request.QueryString["cid"]) ? Convert.ToInt64(Request.QueryString["cid"]) : 0;

                if (PatientID != 0 && ImmunizationID != 0)
                {
                    BindDetails();
                }

            }
        }

        public void BindDetails()
        {
            Hick.Models.Immunizations obj = HttpContext.Current.Cache["EditImmunizations"] as Hick.Models.Immunizations;
            if (obj != null)
            {
                txt_date.Text = obj.AdministrationDate.ToShortDateString();
                txt_Immunization.Text = obj.ImmunizationType;
             
            }

        }

        protected void btnSaveCondition_Click(object sender, EventArgs e)
        {
            try
            {
                var uri = Utility.GetServiceUrl("updateimmunizations");

                IgJObject postData = new IgJObject();
                postData.Add("PatientID", PatientID);
                postData.Add("Pin", PIN);
                postData.Add("ImmunizationType", txt_Immunization.Text);
                postData.Add("AdministrationDate", txt_date.Text);
                postData.Add("ImmunizationID", ImmunizationID);
                
                var res = Utility.PostRequestForSave(uri, postData.ToString(Formatting.None));

                //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/updateimmunizations";
                //var postData = "{\"PatientID\":\"" + PatientID + "\",\"Pin\":\"" + PIN + "\",\"ImmunizationType\":\"" + txt_Immunization.Text + "\",
                //    \"AdministrationDate\":\"" + txt_date.Text + "\",\"ImmunizationID\":\"" + ImmunizationID + "\"}";
                //var res = Utility.PostRequestForSave(uri, postData);
                ClientScript.RegisterStartupScript(GetType(), "scrpt", "parent.window.location.href=parent.window.location.href;", true);

            }
            catch (Exception exc)
            {

            }
        }
    }
}