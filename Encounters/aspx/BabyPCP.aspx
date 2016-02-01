<%@ Page Title="" Language="C#" MasterPageFile="~/Encounters/EncounterMaster.Master" AutoEventWireup="true" 
    CodeBehind="BabyPCP.aspx.cs" Inherits="Hick.Encounters.aspx.BabyPCP" MetaKeywords="BabyPCP parent" %>

<%@ Register  TagName="BabyPCP" TagPrefix="EncounterBabyPCP" Src="~/Encounters/UserControls/BabyPCP.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplEncounters" runat="server">
    <EncounterBabyPCP:BabyPCP ID="babypcp" runat="server" />
</asp:Content>
