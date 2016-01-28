<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="Hick.Chat" %>

<%@ Register Src="~/Shared/UCCallToActionBar.ascx" TagName="ActionBar" TagPrefix="UC" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Ignite</title>
    <!-- Bootstrap -->
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css"
        rel="stylesheet">
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
    <!-- Bootstrap -->
    <%--<link
	href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css"
	rel="stylesheet">--%>
    <link rel="stylesheet" href="https://rawgit.com/wenzhixin/bootstrap-table/master/src/bootstrap-table.css">
    <%--<link
	href="http://cdn-na.infragistics.com/igniteui/2014.2/latest/css/structure/infragistics.css"
	rel="stylesheet" />--%>
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script/Hick_Live/Images/icon_details.png>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
    <script src="https://code.jquery.com/ui/1.11.2/jquery-ui.js"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.1/js/bootstrap.min.js"></script>
    <script src="https://rawgit.com/wenzhixin/bootstrap-table/master/src/bootstrap-table.js"></script>
    <!--<script
	src="http://cdn-na.infragistics.com/igniteui/2014.2/latest/js/infragistics.core.js"></script>
<script
	src="http://cdn-na.infragistics.com/igniteui/2014.2/latest/js/infragistics.lob.js"></script>-->
    <!--smilies-->
    <!-- <script type="text/javascript" src="jquery-1.8.0.min.js"></script> -->
    <link href="Content/emoticons.css" rel="stylesheet" />
    <link href="Content/style.css" rel="stylesheet" />
    <%--<script type="text/javascript">

        var phight = window.innerHeight - (window.innerHeight / 4);
        var pwidth = window.innerWidth - (window.innerWidth / 4);
        var closepopup;

        function popup() {
            $("#divcontent1").html("");
            $("#divcontent1").load("TermsConditions.html");


            $("#termsandcond_popup").css("display", "block");

            $("#termsandcond_popup").dialog({
                modal: false,
                height: phight,
                width: pwidth,
                resizable: false,
                //title: "Terms and Conditions",
                create: function () {
                    //$("#tblexport").attr("cellpadding", "10");

                    $("#divcontent1").css("height", phight - 70);
                    $(".ui-dialog-titlebar-close").css("display", "none");
                    $(".ui-dialog-titlebar").css("display", "none");
                    $(".ui-dialog").css("z-index", "1000");

                }
            });
           
        };
    </script>--%>
    <style>
        div#divcontrols {
            float: inherit !important;
            text-align: center;
        }

        .btnclearchat {
            font-weight: bold;
        }

        .btn_standard {
            background-color: #FFCB05;
            width: auto;
            height: 30px;
            min-width: 120px;
            font-size: 14px;
            font-weight: 600 !important;
            border: 0px;
            color: #000;
            padding: 5px;
            margin: 10px;
        }
    </style>

   

</head>
<body>
    <span id="templates" data-url="templates.html" style="display: none"></span>
    <div style="display: none;">
        <input type="file" id="fileUpload" name="fileUpload" />
        <button id="btnuploadfiles" onclick="uploadfiles()">
            Upload</button>
    </div>
    <div id="diviframe">
    </div>
    <%--<form runat="server">           
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server" ></asp:ScriptManager>--%>
    <%--</form>--%>
    <%--<script src="Scripts/infragistics.core.js"></script>--%>
    <script src="Scripts/emoticons.js"></script>
    <script src="Scripts/jquery.jqote2.js"></script>
    <script src="Scripts/jquery.ajaxfileupload.js"></script>
    <div class="container-fluid" id="fluid">
        <span style="display: none;" id="hdnusertype" runat="server"></span>
        <div id="divcontactsloader" class="fullpageloader">
        </div>
        <div class="list" id="leftside" style="padding-right: 5px; padding-left: 2px;">
            <div class="header affix-top" id="header">
                <nav class="navbar navbar-default toolbar" role="toolbar">
                    <%--    <a href="#" class="btn btn-default btn-link listTabletOff" id="home"role="button">Home</a>--%>
                    <a href="#" class="btn btn-default btn-link" id="home" role="button">Contacts</a>
                    <%--  <a href="#" class="btn btn-default btn-link" role="button">Conversation</a>--%>
                    <a class="btn btn-default btn-link listTabletOff" role="button" id="chatlog1">Chat Log </a>
                    <a class="btn btn-default btn-link listTabletOff" role="button" id="auditlog">Audit Log </a>
                    <%-- <a href="#" class="btn btn-default btn-link listTabletOff" role="button">Tools</a>
                    <a href="#" class="btn btn-default btn-link listTabletOff" role="button">Video Call</a>                   
                    <a href="#" class="btn btn-default btn-link listTabletOff" role="button">Help</a>--%>
                    <a id="signout" class="btn btn-default btn-link listTabletOff" runat="server" role="button">Sign out</a>
                    <a class="btn btn-default btn-link dropdown-toggle dropdownon" data-toggle="dropdown" aria-expanded="false" id="extension" role="button">....</a>
                    <ul class="dropdown-menu" role="menu">
                        <!--COMMENTING UNWANTED SUBMENUS-->
                        <!--<li><a href="#" class="btn btn-default btn-link" role="button">Home</a></li>
                        <li><a href="#" class="btn btn-default btn-link" role="button">Contacts</a></li>
                        <li><a href="#" class="btn btn-default btn-link" role="button">Conversation</a></li>
                        <li><a href="#" class="btn btn-default btn-link" role="button">Tools</a></li>
                        <li><a href="#" class="btn btn-default btn-link" role="button">Video Call</a></li>
                        <li><a href="#" class="btn btn-default btn-link" role="button">Help</a></li>-->
                        <li><a class="btn btn-default btn-link" role="button" id="chatlog">Chat Log </a></li>
                        <li><a id="signout1" class="btn btn-default btn-link" role="button">Sign out</a></li>
                    </ul>
                    <img id="closebtn" style="padding-top: 2px; padding-bottom: 2px; float: right;"
                        src="images/close_btn.png" data-toggle="popover">
                </nav>
                <div class="panel panel-default  chat-info toolbar2">
                    <div class="panel-body toolbar2 ">
                        <div style="padding-top: 3px;">
                            <div style="float: right;">
                                <img src="images/option_selected.png" style="float: right;">
                                <span id="loginname" runat="server" style="float: right; font-size: 14px;"></span>
                                <span id="Userid" runat="server" style="display: none"></span>
                                <div id="divlastlogin" style="padding-top: 20px; font-size: 11px; display: none;"
                                    class="mview">
                                    <span>Last log in:</span> <span runat="server" name="spnlogedindate" id="datetimepicker"></span>
                                </div>
                            </div>
                        </div>
                        <!-- contact list dropdown -->
                        <div id="divcontactmenu" class="dropdown chatdisplay" style="float: left; padding-top: 8px">
                            <button class="btn btn-default dropdown-toggle" style="height: 30px; background-color: lavenderblush;"
                                type="button" id="dropdownMenu1" data-toggle="dropdown" aria-expanded="true">
                                <span class="glyphicon glyphicon-chevron-down"></span>
                            </button>
                            <div class="dropdown-menu" role="list" aria-labelledby="dropdownMenu1" style="width: 250px;">
                                <div class="form-group has-feedback" id="divsearch">
                                    <input type="text" class="form-control txtsearchbox" placeholder="Search" style="width: 100%;" />
                                    <i class="glyphicon glyphicon-search form-control-feedback"></i>
                                </div>
                                <div class="dataTables_wrapper dt-bootstrap footer tablediv" style="overflow-y: auto; max-height: 500px">
                                    <table id="table" data-show-header="false" data-search="true" class="table-responsive table-hover"
                                        style="width: 100%;">
                                        <thead>
                                        </thead>
                                        <tbody>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <!-- Menu dropdown -->
                        <div class="dropdownon dropdown" style="padding-top: 6px; float: left; padding-left: 5px;">
                            <button class="btn btn-default dropdown-toggle" type="button" id="dropdownMenu1"
                                data-toggle="dropdown" aria-expanded="true">
                                <span class="glyphicon glyphicon-list"></span>
                            </button>
                            <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu1">
                                <li role="presentation" class="limessage"><a role="menuitem" tabindex="-1" href="#">
                                    <img src="images/message-1.png" />chat</a> </li>
                                <li role="presentation" class="liaudiocall"><a role="menuitem" tabindex="-1" href="#">
                                    <img style="width: 25px;" src="images/call-outgoing-icon.png" alt="" title="Initiate Audio Call" />
                                    Audio Call</a></li>
                                <li role="presentation"><a role="menuitem" tabindex="-1" href="#">
                                    <img src="images/web_camera.png" title="Initiate Video Call" />web cam</a></li>
                                <li role="presentation"><a class="lnkadduser" role="menuitem" tabindex="-1" href="#">
                                    <img src="images/add-user.png" title="Add User" />
                                    Add user</a></li>
                                <li role="presentation"><a role="menuitem" tabindex="-1" href="#">
                                    <img style="width: 25px;" class="js-commandcenter" src="images/command_center_icon.png" title="Command Center" />
                                    Command Center</a></li>



                            </ul>
                        </div>
                        <div style="float: left">
                            <nav class="navbar navbar-default toolbar2" role="toolbar" style="border-color: transparent;">
                                <%--  <img src="images/hick_logo.png" />--%>
                                <img src="Theme/images/ignite_logo_small.png" />
                                <img src="images/message-1.png" class="listTabletOff imgmessage" style="padding-left: 8px; height: 25px; cursor: pointer;" />
                                <img src="images/call-outgoing-icon.png" alt="" title="Initiate Audio Call" class="listTabletOff imgaudiocall" style="padding-left: 8px; height: 25px; cursor: pointer;" /><a id="videochat" style="cursor: pointer;"><img src="images/web_camera.png" class="listTabletOff" style="padding-left: 8px; height: 25px;" title="Initiate Video Call" />
                                </a>
                                <img src="images/add-user.png" class="listTabletOff lnkadduser" style="padding-left: 8px; height: 25px; cursor: pointer;" title="Add User" />
                                <img src="images/command_center_icon.png" title="Command Center" class="listTabletOff js-commandcenter" style="cursor: pointer; padding-left: 8px; height: 25px;" />
                                <img src='<%=Page.ResolveUrl("~/Images/patient_myrecord.png") %>' style="cursor: pointer; height: 25px;" id="tblmyrecord" title="My Record" alt="MR" />

                            </nav>
                        </div>
                    </div>
                </div>

                <!--Search btn -->
                <div id="searchtag" class="form-group has-feedback" style="padding-left: 5px; padding-right: 2px;">
                    <input type="text" class="form-control txtsearchbox" placeholder="Search" style="width: 100%;" />
                    <i class="glyphicon glyphicon-search form-control-feedback"></i>
                </div>
            </div>
            <!--Header Ends Here  -->
            <div id="page_loader" style="width: 5%; left: 50%; right: 50%; top: 50%; position: absolute;">
                <img src="Images/ajax-loader.gif" />
            </div>

            <div id="userlist" style="overflow-y: auto;">
                <div class="dataTables_wrapper  dt-bootstrap footer tablediv" id="divcontacttable">
                    <table id="table" data-show-header="false" data-search="true" class="table-responsive table-hover"
                        style="width: 100%; height: 100%">
                        <thead>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                    <table data-show-header="false" data-search="true" class="table-responsive table-hover"
                        style="width: 100%; height: 100%">
                        <thead>
                        </thead>
                        <tbody>
                            <tr>
                                <td align="left" style="height: 40px;">
                                    <a href="#" style="color: Blue;" id="morecontacts">Show More Contacts</a>
                                </td>
                            </tr>
                        </tbody>
                    </table>

                    <%-- <table data-show-header="false" data-search="true" class="table-responsive" 
>>>>>>> .r81
                        style="width: 100%; height: 100%">
                        <thead>
                        </thead>
                        <tbody></tbody>
                        <tr>
                            <td align="left" style="height: 60px;">
                                <div class="listFooter listFooter1 " id="groupfavicon" style="margin-left: -65px;">
                                    <footer class="fotter pull-left">
                                        <a style="cursor: pointer;" id="imggroups">
                                            <img id="groupChat" src="images/group-chat_icon.png"></a>
                                        <a style="cursor: pointer;" id="imgfavusers">
                                            <img src="images/group-fav_icon.png"></a>
                                    </footer>
                                </div>
                            </td>
                        </tr>
                        </tbody>
                    </table>--%>
                </div>
            </div>
            <!-- Users Table ends here  -->
        </div>
        <!-- Footer
			<div class="listFooter">
				<footer class="fotter pull-right">
					<img src="images/group-chat_icon.png"> <img
						src="images/group-starred_icon.png"> <img
						src="images/group-fav_icon.png">
				</footer>
			</div> -->
        <!-- Right hand side -->

        <div id="showleft" style="display: none;">
            <a id="leftdisplay" class="btn btn-default btn-link" role="button">Show Users</a>
        </div>
        <div class="conversation chatdisplay " id="rightside">
            <div class="chatHeader affix-top">

                <%-- Tool Bar--%>
                <UC:ActionBar runat="server" />

                <div class="panel panel-default  chat-info">
                    <div class="panel-body">
                        <div class="chat-photo">
                            <img id="usericon" src="images/user-active.png" style="height: 80px; padding-top: 5px;" />
                        </div>
                        <div class="chat-status">
                            <div style="margin-top: 2px; margin-bottom: 2px;">
                                <span id="username" runat="server"></span>
                                <span id="UserRefId" runat="server" style="display: none;"></span>
                                <span id="videoduration" style="float: right;"></span>
                                <span id="txtchatduration" style="float: right;"></span>
                            </div>
                            <div style="margin-top: 2px; min-height: 20px;">
                                <img src="images/option_selected.png" id="userstatus" />
                                <span id="statusdiv">Online</span>
                            </div>
                            <div>
                                <span>Please do NOT use in urgent or emergency situations, please call 911 or (844)
                                    MED-HOME</span>
                            </div>
                            <div>
                                <div class="pull-left">
                                    <a>
                                        <img id="imgsendimage" src="Images/portrait-icon.png" style="cursor: pointer;" title="Send Pictures" /></a>
                                    <img id="imgsendfiles" src="images/folder-icon.png" style="cursor: pointer;" title="Send Documents" />
                                    <%--  <img id="btnclearlog"	src="images/Actions-edit-clear-icon.png" style="cursor:pointer;" title="Clear Chat" /> --%>
                                    <%--<img src="images/drafts-icon.png" />--%>
                                </div>
                                <div class="pull-right">
                                    <%--<img src="images/prev.png" />
                                    <img src="images/next.png" />--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="conversationContainer" style="overflow-y: auto;">
                <div class="textChatContainer" id="textContainer">
                    <img id="imgmessageloader" src="Images/ajax-loader.gif" class="messageloader" />
                    <div class="chatFooter">
                        <div class="input-group">
                            <div id="input_container" class="inputcontainer">
                                <input type="text" class="form-control" id="InputMessage" autocomplete="off" placeholder="enter your message"
                                    style="font-size: 13px; width: 100%; border: none;">
                                <img src="images/smileicon.png" id="input_img" style="padding-top: 2px; height: 34px; position: absolute; right: 85px; z-index: 100; cursor: pointer;">
                            </div>
                            <!--  	<input id="InputMessage" type="text" class="form-control"
									placeholder="enter your message" style="font-size:13px;"> -->
                            <div class="input-group-btn" style="padding-left: 5px;">
                                <button id="send" class="btn btn-default sendButton">
                                    SEND</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="videoChatContainer" id="videoChatContainer" style="display: none;">
                    <div class="videoContainer">
                        <%--<button id="endcall" class="btn btn-default sendButton" style="margin-left:400px;">End Call</button>--%>
                        <%--	<h1>Local video</h1>--%>
                        <div class="local-video-wrap">
                            <div id="divlogedinuser" class="local-video-wrap">
                            </div>
                            <div class="videocurrentuser">
                                <span id="videocurrentuser" style="margin-left: 50px;"></span>
<%--                                <%if (userType.ToLower() != "patient")
                                    {%>
                                <button type="button" id="endcall" class="phone-pop" data-toggle="modal" data-target="#myModal">
                                    <img src="Images/endcl.jpg" role="button" style="float: right; height: 30px; cursor: pointer;" />
                                </button>
                                <%}
                                    else
                                    {%>
                                <img src="Images/endcl.jpg" id="endcall" role="button" style="float: right; height: 30px; cursor: pointer;" />
                                <%}%>--%>
                                 <img src="Images/endcl.jpg" id="endcall" role="button" style="float: right; height: 30px; cursor: pointer;" />
                            </div>
                        </div>
                        <%--   <h1>Remote  video</h1>--%>
                        <div class="remote-video-wrap">
                            <div id="divpeeruser" class="remote-video-wrap">
                            </div>
                            <div id="divacceptbtn" style="display: none; height: 230px; margin-left: 200px;">
                                <input type="button" id="btnreceivevideo" value="Accept" />
                            </div>
                            <div class="videoremoteuser">
                                <span id="videoremoteuser" style="margin-left: 50px;"></span>
                            </div>
                        </div>
                        <%--frm abshk--%>
                        <br />
                        <div class="remote-video-wrap" id="thrdVidUsr">
                            <div id="thirdVideoUser" class="remote-video-wrap" style="display: block; background-color: Black;">
                            </div>
                            <div id="div2" style="display: none; height: 230px; margin-left: 200px;">
                                <input type="button" id="Button1" value="Accept" />
                            </div>
                            <div class="videoremoteuser" style="margin-top: 56%">
                                <span id="videoremoteuser2" style="margin-left: 50px;"></span>
                            </div>
                        </div>
                        <%--frm abshk end--%>
                    </div>
                </div>
                <div id="divdialpad" style="display: none;">
                </div>
                 <script src="scripts/moment.js"></script>
                <script src="Scripts/Chat.js" type="text/javascript"></script>
                <!--	 </div> -->
                <div id="emoticonContainer" class="emoticonscontainer" style="margin-right: 0%; z-index: 100*; /*top: -15px; */">
                    <div style="width: 100%; display: flex; height: 20%; margin-left: 5px;">
                        <div style="width: 20%;" id="smile">
                            <img src="images/smilies/smile_30.png" class="smilyicon" style="padding-top: 2px; height: 30px; position: absolute; z-index: 200; cursor: pointer;"
                                value=":-)"
                                alt="Smile" />
                        </div>
                        <div style="width: 20%;">
                            <img src="images/smilies/sadsmile_30.png" class="smilyicon" style="padding-top: 2px; height: 30px; position: absolute; z-index: 200; cursor: pointer;"
                                alt="Sad" value=":-(" />
                        </div>
                        <div style="width: 20%;">
                            <img src="images/smilies/bigsmile_30.png" class="smilyicon" style="padding-top: 2px; height: 30px; position: absolute; z-index: 200; cursor: pointer;"
                                alt="lol" value=":-D" />
                        </div>
                        <div style="width: 20%;">
                            <img src="images/smilies/cool_30.png" class="smilyicon" style="padding-top: 2px; height: 30px; position: absolute; z-index: 200; cursor: pointer;"
                                alt="Cool"
                                value="8-|" />
                        </div>
                        <div style="width: 20%;">
                            <img src="images/smilies/surprised_30.png" class="smilyicon" style="padding-top: 2px; height: 30px; position: absolute; z-index: 200; cursor: pointer;"
                                alt="Surprised"
                                value=":-O" />
                        </div>
                    </div>
                    <div style="width: 100%; display: flex; height: 20%; margin-left: 5px;">
                        <div style="width: 20%;">
                            <img src="images/smilies/wink_30.png" class="smilyicon" style="padding-top: 2px; height: 30px; position: absolute; z-index: 200; cursor: pointer;"
                                alt="Wink"
                                value=";-)" />
                        </div>
                        <div style="width: 20%;">
                            <img src="images/smilies/crying_30.png" class="smilyicon" style="padding-top: 2px; height: 30px; position: absolute; z-index: 200; cursor: pointer;"
                                alt="Crying"
                                value=":'-(" />
                        </div>
                        <div style="width: 20%;">
                            <img src="images/smilies/sweating_30.png" class="smilyicon" style="padding-top: 2px; height: 30px; position: absolute; z-index: 200; cursor: pointer;"
                                alt="Sweat"
                                value="(sweat)" />
                        </div>
                        <div style="width: 20%;">
                            <img src="images/smilies/speechless_30.png" class="smilyicon" style="padding-top: 2px; height: 30px; position: absolute; z-index: 200; cursor: pointer;"
                                alt="Speechless"
                                value=":-|" />
                        </div>
                        <div style="width: 20%;">
                            <img src="images/smilies/thinking_30.png" class="smilyicon" style="padding-top: 2px; height: 30px; position: absolute; z-index: 200; cursor: pointer;"
                                alt="Thinking"
                                value=":-s" />
                        </div>
                    </div>
                    <div style="width: 100%; display: flex; height: 20%; margin-left: 5px;">
                        <div style="width: 20%;">
                            <img src="images/smilies/devil_30.png" class="smilyicon" style="padding-top: 2px; height: 30px; position: absolute; z-index: 200; cursor: pointer;"
                                value="3:-)"
                                alt="Devil" />
                        </div>
                        <div style="width: 20%;">
                            <img src="images/smilies/heart_30.png" class="smilyicon" style="padding-top: 2px; height: 30px; position: absolute; z-index: 200; cursor: pointer;"
                                value="<3"
                                alt="Heart" />
                        </div>
                        <div style="width: 20%;">
                            <img src="images/smilies/inlove_30.png" class="smilyicon" style="padding-top: 2px; height: 30px; position: absolute; z-index: 200; cursor: pointer;"
                                value="(in-love)"
                                alt="In Love" />
                        </div>
                        <div style="width: 20%;">
                            <img src="images/smilies/evilgrin_30.png" class="smilyicon" style="padding-top: 2px; height: 30px; position: absolute; z-index: 200; cursor: pointer;"
                                value="(evilgrin)"
                                alt="evil-grin" />
                        </div>
                        <div style="width: 20%;">
                            <img src="images/smilies/yes_30.png" class="smilyicon" style="padding-top: 2px; height: 30px; position: absolute; z-index: 200; cursor: pointer;"
                                value="(Y)" alt="Yes" />
                        </div>
                    </div>
                    <div style="width: 100%; display: flex; height: 20%; margin-left: 5px;">
                        <div style="width: 20%;">
                            <img src="images/smilies/no_30.png" class="smilyicon" style="padding-top: 2px; height: 30px; position: absolute; z-index: 200; cursor: pointer;"
                                value="(N)" alt="No" />
                        </div>
                        <div style="width: 20%;">
                            <img src="images/smilies/yawning_30.png" class="smilyicon" style="padding-top: 2px; height: 30px; position: absolute; z-index: 200; cursor: pointer;"
                                value="(yawn)"
                                alt="Yawn" />
                        </div>
                        <div style="width: 20%;">
                            <img src="images/smilies/hi_30.png" class="smilyicon" style="padding-top: 2px; height: 30px; position: absolute; z-index: 200; cursor: pointer;"
                                value="(hi)" alt="Hi" />
                        </div>
                        <div style="width: 20%;">
                            <img src="images/smilies/clapping_30.png" class="smilyicon" style="padding-top: 2px; height: 30px; position: absolute; z-index: 200; cursor: pointer;"
                                value="(clap)"
                                alt="Clap" />
                        </div>
                        <div style="width: 20%;">
                            <img src="images/smilies/angry_30.png" class="smilyicon" style="padding-top: 2px; height: 30px; position: absolute; z-index: 200; cursor: pointer;"
                                value=":@"
                                alt="Angry" />
                        </div>
                    </div>
                    <div style="width: 100%; display: flex; height: 20%; margin-left: 5px;">
                        <div style="width: 20%;">
                            <img src="images/smilies/giggle_30.png" class="smilyicon" style="padding-top: 2px; height: 30px; position: absolute; z-index: 200; cursor: pointer;"
                                value="(chuckle)"
                                alt="Chuckle" />
                        </div>
                        <div style="width: 20%;">
                            <img src="images/smilies/bug_30.png" class="smilyicon" style="padding-top: 2px; height: 30px; position: absolute; z-index: 200; cursor: pointer;"
                                value="(bug)" alt="Bug" />
                        </div>
                        <div style="width: 20%;">
                            <img src="images/smilies/cake_30.png" class="smilyicon" style="padding-top: 2px; height: 30px; position: absolute; z-index: 200; cursor: pointer;"
                                value="(^)"
                                alt="Yes" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-7 teststyle" id="grpChatSide" style="display: none;">
            <div class="row gc-header">
                <h3>Create Group</h3>
            </div>
            <div class="gc-padding">
            </div>
            <div class="row">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label class="control-label col-sm-2">
                            Group Name:</label>
                        <div class="col-sm-4">
                            <input type="text" id="txtGroupName" class="form-control" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" style="overflow-y: scroll; height: 200px;">
                <div class="col-sm-8">
                    <table id="groupTable" data-show-header="false" data-search="true" class="group-table"
                        style="width: 100%; min-height: 40px; overflow-y: scroll;">
                    </table>
                </div>
            </div>
            <div class="row grpbottom">
                <div class="col-sm-12">
                    <input type="button" id="btnGrpSave" value="Add" class=" btn btn-default sendButton" />
                    <input type="button" id="btnGrpCancel" value="Cancel" class="btn btn-default sendButton" />
                </div>
                <div class="col-sm-6">
                </div>
            </div>
        </div>
    </div>
    <div class="listFooter" id="groupfavicon" style="margin-left: 165px;">
        <footer class="fotter pull-right">
            <a style="cursor: pointer;" id="imggroups">
                <img id="groupChat" src="images/group-chat_icon.png"></a>
            <%--<a style="cursor:pointer;"> <img src="images/group-starred_icon.png"> </a>--%>
            <a style="cursor: pointer;" id="imgfavusers">
                <img src="images/group-fav_icon.png"></a>
        </footer>
    </div>
    <%--  <footer style="position: fixed; bottom: 0px; width: 100%; height: 30px;background-color:#E4E4E4;"> <div style="vertical-align:middle;padding-top:5px;"><span style="padding-left: 20%;font-size: 14px;font-family: sans-serif;vertical-align: -webkit-baseline-middle;">
PCLS is our preferred toxicology and pharmacogenetics provider</span></div></footer> --%>
    <footer id="mainFooter" style="position: fixed; bottom: 0px; width: 100%; height: 35px; background-color: #E4E4E4;">
        <!-- <div id="divfooterleft" style="vertical-align: middle; padding-top: 5px; width: 100%; float: left;">
            <span class="footer_txt">...growing a healthy nation starts here</span>
        </div> -->
        <div id="divfooterright" style="vertical-align: middle; padding-top: 5px; width: 100%; text-align: center; display: none;">
            <div style="padding-left: 5%;">
                <%-- <div class="btnclearchat" id="btnclearlog" title="Clear Chat" >
                         <span class="btnclearchat_text">Clear Chat</span>
                     </div>    --%>
                <input id="btnclearlog" type="button" class="btnclearchat" value="Clear Chat" />
                <input id="btnexportchat" type="button" class="btnclearchat btnspaceleft" value="Export Chat" />
            </div>

        </div>
        <div class="divversion">
            <asp:Label ID="lblversion" runat="server" Text=""></asp:Label>
        </div>
    </footer>
    <div style="display: none;">
        <div id="divexportpopup">
            <div class="exportChat_heading" style="width: 100%">
                Export Chats
                <img src="Images/popup_close.png" id="popup_cle" class="pull-right" style="cursor: pointer; margin-top: -5px;" alt="close">
            </div>
            <div id="divcontent" style="overflow: auto; float: left; width: 100%;">
                <table id="tblexport" class="tblexport" border="0" width="100%">
                    <tr class="trheaderrow">
                        <th>Name
                        </th>
                        <th>Time
                        </th>
                        <th>Conversation
                        </th>
                    </tr>
                </table>
            </div>
            <div style="float: left; width: 100%; height: 12px;">
            </div>
            <div id="divcontrols" style="float: left; width: 100%;">
                <input id="btnexport" type="button" class="btnclearchat" value="Export" />
                <input id="btncancel" type="button" class="btnclearchat btnspaceleft" value="Cancel" />
            </div>
        </div>
    </div>
    <div style="display: none;">
        <%-- <div id="divcommandcenter" style="z-index: 10000;">
            <div style="float:left;">
                <iframe id="commandcenter_popup" src="" style="overflow: auto;
                position: fixed; z-index: 10000; width: 86%; height: 537px;"></iframe>
            </div>
            <div style="float:right; margin-right:10px;">
                <img src="Images/popup_close.png" id="Img1" style="cursor:pointer;"/>
            </div>
        </div>--%>
    </div>

    <input type="hidden" id="msgactpeer" style="display:none" value="" />
    <input type="hidden" id="msgactpeerId" style="display:none" value="" />
    <input type="hidden" id="msgtopactpeerId" style="display:none" value="" />
    <input type="hidden" id="UserUserType" style="display:none" value="" />
     <input type="hidden" id="InitiateMSGDate" style="display:none" value="" />
    <div class="new-pop">
    </div>
        <div class="audiopop">
    </div>
     <div class="videopop">
    </div>
    <%--<div id="myModal" class="modal fade session" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <form id="chtformdata" method="post">
                    <div class="modal-header">

                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">End Session</h4>
                    </div>
                    <div class="modal-body">
                        <p>Your current video call session has ended. Please enter any session notes (optional).</p>

                        <p class="does-ses">
                            <span style="color: #ff0000;">*</span>Does this session qualify as an encounter?
               
              <span class="yse">
                  <input type="radio" id="radio_yes" name="session" checked="checked" value="Yes" />
                  Yes </span>
                            <span class="yse">
                                <input type="radio" name="session" id="radio_no" value="NO" />
                                No 

                            </span>
                        </p>
                        <textarea placeholder="Enter text" id="txtSessionNote" ></textarea>
                    </div>
                    <div class="modal-footer">

                        <div class="popup_conter" style="text-align: center;">
                            <input type="button" value="Save" id="Video_Save_User" class="btn_standard" data-dismiss="modal">
                            <input type="button" id="cancel_popup" class="btn_standard close" data-dismiss="modal" value="Cancel">
                        </div>

                    </div>
                </form>
            </div>

        </div>
    </div>--%>

    <%--<div id="termsandcond_popup" style="display:none;">
            <div id="divcontent1" style="z-index: 1000; overflow: auto;">
            </div>
            <div style="text-align: center; margin-top: 10px;">
                <input type="button" id="accept" value="Accept" style="background-color: #FFCB05;
                    width: 80px; height: 30px; border: 0px;" />
                <input type="button" id="cancel" value="Cancel" style="background-color: #FFCB05;
                    width: 80px; height: 30px; border: 0px;" />
            </div>
        </div>--%>
    <script type="text/javascript">

       $(document).ready(function () {
           var usertype = document.getElementById('<%=hdnusertype.ClientID%>').innerText;
           if (usertype == 'patient' || usertype=='admin') {
               //document.getElementById('auditlog').style.display = "inline";
               $('#auditlog').css("display", "inline")
       }
       else
       {
               //document.getElementById('auditlog').style.display = "none";
               $('#auditlog').css("display", "none")
       }
           //popup();         
       });

        //        $("#temp_popup").click(function () {
        //            commonpopup("PatientLookUp/ASPX/AddTask.aspx");
        //        });



        //        $("#cancel").click(function () {
        //            popupclose();
        //        });

        //        $("#accept").click(function () {
        //            $.ajax({
        //                type: "POST",
        //                url: "Chat.aspx/acceptterms",
        //                contentType: "application/json; charset=utf-8",
        //                dataType: "json",
        //                success: function () {
        //                    popupclose();
        //                }
        //            });
        //        });

        //        function popupclose() {
        //            $("#termsandcond_popup").removeClass('ui-dialog-content ui-widget-content');
        //            $("#termsandcond_popup").hide();
        //            $(".ui-dialog").hide();
        //        };
        $('#popup_cle').click(function () {
            $("#divexportpopup").dialog('close');
        });

        //$('#cancel_popup').click(function () {
        //    $("#myModalAudio").dialog('close');
        //});



    </script>


</body>
</html>
