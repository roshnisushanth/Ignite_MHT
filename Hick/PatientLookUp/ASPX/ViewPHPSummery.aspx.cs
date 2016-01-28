//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using Hick.BridgeMessanger;
//using System.Data;
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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using Hick.Authorized;
using Newtonsoft.Json;

namespace Hick.PatientLookUp.ASPX
{
    
    public partial class ViewPHPSummery : System.Web.UI.Page
    {

        //BridgeMessangerService bridgeMessanger = new BridgeMessangerService();
        #region Demographics
        public string patientName,
                      gender,
                      dob,
                      race,
                      ethnicity,
                      preflanguage;
        #endregion
        #region Discharge
        public string discharge_date,
                      discharge_notes,
                      discharge_goals,
                      discharge_Instructions;
        #endregion
        #region Vital
        public string Vital_Height,Vital_Height_Unit,
                      Vital_Weight, Vital_Weight_Unit,
                      Vital_BMI,
                      Vital_Temperature, Vital_Temperature_Unit,
                      Vital_Pulse,
                      Vital_BP,
                      Vital_Respiration;
        #endregion

        #region Lifestyle
        public string Social_Ties, Relatives,
                      Relatives_Discuss_Private_Matters, Relatives_See_Monthly,
                      Living_With,
                      Per_Week, Excercise,
                      Smoking_Habits,
                      Smoke,
                      Alcohol;
        #endregion
        AuthorizedService service = new AuthorizedService();
        protected void gdallergies_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public string  dateTime;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                DateTime now = DateTime.Now;
                dateTime = now.ToString();
                long patientid = 0;
                if (!String.IsNullOrEmpty(Request.QueryString["pageview"]))
                {
                    if (Request.QueryString["pageview"].ToString() == "view")
                    {
                        var auditresult = service.AddAuditLog(Convert.ToInt32(Session["userid"].ToString()), 0, "PHP", "Not Applicable", "Not Applicable", DateTime.Now);
                    }

                }
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    patientid = Convert.ToInt64(Request.QueryString["id"]);

                }
            
                  
                BindMedications(patientid);
                BindConditions(patientid);
                BindFamilyHistory(patientid);
                BindLifeStyle(patientid);
                BindPatientdetails(patientid);
                BindImmunizations(patientid);
                bindLab(patientid);    
                BindAllergies(patientid);
                BindEncounters(patientid);
                BindReferrals(patientid);
                BindVitals(patientid);
                BindProceduresAndSurgeries(patientid);
                //BindProblems(patientid);

            }

        }
         public void BindMedications(long patientid)
        {

            try
            {
                List<Hick.Models.Medications> objColl = null;
                if (patientid != 0)
                {
                    string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                    var uri = Utility.GetServiceUrl("medications");

                    IgJObject postData = new IgJObject();
                    postData.Add("Pin", PinValue);
                    postData.Add("PatientID", patientid);

                    objColl = Utility.PostRequest<Hick.Models.Medications>(uri, postData.ToString(Formatting.None));

                    //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/medications";
                    //var postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + patientid + "\"}";
                    //objColl = Utility.PostRequest<Hick.Models.Medications>(uri, postData);

                    if (objColl.Count > 0)
                    {
                        gdmedication.DataSource = objColl;
                        gdmedication.DataBind();
                    }
                   
                }
             

            }
            catch (Exception exc)
            {

            }
        }
         public void BindConditions(long patientid)
         {

             try
             {
                 List<Conditons> objColl = null;
                 if (patientid != 0)
                 {

                     string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];

                    var uri = Utility.GetServiceUrl("conditionsbypatientid");

                    IgJObject postData = new IgJObject();
                    postData.Add("Pin", PinValue);
                    postData.Add("PatientID", patientid);

                    objColl = Utility.PostRequest<Conditons>(uri, postData.ToString(Formatting.None));

                    //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/conditionsbypatientid";
                    // var postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + patientid + "\"}";
                    // objColl = Utility.PostRequest<Conditons>(uri, postData);
                     if (objColl.Count > 0)
                     {
                     }
                 }
             }
             catch (Exception exc)
             {

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
                    // var postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + patientid + "\"}";
                    // objColl = Utility.PostRequest<FamilyHistoryDetailInfo>(uri, postData);
                     if (objColl.Count > 0)
                     {
                        gdfamily.DataSource = objColl;
                        gdfamily.DataBind();

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
                    // var postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + patientid + "\"}";
                    // objColl = Utility.PostRequest<LifeStyle>(uri, postData);

                     if (objColl.Count > 0)
                     {
                        Social_Ties = null;
                        Relatives = null;
                        Relatives_Discuss_Private_Matters = null;
                        Relatives_See_Monthly = null;
                        Living_With = null;
                        Per_Week = objColl[0].ExerciseDaysPerWeek;
                        Excercise = objColl[0].Exercise;
                        Smoking_Habits = objColl[0].PacksPerDay;
                        Smoke = objColl[0].Smoke;
                        Alcohol = objColl[0].Alcoholic;
                     }

                 }
             }
             catch (Exception exc)
             {

             }
         }
 
         public void BindImmunizations(long patientid)
         {
             try
             {
                 List<Hick.Models.Immunizations> objColl = null;
                 if (patientid != 0)
                 {
                     string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];

                    var uri = Utility.GetServiceUrl("immunization");

                    IgJObject postData = new IgJObject();
                    postData.Add("Pin", PinValue);
                    postData.Add("PatientID", patientid);

                    objColl = Utility.PostRequest<Hick.Models.Immunizations>(uri, postData.ToString(Formatting.None));

                    //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/immunization";
                    // var postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + patientid + "\"}";
                    // objColl = Utility.PostRequest<Immunizations>(uri, postData);
                     if (objColl.Count > 0)
                     {
                        gdimmunization.DataSource = objColl;
                        gdimmunization.DataBind();
                    }
                 }
             }
             catch (Exception exc)
             {

             }
         }
         public void BindProceduresAndSurgeries(long patientid)
         {
            try {
                List<Surgeries> objColl = null;
                string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                var uri = Utility.GetServiceUrl("surgeries");

                IgJObject postData = new IgJObject();
                postData.Add("Pin", PinValue);
                postData.Add("PatientID", patientid);

                objColl = Utility.PostRequest<Surgeries>(uri, postData.ToString(Formatting.None));

                //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/surgeries";
                // var postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + patientid + "\"}";
                // objColl = Utility.PostRequest<Surgeries>(uri, postData);


                if (objColl.Count > 1)
                {
                    gdtest.DataSource = objColl;
                    gdtest.DataBind();
                }
            }
            catch(Exception)
            {

            }
         }
         public void BindReferrals(long patientid)
         {
             try
             {
                 List<Referral> objColl = null;
                 if (patientid != 0)
                 {
                     string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                     var uri = Utility.GetServiceUrl("referralphp");

                    IgJObject postData = new IgJObject();
                    postData.Add("PatientID", patientid);
                    postData.Add("PhysicianID", Session["PhysicianID"].ToString());
                    postData.Add("Pin", PinValue);
                   

                    objColl = Utility.PostRequest<Referral>(uri, postData.ToString(Formatting.None));

                    //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/referralphp";
                    // var postData = "{\"PatientID\":\"" + patientid + "\",\"PhysicianID\":\"" + Session["PhysicianID"] + "\",\"Pin\":\"" + PinValue + "\"}";
                    // objColl = Utility.PostRequest<Referral>(uri, postData);
                     if (objColl.Count > 0)
                     {
                     }

                 }


             }
             catch (Exception exc)
             {

             }
         }

     
         public void bindLab(long patientid)
         {
             try
             {
                 List<LabImaging> objColl = null;
                 if (patientid != 0)
                 {
                     string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                    var uri = Utility.GetServiceUrl("lab");

                    IgJObject postData = new IgJObject();
                    postData.Add("Pin", PinValue);
                    postData.Add("PatientID", patientid);
                    
                    objColl = Utility.PostRequest<LabImaging>(uri, postData.ToString(Formatting.None));

                    //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/lab";
                    // var postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + patientid + "\"}";
                    // objColl = Utility.PostRequest<LabImaging>(uri, postData);
                     if (objColl.Count > 0)
                     {
                        gdLabResults.DataSource = objColl;
                        gdLabResults.DataBind();
                     }
                 }
               

             }
             catch (Exception ex)
             {
             }
         }

        //public void BindProblems(long patientid)
        //{
        //    try
        //    {
        //        List<LabImaging> objColl = null;
        //        if (patientid != 0)
        //        {
        //            string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
        //            var uri = ConfigurationManager.AppSettings["serviceURL"] + "/null";
        //            var postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + patientid + "\"}";
        //            objColl = Utility.PostRequest<LabImaging>(uri, postData);
        //            if (objColl.Count > 0)
        //            {
        //                gdproblem.DataSource = objColl;
        //                gdproblem.DataBind();
        //            }
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        // Complete
        public void BindVitals(long patientid)
         {
             List<VitalSignsInfo> objColl = null;
             if (patientid != 0)
             {
                 string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                var uri = Utility.GetServiceUrl("vitalsigns");

                IgJObject postData = new IgJObject();
                postData.Add("PatientID", patientid);
                postData.Add("Pin", PinValue);

                objColl = Utility.PostRequest<VitalSignsInfo>(uri, postData.ToString(Formatting.None));

                //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/vitalsigns";
                // var postData = "{\"PatientID\":\"" + patientid + "\",\"Pin\":\"" + PinValue + "\"}";
                // objColl = Utility.PostRequest<VitalSignsInfo>(uri, postData);
                 if (objColl.Count > 0)
                 {
                     Vital_Height = objColl[0].Height;
                     Vital_Height_Unit = objColl[0].HeightUnits;
                     Vital_Weight = objColl[0].Weight;
                     Vital_Weight_Unit = objColl[0].WeightUnits;
                     Vital_BMI = "";
                     Vital_Pulse = (objColl[0].Pulse!=null)? objColl[0].Pulse+" BPM": objColl[0].Pulse;
                     Vital_Temperature = objColl[0].Temperature;
                     Vital_Temperature_Unit = objColl[0].TemperatureUnit;
                     Vital_BP = (objColl[0].BloodPressure != null) ? objColl[0].BloodPressure + " mmHg" : objColl[0].BloodPressure;
                     Vital_Respiration = (objColl[0].Respiration != null) ? objColl[0].Respiration + " BPM" : objColl[0].Respiration;
                 }
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
                    // var postData = "{\"Pin\":\"" + PinValue + "\",\"PatientId\":\"" + patientid + "\"}";
                    // objColl = Utility.PostRequest<PatientDetails>(uri, postData);
                     if (objColl.Count > 0)
                     {
                         patientName = objColl[0].FirstName + " " + objColl[0].LastName;
                         gender = objColl[0].Gender;
                         dob = objColl[0].DOB;
                         race = objColl[0].Race;
                         ethnicity = objColl[0].City;
                         preflanguage = "English";
                     }
                 }
             }
             catch (Exception exc)
             {

             }
         }
         public void BindAllergies(long patientid)
         {
             try
             {
                 List<Allergy> objColl = null;
                 if (patientid != 0)
                 {
                     string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                    var uri = Utility.GetServiceUrl("allergies");

                    IgJObject postData = new IgJObject();
                    postData.Add("PatientID", patientid);
                    postData.Add("Pin", PinValue);

                    objColl = Utility.PostRequest<Allergy>(uri, postData.ToString(Formatting.None));

                    //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/allergies";
                    // var postData = "{\"PatientID\":\"" + patientid + "\",\"Pin\":\"" + PinValue + "\"}";
                    // objColl = Utility.PostRequest<Allergy>(uri, postData);
                     if (objColl.Count > 0)
                     {
                         gdallergies.DataSource = objColl;
                         gdallergies.DataBind();
                     }
                 }


             }
             catch (Exception exc)
             {

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
                    postData.Add("PatientID", patientid);
                    postData.Add("Pin", PinValue);

                    objColl = Utility.PostRequest<HealthLogInfo>(uri, postData.ToString(Formatting.None));

                    //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/doctorvisit";
                    // var postData = "{\"PatientID\":\"" + patientid + "\",\"Pin\":\"" + PinValue + "\"}";
                    // objColl = Utility.PostRequest<HealthLogInfo>(uri, postData);
                     if (objColl.Count > 0)
                     {
                         gdvisits.DataSource = objColl;
                         gdvisits.DataBind();
                     }
                 }


             }
             catch (Exception exc)
             {

             }
         }

         public void CareTeam(long patientid)
         {
             try
             {
                 List<HealthLogInfo> objColl = null;
                 if (patientid != 0)
                 {
                     string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                    var uri = Utility.GetServiceUrl("doctorvisit");

                    IgJObject postData = new IgJObject();
                    postData.Add("PatientID", patientid);
                    postData.Add("Pin", PinValue);

                    objColl = Utility.PostRequest<HealthLogInfo>(uri, postData.ToString(Formatting.None));
                     
                    //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/doctorvisit";
                    // var postData = "{\"PatientID\":\"" + patientid + "\",\"Pin\":\"" + PinValue + "\"}";
                    // objColl = Utility.PostRequest<HealthLogInfo>(uri, postData);
                     if (objColl.Count > 0)
                     {
                         gdvisits.DataSource = objColl;
                         gdvisits.DataBind();
                     }
                 }


             }
             catch (Exception exc)
             {

             }
         }
    }
}