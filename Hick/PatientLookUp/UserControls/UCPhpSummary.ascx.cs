using System;
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

namespace Hick.PatientLookUp.UserControls
{
    public partial class UCPhpSummary : System.Web.UI.UserControl
    {
        AuthorizedService service = new AuthorizedService();
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
              
                BindMedications(patientid);
                BindConditions(patientid);
                BindFamilyHistory(patientid);
                BindLifeStyle(patientid);
                BindPatientdetails(patientid);
                BindImmunizations(patientid);
                bindLab(patientid);
                GetLastLogin(patientid);
                BindAllergies(patientid);
                BindEncounters(patientid);
                BindReferrals(patientid);
                BindVitals(patientid);
                BindProceduresAndSurgeries(patientid);
                //bindAllergies();
                //bindMedicalHistory();
                //bindFamilyHistory();
                //bindLifestyle();
                //

                //bindLab();
                //BindImmunizations();
                //bindDoctorVisit();
                //bindHospitalizations();
                //bindProblems();
            }

        }
        public void BindProceduresAndSurgeries(long patientid)
        {
            List<Surgeries> objColl = null;
            //string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
            var uri = Utility.GetServiceUrl("surgeries");

            IgJObject postData = new IgJObject();
            //postData.Add("Pin", PinValue);
            postData.Add("PatientID", patientid);


            objColl = Utility.PostRequest<Surgeries>(uri, postData.ToString(Formatting.None));

            //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/surgeries";
            //var postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + patientid + "\"}";
            //objColl = Utility.PostRequest<Surgeries>(uri, postData);


            if (objColl.Count > 0)
            {
                if (objColl[0].SurgeriesProcedure != null && objColl[0].SurgeriesProcedure != "")
                {
                    lblprocedure1.Text = objColl[0].SurgeriesProcedure;
                }
                else
                {
                    lblprocedure1.Text = "N/A";
                }

                if (objColl[0].Date != null && objColl[0].Date != "")
                {
                    lblprocdate1.Text =Convert.ToDateTime(objColl[0].Date).ToString("MM/dd/yyyy"); 


                }
                else
                {
                    lblprocdate1.Text = "N/A";
                }
            }
            else
            {
                lblprocedure1.Text = "N/A";
                lblprocdate1.Text = "N/A";
            }

            if (objColl.Count > 1)
            {
                if (objColl[1].SurgeriesProcedure != null && objColl[1].SurgeriesProcedure != "")
                {
                    lblprocedure2.Text = objColl[1].SurgeriesProcedure;
                }
                else
                {
                    lblprocedure2.Text = "N/A";
                }

                if (objColl[1].Date != null && objColl[1].Date != "")
                {
                    lblprocdate2.Text =Convert.ToDateTime(objColl[1].Date).ToString("MM/dd/yyyy");
                }
                else
                {
                    lblprocdate2.Text = "N/A";
                }
            }
            else
            {
                lblprocedure2.Text = "N/A";
                lblprocdate2.Text = "N/A";
            }
        }


        public void BindVitals(long patientid)
        {
            List<VitalSignsInfo> objColl = null;
            if (patientid != 0)
            {
                //string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                var uri = Utility.GetServiceUrl("vitalsigns");

                IgJObject postData = new IgJObject();
                postData.Add("PatientID", patientid);
                //postData.Add("Pin", PinValue);
                
                objColl = Utility.PostRequest<VitalSignsInfo>(uri, postData.ToString(Formatting.None));

                //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/vitalsigns";
                //var postData = "{\"PatientID\":\"" + patientid + "\",\"Pin\":\"" + PinValue + "\"}";
                //objColl = Utility.PostRequest<VitalSignsInfo>(uri, postData);
                if (objColl.Count > 0)
                {
                    txt_Height.Text = objColl[0].Height;
                    if (!string.IsNullOrWhiteSpace(objColl[0].HeightUnits))
                    {
                        if (objColl[0].HeightUnits.StartsWith("i"))
                        {
                            ddlHeightUnits.Items[0].Selected = true;
                        }
                        if (objColl[0].HeightUnits.StartsWith("c"))
                        {
                            ddlHeightUnits.Items[1].Selected = true;
                        }
                    }
                    txt_weight.Text = objColl[0].Weight;
                    if (!string.IsNullOrWhiteSpace(objColl[0].WeightUnits))
                    {
                        ddlWeightUnits.Items.FindByText(objColl[0].WeightUnits.ToString()).Selected = true;
                    }
                    txt_temp.Text = objColl[0].Temperature;


                    if (!string.IsNullOrWhiteSpace(objColl[0].TemperatureUnit))
                    {
                        if (objColl[0].TemperatureUnit.StartsWith("F"))
                        {
                            ddlTemperatureUnit.Items[0].Selected = true;
                        }
                        if (objColl[0].TemperatureUnit.StartsWith("C"))
                        {
                            ddlTemperatureUnit.Items[1].Selected = true;
                        }
                    }
                    txt_pulse.Text = objColl[0].Pulse;
                    txtRespiration.Text = objColl[0].Respiration;
                    if (!string.IsNullOrWhiteSpace(objColl[0].BloodPressure))
                    {
                        string[] BP = objColl[0].BloodPressure.ToString().Split('/');
                        if (BP.Length > 0 && BP.Length < 3)
                        {
                            txtBP.Text = string.IsNullOrWhiteSpace(BP[0]) ? "" : BP[0].ToString();
                            txtBP2.Text = string.IsNullOrWhiteSpace(BP[1]) ? "" : BP[1].ToString();
                        }
                    }
                }
            }
        }
        public void BindReferrals(long patientid)
        {
            try
            {
                List<Referral> objColl = null;
                if (patientid != 0)
                {

  
                    var uri = Utility.GetServiceUrl("referralview");

                    IgJObject postData = new IgJObject();
                    postData.Add("PatientID", patientid);
                    postData.Add("PhysicianID", Session["PhysicianID"].ToString());
                    postData.Add("Status", "1");
          

                    objColl = Utility.PostRequest<Referral>(uri, postData.ToString(Formatting.None));

                    //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/referralphp";
                    //var postData = "{\"PatientID\":\"" + patientid + "\",\"PhysicianID\":\"" + Session["PhysicianID"] + "\",\"Pin\":\"" + PinValue + "\"}";
                    //objColl = Utility.PostRequest<Referral>(uri, postData);

                    if (objColl.Count > 0)
                    {
                        if (objColl[0].CreatedDate != null)
                        {
                            lblreferraldate.Text = objColl[0].CreatedDate.ToString("MM/dd/yyyy").Replace("01/01/0001","N/A");
                        }
                        else
                        {
                            lblreferraldate.Text = "N/A";
                        }

                        if (objColl[0].Service != null && objColl[0].Service != "")
                        {
                            lblrefreason.Text = objColl[0].Service;
                        }
                        else
                        {
                            lblrefreason.Text = "N/A";
                        }
                        //if (objColl[0].Status != null)
                        //{
                        //    if (objColl[0].Status == 2)
                        //    {
                        //        lblrefstatus.Text = "Pending";
                        //    }
                        //}
                        //else
                        //{
                        //    lblrefstatus.Text = "N/A";
                        //}

                        if(objColl[0].StatusText!=null)
                        {
                            lblrefstatus.Text = objColl[0].StatusText;
                        }
                        else
                        {
                            lblrefstatus.Text = "N/A";
                        }
                
                    }
                    else
                    {
                        lblreferraldate.Text = "N/A";
                        lblrefreason.Text = "N/A";
                        lblrefstatus.Text = "N/A";
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
                    //string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                    var uri = Utility.GetServiceUrl("doctorvisit");

                    IgJObject postData = new IgJObject();
                    postData.Add("PatientID", patientid);
                    //postData.Add("Pin", PinValue);

                    objColl = Utility.PostRequest<HealthLogInfo>(uri, postData.ToString(Formatting.None));

                    //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/doctorvisit";
                    //var postData = "{\"PatientID\":\"" + patientid + "\",\"Pin\":\"" + PinValue + "\"}";
                    //objColl = Utility.PostRequest<HealthLogInfo>(uri, postData);
                    if (objColl.Count > 0)
                    {
                        if (objColl[0].VisitReason != null && objColl[0].VisitReason != "")
                        {
                            lblvisitreason1.Text = objColl[0].VisitReason;
                        }
                        else
                        {
                            lblvisitreason1.Text = "N/A";
                        }

                        if (objColl[0].Visitdate != null)
                        {
                            lblvisitdate1.Text = objColl[0].Visitdate.ToShortDateString();
                        }
                        else
                        {
                            lblvisitdate1.Text = "N/A";
                        }
                    }
                    else
                    {
                        lblvisitreason1.Text = "N/A";
                        lblvisitdate1.Text = "N/A";
                    }

                    if (objColl.Count > 1)
                    {
                        if (objColl[1].VisitReason != null && objColl[1].VisitReason != "")
                        {
                            lblvisitreason2.Text = objColl[1].VisitReason;
                        }
                        else
                        {
                            lblvisitreason2.Text = "N/A";
                        }

                        if (objColl[1].Visitdate != null)
                        {
                            lblvisitdate2.Text = objColl[1].Visitdate.ToShortDateString();
                        }
                        else
                        {
                            lblvisitdate2.Text = "N/A";
                        }
                    }
                    else
                    {
                        lblvisitreason2.Text = "N/A";
                        lblvisitdate2.Text = "N/A";
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
                    //string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                    var uri = Utility.GetServiceUrl("allergies");

                    IgJObject postData = new IgJObject();
                    postData.Add("PatientID", patientid);
                    //postData.Add("Pin", PinValue);

                    objColl = Utility.PostRequest<Allergy>(uri, postData.ToString(Formatting.None));

                    //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/allergies";
                    //var postData = "{\"PatientID\":\"" + patientid + "\",\"Pin\":\"" + PinValue + "\"}";
                    //objColl = Utility.PostRequest<Allergy>(uri, postData);
                    if (objColl.Count > 0)
                    {
                        if (objColl[0].AllergyType != null && objColl[0].AllergyType != "")
                        {
                            lblallergy1.Text = objColl[0].AllergyType;
                        }
                        else
                        {
                            lblallergy1.Text = "N/A";
                        }

                        if (objColl[0].DateLastOccured != null && objColl[0].DateLastOccured != "")
                        {
                            lblallergydate1.Text = objColl[0].DateLastOccured;
                        }
                        else
                        {
                            lblallergydate1.Text = "N/A";
                        }
                    }
                    else
                    {
                        lblallergy1.Text = "N/A";
                        lblallergydate1.Text = "N/A";
                    }

                    if (objColl.Count > 1)
                    {
                        if (objColl[1].AllergyType != null && objColl[1].AllergyType != "")
                        {
                            lblallergy2.Text = objColl[1].AllergyType;
                        }
                        else
                        {
                            lblallergy2.Text = "N/A";
                        }

                        if (objColl[1].DateLastOccured != null && objColl[1].DateLastOccured != "")
                        {
                            lblallergydate2.Text = objColl[1].DateLastOccured;
                        }
                        else
                        {
                            lblallergydate2.Text = "N/A";
                        }
                    }
                    else
                    {
                        lblallergy2.Text = "N/A";
                        lblallergydate2.Text = "N/A";
                    }
                }


            }
            catch (Exception exc)
            {

            }
        }
        public void GetLastLogin(long patientid)
        {
            try
            {
                string constr = Utility.DBConnectionString;
                List<Users> objUserColl = new List<Users>();
                if (Session["userid"] != null)
                {
                    Users objuser = null;
                    using (SqlConnection conn = new SqlConnection())
                    {

                        conn.ConnectionString = constr;
                        conn.Open();

                        using (SqlCommand command = new SqlCommand("sp_hick_GetLastLogin", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@PatientID", patientid);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    objuser = new Users();
                                    objuser.Lastloggedin = !String.IsNullOrEmpty(Convert.ToString(reader["LastLoggedIn"])) ? Convert.ToString(reader["LastLoggedIn"]) : "NA";
                                    objUserColl.Add(objuser);
                                }
                            }

                        }
                    }
                }
                if (objUserColl.Count > 0)
                {
                    //float CompletedPercentage = 0;

                    lblLastLoggedin.Text = "Last Logged in" + " " + Convert.ToDateTime(objUserColl[0].Lastloggedin).ToString("MMMM dd,  yyyy");
                    //CompletedPercentage = (Convert.Tof(objUserColl[0].Completed) / (Convert.ToDouble(objUserColl[0].NotCompleted) + Convert.ToDouble(objUserColl[0].NoTimerLog))) * 100;
                }
                else
                {
                    lblLastLoggedin.Text = "N/A";
                }


            }
            catch (Exception)
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
                    //string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                    var uri = Utility.GetServiceUrl("lab");

                    IgJObject postData = new IgJObject();
                    //postData.Add("Pin", PinValue);
                    postData.Add("PatientID", patientid);

                    objColl = Utility.PostRequest<LabImaging>(uri, postData.ToString(Formatting.None));

                    //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/lab";
                    //var postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + patientid + "\"}";
                    //objColl = Utility.PostRequest<LabImaging>(uri, postData);
                    if (objColl.Count > 0)
                    {
                        if (objColl[0].TestType != null && objColl[0].TestType != "")
                        {
                            lblLabResults.Text = objColl[0].TestType;
                        }
                        else
                        {
                            lblLabResults.Text = "N/A";
                        }
                        if (objColl[0].Date != null)
                        {
                            lblLabResultsDate.Text =Convert.ToDateTime(objColl[0].Date).ToString("MM/dd/yyyy").Replace("01/01/0001","N/A");
                        }
                        else
                        {
                            lblLabResultsDate.Text = "N/A";
                        }
                    }
                    else
                    {
                        lblLabResults.Text = "N/A";
                        lblLabResultsDate.Text = "N/A";
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }

        public void BindImmunizations(long patientid)
        {
            try
            {
                List<Immunizations> objColl = null;
                if (patientid != 0)
                {
                    //string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                    var uri = Utility.GetServiceUrl("immunization");

                    IgJObject postData = new IgJObject();
                    //postData.Add("Pin", PinValue);
                    postData.Add("PatientID", patientid);

                    objColl = Utility.PostRequest<Immunizations>(uri, postData.ToString(Formatting.None));

                    //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/immunization";
                    //var postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + patientid + "\"}";
                    //objColl = Utility.PostRequest<Immunizations>(uri, postData);
                    if (objColl.Count > 0)
                    {
                        if (objColl[0].ImmunizationType != null && objColl[0].ImmunizationType != "")
                        {
                            lblImmunization.Text = objColl[0].ImmunizationType;
                        }

                        if (objColl[0].AdministrationDate != null)
                        {
                            lblImmunizationDate.Text = objColl[0].AdministrationDate.ToString("MM/dd/yyyy");
                        }
                    }
                    else
                    {
                        lblImmunization.Text = "N/A";
                        lblImmunizationDate.Text = "N/A";
                    }
                    if (objColl.Count > 1)
                    {
                        if (objColl[1].ImmunizationType != null && objColl[1].ImmunizationType != "")
                        {
                            lblImmunization2.Text = objColl[1].ImmunizationType;
                        }

                        if (objColl[1].AdministrationDate != null)
                        {
                            lblImmunizationDate2.Text = objColl[1].AdministrationDate.ToString("MM/dd/yyyy");
                        }
                    }
                    else
                    {
                        lblImmunization2.Text = "N/A";
                        lblImmunizationDate2.Text = "N/A";
                    }
                }
            }
            catch (Exception exc)
            {

            }
        }
        public void BindMedications(long patientid)
        {

            try
            {
                List<Medications> objColl = null;
                if (patientid != 0)
                {
                    //string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                    var uri = Utility.GetServiceUrl("medications");

                    IgJObject postData = new IgJObject();
                    //postData.Add("Pin", PinValue);
                    postData.Add("PatientID", patientid);

                    objColl = Utility.PostRequest<Medications>(uri, postData.ToString(Formatting.None));

                    //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/medications";
                    //var postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + patientid + "\"}";
                    //objColl = Utility.PostRequest<Medications>(uri, postData);

                    if (objColl.Count > 0)
                    {
                        if (objColl[0].Description != null && objColl[0].Description != "")
                        {
                            lblMedicationName.Text = objColl[0].Description;
                        }
                        else
                        {
                            lblMedicationName.Text = "N/A";
                        }
                        if (objColl[0].Date != null && objColl[0].Date != "")
                        {
                            lblMedicationDate.Text = objColl[0].Date;
                        }
                        else
                        {
                            lblMedicationDate.Text = "N/A";
                        }
                    }

                    else
                    {
                        lblMedicationName.Text = "N/A";
                        lblMedicationDate.Text = "N/A";
                    }
                    if (objColl.Count > 1)
                    {

                        if (objColl[1].Description != null && objColl[1].Description != "")
                        {
                            lblMedicationName2.Text = objColl[1].Description;
                        }
                        else
                        {
                            lblMedicationName2.Text = "N/A";
                        }

                        if (objColl[1].Date != null && objColl[1].Date != "")
                        {
                            lblMedicationDate2.Text = objColl[1].Date;
                        }
                        else
                        {
                            lblMedicationDate2.Text = "N/A";
                        }
                    }
                    else
                    {
                        lblMedicationName2.Text = "N/A";
                        lblMedicationDate2.Text = "N/A";
                    }


                }
                //objColl.Add(new Conditons {Condition="XYZ",DateOfOnset="17-08-2015",ICDCode="290",PatientID=12345 });
                //objColl.Add(new Conditons { Condition = "XYZd", DateOfOnset = "17-08-2015", ICDCode = "291", PatientID = 12343 });


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

                    //string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                    var uri = Utility.GetServiceUrl("conditionsbypatientid");

                    IgJObject postData = new IgJObject();
                    //postData.Add("Pin", PinValue);
                    postData.Add("PatientID", patientid);

                    objColl = Utility.PostRequest<Conditons>(uri, postData.ToString(Formatting.None));

                    //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/conditionsbypatientid";
                    //var postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + patientid + "\"}";
                    //objColl = Utility.PostRequest<Conditons>(uri, postData);
                    if (objColl.Count > 0)
                    {
                        if (objColl[0].Condition != null && objColl[0].Condition != "")
                        {
                            lblConditionName.Text = objColl[0].Condition;
                        }
                        else
                        {
                            lblConditionName.Text = "N/A";
                        }

                        if (objColl[0].DateOfOnset != null && objColl[0].DateOfOnset != "")
                        {
                            lblConditionDate.Text = objColl[0].DateOfOnset;
                        }
                        else
                        {
                            lblConditionDate.Text = "N/A";
                        }
                    }
                    else
                    {
                        lblConditionName.Text = "N/A";
                        lblConditionDate.Text = "N/A";
                    }

                    if (objColl.Count > 1)
                    {
                        if (objColl[1].Condition != null && objColl[1].Condition != "")
                        {
                            lblConditionName2.Text = objColl[1].Condition;
                        }
                        else
                        {
                            lblConditionName2.Text = "N/A";
                        }

                        if (objColl[1].DateOfOnset != null && objColl[1].DateOfOnset != "")
                        {
                            lblConditionDate2.Text = objColl[1].DateOfOnset;
                        }
                        else
                        {
                            lblConditionDate2.Text = "N/A";
                        }
                    }
                    else
                    {
                        lblConditionName2.Text = "N/A";
                        lblConditionDate2.Text = "N/A";
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
                    //string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                    var uri = Utility.GetServiceUrl("patientfamilyhistory");

                    IgJObject postData = new IgJObject();
                    //postData.Add("Pin", PinValue);
                    postData.Add("PatientID", patientid);

                    objColl = Utility.PostRequest<FamilyHistoryDetailInfo>(uri, postData.ToString(Formatting.None));

                    //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/patientfamilyhistory"; //familyhistory
                    //var postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + patientid + "\"}";
                    //objColl = Utility.PostRequest<FamilyHistoryDetailInfo>(uri, postData);

                    if (objColl.Count > 0)
                    {
                        if (objColl[0].Relationship == "Father")
                        {
                            lblFamilyHistoryFather.Text = "Yes";
                        }
                        else
                        {
                            lblFamilyHistoryFather.Text = "No";
                        }
                         if (objColl[0].Relationship == "Mother")
                        {
                            lblFamilyHistoryMother.Text = "Yes";
                        }
                         else
                         {
                               lblFamilyHistoryMother.Text = "No";
                         }
                         if (objColl[0].Relationship == "Siblings")
                         {
                             lblFamilyHistorySiblings.Text = "Yes";
                         }
                         else
                         {
                             lblFamilyHistorySiblings.Text = "No";
                         }

                        //if (objColl[0].Mother == "Living")
                        //{
                        //    lblFamilyHistoryMother.Text = "Living";
                        //}
                        //else
                        //{
                        //    lblFamilyHistoryMother.Text = "Deceased";
                        //}

                        //if (objColl[0].Siblings == "Yes")
                        //{
                        //    lblFamilyHistorySiblings.Text = "Yes";
                        //}
                        //else
                        //{
                        //    lblFamilyHistorySiblings.Text = "No";
                        //}

                    }


                }
                //objColl.Add(new Conditons {Condition="XYZ",DateOfOnset="17-08-2015",ICDCode="290",PatientID=12345 });
                //objColl.Add(new Conditons { Condition = "XYZd", DateOfOnset = "17-08-2015", ICDCode = "291", PatientID = 12343 });


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
                    //string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                    var uri = Utility.GetServiceUrl("lifestyle");

                    IgJObject postData = new IgJObject();
                    //postData.Add("Pin", PinValue);
                    postData.Add("PatientID", patientid);

                    objColl = Utility.PostRequest<LifeStyle>(uri, postData.ToString(Formatting.None));

                    //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/lifestyle";
                    //var postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + patientid + "\"}";
                    //objColl = Utility.PostRequest<LifeStyle>(uri, postData);

                    if (objColl.Count > 0)
                    {
                        if (objColl[0].Alcoholic == "Yes")
                        {
                            lblAlcoholUse.Text = "Yes";
                        }
                        else if (objColl[0].Alcoholic == "No")
                        {
                            lblAlcoholUse.Text = "No";
                        }
                        else
                        {
                            lblAlcoholUse.Text = "N/A";
                        }
                        if (objColl[0].Smoke == "Yes")
                        {
                            lblTobaccoUse.Text = "Yes";
                        }
                        else if (objColl[0].Smoke == "No")
                        {
                            lblTobaccoUse.Text = "No";
                        }
                        else
                        {
                            lblTobaccoUse.Text = "N/A";
                        }

                    }

                }
            }
            catch (Exception exc)
            {

            }
        }
        public void BindPatientdetails(long patientid)
        {
            try
            {
                List<PatientDetails> objColl = null;
                if (patientid != 0)
                {
                    //string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                    var uri = Utility.GetServiceUrl("getpatientdetailsbypatientid");

                    IgJObject postData = new IgJObject();
                    //postData.Add("Pin", PinValue);
                    postData.Add("PatientId", patientid);

                    objColl = Utility.PostRequest<PatientDetails>(uri, postData.ToString(Formatting.None));

                    //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/getpatientdetailsbypatientid";
                    //var postData = "{\"Pin\":\"" + PinValue + "\",\"PatientId\":\"" + patientid + "\"}";
                    //objColl = Utility.PostRequest<PatientDetails>(uri, postData);
                    if (objColl.Count > 0)
                    {
                        lblpatname.Text = objColl[0].FirstName + " " + objColl[0].LastName;

                        //start demographics
                        if (!String.IsNullOrEmpty(objColl[0].DOB))
                        {
                            lbldemoDOB.Text = objColl[0].DOB;
                        }
                        else
                        {
                            lbldemoDOB.Text = "N/A";
                        }
                        if (!String.IsNullOrEmpty(objColl[0].State))
                        {
                            if (!String.IsNullOrEmpty(objColl[0].City))
                            {
                                lbldemocitystate.Text = objColl[0].City + "," + objColl[0].State;
                            }
                            else
                            {
                                lbldemocitystate.Text = "N/A" + "," + objColl[0].State;
                            }
                        }
                        else
                        {
                            lbldemocitystate.Text = "N/A";
                        }

                        if (!String.IsNullOrEmpty(objColl[0].Phone))
                        {
                            lbldemophno.Text = objColl[0].Phone;
                        }
                        else
                        {
                            lbldemophno.Text = "N/A";
                        }
                        //end demographics
                        if (!String.IsNullOrEmpty(objColl[0].Height))
                        {
                            lblBP.Text = objColl[0].BP;
                        }
                        else
                        {
                            lblBP.Text = "N/A";
                        }
                        if (!String.IsNullOrEmpty(objColl[0].Gender))
                        {
                            lblgender.Text = objColl[0].Gender;
                        }
                        else
                        {
                            lblgender.Text = "N/A";
                        }

                        if (!String.IsNullOrEmpty(objColl[0].Weight))
                        {
                            lblweight.Text = objColl[0].Weight + "lbs";
                        }
                        else
                        {
                            lblweight.Text = "N/A";
                        }


                        // lblheight.Text = objColl[0].Height;

                        if (!String.IsNullOrEmpty(objColl[0].Height))
                        {
                            string[] Height = objColl[0].Height.Split('.');
                            if (Height.Length > 1)
                            {
                                string feet = Height[0] == "" ? ("0" + "'") : (Height[0] + "'");
                                string inch = Height[1] == "" ? ("0" + '"') : (Height[1] + '"');
                                if (feet + inch != "0'0\"")
                                    lblheight.Text = feet + inch;
                                else
                                    lblheight.Text = "N/A";
                            }
                            else
                            {
                                lblheight.Text = Height[0].ToString();
                            }
                        }
                        else
                        {
                            lblheight.Text = "N/A";
                        }

                        int age = new DateTime(DateTime.Now.Subtract(Convert.ToDateTime(objColl[0].DOB.ToString())).Ticks).Year - 1;
                        lblAge.Text = age.ToString();
                    }
                }
            }
            catch (Exception exc)
            {

            }
        }

        protected void Button_PHPDownload_Click(object sender, EventArgs e)
        {
      
                int iPatientid = Convert.ToInt32(Session["PatientID"]);

                //int iPatientid = Convert.ToInt32(Session["Patient"]);
                GeneratePDFFile(iPatientid);
                string filename = Convert.ToString(ViewState["newFile"]);
                string filenamepath = Server.MapPath("~\\Uploads\\") + filename;
                if (File.Exists(filenamepath))
                {
                    WebClient req = new WebClient();
                    req.Credentials = CredentialCache.DefaultCredentials;
                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Buffer = true;
                    Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                    byte[] data = req.DownloadData(filenamepath);
                    Response.BinaryWrite(data);
                    Response.End();
                    var auditresult = service.AddAuditLog(Convert.ToInt32(Session["userid"].ToString()), 1, "PHP", "Not Applicable", "Not Applicable", DateTime.Now);
                }
                else
                {
                    Response.Write("<script type='text/javascript'>alert('File Doesnt Exists');</script>");
                }
     
        }

        public void GeneratePDFFile(int iPatient)
        {
            Document dc = new Document();
            List<PatientDetails> objColl = null;
            if (iPatient != 0)
            {
                //string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                var uri = Utility.GetServiceUrl("getpatientdetailsbypatientid");

                IgJObject postData = new IgJObject();
                //postData.Add("Pin", PinValue);
                postData.Add("PatientID", iPatient);

                objColl = Utility.PostRequest<PatientDetails>(uri, postData.ToString(Formatting.None));

                //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/getpatientdetailsbypatientid";
                //var postData = "{\"Pin\":\"" + PinValue + "\",\"PatientId\":\"" + iPatient + "\"}";
                //objColl = Utility.PostRequest<PatientDetails>(uri, postData);
                if (objColl.Count > 0)
                {
                    Session["CURRTIME"] = DateTime.Now.ToString("MMddyyyyHHmmss");
                    ViewState["newFile"] = "Patient_Clinical" + "_" + iPatient + "_" + Convert.ToString(objColl[0].LastName) + "_" + Session["CURRTIME"].ToString() + ".pdf";
                    PdfWriter.GetInstance(dc, new FileStream(Server.MapPath("~\\Uploads\\") + "\\" + ViewState["newFile"], FileMode.Create));
                    dc.Open();

                    BaseFont bfFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                    Font fLabelMainHeading = new Font(bfFont, 14, Font.BOLD, Color.BLACK);
                    Font fLabel = new Font(bfFont, 12, Font.BOLD, Color.BLACK);
                    Font fLabelNormal = new Font(bfFont, 10, Font.BOLD, Color.BLACK);
                    Font fText = new Font(bfFont, 10, Font.NORMAL, Color.BLACK);
                    Font fTextItalic = new Font(bfFont, 10, Font.ITALIC, Color.BLACK);

                    //image

                    string imagepath = Server.MapPath("../../images");

                    iTextSharp.text.Image chkdImg = iTextSharp.text.Image.GetInstance(imagepath + "/checkbox_active.gif");
                    chkdImg.ScaleToFit(9f, 9f);

                    iTextSharp.text.Image ntChkdImg = iTextSharp.text.Image.GetInstance(imagepath + "/checkbox.gif");
                    ntChkdImg.ScaleToFit(9f, 9f);

                    //Image gif = Image.GetInstance(imagepath + "/mikesdotnetting.gif");
                    //iTextSharp.text.Image gif = iTextSharp.text.Image.GetInstance(imagepath + "/sub-logo.gif");
                    // Chunk cimageHeading = new Chunk("" + gif);
                    //gif.SetAbsolutePosition(250, 0);

                    iTextSharp.text.Table tblImage = new iTextSharp.text.Table(3);
                    tblImage.Width = 80;
                    tblImage.BorderWidth = 0;

                    Cell cImage1 = new Cell(new Phrase(""));
                    cImage1.BorderWidth = 0;
                    cImage1.BorderWidthBottom = 0;
                    cImage1.BorderWidthLeft = 0;
                    cImage1.BorderWidthRight = 0;
                    cImage1.BorderWidthTop = 0;
                    cImage1.Width = "20";
                    //Cell cImage2 = new Cell(gif);
                    //cImage2.BorderWidth = 0;
                    //cImage2.BorderWidthBottom = 0;
                    //cImage2.BorderWidthLeft = 0;
                    //cImage2.BorderWidthRight = 0;
                    //cImage2.BorderWidthTop = 0;
                    //cImage1.Width = "60";
                    Cell cImage3 = new Cell(new Phrase(""));
                    cImage3.BorderWidth = 0;
                    cImage3.BorderWidthBottom = 0;
                    cImage3.BorderWidthLeft = 0;
                    cImage3.BorderWidthRight = 0;
                    cImage3.BorderWidthTop = 0;
                    cImage1.Width = "20";

                    tblImage.AddCell(cImage1);
                    //tblImage.AddCell(cImage2);
                    tblImage.AddCell(cImage3);

                    dc.Add(tblImage);

                    //LOGO            
                    String imageFilePath = Server.MapPath("/") + "/images/ignite_logo_small.png";
                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageFilePath);
                    image.ScalePercent(40f);
                    //image.SpacingBefore = 100f;
                    image.Alignment = Element.ALIGN_CENTER;
                    dc.Add(image);
                    //END

                    //Heading
                    Chunk cLabelHeading = new Chunk("\t \t \t \t \t \t \t \t \t \t \t \t \t \t \t \t \t \t \t \t \t \t \t   PERSONAL HEALTH PROFILE", fLabelMainHeading);

                    Phrase phFirstLine = new Phrase();

                    phFirstLine.Add(cLabelHeading);

                    Paragraph pPatname = new Paragraph();
                    pPatname.Add(phFirstLine);
                    dc.Add(pPatname);

                    #region Demographics

                    // line space
                    Chunk cSpaceDemographics = new Chunk("\r\r");
                    Phrase phSpaceDemographics = new Phrase();
                    phSpaceDemographics.Add(cSpaceDemographics);
                    dc.Add(phSpaceDemographics);

                    // Identification Heading
                    Chunk cLabelDemographics = new Chunk("Patient Demographics", fLabel);
                    Phrase phDemographics = new Phrase();
                    phDemographics.Add(cLabelDemographics);
                    Paragraph pDemographics = new Paragraph();
                    pDemographics.Add(phDemographics);
                    dc.Add(phDemographics);

                    // line space
                    Chunk cSpaceDemographics1 = new Chunk("\r");
                    //Phrase phSpaceIdentification1 = new Phrase();
                    //phSpaceIdentification1.Add(cSpaceIdentification1);
                    dc.Add(cSpaceDemographics1);

                    //uri = ConfigurationManager.AppSettings["serviceURL"] + "/getpatientdetailsbypatientid";
                    //postData = "{\"Pin\":\"" + PinValue + "\",\"PatientId\":\"" + iPatient + "\"}";
                    //objColl = Utility.PostRequest<PatientDetails>(uri, postData);

                    PdfPTable tblDemographics = new PdfPTable(6);
                    tblDemographics.TotalWidth = 550f;
                    tblDemographics.LockedWidth = true;

                    // 1st row
                    tblDemographics.AddCell(GetCell("Patient Name", "", 1, 1));
                    tblDemographics.AddCell(GetCell("Height", "", 1, 1));
                    tblDemographics.AddCell(GetCell("Weight", "", 1, 1));
                    tblDemographics.AddCell(GetCell("Gender", "", 1, 1));
                    tblDemographics.AddCell(GetCell("Age", "", 1, 1));
                    tblDemographics.AddCell(GetCell("BP", "", 1, 1));
                    // 2nd row
                    tblDemographics.AddCell(GetCell(objColl[0].LastName + ", " + objColl[0].FirstName, 1, 1));
                    if (!String.IsNullOrEmpty(objColl[0].Height))
                    {
                        string[] Height = objColl[0].Height.Split('.');
                        if (Height.Length > 1)
                        {
                            string feet = Height[0] == "" ? ("0" + "'") : (Height[0] + "'");
                            string inch = Height[1] == "" ? ("0" + '"') : (Height[1] + '"');
                            if (feet + inch != "0'0\"")

                                tblDemographics.AddCell(GetCell(feet + inch, 1, 1));
                            else
                                tblDemographics.AddCell(GetCell("N/A", 1, 1));
                        }
                        else
                        {
                            tblDemographics.AddCell(GetCell(Height[0].ToString(), 1, 1));
                        }
                    }

                    tblDemographics.AddCell(GetCell(objColl[0].Weight + " " + "lbs", 1, 1));
                    tblDemographics.AddCell(GetCell(objColl[0].Gender, 1, 1));
                    if (!string.IsNullOrEmpty(objColl[0].DOB))
                    {
                        int age = new DateTime(DateTime.Now.Subtract(Convert.ToDateTime(objColl[0].DOB.ToString())).Ticks).Year - 1;
                        tblDemographics.AddCell(GetCell(age.ToString(), 1, 1));
                        tblDemographics.AddCell(GetCell(objColl[0].BP, 1, 1));
                    }
                    dc.Add(tblDemographics);

                    #endregion


                    #region Identification

                    // line space
                    Chunk cSpaceIdentification = new Chunk("\r\r");
                    Phrase phSpaceIdentification = new Phrase();
                    phSpaceIdentification.Add(cSpaceIdentification);
                    dc.Add(phSpaceIdentification);

                    // Identification Heading
                    Chunk cLabelIdentification = new Chunk("Patient Information", fLabel);
                    Phrase phIdentification = new Phrase();
                    phIdentification.Add(cLabelIdentification);
                    Paragraph pIdentification = new Paragraph();
                    pIdentification.Add(phIdentification);
                    dc.Add(phIdentification);

                    // line space
                    Chunk cSpaceIdentification1 = new Chunk("\r");
                    //Phrase phSpaceIdentification1 = new Phrase();
                    //phSpaceIdentification1.Add(cSpaceIdentification1);
                    dc.Add(cSpaceIdentification1);

                    uri = Utility.GetServiceUrl("getpatientdetailsbypatientid");
                    postData = new IgJObject();
                    postData.Add("PatientID", iPatient);
                    objColl = Utility.PostRequest<PatientDetails>(uri, postData.ToString(Formatting.None));

                    //uri = ConfigurationManager.AppSettings["serviceURL"] + "/getpatientdetailsbypatientid";
                    //postData = "{\"Pin\":\"" + PinValue + "\",\"PatientId\":\"" + iPatient + "\"}";
                    //objColl = Utility.PostRequest<PatientDetails>(uri, postData);

                    //PdfPTable tblIdentification = new PdfPTable(7);
                    PdfPTable tblIdentification = new PdfPTable(4);
                    tblIdentification.TotalWidth = 550f;
                    tblIdentification.LockedWidth = true;

                    // 1st row
                    tblIdentification.AddCell(GetCell("Last Name: ", objColl[0].LastName.ToString(), 2, 1));
                    tblIdentification.AddCell(GetCell("First Name: ", objColl[0].FirstName.ToString(), 2, 1));

                    // 2nd row
                    if (!string.IsNullOrEmpty(objColl[0].MaidenName))
                    {
                        tblIdentification.AddCell(GetCell("Middle Name: ", objColl[0].MiddleName, 2, 1));
                        tblIdentification.AddCell(GetCell("Maiden Name: ", objColl[0].MaidenName.ToString(), 2, 1));
                    }
                    // 3rd row. 
                    if (!string.IsNullOrEmpty(objColl[0].LastName))
                    {

                        tblIdentification.AddCell(GetCell("Last 4 digits of SSN: ", objColl[0].LastName, 2, 1));
                    }
                    if (!string.IsNullOrEmpty(objColl[0].HICN))
                    {
                        tblIdentification.AddCell(GetCell("HICN: ", objColl[0].HICN.ToString(), 2, 1));
                    }
                    //4th row
                    if (!string.IsNullOrEmpty(objColl[0].DOB))
                    {
                        DateTime bdate = Convert.ToDateTime(objColl[0].DOB.ToString());

                        int date = bdate.Day;
                        int month = bdate.Month;
                        int year = bdate.Year;

                        string dob = month.ToString() + "/" + date.ToString() + "/" + year.ToString();
                        tblIdentification.AddCell(GetCell("DOB: ", dob, 1, 1));
                    }
                    if (!string.IsNullOrEmpty(objColl[0].Gender))
                    {
                        tblIdentification.AddCell(GetCell("Gender: ", objColl[0].Gender, 1, 1));
                    }
                    tblIdentification.AddCell(GetCell("Race: ", objColl[0].Race, 2, 1));

                    //5th row
                    if (!string.IsNullOrEmpty(objColl[0].PhoneNumber))
                    {
                        tblIdentification.AddCell(GetCell("Home Phone: ", objColl[0].PhoneNumber.ToString(), 1, 1));
                    }
                    if (!string.IsNullOrEmpty(objColl[0].CellPhone))
                    {
                        tblIdentification.AddCell(GetCell("Cell Phone: ", objColl[0].CellPhone.ToString(), 1, 1));
                    }
                    if (!string.IsNullOrEmpty(objColl[0].EmailId))
                    {

                        tblIdentification.AddCell(GetCell("Email Address: ", objColl[0].EmailId.ToString(), 2, 1));
                    }
                    // 6th row. 
                    if (!string.IsNullOrEmpty(objColl[0].Address1))
                    {
                        tblIdentification.AddCell(GetCell("Primary Address: ", objColl[0].Address1.ToString(), 2, 1));
                    }
                    if (!string.IsNullOrEmpty(objColl[0].AlterAddress1))
                    {

                        tblIdentification.AddCell(GetCell("Alternate Address: ", objColl[0].AlterAddress1.ToString(), 2, 1));
                    }
                    //7th row
                    string blood = "";
                    if (!string.IsNullOrEmpty(objColl[0].BloodType) && objColl[0].BloodType.ToString() == "Select")
                    {
                        blood = "";
                    }
                    else if (!string.IsNullOrEmpty(objColl[0].BloodType))
                    {
                        blood = objColl[0].BloodType.ToString();
                    }

                    if (!string.IsNullOrEmpty(objColl[0].EyeColor))
                    {
                        tblIdentification.AddCell(GetCell("Eye Color: ", objColl[0].EyeColor.ToString()));
                    }
                    if (!string.IsNullOrEmpty(objColl[0].HairColor))
                    {
                        tblIdentification.AddCell(GetCell("Hair Color: ", objColl[0].HairColor.ToString()));
                    }

                    tblIdentification.AddCell(GetCell("Blood/RH Type: ", blood));

                    tblIdentification.AddCell(GetCell("Birthmark/Scars: ", objColl[0].Birthmark));
                    //8th row
                    if (!string.IsNullOrEmpty(objColl[0].PrimaryFinancialClass))
                    {
                        tblIdentification.AddCell(GetCell("Primary Insurance: ", objColl[0].PrimaryFinancialClass.ToString()));
                    }
                    if (!string.IsNullOrEmpty(objColl[0].PrimaryHealthInsurance))
                    {
                        tblIdentification.AddCell(GetCell("Company Name: ", objColl[0].PrimaryHealthInsurance));
                    }

                    tblIdentification.AddCell(GetCell("Group Number: ", ""));
                    if (!string.IsNullOrEmpty(objColl[0].OriginalPolicyNumber))
                    {
                        tblIdentification.AddCell(GetCell("Policy Number: ", objColl[0].OriginalPolicyNumber.ToString()));
                    }
                    //8th row
                    if (!string.IsNullOrEmpty(objColl[0].SecondaryFinancialClass))
                    {
                        tblIdentification.AddCell(GetCell("Secondary Insurance: ", objColl[0].SecondaryFinancialClass.ToString()));
                    }
                    if (!string.IsNullOrEmpty(objColl[0].SecondaryHealthInsurance))
                    {
                        tblIdentification.AddCell(GetCell("Company Name: ", objColl[0].SecondaryHealthInsurance.ToString()));
                    }
                    tblIdentification.AddCell(GetCell("Group Number: ", ""));
                    if (!string.IsNullOrEmpty(objColl[0].SecondaryPolicyNumber))
                    {

                        tblIdentification.AddCell(GetCell("Policy Number: ", objColl[0].SecondaryPolicyNumber));
                    }
                    // 9th row. 
                    if (!string.IsNullOrEmpty(objColl[0].OrginalBP))
                    {
                        tblIdentification.AddCell(GetCell("For Original Medicare only, Name of PBB: ", objColl[0].OrginalBP.ToString(), 2, 1));
                    }
                    if (!string.IsNullOrEmpty(objColl[0].OriginalPolicyNumber))
                    {
                        tblIdentification.AddCell(GetCell("Policy Number: ", objColl[0].OriginalPolicyNumber.ToString(), 2, 1));
                    }
                    dc.Add(tblIdentification);

                    #endregion
                    #region Emergency Contacts
                    // line space
                    Chunk cSpaceEmergency = new Chunk("\r");
                    Phrase phSpaceEmergency = new Phrase();
                    phSpaceEmergency.Add(cSpaceEmergency);
                    dc.Add(phSpaceEmergency);
                    // Emergency Heading
                    Chunk cLabelEmergency = new Chunk("Emergency Contact", fLabel);
                    Phrase phEmergency = new Phrase();
                    phEmergency.Add(cLabelEmergency);
                    Paragraph pEmergency = new Paragraph();
                    pEmergency.Add(phEmergency);
                    dc.Add(phEmergency);
                    // line space
                    Chunk cSpaceEmergency1 = new Chunk("\r\r");
                    dc.Add(cSpaceEmergency1);

                    uri = Utility.GetServiceUrl("emergencycontact");
                    postData = new IgJObject();
                    postData.Add("PatientID", iPatient);
                    List<EmergencyContact> objemg = Utility.PostRequest<EmergencyContact>(uri, postData.ToString(Formatting.None));


                    //List<EmergencyContact> objemg = null;
                    //uri = ConfigurationManager.AppSettings["serviceURL"] + "/emergencycontact";
                    //postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + iPatient + "\"}";
                    //objemg = Utility.PostRequest<EmergencyContact>(uri, postData);

                    if (objemg.Count > 0)
                    {
                        PdfPTable tblEmergency = new PdfPTable(2);
                        tblEmergency.TotalWidth = 550f;
                        tblEmergency.LockedWidth = true;
                        // 1st row
                        tblEmergency.AddCell(GetCell("Primary Emergency Contact Name: ", objemg[0].PrimaryEmergencyLastName.ToString() + " " + objemg[0].PrimaryEmergencyFirstName.ToString()));
                        tblEmergency.AddCell(GetCell("Relationship: ", objemg[0].PrimaryEmergencyRelationship.ToString()));
                        // 2nd row
                        tblEmergency.AddCell(GetCell("Address 1: ", objemg[0].PrimaryEmergencyAddress1.ToString()));
                        tblEmergency.AddCell(GetCell("Address 2: ", objemg[0].PrimaryEmergencyAddress2.ToString()));
                        // 3rd row
                        tblEmergency.AddCell(GetCell("Home Phone: ", objemg[0].PrimaryEmergencyHomePhone.ToString()));
                        tblEmergency.AddCell(GetCell("Work Phone: ", objemg[0].PrimaryEmergencyWorkPhone.ToString()));
                        // 4thd row
                        tblEmergency.AddCell(GetCell("Cell Phone: ", objemg[0].PrimaryEmergencyCellPhone.ToString()));
                        //tblEmergency.AddCell(GetCell("E-mail: ", objemg[0].PrimaryEmergencyEmailID.ToString()));
                        dc.Add(tblEmergency);
                    }
                    else
                    {
                        PdfPTable tblEmergency = new PdfPTable(2);
                        tblEmergency.TotalWidth = 550f;
                        tblEmergency.LockedWidth = true;
                        // 1st row
                        tblEmergency.AddCell(GetCell("Primary Emergency Contact Name: ", ""));
                        tblEmergency.AddCell(GetCell("Relationship: ", ""));
                        // 2nd row
                        tblEmergency.AddCell(GetCell("Address 1: ", ""));
                        tblEmergency.AddCell(GetCell("Address 2: ", ""));
                        // 3rd row
                        tblEmergency.AddCell(GetCell("Home Phone: ", ""));
                        tblEmergency.AddCell(GetCell("Work Phone: ", ""));
                        // 4thd row
                        tblEmergency.AddCell(GetCell("Cell Phone: ", ""));
                        tblEmergency.AddCell(GetCell("E-mail: ", ""));
                        dc.Add(tblEmergency);
                    }
                    // line space
                    Chunk cSpaceSecondary1 = new Chunk("\r");
                    dc.Add(cSpaceSecondary1);
                    if (objemg.Count > 0)
                    {
                        PdfPTable tblSecondary = new PdfPTable(2);
                        tblSecondary.TotalWidth = 550f;
                        tblSecondary.LockedWidth = true;
                        // 1st row
                        tblSecondary.AddCell(GetCell("Secondary Emergency Contact Name: ", objemg[0].SecondaryEmergencyLastName.ToString() + " " + objemg[0].SecondaryEmergencyFirstName.ToString()));
                        string secondaryEmergencyRel = "";
                        if (objemg[0].SecondaryEmergencyRelationship.ToString() == "Select")
                        {
                            secondaryEmergencyRel = "";
                        }
                        else
                        {
                            if (objemg[0].SecondaryEmergencyRelationship.ToString() == "Sel")
                            {
                                secondaryEmergencyRel = "Self";
                            }
                            else
                            {
                                secondaryEmergencyRel = objemg[0].SecondaryEmergencyRelationship.ToString();
                            }
                        }
                        tblSecondary.AddCell(GetCell("Relationship: ", secondaryEmergencyRel));
                        // 2nd row
                        tblSecondary.AddCell(GetCell("Address 1: ", objemg[0].SecondaryEmergencyAddress1.ToString()));
                        tblSecondary.AddCell(GetCell("Address 2: ", objemg[0].SecondaryEmergencyAddress2.ToString()));
                        // 3rd row
                        tblSecondary.AddCell(GetCell("Home Phone: ", objemg[0].SecondaryEmergencyHomePhone.ToString()));
                        tblSecondary.AddCell(GetCell("Work Phone: ", objemg[0].SecondaryEmergencyWorkPhone.ToString()));
                        // 4th row
                        tblSecondary.AddCell(GetCell("Cell Phone: ", objemg[0].SecondaryEmergencyCellPhone.ToString()));
                        //tblSecondary.AddCell(GetCell("E-mail: ", objemg[0].SecondaryEmergencyEmailID.ToString()));
                        dc.Add(tblSecondary);
                    }
                    else
                    {
                        PdfPTable tblSecondary = new PdfPTable(2);
                        tblSecondary.TotalWidth = 550f;
                        tblSecondary.LockedWidth = true;
                        tblSecondary.AddCell(GetCell("Name: ", ""));
                        tblSecondary.AddCell(GetCell("Relationship: ", ""));
                        tblSecondary.AddCell(GetCell("Address 2: ", ""));
                        tblSecondary.AddCell(GetCell("Home Phone: ", ""));
                        tblSecondary.AddCell(GetCell("Work Phone: ", ""));
                        tblSecondary.AddCell(GetCell("Cell Phone: ", ""));
                        tblSecondary.AddCell(GetCell("E-mail: ", ""));
                        dc.Add(tblSecondary);
                    }
                    #endregion

                    #region Healthcare Providers

                    // line space
                    Chunk cSpaceProviders = new Chunk("\r");
                    Phrase phSpaceProviders = new Phrase();
                    phSpaceProviders.Add(cSpaceProviders);
                    dc.Add(phSpaceProviders);

                    // Emergency Heading
                    Chunk cLabelProviders = new Chunk("Healthcare Providers", fLabel);
                    Phrase phProviders = new Phrase();
                    phProviders.Add(cLabelProviders);
                    Paragraph pProviders = new Paragraph();
                    pProviders.Add(phProviders);
                    dc.Add(phProviders);

                    // line space
                    Chunk cSpaceProviders1 = new Chunk("\r");
                    dc.Add(cSpaceProviders1);

                    iTextSharp.text.Table tblProviders = new iTextSharp.text.Table(4);
                    tblProviders.Width = 105;
                    tblProviders.BorderWidth = 0;
                    //tblProviders.BorderColor = Color.BLACK;
                    tblProviders.Padding = 3;
                    tblProviders.Spacing = 1;
                    Cell cProviders1 = new Cell(new Phrase("Provider Name", fLabelNormal));
                    cProviders1.HorizontalAlignment = 1;
                    Cell cProviders2 = new Cell(new Phrase("PCP(Y/N)", fLabelNormal));
                    cProviders2.HorizontalAlignment = 1;
                    Cell cProviders3 = new Cell(new Phrase("Specialty", fLabelNormal));
                    cProviders3.HorizontalAlignment = 1;
                    Cell cProviders5 = new Cell(new Phrase("Phone", fLabelNormal));
                    cProviders5.HorizontalAlignment = 1;
                    tblProviders.AddCell(cProviders1);
                    tblProviders.AddCell(cProviders2);
                    tblProviders.AddCell(cProviders3);
                    tblProviders.AddCell(cProviders5);

                    uri = Utility.GetServiceUrl("gethealthcareproviders");
                    postData = new IgJObject();
                    postData.Add("PatientID", iPatient);
                    List<HealthProvider> objHealth = Utility.PostRequest<HealthProvider>(uri, postData.ToString(Formatting.None));

                    //List<HealthProvider> objHealth = null;
                    //uri = ConfigurationManager.AppSettings["serviceURL"] + "/gethealthcareproviders";
                    //postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + iPatient + "\"}";
                    //objHealth = Utility.PostRequest<HealthProvider>(uri, postData);

                    if (objHealth.Count > 0)
                    {
                        for (int i = 0; i < objHealth.Count; i++)
                        {
                            tblProviders.AddCell(new Phrase(Convert.ToString(objHealth[i].ProviderName), fText));
                            tblProviders.AddCell(new Phrase(Convert.ToString(objHealth[i].pcp), fText));
                            tblProviders.AddCell(new Phrase(Convert.ToString(objHealth[i].Speciality), fText));
                            tblProviders.AddCell(new Phrase(Convert.ToString(objHealth[i].Phone), fText));
                        }

                        dc.Add(tblProviders);
                    }
                    else
                    {
                        dc.Add(tblProviders);

                        Chunk cNAProviders = new Chunk(" No Records found for Healthcare Providers.", fTextItalic);
                        dc.Add(cNAProviders);
                    }


                    #endregion
                    #region Conditions

                    // line space
                    Chunk cSpaceConditions = new Chunk("\r");
                    Phrase phSpaceConditions = new Phrase();
                    phSpaceConditions.Add(cSpaceConditions);
                    dc.Add(phSpaceConditions);

                    // Emergency Heading
                    Chunk cLabelConditions = new Chunk("Conditions", fLabel);
                    Phrase phConditions = new Phrase();
                    phConditions.Add(cLabelConditions);
                    Paragraph pConditions = new Paragraph();
                    pConditions.Add(phConditions);
                    dc.Add(phConditions);

                    // line space
                    Chunk cSpaceConditions1 = new Chunk("\r\r");
                    dc.Add(cSpaceConditions1);

                    iTextSharp.text.Table tblConditions = new iTextSharp.text.Table(4);
                    tblConditions.Width = 105;
                    tblConditions.BorderWidth = 0;
                    //tblProviders.BorderColor = Color.BLACK;
                    tblConditions.Padding = 3;
                    tblConditions.Spacing = 1;
                    Cell cConditions1 = new Cell(new Phrase("Name", fLabelNormal));
                    cConditions1.HorizontalAlignment = 1;
                    Cell cConditions2 = new Cell(new Phrase("Status", fLabelNormal));
                    cConditions2.HorizontalAlignment = 1;
                    Cell cConditions3 = new Cell(new Phrase("Onset Date", fLabelNormal));
                    cConditions3.HorizontalAlignment = 1;
                    Cell cConditions5 = new Cell(new Phrase("Details", fLabelNormal));
                    cConditions5.HorizontalAlignment = 1;
                    tblConditions.AddCell(cConditions1);
                    tblConditions.AddCell(cConditions2);
                    tblConditions.AddCell(cConditions3);
                    tblConditions.AddCell(cConditions5);


                    uri = Utility.GetServiceUrl("conditionsbypatientid");
                    postData = new IgJObject();
                    postData.Add("PatientID", iPatient);
                    List<Conditons> objcon = Utility.PostRequest<Conditons>(uri, postData.ToString(Formatting.None));

                    //List<Conditons> objcon = null;
                    //uri = ConfigurationManager.AppSettings["serviceURL"] + "/conditionsbypatientid";
                    //postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + iPatient + "\"}";
                    //objcon = Utility.PostRequest<Conditons>(uri, postData);
                    objcon = objcon.Where(x => (x.Condition == "Chronic Heart Failure (CHF)" || x.Condition == "Chronic Obstructive Pulmonary Disease (COPD)" || x.Condition == "Coronary Artery Disease (CAD)" || x.Condition == "Diabetes Mellitus Type 1" || x.Condition == "Diabetes Mellitus Type 2" || x.Condition == "Ischemic Vascular Disease (IVD)" || x.Condition == "Hypertension (HTN)") && (x.ConditionStatus == "Active" || x.ConditionStatus == "" || x.ConditionStatus == null)).ToList();

                    if (objcon.Count > 0)
                    {
                        for (int i = 0; i < objcon.Count; i++)
                        {
                            tblConditions.AddCell(new Phrase(Convert.ToString(objcon[i].Condition), fText));
                            tblConditions.AddCell(new Phrase(Convert.ToString(objcon[i].ConditionStatus), fText));
                            tblConditions.AddCell(new Phrase(Convert.ToString(objcon[i].DateOfOnset), fText));
                            tblConditions.AddCell(new Phrase(Convert.ToString(objcon[i].OtherInfo), fText));
                        }

                        dc.Add(tblConditions);
                    }
                    else
                    {
                        dc.Add(tblConditions);

                        Chunk cNAConditions = new Chunk(" No Records found for Conditions.", fTextItalic);
                        dc.Add(cNAConditions);
                    }

                    #endregion

                    #region Problems

                    // line space
                    Chunk cSpaceProblems = new Chunk("\r");
                    Phrase phSpaceProblems = new Phrase();
                    phSpaceProblems.Add(cSpaceProblems);
                    dc.Add(phSpaceProblems);

                    // Emergency Heading
                    Chunk cLabelProblems = new Chunk("Problems", fLabel);
                    Phrase phProblems = new Phrase();
                    phProblems.Add(cLabelProblems);
                    Paragraph pProblems = new Paragraph();
                    pProblems.Add(phProblems);
                    dc.Add(phProblems);

                    // line space
                    Chunk cSpaceProblems1 = new Chunk("\r\r");
                    dc.Add(cSpaceProblems1);

                    iTextSharp.text.Table tblProblems = new iTextSharp.text.Table(4);
                    tblProblems.Width = 105;
                    tblProblems.BorderWidth = 0;
                    //tblProviders.BorderColor = Color.BLACK;
                    tblProblems.Padding = 3;
                    tblProblems.Spacing = 1;
                    Cell cProblems1 = new Cell(new Phrase("Name", fLabelNormal));
                    cProblems1.HorizontalAlignment = 1;
                    Cell cProblems2 = new Cell(new Phrase("Status", fLabelNormal));
                    cProblems2.HorizontalAlignment = 1;
                    Cell cProblems3 = new Cell(new Phrase("Onset Date", fLabelNormal));
                    cProblems3.HorizontalAlignment = 1;
                    Cell cProblems5 = new Cell(new Phrase("Details", fLabelNormal));
                    cProblems5.HorizontalAlignment = 1;
                    tblProblems.AddCell(cProblems1);
                    tblProblems.AddCell(cProblems2);
                    tblProblems.AddCell(cProblems3);
                    tblProblems.AddCell(cProblems5);


                    objcon = objcon.Where(x => (x.Condition != "Chronic Heart Failure (CHF)" && x.Condition != "Chronic Obstructive Pulmonary Disease (COPD)" && x.Condition != "Coronary Artery Disease (CAD)" && x.Condition != "Diabetes Mellitus Type 1" && x.Condition != "Diabetes Mellitus Type 2" && x.Condition != "Ischemic Vascular Disease (IVD)" && x.Condition != "Hypertension (HTN)") && (x.ConditionStatus == "Active" || x.ConditionStatus == "" || x.ConditionStatus == null)).ToList();

                    if (objcon.Count > 0)
                    {
                        for (int i = 0; i < objcon.Count; i++)
                        {
                            tblProblems.AddCell(new Phrase(Convert.ToString(objcon[i].Condition), fText));
                            tblProblems.AddCell(new Phrase(Convert.ToString(objcon[i].ConditionStatus), fText));
                            tblProblems.AddCell(new Phrase(Convert.ToString(objcon[i].DateOfOnset), fText));
                            tblProblems.AddCell(new Phrase(Convert.ToString(objcon[i].OtherInfo), fText));
                        }

                        dc.Add(tblProblems);
                    }
                    else
                    {
                        dc.Add(tblProblems);

                        Chunk cNAProblems = new Chunk(" No Records found for Problems.", fTextItalic);
                        dc.Add(cNAProblems);
                    }

                    #endregion

                    #region Allergies/Drug Sensitivities

                    // line space
                    Chunk cSpaceAllergies = new Chunk("\r");
                    Phrase phSpaceAllergies = new Phrase();
                    phSpaceAllergies.Add(cSpaceAllergies);
                    dc.Add(phSpaceAllergies);

                    // Allergies Heading
                    Chunk cLabelAllergies = new Chunk("Allergies", fLabel);
                    Phrase phAllergies = new Phrase();
                    phAllergies.Add(cLabelAllergies);
                    Paragraph pAllergies = new Paragraph();
                    pAllergies.Add(phAllergies);
                    dc.Add(phAllergies);

                    // line space
                    Chunk cSpaceAllergies1 = new Chunk("\r");
                    dc.Add(cSpaceAllergies1);

                    iTextSharp.text.Table tblAllergies = new iTextSharp.text.Table(4);
                    tblAllergies.Width = 105;
                    tblAllergies.BorderWidth = 0;
                    //tblProviders.BorderColor = Color.BLACK;
                    tblAllergies.Padding = 3;
                    tblAllergies.Spacing = 1;
                    Cell cAllergies1 = new Cell(new Phrase("Allergy/Sensitivity Type", fLabelNormal));
                    cAllergies1.HorizontalAlignment = 1;
                    Cell cAllergies2 = new Cell(new Phrase("Reaction", fLabelNormal));
                    cAllergies2.HorizontalAlignment = 1;
                    Cell cAllergies3 = new Cell(new Phrase("Date Last Occurred", fLabelNormal));
                    cAllergies3.HorizontalAlignment = 1;
                    Cell cAllergies4 = new Cell(new Phrase("Treatment", fLabelNormal));
                    cAllergies4.HorizontalAlignment = 1;

                    tblAllergies.AddCell(cAllergies1);
                    tblAllergies.AddCell(cAllergies2);
                    tblAllergies.AddCell(cAllergies3);
                    tblAllergies.AddCell(cAllergies4);

                    uri = Utility.GetServiceUrl("allergies");
                    postData = new IgJObject();
                    postData.Add("PatientID", iPatient);
                    List<Allergy> objAll = Utility.PostRequest<Allergy>(uri, postData.ToString(Formatting.None));

                    //List<Allergy> objAll = null;
                    //uri = ConfigurationManager.AppSettings["serviceURL"] + "/allergies";
                    //postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + iPatient + "\"}";
                    //objAll = Utility.PostRequest<Allergy>(uri, postData);

                    if (objAll.Count > 0)
                    {
                        for (int i = 0; i < objAll.Count; i++)
                        {
                            tblAllergies.AddCell(new Phrase(Convert.ToString(objAll[i].AllergyType), fText));
                            tblAllergies.AddCell(new Phrase(Convert.ToString(objAll[i].Reaction), fText));
                            tblAllergies.AddCell(new Phrase(Convert.ToString(objAll[i].DateLastOccured), fText));
                            tblAllergies.AddCell(new Phrase(Convert.ToString(objAll[i].Treatment), fText));
                        }

                        dc.Add(tblAllergies);
                    }
                    else
                    {
                        dc.Add(tblAllergies);

                        Chunk cNAAllergies = new Chunk("No Records found for Allergies/Drug Sensitivities.", fTextItalic);
                        dc.Add(cNAAllergies);
                    }


                    #endregion

                    #region Self-Perceived Level of Pain

                    // line space
                    Chunk cSpacePain = new Chunk("\r");
                    Phrase phSpacePain = new Phrase();
                    phSpacePain.Add(cSpacePain);
                    dc.Add(phSpacePain);

                    // Pain
                    uri = Utility.GetServiceUrl("gethealthriskanswers");
                    postData = new IgJObject();
                    postData.Add("PatientID", iPatient);
                    postData.Add("ActionItem", "Pain");
                    List<PatientClinicalQuesRespInformation> objpain = Utility.PostRequest<PatientClinicalQuesRespInformation>(uri, postData.ToString(Formatting.None));

                    //List<PatientClinicalQuesRespInformation> objpain = null;
                    //uri = ConfigurationManager.AppSettings["serviceURL"] + "/gethealthriskanswers";
                    //postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + iPatient + "\",\"ActionItem\":\"Pain\"}";
                    //objpain = Utility.PostRequest<PatientClinicalQuesRespInformation>(uri, postData);



                    int ProviderCountPain = objpain.Count();


                    Chunk cLabelPain = new Chunk("Self-Perceived Level of Pain", fLabel);
                    Phrase phPain = new Phrase();
                    phPain.Add(cLabelPain);
                    Paragraph pPain = new Paragraph();
                    pPain.Add(phPain);
                    dc.Add(phPain);

                    // line space
                    Chunk cSpacePain1 = new Chunk("\r\r");
                    dc.Add(cSpacePain1);


                    PdfPTable tblSelfRecievedPain = new PdfPTable(2);
                    tblSelfRecievedPain.TotalWidth = 550f;
                    tblSelfRecievedPain.LockedWidth = true;

                    tblSelfRecievedPain.AddCell(GetCell("1.	How much bodily pain have you had during the past 4 weeks? ", ""));
                    if (ProviderCountPain > 0)
                    {
                        for (int i = 0; i < ProviderCountPain; i++)
                        {

                            if (objpain[i].QCode.ToString() == "painQ1")
                            {
                                tblSelfRecievedPain.AddCell(GetCell(objpain[i].FactorDesc.Trim(), ""));
                            }
                        }
                    }
                    else
                    {
                        //Chunk cPainA1 = new Chunk("", fText);
                        //Phrase phPainA1 = new Phrase();
                        //phPainA1.Add(cPainA1);
                        //dc.Add(phPainA1);
                        tblSelfRecievedPain.AddCell(GetCell(""));
                    }

                    tblSelfRecievedPain.AddCell(GetCell("2. During the past 4 weeks, how much did pain interfere with your normal work (including both work outside the home and housework)?", ""));
                    if (ProviderCountPain > 0)
                    {
                        for (int i = 0; i < ProviderCountPain; i++)
                        {
                            if (objpain[i].QCode.ToString() == "painQ2")
                            {
                                tblSelfRecievedPain.AddCell(GetCell(objpain[i].FactorDesc.Trim(), ""));
                            }
                        }
                    }
                    else
                    {
                        tblSelfRecievedPain.AddCell(GetCell("", ""));
                    }
                    dc.Add(tblSelfRecievedPain);

                    #endregion

                    #region Family Member History

                    // line space
                    Chunk cSpaceFamilyHistory = new Chunk("\r\r");
                    Phrase phSpaceFamilyHistory = new Phrase();
                    phSpaceFamilyHistory.Add(cSpaceFamilyHistory);
                    dc.Add(phSpaceFamilyHistory);

                    // Family Member History Heading
                    Chunk cLabelFamilyHistory = new Chunk("Family History", fLabel);
                    Phrase phFamilyHistory = new Phrase();
                    phFamilyHistory.Add(cLabelFamilyHistory);
                    Paragraph pFamilyHistory = new Paragraph();
                    pFamilyHistory.Add(phFamilyHistory);
                    dc.Add(phFamilyHistory);

                    // line space
                    Chunk cSpaceFamilyHistory1 = new Chunk("\r");
                    dc.Add(cSpaceFamilyHistory1);

                    PdfPTable tblFamilyHistory = new PdfPTable(4);
                    tblFamilyHistory.TotalWidth = 550f;
                    tblFamilyHistory.LockedWidth = true;

                    // 1st row
                    tblFamilyHistory.AddCell(GetBoldCell("Relation", 1, 1));
                    tblFamilyHistory.AddCell(GetBoldCell("Age", 1, 1));
                    tblFamilyHistory.AddCell(GetBoldCell("If deceased:", 1, 1));
                    tblFamilyHistory.AddCell(GetBoldCell("Health  Conditions", 1, 1));

                    // 2nd row
                    //tblFamilyHistory.AddCell(GetBoldCell("", 1, 1));
                    //tblFamilyHistory.AddCell(GetBoldCell("", 1, 1));
                    //tblFamilyHistory.AddCell(GetBoldCell("Age at death", 1, 1));
                    //tblFamilyHistory.AddCell(GetBoldCell("Cause of death", 1, 1));
                    //tblFamilyHistory.AddCell(GetBoldCell("", 1, 1));

                    uri = Utility.GetServiceUrl("familyhistory");
                    postData = new IgJObject();
                    postData.Add("PatientID", iPatient);
                    List<FamilyHistory> objfamhdr = Utility.PostRequest<FamilyHistory>(uri, postData.ToString(Formatting.None));

                    //List<FamilyHistory> objfamhdr = null;
                    //uri = ConfigurationManager.AppSettings["serviceURL"] + "/familyhistory";
                    //postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + iPatient + "\"}";
                    //objfamhdr = Utility.PostRequest<FamilyHistory>(uri, postData);

                    //      IList<FamilyHistoryInfo> iFamilyHistoryInfo = (new GHPFamilyHistoryBLL()).GetFamilyHistoryHdr(iPatient, Convert.ToInt32(Session["UID"]));
                    if (objfamhdr.Count > 0)
                    {
                        ViewState["FamilyHistoryId"] = objfamhdr[0].FamilyHistoryId.ToString();
                        string noofsiblings = objfamhdr[0].NoOfSibling.ToString();
                        string noofchildrens = objfamhdr[0].NoOfChildren.ToString();

                        //Mother

                        tblFamilyHistory.AddCell(GetCell("Mother", 1, 1));

                        uri = Utility.GetServiceUrl("familyhistorydtl");
                        postData = new IgJObject();
                        postData.Add("PatientID", iPatient);
                        postData.Add("FamilyHistoryId", Convert.ToInt32(ViewState["FamilyHistoryId"]));
                        postData.Add("MedicalHistoryOf", "Mother");
                        List<FamilyHistoryDetailInfo> objfammother = Utility.PostRequest<FamilyHistoryDetailInfo>(uri, postData.ToString(Formatting.None));

                        //List<FamilyHistoryDetailInfo> objfammother = null;
                        //uri = ConfigurationManager.AppSettings["serviceURL"] + "/familyhistorydtl";
                        //postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + iPatient + "\",
                        //    \"FamilyHistoryId\":\"" + Convert.ToInt32(ViewState["FamilyHistoryId"]) + "\",\"MedicalHistoryOf\":\"Mother\"}";
                        //objfammother = Utility.PostRequest<FamilyHistoryDetailInfo>(uri, postData);

                        if (objfammother.Count > 0)
                        {
                            if (objfammother[0].AgeOfLiving.ToString() == "0")
                            {
                                tblFamilyHistory.AddCell(GetCell("", 1, 1));
                            }
                            else
                            {
                                tblFamilyHistory.AddCell(GetCell(objfammother[0].AgeOfLiving.ToString(), 1, 1));
                            }
                            if (objfammother[0].LivingOrDeceased == "Living")
                            {
                                PdfPTable tblFamilyHistoryInner = new PdfPTable(4);
                                tblFamilyHistoryInner.TotalWidth = 550f;

                                tblFamilyHistoryInner.AddCell(GetCellImage(chkdImg));
                                tblFamilyHistoryInner.AddCell(GetInnerCell("Yes", "", 1, 1));
                                tblFamilyHistoryInner.AddCell(GetCellImage(ntChkdImg));
                                tblFamilyHistoryInner.AddCell(GetInnerCell("No", "", 1, 1));

                                tblFamilyHistory.AddCell(tblFamilyHistoryInner);
                            }
                            else
                            {
                                PdfPTable tblFamilyHistoryInner = new PdfPTable(4);
                                tblFamilyHistoryInner.TotalWidth = 550f;

                                tblFamilyHistoryInner.AddCell(GetCellImage(ntChkdImg));
                                tblFamilyHistoryInner.AddCell(GetInnerCell("Yes", "", 1, 1));
                                tblFamilyHistoryInner.AddCell(GetCellImage(chkdImg));
                                tblFamilyHistoryInner.AddCell(GetInnerCell("No", "", 1, 1));

                                tblFamilyHistory.AddCell(tblFamilyHistoryInner);
                            }
                            //if (iMotherHistoryDetail[0].AgeAtDeath.ToString() == "0")
                            //{
                            //    tblFamilyHistory.AddCell(GetCell("", 1, 1));
                            //}
                            //else
                            //{
                            //    tblFamilyHistory.AddCell(GetCell(iMotherHistoryDetail[0].AgeAtDeath.ToString(), 1, 1));
                            //}

                            //tblFamilyHistory.AddCell(GetCell(iMotherHistoryDetail[0].CauseOfDeath.ToString(), 1, 1));
                            tblFamilyHistory.AddCell(GetCell(objfammother[0].MajorHealthProblem.ToString(), 1, 1));
                        }
                        else
                        {
                            tblFamilyHistory.AddCell(GetCell("", 1, 1));
                            tblFamilyHistory.AddCell(GetCell("", 1, 1));
                            tblFamilyHistory.AddCell(GetCell("", 1, 1));
                            tblFamilyHistory.AddCell(GetCell("", 1, 1));
                        }

                        tblFamilyHistory.AddCell(GetCell("Father", 1, 1));

                        //Father
                        uri = Utility.GetServiceUrl("familyhistorydtl");
                        postData = new IgJObject();
                        postData.Add("PatientID", iPatient);
                        postData.Add("FamilyHistoryId", Convert.ToInt32(ViewState["FamilyHistoryId"]));
                        postData.Add("MedicalHistoryOf", "Father");
                        List<FamilyHistoryDetailInfo> objfather = Utility.PostRequest<FamilyHistoryDetailInfo>(uri, postData.ToString(Formatting.None));

                        
                        //List<FamilyHistoryDetailInfo> objfather = null;
                        //uri = ConfigurationManager.AppSettings["serviceURL"] + "/familyhistorydtl";
                        //postData = "{\"Pin\":\"" + PinValue + "\",\"PatientId\":\"" + iPatient + "\",
                        //    \"FamilyHistoryId\":\"" + Convert.ToInt32(ViewState["FamilyHistoryId"]) + "\",\"MedicalHistoryOf\":\"Father\"}";
                        //objfather = Utility.PostRequest<FamilyHistoryDetailInfo>(uri, postData);

                        if (objfather.Count > 0)
                        {
                            if (objfather[0].AgeOfLiving.ToString() == "0")
                            {
                                tblFamilyHistory.AddCell(GetCell("", 1, 1));
                            }
                            else
                            {
                                tblFamilyHistory.AddCell(GetCell(objfather[0].AgeOfLiving.ToString(), 1, 1));
                            }
                            if (objfather[0].LivingOrDeceased == "Living")
                            {
                                PdfPTable tblFamilyHistoryInner = new PdfPTable(4);
                                tblFamilyHistoryInner.TotalWidth = 550f;

                                tblFamilyHistoryInner.AddCell(GetCellImage(chkdImg));
                                tblFamilyHistoryInner.AddCell(GetInnerCell("Yes", "", 1, 1));
                                tblFamilyHistoryInner.AddCell(GetCellImage(ntChkdImg));
                                tblFamilyHistoryInner.AddCell(GetInnerCell("No", "", 1, 1));

                                tblFamilyHistory.AddCell(tblFamilyHistoryInner);
                            }
                            else
                            {
                                PdfPTable tblFamilyHistoryInner = new PdfPTable(4);
                                tblFamilyHistoryInner.TotalWidth = 550f;

                                tblFamilyHistoryInner.AddCell(GetCellImage(ntChkdImg));
                                tblFamilyHistoryInner.AddCell(GetInnerCell("Yes", "", 1, 1));
                                tblFamilyHistoryInner.AddCell(GetCellImage(chkdImg));
                                tblFamilyHistoryInner.AddCell(GetInnerCell("No", "", 1, 1));

                                tblFamilyHistory.AddCell(tblFamilyHistoryInner);
                            }
                            //if (iFatherHistoryDetail[0].AgeAtDeath.ToString() == "0")
                            //{
                            //    tblFamilyHistory.AddCell(GetCell("", 1, 1));
                            //}
                            //else
                            //{
                            //    tblFamilyHistory.AddCell(GetCell(iFatherHistoryDetail[0].AgeAtDeath.ToString(), 1, 1));
                            //}

                            //tblFamilyHistory.AddCell(GetCell(iFatherHistoryDetail[0].CauseOfDeath.ToString(), 1, 1));
                            tblFamilyHistory.AddCell(GetCell(objfather[0].MajorHealthProblem.ToString(), 1, 1));
                        }
                        else
                        {
                            tblFamilyHistory.AddCell(GetCell("", 1, 1));
                            tblFamilyHistory.AddCell(GetCell("", 1, 1));
                            tblFamilyHistory.AddCell(GetCell("", 1, 1));
                            tblFamilyHistory.AddCell(GetCell("", 1, 1));
                        }

                        //sibling


                        tblFamilyHistory.AddCell(GetCell("Do you have Siblings?", 2, 1));
                        tblFamilyHistory.AddCell(GetCell("Number of Siblings:  ", objfamhdr[0].NoOfSibling.ToString(), 2, 1));
                        tblFamilyHistory.AddCell(GetCell("Do you have Children?", 2, 1));
                        tblFamilyHistory.AddCell(GetCell("Number of Children:  ", objfamhdr[0].NoOfChildren.ToString(), 2, 1));
                        //if (iFamilyHistoryInfo[0].Siblings == "Yes")
                        //{
                        //    PdfPTable tblFamilyHistoryInner = new PdfPTable(5);
                        //    tblFamilyHistoryInner.TotalWidth = 550f;
                        //    tblFamilyHistoryInner.AddCell(GetCell("Do you have Siblings?"));
                        //    tblFamilyHistoryInner.AddCell(GetCellImage(chkdImg));
                        //    tblFamilyHistoryInner.AddCell(GetCell("Yes"));
                        //    tblFamilyHistoryInner.AddCell(GetCellImage(ntChkdImg));
                        //    tblFamilyHistoryInner.AddCell(GetCell("No"));

                        //    tblFamilyHistory.AddCell(tblFamilyHistoryInner);
                        //}
                        //else
                        //{
                        //    PdfPTable tblFamilyHistoryInner = new PdfPTable(5);
                        //    tblFamilyHistoryInner.TotalWidth = 550f;
                        //    tblFamilyHistoryInner.AddCell(GetCell("Do you have Siblings"));
                        //    tblFamilyHistoryInner.AddCell(GetCellImage(ntChkdImg));
                        //    tblFamilyHistoryInner.AddCell(GetCell("Yes"));
                        //    tblFamilyHistoryInner.AddCell(GetCellImage(chkdImg));
                        //    tblFamilyHistoryInner.AddCell(GetCell("No"));

                        //    tblFamilyHistory.AddCell(tblFamilyHistoryInner);
                        //}

                        //if (iFamilyHistoryInfo[0].Children == "Yes")
                        //{
                        //    PdfPTable tblFamilyHistoryInner = new PdfPTable(5);
                        //    tblFamilyHistoryInner.TotalWidth = 550f;
                        //    tblFamilyHistory.AddCell(GetCell("Do you have Children?"));
                        //    tblFamilyHistoryInner.AddCell(GetCellImage(chkdImg));
                        //    tblFamilyHistoryInner.AddCell(GetCell("Yes"));
                        //    tblFamilyHistoryInner.AddCell(GetCellImage(ntChkdImg));
                        //    tblFamilyHistoryInner.AddCell(GetCell("No"));

                        //    tblFamilyHistory.AddCell(tblFamilyHistoryInner);
                        //}
                        //else
                        //{
                        //    PdfPTable tblFamilyHistoryInner = new PdfPTable(5);
                        //    tblFamilyHistoryInner.TotalWidth = 550f;
                        //    tblFamilyHistory.AddCell(GetCell("Do you have Children?"));
                        //    tblFamilyHistoryInner.AddCell(GetCellImage(ntChkdImg));
                        //    tblFamilyHistoryInner.AddCell(GetCell("Yes"));
                        //    tblFamilyHistoryInner.AddCell(GetCellImage(chkdImg));
                        //    tblFamilyHistoryInner.AddCell(GetCell("No"));

                        //    tblFamilyHistory.AddCell(tblFamilyHistoryInner);
                        //}

                        //    FamilyHistoryDetail oSiblingFamilyHistoryDetail = new FamilyHistoryDetail();
                        //    oSiblingFamilyHistoryDetail.FamilyHistoryId = Convert.ToInt32(ViewState["FamilyHistoryId"]);//hdnFamilyHistory.Value
                        //    oSiblingFamilyHistoryDetail.MedicalHistoryOf = "Sibling";
                        //    oSiblingFamilyHistoryDetail.PatientId = iPatient;
                        //    oSiblingFamilyHistoryDetail.UserId = Convert.ToInt32(Session["UID"]);

                        //    IList<FamilyHistoryDetail> iSiblingHistoryDetail = (new GHPFamilyHistoryBLL()).GetFamilyHistoryDtl(oSiblingFamilyHistoryDetail);
                        //    if (iSiblingHistoryDetail.Count > 0)
                        //    {
                        //        for (int i = 0; i < iSiblingHistoryDetail.Count; i++)
                        //        {
                        //            int j = i + 1;
                        //            tblFamilyHistory.AddCell(GetCell("Sibling#" + j, 1, 1));

                        //            if (iSiblingHistoryDetail[i].AgeOfLiving.ToString() == "0")
                        //            {
                        //                tblFamilyHistory.AddCell(GetCell("", 1, 1));
                        //            }
                        //            else
                        //            {
                        //                tblFamilyHistory.AddCell(GetCell(iSiblingHistoryDetail[i].AgeOfLiving.ToString(), 1, 1));
                        //            }
                        //            if (iSiblingHistoryDetail[i].AgeAtDeath.ToString() == "0")
                        //            {
                        //                tblFamilyHistory.AddCell(GetCell("", 1, 1));
                        //            }
                        //            else
                        //            {
                        //                tblFamilyHistory.AddCell(GetCell(iSiblingHistoryDetail[i].AgeAtDeath.ToString(), 1, 1));
                        //            }

                        //            tblFamilyHistory.AddCell(GetCell(iSiblingHistoryDetail[i].CauseOfDeath.ToString(), 1, 1));
                        //            tblFamilyHistory.AddCell(GetCell(iSiblingHistoryDetail[i].MajorHealthProblem.ToString(), 1, 1));
                        //        }
                        //    }

                        //    //children
                        //    FamilyHistoryDetail oChildFamilyHistoryDetail = new FamilyHistoryDetail();
                        //    oChildFamilyHistoryDetail.FamilyHistoryId = Convert.ToInt32(ViewState["FamilyHistoryId"]);
                        //    oChildFamilyHistoryDetail.PatientId = iPatient;
                        //    oChildFamilyHistoryDetail.MedicalHistoryOf = "Children";
                        //    oChildFamilyHistoryDetail.UserId = Convert.ToInt32(Session["UID"]);

                        //    IList<FamilyHistoryDetail> iChildHistoryDetail = (new GHPFamilyHistoryBLL()).GetFamilyHistoryDtl(oChildFamilyHistoryDetail);
                        //    if (iChildHistoryDetail.Count > 0)
                        //    {
                        //        for (int i = 0; i < iChildHistoryDetail.Count; i++)
                        //        {
                        //            int j = i + 1;
                        //            tblFamilyHistory.AddCell(GetCell("Child#" + j, 1, 1));

                        //            if (iChildHistoryDetail[i].AgeOfLiving.ToString() == "0")
                        //            {
                        //                tblFamilyHistory.AddCell(GetCell("", 1, 1));
                        //            }
                        //            else
                        //            {
                        //                tblFamilyHistory.AddCell(GetCell(iChildHistoryDetail[i].AgeOfLiving.ToString(), 1, 1));
                        //            }
                        //            if (iChildHistoryDetail[i].AgeAtDeath.ToString() == "0")
                        //            {
                        //                tblFamilyHistory.AddCell(GetCell("", 1, 1));
                        //            }
                        //            else
                        //            {
                        //                tblFamilyHistory.AddCell(GetCell(iChildHistoryDetail[i].AgeAtDeath.ToString(), 1, 1));
                        //            }

                        //            tblFamilyHistory.AddCell(GetCell(iChildHistoryDetail[i].CauseOfDeath.ToString(), 1, 1));
                        //            tblFamilyHistory.AddCell(GetCell(iChildHistoryDetail[i].MajorHealthProblem.ToString(), 1, 1));
                        //        }
                        //    }


                        //}

                    }


                    dc.Add(tblFamilyHistory);

                    #endregion

                    #region Lifestyle

                    // line space
                    Chunk cSpaceLifestyles = new Chunk("\r");
                    Phrase phSpaceLifestyles = new Phrase();
                    phSpaceLifestyles.Add(cSpaceLifestyles);
                    dc.Add(phSpaceLifestyles);

                    // Lifestyle Heading
                    Chunk cLabelLifestyles = new Chunk("Social History", fLabel);
                    Phrase phLifestyles = new Phrase();
                    phLifestyles.Add(cLabelLifestyles);
                    Paragraph pLifestyles = new Paragraph();
                    pLifestyles.Add(phLifestyles);
                    dc.Add(phLifestyles);

                    // line space
                    Chunk cSpaceLifestyles1 = new Chunk("\r");
                    dc.Add(cSpaceLifestyles1);

                    uri = Utility.GetServiceUrl("lifestyle");
                    postData = new IgJObject();
                    postData.Add("PatientID", iPatient);
                    List<LifeStyle> objlife = Utility.PostRequest<LifeStyle>(uri, postData.ToString(Formatting.None));

                    //List<LifeStyle> objlife = null;
                    //uri = ConfigurationManager.AppSettings["serviceURL"] + "/lifestyle";
                    //postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + iPatient + "\"}";
                    //objlife = Utility.PostRequest<LifeStyle>(uri, postData);

                    if (objlife.Count > 0)
                    {
                        PdfPTable tblSocialHistory = new PdfPTable(2);
                        tblSocialHistory.TotalWidth = 550f;
                        tblSocialHistory.LockedWidth = true;
                        tblSocialHistory.AddCell(GetCell("Lifestyle ", "", 2, 1));

                        PdfPTable tblLifestyleInfo = new PdfPTable(3);
                        tblLifestyleInfo.TotalWidth = 550f;
                        tblLifestyleInfo.LockedWidth = true;


                        //1st row
                        if (objlife.ElementAtOrDefault(0).Alcoholic == "Yes")
                        {
                            tblSocialHistory.AddCell(GetCell("Do you drink alcoholic beverages? "));
                            PdfPTable innerTable = new PdfPTable(4);
                            innerTable.TotalWidth = 10f;

                            innerTable.AddCell(GetCellImage(chkdImg));
                            innerTable.AddCell(GetInnerCell("Yes", "", 1, 1));
                            innerTable.AddCell(GetCellImage(ntChkdImg));
                            innerTable.AddCell(GetInnerCell("No", "", 1, 1));

                            tblSocialHistory.AddCell(innerTable);
                            //tblLifestyleInfo.AddCell(GetCell("Drink(s) Per Week: ", iGHPLifestyleInfo.ElementAtOrDefault(0).DrinksPerWeek.ToString(), 1, 1));
                            //tblLifestyleInfo.AddCell(GetCell("Number of Years: ", iGHPLifestyleInfo.ElementAtOrDefault(0).HowlongYearsDrinking.ToString(), 1, 1));

                        }
                        else
                        {
                            tblSocialHistory.AddCell(GetCell("Do you drink alcoholic beverages? "));
                            PdfPTable innerTable = new PdfPTable(4);
                            innerTable.TotalWidth = 10f;

                            innerTable.AddCell(GetCellImage(ntChkdImg));
                            innerTable.AddCell(GetInnerCell("Yes", "", 1, 1));
                            innerTable.AddCell(GetCellImage(chkdImg));
                            innerTable.AddCell(GetInnerCell("No", "", 1, 1));

                            tblSocialHistory.AddCell(innerTable);

                            //tblLifestyleInfo.AddCell(GetCell("Drink(s) Per Week: ", iGHPLifestyleInfo.ElementAtOrDefault(0).DrinksPerWeek.ToString(), 1, 1));
                            //tblLifestyleInfo.AddCell(GetCell("Number of Years: ", iGHPLifestyleInfo.ElementAtOrDefault(0).HowlongYearsDrinking.ToString(), 1, 1));
                        }

                        //2nd row
                        if (objlife.ElementAtOrDefault(0).Smoke == "Yes")
                        {
                            tblSocialHistory.AddCell(GetCell("Do you smoke? "));
                            PdfPTable innerTable = new PdfPTable(4);
                            innerTable.TotalWidth = 10f;

                            innerTable.AddCell(GetCellImage(chkdImg));
                            innerTable.AddCell(GetInnerCell("Yes", "", 1, 1));
                            innerTable.AddCell(GetCellImage(ntChkdImg));
                            innerTable.AddCell(GetInnerCell("No", "", 1, 1));

                            tblSocialHistory.AddCell(innerTable);
                            //PdfPTable innerTable = new PdfPTable(2);
                            //innerTable.TotalWidth = 10f;

                            //innerTable.AddCell(GetCellImage(chkdImg));
                            //innerTable.AddCell(GetCellText("Smoking", "", 1, 1));

                            //tblLifestyleInfo.AddCell(innerTable);

                            //tblLifestyleInfo.AddCell(GetCell("Pack(s) Per Day: ", iGHPLifestyleInfo.ElementAtOrDefault(0).PacksPerDay.ToString(), 1, 1));
                            //tblLifestyleInfo.AddCell(GetCell("Number of Years: ", iGHPLifestyleInfo.ElementAtOrDefault(0).HowlongYearsSmoking.ToString(), 1, 1));

                        }
                        else
                        {
                            tblSocialHistory.AddCell(GetCell("Do you smoke? "));
                            PdfPTable innerTable = new PdfPTable(4);
                            innerTable.TotalWidth = 10f;

                            innerTable.AddCell(GetCellImage(ntChkdImg));
                            innerTable.AddCell(GetInnerCell("Yes", "", 1, 1));
                            innerTable.AddCell(GetCellImage(chkdImg));
                            innerTable.AddCell(GetInnerCell("No", "", 1, 1));

                            tblSocialHistory.AddCell(innerTable);
                            //PdfPTable innerTable = new PdfPTable(2);
                            //innerTable.TotalWidth = 10f;

                            //innerTable.AddCell(GetCellImage(ntChkdImg));
                            //innerTable.AddCell(GetCellText("Smoking", "", 1, 1));

                            //tblLifestyleInfo.AddCell(innerTable);

                            //tblLifestyleInfo.AddCell(GetCell("Pack(s) Per Day: ", iGHPLifestyleInfo.ElementAtOrDefault(0).PacksPerDay.ToString(), 1, 1));
                            //tblLifestyleInfo.AddCell(GetCell("Number of Years: ", iGHPLifestyleInfo.ElementAtOrDefault(0).HowlongYearsSmoking.ToString(), 1, 1));
                        }

                        tblSocialHistory.AddCell(GetCell("How many pack(s) per day? "));
                        tblSocialHistory.AddCell(GetCell(objlife.ElementAtOrDefault(0).PacksPerDay.ToString()));
                        tblSocialHistory.AddCell(GetCell("How long (in years) have you been smoking? "));
                        tblSocialHistory.AddCell(GetCell(objlife.ElementAtOrDefault(0).HowlongYearsSmoking.ToString()));

                        //3rd row
                        if (objlife.ElementAtOrDefault(0).Exercise == "Yes")
                        {
                            tblSocialHistory.AddCell(GetCell("Do you exercise? "));
                            PdfPTable innerTable = new PdfPTable(4);
                            innerTable.TotalWidth = 10f;

                            innerTable.AddCell(GetCellImage(chkdImg));
                            innerTable.AddCell(GetInnerCell("Yes", "", 1, 1));
                            innerTable.AddCell(GetCellImage(ntChkdImg));
                            innerTable.AddCell(GetInnerCell("No", "", 1, 1));

                            tblSocialHistory.AddCell(innerTable);

                            //if (iGHPLifestyleInfo.ElementAtOrDefault(0).ExerciseDaysPerWeek.ToString() != "Select")
                            //{
                            //    tblLifestyleInfo.AddCell(GetCell("Days Per Week: ", iGHPLifestyleInfo.ElementAtOrDefault(0).ExerciseDaysPerWeek.ToString(), 2, 1));
                            //}
                            //else
                            //{
                            //    tblLifestyleInfo.AddCell(GetCell("Days Per Week: ", "", 2, 1));
                            //}

                        }
                        else
                        {
                            tblSocialHistory.AddCell(GetCell("Do you exercise? "));
                            PdfPTable innerTable = new PdfPTable(4);
                            innerTable.TotalWidth = 10f;

                            innerTable.AddCell(GetCellImage(ntChkdImg));
                            innerTable.AddCell(GetInnerCell("Yes", "", 1, 1));
                            innerTable.AddCell(GetCellImage(chkdImg));
                            innerTable.AddCell(GetInnerCell("No", "", 1, 1));

                            tblSocialHistory.AddCell(innerTable);

                            //PdfPTable innerTable = new PdfPTable(2);
                            //innerTable.TotalWidth = 10f;

                            //innerTable.AddCell(GetCellImage(ntChkdImg));
                            //innerTable.AddCell(GetCellText("Exercise", "", 1, 1));

                            //tblLifestyleInfo.AddCell(innerTable);

                            //if (iGHPLifestyleInfo.ElementAtOrDefault(0).ExerciseDaysPerWeek.ToString() != "Select")
                            //{
                            //    tblLifestyleInfo.AddCell(GetCell("Days Per Week: ", iGHPLifestyleInfo.ElementAtOrDefault(0).ExerciseDaysPerWeek.ToString(), 2, 1));
                            //}
                            //else
                            //{
                            //    tblLifestyleInfo.AddCell(GetCell("Days Per Week: ", "", 2, 1));
                            //}

                        }

                        //oPatientInfoID.Lastname.ToString()
                        tblSocialHistory.AddCell(GetCell("BMI: "));
                        if (objColl[0].BMI != null)
                        {
                            tblSocialHistory.AddCell(GetCell(objColl[0].BMI.ToString()));
                        }
                        else
                        {
                            tblSocialHistory.AddCell(GetCell(""));
                        }

                        dc.Add(tblSocialHistory);
                    }

                    #endregion

                    #region Social Supports

                    // line space
                    Chunk cSpaceSocialSupports = new Chunk("\r");
                    Phrase phSpaceSocialSupports = new Phrase();
                    phSpaceSocialSupports.Add(cSpaceSocialSupports);
                    dc.Add(phSpaceSocialSupports);

                    uri = Utility.GetServiceUrl("gethealthriskanswers");
                    postData = new IgJObject();
                    postData.Add("PatientID", iPatient);
                    postData.Add("ActionItem", "SocialSupports");
                    List<PatientClinicalQuesRespInformation> objSocial = Utility.PostRequest<PatientClinicalQuesRespInformation>(uri, postData.ToString(Formatting.None));

                    //List<PatientClinicalQuesRespInformation> objSocial = null;
                    //uri = ConfigurationManager.AppSettings["serviceURL"] + "/gethealthriskanswers";
                    //postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + iPatient + "\",\"ActionItem\":\"SocialSupports\"}";
                    //objSocial = Utility.PostRequest<PatientClinicalQuesRespInformation>(uri, postData);

                    int ProviderCountSocialSupports = objSocial.Count();

                    PdfPTable tblSocialSupport = new PdfPTable(2);
                    tblSocialSupport.TotalWidth = 550f;
                    tblSocialSupport.LockedWidth = true;
                    tblSocialSupport.AddCell(GetCell("Social Support ", "", 2, 1));

                    if (ProviderCountSocialSupports > 0)
                    {
                        for (int i = 0; i < ProviderCountSocialSupports; i++)
                        {
                            if (objSocial.ElementAtOrDefault(i).QCode.ToString() == "socialsupportsQ1")
                            {
                                tblSocialSupport.AddCell(GetCell("1. With whom do you live? "));
                                tblSocialSupport.AddCell(GetCell(objSocial.ElementAtOrDefault(i).FactorDesc.Trim()));
                                //Chunk cSocialSupportsA1 = new Chunk(iPatientInfoSocialSupports.ElementAtOrDefault(i).FactorDesc.Trim(), fText);
                                //Phrase phSocialSupportsA1 = new Phrase();
                                //phSocialSupportsA1.Add(cSocialSupportsA1);
                                //dc.Add(phSocialSupportsA1);
                            }
                        }
                    }
                    else
                    {
                        tblSocialSupport.AddCell(GetCell("1. With whom do you live? "));
                        tblSocialSupport.AddCell(GetCell(""));
                        //Chunk cSocialSupportsA1 = new Chunk("", fText);
                        //Phrase phSocialSupportsA1 = new Phrase();
                        //phSocialSupportsA1.Add(cSocialSupportsA1);
                        //dc.Add(phSocialSupportsA1);
                    }


                    if (ProviderCountSocialSupports > 0)
                    {
                        for (int i = 0; i < ProviderCountSocialSupports; i++)
                        {
                            if (objSocial.ElementAtOrDefault(i).QCode.ToString() == "socialsupportsQ2")
                            {
                                tblSocialSupport.AddCell(GetCell("2. How many relatives or friends do you see or hear from at least once a month? "));
                                tblSocialSupport.AddCell(GetCell(objSocial.ElementAtOrDefault(i).FactorDesc.Trim()));
                            }
                        }
                    }
                    else
                    {
                        tblSocialSupport.AddCell(GetCell("2. How many relatives or friends do you see or hear from at least once a month? "));
                        tblSocialSupport.AddCell(GetCell(""));
                    }

                    if (ProviderCountSocialSupports > 0)
                    {
                        for (int i = 0; i < ProviderCountSocialSupports; i++)
                        {
                            if (objSocial.ElementAtOrDefault(i).QCode.ToString() == "socialsupportsQ3")
                            {
                                tblSocialSupport.AddCell(GetCell("3. How many relatives or friends do you feel at ease with that you can talk about private matters, including your health?  "));
                                tblSocialSupport.AddCell(GetCell(objSocial.ElementAtOrDefault(i).FactorDesc.Trim()));
                            }
                        }
                    }
                    else
                    {
                        tblSocialSupport.AddCell(GetCell("3. How many relatives or friends do you feel at ease with that you can talk about private matters, including your health?  "));
                        tblSocialSupport.AddCell(GetCell(""));
                    }


                    if (ProviderCountSocialSupports > 0)
                    {
                        for (int i = 0; i < ProviderCountSocialSupports; i++)
                        {
                            if (objSocial.ElementAtOrDefault(i).QCode.ToString() == "socialsupportsQ4")
                            {
                                tblSocialSupport.AddCell(GetCell("4. How many relatives or friends do you feel close to such that you could call on them for help, including assisting you in health needs?"));
                                tblSocialSupport.AddCell(GetCell(objSocial.ElementAtOrDefault(i).FactorDesc.Trim()));
                            }
                        }
                    }
                    else
                    {
                        tblSocialSupport.AddCell(GetCell("4. How many relatives or friends do you feel close to such that you could call on them for help, including assisting you in health needs?"));
                        tblSocialSupport.AddCell(GetCell(""));
                    }

                    if (ProviderCountSocialSupports > 0)
                    {
                        for (int i = 0; i < ProviderCountSocialSupports; i++)
                        {
                            if (objSocial.ElementAtOrDefault(i).QCode.ToString() == "socialsupportsQ5")
                            {
                                tblSocialSupport.AddCell(GetCell("5. In general, how strong are your social ties "));
                                tblSocialSupport.AddCell(GetCell(objSocial.ElementAtOrDefault(i).FactorDesc.Trim()));
                            }
                        }
                    }
                    else
                    {
                        tblSocialSupport.AddCell(GetCell("5. In general, how strong are your social ties "));
                        tblSocialSupport.AddCell(GetCell(""));
                    }

                    dc.Add(tblSocialSupport);

                    #endregion


                    #region Self-Perceived Quality of Life
                    // line space
                    Chunk cSpaceQualityofLife = new Chunk("\r\r");
                    Phrase phSpaceQualityofLife = new Phrase();
                    phSpaceQualityofLife.Add(cSpaceQualityofLife);
                    dc.Add(phSpaceQualityofLife);

                    uri = Utility.GetServiceUrl("gethealthriskanswers");
                    postData = new IgJObject();
                    postData.Add("PatientID", iPatient);
                    postData.Add("ActionItem", "QualityOfLife");
                    List<PatientClinicalQuesRespInformation> objqualityoflife = Utility.PostRequest<PatientClinicalQuesRespInformation>(uri, postData.ToString(Formatting.None));

                    //List<PatientClinicalQuesRespInformation> objqualityoflife = null;
                    //uri = ConfigurationManager.AppSettings["serviceURL"] + "/gethealthriskanswers";
                    //postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + iPatient + "\",\"ActionItem\":\"QualityOfLife\"}";
                    //objqualityoflife = Utility.PostRequest<PatientClinicalQuesRespInformation>(uri, postData);



                    int ProviderCountQualityOfLife = objqualityoflife.Count();
                    PdfPTable tblQualityOfLife = new PdfPTable(2);
                    tblQualityOfLife.TotalWidth = 550f;
                    tblQualityOfLife.LockedWidth = true;
                    tblQualityOfLife.AddCell(GetCell("Self-Perceived Quality of Life ", "", 2, 1));
                    if (ProviderCountQualityOfLife > 0)
                    {
                        for (int i = 0; i < ProviderCountQualityOfLife; i++)
                        {
                            if (objqualityoflife.ElementAtOrDefault(i).QCode.ToString() == "qualityoflifeQ1")
                            {
                                tblQualityOfLife.AddCell(GetCell("1. In general, how satisfied are you with your life (include personal and professional aspects)?"));
                                tblQualityOfLife.AddCell(GetCell(objqualityoflife.ElementAtOrDefault(i).FactorDesc.Trim()));
                            }
                        }
                    }
                    else
                    {
                        tblQualityOfLife.AddCell(GetCell("1. In general, how satisfied are you with your life (include personal and professional aspects)?"));
                        tblQualityOfLife.AddCell(GetCell(""));
                    }
                    if (ProviderCountQualityOfLife > 0)
                    {
                        for (int i = 0; i < ProviderCountQualityOfLife; i++)
                        {
                            if (objqualityoflife.ElementAtOrDefault(i).QCode.ToString() == "qualityoflifeQ2")
                            {
                                tblQualityOfLife.AddCell(GetCell("2. All in all, how much happiness would you say you find in life today"));
                                tblQualityOfLife.AddCell(GetCell(objqualityoflife.ElementAtOrDefault(i).FactorDesc.Trim()));
                            }
                        }
                    }
                    else
                    {
                        tblQualityOfLife.AddCell(GetCell("2. All in all, how much happiness would you say you find in life today"));
                        tblQualityOfLife.AddCell(GetCell(""));
                    }
                    tblQualityOfLife.AddCell(GetCell("3. During the past 4 weeks, have you:", 2, 1));
                    //tblQualityOfLife.AddCell(GetCell("", ""));
                    if (ProviderCountQualityOfLife > 0)
                    {
                        for (int i = 0; i < ProviderCountQualityOfLife; i++)
                        {
                            if (objqualityoflife.ElementAtOrDefault(i).QCode.ToString() == "qualityoflifeQ3")
                            {
                                tblQualityOfLife.AddCell(GetCell("a. Seemed to exhibit little interest or please in doing things?"));
                                //1st inner row
                                if (objqualityoflife.ElementAtOrDefault(i).FactorDesc.ToString() == "Yes")
                                {
                                    PdfPTable tblQualityOfLifeInner = new PdfPTable(4);
                                    tblQualityOfLife.TotalWidth = 550f;
                                    tblQualityOfLifeInner.AddCell(GetCellImage(chkdImg));
                                    tblQualityOfLifeInner.AddCell(GetInnerCell("Yes", "", 1, 1));
                                    tblQualityOfLifeInner.AddCell(GetCellImage(ntChkdImg));
                                    tblQualityOfLifeInner.AddCell(GetInnerCell("No", "", 1, 1));
                                    tblQualityOfLife.AddCell(tblQualityOfLifeInner);
                                }
                                else if (objqualityoflife.ElementAtOrDefault(i).FactorDesc.ToString() == "No")
                                {
                                    PdfPTable innerTable = new PdfPTable(4);
                                    innerTable.TotalWidth = 10f;
                                    innerTable.AddCell(GetCellImage(ntChkdImg));
                                    innerTable.AddCell(GetInnerCell("Yes", "", 1, 1));
                                    innerTable.AddCell(GetCellImage(chkdImg));
                                    innerTable.AddCell(GetInnerCell("No", "", 1, 1));
                                    tblQualityOfLife.AddCell(innerTable);
                                }
                            }
                            if (objqualityoflife.ElementAtOrDefault(i).QCode.ToString() == "qualityoflifeQ4")
                            {
                                tblQualityOfLife.AddCell(GetCell("b. Felt down, depressed, hopeless?"));
                                //2nd inner row
                                if (objqualityoflife.ElementAtOrDefault(i).FactorDesc.ToString() == "Yes")
                                {
                                    PdfPTable innerTable = new PdfPTable(4);
                                    innerTable.TotalWidth = 10f;
                                    innerTable.AddCell(GetCellImage(chkdImg));
                                    innerTable.AddCell(GetInnerCell("Yes", "", 1, 1));
                                    innerTable.AddCell(GetCellImage(ntChkdImg));
                                    innerTable.AddCell(GetInnerCell("No", "", 1, 1));
                                    tblQualityOfLife.AddCell(innerTable);
                                }
                                else if (objqualityoflife.ElementAtOrDefault(i).FactorDesc.ToString() == "No")
                                {
                                    PdfPTable innerTable = new PdfPTable(4);
                                    innerTable.TotalWidth = 10f;
                                    innerTable.AddCell(GetCellImage(ntChkdImg));
                                    innerTable.AddCell(GetInnerCell("Yes", "", 1, 1));
                                    innerTable.AddCell(GetCellImage(chkdImg));
                                    innerTable.AddCell(GetInnerCell("No", "", 1, 1));
                                    tblQualityOfLife.AddCell(innerTable);
                                }
                            }
                            if (objqualityoflife.ElementAtOrDefault(i).QCode.ToString() == "qualityoflifeQ5")
                            {
                                tblQualityOfLife.AddCell(GetCell("c. Felt nervous, anxious or on edge"));
                                //3rd inner row
                                if (objqualityoflife.ElementAtOrDefault(i).FactorDesc.ToString() == "Yes")
                                {
                                    PdfPTable innerTable = new PdfPTable(4);
                                    innerTable.TotalWidth = 10f;
                                    innerTable.AddCell(GetCellImage(chkdImg));
                                    innerTable.AddCell(GetInnerCell("Yes", "", 1, 1));
                                    innerTable.AddCell(GetCellImage(ntChkdImg));
                                    innerTable.AddCell(GetInnerCell("No", "", 1, 1));
                                    tblQualityOfLife.AddCell(innerTable);
                                }
                                else if (objqualityoflife.ElementAtOrDefault(i).FactorDesc.ToString() == "No")
                                {
                                    PdfPTable innerTable = new PdfPTable(4);
                                    innerTable.TotalWidth = 10f;
                                    innerTable.AddCell(GetCellImage(ntChkdImg));
                                    innerTable.AddCell(GetInnerCell("Yes", "", 1, 1));
                                    innerTable.AddCell(GetCellImage(chkdImg));
                                    innerTable.AddCell(GetInnerCell("No", "", 1, 1));
                                    tblQualityOfLife.AddCell(innerTable);
                                }
                            }
                            if (objqualityoflife.ElementAtOrDefault(i).QCode.ToString() == "qualityoflifeQ6")
                            {
                                tblQualityOfLife.AddCell(GetCell("d. Found yourself worrying about a lot of different things?"));
                                //4th inner row
                                if (objqualityoflife.ElementAtOrDefault(i).FactorDesc.ToString() == "Yes")
                                {
                                    PdfPTable innerTable = new PdfPTable(4);
                                    innerTable.TotalWidth = 10f;
                                    innerTable.AddCell(GetCellImage(chkdImg));
                                    innerTable.AddCell(GetInnerCell("Yes", "", 1, 1));
                                    innerTable.AddCell(GetCellImage(ntChkdImg));
                                    innerTable.AddCell(GetInnerCell("No", "", 1, 1));
                                    tblQualityOfLife.AddCell(innerTable);
                                }
                                else if (objqualityoflife.ElementAtOrDefault(i).FactorDesc.ToString() == "No")
                                {
                                    PdfPTable innerTable = new PdfPTable(4);
                                    innerTable.TotalWidth = 10f;
                                    innerTable.AddCell(GetCellImage(ntChkdImg));
                                    innerTable.AddCell(GetInnerCell("Yes", "", 1, 1));
                                    innerTable.AddCell(GetCellImage(chkdImg));
                                    innerTable.AddCell(GetInnerCell("No", "", 1, 1));
                                    tblQualityOfLife.AddCell(innerTable);
                                }
                            }
                            if (objqualityoflife.ElementAtOrDefault(i).QCode.ToString() == "qualityoflifeQ7")
                            {
                                tblQualityOfLife.AddCell(GetCell("e. Had an anxiety attack (sudden feeling of fear or panic)?"));
                                //5th inner row
                                if (objqualityoflife.ElementAtOrDefault(i).FactorDesc.ToString() == "Yes")
                                {
                                    PdfPTable innerTable = new PdfPTable(4);
                                    innerTable.TotalWidth = 10f;
                                    innerTable.AddCell(GetCellImage(chkdImg));
                                    innerTable.AddCell(GetInnerCell("Yes", "", 1, 1));
                                    innerTable.AddCell(GetCellImage(ntChkdImg));
                                    innerTable.AddCell(GetInnerCell("No", "", 1, 1));
                                    tblQualityOfLife.AddCell(innerTable);
                                }
                                else if (objqualityoflife.ElementAtOrDefault(i).FactorDesc.ToString() == "No")
                                {
                                    PdfPTable innerTable = new PdfPTable(4);
                                    innerTable.TotalWidth = 10f;
                                    innerTable.AddCell(GetCellImage(ntChkdImg));
                                    innerTable.AddCell(GetInnerCell("Yes", "", 1, 1));
                                    innerTable.AddCell(GetCellImage(chkdImg));
                                    innerTable.AddCell(GetInnerCell("No", "", 1, 1));
                                    tblQualityOfLife.AddCell(innerTable);
                                }
                            }
                        }
                        dc.Add(tblQualityOfLife);
                    }
                    else
                    {
                        tblQualityOfLife.AddCell(GetCell("a. Seemed to exhibit little interest or please in doing things?"));
                        PdfPTable innerTable = new PdfPTable(4);
                        innerTable.TotalWidth = 10f;
                        innerTable.AddCell(GetCellImage(ntChkdImg));
                        innerTable.AddCell(GetInnerCell("Yes", "", 1, 1));
                        innerTable.AddCell(GetCellImage(ntChkdImg));
                        innerTable.AddCell(GetInnerCell("No", "", 1, 1));
                        tblQualityOfLife.AddCell(innerTable);
                        //2nd inner row
                        tblQualityOfLife.AddCell(GetCell("b. Felt down, depressed, hopeless?"));
                        PdfPTable innerTable2 = new PdfPTable(4);
                        innerTable2.TotalWidth = 10f;
                        innerTable2.AddCell(GetCellImage(ntChkdImg));
                        innerTable2.AddCell(GetInnerCell("Yes", "", 1, 1));
                        innerTable2.AddCell(GetCellImage(ntChkdImg));
                        innerTable2.AddCell(GetInnerCell("No", "", 1, 1));
                        tblQualityOfLife.AddCell(innerTable2);
                        //3rd inner row
                        tblQualityOfLife.AddCell(GetCell("c. Felt nervous, anxious or on edge"));
                        PdfPTable innerTable3 = new PdfPTable(4);
                        innerTable3.TotalWidth = 10f;
                        innerTable3.AddCell(GetCellImage(ntChkdImg));
                        innerTable3.AddCell(GetInnerCell("Yes", "", 1, 1));
                        innerTable3.AddCell(GetCellImage(ntChkdImg));
                        innerTable3.AddCell(GetInnerCell("No", "", 1, 1));
                        tblQualityOfLife.AddCell(innerTable3);
                        //4th inner row
                        tblQualityOfLife.AddCell(GetCell("d. Found yourself worrying about a lot of different things?"));
                        PdfPTable innerTable4 = new PdfPTable(4);
                        innerTable4.TotalWidth = 10f;
                        innerTable4.AddCell(GetCellImage(ntChkdImg));
                        innerTable4.AddCell(GetInnerCell("Yes", "", 1, 1));
                        innerTable4.AddCell(GetCellImage(ntChkdImg));
                        innerTable4.AddCell(GetInnerCell("No", "", 1, 1));
                        tblQualityOfLife.AddCell(innerTable4);
                        //5th inner row
                        tblQualityOfLife.AddCell(GetCell("e. Had an anxiety attack (sudden feeling of fear or panic)?"));
                        PdfPTable innerTable5 = new PdfPTable(4);
                        innerTable5.TotalWidth = 10f;
                        innerTable5.AddCell(GetCellImage(ntChkdImg));
                        innerTable5.AddCell(GetInnerCell("Yes", "", 1, 1));
                        innerTable5.AddCell(GetCellImage(ntChkdImg));
                        innerTable5.AddCell(GetInnerCell("No", "", 1, 1));
                        tblQualityOfLife.AddCell(innerTable5);
                        dc.Add(tblQualityOfLife);
                    }
                    #endregion

                    #region Problems with Activities of Daily Living

                    // line space
                    Chunk cSpaceActivities = new Chunk("\r");
                    Phrase phSpaceActivities = new Phrase();
                    phSpaceActivities.Add(cSpaceActivities);
                    dc.Add(phSpaceActivities);

                    uri = Utility.GetServiceUrl("activities");
                    postData = new IgJObject();
                    postData.Add("PatientID", iPatient);
                    List<Activities> iGHPActivitiesInfo = Utility.PostRequest<Activities>(uri, postData.ToString(Formatting.None));

                    //List<Activities> iGHPActivitiesInfo = null;
                    //uri = ConfigurationManager.AppSettings["serviceURL"] + "/activities";
                    //postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + iPatient + "\"}";
                    //iGHPActivitiesInfo = Utility.PostRequest<Activities>(uri, postData);


                    int ProviderCountActivities = iGHPActivitiesInfo.Count();

                    PdfPTable tblDailyActivities = new PdfPTable(2);
                    tblDailyActivities.TotalWidth = 550f;
                    tblDailyActivities.LockedWidth = true;
                    tblDailyActivities.AddCell(GetCell("Problems with Activities of Daily Living", "", 2, 1));

                    if (ProviderCountActivities > 0)
                    {
                        tblDailyActivities.AddCell(GetCell("Bathing"));
                        if (iGHPActivitiesInfo.ElementAtOrDefault(0).Bathing.ToString() == "Select")
                        {
                            tblDailyActivities.AddCell(GetCell(""));
                        }
                        else if (iGHPActivitiesInfo.ElementAtOrDefault(0).Bathing.ToString() == "No Help Needed")
                        {
                            tblDailyActivities.AddCell(GetCell("No Help Needed"));
                        }
                        else if (iGHPActivitiesInfo.ElementAtOrDefault(0).Bathing.ToString() == "Some Help Needed")
                        {
                            tblDailyActivities.AddCell(GetCell("Some Help Needed"));
                        }
                        else if (iGHPActivitiesInfo.ElementAtOrDefault(0).Bathing.ToString() == "Total Help")
                        {
                            tblDailyActivities.AddCell(GetCell("Total Help"));
                        }

                        tblDailyActivities.AddCell(GetCell("Dressing"));
                        if (iGHPActivitiesInfo.ElementAtOrDefault(0).Dressing.ToString() == "Select")
                        {
                            tblDailyActivities.AddCell(GetCell(""));
                        }
                        else if (iGHPActivitiesInfo.ElementAtOrDefault(0).Dressing.ToString() == "No Help Needed")
                        {
                            tblDailyActivities.AddCell(GetCell("No Help Needed"));
                        }
                        else if (iGHPActivitiesInfo.ElementAtOrDefault(0).Dressing.ToString() == "Some Help Needed")
                        {
                            tblDailyActivities.AddCell(GetCell("Some Help Needed"));
                        }
                        else if (iGHPActivitiesInfo.ElementAtOrDefault(0).Dressing.ToString() == "Total Help")
                        {
                            tblDailyActivities.AddCell(GetCell("Total Help"));
                        }

                        tblDailyActivities.AddCell(GetCell("Eating"));
                        if (iGHPActivitiesInfo.ElementAtOrDefault(0).Eating.ToString() == "Select")
                        {
                            tblDailyActivities.AddCell(GetCell(""));
                        }
                        else if (iGHPActivitiesInfo.ElementAtOrDefault(0).Eating.ToString() == "No Help Needed")
                        {
                            tblDailyActivities.AddCell(GetCell("No Help Needed"));
                        }
                        else if (iGHPActivitiesInfo.ElementAtOrDefault(0).Eating.ToString() == "Some Help Needed")
                        {
                            tblDailyActivities.AddCell(GetCell("Some Help Needed"));
                        }
                        else if (iGHPActivitiesInfo.ElementAtOrDefault(0).Eating.ToString() == "Total Help")
                        {
                            tblDailyActivities.AddCell(GetCell("Total Help"));
                        }

                        tblDailyActivities.AddCell(GetCell("Using the bathroom"));
                        if (iGHPActivitiesInfo.ElementAtOrDefault(0).UsingtheBathroom.ToString() == "Select")
                        {
                            tblDailyActivities.AddCell(GetCell(""));
                        }
                        else if (iGHPActivitiesInfo.ElementAtOrDefault(0).UsingtheBathroom.ToString() == "No Help Needed")
                        {
                            tblDailyActivities.AddCell(GetCell("No Help Needed"));
                        }
                        else if (iGHPActivitiesInfo.ElementAtOrDefault(0).UsingtheBathroom.ToString() == "Some Help Needed")
                        {
                            tblDailyActivities.AddCell(GetCell("Some Help Needed"));
                        }
                        else if (iGHPActivitiesInfo.ElementAtOrDefault(0).UsingtheBathroom.ToString() == "Total Help")
                        {
                            tblDailyActivities.AddCell(GetCell("Total Help"));
                        }

                        tblDailyActivities.AddCell(GetCell("Walking"));
                        if (iGHPActivitiesInfo.ElementAtOrDefault(0).Walking.ToString() == "Select")
                        {
                            tblDailyActivities.AddCell(GetCell(""));
                        }
                        else if (iGHPActivitiesInfo.ElementAtOrDefault(0).Walking.ToString() == "No Help Needed")
                        {
                            tblDailyActivities.AddCell(GetCell("No Help Needed"));
                        }
                        else if (iGHPActivitiesInfo.ElementAtOrDefault(0).Walking.ToString() == "Some Help Needed")
                        {
                            tblDailyActivities.AddCell(GetCell("Some Help Needed"));
                        }
                        else if (iGHPActivitiesInfo.ElementAtOrDefault(0).Walking.ToString() == "Total Help")
                        {
                            tblDailyActivities.AddCell(GetCell("Total Help"));
                        }

                        tblDailyActivities.AddCell(GetCell("Preparing Meals"));
                        if (iGHPActivitiesInfo.ElementAtOrDefault(0).PreparingMeals.ToString() == "Select")
                        {
                            tblDailyActivities.AddCell(GetCell(""));
                        }
                        else if (iGHPActivitiesInfo.ElementAtOrDefault(0).PreparingMeals.ToString() == "No Help Needed")
                        {
                            tblDailyActivities.AddCell(GetCell("No Help Needed"));
                        }
                        else if (iGHPActivitiesInfo.ElementAtOrDefault(0).PreparingMeals.ToString() == "Some Help Needed")
                        {
                            tblDailyActivities.AddCell(GetCell("Some Help Needed"));
                        }
                        else if (iGHPActivitiesInfo.ElementAtOrDefault(0).PreparingMeals.ToString() == "Total Help")
                        {
                            tblDailyActivities.AddCell(GetCell("Total Help"));
                        }

                        tblDailyActivities.AddCell(GetCell("Taking Medications "));
                        if (iGHPActivitiesInfo.ElementAtOrDefault(0).TakingMedications.ToString() == "Select")
                        {
                            tblDailyActivities.AddCell(GetCell(""));
                        }
                        else if (iGHPActivitiesInfo.ElementAtOrDefault(0).TakingMedications.ToString() == "No Help Needed")
                        {
                            tblDailyActivities.AddCell(GetCell("No Help Needed"));
                        }
                        else if (iGHPActivitiesInfo.ElementAtOrDefault(0).TakingMedications.ToString() == "Some Help Needed")
                        {
                            tblDailyActivities.AddCell(GetCell("Some Help Needed"));
                        }
                        else if (iGHPActivitiesInfo.ElementAtOrDefault(0).TakingMedications.ToString() == "Total Help")
                        {
                            tblDailyActivities.AddCell(GetCell("Total Help"));
                        }
                    }
                    else
                    {
                        tblDailyActivities.AddCell(GetCell("Bathing"));

                        tblDailyActivities.AddCell(GetCell(""));


                        tblDailyActivities.AddCell(GetCell("Dressing"));

                        tblDailyActivities.AddCell(GetCell(""));


                        tblDailyActivities.AddCell(GetCell("Eating"));

                        tblDailyActivities.AddCell(GetCell(""));


                        tblDailyActivities.AddCell(GetCell("Using the bathroom"));

                        tblDailyActivities.AddCell(GetCell(""));


                        tblDailyActivities.AddCell(GetCell("Walking"));

                        tblDailyActivities.AddCell(GetCell(""));


                        tblDailyActivities.AddCell(GetCell("Preparing Meals"));

                        tblDailyActivities.AddCell(GetCell(""));


                        tblDailyActivities.AddCell(GetCell("Taking Medications "));

                        tblDailyActivities.AddCell(GetCell(""));

                    }

                    dc.Add(tblDailyActivities);

                    #endregion

                    #region History of Falls and Self-Expressed Fear of Falling

                    // line space
                    Chunk cSpaceFearofFalling = new Chunk("\r");
                    Phrase phSpaceFearofFalling = new Phrase();
                    phSpaceFearofFalling.Add(cSpaceFearofFalling);
                    dc.Add(phSpaceFearofFalling);

                    uri = Utility.GetServiceUrl("gethealthriskanswers");
                    postData = new IgJObject();
                    postData.Add("PatientID", iPatient);
                    postData.Add("ActionItem", "FallsAndFear");
                    List<PatientClinicalQuesRespInformation> iPatientInfoFearofFalling = Utility.PostRequest<PatientClinicalQuesRespInformation>(uri, postData.ToString(Formatting.None));

                    //List<PatientClinicalQuesRespInformation> iPatientInfoFearofFalling = null;
                    //uri = ConfigurationManager.AppSettings["serviceURL"] + "/gethealthriskanswers";
                    //postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + iPatient + "\",\"ActionItem\":\"FallsAndFear\"}";
                    //iPatientInfoFearofFalling = Utility.PostRequest<PatientClinicalQuesRespInformation>(uri, postData);

                    int ProviderCountFearofFalling = iPatientInfoFearofFalling.Count();


                    PdfPTable tblHistoryOfFalls = new PdfPTable(2);
                    tblHistoryOfFalls.TotalWidth = 550f;
                    tblHistoryOfFalls.LockedWidth = true;
                    tblHistoryOfFalls.AddCell(GetCell("History of Falls and Self-Expressed Fear of Falling", "", 2, 1));
                    tblHistoryOfFalls.AddCell(GetCell("1. During the past 12 months, how many times did you experience a fall?"));
                    if (ProviderCountFearofFalling > 0)
                    {
                        for (int i = 0; i < ProviderCountFearofFalling; i++)
                        {
                            if (iPatientInfoFearofFalling.ElementAtOrDefault(i).QCode.ToString() == "fallsandfearQ1")
                            {
                                tblHistoryOfFalls.AddCell(GetCell(iPatientInfoFearofFalling.ElementAtOrDefault(i).FactorDesc.Trim()));
                            }
                        }
                    }
                    else
                    {
                        tblHistoryOfFalls.AddCell(GetCell(""));
                    }
                    tblHistoryOfFalls.AddCell(GetCell("2. For each of the following activities, please check the opinion closest to your own to show how concerned you are that you might fall if you did this activity?", 2, 1));
                    tblHistoryOfFalls.AddCell(GetCell("a. Cleaning the house "));
                    if (ProviderCountFearofFalling > 0)
                    {
                        for (int i = 0; i < ProviderCountFearofFalling; i++)
                        {
                            //2nd row
                            if (iPatientInfoFearofFalling.ElementAtOrDefault(i).QCode.ToString() == "fallsandfearQ2")
                            {
                                tblHistoryOfFalls.AddCell(GetCell(iPatientInfoFearofFalling.ElementAtOrDefault(i).FactorDesc.ToString()));
                            }
                        }
                    }
                    else
                    {
                        tblHistoryOfFalls.AddCell(GetCell(""));
                    }
                    tblHistoryOfFalls.AddCell(GetCell("b. Getting dressed or undressed "));
                    if (ProviderCountFearofFalling > 0)
                    {
                        for (int i = 0; i < ProviderCountFearofFalling; i++)
                        {
                            //2nd row
                            if (iPatientInfoFearofFalling.ElementAtOrDefault(i).QCode.ToString() == "fallsandfearQ3")
                            {
                                tblHistoryOfFalls.AddCell(GetCell(iPatientInfoFearofFalling.ElementAtOrDefault(i).FactorDesc.ToString()));
                            }
                        }
                    }
                    else
                    {
                        tblHistoryOfFalls.AddCell(GetCell(""));
                    }
                    tblHistoryOfFalls.AddCell(GetCell("c. Taking a bath or shower "));
                    if (ProviderCountFearofFalling > 0)
                    {
                        for (int i = 0; i < ProviderCountFearofFalling; i++)
                        {
                            //2nd row
                            if (iPatientInfoFearofFalling.ElementAtOrDefault(i).QCode.ToString() == "fallsandfearQ4")
                            {
                                tblHistoryOfFalls.AddCell(GetCell(iPatientInfoFearofFalling.ElementAtOrDefault(i).FactorDesc.ToString()));
                            }
                        }
                    }
                    else
                    {
                        tblHistoryOfFalls.AddCell(GetCell(""));
                    }
                    tblHistoryOfFalls.AddCell(GetCell("d. Getting in and out of a chair "));
                    if (ProviderCountFearofFalling > 0)
                    {
                        for (int i = 0; i < ProviderCountFearofFalling; i++)
                        {
                            //2nd row
                            if (iPatientInfoFearofFalling.ElementAtOrDefault(i).QCode.ToString() == "fallsandfearQ5")
                            {
                                tblHistoryOfFalls.AddCell(GetCell(iPatientInfoFearofFalling.ElementAtOrDefault(i).FactorDesc.ToString()));
                            }
                        }
                    }
                    else
                    {
                        tblHistoryOfFalls.AddCell(GetCell(""));
                    }
                    tblHistoryOfFalls.AddCell(GetCell("e. Getting up and down stairs "));
                    if (ProviderCountFearofFalling > 0)
                    {
                        for (int i = 0; i < ProviderCountFearofFalling; i++)
                        {
                            //2nd row
                            if (iPatientInfoFearofFalling.ElementAtOrDefault(i).QCode.ToString() == "fallsandfearQ6")
                            {
                                tblHistoryOfFalls.AddCell(GetCell(iPatientInfoFearofFalling.ElementAtOrDefault(i).FactorDesc.ToString()));
                            }
                        }
                    }
                    else
                    {
                        tblHistoryOfFalls.AddCell(GetCell(""));
                    }
                    tblHistoryOfFalls.AddCell(GetCell("f. Going out to social events  "));
                    if (ProviderCountFearofFalling > 0)
                    {
                        for (int i = 0; i < ProviderCountFearofFalling; i++)
                        {
                            //2nd row
                            if (iPatientInfoFearofFalling.ElementAtOrDefault(i).QCode.ToString() == "fallsandfearQ7")
                            {
                                tblHistoryOfFalls.AddCell(GetCell(iPatientInfoFearofFalling.ElementAtOrDefault(i).FactorDesc.ToString()));
                            }
                        }
                    }
                    else
                    {
                        tblHistoryOfFalls.AddCell(GetCell(""));
                    }


                    dc.Add(tblHistoryOfFalls);

                    #endregion

                    #region Preventive Screens

                    // line space
                    Chunk cSpaceScreens = new Chunk("\r");
                    Phrase phSpaceScreens = new Phrase();
                    phSpaceScreens.Add(cSpaceScreens);
                    dc.Add(phSpaceScreens);

                    // Preventive Screens Heading
                    Chunk cLabelScreens = new Chunk("Preventive Screens", fLabel);
                    Phrase phScreens = new Phrase();
                    phScreens.Add(cLabelScreens);
                    Paragraph pScreens = new Paragraph();
                    pScreens.Add(phScreens);
                    dc.Add(phScreens);

                    // line space
                    Chunk cSpaceScreens1 = new Chunk("\r");
                    dc.Add(cSpaceScreens1);

                    //Session["Gender"] = oPatientInfoID.Gender.ToString().ToLower().Trim();

                    uri = Utility.GetServiceUrl("preventiveinfo");
                    postData = new IgJObject();
                    postData.Add("PatientID", iPatient);
                    List<PreventiveInfo> iGHPPreventiveInfo = Utility.PostRequest<PreventiveInfo>(uri, postData.ToString(Formatting.None));

                    //List<PreventiveInfo> iGHPPreventiveInfo = null;
                    //uri = ConfigurationManager.AppSettings["serviceURL"] + "/preventiveinfo";
                    //postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + iPatient + "\"}";
                    //iGHPPreventiveInfo = Utility.PostRequest<PreventiveInfo>(uri, postData);

                    PdfPTable tblScreens = new PdfPTable(2);
                    tblScreens.TotalWidth = 550f;
                    tblScreens.LockedWidth = true;

                    if (iGHPPreventiveInfo.Count > 0)
                    {
                        // 1st row
                        //tblScreens.AddCell(GetBoldCell("Preventive Screen", 1, 1));
                        //tblScreens.AddCell(GetBoldCell("Date of Most Recent Screen", 1, 1));

                        // 2nd row. 
                        tblScreens.AddCell(GetCell("Bone mass measurement", 1, 1));
                        tblScreens.AddCell(GetCell(iGHPPreventiveInfo.ElementAtOrDefault(0).BoneMassMeasurement.ToString(), 1, 1));

                        // 3rd row. 
                        tblScreens.AddCell(GetCell("Colorectal screening", 1, 1));
                        tblScreens.AddCell(GetCell(iGHPPreventiveInfo.ElementAtOrDefault(0).ColorectalScreening.ToString(), 1, 1));

                        // 4th row. 
                        tblScreens.AddCell(GetCell("Immunizations", 1, 1));
                        tblScreens.AddCell(new Phrase("Pneumonia: " + iGHPPreventiveInfo.ElementAtOrDefault(0).Pneumonia.ToString() + "\r" + "Flu Shot: " + iGHPPreventiveInfo.ElementAtOrDefault(0).FluShot.ToString() + "\r" + "Hepatitis B:" + iGHPPreventiveInfo.ElementAtOrDefault(0).HepatitisB.ToString(), fText));

                        // 5th row. 
                        tblScreens.AddCell(GetCell("Mammography screening", 1, 1));
                        tblScreens.AddCell(GetCell(iGHPPreventiveInfo.ElementAtOrDefault(0).MammographyScreening.ToString(), 1, 1));

                        // 6th row. 
                        tblScreens.AddCell(GetCell("Pap test, pelvic exams, and clinical breast exam", 1, 1));
                        tblScreens.AddCell(GetCell(iGHPPreventiveInfo.ElementAtOrDefault(0).PapTest.ToString(), 1, 1));

                        // 7th row. 
                        tblScreens.AddCell(GetCell("Prostate cancer screening exams", 1, 1));
                        tblScreens.AddCell(GetCell(iGHPPreventiveInfo.ElementAtOrDefault(0).ProstateCancer.ToString(), 1, 1));
                    }
                    else
                    {
                        // 1st row
                        tblScreens.AddCell(GetBoldCell("Preventive Screen", 1, 1));
                        tblScreens.AddCell(GetBoldCell("Date of Most Recent Screen", 1, 1));

                        // 2nd row. 
                        tblScreens.AddCell(GetCell("Bone mass measurement", 1, 1));
                        tblScreens.AddCell(GetCell("", 1, 1));

                        // 3rd row. 
                        tblScreens.AddCell(GetCell("Colorectal screening", 1, 1));
                        tblScreens.AddCell(GetCell("", 1, 1));

                        // 4th row. 
                        tblScreens.AddCell(GetCell("Immunizations", 1, 1));
                        tblScreens.AddCell(new Phrase("Pneumonia: " + "\r" + "Flu Shot: " + "\r" + "Hepatitis B:", fText));

                        // 5th row. 
                        tblScreens.AddCell(GetCell("Mammography screening", 1, 1));
                        tblScreens.AddCell(GetCell("", 1, 1));

                        // 6th row. 
                        tblScreens.AddCell(GetCell("Pap test, pelvic exams, and clinical breast exam", 1, 1));
                        tblScreens.AddCell(GetCell("", 1, 1));

                        // 7th row. 
                        tblScreens.AddCell(GetCell("Prostate cancer screening exams", 1, 1));
                        tblScreens.AddCell(GetCell("", 1, 1));
                    }

                    dc.Add(tblScreens);

                    #endregion

                    #region Immunizations

                    // line space
                    Chunk cSpaceImmunizations = new Chunk("\r");
                    Phrase phSpaceImmunizations = new Phrase();
                    phSpaceImmunizations.Add(cSpaceImmunizations);
                    dc.Add(phSpaceImmunizations);

                    // Emergency Heading
                    Chunk cLabelImmunizations = new Chunk("Immunizations", fLabel);
                    Phrase phImmunizations = new Phrase();
                    phImmunizations.Add(cLabelImmunizations);
                    Paragraph pImmunizations = new Paragraph();
                    pImmunizations.Add(phImmunizations);
                    dc.Add(phImmunizations);

                    // line space
                    Chunk cSpaceImmunizations1 = new Chunk("\r\r");
                    dc.Add(cSpaceImmunizations1);

                    iTextSharp.text.Table tblImmunizations = new iTextSharp.text.Table(4);
                    tblImmunizations.Width = 105;
                    tblImmunizations.BorderWidth = 0;
                    //tblProviders.BorderColor = Color.BLACK;
                    tblImmunizations.Padding = 3;
                    tblImmunizations.Spacing = 1;
                    Cell cImmunizations1 = new Cell(new Phrase("Immunization", fLabelNormal));
                    cImmunizations1.HorizontalAlignment = 1;
                    Cell cImmunizations2 = new Cell(new Phrase("Administration Date", fLabelNormal));
                    cImmunizations2.HorizontalAlignment = 1;
                    Cell cImmunizations3 = new Cell(new Phrase("Adverse Event", fLabelNormal));
                    cImmunizations3.HorizontalAlignment = 1;
                    Cell cImmunizations5 = new Cell(new Phrase("Details", fLabelNormal));
                    cImmunizations5.HorizontalAlignment = 1;
                    tblImmunizations.AddCell(cImmunizations1);
                    tblImmunizations.AddCell(cImmunizations2);
                    tblImmunizations.AddCell(cImmunizations3);
                    tblImmunizations.AddCell(cImmunizations5);

                    uri = Utility.GetServiceUrl("immunization");
                    postData = new IgJObject();
                    postData.Add("PatientID", iPatient);
                    List<Immunizations> listPatientImmunization = Utility.PostRequest<Immunizations>(uri, postData.ToString(Formatting.None));

                    //List<Immunizations> listPatientImmunization = null;
                    //uri = ConfigurationManager.AppSettings["serviceURL"] + "/immunization";
                    //postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + iPatient + "\"}";
                    //listPatientImmunization = Utility.PostRequest<Immunizations>(uri, postData);

                    if (listPatientImmunization.Count > 0)
                    {
                        for (int i = 0; i < listPatientImmunization.Count; i++)
                        {
                            tblImmunizations.AddCell(new Phrase(Convert.ToString(listPatientImmunization[i].ImmunizationType), fText));
                            tblImmunizations.AddCell(new Phrase(Convert.ToString(listPatientImmunization[i].AdministrationDate.ToString("MM/dd/yyyy")), fText));
                            tblImmunizations.AddCell(new Phrase(Convert.ToString(listPatientImmunization[i].AdverseEvent), fText));
                            tblImmunizations.AddCell(new Phrase(Convert.ToString(listPatientImmunization[i].Notes), fText));
                        }

                        dc.Add(tblImmunizations);
                    }
                    else
                    {
                        dc.Add(tblImmunizations);

                        Chunk cNAImmunizations = new Chunk(" No Records found.", fTextItalic);
                        dc.Add(cNAImmunizations);
                    }

                    #endregion

                    #region Health Log

                    // line space
                    Chunk cSpaceHealth = new Chunk("\r");
                    Phrase phSpaceHealth = new Phrase();
                    phSpaceHealth.Add(cSpaceHealth);
                    dc.Add(phSpaceHealth);

                    // Allergies Heading
                    Chunk cLabelHealth = new Chunk(" Claims Log", fLabel);
                    Phrase phHealth = new Phrase();
                    phHealth.Add(cLabelHealth);
                    Paragraph pHealth = new Paragraph();
                    pHealth.Add(phHealth);
                    dc.Add(phHealth);

                    // line space
                    Chunk cSpaceHealth1 = new Chunk("\r");
                    dc.Add(cSpaceHealth1);

                    iTextSharp.text.Table tblHealth = new iTextSharp.text.Table(6);
                    tblHealth.Width = 105;
                    tblHealth.BorderWidth = 0;
                    //tblProviders.BorderColor = Color.BLACK;
                    tblHealth.Padding = 3;
                    tblHealth.Spacing = 1;
                    Cell cHealth1 = new Cell(new Phrase("Date Diagnosed", fLabelNormal));
                    cHealth1.HorizontalAlignment = 1;
                    Cell cHealth2 = new Cell(new Phrase("Doctor", fLabelNormal));
                    cHealth2.HorizontalAlignment = 1;
                    Cell cHealth3 = new Cell(new Phrase("Nature of Health Problems", fLabelNormal));
                    cHealth3.HorizontalAlignment = 1;
                    Cell cHealth4 = new Cell(new Phrase("Age at Onset", fLabelNormal));
                    cHealth4.HorizontalAlignment = 1;
                    Cell cHealth5 = new Cell(new Phrase("Condition Status", fLabelNormal));
                    cHealth5.HorizontalAlignment = 1;
                    Cell cHealth6 = new Cell(new Phrase("Remarks", fLabelNormal));
                    cHealth6.HorizontalAlignment = 1;

                    tblHealth.AddCell(cHealth1);
                    tblHealth.AddCell(cHealth2);
                    tblHealth.AddCell(cHealth3);
                    tblHealth.AddCell(cHealth4);
                    tblHealth.AddCell(cHealth5);
                    tblHealth.AddCell(cHealth6);

                    uri = Utility.GetServiceUrl("healthlog");
                    postData = new IgJObject();
                    postData.Add("PatientID", iPatient);
                    List<HealthLogInfo> lioHealthLog = Utility.PostRequest<HealthLogInfo>(uri, postData.ToString(Formatting.None));

                    //List<HealthLogInfo> lioHealthLog = null;
                    //uri = ConfigurationManager.AppSettings["serviceURL"] + "/healthlog";
                    //postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + iPatient + "\"}";
                    //lioHealthLog = Utility.PostRequest<HealthLogInfo>(uri, postData);

                    if (lioHealthLog.Count > 0)
                    {
                        for (int i = 0; i < lioHealthLog.Count; i++)
                        {
                            tblHealth.AddCell(new Phrase(Convert.ToString(lioHealthLog[i].DiagnosedDate), fText));
                            tblHealth.AddCell(new Phrase(Convert.ToString(lioHealthLog[i].Doctor), fText));
                            tblHealth.AddCell(new Phrase(Convert.ToString(lioHealthLog[i].Diagnoses), fText));
                            tblHealth.AddCell(new Phrase(Convert.ToString(lioHealthLog[i].AgeatOnset), fText));
                            tblHealth.AddCell(new Phrase(Convert.ToString(lioHealthLog[i].ConditionStatus), fText));
                            tblHealth.AddCell(new Phrase(Convert.ToString(lioHealthLog[i].Remarks), fText));
                        }

                        dc.Add(tblHealth);
                    }
                    else
                    {
                        dc.Add(tblHealth);

                        Chunk cNAHealth = new Chunk("No Records found for Claims Log.", fTextItalic);
                        dc.Add(cNAHealth);
                    }



                    #endregion

                    #region Medications

                    // line space
                    Chunk cSpaceMedications = new Chunk("\r");
                    Phrase phSpaceMedications = new Phrase();
                    phSpaceMedications.Add(cSpaceMedications);
                    dc.Add(phSpaceMedications);

                    // Allergies Heading
                    Chunk cLabelMedications = new Chunk("Medications", fLabel);
                    Phrase phMedications = new Phrase();
                    phMedications.Add(cLabelMedications);
                    Paragraph pMedications = new Paragraph();
                    pMedications.Add(phMedications);
                    dc.Add(phMedications);

                    // line space
                    Chunk cSpaceMedications1 = new Chunk("\r");
                    dc.Add(cSpaceMedications1);

                    iTextSharp.text.Table tblMedications = new iTextSharp.text.Table(6);
                    tblMedications.Width = 105;
                    tblMedications.BorderWidth = 0;
                    //tblProviders.BorderColor = Color.BLACK;
                    tblMedications.Padding = 3;
                    tblMedications.Spacing = 1;
                    Cell cMedications1 = new Cell(new Phrase("Name", fLabelNormal));
                    cMedications1.HorizontalAlignment = 1;
                    Cell cMedications2 = new Cell(new Phrase("Dosage", fLabelNormal));
                    cMedications2.HorizontalAlignment = 1;
                    Cell cMedications3 = new Cell(new Phrase("How Often Taken", fLabelNormal));
                    cMedications3.HorizontalAlignment = 1;
                    Cell cMedications4 = new Cell(new Phrase("Reason for taking", fLabelNormal));
                    cMedications4.HorizontalAlignment = 1;
                    Cell cMedications5 = new Cell(new Phrase("Date started", fLabelNormal));
                    cMedications5.HorizontalAlignment = 1;
                    Cell cMedications6 = new Cell(new Phrase("Date stopped", fLabelNormal));
                    cMedications6.HorizontalAlignment = 1;

                    tblMedications.AddCell(cMedications1);
                    tblMedications.AddCell(cMedications2);
                    tblMedications.AddCell(cMedications3);
                    tblMedications.AddCell(cMedications4);
                    tblMedications.AddCell(cMedications5);
                    tblMedications.AddCell(cMedications6);

                    //HealthLog oMedication = new HealthLog();
                    //oMedication.PatientId = iPatient;
                    //oMedication.UserId = Convert.ToInt32(Session["UID"]);
                    //oMedication.UserType = Convert.ToInt32(Session["UserType"]);
                    //IList<HealthLog> lioMedication = (new HealthLogMedicationsBLL()).GetMedicationsList(oMedication);

                    uri = Utility.GetServiceUrl("medications");
                    postData = new IgJObject();
                    postData.Add("PatientID", iPatient);
                    List<Medications> iPatientInfo = Utility.PostRequest<Medications>(uri, postData.ToString(Formatting.None));

                    //List<Medications> iPatientInfo = null;
                    //uri = ConfigurationManager.AppSettings["serviceURL"] + "/medications";
                    //postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + iPatient + "\"}";
                    //iPatientInfo = Utility.PostRequest<Medications>(uri, postData);

                    if (iPatientInfo.Count > 0)
                    {
                        for (int i = 0; i < iPatientInfo.Count; i++)
                        {
                            tblMedications.AddCell(new Phrase(Convert.ToString(iPatientInfo[i].Description), fText));
                            tblMedications.AddCell(new Phrase(Convert.ToString(iPatientInfo[i].Dosage), fText));
                            tblMedications.AddCell(new Phrase(Convert.ToString(iPatientInfo[i].HowTaken), fText));
                            tblMedications.AddCell(new Phrase(Convert.ToString(iPatientInfo[i].Reason), fText));
                            tblMedications.AddCell(new Phrase(Convert.ToString(iPatientInfo[i].Date), fText));
                            tblMedications.AddCell(new Phrase(Convert.ToString(iPatientInfo[i].DateStopped), fText));
                        }

                        dc.Add(tblMedications);
                    }
                    else
                    {
                        dc.Add(tblMedications);

                        Chunk cNAMedications = new Chunk("No Records found for Medications.", fTextItalic);
                        dc.Add(cNAMedications);
                    }



                    #endregion

                    #region Doctor Visits

                    // line space
                    Chunk cSpaceDoctorVisits = new Chunk("\r");
                    Phrase phSpaceDoctorVisits = new Phrase();
                    phSpaceDoctorVisits.Add(cSpaceDoctorVisits);
                    dc.Add(phSpaceDoctorVisits);

                    // Doctor Visits
                    Chunk cLabelDoctorVisits = new Chunk("Encounters", fLabel);
                    Phrase phDoctorVisits = new Phrase();
                    phDoctorVisits.Add(cLabelDoctorVisits);
                    Paragraph pDoctorVisits = new Paragraph();
                    pDoctorVisits.Add(phDoctorVisits);
                    dc.Add(phDoctorVisits);

                    // line space
                    Chunk cSpaceDoctorVisits1 = new Chunk("\r");
                    dc.Add(cSpaceDoctorVisits1);

                    iTextSharp.text.Table tblDoctorVisits = new iTextSharp.text.Table(4);
                    tblDoctorVisits.Width = 105;
                    tblDoctorVisits.BorderWidth = 0;
                    //tblProviders.BorderColor = Color.BLACK;
                    tblDoctorVisits.Padding = 3;
                    tblDoctorVisits.Spacing = 1;
                    Cell cDoctorVisits1 = new Cell(new Phrase("Date", fLabelNormal));
                    cDoctorVisits1.HorizontalAlignment = 1;
                    Cell cDoctorVisits2 = new Cell(new Phrase("Doctor", fLabelNormal));
                    cDoctorVisits2.HorizontalAlignment = 1;
                    Cell cDoctorVisits3 = new Cell(new Phrase("Reason", fLabelNormal));
                    cDoctorVisits3.HorizontalAlignment = 1;
                    Cell cDoctorVisits4 = new Cell(new Phrase("Diagnoses", fLabelNormal));
                    cDoctorVisits4.HorizontalAlignment = 1;
                    Cell cDoctorVisits5 = new Cell(new Phrase("Doctor Visits", fLabelNormal));
                    cDoctorVisits5.HorizontalAlignment = 1;

                    tblDoctorVisits.AddCell(cDoctorVisits1);
                    tblDoctorVisits.AddCell(cDoctorVisits2);
                    tblDoctorVisits.AddCell(cDoctorVisits3);
                    tblDoctorVisits.AddCell(cDoctorVisits4);

                    uri = Utility.GetServiceUrl("doctorvisit");
                    postData = new IgJObject();
                    postData.Add("PatientID", iPatient);
                    List<HealthLogInfo> liDoctorVisit = Utility.PostRequest<HealthLogInfo>(uri, postData.ToString(Formatting.None));

                    //List<HealthLogInfo> liDoctorVisit = null;
                    //uri = ConfigurationManager.AppSettings["serviceURL"] + "/doctorvisit";
                    //postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + iPatient + "\"}";
                    //liDoctorVisit = Utility.PostRequest<HealthLogInfo>(uri, postData);

                    if (liDoctorVisit.Count > 0)
                    {
                        for (int i = 0; i < liDoctorVisit.Count; i++)
                        {
                            tblDoctorVisits.AddCell(new Phrase(Convert.ToString(liDoctorVisit[i].Visitdate.ToString("MM/dd/yyyy")), fText));
                            tblDoctorVisits.AddCell(new Phrase(Convert.ToString(liDoctorVisit[i].DoctorName), fText));
                            tblDoctorVisits.AddCell(new Phrase(Convert.ToString(liDoctorVisit[i].VisitReason), fText));
                            tblDoctorVisits.AddCell(new Phrase(Convert.ToString(liDoctorVisit[i].VisitDiagnosis), fText));
                        }

                        dc.Add(tblDoctorVisits);
                    }
                    else
                    {
                        dc.Add(tblDoctorVisits);

                        Chunk cNADoctorVisits = new Chunk("No Records found for Doctor Visits.", fTextItalic);
                        dc.Add(cNADoctorVisits);
                    }



                    #endregion

                    #region Hospitalizations

                    // line space
                    Chunk cSpaceHospitalizations = new Chunk("\r");
                    Phrase phSpaceHospitalizations = new Phrase();
                    phSpaceHospitalizations.Add(cSpaceHospitalizations);
                    dc.Add(phSpaceHospitalizations);

                    // Hospitalizations Heading
                    Chunk cLabelHospitalizations = new Chunk("Hospitalizations ", fLabel);
                    Phrase phHospitalizations = new Phrase();
                    phHospitalizations.Add(cLabelHospitalizations);
                    Paragraph pHospitalizations = new Paragraph();
                    pHospitalizations.Add(phHospitalizations);
                    dc.Add(phHospitalizations);

                    // line space
                    Chunk cSpaceHospitalizations1 = new Chunk("\r");
                    dc.Add(cSpaceHospitalizations1);

                    iTextSharp.text.Table tblHospitalizations = new iTextSharp.text.Table(4);
                    tblHospitalizations.Width = 105;
                    tblHospitalizations.BorderWidth = 0;
                    //tblProviders.BorderColor = Color.BLACK;
                    tblHospitalizations.Padding = 3;
                    tblHospitalizations.Spacing = 1;
                    Cell cHospitalizations1 = new Cell(new Phrase("Hospitalization Type", fLabelNormal));
                    cHospitalizations1.HorizontalAlignment = 1;
                    Cell cHospitalizations2 = new Cell(new Phrase("Hospital Name", fLabelNormal));
                    cHospitalizations2.HorizontalAlignment = 1;
                    Cell cHospitalizations3 = new Cell(new Phrase("Admit Date", fLabelNormal));
                    cHospitalizations3.HorizontalAlignment = 1;
                    Cell cHospitalizations5 = new Cell(new Phrase("Discharge Date", fLabelNormal));
                    cHospitalizations5.HorizontalAlignment = 1;
                    tblHospitalizations.AddCell(cHospitalizations1);
                    tblHospitalizations.AddCell(cHospitalizations2);
                    tblHospitalizations.AddCell(cHospitalizations3);
                    tblHospitalizations.AddCell(cHospitalizations5);

                    uri = Utility.GetServiceUrl("hospitalizations");
                    postData = new IgJObject();
                    postData.Add("PatientID", iPatient);
                    List<HealthLogInfo> liHospitalizations = Utility.PostRequest<HealthLogInfo>(uri, postData.ToString(Formatting.None));

                    //List<HealthLogInfo> liHospitalizations = null;
                    //uri = ConfigurationManager.AppSettings["serviceURL"] + "/hospitalizations";
                    //postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + iPatient + "\"}";
                    //liHospitalizations = Utility.PostRequest<HealthLogInfo>(uri, postData);

                    if (liHospitalizations.Count > 0)
                    {
                        for (int i = 0; i < liHospitalizations.Count; i++)
                        {
                            //1st row
                            tblHospitalizations.AddCell(new Phrase(Convert.ToString(liHospitalizations[i].HospitalizationType), fText));
                            tblHospitalizations.AddCell(new Phrase(Convert.ToString(liHospitalizations[i].HospitalName), fText));
                            tblHospitalizations.AddCell(new Phrase(Convert.ToString(liHospitalizations[i].AdmitDate.ToString("MM/dd/yyyy")), fText));
                            tblHospitalizations.AddCell(new Phrase(Convert.ToString(liHospitalizations[i].DischargeDate.ToString("MM/dd/yyyy")), fText));
                        }
                        dc.Add(tblHospitalizations);
                    }
                    else
                    {
                        Chunk cNAHospitalizations = new Chunk("No Records found for Hospitalizations.", fTextItalic);
                        dc.Add(cNAHospitalizations);
                    }

                    #endregion

                    #region Surgeries

                    // line space
                    Chunk cSpaceSurgeries = new Chunk("\r");
                    Phrase phSpaceSurgeries = new Phrase();
                    phSpaceSurgeries.Add(cSpaceSurgeries);
                    dc.Add(phSpaceSurgeries);

                    // Surgeries Heading
                    Chunk cLabelSurgeries = new Chunk("Procedures/Surgeries ", fLabel);
                    Phrase phSurgeries = new Phrase();
                    phSurgeries.Add(cLabelSurgeries);
                    Paragraph pSurgeries = new Paragraph();
                    pSurgeries.Add(phSurgeries);
                    dc.Add(phSurgeries);

                    // line space
                    Chunk cSpaceSurgeries1 = new Chunk("\r");
                    dc.Add(cSpaceSurgeries1);

                    uri = Utility.GetServiceUrl("surgeries");
                    postData = new IgJObject();
                    postData.Add("PatientID", iPatient);
                    List<Surgeries> iGHPSurgeriesInfo = Utility.PostRequest<Surgeries>(uri, postData.ToString(Formatting.None));

                    //List<Surgeries> iGHPSurgeriesInfo = null;
                    //uri = ConfigurationManager.AppSettings["serviceURL"] + "/surgeries";
                    //postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + iPatient + "\"}";
                    //iGHPSurgeriesInfo = Utility.PostRequest<Surgeries>(uri, postData);

                    if (iGHPSurgeriesInfo.Count > 0)
                    {
                        for (int i = 0; i < iGHPSurgeriesInfo.Count; i++)
                        {
                            PdfPTable tblSurgeries = new PdfPTable(4);
                            tblSurgeries.TotalWidth = 550f;
                            tblSurgeries.LockedWidth = true;

                            if (iGHPSurgeriesInfo.Count > 0)
                            {
                                //1st row
                                tblSurgeries.AddCell(GetCell("Date: ", iGHPSurgeriesInfo.ElementAtOrDefault(i).Date.ToString(), 1, 1));
                                tblSurgeries.AddCell(GetCell("Doctor: ", iGHPSurgeriesInfo.ElementAtOrDefault(i).Doctor.ToString(), 1, 1));
                                tblSurgeries.AddCell(GetCell("Hospital: ", iGHPSurgeriesInfo.ElementAtOrDefault(i).Hospital.ToString(), 1, 1));
                                tblSurgeries.AddCell(GetCell("Procedure: ", iGHPSurgeriesInfo.ElementAtOrDefault(i).SurgeriesProcedure.ToString(), 1, 1));
                            }
                            else
                            {
                                //1st row
                                tblSurgeries.AddCell(GetCell("Date: ", "", 1, 1));
                                tblSurgeries.AddCell(GetCell("Doctor: ", "", 1, 1));
                                tblSurgeries.AddCell(GetCell("Hospital: ", "", 1, 1));
                                tblSurgeries.AddCell(GetCell("Procedure: ", "", 1, 1));
                            }

                            dc.Add(tblSurgeries);

                        }
                    }
                    else
                    {
                        Chunk cNASurgeries = new Chunk("No Records found for Surgeries.", fTextItalic);
                        dc.Add(cNASurgeries);
                    }



                    #endregion

                    #region Lab

                    // line space
                    Chunk cSpaceLabImaging = new Chunk("\r");
                    Phrase phSpaceLabImaging = new Phrase();
                    phSpaceLabImaging.Add(cSpaceLabImaging);
                    dc.Add(phSpaceLabImaging);

                    // LabImaging Heading
                    Chunk cLabelLabImaging = new Chunk("Lab", fLabel);
                    Phrase phLabImaging = new Phrase();
                    phLabImaging.Add(cLabelLabImaging);
                    Paragraph pLabImaging = new Paragraph();
                    pLabImaging.Add(phLabImaging);
                    dc.Add(phLabImaging);

                    // line space
                    Chunk cSpaceLabImaging1 = new Chunk("\r");
                    dc.Add(cSpaceLabImaging1);

                    iTextSharp.text.Table tblLabImaging = new iTextSharp.text.Table(4);
                    tblLabImaging.Width = 105;
                    tblLabImaging.BorderWidth = 0;
                    //tblProviders.BorderColor = Color.BLACK;
                    tblLabImaging.Padding = 3;
                    tblLabImaging.Spacing = 1;
                    Cell cLabImaging1 = new Cell(new Phrase("Date", fLabelNormal));
                    cLabImaging1.HorizontalAlignment = 1;
                    Cell cLabImaging2 = new Cell(new Phrase("Test Type", fLabelNormal));
                    cLabImaging2.HorizontalAlignment = 1;
                    Cell cLabImaging3 = new Cell(new Phrase("Results", fLabelNormal));
                    cLabImaging3.HorizontalAlignment = 1;
                    Cell cLabImaging5 = new Cell(new Phrase("Requesting Doctor", fLabelNormal));
                    cLabImaging5.HorizontalAlignment = 1;
                    tblLabImaging.AddCell(cLabImaging1);
                    tblLabImaging.AddCell(cLabImaging2);
                    tblLabImaging.AddCell(cLabImaging3);
                    tblLabImaging.AddCell(cLabImaging5);

                    uri = Utility.GetServiceUrl("labimaging");
                    postData = new IgJObject();
                    postData.Add("PatientID", iPatient);
                    List<LabImaging> iGHPLabImagingInfo = Utility.PostRequest<LabImaging>(uri, postData.ToString(Formatting.None));

                    //List<LabImaging> iGHPLabImagingInfo = null;
                    //uri = ConfigurationManager.AppSettings["serviceURL"] + "/labimaging";
                    //postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + iPatient + "\"}";
                    //iGHPLabImagingInfo = Utility.PostRequest<LabImaging>(uri, postData);

                    if (iGHPLabImagingInfo.Count > 0)
                    {

                        for (int i = 0; i < iGHPLabImagingInfo.Count; i++)
                        {
                            //1st row
                            tblLabImaging.AddCell(new Phrase(Convert.ToString(iGHPLabImagingInfo[i].Date.ToString()), fText));
                            tblLabImaging.AddCell(new Phrase(Convert.ToString(iGHPLabImagingInfo[i].TestType.ToString()), fText));
                            tblLabImaging.AddCell(new Phrase(Convert.ToString(iGHPLabImagingInfo[i].Results.ToString()), fText));
                            tblLabImaging.AddCell(new Phrase(Convert.ToString(iGHPLabImagingInfo[i].RequestingDoctor.ToString()), fText));

                        }
                        dc.Add(tblLabImaging);
                    }
                    else
                    {
                        Chunk cNALabImaging = new Chunk("No Records found for Lab.", fTextItalic);
                        dc.Add(cNALabImaging);
                    }

                    #endregion

                    #region Imaging
                    // line space
                    Chunk cSpaceImaging = new Chunk("\r");
                    Phrase phSpaceImaging = new Phrase();
                    phSpaceImaging.Add(cSpaceImaging);
                    dc.Add(phSpaceImaging);

                    // LabImaging Heading
                    Chunk cLabelImaging = new Chunk("Imaging", fLabel);
                    Phrase phImaging = new Phrase();
                    phImaging.Add(cLabelImaging);
                    Paragraph pImaging = new Paragraph();
                    pImaging.Add(phImaging);
                    dc.Add(phImaging);

                    // line space
                    Chunk cSpaceImaging1 = new Chunk("\r");
                    dc.Add(cSpaceImaging1);

                    iTextSharp.text.Table tblImaging = new iTextSharp.text.Table(4);
                    tblImaging.Width = 105;
                    tblImaging.BorderWidth = 0;
                    //tblProviders.BorderColor = Color.BLACK;
                    tblImaging.Padding = 3;
                    tblImaging.Spacing = 1;
                    Cell cImaging1 = new Cell(new Phrase("Date", fLabelNormal));
                    cImaging1.HorizontalAlignment = 1;
                    Cell cImaging2 = new Cell(new Phrase("Test Type", fLabelNormal));
                    cImaging2.HorizontalAlignment = 1;
                    Cell cImaging3 = new Cell(new Phrase("RequestingDoctor", fLabelNormal));
                    cImaging3.HorizontalAlignment = 1;
                    Cell cImaging5 = new Cell(new Phrase("Administered By", fLabelNormal));
                    cImaging5.HorizontalAlignment = 1;
                    tblImaging.AddCell(cImaging1);
                    tblImaging.AddCell(cImaging2);
                    tblImaging.AddCell(cImaging3);
                    tblImaging.AddCell(cImaging5);

                    uri = Utility.GetServiceUrl("labimaging");
                    postData = new IgJObject();
                    postData.Add("PatientID", iPatient);
                    List<LabImaging> iGHPImagingInfo = Utility.PostRequest<LabImaging>(uri, postData.ToString(Formatting.None));

                    //List<LabImaging> iGHPImagingInfo = null;
                    //uri = ConfigurationManager.AppSettings["serviceURL"] + "/labimaging";
                    //postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + iPatient + "\"}";
                    //iGHPImagingInfo = Utility.PostRequest<LabImaging>(uri, postData);

                    if (iGHPImagingInfo.Count > 0)
                    {

                        for (int i = 0; i < iGHPImagingInfo.Count; i++)
                        {
                            //1st row
                            tblImaging.AddCell(new Phrase(Convert.ToString(iGHPImagingInfo[i].Date.ToString()), fText));
                            tblImaging.AddCell(new Phrase(Convert.ToString(iGHPImagingInfo[i].TestType.ToString()), fText));
                            tblImaging.AddCell(new Phrase(Convert.ToString(iGHPImagingInfo[i].RequestingDoctor.ToString()), fText));
                            tblImaging.AddCell(new Phrase(Convert.ToString(iGHPImagingInfo[i].AdministeredBy.ToString()), fText));

                        }
                        dc.Add(tblImaging);
                    }
                    else
                    {
                        Chunk cNAImaging = new Chunk("No Records found for Imaging.", fTextItalic);
                        dc.Add(cNAImaging);
                    }
                    #endregion Imaging

                    #region Medical Devices

                    // line space
                    Chunk cSpaceMedicalDevices = new Chunk("\r");
                    Phrase phSpaceMedicalDevices = new Phrase();
                    phSpaceMedicalDevices.Add(cSpaceMedicalDevices);
                    dc.Add(phSpaceMedicalDevices);

                    // Medical Devices
                    Chunk cLabelMedicalDevices = new Chunk("DME", fLabel);
                    Phrase phMedicalDevices = new Phrase();
                    phMedicalDevices.Add(cLabelMedicalDevices);
                    Paragraph pMedicalDevices = new Paragraph();
                    pMedicalDevices.Add(phMedicalDevices);
                    dc.Add(phMedicalDevices);

                    // line space
                    Chunk cSpaceMedicalDevices1 = new Chunk("\r");
                    dc.Add(cSpaceMedicalDevices1);

                    iTextSharp.text.Table tblMedicalDevices = new iTextSharp.text.Table(5);
                    tblMedicalDevices.Width = 105;
                    tblMedicalDevices.BorderWidth = 0;
                    //tblProviders.BorderColor = Color.BLACK;
                    tblMedicalDevices.Padding = 3;
                    tblMedicalDevices.Spacing = 1;
                    Cell cMedicalDevices1 = new Cell(new Phrase("Device Type", fLabelNormal));
                    cMedicalDevices1.HorizontalAlignment = 1;
                    Cell cMedicalDevices2 = new Cell(new Phrase("Doctor", fLabelNormal));
                    cMedicalDevices2.HorizontalAlignment = 1;
                    Cell cMedicalDevices3 = new Cell(new Phrase("Service", fLabelNormal));
                    cMedicalDevices3.HorizontalAlignment = 1;
                    Cell cMedicalDevices4 = new Cell(new Phrase("Date", fLabelNormal));
                    cMedicalDevices4.HorizontalAlignment = 1;
                    Cell cMedicalDevices5 = new Cell(new Phrase("Reason", fLabelNormal));
                    cMedicalDevices5.HorizontalAlignment = 1;

                    tblMedicalDevices.AddCell(cMedicalDevices1);
                    tblMedicalDevices.AddCell(cMedicalDevices2);
                    tblMedicalDevices.AddCell(cMedicalDevices3);
                    tblMedicalDevices.AddCell(cMedicalDevices4);
                    tblMedicalDevices.AddCell(cMedicalDevices5);

                    uri = Utility.GetServiceUrl("medicaldevices");
                    postData = new IgJObject();
                    postData.Add("PatientID", iPatient);
                    List<MedicalDevices> iGHPMedicalDevicesInfo = Utility.PostRequest<MedicalDevices>(uri, postData.ToString(Formatting.None));

                    //List<MedicalDevices> iGHPMedicalDevicesInfo = null;
                    //uri = ConfigurationManager.AppSettings["serviceURL"] + "/medicaldevices";
                    //postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + iPatient + "\"}";
                    //iGHPMedicalDevicesInfo = Utility.PostRequest<MedicalDevices>(uri, postData);

                    if (iGHPMedicalDevicesInfo.Count > 0)
                    {
                        for (int i = 0; i < iGHPMedicalDevicesInfo.Count; i++)
                        {
                            tblMedicalDevices.AddCell(new Phrase(Convert.ToString(iGHPMedicalDevicesInfo[i].DeviceType), fText));
                            tblMedicalDevices.AddCell(new Phrase(Convert.ToString(iGHPMedicalDevicesInfo[i].Doctor), fText));
                            tblMedicalDevices.AddCell(new Phrase(Convert.ToString(iGHPMedicalDevicesInfo[i].Hospital), fText));
                            tblMedicalDevices.AddCell(new Phrase(Convert.ToString(iGHPMedicalDevicesInfo[i].Date), fText));
                            tblMedicalDevices.AddCell(new Phrase(Convert.ToString(iGHPMedicalDevicesInfo[i].Reason), fText));
                        }

                        dc.Add(tblMedicalDevices);
                    }
                    else
                    {
                        dc.Add(tblMedicalDevices);

                        Chunk cNAMedicalDevices = new Chunk("No Records found for Medical Devices.", fTextItalic);
                        dc.Add(cNAMedicalDevices);
                    }

                    #endregion

                    #region Physical/Occupation Therapy

                    // line space
                    Chunk cSpacePhysicalTherapy = new Chunk("\r");
                    Phrase phSpacePhysicalTherapy = new Phrase();
                    phSpacePhysicalTherapy.Add(cSpacePhysicalTherapy);
                    dc.Add(phSpacePhysicalTherapy);

                    // Physical Therapy
                    Chunk cLabelPhysicalTherapy = new Chunk("Physical/Occupation Therapy", fLabel);
                    Phrase phPhysicalTherapy = new Phrase();
                    phPhysicalTherapy.Add(cLabelPhysicalTherapy);
                    Paragraph pPhysicalTherapy = new Paragraph();
                    pPhysicalTherapy.Add(phPhysicalTherapy);
                    dc.Add(phPhysicalTherapy);

                    // line space
                    Chunk cSpacePhysicalTherapy1 = new Chunk("\r");
                    dc.Add(cSpacePhysicalTherapy1);

                    iTextSharp.text.Table tblPhysicalTherapy = new iTextSharp.text.Table(5);
                    tblPhysicalTherapy.Width = 105;
                    tblPhysicalTherapy.BorderWidth = 0;
                    //tblPhysicalTherapy.BorderColor = Color.BLACK;
                    tblPhysicalTherapy.Padding = 3;
                    tblPhysicalTherapy.Spacing = 1;
                    Cell cPhysicalTherapy1 = new Cell(new Phrase("Therapy Type", fLabelNormal));
                    cPhysicalTherapy1.HorizontalAlignment = 1;
                    Cell cPhysicalTherapy2 = new Cell(new Phrase("Start Date", fLabelNormal));
                    cPhysicalTherapy2.HorizontalAlignment = 1;
                    Cell cPhysicalTherapy3 = new Cell(new Phrase("Stop Date", fLabelNormal));
                    cPhysicalTherapy3.HorizontalAlignment = 1;
                    Cell cPhysicalTherapy4 = new Cell(new Phrase("Frequency", fLabelNormal));
                    cPhysicalTherapy4.HorizontalAlignment = 1;
                    Cell cPhysicalTherapy5 = new Cell(new Phrase("Therapist", fLabelNormal));
                    cPhysicalTherapy5.HorizontalAlignment = 1;

                    tblPhysicalTherapy.AddCell(cPhysicalTherapy1);
                    tblPhysicalTherapy.AddCell(cPhysicalTherapy2);
                    tblPhysicalTherapy.AddCell(cPhysicalTherapy3);
                    tblPhysicalTherapy.AddCell(cPhysicalTherapy4);
                    tblPhysicalTherapy.AddCell(cPhysicalTherapy5);

                    uri = Utility.GetServiceUrl("physicaltherapy");
                    postData = new IgJObject();
                    postData.Add("PatientID", iPatient);
                    List<PhysicalTherapy> iGHPPhysicalTherapyInfo = Utility.PostRequest<PhysicalTherapy>(uri, postData.ToString(Formatting.None));

                    //List<PhysicalTherapy> iGHPPhysicalTherapyInfo = null;
                    //uri = ConfigurationManager.AppSettings["serviceURL"] + "/physicaltherapy";
                    //postData = "{\"Pin\":\"" + PinValue + "\",\"PatientID\":\"" + iPatient + "\"}";
                    //iGHPPhysicalTherapyInfo = Utility.PostRequest<PhysicalTherapy>(uri, postData);

                    if (iGHPPhysicalTherapyInfo.Count > 0)
                    {
                        for (int i = 0; i < iGHPPhysicalTherapyInfo.Count; i++)
                        {
                            tblPhysicalTherapy.AddCell(new Phrase(Convert.ToString(iGHPPhysicalTherapyInfo[i].TherapyType), fText));
                            tblPhysicalTherapy.AddCell(new Phrase(Convert.ToString(iGHPPhysicalTherapyInfo[i].StartDate), fText));
                            tblPhysicalTherapy.AddCell(new Phrase(Convert.ToString(iGHPPhysicalTherapyInfo[i].StopDate), fText));
                            tblPhysicalTherapy.AddCell(new Phrase(Convert.ToString(iGHPPhysicalTherapyInfo[i].Frequency), fText));
                            tblPhysicalTherapy.AddCell(new Phrase(Convert.ToString(iGHPPhysicalTherapyInfo[i].Therapist), fText));
                        }

                        dc.Add(tblPhysicalTherapy);
                    }
                    else
                    {
                        dc.Add(tblPhysicalTherapy);

                        Chunk cNAPhysicalTherapy = new Chunk("No Records found for Physical/Occupation Therapy.", fTextItalic);
                        dc.Add(cNAPhysicalTherapy);
                    }


                    #endregion

                    dc.Close();
                }
            }
        }

        public PdfPCell GetCell(string text)
        {
            return GetCell(text, 1, 1);
        }

        public PdfPCell GetCell(string text, string text1)
        {
            return GetCell(text, text1, 1, 1);
        }

        public PdfPCell GetCell(string text, int colSpan, int rowSpan)
        {
            BaseFont bfFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            Font fLabel = new Font(bfFont, 10, Font.BOLD, Color.BLACK);
            Font fText = new Font(bfFont, 10, Font.NORMAL, Color.BLACK);

            PdfPCell cell = new PdfPCell(new Phrase(text, fText));
            cell.HorizontalAlignment = 0;
            //cell.Padding = 10f;
            cell.PaddingBottom = 5f;

            //cell.Width = 10;

            cell.Colspan = colSpan;
            return cell;
        }

        public PdfPCell GetBoldCell(string text, int colSpan, int rowSpan)
        {
            BaseFont bfFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            Font fLabel = new Font(bfFont, 10, Font.BOLD, Color.BLACK);
            Font fText = new Font(bfFont, 10, Font.NORMAL, Color.BLACK);

            PdfPCell cell = new PdfPCell(new Phrase(text, fLabel));
            cell.HorizontalAlignment = 1;

            cell.PaddingBottom = 5f;

            //cell.Width = 10;

            cell.Colspan = colSpan;
            return cell;
        }

        public PdfPCell GetInnerCell(string text, string text1, int colSpan, int rowSpan)
        {
            BaseFont bfFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            Font fLabel = new Font(bfFont, 10, Font.NORMAL, Color.BLACK);
            Font fLabelNormal = new Font(bfFont, 10, Font.NORMAL, Color.BLACK);
            Font fText = new Font(bfFont, 10, Font.NORMAL, Color.BLACK);
            Font fText1 = new Font(bfFont, 10, Font.NORMAL, Color.BLACK);

            PdfPCell cell = new PdfPCell();
            cell.Border = 0;
            //cell.AddElement(new Phrase(text, fText));
            //cell.AddElement(new Phrase(text1, fText1));

            Chunk cLabelHeading = new Chunk(text, fLabelNormal);
            Chunk cLabelHeading1 = new Chunk(text1, fText1);

            Phrase phFirstLine = new Phrase();

            phFirstLine.Add(cLabelHeading);
            phFirstLine.Add(cLabelHeading1);

            cell.AddElement(phFirstLine);

            cell.HorizontalAlignment = 0;
            cell.VerticalAlignment = 1;
            //cell.Padding = 10f;
            cell.PaddingBottom = 5f;
            cell.PaddingTop = 5f;

            //cell.Rowspan = rowSpan;  
            cell.Colspan = colSpan;
            return cell;
        }

        public PdfPCell GetCell(string text, string text1, int colSpan, int rowSpan)
        {
            BaseFont bfFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            Font fLabel = new Font(bfFont, 10, Font.BOLD, Color.BLACK);
            Font fLabelNormal = new Font(bfFont, 10, Font.BOLD, Color.BLACK);
            Font fText = new Font(bfFont, 10, Font.BOLD, Color.BLACK);
            Font fText1 = new Font(bfFont, 10, Font.NORMAL, Color.BLACK);

            PdfPCell cell = new PdfPCell();

            //cell.AddElement(new Phrase(text, fText));
            //cell.AddElement(new Phrase(text1, fText1));

            Chunk cLabelHeading = new Chunk(text, fLabelNormal);
            Chunk cLabelHeading1 = new Chunk(text1, fText1);

            Phrase phFirstLine = new Phrase();

            phFirstLine.Add(cLabelHeading);
            phFirstLine.Add(cLabelHeading1);

            cell.AddElement(phFirstLine);

            cell.HorizontalAlignment = 0;
            cell.VerticalAlignment = 1;
            //cell.Padding = 10f;
            cell.PaddingBottom = 5f;
            cell.PaddingTop = 5f;

            //cell.Rowspan = rowSpan;  
            cell.Colspan = colSpan;
            return cell;
        }

        public PdfPCell GetCellImage(iTextSharp.text.Image image)
        {
            PdfPCell cell = new PdfPCell(image);
            cell.Border = 0;

            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;

            cell.PaddingTop = 10f;
            cell.PaddingBottom = 5f;

            //cell.FixedHeight = 10;

            return cell;
        }

        public PdfPCell GetCellText(string text, string text1, int colSpan, int rowSpan)
        {
            BaseFont bfFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            Font fLabel = new Font(bfFont, 10, Font.BOLD, Color.BLACK);
            Font fText = new Font(bfFont, 10, Font.NORMAL, Color.BLACK);
            Font fText1 = new Font(bfFont, 10, Font.NORMAL, Color.BLACK);

            PdfPCell cell = new PdfPCell();
            cell.Border = 0;

            Chunk cLabelHeading = new Chunk(text, fText);

            Phrase phFirstLine = new Phrase();

            phFirstLine.Add(cLabelHeading);

            cell.AddElement(phFirstLine);

            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;

            cell.Colspan = colSpan;
            return cell;
        }

        protected void save_vitals_Click(object sender, System.EventArgs e)
        {
            try
            {
                string PIN = ConfigurationManager.AppSettings["Bridge_Base"];
                var uri = Utility.GetServiceUrl("updatevitalsigns");

                IgJObject postData = new IgJObject();
                postData.Add("PatientID", Session["PatientID"].ToString());
                postData.Add("Pin", PIN);
                postData.Add("Height", txt_Height.Text);
                postData.Add("HeightUnits", ddlHeightUnits.SelectedItem.ToString());
                postData.Add("Weight", txt_weight.Text);
                postData.Add("WeightUnits", ddlWeightUnits.SelectedItem.ToString());
                postData.Add("Temperature", txt_temp.Text);
                postData.Add("TemperatureUnit", ddlTemperatureUnit.SelectedItem.ToString());
                postData.Add("Pulse", txt_pulse.Text);
                postData.Add("Respiration", txtRespiration.Text);
                postData.Add("BloodPressure", txtBP.Text + "/" + txtBP2.Text);

                var res = Utility.PostRequestForSave(uri, postData.ToString(Formatting.None));

                //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/updatevitalsigns";
                //var postData = "{\"PatientID\":\"" + Session["PatientID"] + "\",\"Pin\":\"" + PIN + "\",\"Height\":\"" + txt_Height.Text + "\",\"HeightUnits\":\"" + ddlHeightUnits.SelectedItem.ToString() + "\",\"Weight\":\"" + txt_weight.Text + "\",\"WeightUnits\":\"" + ddlWeightUnits.SelectedItem.ToString() + "\",\"Temperature\":\"" + txt_temp.Text + "\",\"TemperatureUnit\":\"" + ddlTemperatureUnit.SelectedItem.ToString() + "\",\"Pulse\":\"" + txt_pulse.Text + "\",\"Respiration\":\"" + txtRespiration.Text + "\",\"BloodPressure\":\"" + txtBP.Text + "/" + txtBP2.Text + "\"}";
                //var res = Utility.PostRequestForSave(uri, postData);
                if (res == "1")
                {
                    lbl_msg.Text = "Vitals updated successfully";
                    BindVitals(Convert.ToInt32(Session["PatientID"]));
                    BindPatientdetails(Convert.ToInt32(Session["PatientID"]));
                }
                else
                {
                    lbl_msg.Text = "Vitals noy updated successfully";
                    BindVitals(Convert.ToInt32(Session["PatientID"]));
                    BindPatientdetails(Convert.ToInt32(Session["PatientID"]));
                }
              
                
                //ClientScript.RegisterStartupScript(GetType(), "scrpt", "parent.window.location.href=parent.window.location.href;", true);

            }
            catch (Exception exc)
            {

            }
        }
        public DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others 

                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
        protected void btn_submitvitals_Click(object sender, System.EventArgs e)
        {
            try
            {
                List<VitalSignsInfo> objColl = null;


                string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                var uri = Utility.GetServiceUrl("vitalsignsgraph");

                IgJObject postData = new IgJObject();
                postData.Add("PatientID", Session["PatientID"].ToString());
                postData.Add("Pin", PinValue);
                postData.Add("FromDate", txtFromdate.Text);
                postData.Add("ToDate", txtTodate.Text);

                objColl = Utility.PostRequest<VitalSignsInfo>(uri, postData.ToString(Formatting.None));

                //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/vitalsignsgraph";
                //var postData = "{\"PatientID\":\"" + Session["PatientID"] + "\",\"Pin\":\"" + PinValue + "\",\"FromDate\":\"" + txtFromdate.Text + "\",\"ToDate\":\"" + txtTodate.Text + "\"}";
                //objColl = Utility.PostRequest<VitalSignsInfo>(uri, postData);

                if (objColl.Count > 0)
                {
                    Session["groupedData"] = null;
                    var sbDate = new StringBuilder();
                    var sbVital = new StringBuilder();
                    var sbVitalbp = new StringBuilder();
                    string HeightInInches;
                    string WeightInlbs;
                    string TempInFaren;

                    DataTable dt_data = LINQToDataTable(objColl);
                    string vitalsignType = ddlSelectVitals.SelectedItem.ToString();//Request.Form["ddlSelectVitals"].ToString();   
                    int count = dt_data.Rows.Count;
                    for (int i = 0; i < dt_data.Rows.Count; i++)
                    {
                        if (vitalsignType.ToLower().Equals("height") && !string.IsNullOrEmpty(dt_data.Rows[i]["Height"].ToString()))
                        {

                            if (dt_data.Rows[i]["HeightUnits"].ToString().ToLower().StartsWith("c"))
                            {
                                HeightInInches = (Math.Round((Convert.ToDouble(dt_data.Rows[i]["Height"].ToString())) * 0.393701, 1)).ToString();
                                sbVital.Append(",").Append(HeightInInches);
                            }
                            else
                            {
                                sbVital.Append(",").Append(dt_data.Rows[i]["Height"].ToString());
                            }
                            sbDate.Append(",").Append(dt_data.Rows[i]["CreatedDate"].ToString().Replace(",", ""));
                            count--;
                        }
                        else if (vitalsignType.ToLower().Equals("weight") && !string.IsNullOrEmpty(dt_data.Rows[i]["Weight"].ToString()))
                        {
                            if (dt_data.Rows[i]["WeightUnits"].ToString() == "kg")
                            {
                                WeightInlbs = (Math.Round((Convert.ToDouble(dt_data.Rows[i]["Weight"].ToString())) * 2.20462, 1)).ToString();
                                sbVital.Append(",").Append(WeightInlbs);
                            }
                            else
                            {
                                sbVital.Append(",").Append(dt_data.Rows[i]["Weight"].ToString());
                            }

                            sbDate.Append(",").Append(dt_data.Rows[i]["CreatedDate"].ToString().Replace(",", ""));
                            count--;
                        }
                        else if (vitalsignType.ToLower().Equals("temperature") && !string.IsNullOrEmpty(dt_data.Rows[i]["Temperature"].ToString()))
                        {

                            if (dt_data.Rows[i]["TemperatureUnit"].ToString().ToLower().StartsWith("c"))
                            {
                                TempInFaren = (Math.Round((Convert.ToDouble(dt_data.Rows[i]["Temperature"].ToString())) * 9 / 5 + 32, 1)).ToString();
                                sbVital.Append(",").Append(TempInFaren);
                            }
                            else
                            {
                                sbVital.Append(",").Append(dt_data.Rows[i]["Temperature"].ToString());
                            }

                            sbDate.Append(",").Append(dt_data.Rows[i]["CreatedDate"].ToString().Replace(",", ""));
                            count--;
                        }
                        else if (vitalsignType.ToLower().Equals("pulse") && !string.IsNullOrEmpty(dt_data.Rows[i]["Pulse"].ToString()))
                        {
                            sbVital.Append(",").Append(dt_data.Rows[i]["Pulse"].ToString());

                            sbDate.Append(",").Append(dt_data.Rows[i]["CreatedDate"].ToString().Replace(",", ""));
                            count--;
                        }
                        else if (vitalsignType.ToLower().Trim().Equals("respiration") && !string.IsNullOrEmpty(dt_data.Rows[i]["Respiration"].ToString()))
                        {
                            sbVital.Append(",").Append(dt_data.Rows[i]["Respiration"].ToString());

                            sbDate.Append(",").Append(dt_data.Rows[i]["CreatedDate"].ToString().Replace(",", ""));
                            count--;
                        }
                        else if (vitalsignType.ToLower().Equals("blood pressure") && !string.IsNullOrEmpty(dt_data.Rows[i]["BloodPressure"].ToString()))
                        {
                            string[] BP = dt_data.Rows[i]["BloodPressure"].ToString().Split('/');
                            sbVital.Append(",").Append(BP[0].ToString());
                            sbVitalbp.Append(",").Append(BP[1].ToString());

                            sbDate.Append(",").Append(dt_data.Rows[i]["CreatedDate"].ToString().Replace(",", ""));
                            count--;
                        }

                    }
                    if (count == dt_data.Rows.Count)
                    {
                        //  lblGraphResult.Text = "No data found for the selected criteria.";
                        //  lblGraphResult.Style.Value = "color:Red";
                    }
                    else
                    {
                        //string Date = sbDate.ToString().TrimStart(',').TrimEnd(',');
                        //string VitalVal = sbVital.ToString().TrimStart(',').TrimEnd(',');
                        //Session["groupedData"] = Date + "|" + VitalVal;

                        //Page.RegisterStartupScript("validation", "<script type=text/javascript> bind_barchart();</script>");

                        string Date = sbDate.ToString().TrimStart(',').TrimEnd(',');
                        string VitalVal = sbVital.ToString().TrimStart(',').TrimEnd(',');
                        string Vitalbpvalue = sbVitalbp.ToString().TrimStart(',').TrimEnd(',');
                        if (ddlSelectVitals.SelectedItem.ToString() == "Blood Pressure")
                        {
                            Session["groupedData"] = Date + "|" + VitalVal + "|" + Vitalbpvalue;
                            Page.RegisterStartupScript("validation", "<script type=text/javascript> bind_linechartforbp();</script>");
                        }
                        else
                        {
                            Session["groupedData"] = Date + "|" + VitalVal;
                            Page.RegisterStartupScript("validation", "<script type=text/javascript> bind_barchart();</script>");
                        }

                    }

                }
                else
                {
                    // lblGraphResult.Text = "No data found for the selected criteria.";
                    //lblGraphResult.Style.Value = "color:Red";
                }


            }

            catch (Exception exc)
            {

            }
        }
    }
}