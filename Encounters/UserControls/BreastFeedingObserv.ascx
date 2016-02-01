<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BreastFeedingObserv.ascx.cs" Inherits="Hick.Encounters.UserControls.BreastFeedingObserv" %>


<script>
    $("#div_patientsearch").css("display", "block");
    $("#patientsearch_leftpart").css("display", "block");
    $("#imgsearchuser").css("display", "block");
</script>
<style>
    .feeding_occured {
        display: inline-block;
    }
</style>
<div class="patsearch_heading">
    Breastfeeding Observation
    <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popupclose" class="pull-right" style="cursor: pointer; margin-top: -5px;"
        alt="close" />
</div>
<div class="patsearch_border">

    <div class="conditions_head" style="margin: 3px;">
        <div class="feeding_occured" style="display: inline-flex;" id="feeding_occured">
            <div style="margin: 20px;">
                <h4>Has Breastfeeding Observation occured? </h4>
            </div>
            <div style="margin: 30px;">
                <asp:RadioButtonList ID="radioconditions" CssClass="paddradiobtn" Style="width: auto;" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Yes" Value="yes" Enabled="true"></asp:ListItem>
                    <asp:ListItem Text="No" Value="no"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
        <div class="conditions_div" style="display: inline-flex; display: -webkit-flex; width: 100%">
            <div class="table_brdr">
                <div style="width: 300px; font-weight: bold; margin-left: 25px; margin-bottom: 5px; margin-top: 22px;" class="ele_center">Breastfeeding Mother</div>
            </div>
            <div class="table_brdr">
                <div style="width: 80px; font-weight: bold; margin-left: -5px; margin-bottom: 5px; margin-top: 22px;" class="text-center">Normal</div>
            </div>
            <div class="table_brdr">
                <div style="width: 80px; font-weight: bold; margin-left: 0px; margin-bottom: 5px; margin-top: 22px;" class="text-center">Abnormal</div>
            </div>
            <div class="table_brdr">
                <div style="width: 80px; font-weight: bold; margin-left: -6px; margin-bottom: 5px; margin-top: 22px;" class="text-center">NA</div>
            </div>
            <div class="table_brdr">
                <div style="width: 169px; font-weight: bold; margin-left: 85px; margin-bottom: 5px; margin-top: 22px;" class="ele_center">Notes</div>
            </div>
        </div>
    </div>



    <!-- Template starts here -->
    <div class="conditions_head conditions_div js-gridhead" style="margin: 3px;">
        <%
            foreach (var child in observation.ChildObservations)
            {
                var id = child.Id;
                var desc = child.Description;
                var ccon = false;
                var cdeny = false;
                var cna = false;
                var feedingid = child.FeedingId;
                var odsId = child.ObservationParentId;
                var notes = child.Notes;
                var status = child.Status;
                var sts = string.IsNullOrEmpty(child.StatusStr) ? 0 : Convert.ToInt32(child.StatusStr);
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
        <div class="js_feeding" fid="<%=feedingid %>" oid="<%=odsId %>" style="display: inline-flex; border: 1px solid #D3D3D3; width: 100%">
            <div style="width: 300px; margin-left: 10px;" class="ele_center">
                <div style="width: 300px; margin-left: 10px;" class="ele_center "><%=desc%></div>
            </div>
            <div style="width: 110px; position: relative; margin-top: 25px;" class="text-center">
                <input type="radio" name="babyfeed_<%=feedingid %>" ftype="1" class="paddradiobtn" <%if (ccon)
                    { %> checked <%}%> />
            </div>
            <div style="width: 60px; position: relative; margin-top: 25px;" class="text-center">
                <input type="radio" name="babyfeed_<%=feedingid %>" ftype="2" class="paddradiobtn" <%if (cdeny)
                    { %> checked <%}%> />
            </div>
            <div style="width: 90px; margin-top: 25px;" class="text-center">
                <input type="radio" name="babyfeed_<%=feedingid %>" ftype="0" class="paddradiobtn" <%if (cna)
                    { %> checked <%}%> />
            </div>
            <div style="width: 214px; margin-top: 25px;" class="text-center">
                <input type="text" style="margin-left: 13px;" class="bf_txt_bx js_notes" value="<%=notes%>" />
            </div>
        </div>
        <%   } %>
    </div>
    <input type="button" value="Save" name="save" class="btn_standard" id="save_feeding_observation" style="margin-left: 1%; margin-top: 1%" />
    <!-- Template ends here -->
    <input type="hidden" clientidmode="Static" id="asId" name="asId" runat="server" />
</div>


<script>
    function handleRadio() {
        if ($('#<%=radioconditions.ClientID %> input:checked').val() == 'yes') {
            $('.conditions_div').show();
            $('.contents_div').show();
            $('#save_feeding_observation').show();
        }
        else {
            $('.conditions_div').hide();
            $('.contents_div').hide();
            //$('#save_feeding_observation').hide();
        }
    }

    $("#<%=radioconditions.ClientID%>").change(function () {
        handleRadio();
    });

    $(function () {
        handleRadio();
    });

    $("#popupclose").click(function () {
        parent.assesummary_close();
    });

    $('#save_feeding_observation').click(function () {
        var that = this, fid, status, bfeed = new Array(), fjson = [], asId = $("#asId").val();
        var isObs = $('#<%=radioconditions.ClientID %> input:checked').val() == "yes" ? true : false;

        if (isObs) {
            $('.js_feeding').each(function (idx, item) {
                fid = $(item).attr('fid');
                oid = $(item).attr('oid');
                status = $(item).find("input:checked").attr('ftype');
                notes = $(item).find(".js_notes").val();
                bfeed.push({
                    "ObservationId": fid,
                    "Status": status,
                    "ObservationParent": oid,
                    "Notes": notes
                });
            });
            fjson = JSON.stringify(bfeed);
        }
        $.ajax({
            type: "POST",
            url: "../Services/EncounterService.svc/AddFeedingObservation",
            data: JSON.stringify({ AssessmentId: asId, IsObserved: isObs, data: fjson }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.d == "error") {
                    showError({ text: 'Sorry an error has occured. Please contact administrator.' });
                }
                else {
                    showSucess({ text: 'Breast feeding observations updated successfully.' });
                }
            },
            error: function (res) {
                showError({ text: 'Sorry an error has occured. Please contact administrator.' });
            }
        });
    });
</script>

