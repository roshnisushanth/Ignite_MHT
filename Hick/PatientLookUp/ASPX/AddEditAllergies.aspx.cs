using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hick.Models;
using System.Configuration;
using Newtonsoft.Json;

namespace Hick.PatientLookUp.ASPX
{
    public partial class AddEditAllergies : Hick.Base.BasePage
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
        public long AllergyID
        {
            get
            {

                return Convert.ToInt64(ViewState["AllergyID"]);
            }
            set
            {
                ViewState["AllergyID"] = value;
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
                AllergyID = !String.IsNullOrEmpty(Request.QueryString["cid"]) ? Convert.ToInt64(Request.QueryString["cid"]) : 0;

                if (PatientID != 0 && AllergyID != 0)
                {
                    BindDetails();
                }

            }
        }
        public void BindDetails()
        {
            Hick.Models.Allergy obj = HttpContext.Current.Cache["EditAllergies"] as Hick.Models.Allergy;
            if (obj != null)
            {
                allergy.Text = obj.AllergyType;
                dob.Text = obj.DateLastOccured;
                reaction.Text = obj.Reaction;
               
            }

        }
        protected void saveAllergy(object sender, EventArgs e)
        {
            SaveDetails();
        }
        public void SaveDetails()
        {
            try
            {
                var uri = Utility.GetServiceUrl("updateallergies");

                IgJObject postData = new IgJObject();
                postData.Add("PatientID", PatientID);
                postData.Add("Reaction", reaction.Text);
                postData.Add("AllergyType", allergy.Text);
                postData.Add("AllergyID", AllergyID);
                postData.Add("DateLastOccured", dob.Text);
                postData.Add("Treatment", string.Empty);
                postData.Add("IsMedicationAllergy", string.Empty);

                var res = Utility.PostRequestForSave(uri, postData.ToString(Formatting.None));
                ClientScript.RegisterStartupScript(GetType(), "scrpt", "parent.window.location.href=parent.window.location.href;", true);

            }
            catch (Exception exc)
            {

            }

        }
    }
}