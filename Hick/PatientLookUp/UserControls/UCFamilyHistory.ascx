<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCFamilyHistory.ascx.cs"
    Inherits="Hick.PatientLookUp.UserControls.UCFamilyHistory" %>
<style>
    div.patient_search_right_frame
    {
        margin: 5px 0;
    }
    .patsearch_heading.patient
    {
        width: 100%;
        padding-top: 4px;
    }
    .patsearch_heading.patient .btn_standard
    {
        margin: 0px 50px 0 0 !important;
        padding: 5px 10px;
        float: right;
    }
    .patsearch_heading.patient #popup_close
    {
        margin-right: -164px;
        margin-top: 0 !important;
    }
</style>
<div class="patsearch_heading patient">
    Family History
    <input type="button" value="Add New" name="add_conditions" class="btn_standard" id="add_conditions"
        style="margin-left: 508px; margin-top: -28px;" />
    <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popup_close" class="pull-right"
        style="cursor: pointer; margin-top: -42px;" alt="close" />
</div>
<div class="patsearch_border">
    <div class="conditions_head" style="margin: 3px;">
        <asp:GridView runat="server" ID="gdfamilyhistory" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" HeaderStyle-BorderWidth="0" RowStyle-BorderWidth="1px" RowStyle-BorderColor="#D3D3D3">
            <Columns>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div style="width: 110px; font-weight: bold; text-align: left;" class="ele_center">
                            Relationship</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="float: left; white-space: nowrap;" class="ele_center">
                            <asp:Label ID="lblRelationship" CssClass="lblRelationship" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Relationship")%>'></asp:Label></div>
                        <span class="lblpatientid" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem,"PatientID")%>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div style="width: 250px; font-weight: bold; text-align: left;" class="ele_center">
                            Condition</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="float: left; white-space: nowrap;" class="ele_center">
                            <asp:Label ID="lblCondition" CssClass="lblCondition" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ConditionName")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div style="width: 110px; font-weight: bold; text-align: left;" class="ele_center">
                            Onset Date</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="float: left; white-space: nowrap;" class="ele_center">
                            <asp:Label ID="lbldate" CssClass="lbldate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"OnsetDate","{0:MM/dd/yyyy}")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="text-align: center;" class="">
                            <img style="cursor: pointer;" src="../../Images/button_edit.jpg" alt="Edit" class="edit_familyhistory"
                                cid='<%#DataBinder.Eval(Container.DataItem,"PatientFamilyHistoryID")%>' />
                             <img style="cursor: pointer;" src="../../Images/button_close.jpg" alt="Delete" class="delete_conditions" 
                                 onclick="return deletefamNote(<%#DataBinder.Eval(Container.DataItem,"PatientFamilyHistoryID")%>);" />
                                </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="Label1" Text="No records found" Font-Bold="true" runat="server" Style="white-space: nowrap;
                    padding: 10px;"></asp:Label>
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
                <iframe id="frmeditconditions_popup" src="" style="overflow: auto; position: fixed;
                    width: 54%; height: 346px; border: none; margin-top: 40px;"></iframe>
            </div>
            <div class="popup_header Content">
            </div>
            <div class="pop-new">
                <img src="../../Images/popup_close.png" id="popupclose"   class="pop-img" />
            </div>
        </div>
    </div>
</div>

<input type="hidden" id="deletefamNoteId" value="0" />
        <div style="display: none;">
    <div id="divdelfamnote" style="z-index: 10000;">
        <div class="edit_sessionnotediv">
            
             <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' onclick="closefamPopup()" class="pull-right "
                alt="close" />
              <p>Are you sure want to delete this task</p>
                <div class="center-txt">                     
                       <input type="button" id="" name="" value="Yes" class="btn_standard" onclick="FamNoteDelete()"/> 
                        <input type="button" id="" value="No" class="btn_standard" onclick="closefamPopup()"/>
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

        

        $(".edit_familyhistory").click(function () {
            $('.Content').text("Edit");

            var uid = $(this).attr("cid");
            var _relationship = $(this).closest("tr").find("[class~=lblRelationship]").html();
            var _condition = $(this).closest("tr").find("[class~=lblCondition]").html();
            var _date = $(this).closest("tr").find("[class~=lbldate]").html();
          

            $.ajax({
                type: "POST",
                url: "FamilyHistory.aspx/CacheDetails",
                data: '{"Relationship":"' + _relationship + '","ConditionName":"' + _condition + '","OnsetDate":"' + _date + '"}',
                contentType: "application/json; charset=utf-8",
                success: function (msg) {

                },
                error: function (a) {

                },
                complete: function () {
                    $("#frmeditconditions_popup").attr("src", "");
                    $("#frmeditconditions_popup").attr("src", "AddEditFamilyHistory.aspx?cid=" + uid);
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
            $("#frmeditconditions_popup").attr("src", "AddEditFamilyHistory.aspx");
            showpopup();
        });
    });

    function showpopup() {
        var popuphight = window.innerHeight - 200;
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

 

    function FamNoteDelete() {
        var uid = $('#deletefamNoteId').val();
            $('.Content').text("Delete");
            //var uid = $(this).attr("cid");
          
           var data = "{ PatientFamilyHistoryID: '" + uid + "'}";

                $.ajax({
                    type: "POST",
                    url: "FamilyHistory.aspx/DeleteFamilyHistory",
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
        function deletefamNote(id) {
            
        $('#deletefamNoteId').val(id);

            var popuphight = window.innerHeight - 150;
            var popupwidth = window.innerWidth - 490;

            $("#divdelfamnote").dialog(
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
    function closefamPopup() {
        $("#divdelfamnote").dialog('close');
    }



</script>
