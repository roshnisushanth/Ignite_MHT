<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Conditions.ascx.cs" Inherits="Hick.Encounters.UserControls.Conditions" %>

<script>
    $("#div_patientsearch").css("display", "block");
    $("#patientsearch_leftpart").css("display", "block");
    $("#imgsearchuser").css("display", "block");
</script>


<div class="patsearch_heading">
    <div style="width: 99%;">
        Conditions
       <%--  <input type="button" value="ADD NEW" name="add_conditions" class="btn_standardToolbar" id="add_conditions" style="float: right;" />--%>
        <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popupclose" class="pull-right" style="cursor: pointer; margin-top: -5px;"
            alt="close" />
    </div>
</div>
<div class="conditions_head" style="margin: 3px; width: 872px; margin-left: 0px">
    <div class="conditions_div" style="display: inline-flex; width: 872px">
        <div style="width: 130px; font-weight: bold; margin-left: 25px; margin-bottom: 5px; margin-top: 22px;" class="ele_center">ICD 9 Code</div>
        <div style="width: 200px; font-weight: bold; margin-left: 15px; margin-bottom: 5px; margin-top: 22px;" class="ele_center">Description</div>
        <div style="width: 130px; font-weight: bold; margin-left: 5px; margin-bottom: 5px; margin-top: 22px;" class="ele_center">Active Since</div>
        <div style="width: 300px; font-weight: bold; margin-top: 0px;">
            <input type="button" value="Add New" name="add_conditions" class="btn_standard pull-right" id="add_conditions" />
        </div>
    </div>
</div>
<div class="patsearch_border_cdtn">
    <!-- Template starts here -->
    <div id="newcondition" style="margin: 3px;">
        <% foreach (var con in conditions.Rows)
            {
                var icd9code = con.ICDCode;
                var icd9Desc = con.Description;
                var activeDate = con.ActiveDateString;
                var id = con.Id;
        %>
        <div id="conditioncont<%=con.Id%>" style="display: inline-flex; border: 1px solid #D3D3D3; width: 100%">
            <div>
                <div style="width: 130px; margin-left: 25px;" class="ele_center "><%=icd9code%></div>
            </div>
            <div>
                <div style="width: 200px; margin-left: 10px;" class="ele_center "><%=icd9Desc%> </div>
            </div>
            <div>
                <div style="width: 130px; margin-left: 5px;" class="ele_center "><%=activeDate%></div>
            </div>
            <div>
                <div style="width: 130px; margin-left: 5px;" class="ele_center">
                    <img src="../../Images/button_edit.jpg" alt="Edit" class="edit_conditions" sid="<%=con.Id%>" style="width: 36px; height: 36px; cursor: pointer" />
                    <img src="../../Images/button_close.jpg" alt="Delete" class="delete_conditions" sid="<%=con.Id%>" style="width: 36px; height: 36px; cursor: pointer" />
                </div>
            </div>
        </div>
        <%   } %>
    </div>

    <!-- Template ends here -->

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
<input type="hidden" clientidmode="Static" id="asId" name="asId" runat="server" />
<script>
    if (isreadyonly) {
        $('#add_conditions').hide();
    }
    $(document).ready(function () {
       
        $(".edit_conditions").click(function () {
            var that = this, id;
            id = $(that).attr('sid');
            var aId = $('#asId').val();
            $("#frameConditionsdiv").attr("src", "");
            $("#frameConditionsdiv").attr("src", "../../Encounters/ASPX/EditConditions.aspx?pm=edit&conId=" + id + "&aId=" + aId);
            $('#small_pop_head').text("Edit")
            showpopup();
        });
        $("#editpopupclose").click(function () {
            closeConpopUp();
        });

        $(".delete_conditions").click(function () {
            var that = this, id = $(that).attr('sid');
            showMessage({
                title: 'Delete',
                text: 'Do you want to delete the existing condition?',
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
            closeConfirmPopup();
        });

    });

    function confirmDelete(id) {
        $.ajax({
            type: "POST",
            url: "../Services/EncounterService.svc/DeleteCondition",
            data: JSON.stringify({Id:id}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.d == "error") {
                    showError({ text: 'Sorry an error has occured. Please contact administrator.' });
                    closeConfirmPopup();
                }
                else {
                    $('#conditioncont' + id).remove();
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

    $("#add_conditions").click(function () {
        var aId = $('#asId').val();
        $("#frameConditionsdiv").attr("src", "");
        $("#frameConditionsdiv").attr("src", "../../Encounters/ASPX/EditConditions.aspx?pm=add&aId=" + aId);
        $('#small_pop_head').text("Add New")
        showpopup();
    });

    function getHTML(con) {
        return newHtml = '<div style="display: inline-flex; border: 1px solid #D3D3D3; width: 100%"><div><div style="width: 130px; margin-left: 25px;" class="ele_center ">' + con.ICDCode + '</div>' +
            '</div><div><div style="width: 200px; margin-left: 10px;" class="ele_center ">' + con.Description + '</div></div><div><div style="width: 130px; margin-left: 5px;" class="ele_center ">' + con.ActiveDateString + '</div>' +
            '</div><div><div style="width: 130px; margin-left: 5px;" class="ele_center"><img src="../../Images/button_edit.jpg" alt="Edit" class="edit_conditions" mid="' + con.Id + '" style="width: 36px; height: 36px; cursor: pointer" />' +
            '<img src="../../Images/button_close.jpg" alt="Delete" class="delete_conditions" mid="' + con.Id + '" style="width: 36px; height: 36px; cursor: pointer" />' +
            '</div></div></div>';
    }

    function reloadCon(con) {
        $('#newcondition').append(getHTML(con));
        closeConpopUp();
    }

    function updateCon(con) {
        var getele = $('#' + con.Id).html(getHTML(con));
        closeConpopUp();
    }
    function closeConpopUp() {
        $("#divshowconditions").dialog('close');
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



    $("#popupclose").click(function () {
        parent.assesummary_close();
    });
</script>
