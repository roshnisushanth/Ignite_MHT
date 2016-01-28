using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Configuration;
using Hick.Models;
using Newtonsoft.Json;

namespace Hick.PatientLookUp.ASPX
{
    public partial class Medications : Hick.Base.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static void CacheDetails(string description, string dosage, string activesince, string dosageunits)
        {


            Hick.Models.Medications obj = new Hick.Models.Medications();
            obj.Description = description;
            obj.Dosage = dosage;
            obj.Date = activesince;
            obj.DosageUnits = dosageunits;

            HttpContext.Current.Cache["EditMedications"] = obj;
        }

        [WebMethod]
        public static void DeleteMedications(long MedicationID)
        {
            try {
                string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                var uri = Utility.GetServiceUrl("deletemedications");

                IgJObject postData = new IgJObject();
                postData.Add("MedicationID", MedicationID);
                postData.Add("Pin", PinValue);

                var res = Utility.PostRequestForSave(uri, postData.ToString(Formatting.None));
            }
            catch(Exception)
            {

            }

            //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/deletemedications";
            //var postData = "{\"MedicationID\":\"" + MedicationID + "\",\"Pin\":\"" + PinValue + "\"}";
            //var res = Utility.PostRequestForSave(uri, postData);
            // ClientScript.RegisterStartupScript(GetType(), "scrpt", "parent.window.location.href=parent.window.location.href;", true);

            // exists in the user control deleteallergies
        }
    }
}