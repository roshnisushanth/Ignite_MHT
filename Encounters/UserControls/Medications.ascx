<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Medications.ascx.cs" Inherits="Hick.Encounters.UserControls.Medications" %>


<script>
    $("#div_patientsearch").css("display", "block");
    $("#patientsearch_leftpart").css("display", "block");
    $("#imgsearchuser").css("display", "block");
</script>

<div class="patsearch_heading">
    <div style="width: 99%;">
        Medications
      <%--   <input type="button" value="ADD NEW" name="add_conditions" class="btn_standardToolbar" id="add_conditions" style="float: right;" />--%>
        <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popupclose" class="pull-right" style="cursor: pointer; margin-top: -5px;"
            alt="close" />
    </div>
</div>
<div class="conditions_head" style="margin: 3px; margin-left: 0px; width:872px">
    <div class="conditions_div" style="display: inline-flex; display: -webkit-flex; width: 872px">
        <div style="width: 130px; font-weight: bold; margin-left: 25px; margin-bottom: 5px; margin-top: 22px;" class="ele_center">Medication</div>
        <div style="width: 200px; font-weight: bold; margin-left: 5px; margin-bottom: 5px; margin-top: 22px;" class="ele_center">Dosage</div>
        <div style="width: 130px; font-weight: bold; margin-left: 5px; margin-bottom: 5px; margin-top: 22px;" class="ele_center">Active Since</div>
        <%--<div style="text-align: right;" class="ele_center">--%>
        <div style="width: 300px; font-weight: bold; margin-top: 0px;" >
            <input type="button" value="Add New" name="add_conditions" class="btn_standard pull-right" id="add_conditions" />
        </div>
    </div>
</div>

<div class="patsearch_border_cdtn">
    <!-- Template starts here -->
    <div  id="newmedication" style="margin: 3px;">
        <% foreach (var med in medications.Rows)
            {
                var id = med.Id;
                var medicine = med.Medicine;
                var inactive = med.IsActive;
                var dosage = med.Dosage;
                var type = med.Type;
                var startDate = med.StartDateString;
                var stopDate = med.StopDate;
        %>
        <div id="medicationcont<%=med.Id%>"  style="display: inline-flex; border: 1px solid #D3D3D3; width: 100%">
            <div>
                <div style="width: 130px; margin-left: 25px;" class="ele_center "><%=medicine%></div>
            </div>
            <div>
                <div style="width: 200px; margin-left: 10px;" class="ele_center "><%=dosage%> <%=type%></div>
            </div>
            <div>
                <div style="width: 130px; margin-left: 5px;" class="ele_center "><%=startDate%></div>
            </div>
            <div>
                <div style="width: 130px; margin-left: 15px;" class="ele_center">
                    <img src="../../Images/button_edit.jpg" alt="Edit" class="edit_conditions" mid="<%=med.Id%>" style="width: 36px; height: 36px; cursor: pointer" />
                    <img src="../../Images/button_close.jpg" alt="Delete" class="delete_conditions" mid="<%=med.Id%>" style="width: 36px; height: 36px; cursor: pointer" />
                </div>
            </div>
        </div>
        <%   } %>
    </div>
</div>

<div style="display: none;">
    <div id="divshowconditions" style="z-index: 10000;">
        <div class="edit_medicationdiv">
            <div style="float: left;">
                <iframe id="frameConditionsdiv" src="" style="overflow: auto; position: fixed; width: 54%; height: 346px; border: none; margin-top: 40px;"></iframe>
            </div>
            <div id="small_pop_head" class="popup_header Content">
            </div>
            <div style="float: right; margin-right: 10px;">
                <img src="../../Images/popup_close.png" id="editpopupclose" style="cursor: pointer; margin-top: -35px; margin-right: -10px;" />
            </div>
        </div>
    </div>
</div>

<div style="display: none;">
    <div id="divdeleteconditions" style="z-index: 10000;">
        <div class="edit_medicationdiv">
            <div style="float: left;">
            </div>
            <div class="popup_header Content">
                Delete
            </div>
            <div style="float: right; margin-right: 10px;">
                <img src="../../Images/popup_close.png" id="deletepopupclose" style="cursor: pointer; margin-top: -35px; margin-right: -10px;" />
            </div>
        </div>
        <div id="btn_groups" style="margin-left: 195px;">
            <input type="button" value="Yes" id="" class="btn_standard" />
            <input type="button" value="No" id="" class="btn_standard" />
        </div>
    </div>
</div>
<input type="hidden" clientidmode="Static" id="asId" name="asId" runat="server" />

<script>
    if (isreadyonly) {
        $('#add_conditions').hide();
    }
    function editCondition(mid){
        var id = mid,aId = $('#asId').val();
        $("#frameConditionsdiv").attr("src", "");
        $("#frameConditionsdiv").attr("src", "../../Encounters/ASPX/AddEditMedications.aspx?pm=edit&medId=" + id + "&aId=" + aId);
        $('#small_pop_head').text("Edit")
        showpopup();
    }

    $(document).ready(function () {
        $(".edit_conditions").click(function () {
            var that = this, id = $(that).attr('mid');
            editCondition(id)
        });
        $("#editpopupclose").click(function () {
            closeMedpopUp();
        });

        $(".delete_conditions").click(function () {
            var that = this, id = $(that).attr('mid');
            showMessage({
                title: 'Delete',
                text: 'Do you want to delete the existing medication?',
                answers: ['Yes', 'No'],
                callback: function (btn) {
                    if (btn == 'No') {
                        closeConfirmPopup();
                    }
                    else {
                        confirmDelete(id);
                    }
                }
            });
        });
        $("#deletepopupclose").click(function () {
            $("#divdeleteconditions").dialog('close');
        });

    });
    $("#add_conditions").click(function () {
        var aId = $('#asId').val();
        $('#small_pop_head').text("Add New");
        $(".edit_conditionsdiv").css("display", "block");
        $("#frameConditionsdiv").attr("src", "");
        $("#frameConditionsdiv").attr("src", "../../Encounters/ASPX/AddEditMedications.aspx?pm=add&aId=" + aId);
        showpopup();
    });

    function confirmDelete(id) {
        $.ajax({
            type: "POST",
            url: "../Services/EncounterService.svc/DeleteMedication",
            data: JSON.stringify({ Id: id }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.d == "error") {
                    showError({ text: 'Sorry an error has occured. Please contact administrator.' });
                    closeConfirmPopup();
                }
                else {
                    $('#medicationcont' + id).remove();
                    showSucess({ text: 'Deleted the selected condition' });
                    closeConfirmPopup();
                }
            },
            error: function () {
                showError({ text: 'Sorry an error has occured. Please contact administrator.' });
                closeConfirmPopup();
            }
        });
    }

    function showpopup() {
        var popuphight = window.innerHeight - 150;
        var popupwidth = window.innerWidth - 490;

        $("#divshowconditions").dialog(
            {
                modal: true,
                height: popuphight,
                width: popupwidth,
                resizable: false,
                //title: "Patient Search",
                create: function () {
                    $(".ui-dialog-titlebar").hide();
                    $(".ui-dialog-content").css("padding", "0px");
                }
            });
    }

    function getHTML(med) {
        return  newmedHtml = '<div id="' + med.Id + '" style="display: inline-flex; border: 1px solid #D3D3D3; width: 100%"><div><div style="width: 130px; margin-left: 25px;" class="ele_center ">' + med.Medicine + '</div>' +
            '</div><div><div style="width: 200px; margin-left: 10px;" class="ele_center ">' + med.Dosage + ' ' + med.Type + '</div></div><div><div style="width: 130px; margin-left: 5px;" class="ele_center ">' + med.StartDateString + '</div>' +
            '</div><div><div style="width: 130px; margin-left: 15px;" class="ele_center"><img src="../../Images/button_edit.jpg" alt="Edit" onclick="editCondition(' + med.Id + ')" class="edit_conditions" mid="' + med.Id + '" style="width: 36px; height: 36px; cursor: pointer" />' +
            '<img src="../../Images/button_close.jpg" alt="Delete" class="delete_conditions" mid="' + med.Id + '" style="width: 36px; height: 36px; cursor: pointer" />' +
            '</div></div></div>';
    }
    function reloadMed(med) {
        $('#newmedication').append(getHTML(med));
        closeMedpopUp();
    }

    function updateMed(med) {
        var getele = $('#' + med.Id).html(getHTML(med));
        closeMedpopUp();
    }

    function closeMedpopUp() {
        $("#divshowconditions").dialog('close');
    }

    function showsmallpopup() {
        var popupwidth = window.innerWidth - 490;

        $("#divdeleteconditions").dialog(
            {
                modal: true,
                width: popupwidth,
                resizable: false,
                //title: "Patient Search",
                create: function () {
                    $(".ui-dialog-titlebar").hide();
                    $(".ui-dialog-content").css("padding", "0px");
                }
            });
    }


    $("#popupclose").click(function () {
        parent.assesummary_close();
    });
</script>
