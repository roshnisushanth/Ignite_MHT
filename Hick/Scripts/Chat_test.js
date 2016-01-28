

var sendBtn = document.getElementById("send");
sendBtn.addEventListener('click', sendText);
var user = document.getElementById("user");

var sqldata;
var sdata;
var userpresent = false;
var date = new Date();
var hours = date.getHours();
var minutes = date.getMinutes();
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
var uniqueid;
var username = localStorage.getItem("name");
var time = hours + ":" + minutes + " " + ampm;
var isInitiator;
var room;
$("#loginname").text(localname);
/****************************************************************************
 * Signaling server 
 ****************************************************************************/

// Connect to the signaling server
var socket = io.connect();

socket.on('ipaddr', function (ipaddr) {
    console.log('Server IP address is: ' + ipaddr);
    updateRoomURL(ipaddr);
});

socket.on('created', function (room, clientId) {
    if (room == currentsessionvalue) {
        console.log('Created room', room, '- my client ID is', clientId);
        keyid = clientId;
        isInitiator = true;
    }
    else {
        //do nothing
    }
});

socket.on('joined', function (room, clientId) {
    console.log('This peer has joined room', room, 'with client ID', clientId);
    keyid = clientId;
    isInitiator = false;
});

socket.on('userspresent', function (result) {
    sqldata = JSON.stringify(result);
    sdata = result;
    $('#table tr').remove();
    userpresent = true;
    listusers(userpresent);
});

socket.on('sessionvalue1', function (value) {
    room = value[0].Session;
    currentsessionvalue = value[0].Session;
    socket.emit('create or join', room);
});

socket.on('full', function (room) {
    alert('rom is full');
});

socket.on('ready', function () {
    uniqueid = room;
    createPeerConnection(isInitiator, configuration, uniqueid);
});

socket.on('testready', function (room, clientId) {
    uniqueid = room;
    if (room == currentsessionvalue) {
        createPeerConnection(isInitiator, configuration, uniqueid);
    }
    else {
        //do nothing
    }
});

socket.on('log', function (array) {
    console.log.apply(console, array);
});

socket.on('message', function (message) {
    console.log('Client received message:', message);
    signalingMessageCallback(message);
});

socket.emit('ipaddr');
socket.emit('getusers', username);


/**
 * Send message to signaling server
 */
function sendMessage(message) {
    console.log('Client sending message: ', message);
    socket.emit('message', message);
}

/**
 * Updates URL on the page so that users can copy&paste it to their peers.
 */
function updateRoomURL(ipaddr) {
    var url;
    if (!ipaddr) {
        url = location.href
    } else {
        url = location.protocol + '//' + ipaddr + ':2013/#' + room
    }
}


/**************************************************************************** 
 * WebRTC peer connection and data channel
 ****************************************************************************/

var peerConn;
var dataChannel;

function signalingMessageCallback(message) {
    try {
        if (message.type === 'offer') {
            console.log('Got offer. Sending answer to peer.' + message.type);
            peerConn.setRemoteDescription(new RTCSessionDescription(message), function () { }, logError);
            peerConn.createAnswer(onLocalSessionCreated, logError);

        } else if (message.type === 'answer') {
            console.log('Got answer.' + message.type);
            peerConn.setRemoteDescription(new RTCSessionDescription(message), function () { }, logError);

        } else if (message.type === 'candidate') {
            console.log('Candidate:' + message.type);
            peerConn.addIceCandidate(new RTCIceCandidate({ candidate: message.candidate }));

        } else if (message === 'bye') {
            console.log('closing peer connection:');
            //peerConn.close();
            // TODO: cleanup RTC connection?

        }
    }
    catch (err) {
        console.log(err);
    }
}

function createPeerConnection(isInitiator, config, uniqueid) {
    console.log('Creating Peer connection as initiator?', isInitiator, 'config:', config);
    peerConn = new RTCPeerConnection(config);

    // send any ice candidates to the other peer
    peerConn.onicecandidate = function (event) {
        console.log('onIceCandidate event:', event);
        if (event.candidate) {
            sendMessage({
                type: 'candidate',
                label: event.candidate.sdpMLineIndex,
                id: event.candidate.sdpMid,
                candidate: event.candidate.candidate
            });
        } else {
            console.log('End of candidates.');
        }
    };


    try {
        if (isInitiator) {
            console.log('Creating Data Channel');
            dataChannel = peerConn.createDataChannel(uniqueid);
            onDataChannelCreated(dataChannel);

            console.log('Creating an offer');
            peerConn.createOffer(onLocalSessionCreated, logError);
        }
        else {
            peerConn.ondatachannel = function (event) {
                console.log('ondatachannel:', event.channel);
                dataChannel = event.channel;
                onDataChannelCreated(dataChannel);
            };
        }
    }
    catch (err) {
        console.log(err);
    }
}

function onLocalSessionCreated(desc) {
    console.log('local session created:', desc);
    peerConn.setLocalDescription(desc, function () {
        console.log('sending local desc:', peerConn.localDescription);
        sendMessage(peerConn.localDescription);
    }, logError);
}


function onDataChannelCreated(channel) {
    console.log('onDataChannelCreated:', channel);

    channel.onopen = function () {
        console.log('CHANNEL opened!!!');
        document.getElementById("send").disabled = false;
    };
    //this event is triggered when a msg is sent through the channel
    channel.onmessage = (webrtcDetectedBrowser == 'firefox') ?
        receiveDataFirefoxFactory() :
        receiveDataChromeFactory();
}

function receiveDataChromeFactory() {
    var buf, count;
    return function onmessage(event) {
        var data = event.data;
        rendertext(data);
    }
}

function receiveDataFirefoxFactory() {
    var count, total, parts;
    return function onmessage(event) {
        var data = event.data;
        rendertext(data);
    }
}




//function to send text to the other peer
function sendText() {
    var definition = { "smile": { "title": "Smile", "codes": [":)", ":=)", ":-)"] }, "sad-smile": { "title": "Sad Smile", "codes": [":(", ":=(", ":-("] }, "big-smile": { "title": "Big Smile", "codes": [":D", ":=D", ":-D", ":d", ":=d", ":-d"] } };
    $.emoticons.define(definition);
    var msgField = $('#InputMessage');
    var textWithEmoticons = $.emoticons.replace(msgField.val());
    var element = '<li><span class="pull-left spanlabel ">' + username + ':' + '</span> <span class="pull-right" style="color: #B3B3B3;">' + time + '</span> <div class="clearfix" style="margin-left: 50px; margin-right: 70px;">' + textWithEmoticons + '</div></li>';
    var completetext = username + "=" + msgField.val();
    $('.chat').append(element);
    $('.chat').animate({
        scrollTop: $('.chat li:last-child').offset().top + 'px'
    }, 1000);
    try {
        dataChannel.send(String(completetext));
    }
    catch (err) {
        alert(err);
    }
    msgField.val('');
    ChatArray.push(element);
    localStorage.setItem(keyid, ChatArray);
}



//on send button press call sendtext() method
$("#InputMessage").keypress(function (event) {
    var keycode = (event.keyCode ? event.keyCode : event.which);
    if (keycode == '13') {
        sendText();
    }
});

//function to display the text recieved from the other peer
function rendertext(data) {
    var sendername;
    var message;
    var res = data.split("=", 2);
    sendername = res[0];
    message = res[1];
    var definition = { "smile": { "title": "Smile", "codes": [":)", ":=)", ":-)"] }, "sad-smile": { "title": "Sad Smile", "codes": [":(", ":=(", ":-("] }, "big-smile": { "title": "Big Smile", "codes": [":D", ":=D", ":-D", ":d", ":=d", ":-d"] } };
    $.emoticons.define(definition);
    var textWithEmoticons = $.emoticons.replace(message);
    $('#username').text(sendername);
    $("#leftside").addClass("col-sm-4");
    $("#leftside").removeClass("list");
    $("#divcontactmenu").addClass("dropdownsearch");
    $("#divcontactmenu").removeClass("chatdisplay");
    $("#userlist").addClass("hidecontactlist");
    $("#searchtag").addClass("hidecontactlist");
    $("#rightside").removeClass("chatdisplay");
    $("#rightside").addClass("col-sm-8");
    var textArea = "<div class='textChatMessages'> <div class='textdivtag'><ul class='chat'></ul></div></div>";
    $('#textContainer').append(textArea);
    chatopen = true;
    var element = ' <li><span class="pull-left spanlabel">' + sendername + ':' + '</span> <span class="pull-right" style="color: #B3B3B3;">' + time + '</span> <div class="clearfix" style="margin-left: 50px; margin-right: 70px;">' + textWithEmoticons + '</div></li>';
    $('.chat').append(element);
    $('.chat').animate({
        scrollTop: $('.chat li:last-child').offset().top + 'px'
    }, 1000);
    ChatArray.push(element);
    localStorage.setItem(keyid, ChatArray);
}



function logError(err) {
    console.log(err.toString(), err);
}


/*....users from database..*/

function listusers(userpresent) {

    var renderTable = function (success, error) {
        var onlineuser;
        var template;
        var userphoto;
        for (var i = 0; i < sdata.length; i++) {

            if (sdata[i].Status.trim() == "1") {
                onlineuser = "images/option_selected.png";
                userphoto = "images/" + sdata[i].Image + ".png";
                if (sdata[i].Username.toLowerCase() == username) {
                    template = null;
                }
                else {
                    template = "<tr id='tableRow' data-row-key='${User}' class='tableRow'> <td id='userThumbnail'><img id='imgOption' src='" + userphoto + "'></img><img id='status' src='" + onlineuser + "'></img></td> <td>   <div class='userselct' id='userName'>" + sdata[i].Username + "</div><div class='small1' id='statusmessage'>" + sdata[i].StatusMessage + "</div>  <div class='small' id='session'>" + sdata[i].Session + "</div> </td>  </tr>";
                }
            }
            else {
                onlineuser = "images/option_unselected.png";
                userphoto = "images/" + sdata[i].Image + ".png";
                if (sdata[i].Username.toLowerCase() == username) {
                    template = null;
                }
                else {
                    template = "<tr id='tableRow' data-row-key='${User}' class='tableRow'><td id='userThumbnail'><img id='imgOption' src='" + userphoto + "'></img><img id='status' src='" + onlineuser + "'></img></td> <td>   <div class='userselct' id='userName'>" + sdata[i].Username + "</div> <div class='small1' id='statusmessage'>" + sdata[i].StatusMessage + "</div> <div class='small' id='session'>" + sdata[i].Session + "</div> </td> </tr>";
                }
            }

            $('#table tbody').append(template);
        }
    }

    var dNow = new Date();
    var localdate = dNow.getHours() + ':' + dNow.getMinutes() + 'hrs';
    $('#datetimepicker-input').text(localdate)

    var ds = new $.ig.DataSource({
        type: "json",
        dataSource: sqldata,
        callback: renderTable
    });

    ds.dataBind();


    var desiredHeight = $('.textChatMessages').height();
    $('.chat').css('max-height', desiredHeight);

    var h = $('#fluid').height() - $('#header').height()
    // h = h > minHeight ? h : minHeight;
    $('#userlist').css('max-height', h);

    $('#divcontacttable').css('max-height', h);

    $(window).resize(function () {
        var desiredHeight = $('.textChatMessages').height();
        $('.chat').css('max-height', desiredHeight);

        var h = $('#fluid').height() - $('#header').height()
        // h = h > minHeight ? h : minHeight;
        $('#userlist').css('max-height', h);
        $('#divcontacttable').css('max-height', h);
    });





    $(document).ready(function () {

        $("tr.tableRow").click('click', function (e, value, row, index) {

            $('.textChatMessages').hide();
            var imgurl = $(this).closest("tr")   // Finds the closest row <tr> 
                               .find("#imgOption").attr("src");     // Gets a descendent with class="nr"

            var Name = $(this).closest("tr")   // Finds the closest row <tr> 
                               .find(".userselct")     // Gets a descendent with class="nr"
                               .text();         // Retrieves the text within <td>


            var previousroomvalue = room;

            sessionvalue = $(this).closest("tr").find("#session").text().toString();

            room = sessionvalue;
            concatsession = currentsessionvalue + '||' + room;
            keyid = concatsession;
            //socket.emit('testleave',previousroomvalue);


            ChatArray = [];

            var staturl = $(this).closest("tr")   // Finds the closest row <tr> 
            .find("#status") // Gets a descendent with class="nr"
            .attr("src");  // Retrieves the text within <td>
            $("#leftside").addClass("col-sm-4");
            $("#leftside").removeClass("list");
            $("#divcontactmenu").addClass("dropdownsearch");
            $("#divcontactmenu").removeClass("chatdisplay");
            $("#userlist").addClass("hidecontactlist");
            $("#searchtag").addClass("hidecontactlist");
            $("#rightside").removeClass("chatdisplay");
            $("#rightside").addClass("col-sm-8");
            var textArea = "<div class='textChatMessages' id='mytext'><div class='textdivtag' > <ul class='chat'></ul></div></div>";
            textArea.id = Name;
            $('#textContainer').append(textArea);
            $('#usericon').attr("src", imgurl);
            chatopen = true;
            $('#username').text(Name);
            $('#userstatus').attr("src", staturl);
            if (staturl == "images/option_selected.png") {
                $("#statusdiv").text('online');
            }
            else if (staturl == "images/option_unselected.png") {
                $("#statusdiv").text('offline');
            }

            $('.chat').val('');
            socket.emit('sessionupdate', { name: concatsession, message: sessionvalue });
            if (localStorage.getItem(keyid) == null) {

            }
            else {
                var getitem = localStorage.getItem(keyid);
                var res = getitem.split(",");
                if (res.length > 1) {
                    for (var i = 0; i < res.length; i++) {
                        $('.chat').append(res[i] + '<br/>');

                    }
                }
            }
            document.getElementById("send").disabled = true;
        });
    });

}

$(function () {
    $("#signout").click('click', function () {
        socket.emit('signout', username);
        window.location = "index.html";
    });
});


$(function () {
    $("#signout1").click('click', function () {
        socket.emit('signout', username);
        window.location = "index.html";
    });
});

$(function () {
    $("#vcall").click('click', function () {

        window.location = "Video.html";
    });
});
$(function () {
    $("#vcall1").click('click', function () {

        window.location = "Video.html";
    });
});
