<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VideoChat.aspx.cs" Inherits="Hick.VideoChat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <span id="templates" data-url="templates.html" style="display: none"></span>
    <form id="form1" runat="server">
        <div>
            <div id="divLoggedinuser" class="video_first" style="width: 300px; height: 200px; float: left;"></div>

            <input type="button" value="Receive" onclick="receiveVideo()" style="float: left;" />

            <div id="divPeeruser" class="video_second" style="width: 300px; height: 200px; float: left;"></div>

            <div id="fileUploadDialog" title="Send File">
                <p>
                    <input type="file" id="fileUpload" name="fileUpload" size="23" />
                </p>
                <p>
                    <button id="uploadButton">Upload</button>
                </p>
            </div>
        </div>
    </form>


    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <script src="Scripts/jquery.jqote2.js"></script>
    <script src="Scripts/jquery.ajaxfileupload.js"></script>
    <script type="text/javascript">
        var guid;
        $(document).ready(function () {
            var templateUrl = $('#templates').data('url');
            $('#templates').load(templateUrl, function () {

                // Set default jqote tag
                $.jqotetag('*');
                //  brodcastvideo();



            });
            $("#uploadButton").click(function () {

                $.ajaxFileUpload({
                    url: "SendFile.ashx",
                    secureuri: false,
                    fileElementId: 'fileUpload',
                    dataType: 'json',
                    success: function (data, status) {
                        //$('#fileUploadDialog').dialog('close');
                        //if (typeof (data.error) != 'undefined') {
                        //    if (data.error != '') {
                        //        alert(data.error);
                        //    } else {
                        //        var messagePanel = getPanelByUserId(toUserId);
                        //        messagePanel.append($('#fileSentTemplate').jqote());
                        //    }
                        //}
                    },
                    error: function (data, status, e) {
                        alert(e);
                    }
                });
            });
        });
        function brodcastvideo() {
            $.ajax({
                type: "POST",
                url: "HickChatEngine.svc/BroadcastVideo",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {

                    var currentVideoBroadcastGuid = String(result.d.BroadcastVideoGuid);
                    guid = currentVideoBroadcastGuid;
                    var flashMediaServer = String(result.d.FlashMediaServer);
                    var broadcastVideoWidth = String(result.d.BroadcastVideoWidth);
                    var broadcastVideoHeight = String(result.d.BroadcastVideoheight);

                    var videoWindow = $('#broadcastVideoWindowTemplate').jqote({ Guid: currentVideoBroadcastGuid, FlashMediaServer: flashMediaServer, BroadcastVideoWidth: broadcastVideoWidth, BroadcastVideoHeight: broadcastVideoHeight });

                    $('#divLoggedinuser').append(videoWindow);

                    $('#divLoggedinuser').css("background-image", "");
                    $('#divLoggedinuser').css("background-color", "black");


                }
            });
        }

        function receiveVideo() {
            var flashMediaServer = "rtmp://fms.aspnetdating.com/live";
            var senderUserId = '2';

            var videoWindow = $('#receiveVideoWindowTemplate').jqote({ SenderUserId: senderUserId, Guid: guid, FlashMediaServer: flashMediaServer });
            $('#divPeeruser').html("");
            $('#divPeeruser').append(videoWindow);

            $('#divPeeruser').css("background-image", "");
            $('#divPeeruser').css("background-color", "black");

        }



        function test() {
            $.ajax({
                type: "POST",
                url: "SendFile.ashx",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {



                }
            });
        }
    </script>
</body>

</html>
