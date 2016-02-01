<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCEncounters.ascx.cs"
    Inherits="Hick.PatientLookUp.UserControls.UCEncounters" %>
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
  .ui-dialog.ui-widget.ui-widget-content.ui-corner-all.ui-front.ui-draggable[aria-describedby="divshowconditions"] {
    height: 400px!important;
}
.ui-dialog.ui-widget.ui-widget-content.ui-corner-all.ui-front.ui-draggable[aria-describedby="divshowencounter"] #divshowencounter{height:auto!important;}
  .ui-dialog.ui-widget.ui-widget-content.ui-corner-all.ui-front.ui-draggable[aria-describedby="divshowconditions"] div#divshowconditions {
    height: auto!important;
}
</style>
<div class="patsearch_heading patient">
    Encounters
    <input type="button" value="Add New" name="add_conditions" class="btn_standard" id="add_conditions"
        style="margin-left: 508px; margin-top: -28px;" />
    <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popup_close" class="pull-right"
        style="cursor: pointer; margin-top: -42px;" alt="close" />
</div>
<div class="patsearch_border med">
    <div class="conditions_head" style="margin: 3px;">
        <asp:GridView runat="server" ID="gdencounters" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" HeaderStyle-BorderWidth="0" RowStyle-BorderWidth="1px" RowStyle-BorderColor="#D3D3D3">
            <Columns>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div style="width: 110px; font-weight: bold; text-align: left;" class="ele_center">
                            Date</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="float: left; white-space: nowrap;" class="ele_center">
                            <asp:Label ID="lblvisitdate" CssClass="lblvisitdate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Visitdate","{0:MM/dd/yyyy}")%>'></asp:Label></div>
                        <span class="lblpatientid" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem,"PatientID")%>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div style="width: 250px; font-weight: bold; text-align: left;" class="ele_center">
                            Doctor</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="float: left; white-space: nowrap;" class="ele_center">
                            <asp:Label ID="lbldoctor" CssClass="lbldoctor" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"DoctorName")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div style="width: 110px; font-weight: bold; text-align: left;" class="ele_center">
                           Reason</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="float: left; white-space: nowrap;" class="ele_center">
                            <asp:Label ID="lblvisitreason" CssClass="lblvisitreason" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"VisitReason")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div style="width: 110px; font-weight: bold; text-align: left;" class="ele_center">
                           Diagnoses</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="float: left; white-space: nowrap;" class="ele_center">
                            <asp:Label ID="lbldiagnosis" CssClass="lbldiagnosis" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"VisitDiagnosis")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="text-align: center;" class="enco-butt">
                            <img style="cursor: pointer;" src="../../Images/button_edit.jpg" alt="Edit" class="edit_encounter"
                                cid='<%#DataBinder.Eval(Container.DataItem,"DoctorVisitId")%>' />
                             <img style="cursor: pointer;" src="../../Images/button_close.jpg" alt="Delete" class="delete_conditions" 
                                 onclick="return deleteencNote(<%#DataBinder.Eval(Container.DataItem,"DoctorVisitId")%>);" />
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
                <img src="../../Images/popup_close.png" id="popupclose"  class="pop-img" />
            </div>
        </div>
    </div>
</div>

<input type="hidden" id="deleteencNoteId" value="0" />
        <div style="display: none;">
    <div id="divdelencnote" style="z-index: 10000;">
        <div class="edit_sessionnotediv">
            
             <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' onclick="closedeletePopup()" class="pull-right "
                alt="close" />
              <p>Are you sure want to delete this task</p>
                <div class="center-txt">                     
                       <input type="button" id="" name="" value="Yes" class="btn_standard" onclick="EncNoteDelete()"/> 
                        <input type="button" id="" value="No" class="btn_standard" onclick="closedeletePopup()"/>
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

       

        $(".edit_encounter").click(function () {
            $('.Content').text("Edit");

            var uid = $(this).attr("cid");
           
            var _visitdate = $(this).closest("tr").find("[class~=lblvisitdate]").html();
            var _doctor = $(this).closest("tr").find("[class~=lbldoctor]").html();
            var _reason = $(this).closest("tr").find("[class~=lblvisitreason]").html();
            var _diagnosis = $(this).closest("tr").find("[class~=lbldiagnosis]").html();


            $.ajax({
                type: "POST",
                url: "Encounters.aspx/CacheDetails",
                data: '{"VisitDate":"' + _visitdate + '","DoctorName":"' + _doctor + '","VisitReason":"' + _reason + '","VisitDiagnosis":"' + _diagnosis + '"}',
                contentType: "application/json; charset=utf-8",
                success: function (msg) {

                },
                error: function (a) {

                },
                complete: function () {
                    $("#frmeditconditions_popup").attr("src", "");
                    $("#frmeditconditions_popup").attr("src", "EditEncounters.aspx?cid=" + uid);
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
            $("#frmeditconditions_popup").attr("src", "EditEncounters.aspx");
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
   
    $("#popup_close").click(function () {
        parent.popup_close();
    });



    function EncNoteDelete() {
        var uid = $('#deleteencNoteId').val();
            $('.Content').text("Delete");
            //var uid = $(this).attr("cid");
          
            var data = "{ EncounterID: '" + uid + "'}";

                $.ajax({
                    type: "POST",
                    url: "Encounters.aspx/DeleteEncounter",
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
        function deleteencNote(id) {
            
        $('#deleteencNoteId').val(id);

            var popuphight = window.innerHeight - 150;
            var popupwidth = window.innerWidth - 490;

            $("#divdelencnote").dialog(
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
        $("#divdelencnote").dialog('close');
    }

    function closeencPopup() {
        $("#divshowconditions").dialog('close');
    }

</script>
