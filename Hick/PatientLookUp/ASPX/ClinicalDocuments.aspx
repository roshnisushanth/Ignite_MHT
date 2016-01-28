<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClinicalDocuments.aspx.cs" MasterPageFile="~/PatientLookUp/LookUpMaster.Master" Inherits="Hick.PatientLookUp.ASPX.ClinicalDocuments" MetaKeywords="clinicaldocument" %>
<%@ Register  TagName="ClinicalDocument" TagPrefix="clinicaldocument" Src="~/PatientLookUp/UserControls/UCClinicalDocuments.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplPatientLookUp" runat="server">
    <clinicaldocument:ClinicalDocument ID="ClinicalDocument" runat="server" />
</asp:Content>

