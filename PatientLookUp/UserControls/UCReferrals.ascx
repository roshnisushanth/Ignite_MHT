<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCReferrals.ascx.cs"
    Inherits="Hick.PatientLookUp.UserControls.UCReferrals" %>
<style>
    div.patient_search_right_frame
    {
        margin: 5px 0;
    }
    .patsearch_heading.patient
    {
        width: 100%;
        padding-top: 4px;
    }
    .patsearch_heading.patient .btn_standard
    {
        margin: 0px 50px 0 0 !important;
        padding: 5px 10px;
        float: right;
    }
    .patsearch_heading.patient #popup_close
    {
        margin-top: 0 !important;
    }
</style>
 <div class="patsearch_heading patient">
        Referrals
         
        <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popup_close" class="pull-right" style="cursor: pointer;"
                alt="close" />
    </div>
<div class="patsearch_border med">
    <div class="conditions_head" style="margin: 3px;">
        <asp:GridView runat="server" ID="gdreferral" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" HeaderStyle-BorderWidth="0" RowStyle-BorderWidth="1px" RowStyle-BorderColor="#D3D3D3" OnRowCommand="gdreferral_RowCommand">
            <Columns>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div style="width: 110px; font-weight: bold; text-align: left;" class="ele_center">
                            Referral Requested Date</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="float: left; white-space: nowrap;" class="ele_center">
                            <asp:Label ID="lbldate" CssClass="lbldate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CreatedDate","{0:MM/dd/yyyy}") %>'></asp:Label></div>
                        <span class="lblpatientid" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem,"PatientID")%>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div style="width: 250px; font-weight: bold; text-align: left;" class="ele_center">
                            Referred To</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="float: left; white-space: nowrap;" class="ele_center">
                            <asp:Label ID="lblreferredto" CssClass="lblreferredto" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AssignedPhysician")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div style="width: 110px; font-weight: bold; text-align: left;" class="ele_center">
                            Ordering Physician</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="float: left; white-space: nowrap;" class="ele_center">
                            <asp:Label ID="lblordphys" CssClass="lblordphys" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ReferredBy")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div style="width: 110px; font-weight: bold; text-align: left;" class="ele_center">
                            Appointment Date/Time</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="float: left; white-space: nowrap;" class="ele_center">
                            <asp:Label ID="lblapptdate" CssClass="lblapptdate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AppDatePref1","{0:MM/dd/yyyy}")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div style="width: 110px; font-weight: bold; text-align: left;" class="ele_center">
                            Status</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="float: left; white-space: nowrap;" class="ele_center">
                            <asp:Label ID="lblstatus" CssClass="lblstatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"StatusText")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="text-align: center;" class="" style="min-width:90px;">
                            <asp:Button ID="btnview" Text="View" runat="server" class="btn_standard" CommandName="Export" style="min-width:90px;"
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem, "ReferralId")%>'></asp:Button></div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="Label1" Text="No information available" Font-Bold="true" runat="server" Style="white-space: nowrap;
                    padding: 10px;"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
    <%--  <input type="button" value="ADD NEW" name="add_conditions" class="btn_standard" id="add_conditions"
        style="float: right; margin-right: 115px;" />--%>
    <!-- Template ends here -->
    <div id="btn_groups" style="margin-left: 45px; display: none;">
        <input type="button" value="SAVE" id="btn_save" class="btn_standard" />
        <input type="button" value="CANCEL" id="btn_cancel" class="btn_standard" />
    </div>
</div>
<div style="display: none;">
    <div id="divshowconditions" style="z-index: 10000;">
        <div class="edit_conditionsdiv">
            <div style="float: left;">
                <iframe id="frmeditconditions_popup" src="" style="overflow: auto; position: fixed;
                    width: 76%; height: 346px; border: none; margin-top: 40px;"></iframe>
            </div>
            <div class="popup_header Content">
            </div>
            <div style="float: right; margin-right: 10px;">
                <img src="../../Images/popup_close.png" id="popupclose" style="cursor: pointer; margin-top: -5px;
                    margin-right: -5px;" />
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

            $("#divshowconditions").dialog('close');
        });

        

       
        $("#add_conditions").click(function () {
            $('.Content').text("Add New");

            // $('<span>Add New</span>').appendTo('.Content');

            // $('<span>Add New</span>').appendTo('.Content');

            $(".edit_conditionsdiv").css("display", "block");

            $("#frmeditconditions_popup").attr("src", "");
            $("#frmeditconditions_popup").attr("src", "EditEncounters.aspx");
            showpopup();
        });
    });

    function showpopup() {
        var popuphight = window.innerHeight - 200;
        var popupwidth = window.innerWidth - 490;

        $("#divshowconditions").dialog(
            {
                modal: true,
                height: popuphight,
                width: popupwidth,
                resizable: false,
                title: "Referral",
                create: function () {
                    $(".ui-dialog-titlebar").hide();
                    $(".ui-dialog-content").css("padding", "0px");
                }
            });
    }
    function ClosePopup() {

        $("#divshowconditions").dialog('close');

    }
    $("#popup_close").click(function () {
        parent.popup_close();
    });
</script>
