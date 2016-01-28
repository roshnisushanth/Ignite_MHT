<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestAndProcedures.aspx.cs" MasterPageFile="~/PatientLookUp/LookUpMaster.Master" Inherits="Hick.PatientLookUp.ASPX.TestAndProcedures" MetaKeywords="Testandprocedure" %>
<%@ Register  TagName="testandprocedures" TagPrefix="UCtestandprocedures" Src="~/PatientLookUp/UserControls/UCTestAndProcedures.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplPatientLookUp" runat="server">
<UCtestandprocedures:testandprocedures ID="uctestandprocedures" runat="server" />
</asp:Content>