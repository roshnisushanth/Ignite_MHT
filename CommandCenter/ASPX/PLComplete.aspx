<%@ Page Title="" Language="C#" MasterPageFile="~/PatientLookUp/LookUpMaster.Master" AutoEventWireup="true" CodeBehind="PLComplete.aspx.cs" Inherits="Hick.CommandCenter.ASPX.PLComplete"  MetaKeywords="Complete"%>

<%@ Register  TagName="Complete" TagPrefix="UCPatientList" Src="~/CommandCenter/UserControls/CCPatientList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplPatientLookUp" runat="server">

<UCPatientList:Complete runat="server"/>
</asp:Content>
