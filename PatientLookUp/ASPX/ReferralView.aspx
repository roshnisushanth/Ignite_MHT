<%@ Page Title="" Language="C#" MasterPageFile="~/PatientLookUp/LookUpMaster.Master" AutoEventWireup="true" CodeBehind="ReferralView.aspx.cs" MetaKeywords="referrals"Inherits="Hick.PatientLookUp.ASPX.ReferralView" %>
<%@ Register  TagName="Encounters" TagPrefix="UCEncounters" Src="~/PatientLookUp/UserControls/UCReferrals.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplPatientLookUp" runat="server">
<UCEncounters:Encounters ID="ucencounters" runat="server" />
</asp:Content>
