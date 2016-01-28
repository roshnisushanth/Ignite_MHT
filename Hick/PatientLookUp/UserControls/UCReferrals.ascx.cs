using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hick.Models;
using System.Configuration;
using Newtonsoft.Json;

namespace Hick.PatientLookUp.UserControls
{
    public partial class UCReferrals : System.Web.UI.UserControl
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
           
                BindReferral(patientid);
            }
        }

        public void BindReferral(long patientid)
        {

            try
            {
                List<Referral> objColl = null;
                if (patientid != 0)
                {

                    string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                    var uri = Utility.GetServiceUrl("referralview");

                    IgJObject postData = new IgJObject();
                    postData.Add("Pin", PinValue);
                    postData.Add("PhysicianID", Session["PhysicianID"].ToString());
                    postData.Add("Status", "1");

                    objColl = Utility.PostRequest<Referral>(uri, postData.ToString(Formatting.None));

                    //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/referralview";
                    //var postData = "{\"Pin\":\"" + PinValue + "\",\"PhysicianID\":\"" + Session["PhysicianID"] + "\",\"Status\":\"" + 1 + "\"}";
                    //objColl = Utility.PostRequest<Referral>(uri, postData);

                    if (objColl[0].AssignedPhysician != null)
                    {
                        gdreferral.DataSource = objColl;
                        gdreferral.DataBind();
                    }
                    else
                    {
                        gdreferral.DataSource = null;
                        gdreferral.DataBind();
                    }

                }
                //objColl.Add(new Conditons {Condition="XYZ",DateOfOnset="17-08-2015",ICDCode="290",PatientID=12345 });
                //objColl.Add(new Conditons { Condition = "XYZd", DateOfOnset = "17-08-2015", ICDCode = "291", PatientID = 12343 });


            }
            catch (Exception ex)
            {

            }
        }


        protected void gdreferral_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int RfID = int.Parse(e.CommandArgument.ToString());
            string src = Page.ResolveUrl("~/PatientLookUp/ASPX/ReferralReceipt.aspx?ReffId=" + RfID);
            Response.Redirect(src, false);
        }
    }
}