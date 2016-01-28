<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditConditions.aspx.cs"
    Inherits="Hick.Encounters.ASPX.EditConditions" %>

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
            $('#OnsetDate').datepicker({ maxDate: new Date() });
            $('#inactivescience').datepicker({ maxDate: new Date() });
            $('#calendar_img').click(function () {
                $('#OnsetDate').datepicker('show');
            });
            $('#calImginactive').click(function () {
                $('#inactivescience').datepicker('show');
            });

            if ($('#<%=radioconditions.ClientID %> input:checked').val() == 'Active') {
                <%--ValidatorEnable(document.getElementById('<%=inactivesinceError.ClientID%>'), false);--%>
                $('.txtInctive').hide();
                $('#dOnSetDate').html('Onset Date');
            }
            else {
               <%-- ValidatorEnable(document.getElementById('<%=inactivesinceError.ClientID%>'), true);--%>
                $('.txtInctive').show();
                $('#dOnSetDate').html('Active Since');
            }
            $("#<%=radioconditions.ClientID%>").change(function () {
                if ($('#<%=radioconditions.ClientID %> input:checked').val() == 'Active') {
                   <%-- ValidatorEnable(document.getElementById('<%=inactivesinceError.ClientID%>'), false);--%>
                    $('.txtInctive').hide();
                    $('#dOnSetDate').html('Onset Date');
                }
                else {
                    <%--ValidatorEnable(document.getElementById('<%=inactivesinceError.ClientID%>'), true);--%>
                    $('.txtInctive').show();
                    $('#dOnSetDate').html('Active Since');
                }
            });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server" style="font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif;">
        <div>
            <asp:HiddenField ID="hdndesc" runat="server" Value="test" />
            <div class="edit_conditionsdiv" style="float: left; margin: 6px; width: 100%;">
                <div id="Div1" class="popup_content" runat="server">
                    <div class="popup_conter">
                        <span>ICD 9 Code</span>
                        <asp:TextBox ID="CodeConditions" runat="server" CssClass="popup_textbox textboxScroll" ClientIDMode="Static"
                            Style="width: 37%;"></asp:TextBox><br />
                    </div>
                    <div style="margin-left: 36%; margin-top: 15px; margin-bottom: 15px;">
                        <asp:RadioButtonList ID="radioconditions" CssClass="paddradiobtn" Style="width: auto;"
                            runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Active" Value="Active" Enabled="true" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Inactive" Value="Inctive"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="popup_conter" style="display: inline-flex; align-items: center; margin-left: 25%;">
                        <div id="dOnSetDate">
                            Onset Date
                        </div>
                        <div style="display: inline-flex;">
                            <div>
                                <asp:TextBox ID="OnsetDate" runat="server" CssClass="popup_textbox" ClientIDMode="Static" Style="margin-left: 13px;"></asp:TextBox>
                            </div>
                            <div style="margin-top: 8px;">
                                <img src="../../Images/calendar.jpg" id="calendar_img" class="txtbox_spanimg" alt="calendar"
                                    style="margin-top: -3px;" />
                            </div>
                        </div>
                    </div>

                </div>
                <div>
                    <div class="popup_conter txtInctive" style="display: inline-flex; display: -webkit-flex; align-items: center; margin-left: 23%;">
                        <div>
                            Inactive Since
                        </div>
                        <div>
                            <asp:TextBox ID="inactivescience" runat="server" CssClass="popup_textbox" ClientIDMode="Static"
                                Style="margin-left: 18px;"></asp:TextBox>
                        </div>
                        <div style="margin-top: 6px;">
                            <img src="../../Images/calendar.jpg" id="calImginactive" class="txtbox_spanimg" alt="calendar"
                                style="margin-top: -3px;" />
                        </div>
                    </div>


                </div>
                <div class="popup_conter">

                    <input type="button" value="Save" name="btnSaveCondition" class="btn_standard" id="btnSaveCondition" />
                    <input type="button" value="Cancel" name="cancle_edit_conditions" class="btn_standard" id="cancle_edit_conditions" />
                </div>
            </div>
            <input type="hidden" clientidmode="Static" id="pageMode" name="pageMode" runat="server" />
            <input type="hidden" clientidmode="Static" id="cId" name="cId" runat="server" />
            <input type="hidden" clientidmode="Static" id="asId" name="asId" runat="server" />
    </form>
    <script type="text/javascript" language="javascript">
        $("#cancle_edit_conditions").click(function () {
            
        });
        //AddCondition(long assessmentId, long CodeId, DateTime ActiveDate, bool IsActive, DateTime OnSetDate, string InActiveDate)
        $('#btnSaveCondition').click(function () {
            var aId = $('#asId').val();
            var cId = $('#cId').val();
            var icdpCode = $('#CodeConditions').val();
            var pageMode = $('#pageMode').val();
            var isActive = $('#<%=radioconditions.ClientID %> input:checked').val() == 'Active' ? true : false;
            var onsetDate = $('#OnsetDate').val();
            var inActiveSince = $('#inactivescience').val();
            var id = pageMode == 'add' ? aId : cId;
            var method = pageMode == 'add' ? 'AddCondition' : 'EditCondition';
            var dataOB = { "Id": id, "ICDCode": icdpCode, "ActiveDate": onsetDate, "IsActive": isActive, "InActiveDate": inActiveSince };

            $.ajax({
                type: "POST",
                url: "../Services/EncounterService.svc/" + method,
                data: JSON.stringify(dataOB),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d == "error") {
                        alert("Sorry an error has occured. Please contact administrator");
                    }
                    else {
                        if (pageMode == 'add') {
                            $('#cId').val(result.d.Id);
                            parent.reloadCon(result.d);
                            console.log('condition add');
                        }
                        else {
                            parent.updateCon(result.d);
                            console.log('condition updated');
                        }
                    }
                },
                error: function () {
                    alert("Could not 'Add Condition'. Please contact administrator");
                }
            });

        });

        $(function () {
            $('#CodeConditions').autocomplete({            
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
