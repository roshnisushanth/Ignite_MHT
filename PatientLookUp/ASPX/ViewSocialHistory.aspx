<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewSocialHistory.aspx.cs" Inherits="Hick.PatientLookUp.ASPX.ViewSocialHistory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="~/Content/patientlookup.css" rel="stylesheet" />
    <link href="~/Content/style.css" rel="stylesheet" />

    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="//code.jquery.com/ui/1.11.1/jquery-ui.js"></script>    
        <script type="text/javascript" src="../../Scripts/pop-script.js"></script>
    <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css"/>
</head>
<body>
    <form runat="server">
        <div class="content-php">
    <form class="form-horizontal" >
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
                <asp:TextBox ID="txtHowlongDrinking" class="form-control" SkinID="skinTxt" runat="server"></asp:TextBox>
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

        </form>
    </div>

    
<script type="text/javascript">

    $(document).ready(function () {
        $(document).ready(function () {
            $('#dob').datepicker({ maxDate: new Date() });
            $('#calendar_img').click(function () {
                $('#dob').datepicker('show');
            });
        });
    });
</script>
</body>
</html>
