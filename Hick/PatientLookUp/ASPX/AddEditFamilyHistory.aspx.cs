using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Hick.Models;
using System.Configuration;
using System.Web.Script.Services;
using Hick;
using IGNITE_MODEL;
using Newtonsoft.Json;

namespace Hick.PatientLookUp.ASPX
{
    public partial class AddEditFamilyHistory : System.Web.UI.Page
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
        public long PatientFamilyHistoryID
        {
            get
            {

                return Convert.ToInt64(ViewState["PatientFamilyHistoryID"]);
            }
            set
            {
                ViewState["PatientFamilyHistoryID"] = value;
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
                PatientFamilyHistoryID = !String.IsNullOrEmpty(Request.QueryString["cid"]) ? Convert.ToInt64(Request.QueryString["cid"]) : 0;

                if (PatientID != 0 && PatientFamilyHistoryID != 0)
                {
                    BindDetails();
                }

            }
        }

        protected void btnSaveCondition_Click(object sender, EventArgs e)
        {
            try
            {
                var uri = Utility.GetServiceUrl("updatefamilyhistory");

                IgJObject postData = new IgJObject();
                postData.Add("PatientID", PatientID);
                postData.Add("ConditionName", ddlConditions.SelectedItem.Text);
                postData.Add("Relationship", ddlRelationShip.SelectedItem.Text);
                postData.Add("OnsetDate", txtOnsetDate.Text);
  
                var res = Utility.PostRequestForSave(uri, postData.ToString(Formatting.None));
                ClientScript.RegisterStartupScript(GetType(), "scrpt", "parent.window.location.href=parent.window.location.href;", true);

            }
            catch (Exception exc)
            {

            }
        }

        public void BindDetails()
        {
            Hick.Models.FamilyHistoryDetailInfo obj = HttpContext.Current.Cache["EditFamilyHistory"] as Hick.Models.FamilyHistoryDetailInfo;
            if (obj != null)
            {
                ddlRelationShip.Items.FindByText(obj.Relationship).Selected = true;
                ddlConditions.Items.FindByText(obj.ConditionName).Selected = true;
                txtOnsetDate.Text = obj.OnsetDate;
               
            }

        }
    }
}