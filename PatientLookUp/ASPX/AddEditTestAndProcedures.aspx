<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditTestAndProcedures.aspx.cs" Inherits="Hick.PatientLookUp.ASPX.AddEditTestAndProcedures" %>

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
            $('#testdate').datepicker({ maxDate: new Date() });
            $('#calendar_img').click(function () {
                $('#testdate').datepicker('show');
            });
        });


    </script>
    <style>.popup_textbox{margin-left:0;}
    .lab-result{width: 380px; margin: 0 auto;}
        .rgt-lab {width: 169px; }

    </style>
</head>
<body>
    <form id="form1" runat="server" style="font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; width:500px;">
    <div>
        <div class="lab-result">
            <div style="display:-webkit-box;display:inline-flex;margin-top:3px;">
                  <div class="lft-lab"><span ><req>*</req>Test / Procedure</span></div>
                  <div  class="rgt-lab">
                    <asp:TextBox ID="testProcedure" runat="server" CssClass="popup_textbox" ClientIDMode="Static" Style="width: 100%"></asp:TextBox>
                     <asp:RequiredFieldValidator Display="Dynamic" ID="allergyError" CssClass="error" 
                 ControlToValidate="testProcedure" ErrorMessage="Test / Procedure required" runat="server"></asp:RequiredFieldValidator> 
            
                  </div>                                   
            
              </div> 
            <div style="display:-webkit-box;display:inline-flex;margin-top:3px;">
                  <div class="lft-lab"><span><req>*</req>Details</span></div>
                 <div  class="rgt-lab">
                    <asp:TextBox ID="details" runat="server" CssClass="popup_textbox" ClientIDMode="Static" Style="width: 100%"></asp:TextBox>                                
                  <asp:RequiredFieldValidator Display="Dynamic" ID="reactionError" CssClass="error" 
                 ControlToValidate="details" ErrorMessage="Details required" runat="server"></asp:RequiredFieldValidator> 
            </div>  
            
                </div>
            <div style="display:-webkit-box;display:inline-flex;margin-top:3px;">
                 <div class="lft-lab"><span ><req>*</req>Test Date</span></div>
                <div  class="rgt-lab">
                <div>              
                    <asp:TextBox ID="testdate" runat="server" CssClass="popup_textbox" ClientIDMode="Static"></asp:TextBox>
                
                    <img src="../../Images/calendar.jpg" id="calendar_img" class="txtbox_spanimg cald" alt="calendar" style="margin-top: -3px;"/>
               <br /> <asp:RequiredFieldValidator Display="Dynamic" ID="dobError" CssClass="error" 
                 ControlToValidate="testdate" ErrorMessage="Test date required" runat="server"></asp:RequiredFieldValidator> 
             </div>                                
            
            
                </div>   
                    </div> 
          </div>            
          <div class="popup_conter">
               <asp:HiddenField runat="server" ID="buttontype" />
               <asp:Button runat="server" ID="save_testandprocedure" CssClass="btn_standard" Text="Save" OnClick="saveProcedure"></asp:Button>
               <input type="button" value="Cancel" name="cancle_edit_allergies" class="btn_standard" id="cancle_testandprocedure1" >
           </div>     
        </div>
    </form>
</body>
</html>

             
<script type="text/javascript">
    


    $("#cancle_testandprocedure").click(function () {
        $("#divtestandprocedure").dialog('close');
    });
    $("#cancle_testandprocedure1").click(function () {
        parent.window.location.href = parent.window.location.href;
    });

</script>