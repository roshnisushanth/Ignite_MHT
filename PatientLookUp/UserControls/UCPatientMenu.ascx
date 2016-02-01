<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCPatientMenu.ascx.cs" Inherits="Hick.PatientLookUp.UserControls.UCPatientMenu" %>
                <div style="display: inline-flex;">
                    <div class="search_img_label" style="width: 36px; height: 36px;">
                        <img src="../../Images/search_icon.png" alt="search_img" style="width: 100%; height: 100%;
                            display: none;" id="imgsearchuser" />
                        <img src="../../Images/icon_refresh.png" alt="timermanagement_img" style="display: none;"
                            id="timermanagement" />
                        <img src="../../Images/icon_details.png" alt="commandcenter_img" style="display: none;
                            width: 34px; height: 36px;" id="commandcenter" />
                    </div>
                    <div class="search_label" id="div_patientsearch" style="display: none;">
                        Patient Search
                    </div>
                    <%-- <div class="search_label" id="div_Referral" style="display: none;">
                   Referral
               </div>
               <div class="search_label" id="div_careplan" style="display: none;">
                   Care Plan
               </div>--%>
                    <div class="search_label" id="div_timermanagement" style="display: none;">
                        Timer Management
                    </div>
                    <div class="search_label" id="div_command" style="display: none;">
                        Command Center
                    </div>
                </div>
                <div class="left_portion_II" style="display: none;" id="patientsearch_leftpart">
                    <div class="patsearch_listbox">
                        <ul class="patient_search">
                            <li pagekey="patientsearch" class="patient-search"><a href="../../PatientLookUp/ASPX/PatientSearch.aspx">Patient
                                Search/List</a></li>
                            <%--  <li pagekey="patientlist"><a href="../../PatientLookUp/ASPX/PatientList.aspx">Patient
                           List</a></li>--%>
                    
                         <%--   <li pagekey="activitylog"><a href="../../PatientLookUp/ASPX/activitylog.aspx">Activity Log</a></li>--%>
                            <li pagekey="php"><a href="../../PatientLookUp/ASPX/PhpSummary.aspx">PHP View</a></li>
                            <ul class="patient_search">
                                <li pagekey="demographics" style="padding-bottom:5px;"><a href="../../PatientLookUp/ASPX/EditDemographics.aspx">Demographics</a></li>
                                <li pagekey="medications" style="padding-bottom:5px;"><a href="../../PatientLookUp/ASPX/Medications.aspx">Medications</a></li>
                                <li pagekey="allergies" style="padding-bottom:5px;"><a href="../../PatientLookUp/ASPX/Allergies.aspx">Allergies</a></li>
                                <li pagekey="conditions" style="padding-bottom:5px;"><a href="../../PatientLookUp/ASPX/Conditions.aspx">Conditions</a></li>
                                <li pagekey="problems" style="padding-bottom:5px;"><a href="../../PatientLookUp/ASPX/EditSocialHistory.aspx">Social History</a></li>
                               
                                <li pagekey="familyhistory" style="padding-bottom:5px;"><a href="../../PatientLookUp/ASPX/FamilyHistory.aspx">Family History</a></li>
                                <li pagekey="encounters" style="padding-bottom:5px;"><a href="../../PatientLookUp/ASPX/Encounters.aspx">Encounters</a></li>
                                <li pagekey="referrals" style="padding-bottom:5px;"><a href="../../PatientLookUp/ASPX/ReferralView.aspx">Referrals</a></li>
                                <li pagekey="immunizations" style="padding-bottom:5px;"><a href="../../PatientLookUp/ASPX/Immunizations.aspx">Immunizations</a></li>
                                 <li pagekey="testandprocedure" style="padding-bottom:5px;"><a href="../../PatientLookUp/ASPX/TestAndProcedures.aspx">Tests and Procedures</a></li>
                                  <li pagekey="labresult" style="padding-bottom:5px;"><a href="../../PatientLookUp/ASPX/LabResults.aspx">Lab Results</a></li>
                               <li pagekey="clinicaldocument" style="padding-bottom:5px;"><a href="../../PatientLookUp/ASPX/ClinicalDocuments.aspx">Clinical Documents</a></li>
                               
                            </ul>
                            
                            
                        </ul>
                    </div>
                    <div class="patsearch_emptygray">
                    </div>
                </div>
                <div class="left_portion_II" style="display: none;" id="command_leftpart">
                    <div class="patsearch_listbox">
                        <ul class="patient_search">
                            <li pagekey="dashboard"><a href="../ASPX/Dashboard.aspx">Dashboard</a> </li>
                            <li pagekey="ccpatientlist"><a href="../ASPX/PLPatientList.aspx">Patient List</a>
                                <ul class="patient_search">
                                    <li pagekey="complete"><a href="../ASPX/PLComplete.aspx">Complete</a></li>
                                    <li pagekey="notcomplete"><a href="../ASPX/PLNotComplete.aspx">Not complete</a></li>
                                </ul>
                            </li>
                            <li pagekey="dr_retting_billing"><a href="../ASPX/BillingReport.aspx">Billing Reports</a></li>
                            <li pagekey="dr_retting_consent"><a href="../ASPX/ConsentReport.aspx">Consent Report</a></li>
                        </ul>
                    </div>
                    <div class="patsearch_emptygray">
                    </div>
                </div>