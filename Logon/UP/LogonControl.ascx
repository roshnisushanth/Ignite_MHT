<%@ Control Language="C#" AutoEventWireup="true" Debug="true"  CodeBehind="LogonControl.ascx.cs" Inherits="Hick.Logon.UP.LogonControl" %>


<div class="border align ">
    <div class="form-group ">
       
        <label for="usrnme" class="fontweight">Username:</label> 
          <asp:Label ID="lblerrormsg" runat="server" ForeColor="red"  Text="" ></asp:Label>
        <input type="text" class="form-control textarea" onclick="func()" runat="server" id="txtusername" />
    </div>
    <div class="form-group">
        <label for="pwd" class="fontweight">Password:</label>
        <input type="password" class="form-control textarea" onclick="func()" runat="server" id="txtpassword" />
    </div>
    <div class="checkbox">
        <ul id="menu" class="padding">
            <li>
                <label><input type="checkbox" />Remember me</label>

            </li>
            <li class="Remem">
                <a href="#" class="linkcolor">FORGOT PASSWORD?</a>
            </li>
        </ul>
    </div>
    <asp:Button ID="Button1" runat="server" OnClick="Logon_Click" class="btn btn-default textareaSubmit" Text="Sign in"></asp:Button>
    <br>
    <p class="CreateAlign">
        or <a href="signup/mht/ui/VerifyPatient.aspx" class="Create">Create an Account</a>
    </p>
</div>
