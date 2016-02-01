<%@ Page Title="" Language="C#" MasterPageFile="~/PatientLookUp/TimerLog.Master"
    AutoEventWireup="true" CodeBehind="TimerLog.aspx.cs" Inherits="Hick.PatientLookUp.ASPX.TimerLog"
    MetaKeywords="TimerLog" %>

<%@ Register TagName="TimerLog" TagPrefix="UCTimerLog" Src="~/PatientLookUp/UserControls/UCTimerLog.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cplhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplPatientLookUp" runat="server">
    <UCTimerLog:TimerLog ID="uctimerlog" runat="server" />
    <asp:HiddenField ID="hdnPatientId" runat="server" />
    <asp:HiddenField ID="hdnUserId" runat="server" />
</asp:Content>
