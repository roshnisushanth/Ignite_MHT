<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCAddEditSocialHistory.ascx.cs"
    Inherits="Hick.PatientLookUp.UserControls.UCAddEditSocialHistory" %>
<style>
    div.patient_search_right_frame
    {
        margin: 5px 0;
    }
    .patsearch_heading.patient
    {
        width: 100%;
        padding-top: 4px;
    }
    .patsearch_heading.patient .btn_standard
    {
        margin: 0px 50px 0 0 !important;
        padding: 5px 10px;
        float: right;
    }
    .patsearch_heading.patient #popup_close
    {
        margin-top: 0 !important;
    }
    .social-history .form-group {
    margin: 0 0 10px 0 !important;
}
.social-history input {
    display: inline-block;
    margin-right: 5px;
}
</style>
<div class="patsearch_heading patient">
    History
    <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popup_close" class="pull-right" style="cursor: pointer;
        margin-top: -42px;" alt="close" />
</div>
<div class="patsearch_border social-history">
    <form class="form-horizontal">
    <div class="form-group">
        <label class="col-sm-5 control-label">
            Do you drink alcoholic beverages?</label>
        <div class="col-sm-7">
            <asp:DropDownList ID="ddlAlcoholic" runat="server" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="ddlAlcoholic_SelectedIndexChanged">
                <asp:ListItem>Select</asp:ListItem>
                <asp:ListItem>Yes</asp:ListItem>
                <asp:ListItem>No</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <div id="divDrink" runat="server" style="display: none;">
        <div class="form-group">
            <label class="col-sm-5 control-label">
                How many drink(s) do you consume every week?</label>
            <div class="col-sm-7">
                <asp:TextBox ID="txtHowmanydrink" class="form-control" SkinID="skinTxt" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-5 control-label">
                How long (in years) have you been drinking Alcoholic beverages?</label>
            <div class="col-sm-7">
                <asp:TextBox ID="txtHowlongDrinking" class="form-control" SkinID="skinTxt" runat="server"></asp:TextBox>Years
            </div>
        </div>
    </div>
    <div class="form-group">
        <label class="col-sm-5 control-label">
            Do you smoke?</label>
        <div class="col-sm-7">
            <asp:DropDownList ID="ddlSmoke" runat="server" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="ddlSmoke_SelectedIndexChanged">
                <asp:ListItem>Select</asp:ListItem>
                <asp:ListItem>Yes</asp:ListItem>
                <asp:ListItem>No</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <div id="divSmoke" runat="server" style="display: none; clear: both">
        <div class="form-group">
            <label class="col-sm-5 control-label">
                How many pack(s) per day?
            </label>
            <div class="col-sm-7">
                <asp:TextBox ID="txtHowmanypack" class="form-control" SkinID="skinTxt" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-5 control-label">
                How long (in years) have you been smoking?</label>
            <div class="col-sm-7">
                <asp:TextBox ID="txtHowlongSmoking" class="form-control" SkinID="skinTxt" runat="server"></asp:TextBox>Years
            </div>
        </div>
    </div>
    <div class="form-group">
        <label class="col-sm-5 control-label">
            Do you exercise?</label>
        <div class="col-sm-7">
            <asp:DropDownList ID="ddlExercise" runat="server" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="ddlExercise_SelectedIndexChanged">
                <asp:ListItem>Select</asp:ListItem>
                <asp:ListItem>Yes</asp:ListItem>
                <asp:ListItem>No</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <div id="divExercise" runat="server" style="display: none;">
        <div class="form-group">
            <label class="col-sm-5 control-label">
                How many times per weeks?</label>
            <div class="col-sm-7">
                <asp:DropDownList ID="ddlHowmanydaysExercise" runat="server" AutoPostBack="true" class="form-control">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>1 to 2 times per week</asp:ListItem>
                    <asp:ListItem>3 times per week</asp:ListItem>
                    <asp:ListItem>4 or more times per week</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="form-group">
        <label class="col-sm-5 control-label">
            BMI</label>
        <div class="col-sm-7">
            <asp:Label ID="lblBMI" runat="server" Width="30px"></asp:Label>
        </div>
    </div>
    </form>
    <div class="clear">
    </div>
    <div align="center">
         <asp:Button ID="btn_save" runat="server" class="btn_standard" Text="Save" 
             onclick="btn_save_Click"  />
    </div>
    <!-- Template ends here -->
</div>

<script type="text/javascript">

    $(document).ready(function () {

        $('#dob').datepicker({ maxDate: new Date() });
        $('#calendar_img').click(function () {
            $('#dob').datepicker('show');
        });

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
