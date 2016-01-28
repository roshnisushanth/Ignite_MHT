using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Hick.PatientLookUp.UserControls;
using System.Configuration;
using Hick.Models;
using Newtonsoft.Json;

namespace Hick.PatientLookUp.ASPX
{
    public partial class Allergies : Hick.Base.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static void CacheDetails(string Reaction, string Allergy, string activesince, string IsMedicationAllergy, string Treatment)
        {


            Hick.Models.Allergy obj = new Hick.Models.Allergy();
            obj.AllergyType = Allergy;
            obj.DateLastOccured = activesince;
            obj.Reaction = Reaction;
            obj.IsMedicationAllergy = IsMedicationAllergy;
            obj.Treatment = Treatment;

            HttpContext.Current.Cache["EditAllergies"] = obj;
        }

        [WebMethod]
        public static void DeleteAllergies(long AllergyID)
        {
            try {
                string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                var uri = Utility.GetServiceUrl("deleteallergies");

                IgJObject postData = new IgJObject();
                postData.Add("AllergyID", AllergyID);
                postData.Add("Pin", PinValue);

                var res = Utility.PostRequestForSave(uri, postData.ToString(Formatting.None));
            }
            catch(Exception)
            {

            }

            //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/deleteallergies"; 
            //var postData = "{\"AllergyID\":\"" + AllergyID + "\",\"Pin\":\"" + PinValue + "\"}";
            //var res = Utility.PostRequestForSave(uri, postData);
            //// ClientScript.RegisterStartupScript(GetType(), "scrpt", "parent.window.location.href=parent.window.location.href;", true);

            // exists in the user control deleteallergies
        }
    }
}