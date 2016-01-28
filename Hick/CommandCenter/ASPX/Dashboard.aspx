<%@ Page Title="" Language="C#" MasterPageFile="~/PatientLookUp/LookUpMaster.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Hick.CommandCenter.ASPX.Dashboard" MetaKeywords="dashboard"%>

<%@ Register  TagName="Dashboard" TagPrefix="UCDashboard" Src="~/CommandCenter/UserControls/Dashboard.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplPatientLookUp" runat="server">
<UCDashboard:Dashboard ID="dashboard" runat="server" />

</asp:Content>
