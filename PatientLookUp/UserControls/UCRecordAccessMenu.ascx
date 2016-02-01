<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCRecordAccessMenu.ascx.cs" Inherits="Hick.PatientLookUp.UserControls.UCRecordAccessMenu" %>
                <div style="display: inline-flex;">
                    <div class="search_img_label" style="width: 36px; height: 36px;">
                        <img src="../../Images/folder_03.png" alt="search_img" style="width: 100%; height: 100%;
                            display: none;" id="imgsearchuser" />
                        <img src="../../Images/icon_refresh.png" alt="timermanagement_img" style="display: none;"
                            id="timermanagement" />
                        <img src="../../Images/icon_details.png" alt="commandcenter_img" style="display: none;
                            width: 34px; height: 36px;" id="commandcenter" />
                    </div>
                    <div class="search_label" id="div_patientsearch" style="display: none;">
                        My Record
                    </div>
                </div>
                <div class="left_portion_II" style="display: none;" id="patientsearch_leftpart">
                    <div class="patsearch_listbox">
                        <ul class="patient_search">
                              <li pagekey="recordaccess"><a href="../../PatientLookUp/ASPX/Recordaccess.aspx?pageview=record">Record Access</a></li>
                              <li pagekey="php"><a href="../../PatientLookUp/ASPX/PatientPhpSummary.aspx?pageview=record">PHP View</a>
              </li>
                        </ul>
                    </div>
                    <div class="patsearch_emptygray">
                    </div>
                </div>
