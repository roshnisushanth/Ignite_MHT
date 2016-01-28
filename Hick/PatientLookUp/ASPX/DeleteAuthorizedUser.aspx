<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeleteAuthorizedUser.aspx.cs" Inherits="Hick.PatientLookUp.ASPX.DeleteAuthorizedUser" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="~/Content/patientlookup.css" rel="stylesheet" />
    <link href="~/Content/style.css" rel="stylesheet" />
    <style type="text/css">
        .add-form {
    padding-top: 5px;
    overflow-y: hidden;
    overflow-x: hidden;
    height: 220px;
}
  
    </style>
</head>
<body>
    

    <form id="form1" runat="server"  style="font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; ">

        
        <div class="add-form">
            <p style="text-align: center;">Please confirm that you would like to Revoke this User Access Privileges</p>

           <asp:HiddenField ID="hdnuserid" runat="server" />  <asp:HiddenField ID="hdnemail" runat="server" />
                <div class="popup_conter">
               <asp:Button runat="server" ID="Revoke_User"  CssClass="btn_standard" Text="Revoke" OnClick="Revoke_User_Click"></asp:Button>
               <asp:Button runat="server" ID="canclel"  CssClass="btn_standard" Text="Cancel" OnClick="canclel_Click"></asp:Button>
             
           </div>  
  


        </div>
                
             
     
    </form>
</body>
</html>
