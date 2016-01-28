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
    public partial class Conditions : Hick.Base.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static void CacheDetails(string description, string icd9code, string activesince, string conditioncheck, string inactivesince)
        {

          
            Hick.Models.Conditons obj = new Hick.Models.Conditons();
            obj.Condition = description;
            obj.ConditionCheck = conditioncheck;
            obj.DateOfOnset = activesince;
            obj.ICDCode = icd9code;
            obj.History = inactivesince;

            HttpContext.Current.Cache["EditConditions"] = obj;
        }

        [WebMethod]
        public static void DeleteConditions(long ConditionID)
        {
            try {
                string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                var uri = Utility.GetServiceUrl("deleteconditions");

                IgJObject postData = new IgJObject();
                postData.Add("ConditionID", ConditionID);
                postData.Add("Pin", PinValue);

                var res = Utility.PostRequestForSave(uri, postData.ToString(Formatting.None));
            }
            catch(Exception)
            {

            }

            //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/deleteconditions";
            //var postData = "{\"ConditionID\":\"" + ConditionID + "\",\"Pin\":\"" + PinValue + "\"}";
            //var res = Utility.PostRequestForSave(uri, postData);
            //// ClientScript.RegisterStartupScript(GetType(), "scrpt", "parent.window.location.href=parent.window.location.href;", true);

            // exists in the user control deleteallergies
        }
    }
}