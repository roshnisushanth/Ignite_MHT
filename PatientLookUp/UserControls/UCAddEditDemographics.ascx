<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCAddEditDemographics.ascx.cs"
    Inherits="Hick.PatientLookUp.UserControls.UCAddEditDemographics" %>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
<link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
<style>
    div.patient_search_right_frame {
        margin: 5px 0;
    }

    .patsearch_heading.patient {
        width: 100%;
        padding-top: 4px;
    }

        .patsearch_heading.patient .btn_standard {
            margin: 0px 50px 0 0 !important;
            padding: 5px 10px;
            float: right;
        }

        .patsearch_heading.patient #popup_close {
            margin-top: 0 !important;
        }

    .form-group.dob label {
        width: 100%;
    }
</style>
<div class="patsearch_heading patient">
    Demographics
    <img src='../../Images/popup_close.png' id="popup_close" class="pull-right" style="cursor: pointer; margin-top: -42px;"
        alt="close" />
</div>
<div class="patsearch_border demograph">
    <div class="col-md-6 col-sm-6 col-lg-6 col-xs-12 first">
        <div class="first-last clearfix">
            <div class="col-md-6 col-sm-6 col-lg-6 col-xs-12">
                <div class="form-group">
                    <label>
                        First Name</label>
                    <asp:TextBox ID="txt_firstname" runat="server" class="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-6 col-sm-6 col-lg-6 col-xs-12">
                <div class="form-group">
                    <label>
                        Last Name</label>
                    <asp:TextBox ID="txt_lastname" runat="server" class="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="form-group">
            <label>
                Phone Number</label>
            <asp:TextBox ID="txt_phonenumber" MaxLength="10" ForeColor="Black" Width="320px" class="form-control" runat="server" ClientIDMode="Static"
                onblur="javascript:validatePhone(document.getElementById('txt_phonenumber'), document.getElementById('ddlFormat').options[document.getElementById('ddlFormat').selectedIndex].value,document.getElementById('refvtxthomephone'));"
                onfocus="javascript:validatePhonefocus(document.getElementById('txt_phonenumber'), document.getElementById('ddlFormat').options[document.getElementById('ddlFormat').selectedIndex].value,document.getElementById('refvtxthomephone'));"></asp:TextBox>
            <select id="ddlFormat" style="display: none;" name="ddlFormat">
                <option value="0">(xxx)xxx-xxxx</option>
            </select>
            <asp:RegularExpressionValidator SetFocusOnError="true" Style="float: left; margin-left: 245px;" ClientIDMode="Static"
                                                                    ID="refvtxthomephone" runat="server" ErrorMessage="Please enter 10 digits." ControlToValidate="txt_phonenumber"
                                                                    CssClass="error_classcreate" ValidationExpression="^\([0-9]{3}\)[0-9]{3}-[0-9]{4}$"
                                                                    Display="Dynamic" ValidationGroup="PrimaryEmergencycontactgroup"></asp:RegularExpressionValidator>
            <%--  <asp:TextBox ID="txt_phonenumber" runat="server" class="form-control"></asp:TextBox>--%>
        </div>
        <div class="form-group addr">
            <label>Address</label>
            <asp:TextBox ID="txt_address1" runat="server" class="form-control"></asp:TextBox>
            <%--<label class="addr-label">Address 1</label>--%>
            <asp:TextBox ID="txt_zipcode" runat="server" class="form-control" style="display:none"></asp:TextBox>
            <label class="addr-label" style="display:none">Zipcode</label>
        </div>
    </div>
    <div class="col-md-6 col-sm-6 col-lg-6 col-xs-12 second">
        <div class="form-group dob">
            <label>
                Date of Birth</label>
            <asp:TextBox ID="txt_dob" runat="server" class="popup_textbox form-control" Style="margin-left: 0;"></asp:TextBox>
            <img src="../../Images/calendar.jpg" id="calendar_img" class="txtbox_spanimg" alt="calendar" />
        </div>
        <div class="second-last">
            <div class="col-md-6 col-sm-6 col-lg-6 col-xs-12">
                <div class="form-group">
                    <label>
                        HICN</label>
                    <asp:TextBox ID="txt_hicn" runat="server" class="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-6 col-sm-6 col-lg-6 col-xs-12">
                <div class="form-group">
                    <label>
                        SSN</label>
                    <asp:TextBox ID="txt_SSN" runat="server" class="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="form-group addr">
            <label>&nbsp;</label>
                        <label>City</label>
             <asp:TextBox ID="txt_city" runat="server" class="form-control"></asp:TextBox>
            <asp:TextBox ID="txt_address2" runat="server" class="form-control" style="display:none"></asp:TextBox>
            <label class="addr-label" style="display:none">Address 2</label>
            <asp:DropDownList ID="ddlState" runat="server" class="form-control" style="display:none">
                <asp:ListItem Selected="True">Select</asp:ListItem>
                <asp:ListItem Value="Alabama">Alabama</asp:ListItem>
                <asp:ListItem Value="Alaska">Alaska</asp:ListItem>
                <asp:ListItem Value="Arizona">Arizona</asp:ListItem>
                <asp:ListItem Value="Arkansas">Arkansas</asp:ListItem>
                <asp:ListItem Value="California">California</asp:ListItem>
                <asp:ListItem Value="Colorado">Colorado</asp:ListItem>
                <asp:ListItem Value="Connecticut">Connecticut</asp:ListItem>
                <asp:ListItem Value="Delaware">Delaware</asp:ListItem>
                <asp:ListItem Value="District of Columbia">District of Columbia</asp:ListItem>
                <asp:ListItem Value="Florida">Florida</asp:ListItem>
                <asp:ListItem Value="Georgia">Georgia</asp:ListItem>
                <asp:ListItem Value="Hawaii">Hawaii</asp:ListItem>
                <asp:ListItem Value="Idaho">Idaho</asp:ListItem>
                <asp:ListItem Value="Illinois">Illinois</asp:ListItem>
                <asp:ListItem Value="Indiana">Indiana</asp:ListItem>
                <asp:ListItem Value="Iowa">Iowa</asp:ListItem>
                <asp:ListItem Value="Kansas">Kansas</asp:ListItem>
                <asp:ListItem Value="Kentucky">Kentucky</asp:ListItem>
                <asp:ListItem Value="Louisiana">Louisiana</asp:ListItem>
                <asp:ListItem Value="Maine">Maine</asp:ListItem>
                <asp:ListItem Value="Maryland">Maryland</asp:ListItem>
                <asp:ListItem Value="Massachusetts">Massachusetts</asp:ListItem>
                <asp:ListItem Value="Michigan">Michigan</asp:ListItem>
                <asp:ListItem Value="Minnesota">Minnesota</asp:ListItem>
                <asp:ListItem Value="Mississippi">Mississippi</asp:ListItem>
                <asp:ListItem Value="Missouri">Missouri</asp:ListItem>
                <asp:ListItem Value="Montana">Montana</asp:ListItem>
                <asp:ListItem Value="Nebraska">Nebraska</asp:ListItem>
                <asp:ListItem Value="Nevada">Nevada</asp:ListItem>
                <asp:ListItem Value="New Hampshire">New Hampshire</asp:ListItem>
                <asp:ListItem Value="New Jersey">New Jersey</asp:ListItem>
                <asp:ListItem Value="New Mexico">New Mexico</asp:ListItem>
                <asp:ListItem Value="New York">New York</asp:ListItem>
                <asp:ListItem Value="North Carolina">North Carolina</asp:ListItem>
                <asp:ListItem Value="North Dakota">North Dakota</asp:ListItem>
                <asp:ListItem Value="Ohio">Ohio</asp:ListItem>
                <asp:ListItem Value="Oklahoma">Oklahoma</asp:ListItem>
                <asp:ListItem Value="Oregon">Oregon</asp:ListItem>
                <asp:ListItem Value="Puerto Rico">Puerto Rico</asp:ListItem>
                <asp:ListItem Value="Pennsylvania">Pennsylvania</asp:ListItem>
                <asp:ListItem Value="Rhode Island">Rhode Island</asp:ListItem>
                <asp:ListItem Value="South Carolina">South Carolina</asp:ListItem>
                <asp:ListItem Value="South Dakota">South Dakota</asp:ListItem>
                <asp:ListItem Value="Tennessee">Tennessee</asp:ListItem>
                <asp:ListItem Value="Texas">Texas</asp:ListItem>
                <asp:ListItem Value="Utah">Utah</asp:ListItem>
                <asp:ListItem Value="Vermont">Vermont</asp:ListItem>
                <asp:ListItem Value="Virginia">Virginia</asp:ListItem>
                <asp:ListItem Value="Washington">Washington</asp:ListItem>
                <asp:ListItem Value="West Virginia">West Virginia</asp:ListItem>
                <asp:ListItem Value="Wisconsin">Wisconsin</asp:ListItem>
                <asp:ListItem Value="Wyoming">Wyoming</asp:ListItem>
            </asp:DropDownList>
            <label class="addr-label" style="display:none">State</label>
        </div>
    </div>
    <div class="clear">
    </div>
    <div align="center">
        <asp:Button ID="btn_save" runat="server" class="btn_standard" Text="Save" OnClick="btn_save_Click" />
    </div>
    <div align="center">
        <asp:Label runat="server" ID="lbl_msg" ForeColor="Green"></asp:Label>
    </div>
    <!-- Template ends here -->
</div>
<script type="text/javascript">
    $(function () {

        $("[id$=txt_dob]").datepicker({ maxDate: new Date() });
        $('#calendar_img').click(function () {
            $("[id$=txt_dob]").datepicker('show');
        });

    });
</script>
<script type="text/javascript">

    $(document).ready(function () {


        $("#div_patientsearch").css("display", "block");
        $("#patientsearch_leftpart").css("display", "block");
        $("#imgsearchuser").css("display", "block");
        $("#popupclose").click(function () {

            $("#divshowconditions").dialog('close');
        });

        $(".delete_conditions").click(function () {

            $('.Content').text("Delete");
            var uid = $(this).attr("cid");

            var data = "{ ConditionID: '" + uid + "'}";

            $.ajax({
                type: "POST",
                url: "Conditions.aspx/DeleteConditions",
                data: data,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                mode: "queue",
                success: function (msg) {
                    location.reload();
                },
                error: function (xmlhttprequest, textstatus, errorThrown) {
                    alert(xmlhttprequest.responseText);
                }
            });
        });

        $(".edit_conditions").click(function () {
            $('.Content').text("Edit");

            var uid = $(this).attr("cid");
            var _desc = $(this).closest("tr").find("[class~=lbldesc]").html();
            var _icd9 = $(this).closest("tr").find("[class~=lblicd9code]").html();
            var _dos = $(this).closest("tr").find("[class~=lbldos]").html();
            var _conditioncheck = $(this).closest("tr").find("[class~=spnconditioncheck]").html();
            var _history = $(this).closest("tr").find("[class~=spnhistory]").html();

            $.ajax({
                type: "POST",
                url: "Conditions.aspx/CacheDetails",
                data: '{"description":"' + _desc + '","icd9code":"' + _icd9 + '","activesince":"' + _dos + '","conditioncheck":"' + _conditioncheck + '","inactivesince":"' + _history + '"}',
                contentType: "application/json; charset=utf-8",
                success: function (msg) {

                },
                error: function (a) {

                },
                complete: function () {
                    $("#frmeditconditions_popup").attr("src", "");
                    $("#frmeditconditions_popup").attr("src", "EditConditions.aspx?cid=" + uid);
                    showpopup();
                }
            });
        });
        $("#add_conditions").click(function () {
            $('.Content').text("Add New");

            // $('<span>Add New</span>').appendTo('.Content');

            // $('<span>Add New</span>').appendTo('.Content');

            $(".edit_conditionsdiv").css("display", "block");

            $("#frmeditconditions_popup").attr("src", "");
            $("#frmeditconditions_popup").attr("src", "EditConditions.aspx");
            showpopup();
        });
    });
    function validatePhone(phoneField, format, regexp) {

        var num = phoneField.value.replace(/[^\d]/g, '');
        if (num.length != 10) {
            ValidatorEnable(regexp, true);
        }
        else {
            switch (format) {
                case '0': //Format (xxx)-xxx-xxxx
                    phoneField.value = "(" + num.substring(0, 3) + ")" + num.substring(3, 6) + "-" + num.substring(6);
                    phoneField.maxLength = 13;
                    phoneField.blur();
                    ValidatorEnable(regexp, false);
                    break;
            }
        }
    }
    function validatePhonefocus(phoneField, format, regexp) {
       
        var num = phoneField.value.replace(/[^\d]/g, '');
        if (num.length != 10) {
            phoneField.value = num;
        } else {

            switch (format) {
                case '0': //Format (xxx)-xxx-xxxx
                    phoneField.value = num.substring(0, 3) + num.substring(3, 6) + num.substring(6);
                    phoneField.maxLength = 10;
                    break;
            }

        }
    }
    function showpopup() {
        var popuphight = window.innerHeight - 200;
        var popupwidth = window.innerWidth - 490;

        $("#divshowconditions").dialog(
            {
                modal: true,
                height: popuphight,
                width: popupwidth,
                resizable: false,
                //title: "Patient Search",
                create: function () {
                    $(".ui-dialog-titlebar").hide();
                    $(".ui-dialog-content").css("padding", "0px");
                }
            });
    }
    function ClosePopup() {

        $("#divshowconditions").dialog('close');

    }
    $("#popup_close").click(function () {
        parent.popup_close();
    });
</script>
