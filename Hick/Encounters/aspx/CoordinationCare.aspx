<%@ Page Title="" Language="C#" MasterPageFile="~/Encounters/EncounterMaster.Master" AutoEventWireup="true" CodeBehind="CoordinationCare.aspx.cs" Inherits="Hick.Encounters.aspx.CoordinationCare" MetaKeywords="CoordinationCare"%>

<%@ Register  TagName="CoordinationOfCare" TagPrefix="EnCoordinationCare" Src="~/Encounters/UserControls/CoordinationCare.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplEncounters" runat="server">
    <EnCoordinationCare:CoordinationOfCare ID="coordinationOfCare" runat="server" />
</asp:Content>
