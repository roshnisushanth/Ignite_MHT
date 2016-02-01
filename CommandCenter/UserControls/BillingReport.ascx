<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BillingReport.ascx.cs" Inherits="Hick.CommandCenter.UserControls.BillingReport" %>
<style>
    div.patient_search_right_frame {
        margin: 5px 0;
    }

    .patsearch_heading.patients {
        width: 100%;
        padding-top: 4px;
    }

        .patsearch_heading.patients .btn_standard {
            margin: 0px 50px 0 0 !important;
            padding: 5px 10px;
            float: right;
        }

        .patsearch_heading.patients #popup_close {
            margin-right: -164px;
            margin-top: 0 !important;
        }

    #gdconditions th.table_data_list {
        padding: 5px 0;
    }
</style>

<div class="patsearch_heading patients">
    Billing Report - <%=currentMonth %> <%=currentYear %>
    <%-- <input type="button" value="Export" name="export_billing" class="btn_standard " id="export_billing"
            style="margin-top: -28px; margin-left: 74%" />--%>
    <asp:Button ID="btnexportpatientlist" runat="server" Text="Export" OnClick="btnexportpatientlist_Click" CssClass="btn_standard"
        BackColor="#FFCB05" Width="100px" Height="30" Style="margin-top: -28px; margin-left: 76%" BorderWidth="0" />
    <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popup_close" class="pull-right" style="cursor: pointer; margin-top: -42px;"
        alt="close" />
</div>
<div class="patsearch_border med">
    <%-- <div id="assess_head" class="conditions_div" style="display: inline-flex; display: -webkit-flex; width: 100%">
        <div style="width: 150px; font-weight: bold; margin: 15px 0;" class="text-center">Patient Name</div>
        <div style="width: 100px; font-weight: bold; margin: 15px 0;" class="text-center">DOB</div>
        <div style="width: 100px; font-weight: bold; margin: 15px 0;" class="text-center">HICN</div>
        <div style="width: 100px; font-weight: bold; margin: 15px 0;" class="text-center">MRN#</div>
        <div style="width: 100px; font-weight: bold; margin: 15px 0;" class="text-center">Date Span</div>
        <div style="width: 100px; font-weight: bold; margin: 15px 0;" class="text-center">CPT Code</div>
        <div style="width: 100px; font-weight: bold; margin: 15px 0;" class="text-center">ICD Codes</div>
        <div style="width: 100px; font-weight: bold; margin: 15px 0;" class="text-center">POS</div>
    </div>--%>
    <div class="conditions_head" style="margin: 3px 0;">
        <asp:GridView runat="server" ID="gdconditions" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" HeaderStyle-BorderWidth="0" Style="">
            <Columns>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>Patient Name</HeaderTemplate>
                    <ItemTemplate>
                        <div style="width: 150px; margin: 15px 0;" class="text-center ">
                            <asp:Label CssClass="lbldesc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FirstName") +" "+DataBinder.Eval(Container.DataItem,"LastName")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>DOB</HeaderTemplate>
                    <ItemTemplate>
                        <div style="width: 100px; margin: 15px 0;" class="text-center">
                            <asp:Label CssClass="lbldesc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"DOB")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>HICN</HeaderTemplate>
                    <ItemTemplate>
                        <div style="width: 100px; margin: 15px 0;" class="text-center">
                            <asp:Label CssClass="lbldesc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"HICN")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>Date Span</HeaderTemplate>
                    <ItemTemplate>
                        <div style="width: 100px; margin: 15px 0;" class="text-center">
                            <asp:Label CssClass="lbldesc" runat="server" Text='<%#startDate +" - "+endDate%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>CPT Code</HeaderTemplate>
                    <ItemTemplate>
                        <div style="width: 100px; margin: 15px 0;" class="text-center">
                            <asp:Label CssClass="lbldesc" runat="server" Text='99490'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>ICD10 Codes</HeaderTemplate>
                    <ItemTemplate>
                        <div style="width: 200px; overflow:auto; margin: 15px 0;" class="text-center">
                            <asp:Label CssClass="lbldesc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"icdcode")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>POS</HeaderTemplate>
                    <ItemTemplate>
                        <div style="width: 100px; margin: 15px 0;" class="text-center">
                            <asp:Label CssClass="lbldesc" runat="server" Text='11'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="Label1" Text="No records found" Font-Bold="true" runat="server"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
</div>

<script type="text/javascript">
    $("#div_command").css("display", "block");
    $("#command_leftpart").css("display", "block");

    $("#commandcenter").css("display", "block");


    $("#popup_close").click(function () {
        parent.popup_close();
    });
</script>
