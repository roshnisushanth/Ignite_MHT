<%@ Page Title="" Language="C#" MasterPageFile="~/Encounters/EncounterMaster.Master" AutoEventWireup="true" CodeBehind="MomsOB.aspx.cs" Inherits="Hick.Encounters.MomsOB" MetaKeywords="EncountersMain parent"%>

<%@ Register  TagName="UCMomOB" TagPrefix="EnMomsOB" Src="~/Encounters/UserControls/MomsOB.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplEncounters" runat="server">
    <EnMomsOB:UCMomOB ID="ucmomob" runat="server" />
</asp:Content>
