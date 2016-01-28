<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PatientLookUp/LookUpMaster.Master" CodeBehind="PatientPhpSummary.aspx.cs" Inherits="Hick.PatientLookUp.ASPX.PatientPhpSummary" MetaKeywords="php" %>

<%@ Register  TagName="PatientPHPsummary" TagPrefix="PhpSummary" Src="~/PatientLookUp/UserControls/UCPatientPhpSummary.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplPatientLookUp" runat="server">
    <PhpSummary:PatientPHPsummary ID="Patientphpsummery" runat="server" />
</asp:Content>
