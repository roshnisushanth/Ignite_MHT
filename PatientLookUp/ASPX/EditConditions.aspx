<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditConditions.aspx.cs"
    Inherits="Hick.PatientLookUp.ASPX.EditConditions" %>

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
            $('#inactivescience').datepicker({ maxDate: new Date() });
            $('#calendar_img').click(function () {
                $('#dob').datepicker('show');
            });
            $('#calImginactive').click(function () {
                $('#inactivescience').datepicker('show');
            });

            if ($('#<%=radioconditions.ClientID %> input:checked').val() == 'Active') {
                ValidatorEnable(document.getElementById('<%=inactivesinceError.ClientID%>'), false);
                $('.txtInctive').hide();
            }
            else {
                ValidatorEnable(document.getElementById('<%=inactivesinceError.ClientID%>'), true);
                $('.txtInctive').show();
            }
            $("#<%=radioconditions.ClientID%>").change(function () {
                if ($('#<%=radioconditions.ClientID %> input:checked').val() == 'Active') {
                    ValidatorEnable(document.getElementById('<%=inactivesinceError.ClientID%>'), false);
                    $('.txtInctive').hide();
                }
                else {
                    ValidatorEnable(document.getElementById('<%=inactivesinceError.ClientID%>'), true);
                    $('.txtInctive').show();
                }
            });
        });
    </script>
    <%--<script type="text/javascript">
        $(document).ready(function () {
            $("#CodeConditions").autocomplete({

                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "EditConditions.aspx/GetAutoCompleteData",
                        data: "{'ICDCode':'" + extractLast(request.term) + "'}",
                        dataType: "json",
                        success: function (data) {
                            alert(data);
                            response(data.d);

                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });
                },
                focus: function () {
                    return false;
                },
                select: function (event, ui) {
                    var terms = split(this.value);
                    // remove the current input
                    terms.pop();
                    // add the selected item
                    terms.push(ui.item.value);
                    // add placeholder to get the comma-and-space at the end
                    terms.push("");
                    this.value = terms.join("; ");
                    return false;
                }
            });

            $("#CodeConditions").bind("keydown", function (event) {
                if (event.keyCode === $.ui.keyCode.TAB &&
                    $(this).data("autocomplete").menu.active) {
                    event.preventDefault();
                }
            });
        });
        function split(val) {
            return val.split(/;\s*/);
        }
        function extractLast(term) {
            return split(term).pop();
        }
    </script>--%>
    <style>
        .ui-datepicker {
            width: 14em!important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif;">
    <div>
        <asp:HiddenField ID="hdndesc" runat="server" Value="test" />
        <div class="edit_conditionsdiv" style="float: left; margin: 6px; width: 100%;">
            <div id="Div1" class="popup_content" runat="server">
                <div class="popup_conter">
                    <div class="form-lft">
                        <req>*</req>
                        ICD 10 Code</div>
                    <div class="form-rgt">
                    <asp:TextBox ID="CodeConditions" runat="server" CssClass="popup_textbox" ClientIDMode="Static"
                        Style=""></asp:TextBox><br />
                    <asp:RequiredFieldValidator Display="Dynamic" ID="CodeConditionserror" CssClass="error"
                        ControlToValidate="CodeConditions" ErrorMessage="ICD 10 code required" runat="server" Style="margin-left: -2%; text-align: center"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator Display="Dynamic" runat="server" ID="numberICDerror"
                        CssClass="error" ControlToValidate="CodeConditions" ValidationExpression="\d+(\.\d{1,2})?"
                        ErrorMessage="Please enter in numeric text!" />
                
                <div style=" margin-top: 15px; margin-bottom: 0px; margin-left: 23px;">
                    <asp:RadioButtonList ID="radioconditions" CssClass="paddradiobtn" Style="width: auto;"
                        runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Active" Value="Active" Enabled="true" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Inactive" Value="Inctive"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                        </div>
                <div class="popup_conter" style="width: auto; align-items: center;">
                    <div class="form-lft">
                        <req>*</req>
                        Onset Date</div>
                    <div class="form-rgt" style="position:relative;">
                        
                            <asp:TextBox ID="dob" runat="server" CssClass="popup_textbox" ClientIDMode="Static"
                                Style="margin-left: 17px;"></asp:TextBox>
                        
                            <img src="../../Images/calendar.jpg" id="calendar_img" class="txtbox_spanimg" alt="calendar"
                                style="margin-top: -3px; position: absolute;  top: 2px;" ReadOnly="true" />
                        <asp:RequiredFieldValidator Display="Dynamic" ID="dobError" CssClass="error" ControlToValidate="dob"
                        ErrorMessage="onset date required" runat="server" Style="margin-left: 15%; text-align: left;"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            
                <div class="popup_conter txtInctive" style=" align-items: center; ">
                    <div class="form-lft">
                        <req>*</req>
                        Inactive Since</div>
                    <div class="form-rgt" style="position:relative;">
                        <asp:TextBox ID="inactivescience" runat="server" CssClass="popup_textbox" ClientIDMode="Static"
                            Style="margin-left: 12px;"></asp:TextBox>
                    
                        <img src="../../Images/calendar.jpg" id="calImginactive" class="txtbox_spanimg" alt="calendar"
                            style="margin-top: -3px; position: absolute;  top: 2px;" ReadOnly="true"/>
                        <asp:RequiredFieldValidator Display="Dynamic" ID="inactivesinceError" CssClass="error"
                    ControlToValidate="inactivescience" ErrorMessage="Inactive since date required"
                    runat="server" Style="margin-left: 15%; text-align: left;"></asp:RequiredFieldValidator>

                    </div>
                </div>
                
            
            <div class="popup_conter">
                <asp:Button ID='btnSaveCondition' CssClass='btn_standard' runat='server' Text='Save'
                    OnClick='btnSaveCondition_Click'></asp:Button>
                <input type="button" value="Cancel" name="cancle_edit_conditions" class="btn_standard"
                    id="cancle_edit_conditions"  />
            </div>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $('#cancle_edit_conditions').click(function () {
            parent.window.location.href = parent.window.location.href;
        });

        $(function () {
            $('#CodeConditions').autocomplete({
                max: 8,
                source: function (request, response) {
                    $.ajax({
                        url: "EditConditions.aspx/GetMasterICD9CodesAutoComplete",
                        data: "{ 'pre':'" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return { value: item.CodeId, key: item.Id }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            //alert(textStatus);
                        }
                    });
                }
            });
        });

    </script>
</body>
</html>
