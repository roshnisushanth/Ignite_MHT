<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LogonControl.ascx.cs" Inherits="Hick.Logon.FLD.LogonControl" %>
<div class="border align ">
    <div class="divcontrolspacing">
        <label for="txtlastname" class="fontweight">
            Last Name</label>
        <input type="text" class="form-control textarea" onclick="func()" runat="server"
            id="txtlastname" placeholder="Enter Last Name" />
    </div>
    <div class="divcontrolspacing">
        <label for="txtfirstname" class="fontweight">
            First Name</label>
        <input type="text" class="form-control textarea" onclick="func()" runat="server"
            id="txtfirstname" placeholder="Enter First Name" />
    </div>
    <div class="divcontrolspacing">
        <label for="dob" class="fontweight">
            Date of Birth</label>
        <input type="text" class="form-control textarea" onclick="func()" runat="server"
            id="txtdob" placeholder="MM/DD/YYYY" autocomplete="off" />
    </div>
    <div class="checkbox" style="width: 100%; height: 8px;">        
    </div>
    <asp:Panel ID="p" runat="server" DefaultButton="Button1">
        <asp:Button ID="Button1" runat="server" OnClick="Logon_Click" class="btn btn-default textareaSubmit"
            Text="Sign in"></asp:Button>
    </asp:Panel>
    <br>   
</div>


