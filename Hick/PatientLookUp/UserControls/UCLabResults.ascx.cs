using Hick.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.PatientLookUp.UserControls
{
    public partial class UCLabResults : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                long patientid = 0;
                if (!String.IsNullOrEmpty(Request.QueryString["ptid"]))
                {
                    patientid = Convert.ToInt64(Request.QueryString["ptid"]);
             
                    Session["PatientID"] = patientid;
             

                }
                else if (Session["PatientID"] != null)
                {
                    patientid = Convert.ToInt64(Session["PatientID"]);
                }
               
                BindLabresults(patientid);
            }
        }


        public void BindLabresults(long patientid)
        {
            try
            {
                List<LabImaging> objColl = null;
                if (patientid != 0)
                {
                    string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                    var uri = Utility.GetServiceUrl("lab");

                    IgJObject postData = new IgJObject();
                    postData.Add("PatientID", patientid);
                    postData.Add("Pin", PinValue);

                    objColl = Utility.PostRequest<LabImaging>(uri, postData.ToString(Formatting.None));

                    //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/lab";
                    //var postData = "{\"PatientID\":\"" + patientid + "\",\"Pin\":\"" + PinValue + "\"}";
                    //objColl = Utility.PostRequest<LabImaging>(uri, postData);

                }
                gdlabresult.DataSource = objColl;
                gdlabresult.DataBind();

            }
            catch (Exception exc)
            {

            }
        }
    }
}