<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testform.aspx.cs" Inherits="Hick.testform" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="css/style.css" rel="stylesheet"/>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.1/js/bootstrap.min.js"></script>
    <script src="https://rawgit.com/wenzhixin/bootstrap-table/master/src/bootstrap-table.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <link href="Content/style.css" rel="stylesheet" />
        <div class="container-fluid " style="background-color:lightyellow;">
    <div class="col-xs-1 col-sm-3 col-md-4" style="height:100%"></div>
    <div class="col-xs-10 col-sm-6 col-md-4" style="height:100%">

        <div class="center-align vertical-center">
            <div>
                <div style="width:100%;">
                    <img src="images/hick_logo.png" style=" width:100px" />
                    <div class="toppadding">
                        <span style="width:100%">User Name</span>
                        <input id="txtusername" type="text" class="form-control " style="width:200px" runat="server">
                        <div class="toppadding nav nav-pills">
                            <!-- <li role="presentation" class="active" id="btnlogin"><a style="background-color: #B35714;">Login</a></li> -->
                           <asp:Button id="btnlogin"  class="btn btn-default" runat="server" style="background-color: #B35714;" OnClick="btn_click" Text="Connect"></asp:Button>
                            <%--//<button id="btnlogin" type="button" class="btn btn-default" style="background-color: #B35714;" onclick="btn_click">Connect</button>--%>
                        </div>
                    </div>
                </div>

            </div>
            <div class=" col-xs-1 col-sm-3 col-md-4 " style="height:100%">


            </div>


        </div>

        </div>
    </div>
    </div>
    </form>
</body>
</html>
