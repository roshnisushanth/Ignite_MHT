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
    public partial class Encounters : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static void CacheDetails(string VisitDate, string DoctorName, string VisitReason, string VisitDiagnosis)
        {


            Hick.Models.HealthLogInfo obj = new Hick.Models.HealthLogInfo();
            obj.Visitdate = Convert.ToDateTime(VisitDate);
            obj.DoctorName = DoctorName;
            obj.VisitReason = VisitReason;
            obj.VisitDiagnosis = VisitDiagnosis;


            HttpContext.Current.Cache["EditEncounters"] = obj;
        }

        [WebMethod]
        public static void DeleteEncounter(long EncounterID)
        {
            try {
                string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                var uri = Utility.GetServiceUrl("deletedoctorvist");

                IgJObject postData = new IgJObject();
                postData.Add("DoctorVisitId", EncounterID);
                postData.Add("Pin", PinValue);

                var res = Utility.PostRequestForSave(uri, postData.ToString(Formatting.None));
            }
            catch(Exception)
            {

            }

            //string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
            //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/deletedoctorvist";
            //var postData = "{\"DoctorVisitId\":\"" + EncounterID + "\",\"Pin\":\"" + PinValue + "\"}";
            //var res = Utility.PostRequestForSave(uri, postData);
            // ClientScript.RegisterStartupScript(GetType(), "scrpt", "parent.window.location.href=parent.window.location.href;", true);

            // exists in the user control deleteallergies
        }
    }
}