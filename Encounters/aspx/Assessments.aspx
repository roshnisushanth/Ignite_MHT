<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Assessments.aspx.cs" Inherits="Hick.Encounters.ASPX.Assessments" MetaKeywords="Assessments" %>

<%--<%@ Register  TagName="AssessmentSummary" TagPrefix="Assessment" Src="~/Encounters/UserControls/AssessmentSummary.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplPatientLookUp" runat="server">
    <Assessment:AssessmentSummary ID="assessmentSummary" runat="server" />
</asp:Content>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Content/style.css" rel="stylesheet" />
    <link href="../../Content/patientlookup.css" rel="stylesheet" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
    <script src="//code.jquery.com/ui/1.11.1/jquery-ui.js"></script>

    <style>
        #gdconditions [itemstyle-cssclass="table_data_list"] {
    width: 25%!important;
}
        .eleh_center {
    margin: 5px !important;
     margin-left: 0 !important; 
    width: 25%!important; text-align: center;
}
    </style>

</head>
<body style="padding: 0px; position: relative;">
    <div class="">
        <form id="form1" runat="server">
            <div class="patsearch_heading">
                    Assessment Summary
                <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="ent_popup_close" class="pull-right" style="cursor: pointer;margin-top:-3px;"
                alt="close" />
                </div>
            <div class="patsearch_border_assess">
                <%--<div class="patsearch_heading">
                    Assessment Summary
                </div>--%>
                <input type="button" value="Start New Encounter" name="strt_new" class="btn_standard" id="start_new_encounter" style="float: right; margin-right: 45px;" />
                <div id="assess_head" class="conditions_div" style="display: inline-flex; display: -webkit-flex; width: 100%">
                    <div style="width: 200px; font-weight: bold;" class="eleh_center">Date/Time</div>
                    <div style="width: 150px; font-weight: bold;" class="eleh_center">Assessment By</div>
                    <div style="width: 100px; font-weight: bold;" class="eleh_center"></div>
                    <div style="width: 200px; font-weight: bold;" class="eleh_center"></div>
                </div>



                <div class="conditions_head" style="margin: 3px;">
                    <% foreach (var summary in summaries.Rows)
                        {
                            var aDate = summary.Date;
                            var aBy = summary.AssesmentBy;
                            %>
                    <asp:GridView ID="gdconditions" ClientIDMode="Static" AutoGenerateColumns="false"
                        GridLines="None" HeaderStyle-BorderWidth="0" RowStyle-BorderWidth="1px" RowStyle-BorderColor="#D3D3D3" Style="display: inline-flex">
                        <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                            <itemtemplate>
                        <div style="width: 200px; margin-left: 60px;" class="ele_center">
                            <span CssClass="lbldesc"><%=aDate.ToString("MM/dd/yy H:mm:ss")%></span>

                        </div>
                    </itemtemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                            <itemtemplate>
                       <div style="width: 200px; margin-left:80px;" class="ele_center">
                            <span CssClass="lbldesc"><%=aBy%></span>

                       </div>
                    </itemtemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                            <itemtemplate>
                        <div style="width: 130px; margin-left: 5px;" class="ele_center">
                            <asp:Label CssClass="lbldesc" runat="server" Text=''></asp:Label></div>
                    </itemtemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                            <itemtemplate>
                        <div style="width: 130px; margin-left: 0px;" class="ele_center">
                           <img src="../../Images/button_edit.jpg" alt="Edit" class="edit_assess" sid="<%=summary.AssessmentId%>" style="width: 32px; height: 32px; cursor:pointer" />
                            <img src="../../Images/button_download.jpg" alt="Delete" class="dwnld_assess" sid="<%=summary.AssessmentId%>" style="width: 35px; height: 35px; cursor:pointer" /></div>
                    </itemtemplate>
                        </asp:TemplateField>
                    </asp:GridView>
                    <%   } %>
                </div>
            </div>

            <!-- Template ends here -->
              <input type="hidden" name="peerId" id="peerId" runat="server" clientidmode="Static" />
            <input type="hidden" name="userId" id="userId" runat="server"  clientidmode="Static"/>

        </form>
    </div>

    <script type="text/javascript">
        $(".edit_assess").click(function () {
            var that = this, id;
            id = $(that).attr('sid');
            parent.encounterPopup("Encounters/ASPX/EncountersMain.aspx?pm=edit&aId=" + id);
        });

        $("#start_new_encounter").click(function () {
            var peerId = $('#peerId').val();
            var userId = $('#userId').val();
            var data = { PeerId: peerId, UserId: userId };
            $.ajax({
                type: "POST",
                url: "../Services/EncounterService.svc/AddSummary",
                data: JSON.stringify(data),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d == "error") {
                        alert("Sorry an error has occured. Please contact administrator");
                    }
                    else {
                        parent.encounterPopup("Encounters/ASPX/EncountersMain.aspx?pm=new&aId=" + result.d);
                    }
                },
                error: function () {
                    alert("Could not 'Start new Encounter'. Please contact administrator");
                }
            });
        });

      
        $("#popupclose").click(function () {
            $("#patientsearch_popup").attr("src", "");
            $("#divpatientsearch").dialog('close');
        });

        var pgkey = $("#hdnpagekey").val();
        $(".patsearch_listbox").find("li").each(function () {

            var keyattr = $(this).attr("pagekey");
            if (pgkey == keyattr) {
                $(this).addClass("active_menu");
                $(this).parents("li").addClass("active_menu");
                return false;
            }
        });

        
        $("#ent_popup_close").click(function () {
            parent.popup_close();
        });

    </script>
</body>
</html>
