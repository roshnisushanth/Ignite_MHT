<%@ Page Title="" Language="C#" MasterPageFile="~/PatientLookUp/TimerLog.Master"
    AutoEventWireup="true" CodeBehind="AddTask.aspx.cs" Inherits="Hick.PatientLookUp.ASPX.AddTask"
    MetaKeywords="AddTask" %>

<%@ Register TagName="AddTask" TagPrefix="UCAddTask" Src="~/PatientLookUp/UserControls/UCAddTask.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplPatientLookUp" runat="server">
    <UCAddTask:AddTask ID="ucaddtask" runat="server" />
    <asp:HiddenField ID="hdnPatientId" runat="server" />
    <asp:HiddenField ID="hdnUserId" runat="server" />
</asp:Content>
