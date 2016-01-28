<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BreastFeedingBaby.ascx.cs" Inherits="Hick.Encounters.UserControls.BreastFeedingBaby" %>



<script>
    $("#div_patientsearch").css("display", "block");
    $("#patientsearch_leftpart").css("display", "block");
    $("#imgsearchuser").css("display", "block");

</script>

<div class="patsearch_heading">
    Breastfeeding Baby
      <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popupclose" class="pull-right" style="cursor: pointer; margin-top: -5px;"
          alt="close" />
</div>

<div class="patsearch_border">

    <div class="conditions_head" style="margin: 3px;">
        <div class="conditions_div" style="display: inline-flex; display: -webkit-flex; width: 100%">
            <div class="table_brdr">
                <div style="width: 530px; font-weight: bold; margin-left: 25px; margin-bottom: 5px; margin-top: 22px;" class="ele_center">Breastfeeding Baby</div>
            </div>
            <div class="table_brdr">
                <div style="width: 95px; font-weight: bold; /*margin-left: -5px*/; margin-bottom: 5px; margin-top: 22px;" class="text-center">Confirm</div>
            </div>
            <div class="table_brdr">
                <div style="width: 95px; font-weight: bold; /*margin-left: -15px*/; margin-bottom: 5px; margin-top: 22px;" class="text-center">Deny</div>
            </div>
            <div class="table_brdr">
                <div style="width: 79px; font-weight: bold; /*margin-left: -5px; */ margin-bottom: 5px; margin-top: 22px;" class="text-center">NA</div>
            </div>
        </div>
    </div>
    <!-- Template starts here -->
    <div id="feedingBabyMain" class="conditions_head clr_input" style="margin: 3px;">
        <%
            foreach (var bb in bfBabyList)
            {
                var id = bb.Id;
                var desc = bb.Description;
                var ccon = false;
                var cdeny = false;
                var cna = false;
                var feedingid = bb.FeedingId;
                var sts = string.IsNullOrEmpty(bb.StatusStr) ? 0 : Convert.ToInt32(bb.StatusStr);
                if (sts == 0)
                {
                    cna = true;
                    ccon = false;
                    cdeny = false;
                }
                else if (sts == 1)
                {
                    cna = false;
                    cdeny = false;
                    ccon = true;
                }
                else if (sts == 2)
                {
                    cdeny = true;
                    cna = false;
                    ccon = false;
                }
        %>
        <div class="js_feeding" fid="<%=feedingid %>" style="display: inline-flex; border: 1px solid #D3D3D3; width: 100%">
            <div style="width: 550px; margin-left: 25px;" class="ele_center">
                <div style="width: 550px; margin-left: 25px;" class="ele_center "><%=desc%></div>
            </div>
            <div style="width: 90px; position: relative; margin-top: 25px;" class="text-center">
                <input type="radio" name="babyfeed_<%=feedingid %>" ftype="1" class="paddradiobtn" <%if (ccon)
                    { %>
                    checked <%}%> />
            </div>
            <div style="width: 90px; position: relative; margin-top: 25px;" class="text-center">
                <input type="radio" name="babyfeed_<%=feedingid %>" ftype="2" class="paddradiobtn" <%if (cdeny)
                    { %>
                    checked <%}%> />
            </div>
            <div style="width: 86px; margin-top: 25px;" class="text-center">
                <input type="radio" name="babyfeed_<%=feedingid %>" ftype="0" class="paddradiobtn" <%if (cna)
                    { %>
                    checked <%}%> />
            </div>
        </div>
        <%   } %>
    </div>
    <input type="button" value="Save" name="save" id="save_feeding_baby" class="btn_standard" style="margin-left: 1%; margin-top: 1%" />
    <!-- Template ends here -->
    <input type="hidden" clientidmode="Static" id="asId" name="asId" runat="server" />
</div>
<script>
    $("#popupclose").click(function () {
        parent.assesummary_close();
    });

    $('#save_feeding_baby').click(function () {
        var that = this, fid, status, bfeed = new Array(), fjson, asId = $("#asId").val();
        $('.js_feeding').each(function (idx, item) {
            fid = $(item).attr('fid');
            status = $(item).find("input:checked").attr('ftype');
            bfeed.push({
                "FeedingId": fid,
                "Status": status,
                "AssessmentId": asId

            });
        });
        fjson = JSON.stringify(bfeed);

        $.ajax({
            type: "POST",
            url: "../Services/EncounterService.svc/AddFeedingBabies",
            data: JSON.stringify({ data: fjson }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.d == "error") {
                    showError({ text: 'Sorry an error has occured. Please contact administrator.' });
                }
                else {
                    showSucess({ text: 'Breast feeding baby updated successfully.' });
                }
            },
            error: function (res) {
                showError({ text: 'Sorry an error has occured. Please contact administrator.' });
            }
        });
    });
</script>


