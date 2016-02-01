<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxMUReportDetails.aspx.cs" Inherits="Hick.SuperUser.ASPX.AjaxMUReportDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link href="~/Content/patientlookup.css" rel="stylesheet" />
    <link href="~/Content/style.css" rel="stylesheet" />
    <style>
     .btn_standard.btn-stan{ height: 25px;  min-width: auto; font-size:12px;}
     .modal-content { border-radius: 0;}/*.chec-main{min-height:18px;}*/
     .view-pop-det{height: 300px; overflow-y: scroll; overflow-x: hidden;}
     .mes-no th, .mes-no td{padding:3px;}
     .mureport{height:270px; overflow-y:scroll; overflow-x:hidden;}
    </style>
</head>
<body>
    <form id="form1" runat="server">

    <div class="mureport">
        <asp:GridView ID="MUReportDetails" runat="server" AutoGenerateColumns="false" class="mes-no" style="overflow-y: scroll; overflow-x:hidden;height: 267px; width:100%;">
            <Columns>
         <asp:BoundField DataField="PatientName" HeaderText="Patient Name" />
        <asp:BoundField DataField="DataType" HeaderText="Data Type" />
        <asp:BoundField DataField="DataValue" HeaderText="Data Value"/>
        <asp:BoundField DataField="DateOfActivity" HeaderText="Date of Activity" />
        <asp:BoundField DataField="TimeOfActivity" HeaderText="Time of Activity" />
        <asp:BoundField DataField="IpAddress" HeaderText="IP Address" />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
