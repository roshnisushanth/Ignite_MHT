var groupVideoInterval = 0;

var timerSessionNote = null
var chatopen = false;
var definition = { "smile": { "title": "Smile", "codes": [":)", ":=)", ":-)"] }, "sad-smile": { "title": "Sad Smile", "codes": [":(", ":=(", ":-("] }, "big-smile": { "title": "Big Smile", "codes": [":D", ":=D", ":-D", ":d", ":=d", ":-d"] }, "cool": { "title": "Cool", "codes": ["8|", "8-|", "8=|"] }, "surprised": { "title": "Surprised", "codes": [":O", ":-O", ":=O"] }, "wink": { "title": "Wink", "codes": [";)", ";-)", ";=)"] }, "crying": { "title": "Crying", "codes": [":'(", ":'-(", ":'=("] }, "sweating": { "title": "Sweating", "codes": ["(sweat)"] }, "speechless": { "title": "Speechless", "codes": [":-|", ":|", ":=|"] }, "thinking": { "title": "Thinking", "codes": [":s", ":-s", ":=s"] }, "devil": { "title": "Devil", "codes": ["3:)", "3:-)", "3:=)"] }, "heart": { "title": "Heart", "codes": ["<3"] }, "in-love": { "title": "in-love", "codes": ["(in-love)"] }, "evil-grin": { "title": "evil-grin", "codes": ["(evilgrin)"] }, "yes": { "title": "Yes", "codes": ["(Y)"] }, "no": { "title": "No", "codes": ["(N)"] }, "yawn": { "title": "Yawn", "codes": ["(yawn)"] }, "hi": { "title": "Hi", "codes": ["(hi)"] }, "clapping": { "title": "Clap", "codes": ["(clap)"] }, "angry": { "title": "Angry", "codes": [":@"] }, "giggle": { "title": "Chuckle", "codes": ["(chuckle)"] }, "bug": { "title": "Bug", "codes": ["(bug)"] }, "cake": { "title": "Cake", "codes": ["(^)"] } };
var users;
var sqldata;
var sendBtn = document.getElementById("send");
sendBtn.addEventListener('click', sendText);
var username;
var date = new Date();
var hours = date.getHours();
var minutes = date.getMinutes();
//var currentuser = document.getElementById("loginname");
var currentuser = $("#loginname").html();
var conversationlogid;
//var userid = document.getElementById("Userid");
var userid = $("#Userid").html();
var _userType = $("#hdnusertype").html();
var PeerArray = [];
var _conversationid = 0;
var eventsTimer;
var messageTimer;
var videoTimer;
var remoteusername;
var getmsgtimer;
var _peerusername;
var _srchtxt = '';
var contactsCount = 0;
var favouriteUsers = 0;
var usergroups = 0;
var chating = false;
var txtchat = '';
var totaltxtchatduration = "";
var chatison = '';
var lastmsgcount = 0;
var UserRefID = 0; //nis
var idlecount = 0;
var chattimeout = 0;
var chatscreentimer = null;
var timerchat = 0;
var pageindex = 0;
var pagesize = 50;

var _guid;
var _flshmediaserver;
var _peerid = 0;
var _tempreceivevideoguid = '';
var _tempbroadcastvideoguid = '';
var _receivevideoguid = '';
var videoflag = false;
var _tempConversationid = 0;
var callend = false;

//frm abshk
var _groupvideochatflag;
var UsersArray = [];
var UserNamesArray = [];

var _tempreceivevideoguid1 = '';
var _receivevideoguid1 = '';
var remoteusername1;
var remoteusername2;


var _tempreceivevideoguid2 = '';
var _receivevideoguid2 = '';

var _videoConversationId = 0;
var _videoPeerId = 0;
var isFileUploading = false;
var messageCount = 0;
var cancelFileUpload = false;
var isImage = false;
var isdownloadfailed = 0;
var _timezone;
var _prevConversationId = 0;
var _favoriteIcon;
var _phoneNumber = '';

var interval = undefined;
var chatinterval = undefined;

//frm abshk
var _isvideocall = false;
var _videoPeerId1 = 0;
var _videoPeerId2 = 0;
var _isaudioCall=false;

/* Group Chat */
var isgroupChat = false;
var _groupId = 0;
var usersTemplate = '';

//frm abshk
var _currentBroadcastId;
/* Group Chat */

if (minutes < 10) {
    minutes = '0' + minutes;
}
var ampm = 'AM';
if (hours == 0) { //At 00 hours we need to show 12 am
    hours = 12;
}
else if (hours > 11) {
    if (hours > 12)
        hours = hours % 12;
    ampm = 'PM';
}
var time = hours + ":" + minutes + " " + ampm;

//datetime Display
var dNow = new Date();
var localdate = formatAMPM;
//$('#datetimepicker-input').text(localdate)
var g_objChatColl = [];
var exportchatobject = function () {
    this.Name,
    this.Conversation,
    this.Time
};

function formatAMPM(date) {
    var hours = dNow.getHours();
    var minutes = dNow.getMinutes();
    var ampm = hours >= 12 ? 'pm' : 'am';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes + ' ' + ampm;
    return strTime;
}

var desiredHeight = $('.textChatMessages').height();
$('.chat').css('max-height', desiredHeight);

var h = $('#fluid').height() - $('#header').height() - 45;

// h = h > minHeight ? h : minHeight;
$('#userlist').css('max-height', h);

$('#divcontacttable').css('max-height', h);

$(window).resize(function () {
    var desiredHeight = $('.textChatMessages').height();
    $('.chat').css('max-height', desiredHeight);

    var h = $('#fluid').height() - $('#header').height() - 45;
    // h = h > minHeight ? h : minHeight;
    $('#userlist').css('max-height', h);
    $('#divcontacttable').css('max-height', h);
});
// $('.has-popover').popover();

//button close triggered
$("#closebtn").click('click', function () {
    $("#showleft").css('display', 'block');
    $('#leftside').hide();
    $("#groupfavicon").removeClass("listFooter1");
    $('.conversationContainer').css('bottom', '45px');
    $("#divfooterleft").hide();
});

$("#leftdisplay").click('click', function () {
    $('#leftside').show();
    $("#showleft").css('display', 'none');

    $("#groupfavicon").addClass("listFooter1");
    $("#groupfavicon").addClass("col-sm-5 col-md-5");
    $('.conversationContainer').css('bottom', '8px');
    $("#divfooterleft").show();
});
//extension click
$("#extension").click('click', function () {

    $('#listheader').removeClass("chatdisplay");
    $('#listheader').addClass("dropdownon");

});

var templateUrl = $('#templates').data('url');
$('#templates').load(templateUrl, function () {

});
getTimeZone();
trackUsers();
$(document).ready(function () {


    //    startTrackUserTimer();
    var a = time;
    pageindex = 0;


    //var idleInterval = setInterval(function () { logoff(); }, 1000);//should be 600000 for 10 min

    $("#divfooterright").hide();
    $(function () {
        //        username
        renderTable();
        //        startEventsTimer();
    });

    $(this).mousemove(function (e) {
        //timecount=0;
        //window.clearInterval(idleInterval);
        idlecount = 0;
        //idleInterval = setInterval(function () { logoff(); }, 1000);//should be 600000 for 10 min
    });

    $(this).keypress(function (e) {
        //timecount=0;
        //window.clearInterval(idleInterval);
        idlecount = 0;
        //idleInterval = setInterval(function () { logoff(); }, 1000);//should be 600000 for 10 min

        window.clearInterval(chatscreentimer);
        chattimeout = 0;
        chatscreentimer = setInterval(function () { closechatscreen(); }, 1000);
    });

    document.onkeypress = function (e) {
        //window.clearInterval(idleInterval);
        idlecount = 0;
        //idleInterval = setInterval(function () { logoff(); }, 1000);//should be 600000 for 10 min

        window.clearInterval(chatscreentimer);
        chattimeout = 0;
        chatscreentimer = setInterval(function () { closechatscreen(); }, 1000);
    };

    $(document).click(function () {
        //window.clearInterval(idleInterval);
        idlecount = 0;
        //idleInterval = setInterval(function () { logoff(); }, 1000);//should be 600000 for 10 min

        $("#emoticonContainer").hide();

    });
    $("[class~=lnkadduser]").click(function () {
        if (isgroupChat) {
            isgroupChat = false;
            $(".addtogroup").hide();
        }
        else {
            isgroupChat = true;
            $(".addtogroup").show();
        }
    });

    $(".txtsearchbox").on("keyup", function () {
        _srchtxt = $(this).val();
        resetContacts();
    });

    /* loading dialer pad template*/
    $("#divdialpad").html("");
    $("#divdialpad").load('DialPad.htm');

   

    $(".imgaudiocall").add(".liaudiocall").click(function (e) {
        if (_isvideocall == false) {
            e.stopPropagation();
            if (_groupId == 0) {
                showDialerPad(_phoneNumber);
            }
            else {
                alert("Sorry, Audio call is not supported for this contact.");
            }
        }
    });

    $(".imgmessage").add(".limessage").click(function () {
        hideDialerPad();
    });   

    $("#table tbody").on('click', 'tr.tableRow', function (e) {       
        if ((_isvideocall == false || _isvideocall == undefined) && (_isaudioCall==false)) {
            $("#divcontactsloader").show();
            $("#imgmessageloader").show();

            _tempConversationid = 0;
            //        clearInterval(messageTimer);
            stopEventsTimer();
            stopMessageTimer();
            messageCount = 0;
            _prevConversationId = 0;
            isgroupChat = false;

            hideDialerPad();
            $("#divfooterright").show();
            $('.textChatMessages').hide();

            if ($(this).attr("groupid")) {
                _groupId = $(this).attr("groupid");
            }
            else {
                _groupId = 0;
            }

            var imgurl = $(this).closest("tr")   // Finds the closest row <tr> 
                               .find("#imgOption").attr("src");     // Gets a descendent with class="nr"

            var Name = $(this).closest("tr")   // Finds the closest row <tr> 
                               .find(".userselct")     // Gets a descendent with class="nr"
                               .text();         // Retrieves the text within <td>

            var lastName = $(this).closest("tr").find(".userselctlastname").html();

            UserRefID = $(this).closest("tr").find(".spnrefId").attr("refidval");

            remoteusername = Name;            

            _peerusername = $(this).closest("tr").find(".divusername").html();
            _phoneNumber = '';
            _phoneNumber = $(this).closest("tr").find(".spnphnnumber").attr("pnval");

            //        if(_phoneNumber){
            //         $(".imgaudiocall").attr("disabled",false);
            //        }
            //        else{
            //        $(".imgaudiocall").attr("disabled",true);
            //        }

            var id = $(this).closest("tr")
                          .find("#session")
                          .text();
            var imgstatus = $(this).closest("tr")   // Finds the closest row <tr> 
                               .find("#status").attr("src");
            if (id) {
                _peerid = id;                
            }
            else {
                _peerid = 0;
            }
            if (!$('#videoChatContainer').is(":visible")) {
                _videoPeerId = _peerid;
                /*get group users*/
                if (_groupId != 0) {

                    getgroupUsers();
                }
            }

            clearInterval(chatscreentimer);
            chatscreentimer = null;
            chattimeout = 0;
            chatscreentimer = setInterval(function () { closechatscreen(); }, 1000);

            //call the ajax method to update the total txt chat duration
            if (chatison == '1') {
                updatetxtchatduration(document.getElementById('txtchatduration').innerHTML);

                clearInterval(chatinterval);
                chatinterval = null;
                timerchat = 0;
            }
            else {
                clearInterval(chatinterval);
                chatinterval = null;
                document.getElementById('txtchatduration').innerHTML = '';
                timerchat = 0;
            }


            //        $.ajax({
            //                type:"POST",
            //                url:"HickChatEngine.svc/VideoDuration",
            //                data:'{"currentuserid":"'+userid+'","peerid":"'+_peerid+'"}',
            //                contentType:"application/json; charset=utf-8",
            //                success:function(msg){
            //                    $("#videoduration").html(msg.d);
            //                }
            //            });

            $.ajax({
                type: "POST",
                url: "HickChatEngine.svc/InitiateChat",
                data: '{"currentid":"' + userid + '", "peerid":"' + _peerid + '", "groupid":"' + _groupId + '"}',
                contentType: "application/json; charset=utf-8",
                success: function (msg) {

                    for (var i = 0; i < msg.d.length; i++) {
                        conversationlogid = msg.d[i].Id;
                        PeerArray.push(conversationlogid);
                        localStorage.setItem('peer', PeerArray);
                        _conversationid = msg.d[i].Id;
                        if (!$('#videoChatContainer').is(":visible")) {
                            _videoConversationId = _conversationid;
                        }

                    }
                    getMessages();
                    if (_userType.toLowerCase() != "patient") {
                        //$('.new-pop').append(textAreas);
                    }
                    //$("#myModal").toggle();

                    //                messageTimer = setInterval(function () {
                    //                    getMessages();
                    //                }, 1000);

                    //                startMeassageTimer();
                },
                error: function (err) {
                    //alert(err);
                },
                complete: function () {
                    //                renderTable();                
                    resetContacts();
                }
            });

            //$('#videoChatContainer').css('display', 'none');            
            $("#leftside").addClass("col-sm-4 col-md-4");
            $("#leftside").removeClass("list");
            $("#divcontactmenu").addClass("dropdownsearch");
            $("#divcontactmenu").removeClass("chatdisplay");
            $("#divcontacttable").addClass("hidecontactlist");
            $("#searchtag").addClass("hidecontactlist");
            $("#rightside").removeClass("chatdisplay");            
            $("#rightside").addClass("col-sm-8 col-md-8 teststyle");                             
                var textArea = "<div class='textChatMessages' id='textChatMessages' data-row-key='${name}'><ul class='chat'></ul></div>";
                textArea.id = Name;
                //if (_userType.toLowerCase() != "patient") {
                    if ($('#textContainer').find("div[class~=textChatMessages]").length != 0) {
                        $('#textContainer').find("div[class~=textChatMessages]").remove();
                        //if (isChatCompeted) {
                        $('#UserUserType').val(_userType);
                            //$('.new-pop').addClass('man');
                        //}
                    }
                    $('#textContainer').append(textArea);
                //}

            //popup
						
					//close


                //var textAreas = "<div class='textChatMes' role='dialogue'><div class='popup_header pop'>End Session<a href='#' class='closes'>X</a></div><div class='Inner-section'><form id='chatformdata' method='post'><div class='modal-body'><p>Your current chat session has ended. Please enter any session notes (optional).</p><p class='does-ses'><span style='color:#ff0000;'>*</span>Does this session qualify as an encounter?<span class='yse'><input type='radio' id='chat_radio_yes' name='chatsession' checked='checked' value='Yes'/> Yes </span><span class='yse'><input type='radio' name='chatsession' id='chat_radio_no' value='NO'/> No</span></p><textarea placeholder='Enter text' id='chattxtSessionNote'  style='width: 100%;height: 200px;'></textarea> </div><div class='modal-footer'><div class='popup_conter' style='text-align:center;'><input type='button' value='Save' id='Chat_Save_User' class='btn_standard' onclick='Save_User()'><input type='button' id='chat_cancel_popup' class='btn_standard close' value='Cancel'></div></div></form></div>";
               
            //$("#groupfavicon").css('display', 'block');
            $("#groupfavicon").addClass("listFooter1");
            $("#groupfavicon").addClass("col-sm-5 col-md-5");
            //$("#groupfavicon").removeClass("listFooter");
            $('#usericon').attr("src", imgurl);
            $('#userstatus').attr("src", imgstatus);
            if (imgstatus == "Images/option_selected.png")
            { $('#statusdiv').text("Online"); }
            else
            { $('#statusdiv').text("Offline"); }

            chatopen = true;
            $('#username').html(Name + " " + lastName);
            $('#UserRefId').html(UserRefID);


            if (videoflag == true) {
                $('.textChatMessages').css('top', '300px' );
            }
            /*For group chat*/
            if (_groupId != 0) {
                $('#userstatus').hide();
                $('#statusdiv').hide();
                $('#username').html(Name);
            }
            else {
                $('#userstatus').show();
                $('#statusdiv').show();
                $('#username').html(Name + " " + lastName);
            }


            document.getElementById('videoduration').innerHTML = "";
        }        
    });
    



    //$(window).on("blur focus", function (e) {
    //    var prevType = $(this).data("prevType");

    //    if (prevType != e.type) {   //  reduce double fire issues
    //        switch (e.type) {
    //            case "blur":
    //                idleInterval = setInterval(function () { logoff(); }, 1000);//should be 600000 for 10 min                
    //                break;
    //            case "focus":
    //                window.clearInterval(idleInterval);
    //                idlecount = 0;
    //                idleInterval = setInterval(function () { logoff(); }, 1000);//should be 600000 for 10 min                
    //                break;
    //        }
    //    }
    //    $(this).data("prevType", e.type);
    //});

    $("#imgfavusers").click(function () {
        if (favouriteUsers == 0) {
            favouriteUsers = 1;
            usergroups = 0;
        }
        else {
            favouriteUsers = 0;
        }
        resetContacts();
    });

    $("#imggroups").click(function () {
        if (usergroups == 0) {
            usergroups = 1;
            favouriteUsers = 0;
        }
        else {
            usergroups = 0;
        }
        resetContacts();
    });
    
    $("#table tbody").on('click', '.favorite-img', function (e) {
        e.stopPropagation();
        var fid = $(this).closest("tr")
                      .find("#session")
                      .text();
        $.ajax({
            type: "POST",
            url: "HickChatEngine.svc/UpdateFavorites",
            data: '{"currentid":"' + userid + '", "peerid":"' + fid + '"}',
            contentType: "application/json; charset=utf-8",
            success: function (msg) {
                //                 renderTable();
                resetContacts();
            },
            error: function (err) {
                //alert(err);

            },
            complete: function () {

            }
        });
    });

    $("#table tbody").on('click', '.addtogroup', function (e) {
        clearChatLog();
        e.stopPropagation();
        var _uid = $(this).attr("uid");
        $.ajax({
            type: "POST",
            url: "HickChatEngine.svc/AddUserToGroup",
            data: '{"currentuserid":"' + userid + '", "groupid":"' + _groupId + '", "selecteduser":"' + _uid + '", "peerid":"' + _peerid + '", "conversationid":"' + _conversationid + '"}',
            contentType: "application/json; charset=utf-8",
            success: function (msg) {
                if (msg.d) {

                    _groupId = msg.d.GroupId;
                    _conversationid = msg.d.Id;
                    _prevConversationId = 0;
                    //                renderTable();
                    resetContacts();
                    getMessages();

                }

            },
            error: function (err) {
                //                alert(err);                
            },
            complete: function () {

            }
        });

    });
    $("#imgsendfiles").click(function () {

        isImage = false;
        var filctrl = null;
        filctrl = $("#fileUpload");
        filctrl.replaceWith(filctrl = filctrl.clone(true));
        $(filctrl).unbind("change");
        $(filctrl).bind("change", function () {
            $('.chat').find("li[class~=msgclrchat]").remove();
            $("#btnuploadfiles").trigger("click");
        });
        $(filctrl).trigger("click");

    });

    //    $(".favorite-icon").click(function () {
    //        $(this).css("color", "red");
    //    });
    $("#imgsendimage").click(function () {
        isImage = true;
        var filctrl = null;
        filctrl = $("#fileUpload");
        filctrl.replaceWith(filctrl = filctrl.clone(true));
        $(filctrl).unbind("change");
        $(filctrl).bind("change", function () {
            $('.chat').find("li[class~=msgclrchat]").remove();
            $("#btnuploadfiles").trigger("click");
        });
        $(filctrl).trigger("click");

    });

    $(document).delegate(".lnkreceivefile", "click", function () {

        var flnam = $(this).attr("flnam");
        var _logid = $(this).attr("logid");
        var _prnam = $(this).attr("prnam");
        isFileUploading = true;
        $(this).closest("li").find(".lnkbutton").hide();
        var ldr = "<div><img src='Images/fileupload-loader.gif' style='background-color:lightgreen;' /></div>";
        $(this).closest("div[class~=divconvrsation]").append(ldr);
        receiveDocuments(flnam, _logid, _prnam);
    });
    $(document).delegate(".lnkcancel", "click", function () {
        $(this).removeClass("lnkcancel");
        $(this).removeClass("lnkbutton");
        $(this).html("Cancelling....");
        cancelFileUpload = true;

    });
    $(document).delegate(".lnkcanceldownload", "click", function () {
        var _logid = $(this).attr("logid");
        updateFileReceivedStatus(_logid, 4);

    });

    $("#btnclearlog").click(function () {
        //        _prevConversationId = 0;
        clearChatLog();
    });

    $("#btnexportchat").click(function () {
        var _convid = '';
        if (_prevConversationId != 0) {
            _convid = _prevConversationId + "," + _conversationid;
        }
        else {
            _convid = _conversationid;
        }

        $.ajax({
            type: "POST",
            url: "HickChatEngine.svc/GetChatLogForExport",
            data: '{"currentuserid":"' + userid + '", "conversationid":"' + _convid + '", "flag":"yes", "timezone":"' + _timezone + '", "groupid":"' + _groupId + '", "logdate":""}',
            contentType: "application/json; charset=utf-8",
            success: function (msg) {
                if (msg.d.length > 0) {
                    $("#btnexportchat").attr("disabled", false);
                    $("#btnexportchat").removeClass("btndisabled");
                    var tr;
                    var td = '';

                    g_objChatColl = msg.d;
                    $("#tblexport").find("tr:gt(0)").remove();
                    for (var i = 0; i < msg.d.length; i++) {
                        tr = document.createElement("tr");
                        $(tr).addClass("trnormalrow");
                        td = "<td class='tdname'>" + msg.d[i].Name + "</td><td class='tdtime'>" + msg.d[i].Time + "</td>";
                        if (msg.d[i].MessageType == 1) {
                            td = td + "<td class='tdconv'><input class='txtexportchat' type='text' value='" + formatSymbolsToText(msg.d[i].Conversation) + "'/></td>";
                        }
                        else {
                            td = td + "<td class='tdconv'>" + msg.d[i].Conversation + "</td>";
                        }


                        $(tr).append(td);
                        $("#tblexport").append(tr);
                    }
                    var phight = window.innerHeight - (window.innerHeight / 2);
                    var pwidth = window.innerWidth - (window.innerWidth / 2);
                    $("#divexportpopup").dialog({
                        modal: true,
                        height: phight,
                        width: pwidth,
                        resizable: false,
                        //title: "Export Chat",
                        create: function () {
                            //$("#tblexport").attr("cellpadding", "10");
                            $("#divcontent").css("height", phight - 100);
                            $(".ui-dialog-titlebar").hide();
                            $(".ui-dialog-content").css("padding", "0px");
                        }
                    });
                }
                else {
                    $("#btnexportchat").attr("disabled", "disabled");
                    $("#btnexportchat").addClass("btndisabled");
                }
            },
            error: function (err) {
                //alert(err);
            },
            complete: function () {

            }
        });
    });
    $(document).delegate(".lnksendrecimage", "click", function () {
        var _logid = $(this).attr("logid");
        var _filurl = $(this).attr("flurl");

        var objdata = { currentuserid: userid, fileurl: _filurl };
        var jsondata = JSON.stringify(objdata);
        $.ajax({
            type: "POST",
            url: "HickService.svc/Send",
            data: jsondata,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.d == "error") {
                    alert("Sorry an error has occured. Please contact administrator");
                }
                else {
                    //alert(result.d);
                    window.open(_filurl);
                }
            },
            complete: function () {

            },
            error: function () {
                alert("Sorry an error has occured. Please contact administrator");
            }
        });
    });
    $("#btnexport").click(function () {
        if ($("#tblexport").find("tr:gt(0)").length > 0) {
            g_objChatColl = [];
            $("#tblexport").find("tr:gt(0)").each(function (index, value) {

                var obj = new exportchatobject();
                obj.Name = $(value).find("[class~=tdname]").html();
                obj.Time = $(value).find("[class~=tdtime]").html();

                if ($(value).find("input[class~=txtexportchat]").length != 0) {
                    obj.Conversation = $(value).find("input[class~=txtexportchat]").val();
                }
                else {
                    obj.Conversation = $(value).find("[class~=tdconv]").html();
                }

                if (obj.Conversation.length > 0) {
                    g_objChatColl.push(obj);
                }
            });
        }

        var json = JSON.stringify(g_objChatColl);
        var _data = { strjson: json }
        $.ajax({
            type: "POST",
            url: "Chat.aspx/ModifyChatForExport",
            data: JSON.stringify(_data),
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (msg) {
                exportChat("1");
            },
            complete: function () {
                $("#divexportpopup").dialog('close');
            },
            error: function (err) {
                alert("Sorry an error has occured. Please contact administrator");
            }
        });

        return false;
    });
    $("#btncancel").click(function () {
        $("#divexportpopup").dialog('close');
    });

    //    $("#groupChat").click(function () {
    //        $("#rightside").css("display", "none");
    //        $("#divfooterright").css("display", "none");
    //        $("#grpChatSide").css("display", "block");
    //        var gtemplate;
    //        $.ajax({
    //            type: "POST",
    //            url: "HickChatEngine.svc/GetUsers",
    //            data: '{"currentuserid":"' + userid + '"}',
    //            contentType: "application/json; charset=utf-8",
    //            success: function (msg) {
    //                sqldata = msg.d;
    //                
    //                if (msg.d.length>0)
    //                {
    //                  $('#groupTable tbody').html('');
    //               

    //                for (var i = 0; i < msg.d.length; i++) {
    //                                       
    //                    gtemplate = "<tr id='grpTableRow' data-row-key='${User}' class='grp-users' style='cursor:pointer;'><td>  <input type='checkbox' name='slctUser' value="+ msg.d[i].ID+" id='grpUserChk' /> <span class='userselct' id='userName'>" + msg.d[i].Firstname + "</span>&nbsp;<span class='userselctlastname'>" + msg.d[i].Lastname + "</span>   </td> </tr>";
    //                       
    //                    $('#groupTable').append(gtemplate);
    //                }
    //                }
    //               
    //            },
    //            error: function (err) {

    //                //alert(err);

    //            }
    //        });

    //    });

    $("#btnGrpSave").click(function () {
        var groupName = $("#txtGroupName").val();
        var groupUserId = '';
        //$('.group-table tbody tr').each(function (i,row) {
        //    groupUserId.push($("input[name=slctUser]:checked").val());
        //});
        //

        $('tr.grp-users').each(function (i, r) {

            var $row = $(r),
            $checkedBoxes = $row.find('input[name=slctUser]:checked');

            $checkedBoxes.each(function (i, checkbox) {

                groupUserId += checkbox.value + ",";
            });

        });

        groupUserId = groupUserId.slice(0, -1);

        $.ajax({
            type: "POST",
            url: "HickChatEngine.svc/AddGroup",
            data: '{"userid":"' + userid + '", "groupuserids":"' + groupUserId + '", "groupname":"' + groupName + '"}',
            contentType: "application/json; charset=utf-8",
            success: function (msg) {
                alert("group added successfully!!");
            }
        });

    });

    $("#btnGrpCancel").click(function () {
        $("#grpChatSide").css("display", "none");
        $("#rightside").css("display", "block");
        $("#divfooterright").css("display", "block");
    });

    //    $("#InputMessage").click(function () {
    //        var position=$("#InputMessage").getCursorPosition();
    //    });

    $(".smilyicon").click(function (e) {
        e.stopPropagation();
        var position = $("#InputMessage").getCursorPosition();
        var msg = $("#InputMessage").val();
        var msglength = msg.length;
        var part1 = msg.substring(0, position);
        var part2 = msg.substring(position, msglength);
        msg = part1 + $(this).attr('value') + part2;
        $("#InputMessage").val(msg);
        $("#InputMessage").focus();

        //        msg=msg+$(this).attr('value');
        //        $("#InputMessage").val(msg);
    });

    $("#input_img").click(function (e) {
        e.stopPropagation();
        $("#emoticonContainer").toggle();
    });

    new function ($) {
        $.fn.getCursorPosition = function () {
            var pos = 0;
            var el = $(this).get(0);
            // IE Support
            if (document.selection) {
                el.focus();
                var Sel = document.selection.createRange();
                var SelLength = document.selection.createRange().text.length;
                Sel.moveStart('character', -el.value.length);
                pos = Sel.text.length - SelLength;
            }
                // Firefox support
            else if (el.selectionStart || el.selectionStart == '0')
                pos = el.selectionStart;
            return pos;
        }
    }(jQuery);

});


$(".dropdown-menu").click(function () {


});


var desiredHeight = $('.textChatMessages').height();
$('.conversation').css('max-height', desiredHeight);

//var desiredHeight = $('.videoContainer').height();
//$('.remote-video-wrap').css('height', desiredHeight);

//$('.local-video-wrap').css('height', desiredHeight);




$(window).resize(function () {
    var desiredHeight = $('.textChatMessages').height();
    $('.chat').css('max-height', desiredHeight);

    var desiredHeight = $('.videoContainer').height();
    $('.remote-video-wrap').css('height', desiredHeight);
});



function sendText() {
    var txt = $("#InputMessage").val();

    if (txt.length > 0) {
        if (currentChatPeerUserId != 0 && _peerid != currentChatPeerUserId && chatison) {
            //isChatCompeted = true;
            $('.new-pop').addClass('man');
        }
        currentChatPeerUserId = _peerid;
        $('#msgtopactpeerId').val(_peerid);
        var chatLists = $('#divcontacttable table tbody');
        var truid = "user" + _peerid
        var tabDivLast = $('#divcontacttable table tbody tr').filter('tr[uid="' + truid + '"]');
        var topLastDiv = chatLists.children(':first');
        topLastDiv.before(tabDivLast);
        saveMessage(txt, 1);

        $.emoticons.define(definition);
        var msgField = $('#InputMessage');
        var textWithEmoticons = $.emoticons.replace(formatSymbolsToText(msgField.val()));

        //var element = '<li><span class="pull-left spanlabel ">' + currentuser + ':' + '</span> <span class="pull-right" style="color: #B3B3B3;">' + time + '</span> <div class="clearfix" style="margin-left: 50px; margin-right: 70px;">' + textWithEmoticons + '</div></li>';


        //var element = '<li class="chatcolorgrey"><span class=" spanlabel ">' + currentuser + ':' + '</span><br/><div class="" style="margin-left: 25px;">' + textWithEmoticons + '</div><span class="" style="color: #777;padding-right:8px;">' + time + '</span><br/></li>';

        /*remove chat history message */
        $('.chat').find("li[class~=msgclrchat]").remove();

        var element = '<li class="chatcolorgrey"><span class=" spanlabel ">' + currentuser + ':' + '</span><br/><div class="" style="margin-left: 50px; margin-right: 30px;">' + textWithEmoticons + '</div><span class="pull-right" style="color: #777;padding-right:8px;">' + getLocalTime(new Date(), false) + '</span><br/></li>';

        var completetext = currentuser + "=" + msgField.val();
        $('.chat').append(element);
        $('.chat').animate({
            scrollTop: $('.chat li:last-child').offset().top + 30
        }, 1000);
        // $('.chat').scrollTop = $('.chat').scrollHeight;
        //$('.chat li:last-child').last().focus();
        var objDiv = document.getElementById("textChatMessages");
        objDiv.scrollTop = objDiv.scrollHeight;
        msgField.val('');

        var initiateMSGDateTime = new Date();
        initiateMSGDateTime = moment(initiateMSGDateTime).format('YYYY-MM-DD HH:mm:ss');
        if ($('#InitiateMSGDate').val() == "") {
            $('#InitiateMSGDate').val(initiateMSGDateTime);
        }
        OpenSessionNote(_peerid);
    }
}

function StartSessionNoteTimer() {
    if (timerSessionNote !== null) return;
    timerSessionNote = setInterval(function () {
        ShowSessionNote();
        $('#msgactpeer').val("");
    }, 180000);
}

function OpenSessionNote(peerId) {
    clearInterval(timerSessionNote);
    timerSessionNote = null;
    StartSessionNoteTimer();
    if ($('#msgactpeer').val() == "") {
        $('#msgactpeer').val(peerId);
        $('#msgactpeerId').val($('#msgactpeer').val());
    }
    if ($('#msgactpeer').val() != peerId) {
        ShowSessionNote();
        $('#msgactpeerId').val($('#msgactpeer').val());
        $('#msgactpeer').val("");
        
    }
}

function ShowSessionNote() {
    //var userType = $('#UserUserType').val();
    if (_userType.toLowerCase() != "patient") {
        $('textarea#chattxtSessionNote').val(" ");
        $('.new-pop').removeClass('man');

        //$('.closes').click(function () {
        //    $('textarea#chattxtSessionNote').val(" ");
        //    $('.new-pop').removeClass('man');
        //});

        //$('#chat_cancel_popup').click(function () {
        //    $('textarea#chattxtSessionNote').val(" ");
        //    $('.new-pop').removeClass('man');
        //});

        var textAreas = "<div class='textChatMes' role='dialogue'><div class='popup_header pop'>End Session<a href='#' class='closes' onclick='popupclose()'>X</a></div><div class='Inner-section'><form id='chatformdata' method='post'><div class='modal-body'><p>Your current chat session has ended. Please enter any session notes (optional).</p><p class='does-ses'><span style='color:#ff0000;'>*</span>Does this session qualify as an encounter?<span class='yse'><input type='radio' id='chat_radio_yes' name='chatsession' checked='checked' value='Yes'/> Yes </span><span class='yse'><input type='radio' name='chatsession' id='chat_radio_no' value='NO'/> No</span></p><textarea placeholder='Enter text' id='chattxtSessionNote'  style='width: 100%;height: 200px;'></textarea> </div><div class='modal-footer'><div class='popup_conter' style='text-align:center;'><input type='button' value='Save' id='Chat_Save_User' class='btn_standard' onclick='Save_User()'><input type='button' id='chat_cancel_popup' class='btn_standard close' value='Cancel' onclick='popupclose()'></div></div></form></div>";
        $('.new-pop').append(textAreas);
        $('.new-pop').addClass('man');
    }
    clearInterval(timerSessionNote);
    timerSessionNote = null;
}

function popupclose()
{
    $('textarea#chattxtSessionNote').val(" ");
    $('.new-pop').removeClass('man');
}

$("#InputMessage").keypress(function (event) {
    var keycode = (event.keyCode ? event.keyCode : event.which);
    if (keycode == '13') {
        sendText();
    }
});


$(function () {
    $("#signout").click('click', function () {
        $.ajax({
            type: "POST",
            url: "Chat.aspx/Signout",
            data: '{"userid":"' + userid + '"}',

            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                if(document.getElementById('txtchatduration').innerHTML!="" && document.getElementById('txtchatduration').innerHTML!=undefined)
                {
                    updatetxtchatduration(document.getElementById('txtchatduration').innerHTML);
                }
                stopVideoChat("signout");
            },
            success: function (val) {
            },
            complete: function () {
                window.location = "Index.aspx";
            },
            error: function (err) {
                //alert(err);
            }
        });
    });
});


$(function () {
    $("#signout1").click('click', function () {
        $.ajax({
            type: "POST",
            url: "Chat.aspx/Signout",
            data: '{"userid":"' + userid + '"}',
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                stopVideoChat("signout");
            },
            success: function (val) {
            },
            complete: function () {
                window.location = "Index.aspx";
            },
            error: function (err) {

            }
        });
    });
});
var renderTable = function (success, error) {
    if (pageindex == 0) {
        pageindex = 1;
    }    
    $.ajax({
        type: "POST",
        url: "HickChatEngine.svc/GetUsers",
        data: '{"currentuserid":' + userid + ',"favouriteUsers":"' + favouriteUsers + '","PageIndex":"' + pageindex + '","PageSize":"' + pagesize + '","usertype":"' + _userType + '"}',
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            if (usergroups != 1) {
                sqldata = msg.d;
                var usercoll = msg.d.UsersColl;
                var groupcoll = msg.d.GroupsColl;
                // $('#table tbody').html("");
                usersTemplate = '';
                contactsCount = 0;
                for (var i = 0; i < usercoll.length; i++) {

                    if (usercoll[i].Status == 0) {
                        onlineuser = "Images/option_unselected.png";
                    }
                    else {
                        onlineuser = "Images/option_selected.png";
                    }
                    if (usercoll[i].Image == "") {
                        userphoto = "Images/default_user.png";
                    }
                    else {
                        userphoto = "Images/" + usercoll[i].Image;
                    }

                    /* setting the availability status for selected user  */
                    if (_peerid == usercoll[i].ID) {
                        $('#userstatus').attr("src", onlineuser);
                        if (usercoll[i].Status == 0) {
                            $('#statusdiv').text("Offline");

                        }
                        else {
                            $('#statusdiv').text("Online");
                        }
                    }

                    if (usercoll[i].Favorites == 1) {
                        _favoriteIcon = "Images/heart-red.png";
                    }
                    else {
                        _favoriteIcon = "Images/heart-greay.png";
                    }

                    //if (String(msg.d[i].Username).toLowerCase() == currentuser.toLowerCase()) {
                    //    template = null;
                    //}
                    //else {
                    //    template = "<tr id='tableRow' data-row-key='${User}' class='tableRow' style='cursor:pointer;'> <td id='userThumbnail'><img id='imgOption' src='" + userphoto + "'></img><img id='status' src='" + onlineuser + "'></img></td> <td>   <div class='userselct' id='userName'>" + msg.d[i].Username + "</div><div class='small1' id='statusmessage'>" + msg.d[i].StatusMessage + "</div>  <div class='small' id='session'>" + msg.d[i].ID + "</div> </td>  </tr>";
                    //}


                    if (usercoll[i].ID == userid) {
                        template = null;
                    }
                    else {
                        var grclas = "hide-element";
                        if (isgroupChat) {
                            grclas = "";
                        }

                        var un = usercoll[i].Firstname + " " + usercoll[i].Lastname;
                               var refid = usercoll[i].ReferenceID;
                        var cls = '';
                        if (_srchtxt.length > 0) {
                            if (un) {
                                if (un.toLowerCase().indexOf(_srchtxt.toLowerCase()) == -1) {
                                    cls = "hide-element";
                                }
                                else {
                                    contactsCount += 1;
                                }
                            }
                        }
                        else {
                            contactsCount += 1;
                        }

                      //template = "<tr id='tableRow1' data-row-key='${User}' class='tableRow " + cls + "' style='cursor:pointer;'> <td id='userThumbnail'><img id='imgOption' src='" + userphoto + "'></img><img id='status' src='" + onlineuser + "'></img></td> <td>   <span class='userselct' id='userName'>" + usercoll[i].Firstname + "</span>&nbsp;<span class='userselctlastname'>" + usercoll[i].Lastname + "</span><div class='small1' id='statusmessage'>" + usercoll[i].StatusMessage + "</div>  <div class='small' id='session'>" + usercoll[i].ID + "</div><div class='small divusername'>" + usercoll[i].Username + "</div><span class='hide-element spnphnnumber' pnval=" + usercoll[i].PhoneNumber + "></span> </td><td valign='top' class='action-icon-container'><img class='favorite-img action-icon heart-minheight' title='Favorite Icon' src='" + _favoriteIcon + "'/><img class='addtogroup action-icon " + grclas + "' title='Add To Group' src='Images/add-user.png' uid='" + usercoll[i].ID + "'/>";

                        template = "<tr id='tableRow1' uid='user" + usercoll[i].ID + "' data-row-key='${User}' class='tableRow " + cls + "' style='cursor:pointer;'> <td id='userThumbnail'><img id='imgOption' src='" + userphoto + "'></img><img id='status' src='" + onlineuser + "'></img></td> <td id='usrs'>   <span class='userselct' id='userName'>" + usercoll[i].Firstname + "</span>&nbsp;<span class='userselctlastname'>" + usercoll[i].Lastname + "</span><div class='small1' id='statusmessage'>" + usercoll[i].StatusMessage + "</div>  <div class='small' id='session'>" + usercoll[i].ID + "</div><div class='small divusername'>" + usercoll[i].Username + "</div><span class='hide-element spnphnnumber' pnval=" + usercoll[i].PhoneNumber + "></span><span class='hide-element spnrefId' refidval=" + usercoll[i].ReferenceID + "></span> </td><td valign='center' class='action-icon-container'><img class='favorite-img action-icon heart-minheight' title='Favorite Icon' src='" + _favoriteIcon + "'/><img class='addtogroup action-icon " + grclas + "' title='Add To Group' src='Images/add-user.png' uid='" + usercoll[i].ID + "'/>";

                        if (_peerid == usercoll[i].ID) {
                            //template = "<tr id='tableRow' data-row-key='${User}' class='tableRow rowhighlite " + cls + "' style='cursor:pointer;'> <td id='userThumbnail'><img id='imgOption' src='" + userphoto + "'></img><img id='status' src='" + onlineuser + "'></img></td> <td>   <span class='userselct' id='userName'>" + usercoll[i].Firstname + "</span>&nbsp;<span class='userselctlastname'>" + usercoll[i].Lastname + "</span><div class='small1' id='statusmessage'>" + usercoll[i].StatusMessage + "</div>  <div class='small' id='session'>" + usercoll[i].ID + "</div><div class='small divusername'>" + usercoll[i].Username + "</div><span class='hide-element spnphnnumber' pnval=" + usercoll[i].PhoneNumber + "></span> </td><td valign='top' class='action-icon-container'><img class='favorite-img action-icon heart-minheight' title='Favorite Icon' src='" + _favoriteIcon + "'/><img class='addtogroup action-icon " + grclas + "' title='Add To Group' src='Images/add-user.png' uid='" + usercoll[i].ID + "'/>";
                            template = "<tr id='tableRow' uid='user" + usercoll[i].ID + "' data-row-key='${User}' class='tableRow rowhighlite " + cls + "' style='cursor:pointer;'> <td id='userThumbnail'><img id='imgOption' src='" + userphoto + "'></img><img id='status' src='" + onlineuser + "'></img></td> <td>   <span class='userselct' id='userName'>" + usercoll[i].Firstname + "</span>&nbsp;<span class='userselctlastname'>" + usercoll[i].Lastname + "</span><div class='small1' id='statusmessage'>" + usercoll[i].StatusMessage + "</div>  <div class='small' id='session'>" + usercoll[i].ID + "</div><div class='small divusername'>" + usercoll[i].Username + "</div><span class='hide-element spnphnnumber' pnval=" + usercoll[i].PhoneNumber + "></span> <span class='hide-element spnrefId' refidval=" + usercoll[i].ReferenceID + "></span></td><td valign='center' class='action-icon-container'><img class='favorite-img action-icon heart-minheight' title='Favorite Icon' src='" + _favoriteIcon + "'/><img class='addtogroup action-icon " + grclas + "' title='Add To Group' src='Images/add-user.png' uid='" + usercoll[i].ID + "'/>";
                        }

                        if (usercoll[i].UnReadMessages == true) {
                            template = template + "<img class='imgmsgnotification action-icon' title='New Message' src='Images/messages-icon.png'/>";
                        }
                        if (usercoll[i].IncomingCall == true && !$('#videoChatContainer').is(":visible")) {
                            template = template + " <img class='imgvideonotification action-icon' title='Incomming Video Call' src='Images/call-incoming-icon.png'/> ";
                        }

                        template = template + "<span class='action-icon'>" + usercoll[i].VideoCallDuration + "</span> </td><td> <div style='float:right;'>  </div> </td>  </tr>";

                    }

                    usersTemplate = usersTemplate + template;
                    // $('#table tbody').append(template);                             

                }
                bindGroups(groupcoll);
            }
            else {
                usersTemplate = '';
                bindGroups(msg.d.GroupsColl);
            }
        },
        error: function (err) {

            //alert(err);

        },
        complete: function () {
            $("#page_loader").hide();
            $("#divcontactsloader").hide();
            $("#imgmessageloader").hide();
            //        getGroups();

            startEventsTimer();
        }
    });
}
function saveMessage(msg, msgtyp) {
    var objdata = { conversationid: _conversationid, message: msg, curentuserid: userid, peeruserid: _peerid, messagetype: msgtyp, groupid: _groupId };
    var jsondata = JSON.stringify(objdata);
    if (msg.length > 0) {
        $.ajax({
            type: "POST",
            url: "HickChatEngine.svc/SaveMessage",
            data: jsondata,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                getMessages();


                //            chatinterval = window.setInterval(function(){tictac();}, 1000);            
            }
        });

        //var id = $('#lstMsgUsername').val();
    }

}


function getMessages() {
    if ($('#divfooterright').css('display') == 'block') {
        var _convid = '';
        if (_prevConversationId != 0) {
            _convid = _prevConversationId + "," + _conversationid;
        }
        else {
            _convid = _conversationid;
        }
        $.ajax({
            type: "POST",
            url: "HickChatEngine.svc/GetMessages",
            data: '{"conversationid":"' + _convid + '","currentuserid":"' + userid + '","timezone":"' + _timezone + '","peeruserid":"' + _peerid + '","groupid":"' + _groupId + '"}',
            beforeSend: function () {
                stopMessageTimer();
            },
            contentType: "application/json; charset=utf-8",
            success: function (val) {

                if (!isFileUploading) {
                    //if (val.d.length == 0) {
                    //    $('.chat').html("");
                    //}
                    //else if (messageCount != val.d.length) {

                    $('.chat').html("");
                    var isunreadmsg = false;
                    messageCount = val.d.length;
                    if (messageCount > 0) {
                        $("#btnexportchat").attr("disabled", false);
                        $("#btnexportchat").removeClass("btndisabled");

                        for (var i = 0; i < val.d.length; i++) {

                            var element;
                            if (val.d[i].MessageTo == userid && (val.d[i].MessageType == 1 || val.d[i].MessageType == 3) && (val.d[i].ReadStatus == 2 || val.d[i].ReadStatus == 3)) {
                                isunreadmsg = true;
                            }
                            else if (_groupId != 0 && val.d[i].ReadStatus == "2") {
                                isunreadmsg = true;
                            }
                            if (_conversationid != val.d[i].ConversationId) {
                                _prevConversationId = val.d[i].ConversationId;
                                //                            $("#btnclearlog").attr("disabled", "disabled");
                                //                            $("#btnclearlog").addClass("btndisabled");

                                $("#btnclearlog").attr("disabled", false);
                                $("#btnclearlog").removeClass("btndisabled");
                            }
                            else {
                                $("#btnclearlog").attr("disabled", false);
                                $("#btnclearlog").removeClass("btndisabled");
                            }

                            $.emoticons.define(definition);
                            var msgField = formatSymbolsToText(val.d[i].Conversation);
                            var textWithEmoticons = $.emoticons.replace(msgField);
                            var convrstn = textWithEmoticons;

                            if (val.d[i].PeerID == userid) {

                                if (val.d[i].MessageType == 3) {
                                    if (val.d[i].ReadStatus == 4) {
                                        convrstn = convrstn + "</br>" + "<span style='color:gray;'>Declined</span>";
                                    }
                                    else if (val.d[i].ReadStatus == 3) {
                                        convrstn = convrstn + "</br>" + "<span style='color:gray;'>Transfer Complete</span>";
                                    }
                                    else if (val.d[i].ReadStatus == 5) {
                                        convrstn = convrstn + "</br>" + "<span style='color:gray;'>Received</span>";
                                    }
                                }
                                element = '<li class="chatcolorgrey"><span class=" spanlabel ">' + val.d[i].PeerName + ':' + '</span><br/><div class="" style="margin-left: 50px; margin-right: 30px;">' + convrstn + '</div><span class="pull-right" style="color: #777;padding-right:8px;">' + val.d[i].Time + '</span><br/></li>';
                            }
                            else {
                                //var convrstn = formatSymbolsToText(val.d[i].Conversation);
                                if (val.d[i].MessageType == 3) {
                                    if (val.d[i].ReadStatus == 4) {
                                        convrstn = convrstn + "</br>" + "<span style='color:gray;'>Declined</span>";
                                    }
                                    else if (val.d[i].ReadStatus == 3) {
                                        convrstn = convrstn + "</br>" + "<span class='lnkbutton lnkreceivefile' logid='" + val.d[i].Id + "' flnam='" + val.d[i].Conversation + "' prnam='" + val.d[i].PeerUserName + "'>Accept</span><span class='lnkbutton lnkcanceldownload' logid='" + val.d[i].Id + "' flnam='" + val.d[i].Conversation + "'>Cancel</span>";
                                    }
                                    else {
                                        convrstn = convrstn + "</br>" + "<span style='color:gray;'>Received</span></br><span class='lnkbutton lnksendrecimage' logid='" + val.d[i].Id + "' flurl='" + val.d[i].ReceivedImagePath + "'>Read Open</span>";
                                    }
                                }

                                else if (val.d[i].MessageType == 1) {
                                    if (val.d[i].ReadStatus == 2 && _prevConversationId != val.d[i].ConversationId) {
                                        lastmsgcount = val.d.length;
                                        var diff = messageage(val.d[lastmsgcount - 1].Time);
                                        if (diff <= 300) {
                                            if (chatinterval == null || chatinterval == "") {
                                                if (_groupId == 0) {
                                                    startchattimer();
                                                    updateischaton();
                                                }
                                            }
                                        }
                                    }
                                }
                                element = '<li class=" chatcolorgreen"><span class="spanlabel ">' + val.d[i].PeerName + ':' + '</span><br/>   <div class="clearfix divconvrsation" style="margin-left: 50px; margin-right: 30px;">' + convrstn + '</div><span class="pull-right" style="color:#777;padding-right:8px;">' + val.d[i].Time + '</span><br/></li>';
                            }
                            $('.chat').append(element);

                            if (val.d[i].TotalChatDuration != null) {
                                totaltxtchatduration = val.d[i].TotalChatDuration;
                            }
                            else {
                                totaltxtchatduration = "";
                            }
                        }

                    } else {
                        $("#btnexportchat").attr("disabled", "disabled");
                        $("#btnexportchat").addClass("btndisabled");

                        $("#btnclearlog").attr("disabled", "disabled");
                        $("#btnclearlog").addClass("btndisabled");

                        var display = "You have cleared your recent chat history.";
                        var ele = '<li class="msgclrchat"> <div class="clearfix" style="font-weight:bold;">' + display + '</div></li>';
                        $('.chat').append(ele);
                    }
                }
                if (isunreadmsg) {
                    document.getElementById('textChatMessages').scrollTop = document.getElementById('textChatMessages').scrollHeight;
                }
            },
            error: function (err) {

                //alert(conversationlogid);
                //alert(err);
            },
            complete: function () {

                if (_tempConversationid != _conversationid) {
                    _tempConversationid = _conversationid;
                    //var objDiv = $(document).find("div[class~=textChatMessages]"); //document.getElementById("textChatMessages");
                    //$(objDiv).prop("scrollTop", $(objDiv).prop("scrollHeight"));
                    var objDiv = document.getElementById("textChatMessages");
                    objDiv.scrollTop = objDiv.scrollHeight;
                }
                if (_groupId != '' && _groupId != 0) {
                    receiveVideo1();
                    receiveVideo2();
                }
                else {
                    receiveVideo();
                }

                startMeassageTimer();
            }


        });
    }
}


function brodcastvideo() {
    $.ajax({
        type: "POST",
        url: "HickChatEngine.svc/BroadcastVideo",
        data: '{"conversationid":"' + _videoConversationId + '", "peerid":"' + userid + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            startVideoTimer();
        },
        success: function (result) {
            if (result.d.BroadcastVideoGuid) {

                var currentVideoBroadcastGuid = String(result.d.BroadcastVideoGuid);

                _currentBroadcastId = currentVideoBroadcastGuid;

                _guid = currentVideoBroadcastGuid;
                _tempbroadcastvideoguid = currentVideoBroadcastGuid;
                var flashMediaServer = String(result.d.FlashMediaServer);
                _flshmediaserver = flashMediaServer;
                var broadcastVideoWidth = String(result.d.BroadcastVideoWidth);
                var broadcastVideoHeight = String(result.d.BroadcastVideoheight);
                var videoWindow = $('#broadcastVideoWindowTemplate').jqote({ Guid: currentVideoBroadcastGuid, FlashMediaServer: flashMediaServer, BroadcastVideoWidth: broadcastVideoWidth, BroadcastVideoHeight: broadcastVideoHeight });
                $('#divlogedinuser').html("");
                $('#divlogedinuser').append(videoWindow);
                $('#divlogedinuser').css("background-image", "");
                $('#divlogedinuser').css("background-color", "black");
                $('#divBroadcastVideo').css("height", "227px");

                $("#videocurrentuser").html($("#loginname").html());
                // $("#videoremoteuser").html(remoteusername);

                //groupVideoTimeout

                if (_groupId != '' && _groupId != 0) {
                    $('#videoremoteuser2').css('display', 'block');
                    groupVideoInterval = setInterval(function () {
                        receiveVideo1();
                        receiveVideo2();
                    }, 5000);
                }
            }
            else {
                $('#videoChatContainer').css('display', 'none');
                $('.textChatMessages').css('top', '10px');
                $("#divlogedinuser").empty();
                $("#divpeeruser").empty();
                $("#thirdVideoUser").empty();
            }
        },
        complete: function () {
            if (_groupId != '' && _groupId != 0) {
                receiveVideo1();
                receiveVideo2();
            }
            else {
                receiveVideo();
            }
            //startVideoTimer();
        }
    });
}



function receiveVideo() {
    var chatLists = $('#divcontacttable table tbody');
    var actpeerId = $('#msgtopactpeerId').val();
    var truid = "user" + _peerid
    if (actpeerId != "") {
        truid = "user" + actpeerId
    }
    var tabDivLast = $('#divcontacttable table tbody tr').filter('tr[uid="' + truid + '"]');
    var topLastDiv = chatLists.children(':first');
    topLastDiv.before(tabDivLast);
    
    $.ajax({
        type: "POST",
        url: "HickChatEngine.svc/ReceiveVideo",
        data: '{"conversationid":"' + _videoConversationId + '", "peerid":"' + _videoPeerId + '", "currentuserid":"' + userid + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            startVideoTimer();
        },
        success: function (result) {
            $('#videoduration').css("display", "Block");
            if (result.d.ReceiveVideoGuid) {

                if (_tempreceivevideoguid != result.d.ReceiveVideoGuid) {
                    _receivevideoguid = String(result.d.ReceiveVideoGuid);
                    _tempreceivevideoguid = String(result.d.ReceiveVideoGuid);
                    //blink();
                    $("#videochat").click();
                    $('#divpeeruser').hide();
                    // $("#divacceptbtn").show();
                    window.setTimeout(function () {
                        $("#btnreceivevideo").click();
                    }, 5000);


                    //$(".videoremoteuser").css('margin-top', '0px');
                    $("#videoremoteuser").html(remoteusername);

                    //                    $.ajax({
                    //                            type:"POST",
                    //                            url:"HickChatEngine.svc/VideoDuration",
                    //                            data:'{"currentuserid":"'+userid+'","peerid":"'+_peerid+'"}',
                    //                            contentType:"application/json; charset=utf-8",
                    //                            success:function(msg){
                    //                                $("#videoduration").html(msg.d);
                    //                            }
                    //                        });

                    //                    tictac() ;                              
                    //timer = 0;
                    interval = window.setInterval('tictac()', 1000);
                    $('#msgtopactpeerId').val(_peerid);
                    var chatLists = $('#divcontacttable table tbody');
                    var truid = "user" + _peerid
                    var tabDivLast = $('#divcontacttable table tbody tr').filter('tr[uid="' + truid + '"]');
                    var topLastDiv = chatLists.children(':first');
                    topLastDiv.before(tabDivLast);
                }
            }
            else {

                removeBlink();
                //$("#videoremoteuser").html(remoteusername + " is not connected.");

                if (result.d.BroadcastVideoGuid) {
                    if (_tempbroadcastvideoguid != result.d.BroadcastVideoGuid) {
                        _tempbroadcastvideoguid = result.d.BroadcastVideoGuid;
                        $('#divpeeruser').hide();
                        $("#divacceptbtn").hide();

                        interval = window.clearInterval(interval);
                        timer = 0;
                    }
                    $("#videoremoteuser").html("Calling.......");
                }
                else {
                   
                    removeBlink();
                    $('#videoChatContainer').css('display', 'none');
                    $('.textChatMessages').css('top', '10px');
                    $("#divlogedinuser").empty();
                    $("#divpeeruser").empty();
                    $("#videochat").unbind("click");
                    $("#videochat").bind("click", function () {
//                        $("#divdialpad").css("display", "none");
//                        $("#textContainer").css("display", "block");
                        hideDialerPad();
                        document.getElementById('videoduration').innerHTML = ("00") + ":" + ("00") + ":" + ("00");
                        showVideoFrame();
                    });
                    _tempreceivevideoguid = '';
                    _tempbroadcastvideoguid = '';
                    // $(".videoremoteuser").css('margin-top', '230px');
                    interval = window.clearInterval(interval);
                    timer = 0;
                    document.getElementById('videoduration').innerHTML = ("00") + ":" + ("00") + ":" + ("00");
                    stopVideoTimer();
                    var userType = $('#UserUserType').val();
                   
                    $('#videoduration').hide();
                    if (_isvideocall && _userType.toLowerCase() != "patient") {
                        VideoCallSession();
                    }

                    _isvideocall = false;
                }
            }

        }
    });
}
function stopVideoChat(e) {

    $.ajax({
        type: "POST",
        url: "HickChatEngine.svc/StopVideoChat",
        data: '{"conversationid":"' + _videoConversationId + '","broadcastvideoid":"' + _tempbroadcastvideoguid + '","receivedvideoid":"' + _tempreceivevideoguid + '","groupid":"' + _groupId + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            stopMessageTimer();
        },
        success: function (result) {
            videoflag = false;
            $('#videoChatContainer').css('display', 'none');

            //frm abshk
            $('#thirdVideoUser').css('display', 'none');
            $('#textContainer').css('display', 'block');
            $('#videoremoteuser2').css('display', 'none');

            $('.textChatMessages').css('top', '10px');
            $("#divlogedinuser").empty();
            $("#divpeeruser").empty();
            $("#thirdVideoUser").empty();
            _tempreceivevideoguid = '';
            _tempbroadcastvideoguid = '';
            interval = window.clearInterval(interval);
            timer = 0;
            if (e != "signout") {
                if (_userType.toLowerCase() != "patient") {
                    VideoCallSession();
                }
            }
        },
        complete: function () {
            //            interval = window.clearInterval(interval);
            getMessages();
        }
    });
}
function blink() {
    $('#videochat').delay(100).fadeTo(100, 0.5).delay(100).fadeTo(100, 1, blink);
}
function removeBlink() {
    $('#videochat').stop(true, true).fadeTo("fast", 1);
}
function msgNotification() {
    $(".imgmsgnotification").delay(100).fadeTo(100, 0.5).delay(100).fadeTo(100, 1, msgNotification);
    $(".imgvideonotification").delay(100).fadeTo(100, 0.5).delay(100).fadeTo(100, 1, msgNotification);

}

function showReceiverVideo() {

    if (_groupId != '' && _groupId != 0) {

        if (_receivevideoguid1 != '' && _videoPeerId1 != 0) {

            var flashMediaServer1 = _flshmediaserver;
            var senderUserId1 = _videoPeerId1;
            var currentVideoBroadcastGuid1 = _receivevideoguid1;
            var videoWindow1 = $('#receiveVideoWindowTemplate').jqote({ SenderUserId: senderUserId1, Guid: currentVideoBroadcastGuid1, FlashMediaServer: flashMediaServer1 });

            $('#divpeeruser').html("");
            $('#divpeeruser').show();
            $("#divacceptbtn").hide();

            $('#divpeeruser').append(videoWindow1);
            $('#divReceiveVideo' + senderUserId1 + '').css('height', '227px');
            $('#divpeeruser').css("background-image", "");
            $('#divpeeruser').css("background-color", "black");
            removeBlink();
        }



        //frm abshk
        if (_receivevideoguid2 != '' && _videoPeerId2 != 0) {
            //$('#thrdVidUsr').show();

            var flashMediaServer2 = _flshmediaserver;
            var senderUserId2 = _videoPeerId2;
            var currentVideoBroadcastGuid2 = _receivevideoguid2;
            var videoWindow2 = $('#receiveVideoWindowTemplate').jqote({ SenderUserId: senderUserId2, Guid: currentVideoBroadcastGuid2, FlashMediaServer: flashMediaServer2 });

            $('#thirdVideoUser').html("");
            $('#thirdVideoUser').show();
            $("#divacceptbtn").hide();

            $('#thirdVideoUser').append(videoWindow2);
            $('#divReceiveVideo' + senderUserId2 + '').css('height', '227px');
            $('#thirdVideoUser').css("background-image", "");
            $('#thirdVideoUser').css("background-color", "black");
            removeBlink();
        }
    }
    else {
        var flashMediaServer = _flshmediaserver;
        var senderUserId = _videoPeerId;
        var currentVideoBroadcastGuid = _receivevideoguid;
        var videoWindow = $('#receiveVideoWindowTemplate').jqote({ SenderUserId: senderUserId, Guid: currentVideoBroadcastGuid, FlashMediaServer: flashMediaServer });

        $('#divpeeruser').html("");
        $('#divpeeruser').show();
        $("#divacceptbtn").hide();

        $('#divpeeruser').append(videoWindow);
        $('#divReceiveVideo' + senderUserId + '').css('height', '227px');
        $('#divpeeruser').css("background-image", "");
        $('#divpeeruser').css("background-color", "black");
        removeBlink();
    }    //end

}

function showVideoFrame() {
    var initiateMSGDateTime = new Date();
    initiateMSGDateTime = moment(initiateMSGDateTime).format('YYYY-MM-DD HH:mm:ss');
    if ($('#InitiateMSGDate').val() == "") {
        $('#InitiateMSGDate').val(initiateMSGDateTime);
    }
    _isvideocall = true;
    _videoConversationId = _conversationid;
    if (_groupId != '' && _groupId != 0) {

        if (UsersArray.length <= 2) {


            if (UsersArray.length == 2) {
                $('#thirdVideoUser').css('display', 'block');
                $('#thirdVideoUser').css("background-color", "gray");
                $('#thrdVidUsr').css('display', 'block');
                $('#videoremoteuser2').css('display', 'block');
                //frm absk
                $('#textContainer').css('display', 'none');
            }
            else {
                $('#thrdVidUsr').css('display', 'none');
                $('#videoremoteuser2').css('display', 'none');
                //frm absk
                $('#textContainer').css('display', 'block');
            }

            $('#videoChatContainer').css('display', 'block');
            $('.textChatMessages').css('top', '300px');
            $('.textChatMessages').css('bottom', '60px');
            $("#videochat").unbind("click");
            removeBlink();
            $.jqotetag('*');
            brodcastvideo();

        }
        else {
            alert("Sorry, video call is restricted to three users only");

        }

        //        $.ajax({
        //            type: "POST",
        //            asyn: false,
        //            url: "HickChatEngine.svc/FetchUsersByGroupId",
        //            data: '{"groupID":"' + _groupId + '"}',
        //            contentType: "application/json; charset=utf-8",
        //            dataType: "json",
        //            success: function (result) {
        //                if (result != null && result.d.length > 0) {
        //                    for (var i = 0; i < result.d.length; i++) {
        //                        if (result.d[i].UserId != userid)
        //                        {
        //                            UsersArray.push(result.d[i].UserId);
        //                            UserNamesArray.push(result.d[i].FullName);
        //                        }
        //                        _videoPeerId1=UsersArray[0];
        //                        _videoPeerId2=UsersArray[1];

        //                        remoteusername1 = UserNamesArray[0];
        //                        remoteusername2 = UserNamesArray[1];
        //                    }

        //                    if(result.d.length>2)
        //                    {
        //                        $('#thirdVideoUser').css('display', 'block');
        //                        $('#thirdVideoUser').css("background-color", "gray");
        //                        $('#thrdVidUsr').css('display', 'block');
        //                        $('#videoremoteuser2').css('display', 'block');
        //                    }
        //                    else
        //                    {
        //                        $('#thrdVidUsr').css('display', 'none');
        //                        $('#videoremoteuser2').css('display', 'none');
        //                    }

        //                     $('#videoChatContainer').css('display', 'block');

        //                        //frm absk
        //                        $('#textContainer').css('display', 'none');

        //                        $('.textChatMessages').css('top', '300px');
        //                        $('.textChatMessages').css('bottom', '60px');
        //                      $("#videochat").unbind("click");
        //                     removeBlink();
        //                       $.jqotetag('*');
        //                         brodcastvideo();
        //                  
        //                }

        //            },
        //            complete: function () {
        //            showReceiverVideo();              
        //            }
        //        });

    }
    else {
        _videoPeerId = _peerid;
        removeBlink();
        $("#videochat").unbind("click");
        var templateUrl = $('#templates').data('url');
        $('#templates').load(templateUrl, function () {
            videoflag = true;
            $('#videoChatContainer').css('display', 'block');

            //frm absk
            $('#thrdVidUsr').css('display', 'none');
            $('#videoremoteuser2').css('display', 'none');

            //$('#textContainer').css('display', 'none');

            $('.textChatMessages').css('top', '300px');
            $('.textChatMessages').css('bottom', '60px');
            // frm abshk end

            // Set default jqote tag
            $.jqotetag('*');
            brodcastvideo();
        });

    }
}
$(function () {
    $("#chatlog").click('click', function () {


        var url = "Chat_log.aspx";
        window.location = (url);
    });

    $("#chatlog1").click('click', function () {
        var hashes = '';

        if (window.location.href.indexOf('?') != -1) {
            hashes = window.location.href.slice(window.location.href.indexOf('?'));

        }

        var url = "Chat_log.aspx";
        window.location = url + hashes;
    });

    $("#auditlog").click('click', function () {
        var hashes = '';

        if (window.location.href.indexOf('?') != -1) {
            hashes = window.location.href.slice(window.location.href.indexOf('?'));

        }

        var url = "AuditLog.aspx?peerid="+_peerid;
        window.location = url + hashes;
    });

    $("#home").click('click', function () {
        var hashes = '';
        if (window.location.href.indexOf('?') != -1) {
            hashes = window.location.href.slice(window.location.href.indexOf('?'));
        }
        var url = "Chat.aspx";
        window.location = url + hashes;
    });
    $("#videochat").click(function () {

     if (_isaudioCall == false) {
         //frm abshk
         _isvideocall = true;
         //frm abshk end
         $("#divdialpad").css("display", "none");

         if (_conversationid) {
             showVideoFrame();
         }

         //$('#videoChatContainer').css('display', 'block');

         //$('.textChatMessages').css('top', '300px');


         ////$(".chat").animate({ scrollDown: $(".chat")[0].scrollHeight });
         //$("#videocurrentuser").text(currentuser);


         ////$("#textChatMessages").slideDown(500000);
         ////$("#textContainer").slideUp("slow");
     }

    });
    $("#btnreceivevideo").click(function () {

        showReceiverVideo();
        return false;
    });
    $("#endcall").click('click', function () {
        stopVideoChat();
        removeBlink();

        //frm abshk
        _isvideocall = false;
        //frm abshk end

        $("#videochat").bind("click", function () {
            showVideoFrame();
        });
        //$(".videoremoteuser").css('margin-top', '230px');

        _receivevideoguid1 = '';
        _receivevideoguid2 = '';


    });



});

function uploadfiles() {

    if (isImage) {
        var sendurl = "SendFile.ashx?conversationid=" + _conversationid + "&username=" + currentuser + "&flag=image";
    }
    else {
        var sendurl = "SendFile.ashx?conversationid=" + _conversationid + "&username=" + currentuser + "&flag=doc";
    }

    $.ajax({
        type: "POST",
        url: "",
        data: {},
        beforeSend: function () {
            isFileUploading = true;
            var fullFileName = $("#fileUpload").val();
            var fileName = fullFileName.substr(fullFileName.lastIndexOf("\\") + 1, fullFileName.length);
            var convrstn = fileName;
            convrstn = convrstn + "</br>" + "<div><img src='Images/fileupload-loader.gif' style='background-color:#E4E4E4;' /></div><span class='lnkbutton lnkcancel'>Cancel</span>"

            var element = '<li class="chatcolorgrey"><span class=" spanlabel ">' + currentuser + ':' + '</span><br/><div class="" style="margin-left: 50px;">' + convrstn + '</div><span class="pull-right" style="color: #777;padding-right:8px;">' + time + '</span><br/></li>';
            $('.chat').append(element);
            var objDiv = document.getElementById("textChatMessages");
            objDiv.scrollTop = objDiv.scrollHeight;
        },
        complete: function () {
            if (!cancelFileUpload) {
                window.setTimeout(function () {
                    $.ajaxFileUpload({
                        url: sendurl,
                        secureuri: false,
                        fileElementId: 'fileUpload',
                        dataType: 'json',
                        success: function (data, status) {
                            if (typeof (data.error) != 'undefined') {
                                var ary = data.error.split('|');
                                if (ary[0] != '') {
                                    alert(data.error);
                                } else {
                                    if (!cancelFileUpload) {
                                        saveMessage(ary[1], 3);
                                    }

                                }
                            }

                        },
                        error: function (data, status, e) {
                            isFileUploading = false;
                            alert(e);
                        },
                        complete: function () {
                            isFileUploading = false;
                            cancelFileUpload = false;
                            messageCount = 0;
                        }
                    });

                }, 5000);
            }
            else {
                isFileUploading = false;
                cancelFileUpload = false;
                messageCount = 0;
            }

        },

    });


}

function receiveDocuments(filename, _logid, prnam) {

    var recstr = "ReceiveFile.ashx?filename=" + filename + "&peername=" + prnam + "&username=" + currentuser;

    $.ajax({
        type: "POST",
        url: "",
        data: '{}',
        success: function (result) {
            isdownloadfailed = 0;
            $("#diviframe").html("");
            $("#diviframe").append($('<iframe id="frmdownload" src="' + recstr + '"></iframe>')).hide();
        },
        complete: function () {
            window.setTimeout(function () {
                if (isdownloadfailed == 0) {
                    updateFileReceivedStatus(_logid, 5);
                }
                else if (isdownloadfailed == 1) {
                    updateFileReceivedStatus(_logid, 4);
                }
                else if (isdownloadfailed == 2) {
                    isFileUploading = false;
                }

            }, 2000);

            // updateFileReceivedStatus(_logid, 5);
            // $("#diviframe").html("");
        },
        error: function () {

        }
    });

}
function updateFileReceivedStatus(_logid, _status) {

    $.ajax({
        type: "POST",
        url: "HickChatEngine.svc/UpdateFileReceivedStatus",
        data: '{"conversationid":"' + _conversationid + '","curentuserid":"' + userid + '","logid":"' + _logid + '","status":"' + _status + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            messageCount = 0;
        },
        complete: function () {
            isFileUploading = false;
        }
    });
}
function clearChatLog() {
    var _clrConvid = 0;
    if (_prevConversationId != 0) {
        _clrConvid = _prevConversationId + ',' + _conversationid;
    }
    else {
        _clrConvid = _conversationid;
    }
    $.ajax({
        type: "POST",
        url: "HickChatEngine.svc/ClearChatLog",
        data: '{"conversationid":"' + _clrConvid + '","currentuserid":"' + userid + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d == "error") {
                alert("Sorry an error has occured. Please contact administrator");
            }
        },
        complete: function () {
            _prevConversationId = 0;
            getMessages();
        },
        error: function () {
            alert("Sorry an error has occured. Please contact administrator");
        }
    });
}
function exportChat(mesagetype) {

    var sendurl = "ExportChat.ashx?conversationid=" + _conversationid + "&currentuserid=" + userid + "&ischatwindow=yes&messagetype=" + mesagetype + "";


    $("#diviframe").html("");
    $("#diviframe").append($('<iframe id="frmdownload" src="' + sendurl + '"></iframe>')).hide();
    //$.ajax({
    //    type: "POST",
    //    url: '',
    //    data: '{}',
    //    success: function (result) {
    //        $("#diviframe").html("");
    //        $("#diviframe").append($('<iframe id="frmdownload" src="' + sendurl + '"></iframe>')).hide();
    //    },
    //    complete: function () {
    //    },
    //    error: function (er) {
    //        alert("Sorry an error has occured. Please contact administrator");
    //    }
    //});
}

function formatSymbolsToText(str) {
    if (str) {
        if (str.indexOf('<') != -1) {
            str = str.split('<').join('&lt;');
        }
        if (str.indexOf('>') != -1) {
            str = str.split('>').join('&gt;');
        }
        if (str.indexOf('&nbsp;') != -1) {
            str = str.split('&nbsp;').join(' ');
        }
        if (str.indexOf('\'') != -1) {
            str = str.split('\'').join('&#39;');
        }

        return str;
    }
    else {
        return '';
    }
}

function getLocalTime(datetime, isutc) {

    var d = isutc == true ? new Date(datetime + " UTC") : datetime;
    var _hours = d.getHours();
    var _minutes = d.getMinutes();
    if (_minutes < 10) {
        _minutes = '0' + _minutes;
    }
    var ampm = 'AM';
    if (_hours == 0) { //At 00 hours we need to show 12 am
        _hours = 12;
    }
    else if (_hours > 11) {
        if (_hours > 12)
            _hours = _hours % 12;
        ampm = 'PM';
    }
    _hours = _hours < 10 ? "0" + _hours : _hours;
    var _tim = _hours + ":" + _minutes + " " + ampm;
    return _tim;
}
function getTimeZone() {

    var timzonemints = new Date().getTimezoneOffset();
    $.ajax({
        type: "POST",
        url: "HickChatEngine.svc/GetUsersTimeZone",
        data: '{"ofsetminutes":"' + timzonemints + '"}',
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            if (msg.d) {
                _timezone = msg.d;
                setTimeZoneToSession(_timezone);
            }

        },
        error: function (err) {
            //alert(err);
        },
        complete: function () {

        }
    });
}
function setTimeZoneToSession(str) {
    $.ajax({
        type: "POST",
        url: "Chat.aspx/StoreTimeZoneToSession",
        data: '{"timezone":"' + str + '"}',
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            setLastLoggedInLable();
        },
        error: function (err) {
        },
        complete: function () {

        }
    });
}
function setLastLoggedInLable() {
    $.ajax({
        type: "POST",
        url: "Chat.aspx/GetLastLoggedInTime",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            if (msg.d) {
                $("#datetimepicker").html(msg.d);
                $("#divlastlogin").show();
            }


        },
        error: function (err) {
        },
        complete: function () {

        }
    });

}

function bindGroups(groupcoll) {
    if (groupcoll) {
        if (groupcoll.length > 0) {
            for (var i = 0; i < groupcoll.length; i++) {
                var userlist = '';
                for (var j = 0; j < groupcoll[i].GroupUsersColl.length; j++) {
                    userlist = userlist.length == 0 ? groupcoll[i].GroupUsersColl[j].FullName : userlist + ", " + groupcoll[i].GroupUsersColl[j].FullName;
                }

                var clsgrp = '';
                if (_srchtxt.length > 0) {
                    if (userlist) {
                        if (userlist.toLowerCase().indexOf(_srchtxt.toLowerCase()) == -1) {
                            clsgrp = "hide-element";
                        }
                        else {
                            contactsCount += 1;
                        }
                    }
                }
                else {
                    contactsCount += 1;
                }

                // template = "<tr id='tableRow' data-row-key='${User}' class='tableRow' style='cursor:pointer;'> <td id='userThumbnail'><img id='imgOption' src='" + userphoto + "'></img><img id='status' src='" + onlineuser + "'></img></td> <td>   <span class='userselct' id='userName'>" + msg.d[i].Firstname + "</span>&nbsp;<span class='userselctlastname'>" + msg.d[i].Lastname + "</span><div class='small1' id='statusmessage'>" + msg.d[i].StatusMessage + "</div>  <div class='small' id='session'>" + msg.d[i].ID + "</div><div class='small divusername'>" + msg.d[i].Username + "</div> </td><td><img class='favorite-img action-icon' title='Favorite Icon' src='" + _favoriteIcon + "'/><img class='addtogroup action-icon "+grclas+"' title='Add To Group' src='Images/add-user.png' uid='"+msg.d[i].ID+"'/> </td><td> <div style='float:right;'>  </div> </td>  </tr>";
                //old code
//                if (groupcoll[i].IncomingCall == true && !$('#videoChatContainer').is(":visible")) {
//                    template = "<tr id='tableRow' data-row-key='${User}' class='tableRow " + clsgrp + "' style='cursor:pointer;' groupid=" + groupcoll[i].Id + "> <td id='userThumbnail' style='height:70px;'><img id='imgOption' src='images/group_icon.png' style='width:50px; height:50px;'></img><img id='status' src='Images/option_unselected.png'></img></td> <td valign='top'><span id='" + groupcoll[i].Id + "' class='userselct grpusers' >" + userlist + "</span><div class='small1' id='statusmessage'></div></td><td valign='top' class='action-icon-container'> <img class='imgvideonotification action-icon' title='Incomming Video Call' src='Images/call-incoming-icon.png'/> </td></tr>";
//                }
//                else {
//                    template = "<tr id='tableRow' data-row-key='${User}' class='tableRow " + clsgrp + "' style='cursor:pointer;' groupid=" + groupcoll[i].Id + "> <td id='userThumbnail' style='height:70px;'><img id='imgOption' src='images/group_icon.png' style='width:50px; height:50px;'></img><img id='status' src='Images/option_unselected.png'></img></td> <td valign='top'><span id='" + groupcoll[i].Id + "' class='userselct grpusers' >" + userlist + "</span><div class='small1' id='statusmessage'></div></td> <td></td></tr>";
//                }

                //new code
                //template = "<tr id='tableRow' data-row-key='${User}' class='tableRow " + clsgrp + "' style='cursor:pointer;' groupid=" + groupcoll[i].Id + "> <td id='userThumbnail' style='height:70px;'><img id='imgOption' src='images/group_icon.png' style='width:50px; height:50px;'></img><img id='status' src='Images/option_unselected.png'></img></td> <td valign='top'><span id='" + groupcoll[i].Id + "' class='userselct grpusers' >" + userlist + "</span><div class='small1' id='statusmessage'></div></td><td valign='top' class='action-icon-container'>";
                if ($(window).width() < 768) {
               
                        var trimmed_userlist;
                    
                        if ((userlist.length) > 25) {
                            trimmed_userlist = userlist.substring(0, 25) + "...";
                        }
                        else {
                            trimmed_userlist = userlist;
                        }

                        template = "<tr id='tableRow' data-row-key='${User}' class='tableRow " + clsgrp + "' style='cursor:pointer;' groupid=" + groupcoll[i].Id + "> <td id='userThumbnail' style='height:70px;'><img id='imgOption' src='images/group_icon.png' style='width:50px; height:50px;'></img><img id='status' src='Images/option_unselected.png'></img></td> <td class='grp_usrs'><span id='" + groupcoll[i].Id + "' class='userselct grpusers' >" + trimmed_userlist + "</span><div class='small1' id='statusmessage'></div></td><td valign='top' class='action-icon-container'>";
                }                
                else {
                        template = "<tr id='tableRow' data-row-key='${User}' class='tableRow " + clsgrp + "' style='cursor:pointer;' groupid=" + groupcoll[i].Id + "> <td id='userThumbnail' style='height:70px;'><img id='imgOption' src='images/group_icon.png' style='width:50px; height:50px;'></img><img id='status' src='Images/option_unselected.png'></img></td> <td class='grp_usrs'><span id='" + groupcoll[i].Id + "' class='userselct grpusers' >" + userlist + "</span><div class='small1' id='statusmessage'></div></td><td valign='top' class='action-icon-container'>";
                }

                if (groupcoll[i].IncomingCall == true && !$('#videoChatContainer').is(":visible")) {
                   template =template + "<img class='imgvideonotification action-icon' title='Incomming Video Call' src='Images/call-incoming-icon.png'/>";
               }
                if (groupcoll[i].IsUnreadMessage == true){
                  template = template + "<img class='imgmsgnotification action-icon' title='New Message' src='Images/messages-icon.png'/>";
                }
               template=template+"</td></tr>";

                usersTemplate = usersTemplate + template;
            }

        }
    }

    $('#table tbody').html("");
    if (contactsCount > 0) {
        $('#table tbody').append(usersTemplate);
    }
    else {
        $('#table tbody').append("<tr><td>No contacts found</td></tr>");
    }

    msgNotification();

    $("#table").find("tr[class~=tableRow]").each(function () {

        if ($(this).attr("groupid")) {

            if ($(this).attr("groupid") == _groupId) {
                $('#username').html($(this).find("span[id=" + _groupId + "]").html());
                $('#usericon').attr("src", $(this).find("img[id=imgOption]").attr("src"));
                $('#userstatus').hide();
                $('#statusdiv').hide();
                return false;
            }
        }

    });
}


//function getGroups() {
//    $.ajax({
//        type: "POST",
//        url: "HickChatEngine.svc/GetGroups",
//        data: '{"currentuserid":"' + userid + '"}',
//        contentType: "application/json; charset=utf-8",
//        success: function (msg) {
//            if (msg.d) {
//                if (msg.d.length > 0) {
//               
//                    for (var i = 0; i < msg.d.length; i++) {
//                        var userlist = '';
//                        for (var j = 0; j < msg.d[i].GroupUsersColl.length; j++) {
//                            userlist = userlist.length == 0 ? msg.d[i].GroupUsersColl[j].FullName : userlist + ", " + msg.d[i].GroupUsersColl[j].FullName;
//                        }

//                        var clsgrp = '';
//                        if (_srchtxt.length > 0) {
//                            if (userlist) {
//                                if (userlist.toLowerCase().indexOf(_srchtxt.toLowerCase()) == -1) {
//                                    clsgrp = "hide-element";
//                                }
//                                 else{
//                        contactsCount+=1;
//                        }
//                            }
//                        }
//                         else{
//                        contactsCount+=1;
//                        }

//                        // template = "<tr id='tableRow' data-row-key='${User}' class='tableRow' style='cursor:pointer;'> <td id='userThumbnail'><img id='imgOption' src='" + userphoto + "'></img><img id='status' src='" + onlineuser + "'></img></td> <td>   <span class='userselct' id='userName'>" + msg.d[i].Firstname + "</span>&nbsp;<span class='userselctlastname'>" + msg.d[i].Lastname + "</span><div class='small1' id='statusmessage'>" + msg.d[i].StatusMessage + "</div>  <div class='small' id='session'>" + msg.d[i].ID + "</div><div class='small divusername'>" + msg.d[i].Username + "</div> </td><td><img class='favorite-img action-icon' title='Favorite Icon' src='" + _favoriteIcon + "'/><img class='addtogroup action-icon "+grclas+"' title='Add To Group' src='Images/add-user.png' uid='"+msg.d[i].ID+"'/> </td><td> <div style='float:right;'>  </div> </td>  </tr>";
//                        if (msg.d[i].IncomingCall == true && !$('#videoChatContainer').is(":visible")) {
//                            template = "<tr id='tableRow' data-row-key='${User}' class='tableRow " + clsgrp + "' style='cursor:pointer;' groupid=" + msg.d[i].Id + "> <td id='userThumbnail' style='height:70px;'><img id='imgOption' src='images/group_icon.png' style='width:50px; height:50px;'></img><img id='status' src='Images/option_unselected.png'></img></td> <td valign='top'><span id='" + msg.d[i].Id + "' class='userselct grpusers' >" + userlist + "</span><div class='small1' id='statusmessage'></div></td><td valign='top' class='action-icon-container'> <img class='imgvideonotification action-icon' title='Incomming Video Call' src='Images/call-incoming-icon.png'/> </td></tr>";
//                        }
//                        else {
//                            template = "<tr id='tableRow' data-row-key='${User}' class='tableRow " + clsgrp + "' style='cursor:pointer;' groupid=" + msg.d[i].Id + "> <td id='userThumbnail' style='height:70px;'><img id='imgOption' src='images/group_icon.png' style='width:50px; height:50px;'></img><img id='status' src='Images/option_unselected.png'></img></td> <td valign='top'><span id='" + msg.d[i].Id + "' class='userselct grpusers' >" + userlist + "</span><div class='small1' id='statusmessage'></div></td> <td></td></tr>";
//                        }
//                        usersTemplate = usersTemplate + template;
//                    }

//                }
//            }
//            
//             $('#table tbody').html("");
//            if(contactsCount>0){
//            $('#table tbody').append(usersTemplate);
//            }
//            else{
//             $('#table tbody').append("<tr><td>No contacts found</td></tr>");
//            }

//            msgNotification();

//            $("#table").find("tr[class~=tableRow]").each(function () {

//                if ($(this).attr("groupid")) {

//                    if ($(this).attr("groupid") == _groupId) {
//                        $('#username').html($(this).find("span[id=" + _groupId + "]").html());
//                        $('#usericon').attr("src", $(this).find("img[id=imgOption]").attr("src"));
//                        $('#userstatus').hide();
//                        $('#statusdiv').hide();
//                        return false;
//                    }
//                }

//            });
//        },
//        error: function (err) {
//            //alert(err);
//        },
//        complete: function () {
//        }
//    });
//}


function receiveVideo1() {
    $.ajax({
        type: "POST",
        url: "HickChatEngine.svc/ReceiveVideo",
        asyn: false,
        data: '{"conversationid":"' + _videoConversationId + '", "peerid":"' + _videoPeerId1 + '", "currentuserid":"' + userid + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {

            if (result.d.ReceiveVideoGuid) {

                if (_tempreceivevideoguid1 != result.d.ReceiveVideoGuid) {
                    _receivevideoguid1 = String(result.d.ReceiveVideoGuid);
                    _tempreceivevideoguid1 = String(result.d.ReceiveVideoGuid);

                    $("#videochat").click();
                    $('#divpeeruser').hide();

                    window.setTimeout(function () {
                        $("#btnreceivevideo").click();
                    }, 5000);

                    $("#videoremoteuser").html(remoteusername1);
                }
            }
            else {

                removeBlink();
                if (result.d.BroadcastVideoGuid) {
                    if (_tempbroadcastvideoguid != result.d.BroadcastVideoGuid) {
                        _tempbroadcastvideoguid = result.d.BroadcastVideoGuid;
                        $('#divpeeruser').hide();
                        $("#divacceptbtn").hide();

                    }
                    $("#videoremoteuser").html("Calling.......");
                }
                else {
                    removeBlink();
                    $('#videoChatContainer').css('display', 'none');
                    $('.textChatMessages').css('top', '10px');
                    $("#divlogedinuser").empty();
                    $("#divpeeruser").empty();
                    $("#videochat").unbind("click");
                    $("#videochat").bind("click", function () {
                        showVideoFrame();
                    });
                    _tempreceivevideoguid1 = '';
                    _tempreceivevideoguid1 = '';
                   
                    // $(".videoremoteuser").css('margin-top', '230px');
                    $('#textContainer').css('display', 'block');

                    _isvideocall = false;
                }
            }

        }
    });
}

function receiveVideo2() {
    $.ajax({
        type: "POST",
        url: "HickChatEngine.svc/ReceiveVideo",
        asyn: false,
        data: '{"conversationid":"' + _videoConversationId + '", "peerid":"' + _videoPeerId2 + '", "currentuserid":"' + userid + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {

            if (result.d.ReceiveVideoGuid) {

                if (_tempreceivevideoguid2 != result.d.ReceiveVideoGuid) {
                    _receivevideoguid2 = String(result.d.ReceiveVideoGuid);
                    _tempreceivevideoguid2 = String(result.d.ReceiveVideoGuid);

                    $("#videochat").click();
                    $('#thirdVideoUser').hide();

                    window.setTimeout(function () {
                        $("#btnreceivevideo").click();
                    }, 5000);

                    $("#videoremoteuser2").html(remoteusername2);
                }
            }
            else {

                removeBlink();
                if (result.d.BroadcastVideoGuid) {
                    if (_tempbroadcastvideoguid != result.d.BroadcastVideoGuid) {
                        _tempbroadcastvideoguid = result.d.BroadcastVideoGuid;
                        $('#thirdVideoUser').hide();
                        $("#divacceptbtn").hide();

                    }
                    $("#videoremoteuser2").html("Calling.......");
                }
                else {
                    removeBlink();
                    $('#videoChatContainer').css('display', 'none');
                    $('.textChatMessages').css('top', '10px');
                    $("#divlogedinuser").empty();
                    $("#thirdVideoUser").empty();
                    $("#videochat").unbind("click");
                    $("#videochat").bind("click", function () {
                        showVideoFrame();
                    });
                    _tempreceivevideoguid2 = '';
                    _tempreceivevideoguid2 = '';
                   
                    // $(".videoremoteuser").css('margin-top', '230px');
                    $('#textContainer').css('display', 'block');
                    _isvideocall = false;
                }
            }

        }
    });
}

//M
var getGUID = (function () {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000)
                   .toString(16)
                   .substring(1);
    }
    return function () {
        return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
               s4() + '-' + s4() + s4() + s4();
    };
})();

function getgroupUsers() {
    UsersArray = [];
    UserNamesArray = [];
    $.ajax({
        type: "POST",
        url: "HickChatEngine.svc/FetchUsersByGroupId",
        data: '{"groupID":"' + _groupId + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result != null && result.d.length > 0) {
                for (var i = 0; i < result.d.length; i++) {
                    if (result.d[i].UserId != userid) {
                        UsersArray.push(result.d[i].UserId);
                        UserNamesArray.push(result.d[i].FullName);
                    }
                    _videoPeerId1 = UsersArray[0];
                    _videoPeerId2 = UsersArray[1];

                    remoteusername1 = UserNamesArray[0];
                    remoteusername2 = UserNamesArray[1];
                }
            }

        }
    });
}
function resetContacts() {
    stopEventsTimer();
    renderTable();
    //    startEventsTimer();
}
function hideDialerPad() {
    $("#textContainer").show();
    $("#divdialpad").hide();
    if (_conversationid) {
 $("#divfooterright").show();
    }
     
}

function showDialerPad(_phNum) {
    $("#textContainer").hide();
    $("#divdialpad").show();    
    $("#divfooterright").hide();
    if (_isaudioCall == false) {
        $(".number").html(_phNum);
        if (_phNum) {
            $(".number").html(_phNum);
        }

    }

    //$(".number").html("+1");
}
function startEventsTimer() {
    //    eventsTimer = setInterval("renderTable()", 6000);
    //eventsTimer = setTimeout("renderTable()", 3000);
}

function stopEventsTimer() {
    clearInterval(eventsTimer);
}
function startMeassageTimer() {

    //    messageTimer = setInterval(function () {
    //        getMessages();
    //    }, 5000);

    messageTimer = setTimeout(function () {
        getMessages();
    }, 10000);
}

function startVideoTimer()
{
    videoTimer = setTimeout(function () {
        //if (_groupId != '' && _groupId != 0) {
        //    receiveVideo1();
        //    receiveVideo2();
        //}
        //else {
        //    receiveVideo();
        //}
        receiveVideo();
    }, 10000)
}
function stopVideoTimer() {
    clearInterval(videoTimer);
}
function stopMessageTimer() {
    clearInterval(messageTimer);
}

var timer = 0;
function tictac() {
    timer++;
    var hours = parseInt(timer / 3600);
    var minutes = (parseInt(timer / 60)) % 60;
    var seconds = timer % 60;

    document.getElementById('videoduration').innerHTML = ("0" + hours).slice(-2) + ":" + ("0" + minutes).slice(-2) + ":" + ("0" + seconds).slice(-2);
}

var renderFavouriteUsers = function (success, error) {

    $.ajax({
        type: "POST",
        url: "HickChatEngine.svc/GetFavouriteUsers",
        data: '{"currentuserid":"' + userid + '"}',
        contentType: "application/json; charset=utf-8",
        success: function (msg) {

            sqldata = msg.d;
            var usercoll = msg.d.UsersColl;
            var groupcoll = msg.d.GroupsColl;
            usersTemplate = '';
            contactsCount = 0;
            for (var i = 0; i < usercoll.length; i++) {

                if (usercoll[i].Status == 0) {
                    onlineuser = "Images/option_unselected.png";
                }
                else {
                    onlineuser = "Images/option_selected.png";
                }
                if (usercoll[i].Image == "") {
                    userphoto = "Images/default_user.png";
                }
                else {
                    userphoto = "Images/" + usercoll[i].Image;
                }

                /* setting the availability status for selected user  */
                if (_peerid == usercoll[i].ID) {
                    $('#userstatus').attr("src", onlineuser);
                    if (usercoll[i].Status == 0) {
                        $('#statusdiv').text("Offline");

                    }
                    else {
                        $('#statusdiv').text("Online");
                    }
                }

                if (usercoll[i].Favorites == 1) {
                    _favoriteIcon = "Images/heart-red.png";
                }
                else {
                    _favoriteIcon = "Images/heart-greay.png";
                }

                if (usercoll[i].ID == userid) {
                    template = null;
                }
                else {
                    var grclas = "hide-element";
                    if (isgroupChat) {
                        grclas = "";
                    }

                    var un = usercoll[i].Firstname + " " + usercoll[i].Lastname;
                    var refid = usercoll[i].ReferenceID;
                    var cls = '';
                    if (_srchtxt.length > 0) {
                        if (un) {
                            if (un.toLowerCase().indexOf(_srchtxt.toLowerCase()) == -1) {
                                cls = "hide-element";
                            }
                            else {
                                contactsCount += 1;
                            }
                        }
                    }
                    else {
                        contactsCount += 1;
                    }

                    template = "<tr id='tableRow' data-row-key='${User}' class='tableRow " + cls + "' style='cursor:pointer;'> <td id='userThumbnail'><img id='imgOption' src='" + userphoto + "'></img><img id='status' src='" + onlineuser + "'></img></td> <td>   <span class='userselct' id='userName'>" + usercoll[i].Firstname + "</span>&nbsp;<span class='userselctlastname'>" + usercoll[i].Lastname + "</span><div class='small1' id='statusmessage'>" + usercoll[i].StatusMessage + "</div>  <div class='small' id='session'>" + usercoll[i].ID + "</div><div class='small divusername'>" + usercoll[i].Username + "</div><span class='hide-element spnphnnumber' pnval=" + usercoll[i].PhoneNumber + "></span> <span class='hide-element spnrefId' refidval=" + usercoll[i].ReferenceID + "></span></td><td valign='top' class='action-icon-container'><img class='favorite-img action-icon heart-minheight' title='Favorite Icon' src='" + _favoriteIcon + "'/><img class='addtogroup action-icon " + grclas + "' title='Add To Group' src='Images/add-user.png' uid='" + usercoll[i].ID + "'/>";
                    if (usercoll[i].UnReadMessages == true) {
                        template = template + "<img class='imgmsgnotification action-icon' title='New Message' src='Images/messages-icon.png'/>";

                    }
                    if (usercoll[i].IncomingCall == true && !$('#videoChatContainer').is(":visible")) {
                        template = template + " <img class='imgvideonotification action-icon' title='Incomming Video Call' src='Images/call-incoming-icon.png'/> ";
                    }

                    template = template + "<span class='action-icon'>" + usercoll[i].VideoCallDuration + "</span> </td><td> <div style='float:right;'>  </div> </td>  </tr>";
                }
                usersTemplate = usersTemplate + template;
            }
            //bindGroups(groupcoll);

        },
        error: function (err) {
        },
        complete: function () {
        }
    });
}

function trackUsers() {
    $.ajax({
        type: "POST",
        url: "HickChatEngine.svc/TrackUsers",
        data: '{"currentuserid":"' + userid + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            startTrackUserTimer();
        }
    });
}
function startTrackUserTimer() {
    window.setTimeout(function () {
        trackUsers();
    }, 10000);
}

function logoff() {
    //idlecount = idlecount + 1;
    //if (idlecount >= 600) {
        if (_isvideocall == false || _isvideocall == undefined) {
            $.ajax({
                type: "POST",
                url: "Chat.aspx/Signout",
                data: '{"userid":"' + userid + '"}',

                contentType: "application/json; charset=utf-8",
                beforeSend: function () {
                    closechatscreen();
                    //updatetxtchatduration(document.getElementById('txtchatduration').innerHTML);
                    stopVideoChat();
                },
                success: function (val) {
                },
                complete: function () {
                    window.location = "Index.aspx";
                },
                error: function (err) {
                    //alert(err);
                }
            });
        }
    //}
};

function closechatscreen() {
    chattimeout = chattimeout + 1;
    if (chattimeout >= 300) {
        if (_isvideocall == false || _isvideocall == undefined) {
            $("#rightside").addClass("chatdisplay");
            $("#rightside").removeClass("col-sm-8 col-md-8 teststyle");

            $("#leftside").removeClass("col-sm-4 col-md-4");
            $("#leftside").addClass("list");

            $("#groupfavicon").removeClass("listFooter1");
            $("#groupfavicon").removeClass("col-sm-5 col-md-5");

            $("#divfooterright").hide();

            //window.clearInterval(chatscreentimer);
            clearInterval(chatscreentimer);
            chatscreentimer = null;

            //call the ajax method to update the total txt chat duration
            if (chatison == '1') {
                updatetxtchatduration(document.getElementById('txtchatduration').innerHTML);

                clearInterval(chatinterval);
                chatinterval = null;
                timerchat = 0;
            }
            else {
                clearInterval(chatinterval);
                chatinterval = null;
                document.getElementById('txtchatduration').innerHTML = '';
                timerchat = 0;
            }
        }
    }
};

var isChatCompeted = false;
var charStarted = new Date();
var currentChatPeerUserId = 0;
function isChatStopped(fromDate, toDate) {
    /*
     * DateFormat month/day/year hh:mm:ss
     * ex.
     * datediff('01/01/2011 12:00:00','01/01/2011 13:30:00','seconds');
     */
    var second = 1000;
    fromDate = new Date(fromDate);
    toDate = new Date(toDate);
    var timediff = toDate - fromDate;
    if (isNaN(timediff)) return NaN;
    return Math.floor(timediff / second);
}

function startchattimer() {
    chatinterval = window.setInterval(function () {
        document.getElementById('txtchatduration').innerHTML = tictacchat();
        if ($('#videoChatContainer').is(":visible")) {
            $('#txtchatduration').hide();

            console.log('asdsa =  ' + isChatStopped(charStarted, new Date()));
            if (isChatStopped(charStarted, new Date()) > 3000) {
                isChatCompeted = true;
            }
        }
        else {
            isChatCompeted = false;
            $('#txtchatduration').show();
            $('#videoduration').hide();
        }

    }, 1000);
}

function updatetxtchatduration(txtchat) {
    $.ajax({
        type: 'POST',
        url: "Chat.aspx/StoreTotalTxtChatDuration",
        data: '{"totaltxtchat":"' + totaltxtchatduration + '","txtchatduration":"' + txtchat + '","conversationId":"' + _conversationid + '"}',
        contentType: "application/json; charset=utf-8",
        success: function () {
            //alert("Success");
        },
        error: function (err) {
            //alert(err);
        },
        complete: function () {
            document.getElementById('txtchatduration').innerHTML = "";
            timerchat = 0;
            chatison = '';
        }
    });
}

function gettxtchatduration() {
    $.ajax({
        type: 'POST',
        url: "Chat.aspx/GetTotalTxtChatDuration",
        data: '{"conversationId":"' + _conversationid + '"}',
        contentType: "application/json; charset=utf-8",
        success: function () {
            //alert("Success");
        },
        error: function (err) {
            alert(err);
        }
    });
}

function tictacchat() {  
    timerchat++;
    var hours = parseInt(timerchat / 3600);
    var minutes = (parseInt(timerchat / 60)) % 60;
    var seconds = timerchat % 60;

    return ("0" + hours).slice(-2) + ":" + ("0" + minutes).slice(-2) + ":" + ("0" + seconds).slice(-2);    
}

function updateischaton() {
    $.ajax({
        type: 'POST',
        data: '{"conversationid":"' + _conversationid + '"}',
        url: 'Chat.aspx/UpdateIsChatOn',
        contentType: 'application/json; charset=utf-8',
        success: function (res) {
            if (res.d == "1") {
                chatison = '1';
            }
        },
        error: function (err) {
            alert(err);
        }
    });
}

function messageage(messagetime) {
    var hour = 0;
    var min = 0;

    var s1 = 0;
    var s2 = 0;

    var t = messagetime;
    var tary = new Array();
    tary = t.split(' ');
    var hm = tary[0].split(':');

    if (tary[1] == 'PM' && parseInt(hm[0]) < 12) {
        hour = parseInt(hm[0]) + 12;
    }
    else {
        hour = parseInt(hm[0]);
    }
    s1 = hour * 3600 + parseInt(hm[1]) * 60;

    var t1 = new Date();
    s2 = (t1.getHours() * 3600) + (t1.getMinutes() * 60);

    return s2 - s1;
}

$("#morecontacts").click(function () {
    pageindex = pageindex + 1;
    $.ajax({
        type: "POST",
        url: "HickChatEngine.svc/GetUsers",
        data: '{"currentuserid":' + userid + ',"favouriteUsers":"' + favouriteUsers + '","PageIndex":"' + pageindex + '","PageSize":"' + pagesize + '","usertype":"' + _userType + '"}',
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            $("#page_loader").show();
        },
        success: function (msg) {
            if (usergroups != 1) {
                sqldata = msg.d;
                var usercoll = msg.d.UsersColl;
                var groupcoll = msg.d.GroupsColl;
                // $('#table tbody').html("");
                usersTemplate = '';
                contactsCount = 0;
                for (var i = 0; i < usercoll.length; i++) {

                    if (usercoll[i].Status == 0) {
                        onlineuser = "Images/option_unselected.png";
                    }
                    else {
                        onlineuser = "Images/option_selected.png";
                    }
                    if (usercoll[i].Image == "") {
                        userphoto = "Images/default_user.png";
                    }
                    else {
                        userphoto = "Images/" + usercoll[i].Image;
                    }

                    /* setting the availability status for selected user  */
                    if (_peerid == usercoll[i].ID) {
                        $('#userstatus').attr("src", onlineuser);
                        if (usercoll[i].Status == 0) {
                            $('#statusdiv').text("Offline");

                        }
                        else {
                            $('#statusdiv').text("Online");
                        }
                    }

                    if (usercoll[i].Favorites == 1) {
                        _favoriteIcon = "Images/heart-red.png";
                    }
                    else {
                        _favoriteIcon = "Images/heart-greay.png";
                    }

                    if (usercoll[i].ID == userid) {
                        template = null;
                    }
                    else {
                        var grclas = "hide-element";
                        if (isgroupChat) {
                            grclas = "";
                        }

                        var un = usercoll[i].Firstname + " " + usercoll[i].Lastname;
                               var refid = usercoll[i].ReferenceID;
                        var cls = '';
                        if (_srchtxt.length > 0) {
                            if (un) {
                                if (un.toLowerCase().indexOf(_srchtxt.toLowerCase()) == -1) {
                                    cls = "hide-element";
                                }
                                else {
                                    contactsCount += 1;
                                }
                            }
                        }
                        else {
                            contactsCount += 1;
                        }
                        if (_peerid == usercoll[i].ID) {
                            template = "<tr id='tableRow' data-row-key='${User}' class='tableRow rowhighlite" + cls + "' style='cursor:pointer;'> <td id='userThumbnail'><img id='imgOption' src='" + userphoto + "'></img><img id='status' src='" + onlineuser + "'></img></td> <td>   <span class='userselct' id='userName'>" + usercoll[i].Firstname + "</span>&nbsp;<span class='userselctlastname'>" + usercoll[i].Lastname + "</span><div class='small1' id='statusmessage'>" + usercoll[i].StatusMessage + "</div>  <div class='small' id='session'>" + usercoll[i].ID + "</div><div class='small divusername'>" + usercoll[i].Username + "</div><span class='hide-element spnphnnumber' pnval=" + usercoll[i].PhoneNumber + "></span> <span class='hide-element spnrefId' refidval=" + usercoll[i].ReferenceID + "></span></td><td valign='top' class='action-icon-container'><img class='favorite-img action-icon heart-minheight' title='Favorite Icon' src='" + _favoriteIcon + "'/><img class='addtogroup action-icon " + grclas + "' title='Add To Group' src='Images/add-user.png' uid='" + usercoll[i].ID + "'/>";
                        }
                        else {
                            template = "<tr id='tableRow' data-row-key='${User}' class='tableRow " + cls + "' style='cursor:pointer;'> <td id='userThumbnail'><img id='imgOption' src='" + userphoto + "'></img><img id='status' src='" + onlineuser + "'></img></td> <td>   <span class='userselct' id='userName'>" + usercoll[i].Firstname + "</span>&nbsp;<span class='userselctlastname'>" + usercoll[i].Lastname + "</span><div class='small1' id='statusmessage'>" + usercoll[i].StatusMessage + "</div>  <div class='small' id='session'>" + usercoll[i].ID + "</div><div class='small divusername'>" + usercoll[i].Username + "</div><span class='hide-element spnphnnumber' pnval=" + usercoll[i].PhoneNumber + "></span><span class='hide-element spnrefId' refidval=" + usercoll[i].ReferenceID + "></span> </td><td valign='top' class='action-icon-container'><img class='favorite-img action-icon heart-minheight' title='Favorite Icon' src='" + _favoriteIcon + "'/><img class='addtogroup action-icon " + grclas + "' title='Add To Group' src='Images/add-user.png' uid='" + usercoll[i].ID + "'/>";
                        }

                        if (usercoll[i].UnReadMessages == true) {
                            template = template + "<img class='imgmsgnotification action-icon' title='New Message' src='Images/messages-icon.png'/>";
                        }
                        if (usercoll[i].IncomingCall == true && !$('#videoChatContainer').is(":visible")) {
                            template = template + " <img class='imgvideonotification action-icon' title='Incomming Video Call' src='Images/call-incoming-icon.png'/> ";
                        }

                        template = template + "<span class='action-icon'>" + usercoll[i].VideoCallDuration + "</span> </td><td> <div style='float:right;'>  </div> </td>  </tr>";

                    }
                    usersTemplate = usersTemplate + template;
                }
                bindGroups(groupcoll);
            }
            else {
                usersTemplate = '';
                bindGroups(msg.d.GroupsColl);
            }
        },
        complete: function () {
            $("#page_loader").hide();
        },
        error: function () {
        }
    });
});

//Hide the emoji icon in dail pad

$(".imgaudiocall").click(function () {
    $("#emoticonContainer").hide();
    $('#videoduration').css("display", "None");
    $('#txtchatduration').css("display", "None");
});

function Save_User() {
    var radioValue = $('#chatformdata input:radio[name=chatsession]:checked').val();
    var sessionNote = $('textarea#chattxtSessionNote').val();
    var InitiateMSGDate = $('#InitiateMSGDate').val();
    var EndDateTime = new Date().toString();
    EndDateTime = moment(EndDateTime).format('YYYY-MM-DD HH:mm:ss');
    var peerId = $('#msgactpeerId').val();
    var dataNotes = { "conversationid": _conversationid, "currentid": userid, "peerid": peerId, "groupid": _groupId, "session": radioValue, "sessionNote": sessionNote, "category": 1, "StartDateTime": InitiateMSGDate, "EndDateTime": EndDateTime };
    $.ajax({
        type: "POST",
        url: "HickChatEngine.svc/InsertSessionNote",
        //data: '{"conversationid":"' + _conversationid + '","currentid":"' + userid + '", "peerid":"' + _peerid + '", "groupid":"' + _groupId + '", "session":"' + radioValue + '", "sessionNote":"' + sessionNote + '", "category":"' + 1 + '"}',
        data: JSON.stringify(dataNotes),
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            $('textarea#chattxtSessionNote').val(" ");
            $('.new-pop').removeClass('man');
            $('#InitiateMSGDate').val("");
        }
    });
}

function audioCallSession() {
    var initiateMSGDateTime = new Date();
    initiateMSGDateTime = moment(initiateMSGDateTime).format('YYYY-MM-DD HH:mm:ss');
    if ($('#InitiateMSGDate').val() == "") {
        $('#InitiateMSGDate').val(initiateMSGDateTime);
    }
    $('textarea#txtAudioSessionNote').val(" ");
    $('.audiopop').removeClass('man');

    //$('.closes').click(function () {
    //    $('textarea#txtAudioSessionNote').val(" ");
    //    $('.audiopop').removeClass('man');
    //});

    //$('#cancel_audio_popup').click(function () {
    //    $('textarea#txtAudioSessionNote').val(" ");
    //    $('.audiopop').removeClass('man');
    //});

    var textAreas = "<div class='textChatMes' role='dialogue'><div class='popup_header pop'>End Session<a href='#' class='closes' onclick='audiopopupclose()'>X</a></div><div class='Inner-section'><form id='audioformdata' method='post'><div class='modal-body'><p>Your current audio session has ended. Please enter any session notes (optional).</p><p class='does-ses'><span style='color:#ff0000;'>*</span>Does this session qualify as an encounter?<span class='yse'><input type='radio' id='audio_radio_yes' name='audiosession' checked='checked' value='Yes' /> Yes </span><span class='yse'><input type='radio' name='audiosession' id='audio_radio_no' value='NO'/> No</span></p><textarea placeholder='Enter text' id='txtAudioSessionNote' style='width: 100%;height: 200px';></textarea> </div><div class='modal-footer'><div class='popup_conter' style='text-align:center;'><input type='button' value='Save' id='Audio_Save_User' class='btn_standard' onclick='SaveAudioSession()'><input type='button' id='cancel_audio_popup' class='btn_standard close' value='Cancel' onclick='audiopopupclose()'></div></div></form></div>";
    $('.audiopop').append(textAreas);
    $('.audiopop').addClass('man');
}
function audiopopupclose() {
    $('.audiopop').removeClass('man');
    $('.new-pop').removeClass('man');
    $('.videopop').removeClass('man');
    
    $('textarea#txtAudioSessionNote').val(" ");
}

function VideoCallSession() {
    var initiateMSGDateTime = new Date();
    initiateMSGDateTime = moment(initiateMSGDateTime).format('YYYY-MM-DD HH:mm:ss');
    if ($('#InitiateMSGDate').val() == "") {
        $('#InitiateMSGDate').val(initiateMSGDateTime);
    }
    $('textarea#txtSessionNote').val(" ");
    $('.videopop').removeClass('man');


    var textAreas = "<div class='textChatMes' role='dialogue'><div class='popup_header pop'>End Session<a href='#' class='closes' onclick='videopopupclose()'>X</a></div><div class='Inner-section'><form id='videoformdata' method='post'><div class='modal-body'><p>Your current video call session has ended. Please enter any session notes (optional).</p><p class='does-ses'><span style='color:#ff0000;'>*</span>Does this session qualify as an encounter?<span class='yse'><input type='radio' id='radio_yes' name='videosession' checked='checked' value='Yes' /> Yes </span><span class='yse'><input type='radio' name='videosession' id='radio_no' value='NO'/> No</span></p><textarea placeholder='Enter text' id='txtSessionNote' style='width: 100%;height: 200px';></textarea> </div><div class='modal-footer'><div class='popup_conter' style='text-align:center;'><input type='button' value='Save' id='Video_Save_User' class='btn_standard' onclick='SaveVideoSession()'><input type='button' id='cancel_popup' class='btn_standard close' value='Cancel' onclick='videopopupclose()'></div></div></form></div>";
    $('.videopop').append(textAreas);
    $('.videopop').addClass('man');
}
function videopopupclose() {
    $('textarea#txtSessionNote').val(" ");
    $('.videopop').removeClass('man');
}

function SaveAudioSession() {
    var radioValue = $('#audioformdata input:radio[name=audiosession]:checked').val();
    var sessionNote = $('textarea#txtAudioSessionNote').val();
    var InitiateMSGDate = $('#InitiateMSGDate').val();
    var EndDateTime = new Date();
    EndDateTime = moment(EndDateTime).format('YYYY-MM-DD HH:mm:ss');
    var peerId = $('#msgactpeerId').val();
    var dataNotes = { "conversationid": _conversationid, "currentid": userid, "peerid": _peerid, "groupid": 0, "session": radioValue, "sessionNote": sessionNote, "category": 2, "StartDateTime": InitiateMSGDate, "EndDateTime": EndDateTime };
    $.ajax({
        type: "POST",
        url: "HickChatEngine.svc/InsertSessionNote",
        //data: JSON.stringify('{"conversationid":"' + _conversationid + '","currentid":"' + userid + '", "peerid":"' + _peerid + '", "groupid":"' + 0 + '", "session":"' + radioValue + '", "sessionNote":"' + sessionNote + '", "category":"' + 2 + '"}'),
        data: JSON.stringify(dataNotes),
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            $('textarea#txtAudioSessionNote').val(" ");
            audiopopupclose();
            $('#InitiateMSGDate').val("");
        }
    });
}

function SaveVideoSession() {
    var radioValue = $('#videoformdata input:radio[name=videosession]:checked').val();
    var sessionNote = $('textarea#txtSessionNote').val();
    var InitiateMSGDate = $('#InitiateMSGDate').val();
    var EndDateTime = new Date();
    EndDateTime = moment(EndDateTime).format('YYYY-MM-DD HH:mm:ss');
    var peerId = $('#msgactpeerId').val();
    var dataNotes = { "conversationid": _conversationid, "currentid": userid, "peerid": _peerid, "groupid": _groupId, "session": radioValue, "sessionNote": sessionNote, "category": 3, "StartDateTime": InitiateMSGDate, "EndDateTime": EndDateTime };
    $.ajax({
        type: "POST",
        url: "HickChatEngine.svc/InsertSessionNote",
        //data: JSON.stringify('{"conversationid":"' + _conversationid + '","currentid":"' + userid + '", "peerid":"' + _peerid + '", "groupid":"' + _groupId + '", "session":"' + radioValue + '", "sessionNote":"' + sessionNote + '", "category":"' + 3 + '"}'),
        data: JSON.stringify(dataNotes),
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            $('textarea#txtAudioSessionNote').val(" ");
            $('.videopop').removeClass('man');
            $('#InitiateMSGDate').val("");
        }
    });
}