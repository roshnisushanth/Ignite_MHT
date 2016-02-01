<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="AddEditAuthorizedUser.aspx.cs" Inherits="Hick.PatientLookUp.ASPX.AddEditAuthorizedUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/patientlookup.css" rel="stylesheet" />
    <link href="~/Content/style.css" rel="stylesheet" />
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="//code.jquery.com/ui/1.11.1/jquery-ui.js"></script>
    <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
    <script type="text/javascript">
        $(document).ready(function () {
          <%--  $('#DOB').datepicker({ maxDate: new Date() });
            var action = document.getElementById('<%=hdnaction.ClientID%>').value;
            if (action == "Edit") {
                $('calendar_img').hide();
            }
            else {
                $('#calendar_img').click(function () {
                    $('#DOB').datepicker('show');
                    $('calendar_img').show();
                });
            }--%>


            $(document).ready(function () {
                $('#DOB').datepicker({ maxDate: new Date() });
                $('#calendar_img').click(function () {
                    $('#DOB').datepicker('show');
                });
            });


            $('#cancel').click(function () {
                parent.window.location.href = parent.window.location.href;
            });
            $('#save_User_Click').click(function () {
                var pswd_length = $("input#username").val().length;
                if (pswd_length < 4) {
                    alert("Password must contain at least 4 charater");
                    return false;
                }
            });

            $("#Relationship").change(function () {
                if($('#Relationship').val()=='Other')
                {
                    $('#divOtherRelationship').show();
                    $('#OtherRelationship').attr("required", true);
                }
                else {
                    $('#divOtherRelationship').hide();
                    $('#OtherRelationship').attr("required", false);
                }
            });

        });

        function isAlphaNumeric(e) { // Alphanumeric only
            var k;
            document.all ? k = e.keycode : k = e.which;
            return ((k > 47 && k < 58) || (k > 64 && k < 91) || (k > 96 && k < 123) || k == 0);
        }

        function ValidateAlpha(evt) {
            var keyCode = (evt.which) ? evt.which : evt.keyCode
            if ((keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 123) && keyCode != 32)

                return false;
            return true;
        }


    </script>
    <style>
        .add-form {
    padding-top: 5px;
    overflow-y: hidden;
    overflow-x: hidden;
    height: 480px;
}
    </style>
</head>
<body>


    <form id="form1" runat="server"  style="font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; ">

        <asp:HiddenField ID="hdnaction" runat="server" />
        <div class="add-form">


                 <div class="form-group">
    <label for="inputEmail3" class="col-sm-2 control-label">First Name</label>
    <div class="col-sm-10">
     
       <asp:TextBox runat="server" ID="FirstName" required="required" class="form-control" ></asp:TextBox>
        <%--<span class="error">Error</span>--%>
    </div>
  </div>
                    <div class="form-group">
    <label for="inputPassword3" class="col-sm-2 control-label">Last Name</label>
    <div class="col-sm-10">

               <asp:TextBox runat="server" required="required" ID="LastName" class="form-control" ></asp:TextBox>
    </div>
  </div>
                 <div class="form-group">
    <label for="inputPassword3" class="col-sm-2 control-label">DOB</label>
    <div class="col-sm-10">

                        <div>              
                    <asp:TextBox ID="DOB" required="required" runat="server" CssClass="popup_textbox" ClientIDMode="Static" Style="margin-left: 0px;"></asp:TextBox>
                     <asp:HiddenField ID="hdnusr_id" runat="server" />
                    <img src="../../Images/calendar.jpg" id="calendar_img" class="txtbox_spanimg cald" alt="calendar" <%--style="float: right;
    margin-top: -29px!important;   margin-right: 10px;"--%>/>
                </div>
    </div>
  </div>
                 <div class="form-group">
    <label for="inputPassword3" class="col-sm-2 control-label">Relationship</label>
    <div class="col-sm-10">

 <select class="form-control" ID="Relationship" runat="server">
                        <option value="Parent">Parent</option>
                        <option value="Spouse/Partner">Spouse/Partner</option>
                        <option value="Medical POA">Medical POA</option>
                        <option value="Legal Guardian">Legal Guardian</option>
                        <option value="Other">Other</option>
                    </select>


    </div>
  </div>
                 <div class="form-group" style="display:none" id="divOtherRelationship" >
    <label for="inputPassword3" class="col-sm-2 control-label">Other Relationship</label>
    <div class="col-sm-10">
        
           <asp:TextBox runat="server" ID="OtherRelationship" class="form-control"></asp:TextBox>
    </div>
  </div>

                  <div class="form-group">
    <label for="inputPassword3" class="col-sm-2 control-label">Email</label>
    <div class="col-sm-10">

                   <asp:TextBox required="required" type="email" runat="server" ID="Email" class="form-control"></asp:TextBox>
    </div>
  </div>

                  <div class="form-group">
    <label for="inputPassword3" class="col-sm-2 control-label">Create Passcode</label>
    <div class="col-sm-10">
        
                   <asp:TextBox required="required" runat="server" ID="Passcode" class="form-control" OnKeypress="javascript:return isAlphaNumeric(event,this.value);"></asp:TextBox>

    </div>
  </div>
  <p style="text-align:center;font-size: 11px;  margin:0;">Please create a passcode. The email recipient will need to enter it to accept the invitation. To protect your information it is recommended to share the passcode in another format with the recipient.  If the recipient forgets the passcode you will need to resend the invitation.</p>
                <div class="popup_conter" style="margin:0;">
               <asp:Button runat="server" ID="save_User"  CssClass="btn_standard" Text="Save" OnClick="save_User_Click"></asp:Button>
               <asp:Button runat="server" ID="Revoke"  CssClass="btn_standard" Text="Revoke Access Privileges" OnClick="Revoke_Click"></asp:Button>
            <%--   <asp:Button runat="server" ID="canclel"  CssClass="btn_standard" Text="Cancel" OnClick="canclel_Click"></asp:Button>--%>
             <input type="button" id="cancel"  class="btn_standard" value="Cancel" />
           </div>  
  


        </div>
                
             
     
    </form>
</body>
</html>


