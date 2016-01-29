<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCLabResults.ascx.cs" Inherits="Hick.PatientLookUp.UserControls.UCLabResults" %>
<style>    
div.patient_search_right_frame{margin:5px 0;}
.patsearch_heading.patient{width:100%; padding-top:4px;}
.patsearch_heading.patient .btn_standard{margin:0px 50px 0 0!important; padding:5px 10px; float:right;}
.patsearch_heading.patient #popup_close{margin-right:-164px; margin-top: 0!important;}
.ui-dialog .ui-dialog-content{overflow:hidden!important;}
.patsearch_heading.patient #labresultpopup_close { margin-right: -164px;}

</style> 
    <div class="patsearch_heading patient">
       Lab Results
         <input type="button" value="Add New" name="add_labresult" class="btn_standard" id="add_labresult"
    style=" margin-left: 508px; margin-top:-28px" />
        <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="labresultpopup_close" class="pull-right" 
                alt="close" style="cursor:pointer;"/>
    </div>

<div class="patsearch_border med">

    <div class="conditions_head" style="margin: 3px;">
        <asp:GridView runat="server" ID="gdlabresult" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" HeaderStyle-BorderWidth="0" RowStyle-BorderWidth="1px" RowStyle-BorderColor="#D3D3D3">
            <Columns>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div style=" font-weight: bold;text-align:left;" class="ele_center">
                           Date</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="float: left; white-space: nowrap;" class="ele_center">
                            <asp:Label ID="lbldate" CssClass="lbldate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Date")%>'></asp:Label></div>
                        <span class="lblpatientid" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem,"PatientID")%>
                        </span><span class="lbllabId" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem, "LabImagingId")%>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div style=" font-weight: bold;text-align:left;" class="ele_center">
                           Test Type</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="float: left; white-space: nowrap;text-align:left;" class="ele_center">
                            <asp:Label ID="lblTestType" CssClass="lblTestType" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestType")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div style=" font-weight: bold;text-align:left;" class="ele_center">
                           Results</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="float: left; white-space: nowrap;" class="ele_center">
                            <asp:Label ID="lblresult" CssClass="lblresult" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Results")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div style=" font-weight: bold;text-align:left;" class="ele_center">
                           Requesting Doctor</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="float: left; white-space: nowrap;" class="ele_center">
                            <asp:Label ID="lbldoctor" CssClass="lbldoctor" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RequestingDoctor")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div style=" font-weight: bold;text-align:left;" class="ele_center">
                         Administered by</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="float: left; white-space: nowrap;" class="ele_center">
                            <asp:Label ID="lbladmin" CssClass="lbladmin" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AdministeredBy")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="enco-butt">
                         
                            <img style="cursor: pointer;" src="../../Images/button_edit.jpg" alt="Edit" class="edit_labresult"
                                cid='<%#DataBinder.Eval(Container.DataItem,"LabImagingId")%>' />
                                   <img style="cursor: pointer;" src="../../Images/button_close.jpg" alt="Delete" class="delete_labresult"
                                cid='<%#DataBinder.Eval(Container.DataItem,"LabImagingId")%>' /></div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="lblnorecords" Text="No records found" Font-Bold="true" runat="server"
                    Style="white-space: nowrap; padding: 10px;"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>


<div style="display: none;">
    <div id="divlabresult" style="z-index: 10000;height: 385px;">
        <div class="edit_allergiesdiv" style="float: left; margin: 3px; width: 99%;">
            <div style="float: left;">
                <iframe id="labresult_popup" src="../ASPX/AddEditLabResults.aspx" style="overflow: auto;
                    position: fixed; width: 54%; height: 385px; border: none; margin-top: 40px;">
                </iframe>
            </div>
            <div class="popup_header Content">
            </div>
            <div class="cls-popup">
                <img src="../../Images/popup_close.png" id="popupcloselabresult" />
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $("#div_patientsearch").css("display", "block");
        $("#patientsearch_leftpart").css("display", "block");
        $("#imgsearchuser").css("display", "block");

        

        $(".delete_labresult").click(function () {
           
            $('.Content').text("Delete");
            var uid = $(this).attr("cid");
          
            var data = "{ LabResultID: '" + uid + "'}";

            $.ajax({
                type: "POST",
                url: "LabResults.aspx/DeleteLabResults",
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
        });
        $(".edit_labresult").click(function () {
            $('.Content').text("Edit");

            var uid = $(this).attr("cid");
            var _Date = $(this).closest("tr").find("[class~=lbldate]").html();
            var _TestType = $(this).closest("tr").find("[class~=lblTestType]").html();
            var _Results = $(this).closest("tr").find("[class~=lblresult]").html();
            var _Doctor = $(this).closest("tr").find("[class~=lbldoctor]").html();
            var _Adminby = $(this).closest("tr").find("[class~=lbladmin]").html();
            $.ajax({
                type: "POST",
                url: "LabResults.aspx/CacheDetails",
                data: '{"Date":"' + _Date + '","TestType":"' + _TestType + '","Results":"' + _Results + '","Doctor":"' + _Doctor + '","Adminby":"' + _Adminby + '"}',
                contentType: "application/json; charset=utf-8",
                success: function (msg) {

                },
                error: function (a) {

                },
                complete: function () {
                    $("#labresult_popup").attr("src", "");
                    $("#labresult_popup").attr("src", "AddEditLabResults.aspx?cid=" + uid);

                    showpopuplabresult();
                }
            });
        });
        $("#add_labresult").click(function () {
            $('.Content').text("Add New");
            showpopuplabresult();
        });
    });

    function showpopuplabresult() {
        var popuphight = window.innerHeight - 225;
        var popupwidth = window.innerWidth - 490;

        $("#divlabresult").dialog(
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

    $("#popupcloselabresult").click(function () {
        $("#divlabresult").dialog('close');
    });

    function ClosePopup() {

        $("#divlabresult").dialog('close');

    }
    $("#labresultpopup_close").click(function () {
        parent.popup_close();
    });

 function ClosePopupresu() {

        $("#divlabresult").dialog('close');

    }

   
</script>
