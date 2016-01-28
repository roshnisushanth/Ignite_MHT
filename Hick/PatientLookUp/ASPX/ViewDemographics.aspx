<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewDemographics.aspx.cs" Inherits="Hick.PatientLookUp.ASPX.ViewDemographics" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
            <link href="../../Content/patientlookup.css" rel="stylesheet" />
    <link href="../../Content/style.css" rel="stylesheet" />
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="//code.jquery.com/ui/1.11.1/jquery-ui.js"></script>
    <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
    <script type="text/javascript" src="../../Scripts/pop-script.js"></script>
    <style>
        .first-last .col-md-6.col-sm-6.col-lg-6.col-xs-12 { width: 50%;}
        .col-md-6.col-sm-6.col-lg-6.col-xs-12.first { width: 50%;  float: left;}
        .col-md-6.col-sm-6.col-lg-6.col-xs-12.second { width: 50%;  float: left;}
        .second-last .col-md-6.col-sm-6.col-lg-6.col-xs-12:nth-child(2){padding-left: 0;}
    </style>
</head>
<body>
    <div class="content-php">
    <form id="form1" runat="server">
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
            <asp:TextBox ID="txt_phonenumber" runat="server" class="form-control" style="width:100%!important;"></asp:TextBox>
        </div>
        <div class="form-group addr">
            <label> Address</label>
            <asp:TextBox ID="txt_address1" runat="server" class="form-control"></asp:TextBox>
            <label>City</label>
             <asp:TextBox ID="txt_city" runat="server" class="form-control"></asp:TextBox>
<%--            <label class="addr-label">City</label>--%>
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
    </form>
        </div>
</body>
</html>
