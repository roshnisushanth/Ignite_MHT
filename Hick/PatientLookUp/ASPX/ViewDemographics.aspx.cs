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

namespace Hick.PatientLookUp.ASPX
{
    public partial class ViewDemographics : System.Web.UI.Page
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

                BindPatientdetails(patientid);
            }
        }

        public void BindPatientdetails(long patientid)
        {

            try
            {
                List<PatientDetails> objColl = null;
                if (patientid != 0)
                {
                    string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                    var uri = Utility.GetServiceUrl("getpatientdetailsbypatientid");

                    IgJObject postData = new IgJObject();
                    postData.Add("Pin", PinValue);
                    postData.Add("PatientId", patientid);

                    objColl = Utility.PostRequest<PatientDetails>(uri, postData.ToString(Formatting.None));

                    //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/getpatientdetailsbypatientid";
                    //var postData = "{\"Pin\":\"" + PinValue + "\",\"PatientId\":\"" + patientid + "\"}";
                    //objColl = Utility.PostRequest<PatientDetails>(uri, postData);
                    if (objColl.Count > 0)
                    {
                        txt_firstname.Text = objColl[0].FirstName;
                        txt_lastname.Text = objColl[0].LastName;
                        txt_dob.Text = objColl[0].DOB;
                        txt_phonenumber.Text = objColl[0].PhoneNumber;
                        txt_hicn.Text = objColl[0].HICN;

                        txt_SSN.Text = objColl[0].Last4SSN;
                        txt_address1.Text = objColl[0].Address1;
                        txt_address2.Text = objColl[0].Address2;
                        txt_city.Text = objColl[0].City;
                        txt_zipcode.Text = objColl[0].Zipcode;
                        ddlState.SelectedValue = objColl[0].State.ToString();

                        //ddlstate
                    }
                }
                //objColl.Add(new Conditons {Condition="XYZ",DateOfOnset="17-08-2015",ICDCode="290",PatientID=12345 });
                //objColl.Add(new Conditons { Condition = "XYZd", DateOfOnset = "17-08-2015", ICDCode = "291", PatientID = 12343 });


            }
            catch (Exception ex)
            {

            }
        }
        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                string Pin = ConfigurationManager.AppSettings["Bridge_Base"];
                var uri = Utility.GetServiceUrl("updateignitepatientdetails");

                IgJObject postData = new IgJObject();
                postData.Add("PatientID", Session["PatientID"].ToString());
                postData.Add("Pin", Pin);
                postData.Add("LastName", txt_lastname.Text);
                postData.Add("FirstName", txt_firstname.Text);
                postData.Add("DOB", txt_dob.Text);
                postData.Add("Last4SSN", txt_SSN.Text);
                postData.Add("HICN", txt_hicn.Text);
                postData.Add("Address1", txt_address1.Text);
                postData.Add("Address2", txt_address2.Text);
                postData.Add("City", txt_city.Text);
                postData.Add("State", ddlState.SelectedValue);
                postData.Add("Zip", txt_zipcode.Text);
                postData.Add("PhoneNumber", txt_phonenumber.Text);

                var res = Utility.PostRequestForSave(uri, postData.ToString(Formatting.None));
                BindPatientdetails(Convert.ToInt32(Session["PatientID"]));

                //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/updateignitepatientdetails";
                //var postData = "{\"PatientID\":\"" + Session["PatientID"] + "\",\"Pin\":\"" + Pin + "\",\"LastName\":\"" + txt_lastname.Text + "\",
                //    \"FirstName\":\"" + txt_firstname.Text + "\",\"DOB\":\"" + txt_dob.Text + "\",\"Last4SSN\":\"" + txt_SSN.Text + "\",
                //    \"HICN\":\"" + txt_hicn.Text + "\",\"Address1\":\"" + txt_address1.Text + "\",\"Address2\":\"" + txt_address2.Text + "\",
                //    \"City\":\"" + txt_city.Text + "\",\"State\":\"" + ddlState.SelectedValue + "\",\"Zip\":\"" + txt_zipcode.Text + "\",\"PhoneNumber\":\"" + txt_phonenumber.Text + "\"}";
                //var res = Utility.PostRequestForSave(uri, postData);
                //BindPatientdetails(Convert.ToInt32(Session["PatientID"]));

            }
            catch (Exception exc)
            {

            }
        }
    }
}