<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditMedications.aspx.cs"
    Inherits="Hick.Encounters.ASPX.AddEditMedications" %>

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
            $('#startDate').datepicker({ maxDate: new Date() });
            $('#medicationStop').datepicker({ maxDate: new Date() });
            $('#imgMedicationStop').click(function () {
                $('#medicationStop').datepicker('show');
            });

           
            $('#calImginactive').click(function () {
                $('#inactivescience').datepicker('show');
            });
            $('#calendar_img').click(function () {
                $('#startDate').datepicker('show');
            });

            if ($('#<%=radioMedications.ClientID %> input:checked').val() == 'Active') {
               <%-- ValidatorEnable(document.getElementById('<%=stopError.ClientID%>'), false);--%>
                $('.txtInctive').hide();
            }
            else {
                <%--ValidatorEnable(document.getElementById('<%=stopError.ClientID%>'), true);--%>
                $('.txtInctive').show();
            }
            $("#<%=radioMedications.ClientID%>").change(function () {
                if ($('#<%=radioMedications.ClientID %> input:checked').val() == 'Active') {
                    <%--ValidatorEnable(document.getElementById('<%=stopError.ClientID%>'), false);--%>
                    $('.txtInctive').hide();
                }
                else {
                    <%--ValidatorEnable(document.getElementById('<%=stopError.ClientID%>'), true);--%>
                    $('.txtInctive').show();
                }
            });

            $("#save_editmedication").click(function () {
                var aId = $('#asId').val();
                var mId = $('#mId').val();
                var pageMode = $('#pageMode').val();
                var med = $('#medications').val();
                var dage = $('#dosage').val();
                var type = $('#dosageType').val();
                var sdate = $('#startDate').val();
                var edate = $('#medicationStop').val();
                var isActive = $('#<%=radioMedications.ClientID %> input:checked').val() == 'Active' ? true : false;
                var id = pageMode == 'add' ? aId : mId;
                var method = pageMode == 'add' ? 'AddMedication' : 'EditMedication';
                var dataOB = { "id": id, "medication": med, "isActive": isActive, "dosage": dage, "type": type, "startDate": sdate, "stopDate": edate };
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
                            if (pageMode == 'add' && result && result.d) {
                                $('#mId').val(result.d.Id);
                                parent.reloadMed(result.d);
                                console.log('medication add');
                            }
                            else {
                                if (result && result.d) {
                                    parent.updateMed(result.d);
                                    console.log('medication updated');
                                }
                            }
                        }
                    },
                    error: function () {
                        alert("Could not 'Add Condition'. Please contact administrator");
                    }
                });
            });

            $("#cancle_editmedication").click(function () {
                parent.closeMedpopUp();
            });
            
        });

    </script>
</head>
<body>
    <form id="form1" runat="server" style="font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; font-size: 14px;">
        <div>
            <div class="edit_medicationdiv" style="float: left; margin: 6px; width: 99%;">
                <div class="popup_content" style="display: inline-flex;">

                    <div style="display: inline-flex;">
                        <div style="padding-top: 10px;"><span style="margin-left: 40px;">Medication</span></div>
                        <div style="margin-left: 66px;">
                            <asp:TextBox ID="medications" runat="server" CssClass="popup_textbox" ClientIDMode="Static" Style="width: 220px;"></asp:TextBox>
                        </div>
                    </div>
                    <div style="margin-top: 5px; margin-left: 10px;">
                        <asp:RadioButtonList ID="radioMedications" CssClass="paddradiobtn" Style="width: auto;" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Active" Value="Active" Enabled="true" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Inactive" Value="Inctive"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                </div>
                <div>

                    <div style="display: -webkit-box; display: inline-flex; margin-top: 20px;">
                        <div><span style="margin-left: 40px;">Dosage</span></div>
                        <div style="margin-left: 87px;">
                            <asp:TextBox ID="dosage" runat="server" CssClass="popup_textbox" ClientIDMode="Static" Style="width: 220px;"></asp:TextBox>
                            <select id="dosageType" class="dosage" style="margin-left: 10px;">
                                <option>mg
                                </option>
                            </select>
                        </div>
                    </div>
                    <div style="padding-top: 9px;">
                        <div style="display: -webkit-box; display: inline-flex; margin-top: 20px;">
                            <div style="padding-top: 9px;"><span style="margin-left: 40px;">Medication Start Date</span></div>
                            <div>
                                <asp:TextBox ID="startDate" runat="server" CssClass="popup_textbox" ClientIDMode="Static" Style="margin-left: 14px;"></asp:TextBox>
                            </div>
                            <div style="padding-top: 8px;">
                                <img src="../../Images/calendar.jpg" id="calendar_img" class="txtbox_spanimg" alt="calendar" style="margin-top: -3px;" />
                            </div>
                        </div>
                        <br />
                        <div class="txtInctive" style="display: -webkit-box; display: inline-flex; margin-top: 20px;">
                            <div style="padding-top: 9px;"><span style="margin-left: 40px;">Medication Stop Date</span></div>
                            <div>
                                <asp:TextBox ID="medicationStop" runat="server" CssClass="popup_textbox" ClientIDMode="Static" Style="margin-left: 15px;"></asp:TextBox>
                            </div>
                            <div style="padding-top: 8px;">
                                <img src="../../Images/calendar.jpg" id="imgMedicationStop" class="txtbox_spanimg" alt="calendar" style="margin-top: -3px;" />
                            </div>
                        </div>
                        <div class="popup_conter">
                            <input type="button" value="Save" name="save_editmedication" class="btn_standard" id="save_editmedication" />
                            <input type="button" value="Cancel" name="cancle_edit_medication" class="btn_standard" id="cancle_editmedication" />
                        </div>
                    </div>
                </div>
            </div>
            <input type="hidden" clientidmode="Static" id="mId" name="mId" runat="server" />
            <input type="hidden" clientidmode="Static" id="pageMode" name="pageMode" runat="server" />
            <input type="hidden" clientidmode="Static" id="asId" name="asId" runat="server" />
        </div>
    </form>
</body>
</html>
