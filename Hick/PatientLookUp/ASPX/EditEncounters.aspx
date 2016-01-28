<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditEncounters.aspx.cs"
    Inherits="Hick.PatientLookUp.ASPX.EditEncounters" %>

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
            $('#txt_date').datepicker({ maxDate: new Date() });

            $('#calendar_img').click(function () {
                $('#txt_date').datepicker('show');
            });

        });
    </script>
    <style>
        .form-rgt input{    width: 100%;     margin-left: 0!important;}
        .form-lft {
    padding-right: 17px;
}span.error {
    text-align: left;
}
        .popup_conter {
            margin-top: 6px;
        }
      
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="edit_conditionsdiv" style="float: left; margin: 6px; width: 100%;">
            <div id="Div1" class="popup_content" runat="server">
                <div class="popup_conter" style="align-items: center;">
                    <div class="form-lft">
                        <req>*</req>
                        Date</div>
                    <div class="form-rgt" style="position:relative;">
                        
                            <asp:TextBox ID="txt_date" runat="server" CssClass="popup_textbox" ClientIDMode="Static"
                                Style="margin-left: 17px;" ></asp:TextBox>
                       
                            <img src="../../Images/calendar.jpg" id="calendar_img" class="txtbox_spanimg" alt="calendar"
                                style="margin-top: -3px; position: absolute; top: 2px;" />


                          <asp:RequiredFieldValidator Display="Dynamic" ID="dobError" CssClass="error" ControlToValidate="txt_date"
                        ErrorMessage="onset date required" runat="server" ></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <div>
                <div class="popup_conter txtInctive" style="
                    align-items: center; ">
                    <div class="form-lft">
                        <req>*</req>
                        Doctor</div>
                    <div class="form-rgt">
                        <asp:TextBox ID="txt_doctor" runat="server" CssClass="popup_textbox" ClientIDMode="Static"
                            Style="margin-left: 12px;"></asp:TextBox>
                         <asp:RequiredFieldValidator Display="Dynamic" ID="inactivesinceError" CssClass="error"
                    ControlToValidate="txt_doctor" ErrorMessage="required field" runat="server" ></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"      CssClass="error"  
                                    ErrorMessage="Please enter in Alphabet!" ControlToValidate="txt_doctor" ValidationExpression="^[a-zA-Z'.\s]{1,40}$" />
                    </div>
                </div>
               
            </div>
            <div>
                <div class="popup_conter txtInctive" style="
                    align-items: center; ">
                    <div class="form-lft">
                        <req>*</req>
                        Reason</div>
                    <div class="form-rgt">
                        <asp:TextBox ID="txt_Reason" runat="server" CssClass="popup_textbox" ClientIDMode="Static"
                            Style="margin-left: 12px;"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" CssClass="error"
                    ControlToValidate="txt_Reason" ErrorMessage="required field" runat="server" ></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"      CssClass="error"  
                                    ErrorMessage="Please enter in Alphabet!" ControlToValidate="txt_Reason" ValidationExpression="^[a-zA-Z'.\s]{1,40}$" />
                    </div>
                </div>
                
            </div>
            <div>
                <div class="popup_conter txtInctive" style="
                    align-items: center; ">
                    <div class="form-lft">
                        <req>*</req>
                        Diagnoses</div>
                    <div class="form-rgt">
                        <asp:TextBox ID="txt_Diagnoses" runat="server" CssClass="popup_textbox" ClientIDMode="Static"
                            Style="margin-left: 12px;"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator2" CssClass="error"
                    ControlToValidate="txt_Diagnoses" ErrorMessage="required field" runat="server"
                    ></asp:RequiredFieldValidator>
                          <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"      CssClass="error"  
                                    ErrorMessage="Please enter in Alphabet!" ControlToValidate="txt_Diagnoses" ValidationExpression="^[a-zA-Z'.\s]{1,40}$" />
                    </div>
                </div>
                
            </div>
            <div class="popup_conter">
                <asp:Button ID='btnSaveCondition' CssClass='btn_standard' runat='server' 
                    Text='Save' onclick="btnSaveCondition_Click">
                </asp:Button>
                <input type="button" value="Cancel" name="cancle_edit_conditions" class="btn_standard"
                    id="cancle_edit_conditions" onclick="parent.closeencPopup();" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
