<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCImmunizations.ascx.cs" Inherits="Hick.PatientLookUp.UserControls.UCImmunizations" %>
<style>    
div.patient_search_right_frame{margin:5px 0;}
.patsearch_heading.patient{width:100%; padding-top:4px;}
.patsearch_heading.patient .btn_standard{margin:0px 50px 0 0!important; padding:5px 10px; float:right;}
.patsearch_heading.patient #popup_close{margin-right:-164px; margin-top: 0!important;}

</style>
<div class="patsearch_heading patient">
        Immunizations
         <input type="button" value="Add New" name="add_immunizations" class="btn_standard" id="add_immunizations"
        style=" margin-left: 508px;margin-top:-28px;" />
        <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popup_close" class="pull-right" style="cursor: pointer;margin-top:-42px;"
                alt="close" />
    </div>
    <div class="patsearch_border med">
    <div class="conditions_head" style="margin: 3px;">
        <asp:GridView runat="server" ID="gdimmunizations" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" HeaderStyle-BorderWidth="0" RowStyle-BorderWidth="1px" RowStyle-BorderColor="#D3D3D3">
            <Columns>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div style="width: 130px; font-weight: bold;text-align:left;" class="ele_center">
                            Immunization Name</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="width: 110px;text-align:left;" class="ele_center">
                            <asp:Label ID="lblImmunization" CssClass="lblImmunization" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ImmunizationType")%>'></asp:Label></div>
                        <span class="lblpatientid" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem,"PatientID")%>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div style="width: 210px; font-weight: bold;text-align:left;" class="ele_center">
                            Date Received</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style=" white-space: nowrap;text-align:left;" class="ele_center">
                            <asp:Label ID="lbldate" CssClass="lbldate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AdministrationDate","{0:MM/dd/yyyy}")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="text-align:left;" class="">                      
                            <img style="cursor: pointer;" src="../../Images/button_edit.jpg" alt="Edit" class="edit_Immunization" mid='<%#DataBinder.Eval(Container.DataItem,"ImmunizationID")%>' />
                              
                              <img style="cursor: pointer;" src="../../Images/button_close.jpg" alt="Delete" class="delete_conditions" 
                                 onclick="return deleteimmNote(<%#DataBinder.Eval(Container.DataItem,"ImmunizationID")%>);" />

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
        <input type="button" value="SAVE" id="btn_save_" class="btn_standard" />
        <input type="button" value="CANCEL" id="btn_cancel" class="btn_standard" />
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



<input type="hidden" id="deleteimmNoteId" value="0" />
        <div style="display: none;">
    <div id="divdelimmnote" style="z-index: 10000;">
        <div class="edit_sessionnotediv">
            
             <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' onclick="closeimmPopup()" class="pull-right "
                alt="close" />
              <p>Are you sure want to delete this task</p>
                <div class="center-txt">                     
                       <input type="button" id="" name="" value="Yes" class="btn_standard" onclick="ImmNoteDelete()"/> 
                        <input type="button" id="" value="No" class="btn_standard" onclick="closeimmPopup()"/>
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

       

        $(".edit_Immunization").click(function () {
            $('.Content').text("Edit");

            var uid = $(this).attr("mid");
            var _date = $(this).closest("tr").find("[class~=lbldate]").html();
            var _immunization = $(this).closest("tr").find("[class~=lblImmunization]").html();
            

            $.ajax({
                type: "POST",
                url: "Immunizations.aspx/CacheDetails",
                data: '{"Immunization":"' + _immunization + '","Date":"' + _date + '"}',
                contentType: "application/json; charset=utf-8",
                success: function (msg) {

                },
                error: function (a) {

                },
                complete: function () {
                    $("#frameMedicationdiv").attr("src", "");
                    $("#frameMedicationdiv").attr("src", "EditImmunizations.aspx?cid=" + uid);

                    showpopup();
                }
            });
        });
        $("#add_immunizations").click(function () {
            $('.Content').text("Add New");

            $("#frameMedicationdiv").attr("src", "");
            $("#frameMedicationdiv").attr("src", "EditImmunizations.aspx");
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



    function ImmNoteDelete() {
        var uid = $('#deleteimmNoteId').val();
            $('.Content').text("Delete");
            //var uid = $(this).attr("cid");
          
             var data = "{ ImmunizationID: '" + uid + "'}";

                $.ajax({
                    type: "POST",
                    url: "Immunizations.aspx/DeleteImmunizations",
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
        function deleteimmNote(id) {
            
        $('#deleteimmNoteId').val(id);

            var popuphight = window.innerHeight - 150;
            var popupwidth = window.innerWidth - 490;

            $("#divdelimmnote").dialog(
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


    function closeimmPopup() {
        $("#divdelimmnote").dialog('close');
    }

</script>