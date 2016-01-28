<%@ Page Title="" Language="C#" MasterPageFile="~/Encounters/EncounterMaster.Master" AutoEventWireup="true" CodeBehind="HistoryIllness.aspx.cs" Inherits="Hick.Encounters.aspx.HistoryIllness" MetaKeywords="HistoryIllness"%>

<%@ Register  TagName="HistoryIllness" TagPrefix="EnHistoryIllness" Src="~/Encounters/UserControls/HistoryIllness.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplEncounters" runat="server">
    <EnHistoryIllness:HistoryIllness ID="historyIll" runat="server" />
</asp:Content>
