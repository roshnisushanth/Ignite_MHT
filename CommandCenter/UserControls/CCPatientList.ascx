<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CCPatientList.ascx.cs"
    Inherits="Hick.CommandCenter.UserControls.CCPatientList" %>
<div class="patsearch_heading">
    <asp:Label ID="mylabel" runat="server"></asp:Label>
    <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popup_close" class="pull-right"
        style="cursor: pointer; margin-top: -5px;" alt="close" />
</div>
<div class="patsearch_border">
    <div style="margin: 0px;">
        <table width="100%" id="pnlGridView" runat="server">
            <tr>
                <td>
                    <asp:GridView runat="server" ID="gvccpatientlist" ClientIDMode="Static" AutoGenerateColumns="false"
                        GridLines="None" HeaderStyle-BorderWidth="0" RowStyle-BorderWidth="0px" RowStyle-BorderColor="#D3D3D3"
                        CssClass="gridstyle">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                                <HeaderTemplate>
                                    Last Name</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label CssClass="lbllastname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Lastname")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                                <HeaderTemplate>
                                    First Name</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label CssClass="lblfirstname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Firstname")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                                <HeaderTemplate>
                                    Date of Birth</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label CssClass="lbldob" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"DateOfBirth")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                                <HeaderTemplate>
                                    Phone Number</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label CssClass="lblphonee" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PhoneNumber")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                                <HeaderTemplate>
                                    Physician</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label CssClass="lblssn" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Physician")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                                <HeaderTemplate>
                                    Total Time</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label CssClass="lblriskprofile" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TotalChatDuration")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="Label1" Text="No records found" Font-Bold="true" runat="server"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <div style="padding-top: 12px; padding-right: 1px; float: left; margin-left: 10px;
        font-weight: 600;">
        <asp:Button ID="btnprintpatientlist" runat="server" Text="Print" OnClientClick="javascript:return PrintPage();"
            BackColor="#FFCB05" Width="100px" Height="30" BorderWidth="0" />
    </div>
    <div style="padding-top: 12px; padding-right: 1px; float: left; margin-left: 10px;
        font-weight: 600;">
        <asp:Button ID="btnexportpatientlist" runat="server" Text="Export" OnClick="btnexportpatientlist_Click"
            BackColor="#FFCB05" Width="100px" Height="30" BorderWidth="0" />
    </div>
     <div style="padding-top: 12px; padding-right: 1px; float: left; margin-left: 10px;
        font-weight: 600;">
        <asp:Button ID="btndownloadpdf" runat="server" Text="Download" OnClick="btndownloadpdf_Click"
            BackColor="#FFCB05" Width="100px" Height="30" BorderWidth="0" />
    </div>
    <%-- <div style="margin:3px;">
        <div style="display:inline-flex;display:-webkit-flex;width:100%;">
                <div style="width: 130px;font-weight:bold;" class="ele_begin">Last Name</div>
                <div style="width: 130px;font-weight:bold;" class="ele_begin">First Name</div>
                <div style="width: 130px;font-weight:bold;" class="ele_begin">Date of Birth</div>
                <div style="width: 145px;font-weight:bold;" class="ele_begin">Phone Number</div>
                <div style="width: 100px;font-weight:bold;" class="ele_begin">Physician</div>
                <div style="width: 80px;font-weight:bold;text-align:center;" class="ele_begin">Total Time</div>
        </div>
    </div>
    <div class="conditions_head" style="margin:3px;">       
        <div class="conditions_div" style="display:inline-flex;display:-webkit-flex;width:100%">
            <div style="width: 130px;" class="ele_begin">Smith</div>
            <div style="width: 130px;" class="ele_begin">Thomas</div>
            <div style="width: 130px" class="ele_begin">04/17/1993</div>
            <div style="width: 145px" class="ele_begin">(407)555-1234</div>
            <div style="width: 100px" class="ele_begin">Dr.Rettig</div>
            <div style="width: 80px;text-align:center;" class="ele_begin">14m</div>
        </div>
        <div class="conditions_div" style="display:inline-flex;display:-webkit-flex;width:100%">
            <div style="width: 130px;" class="ele_begin">Smith</div>
            <div style="width: 130px;" class="ele_begin">Thomas</div>
            <div style="width: 130px" class="ele_begin">04/17/1993</div>
            <div style="width: 145px" class="ele_begin">(407)555-1234</div>
            <div style="width: 100px" class="ele_begin">Dr.Rettig</div>
            <div style="width: 80px;text-align:center;" class="ele_begin">14m</div>
        </div>
    </div>--%>
</div>
<script type="text/javascript">
    $("#div_command").css("display", "block");
    $("#command_leftpart").css("display", "block");

    $("#commandcenter").css("display", "block");


    $("#popup_close").click(function () {
        parent.popup_close();
    });
</script>
<script language="javascript" type="text/javascript">

    function PrintPage() {

        var printContent = document.getElementById('<%= pnlGridView.ClientID %>');

        var printWindow = window.open("All Records", "Print Panel", 'left=50000,top=50000,width=0,height=0');

        printWindow.document.write(printContent.innerHTML);

        printWindow.document.close();

        printWindow.focus();

        printWindow.print();

    }

</script>
