<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChiefComplaints.ascx.cs" Inherits="Hick.Encounters.UserControls.ChiefComplaints" %>



<script>
    $("#div_patientsearch").css("display", "block");
    $("#patientsearch_leftpart").css("display", "block");
    $("#imgsearchuser").css("display", "block");
</script>
<style>
    .selected {
        background-color: #a9a9a9;
    }

    .unselected {
        background-color: #fff;
    }
</style>
<div class="patsearch_heading">
    Chief Complaints
       <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popupclose" class="pull-right" style="cursor: pointer; margin-top: -5px;"
           alt="close" />
</div>
<div class="patsearch_border">
    <div>
        <div class="" style="display: inline-flex; display: -webkit-flex; width: 100%">
            <div class="condition_head" style="margin: 3px;">
                <div class="" style="display: inline-flex; display: -webkit-flex; width: 100%">
                    <div class="table_brdr">
                        <div style="width: 235px; font-weight: bold; margin-left: 25px; margin-bottom: 5px; margin-top: 22px;" class="ele_center">Chief Complaint (Child)</div>
                    </div>
                    <div class="table_brdr">
                        <div style="width: 150px; font-weight: bold; margin-left: -5px; margin-bottom: 5px; margin-top: 22px;" class="text-center">Diagnosis code</div>
                    </div>
                </div>

                <!-- Template starts here -->
                <div class="" style="margin: 3px; margin-left: 0px; cursor: pointer;">
                    <%
                        foreach (var child in babies)
                        {
                            var id = child.Id;
                            var desc = child.Description;
                            var code = child.DiagnosisCode;
                            var compId = child.ComplaintId;
                    %>
                    <div class="js_row <%if (id != 0)
                        {%> selected <%} %>" cid="<%=compId%>" style="display: inline-flex">
                        <div style="border: 1px solid #ddd;" class="childrw">
                            <div>
                                <div style="width: 235px; margin-left: 25px;" class="ele_center">
                                    <span class="lbldesc"><%=desc%></span>
                                </div>
                            </div>
                        </div>
                        <div class="table_brdr childrw">
                            <div>
                                <div style="width: 145px; margin-top: 15px;" class="text-center">
                                    <span class="lbldesc"><%=code%></span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%   } %>
                    <!-- Template ends here -->
                </div>
            </div>


            <div class="condition_head" style="margin: 3px;">
                <div class="" style="display: inline-flex; display: -webkit-flex; width: 100%">
                    <div class="table_brdr">
                        <div style="width: 265px; font-weight: bold; margin-left: 0px; margin-bottom: 5px; margin-top: 22px;" class="text-center">Chief Complaint (Mother)</div>
                    </div>
                    <div class="table_brdr">
                        <div style="width: 150px; font-weight: bold; margin-left: -6px; margin-bottom: 5px; margin-top: 22px;" class="text-center">Diagnosis Code</div>
                    </div>
                </div>

                <!-- Template starts here -->
                <div class="" style="margin: 3px; margin-left: 0px; cursor: pointer;">
                    <%
                        foreach (var mother in mothes)
                        {
                            var id = mother.Id;
                            var desc = mother.Description;
                            var code = mother.DiagnosisCode;
                            var compId = mother.ComplaintId;
                    %>
                    <div class="js_row <%if (id != 0)
                        {%> selected <%} %>" cid="<%=compId%>" style="display: inline-flex">

                        <div style="border: 1px solid #ddd;" class="momrw" id="childrw">
                            <div>
                                <div style="width: 226px; margin-left: 25px;" class="ele_center">
                                    <span class="lbldesc"><%=desc%></span>
                                </div>
                            </div>
                        </div>
                        <div class="table_brdr momrw" id="childrw1">
                            <div>
                                <div style="width: 144px; margin-top: 15px;" class="text-center">
                                    <span class="lbldesc"><%=code%></span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%   } %>
                    <!-- Template ends here -->
                </div>
            </div>

        </div>
        <input type="button" value="Save" name="save" class="btn_standard" id="save_complaints" style="margin-left: 1%; margin-top: 1%" />
        <input type="hidden" clientidmode="Static" id="asId" name="asId" runat="server" />
    </div>
</div>
<script>
    $(document).ready(function () {
        if ($(".js_row").click(function () {
            var that = this,
            $that = $(that);
            if ($that.hasClass('selected')) {
                $that.removeClass('selected').addClass('unselected');
        }
        else {
                $that.removeClass('unselected').addClass('selected');
        }
        }));
    });

    $("#save_complaints").click(function () {
        var asId = $("#asId").val(),
            cfeed = new Array(), cid, cjson;
        $('.js_row').each(function (idx, item) {
            cid = $(item).attr('cid');
            if ($(item).hasClass('selected')) {
                cfeed.push({
                    "ComplaintId": cid
                });
            }
        });
        cjson = JSON.stringify(cfeed);

        $.ajax({
            type: "POST",
            url: "../Services/EncounterService.svc/AddChiefComplaints",
            data: JSON.stringify({ AssessmentId: asId, data: cjson }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.d == "error") {
                    showError({ text: 'Sorry an error has occured. Please contact administrator.' });
                }
                else {
                    showSucess({ text: 'Chief complaints updated successfully.' });
                }
            },
            error: function (res) {
                showError({ text: 'Sorry an error has occured. Please contact administrator.' });
            }
        });
    });

    $("#popupclose").click(function () {
        parent.assesummary_close();
    });
</script>
