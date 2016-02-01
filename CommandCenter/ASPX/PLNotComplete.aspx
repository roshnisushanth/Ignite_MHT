<%@ Page Title="" Language="C#" MasterPageFile="~/PatientLookUp/LookUpMaster.Master" AutoEventWireup="true" CodeBehind="PLNotComplete.aspx.cs" Inherits="Hick.CommandCenter.ASPX.PLNotComplete" MetaKeywords="NotComplete"%>
<%@ Register  TagName="NotComplete" TagPrefix="UCPatientList" Src="~/CommandCenter/UserControls/CCPatientList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplPatientLookUp" runat="server">
<UCPatientList:NotComplete runat="server" />
</asp:Content>
