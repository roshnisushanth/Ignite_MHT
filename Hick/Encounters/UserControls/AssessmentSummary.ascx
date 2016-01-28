<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssessmentSummary.ascx.cs" Inherits="Hick.Encounters.UserControls.AssessmentSummary" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link href="~/Content/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap-theme.min.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1">
        <div class="patsearch_heading">
            Assessment Summary
             <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popupclose" class="pull-right" style="cursor: pointer; margin-top: -5px;"
                 alt="close" />
        </div>
        <div class="patsearch_border">
            <div style="margin-right: 12px;">
                <a href="#">
                    <img src="../../Images/pdf.png" alt="print" style="float: right; margin-top: 2px;" /></a>
                <a href="#">
                    <img src="../../Images/printer.png" alt="print" style="float: right;" /></a>
            </div>
             <div class="row">
                <div class="col-md-12 assemt_marg" style="margin-bottom: 15px;margin-top: 5px;">
                    <div class="col-md-12 assess_boxes pull-left" style="width: 96.4%; padding-left: 7px;">
                        <span><b>Patient Name:</b></span>
                        <span><%=summary.PatientName %></span>
                    </div>
                 
                </div>
            </div>
              <div class="row">
                <div class="col-md-12 assemt_marg">
                    <div class="col-md-6 assess_boxes pull-left" style="width: 45%; padding-left: 7px;">
                        <span><b>Care Plan Creation Date:</b></span>
                        <span><%=summary.AssessmentDate.ToString(Hick.Models.Utility.GlobalDateFormat) %></span>
                    </div>
                    <div class="col-md-6 assess_boxes pull-left" style="margin-left: 10px; padding-left: 7px;">
                        <span><b>Assesment By:</b></span>
                        <span><%=summary.AssessmentBy %></span>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-6 assment_grid" style="width: 45%; padding-left: 2px; margin-left: 16px;">
                        <div style="margin-left: 10px; margin-left: 10px; margin-top: 5px; font-size: 13px;"><span><b>Breastfeeding Baby</b></span></div>
                        <% foreach (var bb in summary.BreastFeedingBabies)
                            { %>
                        <div style="display: inline-flex">
                            <div>
                                <div>
                                    <div style="width: 225px; margin-left: 10px; margin-left: 10px; margin-top: 2px;">
                                        <span class="lbldesc"><%=bb.Description%></span>

                                    </div>
                                </div>
                            </div>
                            <div>
                                <div>
                                    <div style="width: 130px; margin-left: 5px; margin-left: 10px; margin-top: 2px;">
                                        <span class="lbldesc"><% if (bb.StatusStr == "1")
                                                                  { %>Yes<%}
                                                                         else
                                                                         { %>No <%} %></span>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <%} %>
                    </div>

                    <div class="col-md-6 assment_grid" style="width: 50%; padding-left: 2px; margin-left: 10px;">
                        <div style="margin-left: 10px; margin-left: 10px; margin-top: 5px; font-size: 13px;"><span><b>Breastfeeding Mother</b></span></div>
                        <% foreach (var bm in summary.BreastFeedingBabies)
                            { %>
                        <div style="display: inline-flex">
                            <div>
                                <div>
                                    <div style="width: 225px; margin-left: 10px; margin-left: 10px; margin-top: 2px;">
                                        <span class="lbldesc"><%=bm.Description%></span>

                                    </div>
                                </div>
                            </div>
                            <div>
                                <div>
                                    <div style="width: 130px; margin-left: 5px; margin-left: 10px; margin-top: 2px;">
                                        <span class="lbldesc"><% if (bm.StatusStr == "1")
                                                                  { %>Yes<%}
                                                                         else
                                                                         { %>No <%} %></span>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <%} %>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-12 assment_grid" style="width: 96.4%; padding-left: 2px; margin-left: 16px;">
                        <div style="margin-left: 10px; margin-left: 10px; margin-top: 5px; font-size: 13px; display: inline-flex; display: -webkit-flex">
                            <span style="width: 350px;">
                                <b>Breastfeeding Observation</b></span><span style="width: 280px;"><b>Status</b>
                                </span>
                            <span style="width: 36px;"><b>Notes</b></span>
                        </div>
                        <% foreach (var ob in summary.Observations)
                            { %>
                        <div style="display: inline-flex">
                            <div>
                                <div>
                                    <div style="width: 340px; margin-left: 10px; margin-left: 10px; margin-top: 2px;">
                                        <span class="lbldesc"><%=ob.Description%></span>
                                    </div>
                                </div>
                            </div>

                            <div>
                                <div>
                                    <div style="width: 210px; margin-left: 5px; margin-left: 10px; margin-top: 2px;">
                                        <span class="lbldesc"><% if (ob.StatusStr == "1")
                                                                  { %>Normal<%}
                                                                            else
                                                                            { %> Abnormal <%} %></span>
                                    </div>
                                </div>
                            </div>
                            <div>
                                <div>
                                    <div style="width: 250px; margin-left: 5px; margin-left: 10px; margin-top: 2px;">
                                        <span class="lbldesc"><%=ob.Notes%></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%} %>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-12 assment_grid" style="width: 96.4%; padding-left: 2px; margin-left: 16px">
                        <div style="margin-left: 10px; margin-left: 10px; margin-top: 5px; font-size: 13px;">
                            <span><b>Assessment Notes: </b><%=summary.AssessmentNotes %></span>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-12 assment_grid" style="width: 96.4%; padding-left: 2px; margin-left: 16px;">
                        <div style="margin-left: 10px; margin-left: 10px; margin-top: 5px; font-size: 13px;">
                            <span><b>History of Present Illness Notes: </b><%=summary.HPINotes %></span>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-12 assment_grid" style="width: 96.4%; padding-left: 2px; margin-left: 16px;">
                        <div style="margin-left: 10px; margin-left: 10px; margin-top: 5px; font-size: 13px;">
                            <span><b>Care Coordination Notes: </b><%=summary.COCNotes %></span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12" style="margin-top: 20px; width: 96.4%; padding-left: 2px; margin-left: 16px;">
                    <div class="col-md-6 padd_right">
                        <div class="assess_boxes pull-left">
                            <span><b>Chief Complaint:</b></span><br />

                        </div>
                    </div>

                    <div class="col-md-6 padd_right padd_left">
                        <div class="assess_boxes pull-left">
                            <span><b>Plan:</b></span><br />

                        </div>
                    </div>
                </div>
            </div>
            <% foreach (var ccom in summary.ChiefComplaintList)
                { %>
             <div class="row">
            <div class="col-md-12" style="margin-top: 5px; width: 96.4%; padding-left: 2px; margin-left: 16px;">
                <div class="col-md-6 padd_right">
                    <div class="assess_boxes pull-left">
                        <span><%=ccom.Notes %></span>
                    </div>
                </div>

                <div class="col-md-6 padd_right padd_left">
                    <div class="assess_boxes pull-left">
                         
                            <%if (ccom.ComplaintsPlans.Count > 0){ %>
                        <ul>
                                <%foreach (var cp in ccom.ComplaintsPlans){ %><li><%=cp.Plan %></li><%} %>
                             </ul>
                            <%}else{%>N/A<%} %>
                        
                    </div>
                </div>
            </div>
                 </div>
            <%} %>
        </div>
        <div class="col-md-3 padd_right padd_left">
            <input type="button" id="save_n_close" name="save_assment" value="CLOSE" class="btn_standard pull-right" style="margin-top: 80%; border-radius: 4px;" />
        </div>
        <input type="hidden" clientidmode="Static" id="asId" name="asId" runat="server" />
    </form>
    <script>
        $("#div_patientsearch").css("display", "block");
        $("#patientsearch_leftpart").css("display", "block");
        $("#imgsearchuser").css("display", "block");


        $("#save_n_close").click(function () {
            parent.assesummary_close();
        });

        $("#popupclose").click(function () {
            parent.assesummary_close();
        });


    </script>
</body>
</html>
