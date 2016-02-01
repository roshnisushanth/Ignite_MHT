<%@ Page Title="" Language="C#" MasterPageFile="~/Encounters/EncounterMaster.Master" AutoEventWireup="true" CodeBehind="ChiefComplaints.aspx.cs" Inherits="Hick.Encounters.aspx.ChiefComplaints" MetaKeywords="ChiefComplaints"%>

<%@ Register  TagName="ChiefComplaints" TagPrefix="EnChiefComplaints" Src="~/Encounters/UserControls/ChiefComplaints.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplEncounters" runat="server">
    <EnChiefComplaints:ChiefComplaints ID="complaints" runat="server" />
</asp:Content>
