<%@ Page Title="" Language="C#" MasterPageFile="~/Encounters/EncounterMaster.Master" AutoEventWireup="true" CodeBehind="BreastFeedingObserv.aspx.cs" Inherits="Hick.Encounters.aspx.BreastFeedingObserv" MetaKeywords="BreastFeedingObserv"%>

<%@ Register  TagName="BreastFeedingObserv" TagPrefix="EnBreastFeedingObserv" Src="~/Encounters/UserControls/BreastFeedingObserv.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplEncounters" runat="server">
    <EnBreastFeedingObserv:BreastFeedingObserv ID="breastFeedingObv" runat="server" />
</asp:Content>
