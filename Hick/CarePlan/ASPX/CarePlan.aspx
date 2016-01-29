<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CarePlan/CarePlanMaster.Master" CodeBehind="CarePlan.aspx.cs" Inherits="Hick.CarePlan.ASPX.CarePlan" %>

<%@ Register  TagName="CarePlan" TagPrefix="UCCarePlan" Src="~/CarePlan/UserControls/CarePlan.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplPatientLookUp" runat="server">
<UCCarePlan:CarePlan ID="CarePlan1" runat="server" />

</asp:Content>
