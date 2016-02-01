<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCConditions.ascx.cs"
    Inherits="Hick.PatientLookUp.UserControls.UCConditions" %>
<asp:HiddenField ID="hdn" runat="server" Value="asadad" />

<style>
    div.patient_search_right_frame {
        margin: 5px 0;
    }

    .patsearch_heading.patient {
        width: 100%;
        padding-top: 4px;
    }

        .patsearch_heading.patient .btn_standard {
            margin: 0px 50px 0 0 !important;
            padding: 5px 10px;
            float: right;
        }

        .patsearch_heading.patient #popup_close {
            margin-right: -164px;
            margin-top: 0 !important;
        }

    .ui-dialog .ui-dialog-content {
        overflow: hidden !important;
    }


    div.patient_search_right_frame {
        margin: 5px 0;
    }

    .patsearch_heading.patient {
        width: 100%;
        padding-top: 4px;
    }

        .patsearch_heading.patient .btn_standard {
            margin: 0px 50px 0 0 !important;
            padding: 5px 10px;
            float: right;
        }

        .patsearch_heading.patient #popup_close {
            margin-right: -164px;
            margin-top: 0 !important;
        }

    #gdconditions tr:first-child {
        position: absolute;
        font-weight: bold;
        top: 47px;
    }

    #gdconditions {
        margin-top: 30px;
    }

        #gdconditions tr:first-child th {
            background: #fff;
        }
</style>
<div class="patsearch_heading patient">
    Conditions
    <input type="button" value="Add New" name="add_conditions" class="btn_standard" id="add_conditions"
        style="margin-left: 508px; margin-top: -28px;" />
    <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popup_close" class="pull-right"
        style="cursor: pointer; margin-top: -42px;" alt="close" />
</div>
<div class="patsearch_border">
    <div class="conditions_head" style="margin: 3px;"
        <asp:GridView runat="server" ID="gdconditions" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" HeaderStyle-BorderWidth="0" RowStyle-BorderWidth="1px" RowStyle-BorderColor="#D3D3D3">
            <Columns>
                <asp:TemplateField ItemStyle-CssClass="table_data_list" HeaderStyle-Width="165px">
                    <HeaderTemplate>
                        <div style="width: 170px; font-weight: bold; text-align: left;" class="ele_center">
                            ICD 10 Code
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="width: 170px!important;float: left; white-space: nowrap;" class="ele_center">
                            <asp:Label CssClass="lblicd9code" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ICDCode")%>'></asp:Label>
                        </div>
                        <span class="lblpatientid" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem,"PatientID")%>
                        </span><span class="spnconditioncheck" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem, "ConditionCheck")%>
                        </span><span class="spnhistory" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem, "History")%>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="table_data_list" HeaderStyle-Width="190px">
                    <HeaderTemplate>
                        <div style="width: 190px!important; font-weight: bold; text-align: left;" class="ele_center">
                            Description
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="width: 190px!important; float: left; white-space: nowrap;" class="ele_center">
                            <asp:Label CssClass="lbldesc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Condition")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="table_data_list" HeaderStyle-Width="150px">
                    <HeaderTemplate>
                        <div style="width: 150px!important; font-weight: bold; text-align: left;" class="ele_center">
                            Active Since
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="width: 150px!important;float: left; white-space: nowrap;" class="ele_center">
                            <asp:Label CssClass="lbldos" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"DateOfOnset","{0:MM/dd/yyyy}")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="table_data_list" HeaderStyle-Width="187px">
                    <HeaderTemplate>
                        &nbsp;
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="text-align: center;width: 187px!important;" class="">
                            <img style="cursor: pointer;" src="../../Images/button_edit.jpg" alt="Edit" class="edit_conditions"
                                cid='<%#DataBinder.Eval(Container.DataItem,"ConditionID")%>' />

                            <img style="cursor: pointer;" src="../../Images/button_close.jpg" alt="Delete" class="delete_conditions" onclick="return deleteconNote(<%#DataBinder.Eval(Container.DataItem,"ConditionID")%>);" />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="Label1" Text="No records found" Font-Bold="true" runat="server" Style="white-space: nowrap; padding: 10px;"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
    <%--  <input type="button" value="ADD NEW" name="add_conditions" class="btn_standard" id="add_conditions"
        style="float: right; margin-right: 115px;" />--%>
    <!-- Template ends here -->
    <div id="btn_groups" style="margin-left: 45px; display: none;">
        <input type="button" value="SAVE" id="btn_save" class="btn_standard" />
        <input type="button" value="CANCEL" id="btn_cancel" class="btn_standard" />
    </div>
</div>
<div style="display: none;">
    <div id="divshowconditions" style="z-index: 10000;">
        <div class="edit_conditionsdiv">
            <div style="float: left;">
                <iframe id="frmeditconditions_popup" src="" style="overflow: auto; position: fixed; width: 54%; height: 346px; border: none; margin-top: 40px;"></iframe>
            </div>
            <div class="popup_header Content">
            </div>
            <div class="pop-new">
                <img src="../../Images/popup_close.png" id="popupclose"  class="pop-img" />
            </div>
        </div>
    </div>
</div>



<input type="hidden" id="deleteconNoteId" value="0" />
<div style="display: none;">
    <div id="divdelconnote" style="z-index: 10000;">
        <div class="edit_sessionnotediv">

            <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' onclick="closedeletePopup()" class="pull-right "
                alt="close" />
            <p>Are you sure want to delete this task</p>
            <div class="center-txt">
                <input type="button" id="" name="" value="Yes" class="btn_standard" onclick="ConNoteDelete()" />
                <input type="button" id="" value="No" class="btn_standard" onclick="closedeletePopup()" />
            </div>




        </div>
    </div>
</div>


<script type="text/javascript">

    $(document).ready(function () {
        $("#div_patientsearch").css("display", "block");
        $("#patientsearch_leftpart").css("display", "block");
        $("#imgsearchuser").css("display", "block");
        $("#popupclose").click(function () {

            $("#divshowconditions").dialog('close');
        });



        $(".edit_conditions").click(function () {
            $('.Content').text("Edit");

            var uid = $(this).attr("cid");
            var _desc = $(this).closest("tr").find("[class~=lbldesc]").html();
            var _icd9 = $(this).closest("tr").find("[class~=lblicd9code]").html();
            var _dos = $(this).closest("tr").find("[class~=lbldos]").html();
            var _conditioncheck = $(this).closest("tr").find("[class~=spnconditioncheck]").html();
            var _history = $(this).closest("tr").find("[class~=spnhistory]").html();

            $.ajax({
                type: "POST",
                url: "Conditions.aspx/CacheDetails",
                data: '{"description":"' + _desc + '","icd9code":"' + _icd9 + '","activesince":"' + _dos + '","conditioncheck":"' + _conditioncheck + '","inactivesince":"' + _history + '"}',
                contentType: "application/json; charset=utf-8",
                success: function (msg) {

                },
                error: function (a) {

                },
                complete: function () {
                    $("#frmeditconditions_popup").attr("src", "");
                    $("#frmeditconditions_popup").attr("src", "EditConditions.aspx?cid=" + uid);
                    showpopup();
                }
            });
        });

        $("#add_conditions").click(function () {
            $('.Content').text("Add New");

            // $('<span>Add New</span>').appendTo('.Content');

            // $('<span>Add New</span>').appendTo('.Content');

            $(".edit_conditionsdiv").css("display", "block");

            $("#frmeditconditions_popup").attr("src", "");
            $("#frmeditconditions_popup").attr("src", "EditConditions.aspx");
            showpopup();
        });
    });

    function showpopup() {
        var popuphight = window.innerHeight - 170;
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
    function ClosePopup() {

        $("#divshowconditions").dialog('close');

    }
    $("#popup_close").click(function () {
        parent.popup_close();
    });


    function ConNoteDelete() {
        var uid = $('#deleteconNoteId').val();
        $('.Content').text("Delete");
        //var uid = $(this).attr("cid");

        var data = "{ ConditionID: '" + uid + "'}";

        $.ajax({
            type: "POST",
            url: "Conditions.aspx/DeleteConditions",
            data: data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            mode: "queue",
            success: function (msg) {
                location.reload();
            },
            error: function (xmlhttprequest, textstatus, errorThrown) {
                alert(xmlhttprequest.responseText);
            }
        });
    }


    function confirmDelete() {
        var uid = $('#deleteconNoteId').val();
        $.ajax({
            type: "POST",
            url: "Conditions.aspx/DeleteConditions",
            data: JSON.stringify({ ConditionID: uid }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                showSucess({ text: 'Deleted the selected condition' });
                location.reload();
            },
            error: function (xmlhttprequest, textstatus, errorThrown) {
                showError({ text: 'Sorry an error has occured. Please contact administrator.' });
                alert(xmlhttprequest.responseText);
            }
        });

    }

    function deleteconNote(id) {
        $('#deleteconNoteId').val(id);
        var that = this;
        showMessage({
            title: 'Delete',
            text: 'Do you want to delete the existing condition?',
            answers: ['Yes', 'No'],
            callback: function (btn) {
                if (btn == 'No') {
                    closeConfirmPopup();
                }
                else {
                    confirmDelete();
                }
            }
        });
    }

    function deleteconNote1(id) {

        $('#deleteconNoteId').val(id);

        var popuphight = window.innerHeight - 150;
        var popupwidth = window.innerWidth - 490;

        $("#divdelconnote").dialog(
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
    function closedeletePopup() {
        $("#divdelconnote").dialog('close');
    }



</script>
