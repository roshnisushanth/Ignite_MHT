<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCPhpSummary.ascx.cs" 
    Inherits="Hick.PatientLookUp.UserControls.UCPhpSummary" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head> <script src="../../Scripts/HighCharts/highcharts.js" type="text/javascript"></script>

    <script type="text/javascript" src="https://code.highcharts.com/highcharts-more.js"></script>
    <script type="text/javascript" src="https://code.highcharts.com/modules/solid-gauge.js"></script>
   
    <link href="../../Content/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap-theme.min.css">
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.4.4.min.js"></script>
      
    <style>
        .col-lg-4, .col-md-4 {
            padding-right: 5px;
            padding-left: 5px;
        }

        .col-lg-12.col-md-12.hdr.summ.head .col-lg-4.col-md-4 {
            width: 190px;
            float: left;
            padding: 0;
        }
        .col-lg-1.col-md-1.boxs.pull-right.text-center {
            width: 51px;
            float: left !important;
            font-size: 12px;
        }
    </style>

    <script type="text/javascript">
        $(function () {
            //  debugger;
            //var tableOffset = $("#grdconditions").offset().top;
            //var $header = $("#grdconditions > tr:first-child").clone();
            //var $fixedHeader = $("#header-fixed").append($header);

            //$(window).bind("scroll", function () {
            //    var offset = $(this).scrollTop();

            //    if (offset >= tableOffset && $fixedHeader.is(":hidden")) {
            //        $fixedHeader.show();
            //    }
            //    else if (offset < tableOffset) {
            //        $fixedHeader.hide();
            //    }
            //});
        });
    </script>
</head>
<body>
    
 
    
    <div class="patsearch_heading patient">
        PHP Summary
        <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popup_close" class="pull-right" style="cursor: pointer;margin-top:-5px;"
                alt="close" />
    </div>

        <div class="patsearch_border">
        <%--div for profile pic and info--%>
        <div class = "col-lg-12 col-md-12 hdr summ head" style="margin-bottom:20px;">
            <div class = "col-lg-2 col-md-2 prf usr-icon">
                <img src="../../Images/default_user.png" class="prf_pic" id="prf_pic" alt="Profile Pic" width="60px" height="60px" />
            </div>
             <div class="col-lg-4 col-md-4">
                 <div class="usr_name">
                         <asp:Label CssClass="nme" ID="lblpatname" runat="server"></asp:Label><br  />
                  <%--  <span class="log_time ">Last Logged in January 02,2015</span>--%>
                      <asp:Label CssClass="nme" ID="lblLastLoggedin" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-lg-1 col-md-1 boxs  pull-right text-center" style="height:auto;line-height:normal;">
                 <span class="tp_hdr ">BP</span><br  /><br  />
                  <asp:Label CssClass="btm_summary" ID="lblBP" runat="server"></asp:Label>
            </div>
            <div class="col-lg-1 col-md-1 boxs pull-right text-center" style="height:auto;line-height:normal;">
                <span class="tp_hdr ">Age</span><br  /><br  />
                  <asp:Label CssClass="btm_summary" ID="lblAge" runat="server"></asp:Label>
            </div>
             <div class="col-lg-1 col-md-1 boxs pull-right text-center" style="height:auto;line-height:normal;">
                <span class="tp_hdr ">Gender</span><br  /><br  />
                 <asp:Label CssClass="btm_summary" ID="lblgender" runat="server"></asp:Label>
            </div>
             <div class="col-lg-1 col-md-1 boxs pull-right text-center" style="height:auto;line-height:normal;">
                <span class="tp_hdr ">Weight</span><br  /><br  />
                  <asp:Label CssClass="btm_summary" ID="lblweight" runat="server"></asp:Label>
            </div>
            <div class="col-lg-1 col-md-1 boxs pull-right text-center" style="height:auto;line-height:normal;">
                <span class="tp_hdr ">Height</span><br  /><br  />
                  <asp:Label CssClass="btm_summary" ID="lblheight" runat="server"></asp:Label>
            </div>
</div>
        <div class="col-lg-12 col-md-12 rw1 summ-mid">
            <div class="col-lg-4 col-md-4 ">
                <div class="boxes clearfix">
             
                     <div class="col-lg-12 col-md-12">
                        <h5 class="pull-right"><strong>Medications</strong></h5>
                    </div>
                     <div class="col-lg-8 col-md-8">
                            <asp:Label ID="lblMedicationName" runat="server"></asp:Label><br />
                            <span>Active Since:</span><br />
                            <asp:Label ID="lblMedicationDate" runat="server"></asp:Label>
                      </div>
                    <div class="col-lg-4 col-md-4 text-center">
                         <img src="../../Images/Medications.png" class="med" id="med" alt="med" width="50px" height="40px" /> 
                      </div>
                     <div class="col-lg-12 col-md-12"><br />
                             <asp:Label ID="lblMedicationName2" runat="server"></asp:Label><br />
                            <span>Active Since:</span><br />
                            <asp:Label ID="lblMedicationDate2" runat="server"></asp:Label>
                      </div>
                     <div class="col-lg-12 col-md-12 blk col-lg-7 col-md-7 view-det">
                         <span class="pull-right">
                                <a href="../../PatientLookUp/ASPX/Medications.aspx" >View Details 
                             <img src="../../Images/yellow_arrow.png" width="14px" height="14px"/></a>
                         </span>
                    </div>
                </div>
            </div>
            
             <div class="col-lg-4 col-md-4 ">
                 <div class="boxes clearfix">
                    
                     <div class="col-lg-12 col-md-12">
                        <h5 class="pull-right"><strong>Conditions</strong></h5>
                    </div>
                      <div class="col-lg-8 col-md-8">
                             <asp:Label ID="lblConditionName" runat="server"></asp:Label><br />
                            <span>Active Since:</span><br />
                            <asp:Label ID="lblConditionDate" runat="server"></asp:Label>
                         </div>
                     <div class="col-lg-4 col-md-4 text-center">
                           <img src="../../Images/Conditions.png" class="cndtn" id="cndtn" alt="cndtn" width="50px" height="40px" />
                      </div>
                      <div class="col-lg-12 col-md-12"><br />
                           <asp:Label ID="lblConditionName2" runat="server"></asp:Label><br />
                            <span>Active Since:</span><br />
                           <asp:Label ID="lblConditionDate2" runat="server"></asp:Label>
                      </div>
                   <div class="col-lg-12 col-md-12 view-det">
                      <span class="pull-right">
                                <a href="../../PatientLookUp/ASPX/Conditions.aspx" >View Details 
                             <img src="../../Images/yellow_arrow.png" width="14px" height="14px"/></a>
                         </span>
                    </div>

                     <table id="header-fixed"></table>
                     </div>
            </div>
            
             <div class="col-lg-4 col-md-4 ">
                 <div class="boxes clearfix">
                     <div class="col-lg-12 col-md-12">
                        <h5 class="pull-right"><strong>Results</strong> </h5>
                    </div>
                      <div class="col-lg-8 col-md-8">
                      <asp:Label ID="lblLabResults" runat="server"></asp:Label><br />
                            <span>Active Since:</span>
                            <asp:Label ID="lblLabResultsDate" runat="server"></asp:Label><br />
                        <%--  <span>1 MB</span>--%>
                         </div>
                     <div class="col-lg-4 col-md-4 text-center">
                           <img src="../../Images/Results.png" class="res" id="res" alt="res" width="50px" height="40px" />
                      </div>
            
                 <div class="col-lg-12 col-md-12"><br />
                           <asp:Label ID="Label1" runat="server"></asp:Label><br />
                            <span>Active Since:</span><br />
                           <asp:Label ID="Label2" runat="server"></asp:Label>
                      </div>
                  <div class="col-lg-12 col-md-12 view-det">
                    <span class="pull-right">
                                <a href="../../PatientLookUp/ASPX/LabResults.aspx" >View Details 
                             <img src="../../Images/yellow_arrow.png" width="14px" height="14px"/></a>
                         </span>
                    </div>
                     </div>
          </div>
        </div>

        <div class="col-lg-12 col-md-12">
            <div class="rw2">
             <div class="col-lg-1 col-md-1 text-center">
                  <img src="../../Images/DownloadnShare.png" class="dwnldnshr" id="dwnldnshr" alt="dwnldnshr" width="60px" height="60px" />
             </div>
                <div class="col-lg-11 col-md-11">
                    <h5 class="headin"><strong>Download or Share</strong></h5>
               <div class="blk col-lg-7 col-md-7">
                   <div class="col-lg-5 col-md-5 txt-lft">
                               <span>1.Lab Results</span></div>
                   <div class="col-lg-4 col-md-4">
                               <span>12/1/2011</span></div>
                   <div class="col-lg-3 col-md-3">
                               <span>5 MB</span></div><br />
                   <div class="col-lg-5 col-md-5 txt-lft">
                               <span>2.Php</span></div>
                   <div class="col-lg-4 col-md-4">
                               <span>15/1/2011</span></div>
                   <div class="col-lg-3 col-md-3">
                               <span>10 MB</span></div>
                  </div>
                   <div class="col-lg-4 col-md-4">
                                <span><input id="dwnld" type="button" class="btndwnld" value="Download"></span>
                                <span><input id="shr" type="button" class="btndwnld" value="Share"></span><br \>
                                <asp:Button ID="Button_PHPDownload" runat="server" Text="Download" CssClass="btndwnld"
                                    OnClick="Button_PHPDownload_Click" /></span>
                                <span><input id="shr" type="button" class="btndwnld" value="Share"></span>
                           
                    </div>
                   
             </div>
            
            </div>
        </div>

        <div class="col-lg-12 col-md-12 rw1">
            <div class="col-lg-4 col-md-4 ">
                <div class="boxes">
                     <div class="col-lg-12 col-md-12">
                         <h5 class="pull-right"><strong>Family History</strong></h5>
                    </div>
                     <div class="col-lg-8 col-md-8">
                            <span>Non-Contributory</span><br />
                            <span>Mother:</span><asp:Label ID="lblFamilyHistoryMother" runat="server"></asp:Label><br />
                          <span>Father: </span><asp:Label ID="lblFamilyHistoryFather" runat="server"></asp:Label><br />
                         <span>Sibling: </span><asp:Label ID="lblFamilyHistorySiblings" runat="server"></asp:Label>
                         </div>
                     <div class="col-lg-4 col-md-4 text-center">
                           <img src="../../Images/FamilyHistory.png" class="fmly" id="fmly" alt="fmly" width="50px" height="40px" />
                      </div>
                     
                </div>
                 <div class="col-lg-12 col-md-12 bttm">
                       <span class="pull-right" >
                                <a href="../../PatientLookUp/ASPX/FamilyHistory.aspx" >View Details 
                             <img src="../../Images/yellow_arrow.png" width="14px" height="14px"/></a>
                         </span>
                    </div>
            </div>
            
             <div class="col-lg-4 col-md-4 ">
                 <div class="boxes">
                    <div class="col-lg-12 col-md-12">
                        <h5 class="pull-right"><strong>Social History</strong></h5>
                    </div>
                     <div class="col-lg-8 col-md-8">
                            <span>Non-Contributory</span><br />
                            <span>Tobacco Use:</span><asp:Label ID="lblTobaccoUse" runat="server"></asp:Label><br />
                          <span>Alcohol/Drug Use:</span><asp:Label ID="lblAlcoholUse" runat="server"></asp:Label><br />
                        <%-- <span>Domestic Violence:</span><asp:Label ID="lblViolence" runat="server" Text="N/A"></asp:Label>--%>
                         </div>
                     <div class="col-lg-4 col-md-4 text-center">
                           <img src="../../Images/SocialHistory.png" class="scl" id="scl" alt="scl" width="50px" height="40px" />
                      </div>
                     </div>
                  <div class="col-lg-12 col-md-12 bttm">
                      <span class="pull-right">
                                <a href="../../PatientLookUp/ASPX/EditSocialHistory.aspx" >View Details 
                             <img src="../../Images/yellow_arrow.png" width="14px" height="14px"/></a>
                         </span>
                    </div>
            </div>
            
             <div class="col-lg-4 col-md-4 ">
                 <div class="boxes">
                     <div class="col-lg-12 col-md-12">
                        <h5 class="pull-right"><strong>Immunizations</strong></h5>
                    </div>
                     <div class="col-lg-8 col-md-8">
                          
                             <asp:Label ID="lblImmunization" runat="server" Text=""></asp:Label><br />
                          <span>Active Since: </span><br />
                        <asp:Label ID="lblImmunizationDate" runat="server" Text=""></asp:Label>
                         </div>
                     <div class="col-lg-4 col-md-4 text-center">
                           <img src="../../Images/Immunizations.png" class="imnz" id="imnz" alt="imnz" width="50px" height="40px" />
                      </div>
                     <div class="col-lg-12 col-md-12"><br />
                             <asp:Label ID="lblImmunization2" runat="server" Text=""></asp:Label><br />
                            <span>Active Since:</span><br />
                          <asp:Label ID="lblImmunizationDate2" runat="server" Text=""></asp:Label>
                         
                         </div>
                         <div class="col-lg-12 col-md-12 view-det">
                         <span class="pull-right"  >
                                <a href="../../PatientLookUp/ASPX/Immunizations.aspx" >View Details 
                             <img src="../../Images/yellow_arrow.png" width="14px" height="14px"/></a>
                         </span> 
                         </div>
                     
                    
            </div>
          </div>
        </div>
 

        <div class="phpsummary-sec clearfix">
            <div class="col-md-4 col-sm-4 col-lg-4">
                <div class="boxes clearfix">
                     <div class="col-lg-12 col-md-12">
                        <h5 class="pull-right"><strong>Demographics</strong> </h5>
                    </div>
                      <div class="col-lg-8 col-md-8">
                    <br />
                            <span>DOB:</span> <asp:Label ID="lbldemoDOB" runat="server"></asp:Label>
                           <br />
                        <%--  <span>1 MB</span> --%>
                         </div>
                     <div class="col-lg-4 col-md-4 text-center">
                           <img src="../../Images/Icon-Demographics.png" class="res" id="res" alt="res" width="50px" height="40px" />
                      </div>
            
                 <div class="col-lg-12 col-md-12">
                          <br />
                            <span>Phone No:</span>
                              <asp:Label ID="lbldemophno" runat="server"></asp:Label><br />
                        
                      <asp:Label ID="Label23" runat="server"></asp:Label><br />
                            <span>City, State:</span><br />
                             <asp:Label ID="lbldemocitystate" runat="server"></asp:Label><br />
                          
                      </div>
                  <div class="col-lg-12 col-md-12 view-det">
                    <span class="pull-right">
                                <a href="../../PatientLookUp/ASPX/EditDemographics.aspx" >View Details 
                             <img src="../../Images/yellow_arrow.png" width="14px" height="14px"/></a>
                         </span>
                    </div>
                     </div>
            </div>

            <div class="col-md-4 col-sm-4 col-lg-4">
                <div class="boxes clearfix">
                     <div class="col-lg-12 col-md-12">
                        <h5 class="pull-right"><strong>Allergies</strong> </h5>
                    </div>
                      <div class="col-lg-8 col-md-8">
                      <br />
                           <asp:Label ID="lblallergy1" runat="server"></asp:Label><br />
                           <span>Active Since:</span><br />
                            <asp:Label ID="lblallergydate1" runat="server"></asp:Label>
                         <br />
                        <%--  <span>1 MB</span> --%>
                         </div>
                     <div class="col-lg-4 col-md-4 text-center">
                           <img src="../../Images/Icon-Problems.png" class="res" id="res" alt="res" width="50px" height="40px" />
                      </div>
            
                 <div class="col-lg-12 col-md-12">
                             <asp:Label ID="lblallergy2" runat="server"></asp:Label><br>
                           <span>Active Since:</span><br />
                           <asp:Label ID="lblallergydate2" runat="server"></asp:Label>
                          
                      </div>
                  <div class="col-lg-12 col-md-12 view-det">
                    <span class="pull-right">
                                <a href="../../PatientLookUp/ASPX/Allergies.aspx" >View Details 
                             <img src="../../Images/yellow_arrow.png" width="14px" height="14px"/></a>
                         </span>
                    </div>
                     </div>
            </div>

            <div class="col-md-4 col-sm-4 col-lg-4">
                <div class="boxes clearfix">
                     <div class="col-lg-12 col-md-12">
                        <h5 class="pull-right"><strong>Encounters</strong> </h5>
                    </div>
                      <div class="col-lg-8 col-md-8">
                      <asp:Label ID="Label11" runat="server"></asp:Label><br />
                             <asp:Label ID="lblvisitreason1" runat="server"></asp:Label><br />
                           <span>Active Since:</span><br />
                           <asp:Label ID="lblvisitdate1" runat="server"></asp:Label><br />
                            <asp:Label ID="Label12" runat="server"></asp:Label>
                        <%--  <span>1 MB</span> --%>
                         </div>
                     <div class="col-lg-4 col-md-4 text-center">
                           <img src="../../Images/Icon-Encounters.png" class="res" id="res" alt="res" width="50px" height="40px" />
                      </div>
            
                 <div class="col-lg-12 col-md-12"><br />
                           <asp:Label ID="Label13" runat="server"></asp:Label>
                               <asp:Label ID="lblvisitreason2" runat="server"></asp:Label><br />
                           <span>Active Since:</span><br />
                          <asp:Label ID="lblvisitdate2" runat="server"></asp:Label>
                           <asp:Label ID="Label14" runat="server"></asp:Label>
                      </div>
                  <div class="col-lg-12 col-md-12 view-det">
                    <span class="pull-right">
                                <a href="../../PatientLookUp/ASPX/Encounters.aspx" >View Details 
                             <img src="../../Images/yellow_arrow.png" width="14px" height="14px"/></a>
                         </span>
                    </div>
                     </div>
            </div>

            <div class="col-md-4 col-sm-4 col-lg-4">
                <div class="boxes clearfix">
                     <div class="col-lg-12 col-md-12">
                        <h5 class="pull-right"><strong>Referrals</strong> </h5>
                    </div>
                     <div class="col-lg-8 col-md-8">
                      <asp:Label ID="Label15" runat="server"></asp:Label><br />
                            <span>Referral Date</span><br />
                           <asp:Label ID="lblreferraldate" runat="server"></asp:Label>
                            <asp:Label ID="Label16" runat="server"></asp:Label><br />
                        <%--  <span>1 MB</span> --%>
                         </div>
                     <div class="col-lg-4 col-md-4 text-center">
                           <img src="../../Images/Icon-Referrals.png" class="res" id="res" alt="res" width="50px" height="40px" />
                      </div>
            
                 <div class="col-lg-12 col-md-12">
                           <asp:Label ID="Label17" runat="server"></asp:Label>
                            <span>Reason:</span>
                            <asp:Label ID="lblrefreason" runat="server"></asp:Label><br />
                           <asp:Label ID="Label18" runat="server"></asp:Label>
                      <asp:Label ID="Label25" runat="server"></asp:Label><br />
                            <span>Status:</span><br />
                             <asp:Label ID="lblrefstatus" runat="server"></asp:Label><br />
                           <asp:Label ID="Label26" runat="server"></asp:Label>
                      </div>
                  <div class="col-lg-12 col-md-12 view-det">
                    <span class="pull-right">
                                <a href="../../PatientLookUp/ASPX/ReferralView.aspx" >View Details 
                             <img src="../../Images/yellow_arrow.png" width="14px" height="14px"/></a>
                         </span>
                    </div>
                     </div>
            </div>

            
            <div class="col-md-4 col-sm-4 col-lg-4">
                <div class="boxes clearfix">
                     <div class="col-lg-12 col-md-12">
                        <h5 class="pull-right"><strong>Tests and Procedures</strong> </h5>
                    </div>
                      <div class="col-lg-8 col-md-8">
                     <br />
                            <asp:Label ID="lblprocedure1" runat="server"></asp:Label><br />
                           <span>Active Since:</span><br />
                         <asp:Label ID="lblprocdate1" runat="server"></asp:Label>
                            <asp:Label ID="Label20" runat="server"></asp:Label><br />
                        <%--  <span>1 MB</span> --%>
                         </div>
                     <div class="col-lg-4 col-md-4 text-center">
                           <img src="../../Images/Icon-Tests-and-Procedures.png" class="res" id="res" alt="res" width="50px" height="40px" />
                      </div>
            
                 <div class="col-lg-12 col-md-12">
                           <asp:Label ID="Label21" runat="server"></asp:Label><br />
                            <asp:Label ID="lblprocedure2" runat="server"></asp:Label><br />
                           <span>Active Since:</span><br />
                         <asp:Label ID="lblprocdate2" runat="server"></asp:Label>
                           <asp:Label ID="Label22" runat="server"></asp:Label>
                      </div>
                  <div class="col-lg-12 col-md-12 view-det">
                    <span class="pull-right">
                                <a href="../../PatientLookUp/ASPX/TestAndProcedures.aspx" >View Details 
                             <img src="../../Images/yellow_arrow.png" width="14px" height="14px"/></a>
                         </span>
                    </div>
                     </div>
            </div>


        </div>     

            <div class="phpsumm-form">
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                    <div class="vlign">Vital sign:</div>
                 </div>

            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                    <div class="">
                        <p class="units">Units</p>
                        <ul class="sum-li">
                            <li>Height</li>
                            <li> <asp:TextBox ID="txt_Height" runat="server" ></asp:TextBox></li>
                            <li>
                                 <asp:DropDownList ID="ddlHeightUnits" runat="server">
                        <asp:ListItem>in</asp:ListItem>
                        <asp:ListItem>cm</asp:ListItem>
                    </asp:DropDownList>
                            </li>
                        </ul>
 
                        <ul class="sum-li">
                            <li>Temp</li>
                            <li> <asp:TextBox ID="txt_temp" runat="server" ></asp:TextBox></li>
                            <li>
                                <asp:DropDownList ID="ddlTemperatureUnit" runat="server">
                        <asp:ListItem>F&#176;</asp:ListItem>
                        <asp:ListItem>C&#176;</asp:ListItem>
                    </asp:DropDownList>
                            </li>
                        </ul>
                    </div>
                 </div>

         <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                    <div class="">
                        <p class="units">Units</p>
                        <ul class="sum-li">
                            <li>Weight</li>
                            <li><asp:TextBox ID="txt_weight" runat="server" ></asp:TextBox></li>
                            <li>
                                 <asp:DropDownList ID="ddlWeightUnits" runat="server">
                        <asp:ListItem>lbs</asp:ListItem>
                        <asp:ListItem>kg</asp:ListItem>
                    </asp:DropDownList>
                            </li>
                        </ul>

                        <ul class="sum-li">
                            <li>Pulse</li>
                            <li><asp:TextBox ID="txt_pulse" runat="server" ></asp:TextBox></li>
                            <li>
                                BPM
                            </li>
                        </ul>
                    </div>
                 </div>

        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                    <div class="vital-unit">
                        <p class="units">Units</p>
                        <ul class="sum-li lst">
                            <li class="bp-bold" style="width:18px;">BP</li>
                            <li><asp:TextBox ID="txtBP" runat="server" ></asp:TextBox></li>
                            <li><asp:TextBox ID="txtBP2" runat="server" ></asp:TextBox></li>
                            <li style="width:48px;">MM HR</li>
                        </ul>

                        <ul class="sum-li resp">
                            <li style="width: 65px;">Respiration</li>
                            <li><asp:TextBox ID="txtRespiration" runat="server" ></asp:TextBox></li>
                            <li>BPM</li>
                        </ul>
                    </div>
                 </div>

                <div align="center"  class="clear">  <%-- <asp:Button runat="server" ID="save_vitals" 
                        CssClass="btn_standard" Text="Save" onclick="save_vitals_Click"></asp:Button>--%> 
                     <asp:Button runat="server" ID="save_vitals" CssClass="btn_standard" Text="Save" onclick="save_vitals_Click"></asp:Button>
                </div>
                <div align="center">
        <asp:Label runat="server" ID="lbl_msg" ForeColor="Green" ></asp:Label>
    </div>
            </div>

            <div class="historic clearfix">
                <h1>Vitals - Historic View</h1>

                 <div class="form-group clearfix">
    <label for="" class="col-sm-3 col-md-3 col-lg-3 control-label">Select Vital</label>
    <div class="col-sm-4 col-md-4 col-lg-4">
     <asp:DropDownList ID="ddlSelectVitals" runat="server" style="height:28px;">
                           
                            <asp:ListItem>Height</asp:ListItem>
                            <asp:ListItem>Weight</asp:ListItem>
                            <asp:ListItem>Temperature</asp:ListItem>
                            <asp:ListItem>Pulse</asp:ListItem>
                            <asp:ListItem>Respiration </asp:ListItem>
                            <asp:ListItem>Blood Pressure</asp:ListItem>
                        </asp:DropDownList>
    </div>
  </div>

  <div class="form-group clearfix">
    <label for="inputPassword3" class="col-sm-3 col-md-3 col-lg-3 control-label">Date Range</label>
    <div class="col-sm-4">
       <p class="txt-lft"> <b>From</b> </p>

        <asp:TextBox ID="txtFromdate" runat="server" CssClass="popup_textbox" ClientIDMode="Static" style="margin-left:0;"></asp:TextBox>
               
                    <img src="../../Images/calendar.jpg" id="calendar_img" class="txtbox_spanimg" alt="calendar"/>

    </div>
      <div class="col-sm-4 col-md-4 col-lg-4">
         <p class="txt-lft"> <b>To</b></p>
      
           <asp:TextBox ID="txtTodate" runat="server" CssClass="popup_textbox" ClientIDMode="Static" style="margin-left:0;"></asp:TextBox>
               
                    <img src="../../Images/calendar.jpg" id="calendar_imgs" class="txtbox_spanimg" alt="calendar"/>

    </div>

      
  </div>
  
                 <div align="center">   
                     <%--<asp:Button runat="server" ID="btn_submitvitals" 
                        CssClass="btn_standard" Text="Submit" onclick="btn_submitvitals_Click" ></asp:Button>--%>
                       <asp:Button runat="server" ID="btn_submitvitals" 
                        CssClass="btn_standard" Text="Submit" onclick="btn_submitvitals_Click"></asp:Button>
                         </div>

                         <div>
                    <div>
                        <asp:Label ID="lblGraphResult" runat="server"></asp:Label>
                        <div id="LineGraph" style="width: 450; height: 136;">
                        </div>
                    </div>
                </div>

            </div>

    

            </div>
  
  
    <script type="text/javascript">

        $("#div_patientsearch").css("display", "block");

        $("#patientsearch_leftpart").css("display", "block");
        $("#imgsearchuser").css("display", "block");


        $("#popup_close").click(function () {
            parent.popup_close();
        });
    </script>

             <script type="text/javascript">
                 $(document).ready(function () {
                     //$('#txtFromdate').datepicker({ maxDate: new Date() });
                     $('#calendar_img').click(function () {
                         $('#txtFromdate').datepicker('show');
                     });

                     $('#txtTodate').datepicker({ maxDate: new Date() });
                     $('#calendar_imgs').click(function () {
                         $('#txtTodate').datepicker('show');
                     });
                 });
    </script>
        <script type="text/javascript">
            function bind_barchart() {
                vitalType = $('#<%=ddlSelectVitals.ClientID%>').val().toString();
                if (vitalType == 'Height')
                    vitalType = 'Height (in.)'
                else if (vitalType == 'Weight')
                    vitalType = 'Weight (lbs.)'
                else if (vitalType == 'Temperature')
                    vitalType = 'Temperature (F)'
                else if (vitalType == 'Pulse')
                    vitalType = 'Pulse BPM'
                else if (vitalType == 'Respiration')
                    vitalType = 'Respiration BPM'
                else if (vitalType == 'Blood Pressure')
                    vitalType = 'Blood Pressure mmHG'
                $.barchart();
                xyz();
            }
            function xyz() {
                options1 = {



                    chart: {
                        renderTo: 'LineGraph',
                        type: 'line'
                        //zoomType: 'y'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: ''
                    },
                    xAxis: {
                        min: 0,
                        max: 9,
                        categories: [],
                        labels: {
                            rotation: 0,
                            y: 30,
                            x: 10,
                            enabled: false
                        },
                        title: {

                            text: ''
                        }
                    },
                    legend: {
                        //                layout: 'horizontal',
                        backgroundColor: '#FFFFFF',
                        align: 'right',
                        verticalAlign: 'top',
                        //                x: 10,
                        //                y: 10,
                        floating: true,
                        shadow: false,
                        enabled: true
                    },
                    yAxis: {
                        min: 0,
                        title:
                {

                    text: vitalType

                }
                    },

                    tooltip: {
                        formatter: function () {
                            return '' + vitalType + ': ' + this.y + '<br />Date :' + this.x;
                        }
                    },
                    scrollbar:
            {
                enabled: true
            },

                    plotOptions: {
                        column: {
                            stacking: 'normal',
                            pointPadding: 0.2,
                            borderWidth: 4,
                            minPointLength: 0,
                            shadow: false
                            //color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white'
                        },
                        series: {
                            animation: ({
                                duration: 2000,
                                easing: 'swing',
                                complete: function () {

                                }
                            })
                        }
                    },
                    credits: {
                        text: '',
                        href: ''
                    },
                    series: [],
                    exporting: {
                        enabled: true
                    }
                }

            }
            $.barchart = function () {

                $.ajax({
                    type: "POST",
                    url: "PhpSummary.aspx/BindGraphs",
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,

                    success: function (msg) {

                        //$("[id*=btnSubmit1]").click();
                        var element = msg.d.split("|");





                        var xAxisSeries = new Array();
                        options1.xAxis.categories = new Array();
                        options1.series = new Array();


                        if (msg.d.length > 0) {

                            var xaxis = element[0].split(",");
                            for (var i = 0; i < xaxis.length; i++) {

                                options1.xAxis.categories.push((xaxis[i]));
                            }


                            var addseries = false;
                            var columnSeries = {};
                            columnSeries.name = (vitalType);
                            columnSeries.data = new Array();
                            var unknown = element[1].split(",");
                            for (var i = 0; i < unknown.length; i++) {
                                if (parseInt(unknown[i]) != 0)
                                    addseries = true;
                                columnSeries.data.push(parseFloat((unknown[i])));
                            }

                            if (addseries == true)
                                options1.series.push(columnSeries);






                        }

                        chart1 = new Highcharts.Chart(options1);


                    },
                    error: function (xmlhttprequest, textstatus, errorThrown) {
                        //$("#divLoading").css('display', 'none');

                        window.parent.ShowError(request.responseText);
                    }
                });
            }

        </script>
    <script type="text/javascript">

        var chart2;
        var options2;

        function bind_linechartforbp() {

            vitalType = $('#<%=ddlSelectVitals.ClientID%>').val().toString();

            $.barchart1();
            xyz1();

        }
        function xyz1() {
            options2 = {



                chart: {
                    renderTo: 'LineGraph',
                    type: 'line',
                    zoomType: 'y'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: ''
                },
                xAxis: {
                    min: 0,
                    max: 9,
                    categories: [],
                    labels: {
                        rotation: 0,
                        y: 30,
                        x: 10,
                        enabled: false
                    },
                    title: {

                        text: 'Date'
                    }
                },
                yAxis: {
                    min: 0,
                    title:
                {

                    text: vitalType

                }
                },
                legend: {

                    backgroundColor: '#FFFFFF',
                    align: 'right',
                    verticalAlign: 'top',

                    floating: true,
                    shadow: false,
                    enabled: true
                },
                tooltip: {
                    //                  shared:true,
                    formatter: function () {


                        return '' + vitalType + ': ' + this.y + '<br />Date ' + this.x;
                    }
                },

                scrollbar:
            {
                enabled: true
            },



                plotOptions: {
                    column: {
                        stacking: 'normal',
                        pointPadding: 0.2,
                        borderWidth: 4,
                        minPointLength: 0,
                        shadow: false
                        //color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white'
                    },
                    series: {
                        animation: ({
                            duration: 2000,
                            easing: 'swing',
                            complete: function () {

                            }
                        })
                    }
                },
                credits: {
                    text: '',
                    href: ''
                },
                series: [],
                exporting: {
                    enabled: true
                }
            }

        }
        $.barchart1 = function () {

            $.ajax({
                type: "POST",
                url: "PhpSummary.aspx/BindGraphs",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,

                success: function (msg) {

                    //$("[id*=btnSubmit1]").click();
                    var element = msg.d.split("|");

                    var xAxisSeries = new Array();

                    options2.xAxis.categories = new Array();
                    options2.series = new Array();


                    if (msg.d.length > 0) {

                        var xaxis = element[0].split(",");
                        for (var i = 0; i < xaxis.length; i++) {

                            options2.xAxis.categories.push((xaxis[i]));
                        }

                        var addseries = false;
                        var columnSeries = {};
                        columnSeries.name = ('BP Systolic');
                        columnSeries.data = new Array();
                        columnSeries.color = ('#AA4643');
                        var unknown1 = element[1].split(",");
                        for (var i = 0; i < unknown1.length; i++) {
                            if (parseInt(unknown1[i]) != 0)
                                addseries = true;
                            columnSeries.data.push(parseFloat((unknown1[i])));
                        }

                        if (addseries == true)
                            options2.series.push(columnSeries);

                        var addseries = false;
                        var columnSeries = {};
                        columnSeries.name = ('BP Diastolic');
                        columnSeries.data = new Array();
                        columnSeries.color = ('#6085B1');
                        var unknown = element[2].split(",");
                        for (var i = 0; i < unknown.length; i++) {
                            if (parseInt(unknown[i]) != 0)
                                addseries = true;
                            columnSeries.data.push(parseFloat((unknown[i])));
                        }

                        if (addseries == true)
                            options2.series.push(columnSeries);



                    }

                    chart2 = new Highcharts.Chart(options2);


                },
                error: function (xmlhttprequest, textstatus, errorThrown) {
                    //$("#divLoading").css('display', 'none');

                    window.parent.ShowError(request.responseText);
                }
            });
        }


   //$(".patsearch_border .boxes").each(function(){
   // var ss = $(this).height();
   // alert(ss);
  //  });

$('.patsearch_border').each(function(){  
            
            var highestBox = 0;
            $('.boxes', this).each(function(){
            
                if($(this).height() > highestBox) 
                   highestBox = $(this).height(); 
            });  
            
            $('.boxes',this).height(highestBox + 25);
            
        
    });    
    </script>
</body>   
</html>