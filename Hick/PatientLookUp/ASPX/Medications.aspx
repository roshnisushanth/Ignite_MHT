<%@ Page Title="" Language="C#" MasterPageFile="~/PatientLookUp/LookUpMaster.Master" AutoEventWireup="true" CodeBehind="Medications.aspx.cs" Inherits="Hick.PatientLookUp.ASPX.Medications" MetaKeywords="Medications"%>
<%@ Register  TagName="Medications" TagPrefix="UCMedications" Src="~/PatientLookUp/UserControls/UCMedications.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplPatientLookUp" runat="server">
<UCMedications:Medications ID="ucmedications" runat="server" />
</asp:Content>
