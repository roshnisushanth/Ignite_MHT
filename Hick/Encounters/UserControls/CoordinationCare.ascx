<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CoordinationCare.ascx.cs" Inherits="Hick.Encounters.UserControls.CoordinationCare" %>
<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="https://code.jquery.com/ui/1.11.1/jquery-ui.js"></script>
    <script type="text/javascript" src="../../Scripts/validate.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var date = new Date();
            var date = new Date();
            var dobval = $('#dob').val();
            var dobDate = dobval == '' ? date : new Date(dobval);
            $('#dob').val(dobDate.getMonth() + 1 + "/" + dobDate.getDate() + "/" + dobDate.getFullYear());

            //$('#time').val(formatAMPM(date));
            $('#dob').datepicker();
            $('#dob').click(function () {
                $('#dob').datepicker('show');
            });
            $('#calendar_img').click(function () {
                $('#dob').datepicker('show');
            });
        });

        $("#div_patientsearch").css("display", "block");
        $("#patientsearch_leftpart").css("display", "block");
        $("#imgsearchuser").css("display", "block");


    </script>
</head>

<body>
    <form id="form_CfC" style="font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; font-size: 14px;">
        <div>
            <div class="patsearch_heading">
                Coordination of Care
             <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popupclose" class="pull-right" style="cursor: pointer; margin-top: -5px;"
                 alt="close" />
            </div>
            <div class="patsearch_border">

                <div style="display: -webkit-box; display: inline-flex; margin-top: 20px;">
                    <div style="padding-top: 9px;"><span style="margin-left: 40px; font-weight: bold;">Date</span></div>
                    <div>
                        <asp:TextBox ID="dob" runat="server" CssClass="popup_textbox" ClientIDMode="Static" Style="margin-left: 14px;"></asp:TextBox><br />
                    </div>
                    <div style="padding-top: 8px;">
                        <img src="../../Images/calendar.jpg" id="calendar_img" class="txtbox_spanimg" alt="calendar" style="margin-top: -5px;" />
                    </div>

                    <div style="padding-top: 9px;"><span style="margin-left: 40px; font-weight: bold;">Time</span></div>
                    <div style="margin-left: 0px;">
                        <asp:TextBox ID="time" runat="server" CssClass="popup_textbox" ClientIDMode="Static" Style="width: 170px;"></asp:TextBox><br />
                    </div>

                </div>

                <div style="display: -webkit-box; display: inline-flex; margin-top: 40px;">
                    <div style="padding-top: 9px;"><span style="margin-left: 40px; font-weight: bold;">Enter Care Coordination Notes</span></div>
                    <div style="margin-left: 20px;">
                        <asp:TextBox ID="txtNotes" ClientIDMode="Static" TextMode="multiline" Columns="60" Rows="5" runat="server" />
                    </div>
                </div>

                <div style="margin-left: 30%; margin-top: 20px;">
                    <input type="button" id="save_item" class="btn_standard" value="Save" />
                </div>
            </div>
        </div>
        <input type="hidden" clientidmode="Static" id="asId" name="asId" runat="server" />
        <input type="hidden" clientidmode="Static" id="objid" name="objid" runat="server" />
    </form>
</body>

<script type="text/javascript">
    $("#popupclose").click(function () {
        parent.assesummary_close();
    });

    $('#save_item').click(function () {
        var data, date = $('#dob').val(),
            time = $('#time').val(),
            notes = $('#txtNotes').val(),
            aid = $('#asId').val(),
            objid = $('#objid').val(),
            method = (objid && objid != 0) ? 'EditCoordinationOfCare' : 'AddCoordinationOfCare',
            Id = (objid && objid != 0) ? objid : aid,
            dataOB = { "Id": Id, "Date": date, "Time": time, "Notes": notes };
        $.ajax({
            type: "POST",
            url: "../Services/EncounterService.svc/" + method,
            data: JSON.stringify(dataOB),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.d == "error") {
                    showError({ text: 'Sorry an error has occured. Please contact administrator.' });
                }
                else if (method == 'AddCoordinationOfCare') {
                    $('#objid').val(result.d);
                    showSucess({ text: 'Coordination of Care updated successfully.' });
                }
                else {
                    showSucess({ text: 'Coordination of Care updated successfully.' });
                }
            },
            error: function () {
                showError({ text: 'Sorry an error has occured. Please contact administrator.' });
            }
        });
    });
</script>

</html>
