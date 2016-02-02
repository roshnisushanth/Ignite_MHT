<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCCallToActionBar.ascx.cs"
    Inherits="Hick.Shared.UCCallToActionBar" %>

<nav  class="navbar navbar-default toolbar" role="navigation">
                  <img hidden="hidden" src='<%=Page.ResolveUrl("~/Images/icon_Encounter.png") %>' alt="Assessments" style="cursor: pointer; width: 37px;" id="tblAssessments" title="Assessments" /> 
     <span id="nvcalltoaction">
 
        <img src='<%=Page.ResolveUrl("~/Images/search_icon.png") %>' alt="search_img" style="cursor: pointer; width: 37px;" id="patientsearch" title="Patient LookUp" />
        <%--<img src="images/user-icon.png" style="cursor:pointer;" id="temp_popup"/>--%>
        <img src='<%=Page.ResolveUrl("~/Images/Careplan.png") %>' style="cursor: pointer;" class="js-careplan" title="Care Plan" alt="CC" />
        <img src='<%=Page.ResolveUrl("~/Images/icon_details.png") %>' style="cursor: pointer;" class="js-commandcenter" title="Command Center" alt="CC" />
        <span class="tmr-mgmt" style="/*padding-left: 5px; padding-right: 5px; */ padding-top: 9px; padding-bottom: 10px; background-color: #CCCCCC;">
            <img src='<%=Page.ResolveUrl("~/Images/icon_refresh.png") %>' style="cursor: pointer; background-color: #CCCCCC; width: 37px;" id="timermanagement" title="Timer Management" alt="TM" />

             <img src='<%=Page.ResolveUrl("~/Images/session-note.png") %>' style="cursor: pointer; width: 37px;" id="tblsessionnote" title="Session Notes" alt="MR"  /> 
        </span>
           		<span class="mu-mgmt">
            <img src='<%=Page.ResolveUrl("~/Images/MUdashboard.png") %>' style="cursor: pointer;  width: 37px;" id="mudashboard" title="MU Dashboard" alt="MU" />

        </span>     
    </span>
       
</nav>
<div style="display: none;">
    <div id="divpatientsearch" style="z-index: 10000;">
        <div style="float: left;">
            <iframe id="patientsearch_popup" src="" style="overflow: auto; position: fixed; z-index: 10000; width: 86%; height: 537px; border: 0px;"></iframe>
        </div>
        <div style="float: right; margin-right: 10px;">
            <%--<img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popupclose" style="cursor: pointer;"
                alt="close" />--%>
        </div>
    </div>
</div>
<div style="display: none;">
    <div id="divencounters" style="z-index: 10000;">
        <div style="float: left;">
            <iframe id="encounter_popup" src="" style="overflow: auto; position: fixed; z-index: 10000; width: 86%; height: 537px; border: 0px;"></iframe>
        </div>
        <div style="float: right; margin-right: 10px;">

        </div>
    </div>
</div>

<script type="text/javascript">
    if ($("#hdnusertype").html().trim() == 'patient') {
        $("#nvcalltoaction").css("display", "none");
        $("#tblmyrecord").css("visibility", "visible");
        $("#tblsessionnote").css("display", "none");
        $(".js-careplan");
        $(".js-commandcenter").css("display", "none");
        $("#mudashboard").css("display", "none");
        $("#tblAssessments").css("display", "none");
    }
    else if ($("#hdnusertype").html().trim() == 'admin')
    {
        $(".js-commandcenter").css("display", "none");
        $(".js-careplan");
        $("#tblAssessments").css("display", "none");
        $("#tblmyrecord").css("display", "none");
        $("#tblAssessments").css("display", "inline-block");
        $("#mudashboard").css("visibility", "visible");
    }
    else {
        $("#nvcalltoaction").css("visibility", "visible");
        $("#tblmyrecord").css("display", "none");
        $("#tblsessionnote").css("visibility", "visible");
        $(".js-commandcenter").css("visibility", "visible");
        $(".js-careplan");
        $("#mudashboard").css("display", "none");
    }

    $("#popupclose").click(function () {
        $("#patientsearch_popup").attr("src", "");
        $("#divpatientsearch").dialog('close');
    });

    $("#timermanagement").click(function () {
        if (_isvideocall == false) {
            commonpopup("PatientLookUp/ASPX/TimerLog.aspx?UserId=" + userid + "&PatientId=" + _peerid + "", this.id);
        }
    });

    $("#myrecord").click(function () {
        if (_isvideocall == false) {  
            commonpopup("PatientLookUp/ASPX/Recordaccess.aspx?pageview=record");
        }

    });

    $("#tblsessionnote").click(function () {
        if (_isvideocall == false) {
            commonpopup("PatientLookUp/ASPX/SessionNote.aspx?peerId=" + _peerid, this.id);
        }
    });

    $("#tblmyrecord").click(function () {
       
        commonpopup("PatientLookUp/ASPX/PatientPhpSummary.aspx?pageview=record&ptid=" + UserRefID, this.id);
       
    });
    $("#patientsearch").click(function () {
        //if (_isvideocall == false) {
            commonpopup("PatientLookUp/ASPX/PhpSummary.aspx?ptid=" + UserRefID, this.id);
        //}
    });
    var aDialog;
    $("#tblAssessments").click(function () {
        console.log("aId=" + _peerid);
        aDialog = popupNonModel("Encounters/ASPX/Assessments.aspx?peerId=" + _peerid, this.id);
    });

    $("#mudashboard").click(function () {
        if (_isvideocall == false) {
            commonpopup("SuperUser/ASPX/MUReport.aspx");
        }
    });


    $(function () {
        $("#divpatientsearch").draggable();
    });

    $("#temp_popup").click(function () {
        if (_isvideocall == false) {
            commonpopup("PatientLookUp/ASPX/ReferralType.aspx");
        }
    });
    $(".js-commandcenter").click(function () {
        if (_isvideocall == false) {
            commonpopup("CommandCenter/ASPX/Dashboard.aspx");
        }
    });

    $(".js-careplan").click(function () {
        if (_isvideocall == false) {
            commonpopup("CarePlan/ASPX/CarePlan.aspx");
        }
    });

    $("#temp_popup").click(function () {
        if (_isvideocall == false) {
            commonpopup("PatientLookUp/ASPX/ReferralType.aspx");
        }
    });

    function assesummary_close() {
        $("#encounter_popup").attr("src", "");
        $("#divencounters").dialog('close');
        //$("#patientsearch_popup").attr("src", "");
        //$("#divpatientsearch").dialog('close');
        //commonpopup("Encounters/ASPX/Assessments.aspx", 'tblAssessments');
    }

    function popup_close() {
        $("#patientsearch_popup").attr("src", "");
        $("#divpatientsearch").dialog('close');
    }

    function popupNonModel(pageaddress, id) {
        var popupwidth = '87%';
        var popuphight = window.innerHeight - 100;
        $("#patientsearch_popup").attr("src", "");
        $("#patientsearch_popup").attr("src", pageaddress);
        $("#patientsearch_popup").css('width', '86%');
        if (id == 'tblAssessments') { popupwidth = '64.5%'; $("#patientsearch_popup").css('width', '64%'); }
        if (id == 'timermanagement') { popupwidth = '64.5%'; $("#patientsearch_popup").css('width', '64%'); }
        //$("#divpatientsearch").draggable();
        return $("#divpatientsearch").dialog({
            modal: false,
            height: popuphight,
            draggable: true,
            width: popupwidth,
            resizable: false,
            init: function () {
                $("#divpatientsearch").draggable();
            },
            create: function () {
                $(".ui-dialog-titlebar").hide();
                $(".ui-dialog-content").css("padding", "0px");
            }
        });
    };

    function commonpopup(pageaddress, id) {
        var popupwidth = '75%';
        var popuphight = window.innerHeight - 100;
        $("#patientsearch_popup").attr("src", "");
        $("#patientsearch_popup").attr("src", pageaddress);
        $("#patientsearch_popup").css('width', '74.5%');
        if (id == 'tblAssessments') { popupwidth = '64.5%'; $("#patientsearch_popup").css('width', '64%'); }
        if (id == 'timermanagement') { popupwidth = '64.5%'; $("#patientsearch_popup").css('width', '64%'); }
        $("#divpatientsearch").dialog({
            modal: true,
            height: popuphight,
            width: popupwidth,
            resizable: false,
            dialogClass: 'patientSearch-dialog',
            //title: "Patient Search",
            create: function () {
                $(".ui-dialog-titlebar").hide();
                $(".ui-dialog-content").css("padding", "0px");
            }
        });
    };

    $("#entPopupClose").click(function () {
        $("#encounter_popup").attr("src", "");
        $("#divencounters").dialog('close');
        $("#patientsearch_popup").attr("src", "");
        $("#divpatientsearch").dialog('close');
    });

    function encounterPopup(pageaddress, id) {
        var popupwidth = '87%';
        var popuphight = window.innerHeight - 100;
        $("#encounter_popup").attr("src", "");
        $("#encounter_popup").attr("src", pageaddress);
        $("#encounter_popup").css('width', '86%');

        $("#divencounters").dialog({
            modal: false,
            height: popuphight,
            width: popupwidth,
            draggable: true,
            resizable: true,
            //title: "Patient Search",
            create: function () {
                $(".ui-dialog-titlebar").hide();
                $(".ui-dialog-content").css("padding", "0px");
            }
        });
    };

</script>
