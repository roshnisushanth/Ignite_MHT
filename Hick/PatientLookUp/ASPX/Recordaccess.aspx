<%@ Page  Language="C#" AutoEventWireup="true" CodeBehind="Recordaccess.aspx.cs" Inherits="Hick.PatientLookUp.ASPX.Recordaccess" MasterPageFile="~/PatientLookUp/LookUpMaster.Master"  MetaKeywords="Recordaccess"  %>

<%@ Register  TagName="Conditions" TagPrefix="UCConditions" Src="~/PatientLookUp/UserControls/UCRecordaccess.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplPatientLookUp" runat="server">
<UCConditions:Conditions ID="ucRecordaccess1" runat="server" />
</asp:Content>
