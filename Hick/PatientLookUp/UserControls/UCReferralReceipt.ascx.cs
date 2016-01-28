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
    public partial class UCReferralReceipt : System.Web.UI.UserControl
    {
        static int referralid = 0;
        static int redirectflag;
        protected void Page_Load(object sender, EventArgs e)
        {

             if (Request.QueryString["ReffId"] != null)
             {
                 referralid = int.Parse(Request.QueryString["ReffId"].ToString());
                 string RefId = Request.QueryString["ReffId"];
               //  hdnReferralID.Value = RefId;
                // hdnTodayOrPendingReferrals.Value = Request.QueryString["todayOrPendingFlaf"];
                 FetchReferralInformation(RefId);
                 FetchPatientViewDetails(RefId);
           
                 //FetchProviderMessage(RefId, "-1");
             }
        }

        public void FetchReferralInformation(string refid)
        {
            List<PhysicianDetails> objColl = null;
             if (refid != null)
             {
                 string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                var uri = Utility.GetServiceUrl("fetchphysicianlocation");

                IgJObject postData = new IgJObject();
                postData.Add("ReferralID", refid);
                postData.Add("Pin", PinValue);

                objColl = Utility.PostRequest<PhysicianDetails>(uri, postData.ToString(Formatting.None));

                //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/fetchphysicianlocation";
                // var postData = "{\"ReferralID\":\"" + refid + "\",\"Pin\":\"" + PinValue + "\"}";
                // objColl = Utility.PostRequest<PhysicianDetails>(uri, postData);
                 if (objColl.Count > 0)
                 {
                     var sb = string.Empty;
                     var phone = string.Empty;
                     var fax = string.Empty;
                     for (var i = 0; i < objColl.Count; i++)
                     {
                         var elementAtOrDefault = objColl.ElementAtOrDefault(i);
                         if (elementAtOrDefault != null)
                         {
                             if (!string.IsNullOrEmpty(elementAtOrDefault.Address))
                             {
                                 if (!string.IsNullOrEmpty(sb))
                                     sb = sb + "<br/><br/>" + elementAtOrDefault.Address;
                                 else
                                     sb = elementAtOrDefault.Address;
                             }
                             if (!string.IsNullOrEmpty(elementAtOrDefault.ContactPhNo))
                             {
                                 if (!string.IsNullOrEmpty(phone))
                                     phone = phone + "<br/>" + elementAtOrDefault.PhyOfficeName + "<br/>" + " (" +
                                             elementAtOrDefault.ContactPhNo + ")";
                                 else
                                     phone = elementAtOrDefault.PhyOfficeName + "<br/>" + " (" + elementAtOrDefault.ContactPhNo +
                                             ")";

                             }
                             if (!string.IsNullOrEmpty(elementAtOrDefault.FaxNo))
                             {
                                 if (!string.IsNullOrEmpty(fax))
                                     fax = fax + "<br/>" + elementAtOrDefault.PhyOfficeName + "<br/>" + " (" +
                                           elementAtOrDefault.FaxNo + ")";
                                 else
                                     fax = elementAtOrDefault.PhyOfficeName + "<br/>" + " (" + elementAtOrDefault.FaxNo +
                                           ")";

                             }
                         }
                     }
                     lblProviderAdd.Text = sb;
                     lblPhoneNum.Text = phone;
                     ui_lblFaxNum.Text = fax;
                 }
                 else
                 {
                    uri = Utility.GetServiceUrl("fetchphysiciandetails");
                    postData = new IgJObject();
                    postData.Add("PhysicianID", Convert.ToString(Session["PhysicianID"]));
                    List<PhysicalTherapy> iGHPPhysicalTherapyInfo = Utility.PostRequest<PhysicalTherapy>(uri, postData.ToString(Formatting.None));

                    //PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                    //  uri = ConfigurationManager.AppSettings["serviceURL"] + "/fetchphysiciandetails";
                    //  postData = "{\"PhysicianID\":\"" + Session["PhysicianID"] + "\",\"Pin\":\"" + PinValue + "\"}";
                    //  objColl = Utility.PostRequest<PhysicianDetails>(uri, postData);


                      if (objColl.Count > 0)
                      {
                          lblProviderAdd.Text = objColl[0].Address;
                          lblPhoneNum.Text = objColl[0].ContactOfficePhNo;
                          ui_lblFaxNum.Text = objColl[0].FaxNo;
                      }
                 }
             }
        }

        public void FetchPatientViewDetails(string refid)
        {
            List<PatientDetails> objColl = null;
            if (refid != null)
            {
                //string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                var uri = Utility.GetServiceUrl("fetchpatientviewdetailsbyrefid");

                IgJObject postData = new IgJObject();
                postData.Add("ReferralID", refid);
                //postData.Add("Pin", PinValue);

                objColl = Utility.PostRequest<PatientDetails>(uri, postData.ToString(Formatting.None));

                //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/fetchpatientviewdetailsbyrefid";
                //var postData = "{\"ReferralID\":\"" + refid + "\",\"Pin\":\"" + PinValue + "\"}";
                //objColl = Utility.PostRequest<PatientDetails>(uri, postData);
                if (objColl.Count > 0)
                {
                    lblFirstName.Text = objColl[0].FirstName;
                    lblLastName.Text = objColl[0].LastName;
                    //hdnPatientId.Value = objpatient.PatientId.ToString();
                    lblReferralID.Text = refid.ToString(); ;
                   // lblAssignedPhysician.Text = objColl[0].AssignedPhysician;

                    string Date = string.Empty;
                    string Date3 = string.Empty;
                    lblDOB.Text = objColl[0].DOB;
                    lblEmail.Text = objColl[0].EmailId;
                    lblssn.Text = objColl[0].Last4SSN;
                    lblAddress.Text = objColl[0].Address1;
                    lblcity.Text = objColl[0].City;
                    lblState.Text = objColl[0].State;
                    lblZip.Text = objColl[0].Zipcode;
                    if (objColl[0].SelfPay.ToLower() == "yes")
                    {
                        lblInsurance.Text = "Self pay";
                    }
                    else if (objColl[0].SelfPay.ToLower() == "no")
                    {
                        lblInsurance.Text = objColl[0].PrimaryInsuranceCompany;
                    }
                    lblInsurance.Text = objColl[0].PrimaryInsuranceCompany;
                    //lblPrimary.Text = objColl[0].PrimaryFinancialClass;
                    lblCompanyname.Text = objColl[0].PrimaryInsuranceCompany;
                    lblPolicyName.Text = objColl[0].OriginalPolicyNumber;
                    lblPhone.Text = objColl[0].PhoneNumber;
                    lblAssignedPhysician.Text = objColl[0].AssignedPhysician;


                    lblRequestedBy.Text = objColl[0].ReferredByPhyscianOfficeName;
                    lblOdrPhysician.Text = objColl[0].AssignedPhyscianFirstName + " " + objColl[0].AssignedPhyscianLastName;
                    //lblOrderingOhysician.Text = objpatient.ReferredByPhyscianOfficeName; //objpatient.AssignedPhyscianFirstName + ' ' + objpatient.AssignedPhyscianLastName;
                    //lblReferredTo.Text = objColl[0].ReferredPhyscianFirstName + ' ' + objColl[0].ReferredPhyscianLastName + ',' + ' ' + objColl[0].PhyOfficeName;
                   // lblGroupNum.Text = objColl[0].GroupNumber;

                    if (objColl[0].ApptDateTimePref1 != "")
                    {
                        //string formatString = "yyyy-MM-dd";
                        //string sampledate = objpatient.ApptDateTimePref1;
                        //DateTime apptdate1 = DateTime.ParseExact(sampledate, formatString, null);
                        lblAppointmentDate.Text = objColl[0].ApptDateTimePref1;
                       
                    }
                    else
                    {
                        lblAppointmentDate.Text = objColl[0].ApptDateTimePref1;
                       
                    }
                    lblAppointmentTime.Text = objColl[0].ApptTime_Pref1;
                    lblAddAppointementNotes.Text = objColl[0].AddAppNotes;
                }
            }
        }
    }
}