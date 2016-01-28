<%@ Page Title="" Language="C#" MasterPageFile="~/Encounters/EncounterMaster.Master" AutoEventWireup="true" CodeBehind="Medications.aspx.cs" Inherits="Hick.Encounters.aspx.Medications" MetaKeywords="Medications parent"%>

<%@ Register  TagName="Medications" TagPrefix="EnMedications" Src="~/Encounters/UserControls/Medications.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplEncounters" runat="server">
    <EnMedications:Medications ID="medications" runat="server" />
     <asp:HiddenField ID="hdnPatientId" runat="server" />
    <asp:HiddenField ID="hdnUserId" runat="server" />
</asp:Content>
