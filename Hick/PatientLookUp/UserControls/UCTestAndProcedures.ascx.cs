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
    public partial class UCTestAndProcedures : System.Web.UI.UserControl
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
                    //Session["PIN"] = "1111";

                }
                else if (Session["PatientID"] != null)
                {
                    patientid = Convert.ToInt64(Session["PatientID"]);
                }
              
                   BindTestAndProcedures(patientid);
            }
        }


        public void BindTestAndProcedures(long patientid)
        {

            try
            {
                List<Surgeries> objColl = null;
                if (patientid != 0)
                {
                    string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                    var uri = Utility.GetServiceUrl("surgeries");

                    IgJObject postData = new IgJObject();
                    postData.Add("PatientID", patientid);
                    postData.Add("Pin", PinValue);

                    objColl = Utility.PostRequest<Surgeries>(uri, postData.ToString(Formatting.None));

                    //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/surgeries";
                    //var postData = "{\"PatientID\":\"" + patientid + "\",\"Pin\":\"" + PinValue + "\"}";
                    //objColl = Utility.PostRequest<Surgeries>(uri, postData);

                }
                gdtestandprocedure.DataSource = objColl;
                gdtestandprocedure.DataBind();

            }
            catch (Exception exc)
            {

            }
        }
    }
}