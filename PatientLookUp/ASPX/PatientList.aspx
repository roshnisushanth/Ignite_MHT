<%@ Page Title="" Language="C#" MasterPageFile="~/PatientLookUp/LookUpMaster.Master" AutoEventWireup="true" CodeBehind="PatientList.aspx.cs" Inherits="Hick.PatientLookUp.ASPX.PatientList" MetaKeywords="PatientList" %>
<%@ Register  TagName="PatientList" TagPrefix="UCPatientList" Src="~/PatientLookUp/UserControls/UCPatientList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplPatientLookUp" runat="server">
<UCPatientList:PatientList ID="ucpatientlist" runat="server"></UCPatientList:PatientList>
</asp:Content>
