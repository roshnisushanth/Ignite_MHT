<%@ Page Title="" Language="C#" MasterPageFile="~/Encounters/EncounterMaster.Master" AutoEventWireup="true" CodeBehind="AssessmentSummary.aspx.cs" Inherits="Hick.Encounters.aspx.AssessmentSummary" MetaKeywords="AssessmentSummary" %>

<%@ Register  TagName="AssessmentSummary" TagPrefix="EnAssessmentSummary" Src="~/Encounters/UserControls/AssessmentSummary.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cplEncounters" runat="server">
    <EnAssessmentSummary:AssessmentSummary ID="assessment_summary" runat="server" />
</asp:Content>
