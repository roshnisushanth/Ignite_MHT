<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Hick.Index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=edge">
 <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Ignite</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css"
        rel="stylesheet" />
    <link href="Theme/content/indexstyle.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>  
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
    <script src="https://code.jquery.com/ui/1.11.1/jquery-ui.js"></script>
    <script type="text/javascript">
        var phight = window.innerHeight - (window.innerHeight / 4);
        var pwidth = window.innerWidth - (window.innerWidth / 4);
        var closepopup;
        function func() {
            $("#invalidprompt").slideUp("slow");
        }
        function showMessage() {
            $("#invalidprompt").slideDown("slow");
        }
    </script>
</head>
<body class="bodystyle">
    <form id="form1" runat="server">
    <div class="container-fluid color">
        <div class="">
            <div class="row ">
                <div class=" col-sm-1 col-md-3 col-lg-3 border">
                </div>
                <div class="col-xs-12 col-sm-10 col-md-6 col-lg-6 border alignPic " style="position: static;">
                    <div class="col-xs-2 col-sm-4 col-md-3 col-lg-3 border">
                    </div>
                    <div class="col-xs-8 col-sm-4 col-md-6 col-lg-6 border">
                        <img src="Theme/images/ignite_logo.png" class="img-responsive imgpadding" />
                    </div>
                    <div class="col-xs-2 col-sm-4 col-md-3 col-lg-3 border">
                    </div>
                </div>
                <div class=" col-sm-1 col-md-3 col-lg-3 border">
                </div>
            </div>
            <div class="rowError" id="invalidprompt" style="display: none;" runat="server">
                <div class="row  rowError ">
                    <div class=" col-sm-1 col-md-3 col-lg-3 border">
                    </div>
                   
                    <div class="col-xs-12 col-sm-10 col-md-6 col-lg-6 border">
                        <!--to fit the size of textboxes-->
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 border">
                        <div class=" img errorPadding ">
                            <div class="errormsg img">
                                <span>
                                    <img src="Theme/images/Index/cross1.jpg" class="crossimg marginLeft " />Invalid Username
                                    or Password</span>
                            </div>
                        </div>
                        </div>
                    </div>
                    <div class=" col-sm-1 col-md-3 col-lg-3 border">
                    </div>
                </div>
            </div>
            <div class="row ">
                <div class="col-xs-2 col-sm-2 col-md-3 col-lg-3 border">
                </div>
                <!--changed mobile columns(xs) to 12-->
                <div class="col-xs-12  col-sm-8 col-md-6 col-lg-6 border">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 border">
                        <asp:PlaceHolder ID="logonPlaceholder" runat="server"></asp:PlaceHolder>
                    </div>
                </div>
                <div class="col-xs-2 col-sm-2 col-md-3 col-lg-3 border">
                </div>
                <!--<div class="  col-xs-2 col-sm-2 col-md-3 col-lg-3 border2 paddingSteth"><div class="footer border1 "><img class="imgsize img-responsive"  src="imgs\stethoscope.jpg"></div> </div>-->
            </div>
        </div>
        <div class="">
            <div class="footerimg">
            <p>
                <span class="footertxt">Powered by </span>
                <img src="Theme/images/Index/Ignite-footer-logo.png" /></p>
            </div>
            <div class="border1">
                <img class="imgsize img-responsive" src="Theme/images/Index/stethoscope.png" />
            </div>
        </div>
      
        <%--<div id="termsandcond_popup" style="display: none;">
            <div id="divcontent" style="z-index: 1000; overflow: auto;">
            </div>
            <div style="text-align: center; margin-top: 10px;">
                <input type="button" id="accept" value="Accept" style="background-color: #FFCB05;
                    width: 80px; height: 30px; border: 0px;" />
                <input type="button" id="cancel" value="Cancel" style="background-color: #FFCB05;
                    width: 80px; height: 30px; border: 0px;" />
            </div>
        </div>--%>
    </div>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtusername").focus();                                  
        });        
    </script>
</body>
</html>


