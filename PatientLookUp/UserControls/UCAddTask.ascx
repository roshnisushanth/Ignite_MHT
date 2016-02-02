<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCAddTask.ascx.cs" Inherits="Hick.PatientLookUp.UserControls.UCAddTask" %>

<link rel="stylesheet" type="text/css" href=" <%=Page.ResolveUrl("~/scripts/timeentry/jquery.timeentry.css") %> ">
<script type="text/javascript" src=" <%=Page.ResolveUrl("~/scripts/timeentry/jquery.plugin.js") %> "></script>
<script type="text/javascript" src=" <%=Page.ResolveUrl("~/scripts/timeentry/jquery.timeentry.js") %> "></script>
<style>
.cls-popup{margin-top:0;}
</style>

<div class="patsearch_border" style="overflow-x: hidden;">
    <div class="cls-popup">
        <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popupcloses" style="cursor: pointer;"
            alt="close" />
    </div>

    <div class="patsearch_heading">
        <asp:Label Text="Add Task" runat="server" ID="lblheader" />
    </div>
    <div style="padding-left: 10px;">
        <span id="successmsg" style="color: Green;"></span><span id="errormsg" style="color: Red;"></span>
    </div>
    <div class="addtask_subheading" style="display: inline-flex;">
        <div style="width: 20%; text-align: center;">
            Date
        </div>
        <div style="width: 20%; text-align: center;">
            Category
        </div>
        <div style="width: 20%; text-align: center;">
            Start Time
        </div>
        <div style="width: 20%; text-align: center;">
            End Time
        </div>
        <div style="width: 20%; text-align: center;">
            Total Time
        </div>
    </div>

    <div class="addtask_controls" style="display: inline-flex;">
        <div style="width: 20%; text-align: center;">
            <div style="width: 80%; text-align: center; height: 100%; margin: 0 auto; border: 2px solid #E2E4E5; border-radius: 5px; display: inline-flex;"
                id="divsdate">
                <div style="width: 75%;">
                    <input id="taskdate" type="text" runat="server" style="width: 100%; float: left; height: 100%; border-width: 0px; text-align: center; padding: 7px;"
                        placeholder="Enter Date" ReadOnly="readonly"/>
                </div>
                <div style="width: 25%;">
                    <img src="../../Images/calendar.jpg" style="width: 28px; margin: 3px;" id="calendar_img" />
                </div>
            </div>
        </div>
        <div style="width: 20%; text-align: center;">
            <div style="width: 80%; text-align: center; height: 100%; margin: 0 auto; border: 2px solid #E2E4E5; border-radius: 5px; display: inline-flex;" id="divCategory">
                <div style="width: 100%;">
                    <%--<input type="text" style="width:100%; float:left; height:100%; border-width:0px; text-align:center; padding:7px;" placeholder="Select Category"/>--%>
                    <select style="width: 100%; float: left; height: 100%; border-width: 0px; text-align: center; padding: 6px;"
                        id="dropdowncategory" runat="server">
                        <option value="0">Select Category</option>
                        <option value="1">System</option>
                        <option value="2">Chat Review</option>
                    </select>
                </div>
            </div>
        </div>
        <div style="width: 20%; text-align: center;" class="form-horizontal">
            <div style="width: 80%; text-align: center; height: 100%; margin: 0 auto; border: 2px solid #E2E4E5; border-radius: 5px; display: inline-flex;" id="divStartTime">
                <div style="width: 100%;">
                    <input type="text" class="form-control timeRange timeEntry" style="width: 100%; height: 100%; border-width: 0px; text-align: center; padding: 7px; padding-right: 0px;"
                        placeholder="HH:MM" id="starttime" clientidmode="Static" runat="server" />
                </div>
            </div>
            <%--<input type="text" size="10" class="timeRange timeEntry" id="timeFrom"> --%>
        </div>
        <div style="width: 20%; text-align: center;" class="form-horizontal">

            <%-- <input type="text" size="10" class="timeRange timeEntry" id="timeTo">--%>

            <div style="width: 80%; text-align: center; height: 100%; margin: 0 auto; border: 2px solid #E2E4E5; border-radius: 5px; display: inline-flex;" id="divEndTime">
                <div style="width: 100%;">
                    <input type="text" class="form-control timeRange timeEntry" style="width: 100%; height: 100%; border-width: 0px; text-align: center;"
                        placeholder="HH:MM" id="endtime" runat="server" clientidmode="Static" />
                </div>
            </div>

        </div>
        <div style="width: 20%; text-align: center;">
            <div style="width: 70%; text-align: center; height: 100%; margin: 0 auto; display: inline-flex; padding: 6px; border-width: 0px;">
                <div style="width: 25%;">
                    <input type="text" style="width: 100%; float: left; height: 100%; border-top-width: 0px; border-left-width: 0px; border-right-width: 0px; border-bottom-style: solid; text-align: center; padding: 5px;"
                        readonly clientidmode="Static" id="hours" runat="server" />
                </div>
                <div style="width: 25%; padding: 6px;">
                    <span style="color: Gray;">h</span>
                </div>
                <div style="width: 25%;">
                    <input type="text" style="width: 100%; float: left; height: 100%; border-top-width: 0px; border-left-width: 0px; border-right-width: 0px; border-bottom-style: solid; text-align: center; padding: 5px;"
                        readonly clientidmode="Static" id="mins" runat="server" />
                </div>
                <div style="width: 25%; padding: 6px;">
                    <span style="color: Gray;">m</span>
                </div>
            </div>
        </div>
    </div>
    <div class="addtask_entertaskborder">
        <div style="width: 94%; margin-left: 2%;">
            <%--<input type="text" style="width:100%; height:100%; border:2px solid #E2E4E5; border-radius:5px;" />--%>
            <textarea rows="4" style="width: 100%;resize:none; border: 2px solid #E2E4E5; border-radius: 6px;"
                placeholder="Enter Task" id="task" runat="server"></textarea>
        </div>
    </div>
    <div style="float: right; padding-top: 20px; padding-right: 10px;">
        <input type="button" style="background-color: #FFCB05; width: 100px; height: 30px; border-width: 0px; font-weight: 600;"
            value="Save" id="btnsave" />
    </div>
    <input type="text" style="display: none;" id="hdntotaltime" />
    <input id="taskid" type="text" runat="server" style="display: none;" />
    <input id="tasktype" type="text" runat="server" style="display: none;" />
</div>
<script type="text/javascript">
    var t1 = 0;
    var t2 = 0;

    $(document).ready(function () {
        $('.timeRange').timeEntry({spinnerImage: '', beforeShow: function(input){
            return {minTime: (input.id === 'endtime' ?
                    $('#starttime').timeEntry('getTime') : null),
                maxTime: (input.id === 'starttime' ?
                $('#endtime').timeEntry('getTime') : null)};

        }});
        $( "#endtime" ).blur(function() {
            calculateDiff();
        });
    });

    $("#div_timermanagement").css("display", "block");
    $("#TimerManagement_LeftPart").css("display", "block");
    $("#timermanagement").css("display", "block");

    $('#<%=taskdate.ClientID%>').datepicker({ maxDate: 0 });
    $('#calendar_img').click(function () {
        $('#<%=taskdate.ClientID%>').datepicker('show');
    });

    function convertTo24Hour(time) {
        var hours = parseInt(time.substr(0, 2));
        if(time.indexOf('am') != -1 && hours == 12) {
            time = time.replace('12', '0');
        }
        if(time.indexOf('pm')  != -1 && hours < 12) {
            time = time.replace(hours, (hours + 12));
        }
        return time.replace(/(am|pm)/, '');
    }

    function TimeDiff(a,b)
    {
        var first = a.split(":"),second = b.split(":"),xx,yy;
        if(parseInt(first[0]) < parseInt(second[0])){          
            if(parseInt(first[1]) < parseInt(second[1])){
                yy = parseInt(first[1]) + 60 - parseInt(second[1]);
                xx = parseInt(first[0]) + 24 - 1 - parseInt(second[0])
            }else{
                yy = parseInt(first[1]) - parseInt(second[1]);
                xx = parseInt(first[0]) + 24 - parseInt(second[0])
            }
        }else if(parseInt(first[0]) == parseInt(second[0])){
            if(parseInt(first[1]) < parseInt(second[1])){
                yy = parseInt(first[1]) + 60 - parseInt(second[1]);
                xx = parseInt(first[0]) + 24 - 1 - parseInt(second[0])
            }else{
                yy = parseInt(first[1]) - parseInt(second[1]);
                xx = parseInt(first[0]) - parseInt(second[0])
            }
        }else{
            if(parseInt(first[1]) < parseInt(second[1])){
                yy = parseInt(first[1]) + 60 - parseInt(second[1]);
                xx = parseInt(first[0]) - 1 - parseInt(second[0])
            }else{
                yy = parseInt(first[1]) - parseInt(second[1]);
                xx = parseInt(first[0]) - parseInt(second[0])
            }
        }
        if(xx < 10){ xx = "0" + xx}
        if(yy < 10){ yy = "0" + yy}
        return (xx + ":" + yy)  
    }

   

    function calculateDiff(){
        var starttime = convertTo24Hour($('#starttime').val().toLowerCase()),
            endtime = convertTo24Hour($('#endtime').val().toLowerCase()),
            difftime =  TimeDiff(endtime,starttime),
            arr = difftime.split(':'),
            h=arr[0],
            m=arr[1];

        $('#hours').val(h);
        $('#mins').val(m);
        $("#hdntotaltime").val(h + ':' + m)

    }
    function getTimeDiff(time1, time2) {
        var totaltime = t2 - t1;
        var h = parseInt(totaltime / 60);
        var m = parseInt(totaltime % 60);
        if (h < 10) {
            h = "0" + h;
        }
        if (m < 10) {
            m = "0" + m;
        }
        $("#hdntotaltime").val(h + ':' + m)
    }

    $("#btnsave").click(function () {
        var taskid = $("#<%=taskid.ClientID%>").val();
        var tasktype = $("#<%=tasktype.ClientID%>").val();
        var taskdate = $("#<%=taskdate.ClientID%>").val();
        var category = $("#<%=dropdowncategory.ClientID%>").val();
        var starttime = $('#starttime').val();
        var endtime = $('#endtime').val();
        calculateDiff();
        var totaltime =  $("#hdntotaltime").val();
        var task = $("#<%=task.ClientID%>").val();
        var allclear = false;

        if (taskdate == '') {
            allclear = false;
            $("#divsdate").addClass("requiredfield");           
        }
        else {
            allclear = true;
            $("#divsdate").removeClass("requiredfield");
        }
        /////////////

        if (category == '0') {
            allclear = false;            
            $("#divCategory").addClass("requiredfield");           
        }
        else {
            allclear = true;
            $("#divCategory").removeClass("requiredfield");
        }
       
        if ($("#starttime").val() == '') {
            allclear = false;            
            $("#divStartTime").addClass("requiredfield");
        }
        else {
            allclear = true;
            $("#divStartTime").removeClass("requiredfield");
        }

        if ($("#endtime").val() == '') {
            allclear = false;            
            $("#divEndTime").addClass("requiredfield");
        }
        else {
            allclear = true;
            $("#divEndTime").removeClass("requiredfield");
        }

        if (allclear == true) {
            $.ajax({
                type: 'POST',
                data: '{"taskid":"' + taskid + '","tasktype":"' + tasktype + '","task":"' + task + '","taskdate":"' + taskdate + '","category":"' + category + '","starttime":"' + starttime + '","endtime":"' + endtime + '","totaltime":"' + totaltime + '"}',
                url: 'AddTask.aspx/AddTaskDetails',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (msg) {
                    if (taskid.length > 0 && tasktype.length > 0) {
                        showSucess({ text: 'Task updated successfully.',callback: function(){ closeTimerPopUp(); }});
                    }
                    else {
                        showSucess({ text: 'Task added successfully.',callback: function(){ closeTimerPopUp(); }});
                        
                    }
                    //$("#successmsg").show().delay(5000).fadeOut();

                    $("#dropdowncategory [value=0]").attr('selected', true);
                    $("#taskdate").val("");
                    $("#starttime").val("");
                    $("#endtime").val("");
                    $("#hours").val("");
                    $("#mins").val("");
                    $("#task").val("");
                },
                error: function (a, b, c) {
                    showError("Sorry an error occured, please contact administrator.");
                    //$("#errormsg").text("Sorry an error occured, please contact administrator.");
                }
            });
        }
    });

    function parseTime(s) {
        var part = s.match(/(\d+):(\d+)(?: )?(am|pm)?/i);
        var hh = parseInt(part[1], 10);
        var mm = parseInt(part[2], 10);
        var ap = part[3] ? part[3].toUpperCase() : null;
        if (ap === "AM") {
            if (hh == 12) {
                hh = 0;
            }
        }
        if (ap === "PM") {
            if (hh != 12) {
                hh += 12;
            }
        }
        return { hh: hh, mm: mm };
    }

    $("#starttime").keypress(function (e) {
        if (e.keyCode > 31 && (e.keyCode < 48 || e.keyCode > 57)) {
            return false;
        }
        else {
            return true;
        }
    });

    $("#endtime").keypress(function (e) {
        if (e.keyCode > 31 && (e.keyCode < 48 || e.keyCode > 57)) {
            return false;
        }
        else {
            return true;
        }
    });

    function closeTimerPopUp(){
        window.location = "TimerLog.aspx?UserId=" + <%=Session["userid"].ToString()%> + "&PatientId=" + <%=Session["patientid"].ToString()%>;
    }

    $("#popupclose").click(function () {
        closeTimerPopUp();
    });
</script>

<script type="text/javascript">
    var specialKeys = new Array();
    specialKeys.push(8); //Backspace
    //specialKeys.push(58); //colon
    function IsNumeric(e) {
        var keyCode = e.which ? e.which : e.keyCode
        var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1 );
        document.getElementById("error").style.display = ret ? "none" : "inline";
        return ret;
    }

      $("#popupcloses").click(function () {
          closeTimerPopUp();
    });
</script>
