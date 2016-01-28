<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCMostRecent.ascx.cs"
    Inherits="Hick.PatientLookUp.UserControls.MostRecent" %>

    <div class="patsearch_heading">
        Most Recent Visit
         <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popup_close" class="pull-right" style="cursor: pointer;margin-top:-5px;"
                alt="close" />
    </div>
    <div class="patsearch_border">
    <!-- Template you create dynamically -->
    <div class="conditions_template" style="width:100%;display:inline-flex">
        <div style="margin-left: 40px;">
            <div class="standard_label">
                <u style="color:Blue; margin-right:10px;">Lab Results </u>(1MB)
            </div>
            <div class="standard_text" style="width:200px;margin-top:0px;">
                Active Since : 10/08/2015
            </div> 
        </div>  
        <img src="../../Images/download.png" alt="download" style="width: 25px;height:30px;cursor:pointer;margin-left:30px;" onclick="#"/>
    </div>
    <!-- Template ends here -->
    <div id="btn_groups" style="margin-left: 45px; display:none;">
        <input type="button" value="SAVE" id="btn_save_mostrecent" class="btn_standard" />
        <input type="button" value="CANCEL" id="btn_cancel_mostrecent" class="btn_standard" />
    </div>
</div>


<script type="text/javascript">
    $("#div_patientsearch").css("display", "block");

    $("#patientsearch_leftpart").css("display", "block");
    $("#imgsearchuser").css("display", "block");

    $("#popup_close").click(function () {
        parent.popup_close();
    });
</script>