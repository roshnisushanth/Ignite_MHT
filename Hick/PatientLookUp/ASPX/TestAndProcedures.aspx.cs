using Hick.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.PatientLookUp.ASPX
{
    public partial class TestAndProcedures : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static void CacheDetails(string Procedure, string Discription, string Date)
        {


            Hick.Models.Surgeries obj = new Hick.Models.Surgeries();

            obj.SurgeriesProcedure = Procedure;
            obj.Description = Discription;
            obj.Date = Date;

            HttpContext.Current.Cache["EditTestAndProcedures"] = obj;
        }

        [WebMethod]
        public static void DeleteProcedure(long ProcedureID)
        {
            try {
                string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                var uri = Utility.GetServiceUrl("updatetestandsurgeries");

                IgJObject postData = new IgJObject();
                postData.Add("SurgeriesId", ProcedureID);
                postData.Add("Pin", PinValue);
                postData.Add("ActionItem", "DELETE");

                var res = Utility.PostRequestForSave(uri, postData.ToString(Formatting.None));
            }
            catch(Exception)
            {

            }

            //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/updatetestandsurgeries";
            //var postData = "{\"SurgeriesId\":\"" + ProcedureID + "\",\"Pin\":\"" + PinValue + "\",\"ActionItem\":\"" + "DELETE" + "\"}";
            //var res = Utility.PostRequestForSave(uri, postData);
        }
    }
}