<%@ Page Title="" Language="C#" MasterPageFile="~/Encounters/EncounterMaster.Master" AutoEventWireup="true" CodeBehind="BreastFeedingMother.aspx.cs" Inherits="Hick.Encounters.aspx.BreastFeedingMother" MetaKeywords="BreastFeedingMother"%>

<%@ Register  TagName="BreastFeedingMother" TagPrefix="EnBreastFeedingMother" Src="~/Encounters/UserControls/BreastFeedingMother.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplEncounters" runat="server">
         <EnBreastFeedingMother:BreastFeedingMother ID="breastFeedingMother" runat="server" />
</asp:Content>
