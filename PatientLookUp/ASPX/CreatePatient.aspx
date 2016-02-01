<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PatientLookUp/LookUpMaster.Master" CodeBehind="CreatePatient.aspx.cs" Inherits="Hick.PatientLookUp.ASPX.CreatePatient" %>
<%@ Register  TagName="Conditions" TagPrefix="UCConditions" Src="~/PatientLookUp/UserControls/UCCreatePatient.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplPatientLookUp" runat="server">
<UCConditions:Conditions ID="uccreatepatient" runat="server" />
</asp:Content>