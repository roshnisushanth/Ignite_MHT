<%@ Page Title="" Language="C#" MasterPageFile="~/PatientLookUp/LookUpMaster.Master" AutoEventWireup="true" CodeBehind="Immunizations.aspx.cs" Inherits="Hick.PatientLookUp.ASPX.Immunizations" MetaKeywords="immunizations"%>
<%@ Register  TagName="Immunizations" TagPrefix="UCImmunizations" Src="~/PatientLookUp/UserControls/UCImmunizations.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplPatientLookUp" runat="server">
<UCImmunizations:Immunizations ID="ucimmunizations" runat="server" />
</asp:Content>
