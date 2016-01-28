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
    public partial class Immunizations : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static void CacheDetails(string Immunization, string Date)
        {


            Hick.Models.Immunizations obj = new Hick.Models.Immunizations();
            obj.ImmunizationType = Immunization;
            obj.AdministrationDate = Convert.ToDateTime(Date);



            HttpContext.Current.Cache["EditImmunizations"] = obj;
        }

        [WebMethod]
        public static void DeleteImmunizations(long ImmunizationID)
        {
            try {
                string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                var uri = Utility.GetServiceUrl("deletemmunizations");

                IgJObject postData = new IgJObject();
                postData.Add("ImmunizationID", ImmunizationID);
                postData.Add("Pin", PinValue);

                var res = Utility.PostRequestForSave(uri, postData.ToString(Formatting.None));
            }
            catch(Exception)
            {

            }

            //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/deletemmunizations";
            //var postData = "{\"ImmunizationID\":\"" + ImmunizationID + "\",\"Pin\":\"" + PinValue + "\"}";
            //var res = Utility.PostRequestForSave(uri, postData);
            // ClientScript.RegisterStartupScript(GetType(), "scrpt", "parent.window.location.href=parent.window.location.href;", true);

            // exists in the user control deleteallergies
        }
    }
}