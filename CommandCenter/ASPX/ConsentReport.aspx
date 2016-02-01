<%@ Page Title="" Language="C#" MasterPageFile="~/PatientLookUp/LookUpMaster.Master" AutoEventWireup="true" CodeBehind="ConsentReport.aspx.cs" Inherits="Hick.CommandCenter.ASPX.ConsentReport" MetaKeywords="dr_retting_consent" %>

<%@ Register  TagName="ConsentReport" TagPrefix="UCConsentReport" Src="~/CommandCenter/UserControls/ConsentReport.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplPatientLookUp" runat="server">
    <UCConsentReport:ConsentReport ID="billing_report" runat="server" />
</asp:Content>
