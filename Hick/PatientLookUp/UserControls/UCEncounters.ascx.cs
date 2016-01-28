using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hick.Models;
using System.Net;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;
using System.Configuration;
using Newtonsoft.Json;

namespace Hick.PatientLookUp.UserControls
{
    public partial class UCEncounters : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                long patientid = 0;
                if (!String.IsNullOrEmpty(Request.QueryString["ptid"]))
                {
                    patientid = Convert.ToInt64(Request.QueryString["ptid"]);
                    //long patientid = 15929;
                    Session["PatientID"] = patientid;


                }
                else if (Session["PatientID"] != null)
                {
                    patientid = Convert.ToInt64(Session["PatientID"]);
                }

                BindEncounters(patientid);
            }
        }
        public void BindEncounters(long patientid)
        {

            try
            {
                List<HealthLogInfo> objColl = null;
                if (patientid != 0)
                {

                    string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                    var uri = Utility.GetServiceUrl("doctorvisit");

                    IgJObject postData = new IgJObject();
                    postData.Add("Pin", PinValue);
                    postData.Add("PatientID", patientid);

                    objColl = Utility.PostRequest<HealthLogInfo>(uri, postData.ToString(Formatting.None));

                    //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/doctorvisit";
                    //var postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + patientid + "\"}";
                    //objColl = Utility.PostRequest<HealthLogInfo>(uri, postData);

                }
                //objColl.Add(new Conditons {Condition="XYZ",DateOfOnset="17-08-2015",ICDCode="290",PatientID=12345 });
                //objColl.Add(new Conditons { Condition = "XYZd", DateOfOnset = "17-08-2015", ICDCode = "291", PatientID = 12343 });
                gdencounters.DataSource = objColl;
                gdencounters.DataBind();

            }
            catch (Exception ex)
            {

            }
        }
    }
}