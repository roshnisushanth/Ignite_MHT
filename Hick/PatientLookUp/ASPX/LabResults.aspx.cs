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
    public partial class LabResults : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static void CacheDetails(string Date, string TestType, string Results,string Doctor,string Adminby)
        {


            Hick.Models.LabImaging obj = new Hick.Models.LabImaging();

            obj.Date = Date;
            obj.TestType =TestType;
            obj.Results =Results;
            obj.RequestingDoctor= Doctor;
            obj.AdministeredBy =Adminby;

            HttpContext.Current.Cache["EditLabResults"] = obj;
        }

        [WebMethod]
        public static void DeleteLabResults(long LabResultID)
        {
            try {
                string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                var uri = Utility.GetServiceUrl("insertupdatelabimaging");

                IgJObject postData = new IgJObject();
                postData.Add("LabImagingId", LabResultID);
                postData.Add("Pin", PinValue);
                postData.Add("ActionItem", "DELETE");

                var res = Utility.PostRequestForSave(uri, postData.ToString(Formatting.None));
            }
            catch(Exception)
            {

            }

            //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/insertupdatelabimaging ";
            //var postData = "{\"LabImagingId\":\"" + LabResultID + "\",\"Pin\":\"" + PinValue + "\",\"ActionItem\":\"" + "DELETE" + "\"}";
            //var res = Utility.PostRequestForSave(uri, postData);
        }
    }
}