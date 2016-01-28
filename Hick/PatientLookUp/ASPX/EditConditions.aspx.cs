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
using Hick.Encounters;
using IGNITE_MODEL.Encounters;
using Newtonsoft.Json;

namespace Hick.PatientLookUp.ASPX
{
    public partial class EditConditions : Hick.Base.BasePage
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
        public long ConditionID
        {
            get
            {

                return Convert.ToInt64(ViewState["ConditionID"]);
            }
            set
            {
                ViewState["ConditionID"] = value;
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
                ConditionID = !String.IsNullOrEmpty(Request.QueryString["cid"]) ? Convert.ToInt64(Request.QueryString["cid"]) : 0;

                if (PatientID != 0 && ConditionID != 0)
                {
                    BindDetails();
                }

            }

        }
        protected void btnSaveCondition_Click(object sender, EventArgs e)
        {
            SaveDetails();
        }
        public void BindDetails()
        {
            Hick.Models.Conditons obj = HttpContext.Current.Cache["EditConditions"] as Hick.Models.Conditons;
            if (obj != null)
            {
                CodeConditions.Text = obj.ICDCode;
                dob.Text = obj.DateOfOnset;
                inactivescience.Text = obj.History;
                hdndesc.Value = obj.Condition;
            }

        }
        public void SaveDetails()
        {
            try
            {
                var uri = Utility.GetServiceUrl("updateconditions");

                IgJObject postData = new IgJObject();
                postData.Add("PatientID", PatientID);
                postData.Add("Pin", PIN);
                postData.Add("Condition", hdndesc.Value);
                postData.Add("ConditionCheck", radioconditions.SelectedValue);
                postData.Add("ConditionID", ConditionID);
                postData.Add("DateOfOnset", dob.Text);
                postData.Add("History", inactivescience.Text);
                postData.Add("ICDCode", CodeConditions.Text);

                var res = Utility.PostRequestForSave(uri, postData.ToString(Formatting.None));

                //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/updateconditions";
                //var postData = "{\"PatientID\":\"" + PatientID + "\",\"Pin\":\"" + PIN + "\",\"Condition\":\"" + hdndesc.Value + "\",\"ConditionCheck\":\"" + radioconditions.SelectedValue + "\",
                //    \"ConditionID\":\"" + ConditionID + "\",\"DateOfOnset\":\"" + dob.Text + "\",\"History\":\"" + inactivescience.Text + "\",\"ICDCode\":\"" + CodeConditions.Text + "\"}";
                //var res = Utility.PostRequestForSave(uri, postData);
                ClientScript.RegisterStartupScript(GetType(), "scrpt", "parent.window.location.href=parent.window.location.href;", true);

            }
            catch (Exception exc)
            {

            }

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<MasterICD9Codes> GetMasterICD9CodesAutoComplete(string pre)
        {
            EncounterService service = new EncounterService();
            return service.GetMasterICD9CodesAutoComplete(pre);
        }

        [WebMethod]
        public static List<string> GetAutoCompleteData(string ICDCode)
        {
            List<ICDCodes> objColl = null;
            try
            {
                List<string> objcolICDFiltered = new List<string>();
                List<string> objcol = new List<string>();
                //ReferralPhysicianinfo objbll = new ReferralPhysicianinfo();
                //objcol = objbll.FetchICDCodes(ICDCode);
                string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                //  var uri = ConfigurationManager.AppSettings["serviceURL"] + "/fetchicdcodes";
                var uri = "http://localhost/BridgeMessanger/BridgeMessanger.svc/fetchicdcodes";
                var postData = "{\"ICD\":\"" + ICDCode + "\",\"Pin\":\"" + PinValue + "\"}";
                objColl = Utility.PostRequest<ICDCodes>(uri, postData);


                foreach (ICDCodes s in objColl)
                {
                    string result;
                    result = s.ICD;
                    if (result != null)
                    {
                        //if (result[0].ToUpper().StartsWith(ICDCode.ToUpper()) || result[1].Trim().ToUpper().StartsWith(ICDCode.ToUpper()))
                        //{
                        objcolICDFiltered.Add(result);
                        //}
                    }
                }
                return objcolICDFiltered;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}