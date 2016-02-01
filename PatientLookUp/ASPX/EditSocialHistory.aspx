<%@ Page Title="" Language="C#" MasterPageFile="~/PatientLookUp/LookUpMaster.Master" AutoEventWireup="true" CodeBehind="EditSocialHistory.aspx.cs" Inherits="Hick.PatientLookUp.ASPX.EditSocialHistory"  MetaKeywords="problems"%>
<%@ Register  TagName="Conditions" TagPrefix="UCConditions" Src="~/PatientLookUp/UserControls/UCAddEditSocialHistory.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplPatientLookUp" runat="server">
<UCConditions:Conditions ID="ucconditions" runat="server" />
</asp:Content>
