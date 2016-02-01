<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PatientLookUp/LookUpMaster.Master" CodeBehind="PhpSummary.aspx.cs" Inherits="Hick.PatientLookUp.ASPX.PhpSummary"  MetaKeywords="php"%>

<%@ Register  TagName="PHPsummary" TagPrefix="PhpSummary" Src="~/PatientLookUp/UserControls/UCPhpSummary.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplPatientLookUp" runat="server">
    <PhpSummary:PHPsummary ID="phpsummary" runat="server" />
</asp:Content>



