<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCPatientList.ascx.cs"
    Inherits="Hick.PatientLookUp.UserControls.UCPatientList" %>
<div class="patsearch_heading">
    Patient Search
    <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popup_close" class="pull-right"
        style="cursor: pointer; margin-top: -5px;" alt="close" />
</div>
<div class="patsearch_border">
    <div style="padding: 8px;">
        <asp:GridView runat="server" ID="patientList" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" HeaderStyle-BorderWidth="0" RowStyle-BorderWidth="1px" RowStyle-BorderColor="#D3D3D3">
            <Columns>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        Last Name</HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbllastname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"LastName")%>'></asp:Label>
                        <span class="lblpatientid" style="display: none;">
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
                <asp:Label Text="No records found" Font-Bold="true" runat="server"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
        <%--<table id="patientList" cellpadding="10">
            <!-- patient list table heading -->
            <tr>
                <td class="bold_font table_data_list">
                    Last Name
                </td>
                <td class="bold_font table_data_list">
                    First Name
                </td>
                <td class="bold_font table_data_list">
                    Date of Birth
                </td>
                <td class="bold_font table_data_list">
                    Phone Number
                </td>
                <td class="bold_font table_data_list">
                    Last 4 of SSN
                </td>
                <td class="bold_font table_data_list">
                    Risk Profile
                </td>
            </tr>
            <!-- patient lists template starts here-->
            <tr style="border: 1px solid #D3D3D3;">
                <td class="table_data_list">
                    Smith
                </td>
                <td class="table_data_list">
                    Thomas
                </td>
                <td class="table_data_list">
                    04/17/1976
                </td>
                <td class="table_data_list">
                    {407}4444-6678
                </td>
                <td class="table_data_list">
                    1234
                </td>
                <td class="table_data_list">
                    low
                </td>
            </tr>
            <tr style="border: 1px solid #D3D3D3;">
                <td class="table_data_list">
                    Smith
                </td>
                <td class="table_data_list">
                    Thomas
                </td>
                <td class="table_data_list">
                    04/17/1976
                </td>
                <td class="table_data_list">
                    {407}4444-6678
                </td>
                <td class="table_data_list">
                    1234
                </td>
                <td class="table_data_list">
                    low
                </td>
            </tr>
            <!-- patient lists template ends here-->
        </table>--%>
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
