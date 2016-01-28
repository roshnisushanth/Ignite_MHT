<%@ Page Title="" Language="C#" MasterPageFile="~/Encounters/EncounterMaster.Master" AutoEventWireup="true" CodeBehind="Plan.aspx.cs" Inherits="Hick.Encounters.aspx.Plan" MetaKeywords="Plan" %>

<%@ Register  TagName="Plan" TagPrefix="EnPlan" Src="~/Encounters/UserControls/Plan.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplEncounters" runat="server">
    <EnPlan:Plan ID="plan" runat="server" />
</asp:Content>
