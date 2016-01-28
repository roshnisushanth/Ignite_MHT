<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCMedications.ascx.cs"
    Inherits="Hick.PatientLookUp.UserControls.UCMedications" %>
<style>    
div.patient_search_right_frame{margin:5px 0;}
.patsearch_heading.patient{width:100%; padding-top:4px;}
.patsearch_heading.patient .btn_standard{margin:0px 50px 0 0!important; padding:5px 10px; float:right;}
.patsearch_heading.patient #popup_close{margin-right:-164px; margin-top: 0!important;}
.ui-dialog .ui-dialog-content{overflow:hidden!important;}

#gdconditions tr:first-child {
            position: absolute;
            font-weight: bold; top: 47px;
        }   
    #gdconditions{margin-top:30px;}
    #gdconditions tr:first-child th {
    background: #fff;
}

</style>

    <div class="patsearch_heading patient">
        Medications
         <input type="button" value="Add New" name="add_medications" class="btn_standard" id="add_medications" style=" margin-left: 508px;margin-top:-28px;" />
        <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popup_close" class="pull-right" style="cursor: pointer;margin-top:-42px;"
                alt="close" />
    </div>
<div class="patsearch_border med">
    <div class="conditions_head" style="margin: 3px;">
        <asp:GridView runat="server" ID="gdconditions" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" HeaderStyle-BorderWidth="0" RowStyle-BorderWidth="1px" RowStyle-BorderColor="#D3D3D3">
            <Columns>
                <asp:TemplateField ItemStyle-CssClass="table_data_list"  HeaderStyle-Width="170px">
                    <HeaderTemplate>
                        <div style="width: 170px; font-weight: bold;text-align:left;" class="ele_center">
                            Medications</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="width: 170px!important;text-align:left;" class="ele_center">
                            <asp:Label ID="lblDescription" CssClass="lbldesc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Description")%>'></asp:Label></div>
                        <span class="lblpatientid" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem,"PatientID")%>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="table_data_list" HeaderStyle-Width="180px">
                    <HeaderTemplate>
                        <div style="width: 180px!important; font-weight: bold;text-align:left;" class="ele_center">
                            Dosage</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                       
                        <div style=" white-space: nowrap;text-align:left; width: 180px!important;" class="ele_center">

                             <asp:Label ID="lblDosage" CssClass="lbldos" runat="server" style="display:none;" Text='<%#Eval("Dosage") %> '></asp:Label>
                            <asp:Label ID="lblDosageandunit" CssClass="lbldosageunits" runat="server" Text='<%#Eval("Dosage") + " " + Eval("DosageUnits") %> '></asp:Label></div>
                          <asp:Label ID="lblDosageunits" CssClass="lbldosunits" runat="server"  style="display:none;" Text='<%#Eval("DosageUnits") %> '></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list" HeaderStyle-Width="210px">
                    <HeaderTemplate>
                        <div style="width: 210px; font-weight: bold;text-align:left;" class="ele_center">
                            Active Since</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style=" white-space: nowrap;text-align:left; width: 210px!important;" class="ele_center">
                            <asp:Label ID="lblActivedate" CssClass="lbldate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Date")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list" HeaderStyle-Width="178px">
                    <HeaderTemplate>&nbsp;
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="text-align:left; width: 178px!important;" class="">                      
                            <img style="cursor: pointer;" src="../../Images/button_edit.jpg" alt="Edit" class="edit_conditions" mid='<%#DataBinder.Eval(Container.DataItem,"MedicationID")%>' />
                                    <img style="cursor: pointer;" src="../../Images/button_close.jpg" alt="Delete" class="delete_med" 
                                 onclick="return deletemedNote(<%#DataBinder.Eval(Container.DataItem,"MedicationID")%>);" />
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
    <%--<input type="button" value="ADD NEW" name="add_medications" class="btn_standard"
        id="add_medications" style="float: right; margin-right: 115px;" />--%>
    <!-- Template ends here -->
    <div id="btn_groups" style="margin-left: 45px; display: none;">
        <input type="button" value="SAVE" id="btn_save_medications" class="btn_standard" />
        <input type="button" value="CANCEL" id="btn_cancel_medications" class="btn_standard" />
    </div>
</div>
<div style="display: none;">
    <div id="divshowmedications" style="z-index: 10000;">
        <div class="edit_medicationdiv">
            <div style="float: left;">
                <iframe id="frameMedicationdiv" src="" style="overflow: auto; position: fixed; width: 54%;
                    height: 346px; border: none; margin-top: 40px;"></iframe>
            </div>
            <div class="popup_header Content">
            </div>
            <div class="pop-new">
                <img src="../../Images/popup_close.png" id="popupclose" class="pop-img" />
            </div>
        </div>
    </div>
</div>

<input type="hidden" id="deletemedNoteId" value="0" />
        <div style="display: none;">
    <div id="divdelmednote" style="z-index: 10000;">
        <div class="edit_sessionnotediv">
            
             <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' onclick="closemedPopup()" class="pull-right "
                alt="close" />
              <p>Are you sure want to delete this task</p>
                <div class="center-txt">                     
                       <input type="button" id="" name="" value="Yes" class="btn_standard" onclick="MedNoteDelete()"/> 
                        <input type="button" id="" value="No" class="btn_standard" onclick="closemedPopup()"/>
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
            $("#divshowmedications").dialog('close');
        });


        $(".edit_conditions").click(function () {
            $('.Content').text("Edit");

            var uid = $(this).attr("mid");
            var _dosage = $(this).closest("tr").find("[class~=lbldos]").html();
            var _desc = $(this).closest("tr").find("[class~=lbldesc]").html();
            var _dos = $(this).closest("tr").find("[class~=lbldate]").html();
            var _dosageunits = $(this).closest("tr").find("[class~=lbldosunits]").html();

            $.ajax({
                type: "POST",
                url: "Medications.aspx/CacheDetails",
                data: '{"description":"' + _desc + '","dosage":"' + _dosage + '","activesince":"' + _dos + '","dosageunits":"' + _dosageunits + '"}',
                contentType: "application/json; charset=utf-8",
                success: function (msg) {

                },
                error: function (a) {

                },
                complete: function () {
                    $("#frameMedicationdiv").attr("src", "");
                    $("#frameMedicationdiv").attr("src", "AddEditMedications.aspx?cid=" + uid);

                    showpopup();
                }
            });
        });
        $("#add_medications").click(function () {
            $('.Content').text("Add New");

            $("#frameMedicationdiv").attr("src", "");
            $("#frameMedicationdiv").attr("src", "AddEditMedications.aspx");
            showpopup();
        });
    });

    function showpopup() {
        var popuphight = window.innerHeight - 150;
        var popupwidth = window.innerWidth - 490;

        $("#divshowmedications").dialog(
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

        $("#divshowmedications").dialog('close');

    }

    $("#popup_close").click(function () {
        parent.popup_close();
    });



    function MedNoteDelete() {
        var uid = $('#deletemedNoteId').val();
            $('.Content').text("Delete");
            //var uid = $(this).attr("cid");
          
            var data = "{ MedicationID: '" + uid + "'}";

            $.ajax({
                type: "POST",
                url: "Medications.aspx/DeleteMedications",
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
        function deletemedNote(id) {
            
        $('#deletemedNoteId').val(id);

            var popuphight = window.innerHeight - 150;
            var popupwidth = window.innerWidth - 490;

            $("#divdelmednote").dialog(
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
    function closemedPopup() {
        $("#divdelmednote").dialog('close');
    }


</script>
