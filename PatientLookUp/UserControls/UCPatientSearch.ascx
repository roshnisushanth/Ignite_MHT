<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCPatientSearch.ascx.cs"
    Inherits="Hick.PatientLookUp.UserControls.UCPatientSearch" %>

<style type="text/css">
 #grid-view-container
 {
  height:auto;
  overflow:auto;
  max-height:220px;
 }
</style>

<!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.1/js/bootstrap.min.js"></script>
    <script src="https://rawgit.com/wenzhixin/bootstrap-table/master/src/bootstrap-table.js"></script>
<link
	href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css"
	rel="stylesheet">

<div class="patsearch_heading">
    Patient Search
    <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popup_close" class="pull-right"
        style="cursor: pointer; margin-top: -5px;" alt="close" />
</div>
<div>
    <div class="patsearch_contents" style="margin-left: 30px; margin-top: 20px;">
        <div style="width: 90%; margin-left: 12px; height: 20px;">
            <asp:Label ID="lblmessage" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label></div>
        <div class="patsearch_formcontrol">
            <div class="standard_label">
                Last Name</div>
            <%-- <input type="text" id="lastname" class="standard_textbox" />--%>
            <asp:TextBox ID="lastname" runat="server" CssClass="standard_textbox" ClientIDMode="Static"></asp:TextBox>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label">
                First Name</div>
            <%--   <input type="text" id="firstname" class="standard_textbox" />--%>
            <asp:TextBox ID="firstname" CssClass="standard_textbox" runat="server" ClientIDMode="Static"></asp:TextBox>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label">
                Date of Birth</div>
            <%--  <input type="text" id="dob" class="standard_textbox" />      --%>
            <asp:TextBox ID="dob" CssClass="standard_textbox" runat="server" ClientIDMode="Static"></asp:TextBox>
            <img src="../../Images/calendar.jpg" id="calendar_img" class="txtbox_spanimg" alt="calendar" />
        </div>
        <div id="btn_groups">
            <div class="standard_label" style="margin-right: -10px;">
            </div>
            <%--   <input type="button" value="LOOKUP" id="btn_lookup" class="btn_standard" runat="server" onclick="btn_lookup_Click" />--%>
            <div id="divbtns">
                <asp:Button Text="Lookup" ID="btn_lookup" CssClass="btn_standard" runat="server"
                    OnClick="btn_lookup_Click" OnClientClick="showProgress()" />
                <asp:Button Text="Create New Patient" ID="btn_createpatient" CssClass="btn_standard"
                    runat="server" OnClick="btn_createpatient_Click" Visible="false"/>
            </div>
            <span id="imgsubmitprg" style="display: none; padding-left: 10px;">&nbsp;Processing
                please wait....<br />
                <img src="../../Images/fileupload-loader.gif" alt="" /></span>
        </div>
    </div>
    <div id="div_patlist" class="patsearch_heading" runat="server" style="display:none;">
        Patient List</div>
    <div style="padding: 0px;">
        <table width="100%" id="pnlGridView" runat="server">
            <tr>
                <td>
                    <div id="grid-view-container">
        <asp:GridView runat="server" ID="patientList" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" HeaderStyle-BorderWidth="0" RowStyle-BorderWidth="1px" RowStyle-BorderColor="#D3D3D3" style="width:100%;">
                   <Columns>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        Last Name</HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbllastname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LastName")%>'></asp:Label>
                        <span class="lblpatientid" style="display: none;" >
                           <%#DataBinder.Eval(Container.DataItem,"PatientId")%>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        First Name</HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblfirstname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FirstName")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        Date of Birth</HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbldob" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"DOB")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        Phone Number</HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblphonee" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Phone")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        Last 4 of SSN</HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblssn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SSN")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        Risk Profile</HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblriskprofile" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RiskProfile")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="Label1" Text="No records found" Font-Bold="true" runat="server"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
                          </div>
                    </td>
                </tr>
            </table>
    </div>

   

    

</div>




<div class="modal fade bs-example-modal-sm" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel">
  <div class="modal-dialog modal-sm">
    <div class="modal-content">
      ...
    </div>
  </div>
</div>
<script type="text/javascript">
    $(document).ready(function () {

        $("#patientList tr:gt(0)").click(function () {
            var pid = $(this).find("span[class~=lblpatientid]").html().trim();
            if (pid) {
                window.location.href = "PhpSummary.aspx?ptid=" + pid;
            }
            else {
                alert('Sorry an error has occured. Please contact administrator');
            }

        });

    });

    $("#div_patientsearch").css("display", "block");

    $("#patientsearch_leftpart").css("display", "block");
    $("#imgsearchuser").css("display", "block");

    $("#popup_close").click(function () {
        parent.popup_close();
    });
</script>
<script type="text/javascript">
    $('#dob').datepicker();
    $('#calendar_img').click(function () {
        $('#dob').datepicker('show');
    });
    function showProgress() {
            $("#divbtns").hide();
            $("#imgsubmitprg").show();
    }

    $("#div_patientsearch").css("display", "block");

    $("#patientsearch_leftpart").css("display", "block");
    $("#imgsearchuser").css("display", "block");

    $("#popup_close").click(function () {
        parent.popup_close();
    });
       
  
</script>
