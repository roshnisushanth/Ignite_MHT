<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditAllergies.aspx.cs" Inherits="Hick.PatientLookUp.ASPX.AddEditAllergies" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/patientlookup.css" rel="stylesheet" />
    <link href="~/Content/style.css" rel="stylesheet" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="https://code.jquery.com/ui/1.11.1/jquery-ui.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#dob').datepicker({ maxDate: new Date() });
            $('#calendar_img').click(function () {
                $('#dob').datepicker('show');
            });
        });

        
    </script>
</head>
<body>
    <form id="form1" runat="server" style="font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; width:500px;">
    <div>
        <div style="margin-left: 42px;">
            <div style="display:-webkit-box;display:inline-flex;margin-top:20px;">
                  <div style="padding-top: 8px;"><span style="margin-left: 40px;"><req>*</req>Allergy</span></div>
                  <div style="margin-left: 30px;">
                    <asp:TextBox ID="allergy" runat="server" CssClass="popup_textbox" ClientIDMode="Static" Style="margin-left: 20px;width: 250px"></asp:TextBox>
                  </div>                                   
            </div>
            <div>
                 <asp:RequiredFieldValidator Display="Dynamic" ID="allergyError" CssClass="error" style="margin-left:32%"
                 ControlToValidate="allergy" ErrorMessage="Allergy required" runat="server"></asp:RequiredFieldValidator> 
                  <asp:RegularExpressionValidator ID="regexpName" runat="server"      CssClass="error" Style="margin-left: 32%;" 
                                    ErrorMessage="Please enter in Alphabet!" ControlToValidate="allergy" ValidationExpression="^[a-zA-Z'.\s]{1,40}$" />
            </div> 
            <div style="display:-webkit-box;display:inline-flex;">
                 <div style="padding-top: 8px;"><span style="margin-left: 35px;"><req>*</req>Reaction</span></div>
                 <div style="margin-left: 30px;">
                    <asp:TextBox ID="reaction" runat="server" CssClass="popup_textbox" ClientIDMode="Static" Style="margin-left: 11px;width: 250px"></asp:TextBox>                                
                 </div>  
            </div>
            <div>
                 <asp:RequiredFieldValidator Display="Dynamic" ID="reactionError" CssClass="error" style="margin-left:32%"
                 ControlToValidate="reaction" ErrorMessage="Reaction required" runat="server"></asp:RequiredFieldValidator> 
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"      CssClass="error" Style="margin-left: 32%;" 
                                    ErrorMessage="Please enter in Alphabet!" ControlToValidate="reaction" ValidationExpression="^[a-zA-Z'.\s]{1,40}$" />
            </div>
            <div style="display:-webkit-box;display:inline-flex;">
                <div style="padding-top: 8px;"><span style="margin-left: 35px;"><req>*</req>Active Since</span></div>
                <div>              
                    <asp:TextBox ID="dob" runat="server" CssClass="popup_textbox" ClientIDMode="Static" Style="margin-left: 17px;"></asp:TextBox>
                </div>
                <div style="padding-top:7px;">
                    <img src="../../Images/calendar.jpg" id="calendar_img" class="txtbox_spanimg cald" alt="calendar" style="margin-top: -3px;"/>
                </div>                                
            </div>
            <div>
                 <asp:RequiredFieldValidator Display="Dynamic" ID="dobError" CssClass="error" style="margin-left:32%"
                 ControlToValidate="dob" ErrorMessage="Active since date required" runat="server"></asp:RequiredFieldValidator> 
            </div>    
          </div>            
          <div class="popup_conter">
               <asp:Button runat="server" ID="save_allergies" CssClass="btn_standard" Text="Save" OnClick="saveAllergy"></asp:Button>
               <input type="button" value="Cancel" name="cancel_edit_allergies" class="btn_standard" id="cancel_editallergies" onclick="parent.ClosePopupallergies();"/>
           </div>     
        </div>
    </form>
</body>
</html>
