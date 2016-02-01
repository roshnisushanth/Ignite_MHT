

var definition = { "smile": { "title": "Smile", "codes": [":)", ":=)", ":-)"] }, "sad-smile": { "title": "Sad Smile", "codes": [":(", ":=(", ":-("] }, "big-smile": { "title": "Big Smile", "codes": [":D", ":=D", ":-D", ":d", ":=d", ":-d"] }, "cool": { "title": "Cool", "codes": ["8|", "8-|", "8=|"] }, "surprised": { "title": "Surprised", "codes": [":O", ":-O", ":=O"] }, "wink": { "title": "Wink", "codes": [";)", ";-)", ";=)"] }, "crying": { "title": "Crying", "codes": [":'(", ":'-(", ":'=("] }, "sweating": { "title": "Sweating", "codes": ["(sweat)"] }, "speechless": { "title": "Speechless", "codes": [":-|", ":|", ":=|"] }, "thinking": { "title": "Thinking", "codes": [":s", ":-s", ":=s"] }, "devil": { "title": "Devil", "codes": ["3:)", "3:-)", "3:=)"] }, "heart": { "title": "Heart", "codes": ["<3"] }, "in-love": { "title": "in-love", "codes": ["(in-love)"] }, "evil-grin": { "title": "evil-grin", "codes": ["(evilgrin)"] }, "yes": { "title": "Yes", "codes": ["(Y)"] }, "no": { "title": "No", "codes": ["(N)"] }, "yawn": { "title": "Yawn", "codes": ["(yawn)"] }, "hi": { "title": "Hi", "codes": ["(hi)"] }, "clap": { "title": "Clap", "codes": ["(clap)"] }, "angry": { "title": "Angry", "codes": [":@"] }, "giggle": { "title": "Chuckle", "codes": ["(chuckle)"] }, "bug": { "title": "Bug", "codes": ["(bug)"] }, "cake": { "title": "Cake", "codes": ["(^)"] } };

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}
var eventsTimer;
var _srchtxt = '';
var contactsCount = 0;

var pageindex = 0;
var pagesize = 50;
var idlecount = 0;
var _isvideocall = false;
//var timeout = 600000;

//var username = getParameterByName('name');
var username = $("#loginname").html();
var userid = 0;
userid = $("#Userid").html();
var _userType = $("#hdnusertype").html();
var _conversationid;
var _peerid = 0;
var istexthistoryavailable = false;
var isvideologhistoryavailable = false;
var g_objChatColl = [];
var _timezone;
var exportchatobject = function () {
    this.Name,
    this.Conversation,
    this.Time
};
var _logdate = '';
/* Group Chat */
var isgroupChat = false;
var _groupId = 0;
var usersTemplate = '';
/* Group Chat */
$(function () {
    $("#home").click('click', function () {
        var hashes = '';
        if (window.location.href.indexOf('?') != -1) {
            hashes = window.location.href.slice(window.location.href.indexOf('?'));
        }
        var url = "Chat.aspx";
        window.location = url + hashes;
    });
});


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

getTimeZone();
trackUsers();
$(document).ready(function () {

    //var idleInterval = setInterval(function () { logoff(); }, 1000); //should be 600000 for 10 min

    $("#divfooterright").hide();
    $("#btnexportchat").attr("disabled", "disabled");
    $("#btnexportchat").addClass("btndisabled");
    $("#btnexportvideochat").attr("disabled", "disabled");
    $("#btnexportvideochat").addClass("btndisabled");

    $(function () {
        renderTable();
        //        startEventsTimer();
    });

    $(this).mousemove(function (e) {
        //timecount=0;
       // window.clearInterval(idleInterval);
        idlecount = 0;
        //idleInterval = setInterval(function () { logoff(); }, 1000); //should be 600000 for 10 min
    });

    $(this).keypress(function (e) {
        //timecount=0;
       // window.clearInterval(idleInterval);
        idlecount = 0;
        //idleInterval = setInterval(function () { logoff(); }, 1000); //should be 600000 for 10 min
    });

    document.onkeypress = function (e) {
        //window.clearInterval(idleInterval);
        idlecount = 0;
        //idleInterval = setInterval(function () { logoff(); }, 1000); //should be 600000 for 10 min
    };

    $(document).click(function () {
        //timecount=0;   
        //window.clearInterval(idleInterval);
        idlecount = 0;
        //idleInterval = setInterval(function () { logoff(); }, 1000); //should be 600000 for 10 min        
    });

    $(".txtsearchbox").on("keyup", function () {
        _srchtxt = $(this).val();
        resetContacts();
    });

    $("#btnexportchat").click(function () {
        $.ajax({
            type: "POST",
            url: "HickChatEngine.svc/GetChatLogForExport",
            data: '{"currentuserid":"' + userid + '", "conversationid":"' + _conversationid + '", "flag":"no", "timezone":"' + _timezone + '", "groupid":"' + _groupId + '", "logdate":"' + _logdate + '"}',
            contentType: "application/json; charset=utf-8",
            success: function (msg) {
                if (msg.d.length > 0) {

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
                        title: "Export Chat",
                        create: function () {
                            //$("#tblexport").attr("cellpadding", "10");
                            $("#divcontent").css("height", phight - 100);
                            $(".ui-dialog-titlebar-close").css("display", "none");
                        }
                    });
                }
            },
            error: function (err) {
                //alert(err);
            },
            complete: function () {

            }
        });

        //exportChat('1');
    });
    $("#btnexportvideochat").click(function () {

        exportChat('2');
    });
    $("#divtabs").tabs({
        create: function () {
            $(".ui-widget-header").css("background", "none");
            $(".ui-widget-header").css("border", "none");
            $(".ui-widget-header").css("border-bottom", "1px solid #aaaaaa");
            $(".ui-widget-header").css("font-size", "12px");
        }

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
            url: "Chat_log.aspx/ModifyChatForExport",
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


    });

    //$(window).on("blur focus", function (e) {
    //    var prevType = $(this).data("prevType");

    //    if (prevType != e.type) {   //  reduce double fire issues
    //        switch (e.type) {
    //            case "blur":
    //                //$("#testdata").innerHTML="Blured";                    
    //                idleInterval = setInterval(function () { logoff(); }, 1000); //should be 600000 for 10 min
    //                break;
    //            case "focus":
    //                window.clearInterval(idleInterval);
    //                idlecount = 0;
    //                idleInterval = setInterval(function () { logoff(); }, 1000); //should be 600000 for 10 min
    //                break;
    //        }
    //    }
    //    $(this).data("prevType", e.type);
    //});

    $("#btncancel").click(function () {
        $("#divexportpopup").dialog('close');
    });

});

var renderTable = function (success, error) {
    if (pageindex == 0) {
        pageindex = 1;
    }

    $.ajax({
        type: "POST",
        url: "HickChatEngine.svc/GetUsers_log",
        data: '{"currentuserid":"' + userid + '","usertype":"' + _userType + '","PageIndex":"' + pageindex + '","PageSize":"' + pagesize + '"}',
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            sqldata = msg.d;
            //            $('#table tbody').html("");

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
                if (usercoll[i].Status == 0) {
                    $('#statusdiv').text("Offline");

                }
                else {
                    $('#statusdiv').text("Online");
                }

                var un = usercoll[i].Firstname + " " + usercoll[i].Lastname;
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

                if (usercoll[i].Username == username) {
                    template = null;
                }
                else {
                    template = "<tr id='tableRow' data-row-key='${User}' class='tableRow" + cls + "' > <td id='userThumbnail'><img id='imgOption' src='" + userphoto + "'></img><img id='status' src='" + onlineuser + "'></img></td> <td>   <span class='userselct' id='userName'>" + usercoll[i].Firstname + "</span>&nbsp;<span class='userselctlastname'>" + usercoll[i].Lastname + "</span><div class='small1' id='statusmessage'>" + usercoll[i].StatusMessage + "</div>  <div class='small' id='session'>" + usercoll[i].ID + "</div> </td>  </tr>";
                }

                //                $('#table tbody').append(template);
                usersTemplate = usersTemplate + template;

            }

            bindGroups(groupcoll);
        },
        error: function (err) {
            //alert(err);
        },
        complete: function () {

            $("#page_loader").hide();
            $("#divcontactsloader").hide();
            $("#imgmessageloader").hide();
            // getGroups();
            startEventsTimer();
        }
    });
}


$("#table tbody").on('click', 'tr.tableRow', function () {
    $(this).addClass('bg').siblings().removeClass("bg");
    $("#divfooterright").show();
    $("#btnexportchat").attr("disabled", "disabled");
    $("#btnexportchat").addClass("btndisabled");
    $("#btnexportvideochat").attr("disabled", "disabled");
    $("#btnexportvideochat").addClass("btndisabled");

    $('.textChatMessages').hide();
    $("#datepicker").datepicker("setDate", new Date());

    var imgurl = $(this).closest("tr")   // Finds the closest row <tr> 
                       .find("#imgOption").attr("src");     // Gets a descendent with class="nr"

    var Name = $(this).closest("tr")   // Finds the closest row <tr> 
                       .find(".userselct")     // Gets a descendent with class="nr"
                       .text();         // Retrieves the text within <td>

    var lastName = $(this).closest("tr").find(".userselctlastname").html();

    var imgstatus = $(this).closest("tr")   // Finds the closest row <tr> 
                           .find("#status").attr("src");

    var id = $(this).closest("tr")
                  .find("#session")
                  .text();
    if (id) {
        _peerid = id;
    }
    else {
        _peerid = 0;
    }

    if ($(this).attr("groupid")) {
        _groupId = $(this).attr("groupid");
    }
    else {
        _groupId = 0;
    }

    $("#leftside").addClass("col-sm-4 col-md-4");
    $("#leftside").removeClass("list");
    $("#divcontactmenu").addClass("dropdownsearch");
    $("#divcontactmenu").removeClass("chatdisplay");
    $("#divcontacttable").addClass("hidecontactlist");
    $("#searchtag").addClass("hidecontactlist");
    $("#rightside").removeClass("chatdisplay");
    $("#rightside").addClass("col-sm-8 col-md-8  teststyle");
    var textArea = "<div class='textChatMessageslog' id='divtxtlog' data-row-key='${name}'><ul class='chat'></ul></div>";
    textArea.id = Name;
    var txtvideo = "<div class='textChatMessageslog' id='divvideolog' data-row-key='${name}'><ul class='videochat'></ul></div>";

    $('#divtextchat').html(textArea);
    $("#divvideochat").html(txtvideo);

    $("#groupfavicon").addClass("col-sm-4 col-md-4 listFooter1");
    $("#groupfavicon").removeClass("listFooter");
    $('#usericon').attr("src", imgurl);
    $('#userstatus').attr("src", imgstatus);
    if (imgstatus == "Images/option_selected.png") {
        $('#statusdiv').text("Online");
    }
    else {
        $('#statusdiv').text("Offline");
    }

    chatopen = true;
    $('#username').text(Name);
    $('.chat').html("");
    $('.videochat').html("");

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

});

$("#datepicker").datepicker({
    maxDate: '0',
    changeYear: true,
    changeMonth: true,
    dateFormat: 'mm/dd/yy'
});

var date = $("#datepicker")[0].value;
$(function () {

    $("#Viewchatlog").click('click', function () {
        _logdate = $("#datepicker")[0].value + " " + getLocalTime(new Date(), false);
        var jsondata = { currentid: userid, peerid: _peerid, logdate: _logdate, timezone: _timezone, groupid: _groupId };
        $.ajax({
            type: "POST",
            url: "HickChatEngine.svc/GetChatLog",
            data: JSON.stringify(jsondata),
            contentType: "application/json; charset=utf-8",
            success: function (val) {
                $('.chat').html("");
                if (val.d.length == 0) {
                    istexthistoryavailable = false;
                    var display = "No text found for the given date";
                    var ele = '<li> <div class="clearfix" style="margin-left: 50px;">' + display + '</div></li>';
                    $('.chat').append(ele);
                    $("#btnexportchat").attr("disabled", "disabled");
                    $("#btnexportchat").addClass("btndisabled");
                }
                else {
                    $("#btnexportchat").attr("disabled", false);
                    $("#btnexportchat").removeClass("btndisabled");
                    istexthistoryavailable = true;
                    for (var i = 0; i < val.d.length; i++) {
                        var element;
                        _conversationid = val.d[i].ConversationId;

                        //var convrstn = formatSymbolsToText(val.d[i].Conversation);
                        $.emoticons.define(definition);
                        var msgField = formatSymbolsToText(val.d[i].Conversation);
                        var textWithEmoticons = $.emoticons.replace(msgField);
                        var convrstn = textWithEmoticons;

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

                        if (val.d[i].PeerID == userid) {
                            element = '<li class="chatcolorgrey"><span class=" spanlabel ">' + val.d[i].PeerName + ':' + '</span><br/><div class="" style="margin-left: 50px; margin-right: 30px;">' + convrstn + '</div><span class="pull-right" style="color: #777;padding-right:8px;">' + val.d[i].Time + '</span><br/></li>';
                        }
                        else {
                            element = '<li class=" chatcolorgreen"><span class="spanlabel ">' + val.d[i].PeerName + ':' + '</span><br/>   <div class="clearfix divconvrsation" style="margin-left: 50px; margin-right: 30px;">' + convrstn + '</div><span class="pull-right" style="color:#777;padding-right:8px;">' + val.d[i].Time + '</span><br/></li>';
                        }

                        //var element = '<li><span class="pull-left spanlabel ">' + val.d[i].PeerName + ':' + '</span>  <span class="pull-right" style="color: #B3B3B3;"></span> <div class="clearfix" style="margin-left: 50px; margin-right: 70px;">' + val.d[i].Conversation + '</div></li>';



                        $('.chat').append(element);
                    }
                }

            },
            error: function (err) {

                //alert(conversationlogid);
                // alert(err.responseText);
            },
            complete: function () {
                var objDiv = document.getElementById("divtxtlog");
                objDiv.scrollTop = objDiv.scrollHeight;

                $.ajax({
                    type: "POST",
                    url: "HickChatEngine.svc/GetVideoChatLog",
                    data: '{"currentid":"' + userid + '","peerid":"' + _peerid + '","logdate":"' + _logdate + '","timezone":"' + _timezone + '","groupid":"' + _groupId + '"}',

                    contentType: "application/json; charset=utf-8",
                    success: function (val) {
                        $('.videochat').html("");
                        if (val.d.length == 0) {
                            isvideologhistoryavailable = false;
                            var display = "No video chat history found for the given date.";
                            var ele = '<li> <div class="clearfix" style="margin-left: 50px; margin-right: 70px;">' + display + '</div></li>';
                            $('.videochat').append(ele);
                            $("#btnexportvideochat").attr("disabled", "disabled");
                            $("#btnexportvideochat").addClass("btndisabled");
                        }
                        else {
                            $("#btnexportvideochat").attr("disabled", false);
                            $("#btnexportvideochat").removeClass("btndisabled");
                            isvideologhistoryavailable = true;
                            for (var i = 0; i < val.d.length; i++) {
                                var element;
                                _conversationid = val.d[i].ConversationId;
                                var convrstn = "Duration(hh:mm:ss): " + val.d[i].Duration;

                                if (val.d[i].PeerID == userid) {
                                    element = '<li class="chatcolorgrey"><span class=" spanlabel ">' + val.d[i].PeerName + ':' + '</span><br/><div class="" style="margin-left: 50px;">' + convrstn + '</div><span class="pull-right" style="color: #777;padding-right:8px;">' + val.d[i].Time + '</span><br/></li>';
                                }
                                else {
                                    element = '<li class=" chatcolorgreen"><span class="spanlabel ">' + val.d[i].PeerName + ':' + '</span><br/>   <div class="clearfix divconvrsation" style="margin-left: 50px; margin-right: 70px;">' + convrstn + '</div><span class="pull-right" style="color:#777;padding-right:8px;">' + val.d[i].Time + '</span><br/></li>';
                                }

                                $('.videochat').append(element);
                            }
                        }

                    },
                    error: function (err) {

                        //alert(conversationlogid);
                        //alert(err);
                    },
                    complete: function () {

                        var objDiv = document.getElementById("divvideolog");
                        objDiv.scrollTop = objDiv.scrollHeight;
                    }
                });
            }
        });


    });
});


$(function () {
    $("#signout").click('click', function () {
        $.ajax({
            type: "POST",
            url: "Chat.aspx/Signout",
            data: '{"userid":"' + userid + '"}',

            contentType: "application/json; charset=utf-8",
            success: function (val) {

                window.location = "Index.aspx";

            },
            error: function (err) {
                alert(err);
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
            success: function (val) {

                window.location = "Index.aspx";

            },
            error: function (err) {

            }
        });
    });
});

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
function exportChat(mesagetype) {

    var sendurl = "ExportChat.ashx?conversationid=" + _conversationid + "&currentuserid=" + userid + "&ischatwindow=no&messagetype=" + mesagetype + "";

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
function scrolldown(id) {

    if (id == "text") {
        var objDiv = document.getElementById('divtxtlog');
        objDiv.scrollTop = objDiv.scrollHeight;
        $("#btnexportvideochat").hide();
        $("#btnexportchat").show();
        if (istexthistoryavailable) {
            $("#btnexportchat").attr("disabled", false);
            $("#btnexportchat").removeClass("btndisabled");
        }
        else {
            $("#btnexportchat").attr("disabled", "disabled");
            $("#btnexportchat").addClass("btndisabled");
        }
    }
    else if (id == "video") {
        var objDiv = document.getElementById('divvideolog');
        objDiv.scrollTop = objDiv.scrollHeight;
        $("#btnexportvideochat").show();
        $("#btnexportchat").hide();
        if (isvideologhistoryavailable) {
            $("#btnexportvideochat").attr("disabled", false);
            $("#btnexportvideochat").removeClass("btndisabled");
        }
        else {
            $("#btnexportvideochat").attr("disabled", "disabled");
            $("#btnexportvideochat").addClass("btndisabled");
        }
    }

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
        url: "Chat_log.aspx/StoreTimeZoneToSession",
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
    var _tim = _hours + ":" + _minutes + " " + ampm;
    return _tim;
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
                /*code for displaying only 15 characters of the group name*/
               
                
                // template = "<tr id='tableRow' data-row-key='${User}' class='tableRow' style='cursor:pointer;'> <td id='userThumbnail'><img id='imgOption' src='" + userphoto + "'></img><img id='status' src='" + onlineuser + "'></img></td> <td>   <span class='userselct' id='userName'>" + msg.d[i].Firstname + "</span>&nbsp;<span class='userselctlastname'>" + msg.d[i].Lastname + "</span><div class='small1' id='statusmessage'>" + msg.d[i].StatusMessage + "</div>  <div class='small' id='session'>" + msg.d[i].ID + "</div><div class='small divusername'>" + msg.d[i].Username + "</div> </td><td><img class='favorite-img action-icon' title='Favorite Icon' src='" + _favoriteIcon + "'/><img class='addtogroup action-icon "+grclas+"' title='Add To Group' src='Images/add-user.png' uid='"+msg.d[i].ID+"'/> </td><td> <div style='float:right;'>  </div> </td>  </tr>";
                template = "<tr id='tableRow' data-row-key='${User}' class='tableRow " + clsgrp + "' style='cursor:pointer;' groupid=" + groupcoll[i].Id + "> <td id='userThumbnail'><img id='imgOption' src='Images/group_icon.png' style='width:50px; height:50px;'></img><img id='status' src='Images/option_unselected.png'></img></td> <td valign='top'><span id='" + groupcoll[i].Id + "' class='userselct grpusers' >" + userlist + "</span><div class='small1' id='statusmessage'></div></td> <td></td> <td></td></tr>";
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
    $("#page_loader").hide();
    //$('#table tr:first-child').addClass('bg')
    $("#table").find("tr[class~=tableRow.bg]").each(function () {

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
//                                else {
//                                    contactsCount += 1;
//                                }
//                            }
//                        }
//                        else {
//                            contactsCount += 1;
//                        }

//                        // template = "<tr id='tableRow' data-row-key='${User}' class='tableRow' style='cursor:pointer;'> <td id='userThumbnail'><img id='imgOption' src='" + userphoto + "'></img><img id='status' src='" + onlineuser + "'></img></td> <td>   <span class='userselct' id='userName'>" + msg.d[i].Firstname + "</span>&nbsp;<span class='userselctlastname'>" + msg.d[i].Lastname + "</span><div class='small1' id='statusmessage'>" + msg.d[i].StatusMessage + "</div>  <div class='small' id='session'>" + msg.d[i].ID + "</div><div class='small divusername'>" + msg.d[i].Username + "</div> </td><td><img class='favorite-img action-icon' title='Favorite Icon' src='" + _favoriteIcon + "'/><img class='addtogroup action-icon "+grclas+"' title='Add To Group' src='Images/add-user.png' uid='"+msg.d[i].ID+"'/> </td><td> <div style='float:right;'>  </div> </td>  </tr>";
//                        template = "<tr id='tableRow' data-row-key='${User}' class='tableRow " + clsgrp + "' style='cursor:pointer;' groupid=" + msg.d[i].Id + "> <td id='userThumbnail'><img id='imgOption' src='Images/group_icon.png' style='width:50px; height:50px;'></img><img id='status' src='Images/option_unselected.png'></img></td> <td valign='top'><span id='" + msg.d[i].Id + "' class='userselct grpusers' >" + userlist + "</span><div class='small1' id='statusmessage'></div></td> <td></td> <td></td></tr>";
//                        usersTemplate = usersTemplate + template;

//                    }

//                }
//            }


//            $('#table tbody').html("");
//            if (contactsCount > 0) {
//                $('#table tbody').append(usersTemplate);
//            }
//            else {
//                $('#table tbody').append("<tr><td>No contacts found</td></tr>");
//            }

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
function startEventsTimer() {
    //    eventsTimer = setInterval("renderTable()", 5000);
    eventsTimer = setTimeout("renderTable()", 5000);

}
function stopEventsTimer() {
    clearInterval(eventsTimer);
}
function resetContacts() {
    stopEventsTimer();
    renderTable();
    //    startEventsTimer();
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
        $.ajax({
            type: "POST",
            url: "Chat.aspx/Signout",
            data: '{"userid":"' + userid + '"}',

            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
            //stopVideoChat();
            },
            success: function (val) {
            },
            complete: function () {
            window.location = "Index.aspx";
            },
            error: function (err) {
            alert(err);
            }
        });
    //}
};

$("#morecontacts").click(function () {
    pageindex = pageindex + 1;

    $.ajax({
        type: "POST",
        url: "HickChatEngine.svc/GetUsers_log",
        data: '{"currentuserid":"' + userid + '","usertype":"' + _userType + '","PageIndex":"' + pageindex + '","PageSize":"' + pagesize + '"}',
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            sqldata = msg.d;
            //            $('#table tbody').html("");

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
                if (usercoll[i].Status == 0) {
                    $('#statusdiv').text("Offline");

                }
                else {
                    $('#statusdiv').text("Online");
                }

                var un = usercoll[i].Firstname + " " + usercoll[i].Lastname;
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

                if (usercoll[i].Username == username) {
                    template = null;
                }
                else {
                    template = "<tr id='tableRow' data-row-key='${User}' class='tableRow " + cls + "' > <td id='userThumbnail'><img id='imgOption' src='" + userphoto + "'></img><img id='status' src='" + onlineuser + "'></img></td> <td>   <span class='userselct' id='userName'>" + usercoll[i].Firstname + "</span>&nbsp;<span class='userselctlastname'>" + usercoll[i].Lastname + "</span><div class='small1' id='statusmessage'>" + usercoll[i].StatusMessage + "</div>  <div class='small' id='session'>" + usercoll[i].ID + "</div> </td>  </tr>";
                }

                //                $('#table tbody').append(template);
                usersTemplate = usersTemplate + template;

            }

            bindGroups(groupcoll);
        },
        error: function (err) {
            //alert(err);
        },
        complete: function () {

            $("#page_loader").hide();
            $("#divcontactsloader").hide();
            $("#imgmessageloader").hide();
            // getGroups();
            startEventsTimer();
        }
    });
});