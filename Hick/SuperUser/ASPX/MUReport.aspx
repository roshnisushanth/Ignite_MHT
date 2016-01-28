<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SuperUser/SuperUserMaster.Master" CodeBehind="MUReport.aspx.cs" Inherits="Hick.SuperUser.ASPX.MUReport" MetaKeywords="mureport" %>

<%@ Register  TagName="MUReports" TagPrefix="UCMUReport" Src="~/SuperUser/UserControls/UCMUReport.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplSuperUser" runat="server">
<UCMUReport:MUReports ID="MUReports" runat="server" />
</asp:Content>
