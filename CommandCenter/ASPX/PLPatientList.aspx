<%@ Page Title="" Language="C#" MasterPageFile="~/PatientLookUp/LookUpMaster.Master" AutoEventWireup="true" CodeBehind="PLPatientList.aspx.cs" Inherits="Hick.CommandCenter.ASPX.PLPatientList" MetaKeywords="CCPatientList"%>
<%@ Register  TagName="PatientList" TagPrefix="UCPatientList" Src="~/CommandCenter/UserControls/CCPatientList.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplPatientLookUp" runat="server">
<UCPatientList:PatientList ID="NotComplete1" runat="server" />
</asp:Content>
