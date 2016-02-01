<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditMedications.aspx.cs"
    Inherits="Hick.PatientLookUp.ASPX.AddEditMedications" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="~/Content/patientlookup.css" rel="stylesheet" />
    <link href="~/Content/style.css" rel="stylesheet" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="https://code.jquery.com/ui/1.11.1/jquery-ui.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtdate').datepicker({ maxDate: new Date() });
            $('#medicationStop').datepicker({ maxDate: new Date() });
            $('#imgMedicationStop').click(function () {
                $('#medicationStop').datepicker('show');
            });

            $('#calImginactive').click(function () {
                $('#inactivescience').datepicker('show');
            });
            $('#calendar_img').click(function () {
                $('#txtdate').datepicker('show');
            });

            if ($('#<%=radioMedications.ClientID %> input:checked').val() == 'Active') {
                ValidatorEnable(document.getElementById('<%=stopError.ClientID%>'), false);
                $('.txtInctive').hide();
            }
            else {
                ValidatorEnable(document.getElementById('<%=stopError.ClientID%>'), true);
                $('.txtInctive').show();
            }
            $("#<%=radioMedications.ClientID%>").change(function () {
                if ($('#<%=radioMedications.ClientID %> input:checked').val() == 'Active') {
                    ValidatorEnable(document.getElementById('<%=stopError.ClientID%>'), false);
                    $('.txtInctive').hide();
                }
                else {
                    ValidatorEnable(document.getElementById('<%=stopError.ClientID%>'), true);
                    $('.txtInctive').show();
                }
            });

            $("#cancle_editmedication").click(function () {
                parent.window.location.href = parent.window.location.href;
            });
        });
    </script>
   <style>       
       .rado{padding-bottom:2px;}
   </style>
</head>
<body>
    <form id="form1" runat="server" style="font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif;">
        <div>
            <div class="edit_medicationdiv" style="float: left; margin: 6px; width: 99%;">
                <div class="popup_content" style="display: inline-flex;">

                    <div style="display: inline-flex;" class="form-group">
                        <req>*</req>
                        <div class="col-sm-3 control-label">Medication</div>
                        <div class="col-sm-9">
                            <asp:TextBox ID="medications" runat="server" CssClass="popup_textbox" ClientIDMode="Static" Style="width: 150px; float: left;"></asp:TextBox>
                            <div style="margin-top: 8px; margin-left: 10px;" class="pull-left rado">

                                <asp:RadioButtonList ID="radioMedications" CssClass="paddradiobtn" Style="width: auto;" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Active" Value="Active" Enabled="true" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Inactive" Value="Inctive"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="medicationserror" CssClass="error" Style="margin-left: 5%;" 
                    ControlToValidate="medications" ErrorMessage="Medication required" runat="server"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="regexpName" runat="server"      CssClass="error" Style="margin-left: 15px;" 
                                    ErrorMessage="Please enter in Alphabet!" ControlToValidate="medications" ValidationExpression="^[a-zA-Z'.\s]{1,40}$" />
                        </div>
                    </div>

                </div>
                
                <div>

                    <div style="display: -webkit-box; display: inline-flex; margin-top: 20px;" class="form-group">
                        <req>*</req>
                        <div class="col-sm-3 control-label">Dosage</div>
                        <div class="col-sm-9">
                            <asp:TextBox ID="dosage" runat="server" CssClass="popup_textbox" ClientIDMode="Static" Style="width: 220px;"></asp:TextBox>
                            
                            <asp:DropDownList ID="drp_dosageunits" runat="server" class="dosage" Style="margin-left: 10px;">
                                <asp:ListItem>mg</asp:ListItem>
                                <asp:ListItem>floz</asp:ListItem>
                                <asp:ListItem>g</asp:ListItem>
                                <asp:ListItem>gtts</asp:ListItem>
                                <asp:ListItem>mcg</asp:ListItem>
                                <asp:ListItem>mL</asp:ListItem>
                                <asp:ListItem>oz</asp:ListItem>
                                <asp:ListItem>tbsp</asp:ListItem>
                                <asp:ListItem>tsp</asp:ListItem>
                                <asp:ListItem>IU</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="dosageError" CssClass="error" Style="margin-left: 5%;"
                        ControlToValidate="dosage" ErrorMessage="Dosage required" runat="server"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"  CssClass="error" Style="margin-left: 15px;" 
                                    ErrorMessage="Please enter in Number!" ControlToValidate="dosage" ValidationExpression="^\d+$" />
                        </div>
                    </div>
                    

                    <div style="padding-top: 9px;">
                        <div style="display: -webkit-box; display: inline-flex; margin-top: 20px;" class="form-group">
                            <req>*</req>
                            <div class="col-sm-3 control-label">Medication Start Date</div>
                            <div class="col-sm-9">
                                <asp:TextBox ID="txtdate" runat="server" CssClass="popup_textbox" ClientIDMode="Static" Style="margin-left: 15px;"></asp:TextBox>
                                <img src="../../Images/calendar.jpg" id="calendar_img" class="txtbox_spanimg" alt="calendar" style="margin-top: -3px; vertical-align: middle;" />
                            </div>
                        </div>
                        <br />
                        <div>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="activedateError" CssClass="error" Style="margin-left: 34%;"
                                ControlToValidate="txtdate" ErrorMessage="Medication start date required" runat="server"></asp:RequiredFieldValidator>
                        </div>
                        <div class="txtInctive form-group" style="display: -webkit-box; display: inline-flex; margin-top: 20px;">
                            <req style="color:#fff;">*</req>
                            <div class="col-sm-3 control-label">Medication Stop Date</div>
                            <div class="col-sm-9">
                                <asp:TextBox ID="medicationStop" runat="server" CssClass="popup_textbox" ClientIDMode="Static" Style="margin-left: 15px;"></asp:TextBox>
                                <img src="../../Images/calendar.jpg" id="imgMedicationStop" class="txtbox_spanimg" alt="calendar" style="margin-top: -3px; vertical-align: middle;" />
                            </div>
                        </div>
                        <asp:RequiredFieldValidator Display="Dynamic" ID="stopError" CssClass="error" Style="margin-left: 34%;"
                            ControlToValidate="medicationStop" ErrorMessage="Medication stop date required" runat="server"></asp:RequiredFieldValidator>
                        <div class="popup_conter">
                            <asp:Button runat="server" ID="save_editmedication" CssClass="btn_standard" Text="Save" OnClick="savemedications"></asp:Button>
                            <input type="button" value="Cancel" name="cancle_edit_medication" class="btn_standard" id="cancle_editmedication" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    
</body>
</html>
