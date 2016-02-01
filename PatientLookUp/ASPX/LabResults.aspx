<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LabResults.aspx.cs"  MasterPageFile="~/PatientLookUp/LookUpMaster.Master" Inherits="Hick.PatientLookUp.ASPX.LabResults" MetaKeywords="labresult" %>

<%@ Register  TagName="labresults" TagPrefix="UClabresults" Src="~/PatientLookUp/UserControls/UCLabResults.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplPatientLookUp" runat="server">
<UClabresults:labresults ID="uctestandprocedures" runat="server" />
</asp:Content>
 