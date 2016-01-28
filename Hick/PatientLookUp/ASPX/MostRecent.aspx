<%@ Page Title="" Language="C#" MasterPageFile="~/PatientLookUp/LookUpMaster.Master" AutoEventWireup="true" CodeBehind="MostRecent.aspx.cs" Inherits="Hick.PatientLookUp.ASPX.MostRecent" MetaKeywords="RecentVisit"%>
<%@ Register  TagName="MostRecent" TagPrefix="UCMostRecent" Src="~/PatientLookUp/UserControls/UCMostRecent.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplPatientLookUp" runat="server">
<UCMostRecent:MostRecent ID="ucmostrecent" runat="server" />
</asp:Content>
