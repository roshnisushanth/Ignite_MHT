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
    public partial class ViewSocialHistory : System.Web.UI.Page
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

                BindLifeStyle(patientid);
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
                    postData.Add("PatientID", patientid);

                    objColl = Utility.PostRequest<PatientDetails>(uri, postData.ToString(Formatting.None));

                    //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/getpatientdetailsbypatientid";
                    //var postData = "{\"Pin\":\"" + PinValue + "\",\"PatientId\":\"" + patientid + "\"}";
                    //objColl = Utility.PostRequest<PatientDetails>(uri, postData);
                    if (objColl.Count > 0)
                    {
                        if (objColl[0].BMI.ToString() == "")
                        {
                            lblBMI.Text = "0";
                        }
                        else
                        {

                            lblBMI.Text = objColl[0].BMI.ToString();
                        }

                    }
                }
            }
            catch (Exception exc)
            {

            }
        }
        public void BindLifeStyle(long patientid)
        {
            try
            {
                List<LifeStyle> objColl = null;
                if (patientid != 0)
                {
                    string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                    var uri = Utility.GetServiceUrl("lifestyle");

                    IgJObject postData = new IgJObject();
                    postData.Add("Pin", PinValue);
                    postData.Add("PatientID", patientid);

                    objColl = Utility.PostRequest<LifeStyle>(uri, postData.ToString(Formatting.None));

                    //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/lifestyle";
                    //var postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + patientid + "\"}";
                    //objColl = Utility.PostRequest<LifeStyle>(uri, postData);
                    if (objColl.Count > 0)
                    {
                        if (objColl[0].Alcoholic.ToString() == "Yes")
                        {
                            ddlAlcoholic.SelectedValue = "Yes";
                            divDrink.Style.Value = "display:";
                        }
                        else if (objColl[0].Alcoholic.ToString() == "No")
                        {
                            ddlAlcoholic.SelectedValue = "No";
                            divDrink.Style.Value = "display:none";
                        }
                        else
                        {
                            ddlAlcoholic.SelectedValue = "Select";
                            divDrink.Style.Value = "display:none";
                        }
                        txtHowmanydrink.Text = objColl[0].DrinksPerWeek.ToString();
                        txtHowlongDrinking.Text = objColl[0].HowlongYearsDrinking.ToString();

                        if (objColl[0].Smoke == "Yes")
                        {
                            ddlSmoke.SelectedValue = "Yes";
                            divSmoke.Style.Value = "display:";
                        }
                        else if (objColl[0].Smoke == "No")
                        {
                            ddlSmoke.SelectedValue = "No";
                            divSmoke.Style.Value = "display:none";
                        }
                        else
                        {
                            ddlSmoke.SelectedValue = "Select";
                            divSmoke.Style.Value = "display:none";
                        }


                        if (objColl[0].Exercise == "Yes")
                        {
                            ddlExercise.SelectedValue = "Yes";
                            divExercise.Style.Value = "display:";

                            if (objColl[0].ExerciseDaysPerWeek == "1 to 2 times per week")
                            {
                                ddlHowmanydaysExercise.SelectedValue = "1 to 2 times per week";
                            }
                            else if (objColl[0].ExerciseDaysPerWeek == "3 times per week")
                            {
                                ddlHowmanydaysExercise.SelectedValue = "3 times per week";
                            }
                            else if (objColl[0].ExerciseDaysPerWeek == "4 or more times per week")
                            {
                                ddlHowmanydaysExercise.SelectedValue = "4 or more times per week";
                            }
                            else if (objColl[0].ExerciseDaysPerWeek == "Select")
                            {
                                ddlHowmanydaysExercise.SelectedValue = "Select";

                            }
                        }
                        else if (objColl[0].Smoke == "No")
                        {
                            ddlSmoke.SelectedValue = "No";
                            divSmoke.Style.Value = "display:none";
                        }
                        else
                        {
                            ddlSmoke.SelectedValue = "Select";
                            divSmoke.Style.Value = "display:none";
                        }

                        txtHowmanypack.Text = objColl[0].PacksPerDay.ToString();
                        txtHowlongSmoking.Text = objColl[0].HowlongYearsSmoking.ToString();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlAlcoholic_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAlcoholic.SelectedValue == "Yes")
            {
                divDrink.Style.Value = "display:";

            }
            else
            {
                divDrink.Style.Value = "display:none";
                txtHowmanydrink.Text = "";
                txtHowlongDrinking.Text = "";
            }
        }

        protected void ddlSmoke_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSmoke.SelectedValue == "Yes")
            {
                divSmoke.Style.Value = "display:";
            }
            else
            {
                divSmoke.Style.Value = "display:none";
                txtHowmanypack.Text = "";
                txtHowlongSmoking.Text = "";
            }

        }

        protected void ddlExercise_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlExercise.SelectedValue == "Yes")
            {
                divExercise.Style.Value = "display:";

            }
            else
            {
                divExercise.Style.Value = "display:none";
                //txtWhattypeExercise.Text = "";
                //txtHowmanydaysExercise.Text = "";
                ddlHowmanydaysExercise.SelectedValue = "Select";
            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                string Pin = ConfigurationManager.AppSettings["Bridge_Base"];
                var uri = Utility.GetServiceUrl("insertlifestyle");

                IgJObject postData = new IgJObject();
                postData.Add("PatientID", Session["PatientID"].ToString());
                postData.Add("Pin", Pin);
                postData.Add("UserId", Session["PhysicianID"].ToString());
                postData.Add("Alcoholic", ddlAlcoholic.SelectedValue);
                postData.Add("Smoke", ddlSmoke.SelectedValue);
                postData.Add("Exercise", ddlExercise.SelectedValue);
                postData.Add("HowlongYearsDrinking", txtHowlongDrinking.Text);
                postData.Add("DrinksPerWeek", txtHowmanydrink.Text);
                postData.Add("PacksPerDay", txtHowmanypack.Text);
                postData.Add("HowlongYearsSmoking", txtHowlongSmoking.Text);
                postData.Add("ExerciseDaysPerWeek", ddlHowmanydaysExercise.SelectedValue);
                
                var res = Utility.PostRequestForSave(uri, postData.ToString(Formatting.None));
                BindLifeStyle(Convert.ToInt32(Session["PatientID"]));

                //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/insertlifestyle";
                //var postData = "{\"PatientID\":\"" + Session["PatientID"] + "\",\"Pin\":\"" + Pin + "\",\"UserId\":\"" + Session["PhysicianID"] + "\",\"Alcoholic\":\"" + ddlAlcoholic.SelectedValue + "\",\"Smoke\":\"" + ddlSmoke.SelectedValue + "\",\"Exercise\":\"" + ddlExercise.SelectedValue + "\",\"HowlongYearsDrinking\":\"" + txtHowlongDrinking.Text + "\",\"DrinksPerWeek\":\"" + txtHowmanydrink.Text + "\",\"PacksPerDay\":\"" + txtHowmanypack.Text + "\",\"HowlongYearsSmoking\":\"" + txtHowlongSmoking.Text + "\",\"ExerciseDaysPerWeek\":\"" + ddlHowmanydaysExercise.SelectedValue + "\"}";
                //var res = Utility.PostRequestForSave(uri, postData);
                //BindLifeStyle(Convert.ToInt32(Session["PatientID"]));

            }
            catch (Exception exc)
            {

            }
        }
    }
}