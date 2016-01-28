<%@ Page Title="" Language="C#" MasterPageFile="~/PatientLookUp/LookUpMaster.Master" AutoEventWireup="true" CodeBehind="BillingReport.aspx.cs" Inherits="Hick.CommandCenter.ASPX.BillingReport" MetaKeywords="dr_retting_billing"%>

<%@ Register  TagName="BillingReport" TagPrefix="UCBillingReport" Src="~/CommandCenter/UserControls/BillingReport.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplPatientLookUp" runat="server">
    <UCBillingReport:BillingReport ID="billing_report" runat="server" />
</asp:Content>
