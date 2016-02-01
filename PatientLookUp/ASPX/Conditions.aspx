<%@ Page Title="" Language="C#" MasterPageFile="~/PatientLookUp/LookUpMaster.Master" AutoEventWireup="true" CodeBehind="Conditions.aspx.cs" Inherits="Hick.PatientLookUp.ASPX.Conditions" MetaKeywords="Conditions"%>
<%@ Register  TagName="Conditions" TagPrefix="UCConditions" Src="~/PatientLookUp/UserControls/UCConditions.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplPatientLookUp" runat="server">
<UCConditions:Conditions ID="ucconditions" runat="server" />
</asp:Content>
