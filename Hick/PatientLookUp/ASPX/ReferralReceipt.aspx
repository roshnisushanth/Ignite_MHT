<%@ Page Title="" Language="C#" MasterPageFile="~/PatientLookUp/LookUpMaster.Master" AutoEventWireup="true" CodeBehind="ReferralReceipt.aspx.cs" Inherits="Hick.PatientLookUp.ASPX.ReferralReceipt" %>
<%@ Register  TagName="ReferralReceipt" TagPrefix="UCEncounters" Src="~/PatientLookUp/UserControls/UCReferralReceipt.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplPatientLookUp" runat="server">
<UCEncounters:ReferralReceipt ID="ucencounters" runat="server" />
</asp:Content>
