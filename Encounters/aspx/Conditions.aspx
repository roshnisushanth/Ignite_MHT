<%@ Page Title="" Language="C#" MasterPageFile="~/Encounters/EncounterMaster.Master" AutoEventWireup="true" CodeBehind="Conditions.aspx.cs" Inherits="Hick.Encounters.aspx.Conditions"  MetaKeywords="Conditions parent"%>
 
<%@ Register  TagName="Conditions" TagPrefix="EnConditions" Src="~/Encounters/UserControls/Conditions.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplEncounters" runat="server">
    <EnConditions:Conditions ID="ucmomob" runat="server" />
</asp:Content>
