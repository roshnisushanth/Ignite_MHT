<%@ Page Title="" Language="C#" MasterPageFile="~/Encounters/EncounterMaster.Master" AutoEventWireup="true" CodeBehind="BreastFeedingBaby.aspx.cs" Inherits="Hick.Encounters.aspx.BreastFeedingBaby" MetaKeywords="BreastFeedingBaby"%>

<%@ Register  TagName="BreastFeedingBaby" TagPrefix="EnBreastFeedingBaby" Src="~/Encounters/UserControls/BreastFeedingBaby.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplEncounters" runat="server">
    <EnBreastFeedingBaby:BreastFeedingBaby ID="breastFeedingBby" runat="server" />
</asp:Content>
