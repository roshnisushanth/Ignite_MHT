<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCAllergies.ascx.cs"
    Inherits="Hick.PatientLookUp.UserControls.UCAllergies" %>
<style>    
div.patient_search_right_frame{margin:5px 0;}
.patsearch_heading.patient{width:100%; padding-top:4px;}
.patsearch_heading.patient .btn_standard{margin:0px 50px 0 0!important; padding:5px 10px; float:right;}
.patsearch_heading.patient #popup_close{margin-right:-164px; margin-top: 0!important;}
.ui-dialog .ui-dialog-content{overflow:hidden!important;}

</style>
    <div class="patsearch_heading patient">
        Allergies
         <input type="button" value="Add New" name="add_allergies" class="btn_standard" id="add_allergies"
    style=" margin-left: 508px; margin-top:-28px" />
        <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popup_close" class="pull-right" style="cursor: pointer;margin-top:-42px;"
                alt="close" />
    </div>

<div class="patsearch_border med">

    <div class="conditions_head" style="margin: 3px;">
        <asp:GridView runat="server" ID="gdallergy" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" HeaderStyle-BorderWidth="0" RowStyle-BorderWidth="1px" RowStyle-BorderColor="#D3D3D3">
            <Columns>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div style="width: 110px; font-weight: bold;text-align:left;" class="ele_center">
                            Allergy</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="float: left; white-space: nowrap;" class="ele_center">
                            <asp:Label ID="lblAllergy" CssClass="lblallergytype" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AllergyType")%>'></asp:Label></div>
                        <span class="lblpatientid" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem,"PatientID")%>
                        </span><span class="spnismedallergy" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem, "IsMedicationAllergy")%>
                        </span><span class="spntreatment" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem, "Treatment")%>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div style="width: 250px; font-weight: bold;text-align:left;" class="ele_center">
                            Reaction</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="float: left; white-space: nowrap;text-align:left;" class="ele_center">
                            <asp:Label ID="lblReaction" CssClass="lblreactions" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Reaction")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div style="width: 110px; font-weight: bold;text-align:left;" class="ele_center">
                            Active Since</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="float: left; white-space: nowrap;" class="ele_center">
                            <asp:Label ID="lblactivesince" CssClass="lbldos" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"DateLastOccured")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="text-align: center;" class="">
                         
                            <img style="cursor: pointer;" src="../../Images/button_edit.jpg" alt="Edit" class="edit_conditions"
                                cid='<%#DataBinder.Eval(Container.DataItem,"AllergyID")%>' />
                                
                                 <img style="cursor: pointer;" src="../../Images/button_close.jpg" alt="Delete" class="delete_conditions" 
                                 onclick="return deletealgNote(<%#DataBinder.Eval(Container.DataItem,"AllergyID")%>);" />
                                </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="lblnorecords" Text="No records found" Font-Bold="true" runat="server"
                    Style="white-space: nowrap; padding: 10px;"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
   <%-- <input type="button" value="ADD NEW" name="add_allergies" class="btn_standard" id="add_allergies"
    style="float: right; margin-right: 115px;" />--%>
</div>
<%--<input type="button" value="ADD NEW" name="add_allergies" class="btn_standard" id="add_allergies"
    style="float: right; margin-right: 115px;" />--%>
<!-- Template ends here -->
<div id="btn_groups" style="margin-left: 45px; display: none;">
    <input type="button" value="SAVE" id="btn_save_allergies" class="btn_standard" />
    <input type="button" value="CANCEL" id="btn_cancel_allergies" class="btn_standard" />
</div>
<div style="display: none;">
    <div id="divshowallergies" style="z-index: 10000;">
        <div class="edit_allergiesdiv" style="float: left; margin: 3px; width: 99%;">
            <div style="float: left;">
                <iframe id="patientsearch_popup" src="../ASPX/AddEditAllergies.aspx" style="overflow: auto;
                    position: fixed; width: 54%; height: 346px; border: none; margin-top: 40px;">
                </iframe>
            </div>
            <div class="popup_header Content">
            </div>
            <div class="pop-new">
                <img src="../../Images/popup_close.png" id="popupclose"  class="pop-img" />
            </div>
        </div>
    </div>
</div>


<input type="hidden" id="deletealgNoteId" value="0" />
        <div style="display: none;">
    <div id="divdelalgnote" style="z-index: 10000;">
        <div class="edit_sessionnotediv">
            
             <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' onclick="closedeletePopup()" class="pull-right "
                alt="close" />
              <p>Are you sure want to delete this task</p>
                <div class="center-txt">                     
                       <input type="button" id="" name="" value="Yes" class="btn_standard" onclick="AglNoteDelete()"/> 
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
            $("#divshowallergies").dialog('close');
        });

        
        $(".edit_conditions").click(function () {
            $('.Content').text("Edit");

            var uid = $(this).attr("cid");
            var _reaction = $(this).closest("tr").find("[class~=lblreactions]").html();
            var _allergy = $(this).closest("tr").find("[class~=lblallergytype]").html();
            var _dos = $(this).closest("tr").find("[class~=lbldos]").html();
            var _ismedicalallergy = $(this).closest("tr").find("[class~=spnismedallergy]").html();
            var _treatment = $(this).closest("tr").find("[class~=spntreatment]").html();


            $.ajax({
                type: "POST",
                url: "Allergies.aspx/CacheDetails",
                data: '{"Reaction":"' + _reaction + '","Allergy":"' + _allergy + '","activesince":"' + _dos + '","IsMedicationAllergy":"' + _ismedicalallergy + '","Treatment":"' + _treatment + '"}',
                contentType: "application/json; charset=utf-8",
                success: function (msg) {

                },
                error: function (a) {

                },
                complete: function () {
                    $("#patientsearch_popup").attr("src", "");
                    $("#patientsearch_popup").attr("src", "AddEditAllergies.aspx?cid=" + uid);

                    showpopup();
                }
            });
        });
        $("#add_allergies").click(function () {
            $('.Content').text("Add New");
            showpopup();
        });
    });

    function showpopup() {
        var popuphight = window.innerHeight - 225;
        var popupwidth = window.innerWidth - 490;

        $("#divshowallergies").dialog(
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

    function ClosePopupallergies() {

        $("#divshowallergies").dialog('close');

    }
    $("#popup_close").click(function () {
        parent.popup_close();
    });



    function AglNoteDelete() {
        var uid = $('#deletealgNoteId').val();
            $('.Content').text("Delete");
            //var uid = $(this).attr("cid");
          
            var data = "{ AllergyID: '" + uid + "'}";

            $.ajax({
                    type: "POST",
                    url: "Allergies.aspx/DeleteAllergies",
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
        function deletealgNote(id) {
            
        $('#deletealgNoteId').val(id);

            var popuphight = window.innerHeight - 150;
            var popupwidth = window.innerWidth - 490;

            $("#divdelalgnote").dialog(
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
        $("#divdelalgnote").dialog('close');
    }



</script>
