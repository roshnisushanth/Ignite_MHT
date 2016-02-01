<%@ Page Title="" Language="C#" MasterPageFile="~/PatientLookUp/LookUpMaster.Master" AutoEventWireup="true" CodeBehind="PatientSearch.aspx.cs" Inherits="Hick.PatientLookUp.ASPX.PatientSearch" MetaKeywords="PatientSearch" %>

<%@ Register  TagName="UCSearch" TagPrefix="PatientSearch" Src="~/PatientLookUp/UserControls/UCPatientSearch.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplPatientLookUp" runat="server">
<PatientSearch:UCSearch ID="ucsearch" runat="server" />
</asp:Content>
