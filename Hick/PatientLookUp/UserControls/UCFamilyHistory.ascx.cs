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
    public partial class UCFamilyHistory : System.Web.UI.UserControl
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

                BindFamilyHistory(patientid);
            }
        }

        public void BindFamilyHistory(long patientid)
        {

            try
            {
                List<FamilyHistoryDetailInfo> objColl = null;
                if (patientid != 0)
                {

                    string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                    var uri = Utility.GetServiceUrl("patientfamilyhistory");

                    IgJObject postData = new IgJObject();
                    postData.Add("Pin", PinValue);
                    postData.Add("PatientID", patientid);

                    objColl = Utility.PostRequest<FamilyHistoryDetailInfo>(uri, postData.ToString(Formatting.None));

                    //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/patientfamilyhistory"; 
                    //var postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + patientid + "\"}";
                    //objColl = Utility.PostRequest<FamilyHistoryDetailInfo>(uri, postData);

                }
                //objColl.Add(new Conditons {Condition="XYZ",DateOfOnset="17-08-2015",ICDCode="290",PatientID=12345 });
                //objColl.Add(new Conditons { Condition = "XYZd", DateOfOnset = "17-08-2015", ICDCode = "291", PatientID = 12343 });
                gdfamilyhistory.DataSource = objColl;
                gdfamilyhistory.DataBind();

            }
            catch (Exception exc)
            {

            }
        }
    }
}