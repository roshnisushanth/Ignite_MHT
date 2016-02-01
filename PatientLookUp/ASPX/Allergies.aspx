<%@ Page Title="" Language="C#" MasterPageFile="~/PatientLookUp/LookUpMaster.Master" AutoEventWireup="true" CodeBehind="Allergies.aspx.cs" Inherits="Hick.PatientLookUp.ASPX.Allergies" MetaKeywords="Allergies"%>
<%@ Register  TagName="Allergies" TagPrefix="UCAllergies" Src="~/PatientLookUp/UserControls/UCAllergies.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplPatientLookUp" runat="server">
<UCAllergies:Allergies ID="ucallergies" runat="server" />
</asp:Content>
