<%@ Page Title="" Language="C#" MasterPageFile="~/Encounters/EncounterMaster.Master" AutoEventWireup="true" CodeBehind="Assessmnt.aspx.cs" Inherits="Hick.Encounters.aspx.Assessmnt" MetaKeywords="Assessmnt" %>

<%@ Register  TagName="Assessment" TagPrefix="EnAssessment" Src="~/Encounters/UserControls/Assessmnt.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplEncounters" runat="server">
<EnAssessment:Assessment ID="assessment" runat="server" />
</asp:Content>
