<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCTestAndProcedures.ascx.cs" Inherits="Hick.PatientLookUp.UserControls.UCTestAndProcedures" %>

<style>    
div.patient_search_right_frame{margin:5px 0;}
.patsearch_heading.patient{width:100%; padding-top:4px;}
.patsearch_heading.patient .btn_standard{margin:0px 50px 0 0!important; padding:5px 10px; float:right;}
.patsearch_heading.patient #popup_close{margin-right:-164px; margin-top: 0!important;}
.ui-dialog .ui-dialog-content{overflow:hidden!important;}
.patsearch_border.med{overflow-x:hidden; overflow-y:scroll;}
span#lblprocedure {
    word-wrap: break-word;
    width: 250px;
    display: inherit;
}
    
.patsearch_heading.patient #testandprocedurepopup_close { margin-right: -164px;}
span#lblprocedure {
    width: 100%!important;
}
   .conditions_head .ele_center {
    width: 180px!important;
        white-space: inherit!important;
    }
.conditions_head td .ele_center{font-size:12px;}

</style>
    <div class="patsearch_heading patient">
       <span>Tests and Procedures</span>
         <input type="button" value="Add New" name="add_testandprocedure" class="btn_standard" id="add_testandprocedure"
    style=" margin-left: 508px; margin-top:-28px" />
        <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="testandprocedurepopup_close" class="pull-right" style="cursor: pointer;margin-top:-42px;"
                alt="close" />
    </div>

<div class="patsearch_border med">

    <div class="conditions_head" style="margin: 3px;">
        <asp:GridView runat="server" ID="gdtestandprocedure" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" HeaderStyle-BorderWidth="0" RowStyle-BorderWidth="1px" RowStyle-BorderColor="#D3D3D3">
            <Columns>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div style="width: 110px; font-weight: bold;text-align:left;" class="ele_center">
                           Procedure</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="float: left; white-space: nowrap;" class="ele_center">
                            <asp:Label ID="lblprocedure" CssClass="lblprocedure" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SurgeriesProcedure")%>'></asp:Label></div>
                        <span class="lblpatientid" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem,"PatientID")%>
                        </span><span class="lblSurgeriesId" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem, "SurgeriesId")%>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div style="width: 250px; font-weight: bold;text-align:left;" class="ele_center">
                           Description</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="float: left; white-space: nowrap;text-align:left;" class="ele_center">
                            <asp:Label ID="lbldiscription" CssClass="lbldiscription" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Description")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div style="width: 110px; font-weight: bold;text-align:left;" class="ele_center">
                           Test Date</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="float: left; white-space: nowrap;" class="ele_center">
                            <asp:Label ID="lbldate" CssClass="lbldate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Date","{0:MM/dd/yyyy}")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="enco-butt">
                         
                            <img style="cursor: pointer;" src="../../Images/button_edit.jpg" alt="Edit" class="edit_testandprocedure"
                                cid='<%#DataBinder.Eval(Container.DataItem,"SurgeriesId")%>' />

                                    <img style="cursor: pointer;" src="../../Images/button_close.jpg" alt="Delete" class="delete_conditions" 
                                 onclick="return deletetestNote(<%#DataBinder.Eval(Container.DataItem,"SurgeriesId")%>);" />
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
    <input type="button" value="SAVE" id="btn_save_testandprocedure" class="btn_standard" />
    <input type="button" value="CANCEL" id="btn_cancel_testandprocedure" class="btn_standard" />
</div>
<div style="display: none;">
    <div id="divtestandprocedure" style="z-index: 10000;">
        <div class="edit_allergiesdiv" style="float: left; margin: 3px; width: 99%;">
            <div style="float: left;">
                <iframe id="testandprocedure_popup" src="../ASPX/AddEditTestAndProcedures.aspx" style="overflow: auto;
                    position: fixed; width: 54%; height: 346px; border: none; margin-top: 40px;">
                </iframe>
            </div>
            <div class="popup_header Content">
            </div>
            <div class="cls-popup">
                <img src="../../Images/popup_close.png" id="popupclosetestandprocedure"  />
            </div>
        </div>
    </div>
</div>

<input type="hidden" id="deletetestNoteId" value="0" />
        <div style="display: none;">
    <div id="divdeltestnote" style="z-index: 10000;">
        <div class="edit_sessionnotediv">
            
             <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' onclick="closedeletePopup()" class="pull-right "
                alt="close" />
              <p>Are you sure want to delete this task</p>
                <div class="center-txt">                     
                       <input type="button" id="" name="" value="Yes" class="btn_standard" onclick="TestNoteDelete()"/> 
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

        $("#popupclosetestandprocedure").click(function () {
            $("#divshowtestandprocedure").dialog('close');
        });

        
        $(".edit_testandprocedure").click(function () {
            $('.Content').text("Edit");

            var uid = $(this).attr("cid");
            var _lblprocedure = $(this).closest("tr").find("[class~=lblprocedure]").html();
            var _lbldiscription = $(this).closest("tr").find("[class~=lbldiscription]").html();
            var _lbldate = $(this).closest("tr").find("[class~=lbldate]").html();

            $.ajax({
                type: "POST",
                url: "TestAndProcedures.aspx/CacheDetails",
                data: '{"Procedure":"' + _lblprocedure + '","Discription":"' + _lbldiscription + '","Date":"' + _lbldate + '"}',
                contentType: "application/json; charset=utf-8",
                success: function (msg) {

                },
                error: function (a) {

                },
                complete: function () {
                    $("#testandprocedure_popup").attr("src", "");
                    $("#testandprocedure_popup").attr("src", "AddEditTestAndProcedures.aspx?cid=" + uid);

                    showpopuptestandprocedure();
                }
            });
        });
        $("#add_testandprocedure").click(function () {
            $('.Content').text("Add New");
            showpopuptestandprocedure();
        });
    });

    function showpopuptestandprocedure() {
        var popuphight = window.innerHeight - 225;
        var popupwidth = window.innerWidth - 490;

        $("#divtestandprocedure").dialog(
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
    $("#popupclosetestandprocedure").click(function () {
        $("#divtestandprocedure").dialog('close');
    });

    function ClosePopuptestandprocedure() {

        $("#divtestandprocedure").dialog('close');

    }

    $("#testandprocedurepopup_close").click(function () {
        parent.popup_close();
    });



    function TestNoteDelete() {
        var uid = $('#deletetestNoteId').val();
            $('.Content').text("Delete");
            //var uid = $(this).attr("cid");
          
           var data = "{ ProcedureID: '" + uid + "'}";

            $.ajax({
                type: "POST",
                url: "TestAndProcedures.aspx/DeleteProcedure",
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
        function deletetestNote(id) {
            
        $('#deletetestNoteId').val(id);

            var popuphight = window.innerHeight - 150;
            var popupwidth = window.innerWidth - 490;

            $("#divdeltestnote").dialog(
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
        $("#divdeltestnote").dialog('close');
    }

    
    
</script>
