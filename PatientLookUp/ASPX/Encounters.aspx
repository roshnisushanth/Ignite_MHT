<%@ Page Title="" Language="C#" MasterPageFile="~/PatientLookUp/LookUpMaster.Master" AutoEventWireup="true" CodeBehind="Encounters.aspx.cs" Inherits="Hick.PatientLookUp.ASPX.Encounters"  MetaKeywords="encounters"%>
<%@ Register  TagName="Encounters" TagPrefix="UCEncounters" Src="~/PatientLookUp/UserControls/UCEncounters.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplPatientLookUp" runat="server">
<UCEncounters:Encounters ID="ucencounters" runat="server" />
</asp:Content>
