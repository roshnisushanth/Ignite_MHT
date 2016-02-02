<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chat_log.aspx.cs" Inherits="Hick.Chat_log" %>
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
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css">
    <script src="//code.jquery.com/jquery-1.10.2.min.js"></script>
    <script src="//code.jquery.com/ui/1.10.2/jquery-ui.min.js"></script>
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <%--<script
	src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>--%>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.1/js/bootstrap.min.js"></script>
    <script src="https://rawgit.com/wenzhixin/bootstrap-table/master/src/bootstrap-table.js"></script>
    <!--smilies-->
    <!-- <script type="text/javascript" src="jquery-1.8.0.min.js"></script> -->
    <link href="Content/emoticons.css" rel="stylesheet" />
    <link href="Content/style.css" rel="stylesheet" />
    <style>
        .ui-dialog-titlebar-close{display: inline-block;
    height: 37px !important;   width: 34px !important;}
        div#divcontrols{float: inherit; text-align: center;  width: 100%}
        #divcontrols .btnclearchat {font-weight: bold;}    
        .ui-dialog-titlebar.ui-widget-header.ui-corner-all.ui-helper-clearfix{display:none;}
        div#divexportpopup {overflow: hidden; padding: 0; }
        .pop-img{margin-top: -5px; }
    </style>
</head>
<body>
    <%--<form runat="server">           
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server" ></asp:ScriptManager>--%>
    <%--</form>--%>
    <script src="Scripts/emoticons.js"></script>
    <div id="diviframe">
    </div>
    <div class="container-fluid" id="fluid">
        <span style="display: none;" id="hdnusertype" runat="server"></span>
        <div class="list" id="leftside" style="padding-right: 5px; padding-left: 2px;">
            <div class="header affix-top" id="header">
                <nav class="navbar navbar-default toolbar" role="toolbar">
                    <a href="#" class="btn btn-default btn-link" role="button" id="home">Contacts</a>
                   <%-- <a href="#" class="btn btn-default btn-link" role="button">Conversation</a>
                     <a href="#" class="btn btn-default btn-link listTabletOff" role="button">Tools</a>
                    <a href="#" class="btn btn-default btn-link listTabletOff" role="button">Help</a>--%>
                    <a id="signout" class="btn btn-default btn-link listTabletOff" role="button">Sign out</a>
                    <a class="btn btn-default btn-link dropdown-toggle dropdownon" data-toggle="dropdown" aria-expanded="false" id="extension" role="button">....</a>
                    <ul class="dropdown-menu" role="menu">
                        
                        <%--<li><a href="#" class="btn btn-default btn-link" role="button">Tools</a></li>
                        <li><a href="#" class="btn btn-default btn-link" role="button">Help</a></li>--%>
                        <li><a id="signout1" class="btn btn-default btn-link" role="button">Sign out</a></li>
                    </ul>
                    <img id="closebtn" style="padding-top: 2px; padding-bottom: 2px; float: right;"
                         src="images/close_btn.png" data-toggle="popover">
                </nav>
                <div class="panel panel-default  chat-info toolbar2">
                    <div class="panel-body toolbar2 ">
                        <div style="float: right; padding-top: 3px;" class="button">
                            <div style="width: 100%;">
                                <span id="loginname" runat="server" style="float: right;"></span><span id="Userid"
                                    runat="server" style="display: none"></span>
                                <img src="images/option_selected.png" style="float: right;">
                            </div>
                            <div id="divlastlogin" style="padding-top: 20px; font-size: 11px; display: none"
                                class="mview">
                                <span>Last log in:</span> <span runat="server" name="spnlogedindate" id="datetimepicker">
                                </span>
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
                                <div class="dataTables_wrapper dt-bootstrap footer tablediv" style="overflow-y: auto;
                                    max-height: 500px">
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
                            <%-- <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu1">
                                <li role="presentation"><a role="menuitem" tabindex="-1" href="#">
                                    <img src="images/message-1.png" />chat</a> </li>
                                <li role="presentation"><a role="menuitem" tabindex="-1" href="#">
                                    <img src="images/web_camera.png" />web cam</a></li>
                                <li role="presentation"><a role="menuitem" tabindex="-1" href="#">
                                    <img src="images/add-user.png" />
                                    Add user</a></li>
                                <li role="presentation"><a role="menuitem" tabindex="-1" href="#">
                                    <img src="images/group_chat.png" />
                                    Group chat</a></li>
                            </ul>--%>
                        </div>
                        <div style="float: left">
                            <nav class="navbar navbar-default toolbar2" role="toolbar" style="border-color: transparent;">
                              <%--  <img src="images/hick_logo.png" />--%>
                               <img src="images/ignite_logo_small.png" />
                               <%-- <img src="images/message-1.png" class="listTabletOff" style="padding-left:8px;" /> <img src="images/web_camera.png" class="listTabletOff" style="padding-left:8px;" /> <img src="images/add-user.png" class="listTabletOff" style="padding-left:8px;" /> <img src="images/group_chat.png" class="listTabletOff" style="padding-left:8px;" />--%>
                            </nav>
                        </div>
                    </div>
                </div>
                <!-- contact list dropdown

            <ul class="is-hidden">
                                                <li class="go-back"><a href="#0">Accessories</a></li>
                                                <li class="see-all"><a href="http://codyhouse.co/?p=409">All Benies</a></li>
                                                <li><a href="http://codyhouse.co/?p=409">Caps &amp; Hats</a></li>
                                                <li><a href="http://codyhouse.co/?p=409">Gifts</a></li>
                                                <li><a href="http://codyhouse.co/?p=409">Scarves &amp; Snoods</a></li>
            </ul>-->
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
        <div class="conversation chatdisplay " id="rightside">
            <div class="chatHeader affix-top">
                  <%-- Tool Bar--%>
                <UC:ActionBar ID="ActionBar1" runat="server" />

                <div class="panel panel-default  chat-info">
                    <div class="panel-body">
                        <div class="chat-photo">
                            <img id="usericon" src="images/user-active.png" style="height: 80px; padding-top: 5px;" />
                        </div>
                        <div class="chat-status">
                            <div style="margin-top: 2px; margin-bottom: 2px;">
                                <span id="username" runat="server"></span>
                                 <span id="userrefid" runat="server"></span>
                            </div>
                            <div style="margin-top: 2px;">
                                <img src="images/option_selected.png" id="userstatus" />
                                <span id="statusdiv">Online</span>
                            </div>
                            <div>
                                <span>Please do NOT use in urgent or emergency situations, please call 911 or (844)
                                    MED-HOME</span>
                            </div>
                            <div>
                                <div class="pull-left">
                                    <%--<img src="images/portrait-icon.png" />
                                     <img src="images/folder-icon.png" /> --%>
                                    <%--  <img src="images/drafts-icon.png" />--%>
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
            <div class="conversationContainer " style="overflow-y: auto;">
                <div class="textChatContainer" id="textContainer">
                    <span>Date:
                        <input type="text" style="height: 35px;" id="datepicker"></span>&nbsp;
                    <button id="Viewchatlog" class="btn sendButton">
                        View</button>
                    <%--<a  class="btn btn-default sendButton"  id="Viewchatlog" role="button">View</a>--%>
                    <div class="chatFooter">
                        <div class="input-group">
                            <%--<div id="input_container" class="inputcontainer">
                                        <input type="text" class="form-control" id="InputMessage" autocomplete="off" placeholder="enter your message" style="font-size:13px;width:100%;border:none;">
		  							<!--   <img src="images/smileicon.png" id="input_img" style="padding-top:2px;height:34px;"">  -->
									</div>
							<!--  	<input id="InputMessage" type="text" class="form-control"
									placeholder="enter your message" style="font-size:13px;"> -->
                                <div class="input-group-btn" style="padding-left:5px;">
                                    <button id="send" class="btn btn-default sendButton">SEND</button>
                                </div>--%>
                        </div>
                    </div>
                    <div id="divtabs">
                        <ul>
                            <li onclick="scrolldown('text');"><a href="#divtextchat">Text Chat</a></li>
                            <li onclick="scrolldown('video');"><a href="#divvideochat">Video/Audio Chat</a></li>
                        </ul>
                        <div id="divtextchat">
                        </div>
                        <div id="divvideochat">
                        </div>
                    </div>
                </div>
                <div class="videoChatContainer" style="display: none">
                    <div class="videoContainer">
                        <div class="local-video-wrap">
                            <!--	<video autoplay loop style="height: 100%; width: 100%;">
								<source
									src="http://download.wavetlan.com/SVV/Media/HTTP/H264/Talkinghead_Media/H264_test3_Talkingheadclipped_mp4_480x360.mp4"
									type="video/mp4">
							</video> -->
                        </div>
                        <div class="remote-video-wrap">
                            <!-- <video autoplay loop style="height: 100%; width: 100%;">
								<source
									src="http://techslides.com/demos/sample-videos/small.mp4"
									type="video/mp4">
							</video> -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script src="Scripts/Chat_log.js"></script>
        <!--	 </div> -->
    </div>
    <div class="listFooter" id="groupfavicon">
        <footer class="fotter pull-right">
					<img src="images/group-chat_icon.png"> 
                    <%--<img src="images/group-starred_icon.png"> --%>
                    <img src="images/group-fav_icon.png">
				</footer>
    </div>
    <footer style="position: fixed; bottom: 0px; width: 100%; height: 35px; background-color: #E4E4E4;">
             <div id="divfooterleft" style="vertical-align:middle;padding-top:5px; width:50%;float:left;"> 
                 <!--<span class="footer_txt">
...growing a healthy nation starts herer</span>-->
             </div>
             <div id="divfooterright" style="vertical-align:middle;padding-top:5px; width:50%;float:left; display:none;">
                 <div style="padding-left: 5%;">
                    <%-- <div class="btnclearchat" id="btnclearlog" title="Clear Chat" >
                         <span class="btnclearchat_text">Clear Chat</span>
                     </div>    --%>            
                    
                      <input id="btnexportchat" type="button" class="btnclearchat btnspaceleft" value="Export Chat" />
                      <input id="btnexportvideochat" type="button" class="btnclearchat btnspaceleft" value="Export Chat" style="display:none;" />
                 </div>

             </div>
            <div class="divversion"><asp:Label ID="lblversion" runat="server" Text=""></asp:Label></div>
        </footer>
    <div style="display: none;">
        <div id="divexportpopup">
           <div class="exportChat_heading" style="width:100%">
                    Export Chat
                <img src="Images/popup_close.png" id="chatpopupclose" class="pop-img" />
            </div>
            <div id="divcontent" style="overflow: auto; float: left; width: 100%;">
                <table id="tblexport" class="tblexport" border="0" width="100%">
                    <tr class="trheaderrow">
                        <th>
                            Name
                        </th>
                        <th>
                            Time
                        </th>
                        <th>
                            Conversation
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

$("#chatpopupclose").click(function () {
            $("#divexportpopup").dialog('close');
        });
</script>
</body>
</html>
