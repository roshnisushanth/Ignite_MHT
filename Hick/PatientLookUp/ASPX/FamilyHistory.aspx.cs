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
    public partial class FamilyHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static void CacheDetails(string Relationship, string ConditionName, string OnsetDate)
        {


            Hick.Models.FamilyHistoryDetailInfo obj = new Hick.Models.FamilyHistoryDetailInfo();
            obj.Relationship = Relationship;
            obj.ConditionName = ConditionName;
            obj.OnsetDate = OnsetDate;

            HttpContext.Current.Cache["EditFamilyHistory"] = obj;
        }

        [WebMethod]
        public static void DeleteFamilyHistory(long PatientFamilyHistoryID)
        {
            try {
                string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                var uri = Utility.GetServiceUrl("deletefamilyhistory");

                IgJObject postData = new IgJObject();
                postData.Add("PatientFamilyHistoryID", PatientFamilyHistoryID);
                postData.Add("Pin", PinValue);

                var res = Utility.PostRequestForSave(uri, postData.ToString(Formatting.None));
            }
            catch(Exception)
            {

            }

            //string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
            //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/deletefamilyhistory";
            //var postData = "{\"PatientFamilyHistoryID\":\"" + PatientFamilyHistoryID + "\",\"Pin\":\"" + PinValue + "\"}";
            //var res = Utility.PostRequestForSave(uri, postData);
            // ClientScript.RegisterStartupScript(GetType(), "scrpt", "parent.window.location.href=parent.window.location.href;", true);

            // exists in the user control deleteallergies
        }
    }
}