
<%@ Page Title="" Language="C#" MasterPageFile="~/Encounters/EncounterMaster.Master" AutoEventWireup="true" 
    CodeBehind="EncountersMain.aspx.cs" Inherits="Hick.Encounters.aspx.EncountersMain" MetaKeywords="main" %>

<%@ Register  TagName="UCMomOB" TagPrefix="Encounters" Src="~/Encounters/UserControls/MomsOB.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplEncounters" runat="server">
<Encounters:UCMomOB ID="ucmomob" runat="server" />
</asp:Content>
