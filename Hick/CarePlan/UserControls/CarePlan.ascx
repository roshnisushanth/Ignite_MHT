<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CarePlan.ascx.cs" Inherits="Hick.CarePlan.UserControls.CarePlan" %>

<style>
</style>
<div class="patsearch_heading patient">
    Care Plan
    <input type="button" value="APPLY" name="apply" class="btn_standard" id="apply" style=" margin-left :434px;margin-top:-28px;" />
    <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popup_close" class="pull-right" style="cursor: pointer;margin-top:-5px;"
         alt="close" />
</div>
<div class="patsearch_border care">
    <p>Please select care plans and docuements that would like to add to the patent's library.</p>
    <from id="planfrom">
        <div class="col-md-6 col-sm-6 col-lg-6">
            <h1>Care Plans</h1>
            <ul class="care-ul">
                <li><a><input type="checkbox" value="Arrhythma / Stroke" id="1"  /> Arrhythma / Stroke</a></li>
                <li><a><input type="checkbox" value="CHF" id="2" /> CHF</a></li>
                <li><a><input type="checkbox" value="Gastrontestnal" id="3" /> Gastrontestnal</a></li>
                <li><a><input type="checkbox" value="HEENT" id="4" /> HEENT</a></li>
                <li><a><input type="checkbox" value="Hyper / Hypothyroidism" id="5" /> Hyper / Hypothyroidism</a></li>
                <li><a><input type="checkbox" value="Respiratory" id="6" /> Respiratory </a></li>
                <li><a><input type="checkbox" value="Diabetes" id="7" /> Diabetes</a></li>
                <li><a><input type="checkbox" value="COPD" id="8" />COPD</a></li>
                <li><a><input type="checkbox" value="Hypertension" id="9" />Hypertensio </a></li>
            </ul>
        </div>

        <div class="col-md-6 col-sm-6 col-lg-6">
            <h1>Bed Lights / Misc</h1>
            <ul class="care-ul">
                <li><a href="CarePlan.ascx"><input type="checkbox" value="Red Flag Heart Disease" id="10" /> Red Flag Heart Disease</a></li>
                <li><a href="CarePlan1.ascx"><input type="checkbox" value="Red Flag Heart Failure" id="11" /> Red Flag Heart Failure</a></li>
                <li><a href="CarePlan2.ascx"><input type="checkbox" value="Red Flag Wounds" id="12" /> Red Flag Wounds</a></li>
                <li><a href="CarePlan3.ascx"><input type="checkbox" value="Red Flag Afib" id="13" /> Red Flag Afib</a></li>
                <li><a><input type="checkbox" value="Red Flag Depression" id="14" />Red Flag Depression</a></li>
                <li><a><input type="checkbox" value="Red Flag Pneumonia" id="15" />Red Flag Pneumonia</a></li>
                <li><a><input type="checkbox" value="Red Flag Respiratory Disea" id="16" /> Red Flag Respiratory Disea</a></li>
                <li><a><input type="checkbox" value="Red Flag Diabetes" id="17" />Red Flag Diabetes</a></li>
                <li><a><input type="checkbox" value="Sliding Scale Insulin" id="18" />Sliding Scale Insulin</a></li>
                <li><a><input type="checkbox" value="Red Flag Falls" id="19" />Red Flag Falls</a></li>
                <li><a><input type="checkbox" value="Sliding Scale Insulin" id="20" />Red Flag COPD</a></li>
                <li><a><input type="checkbox" value="Red Flag Hypertension" id="21" />Red Flag Hypertension</a></li>
                <li><a><input type="checkbox" value="Foods with Vitamin K" id="22" />Foods with Vitamin K</a></li>
            </ul>
        </div>
    </from>

    <div id="result"></div>

    <br />








    <script>
    $(document).ready(function () {
        $("#div_patientsearch").css("display", "block");
        $("#patientsearch_leftpart").css("display", "block");
        $("#imgsearchuser").css("display", "block");
        $("#popupclose").click(function () {
            $("#divshowmedications").dialog('close');
        });


    });

    $("#popup_close").click(function () {
        parent.popup_close();
    });



  

    $('#apply').click(function () {
        var arrCB = {};
        $(".care input[type='checkbox']").each(function () {
            var el = $(this);
            var id = el.attr('id');
            if (this.checked) {
                var ids = $(this).attr('id');
                var vals = $(this).attr('value');
                //var links = $(this).parent().attr('href', '#');
                //var finalVar = vals + links;
            }
            //arrCB[id] = (this.checked ? 1 : 0);

            arrCB[id] = vals;
           
            var arrMN = arrCB[id];
            var value = el.attr('value');
            if (arrMN == 1) {
                alert(value);
            } else
            {
               // alert('No');
            }
            //alert(arrMN);
        });
        var text = JSON.stringify(arrCB);
        alert(text);

       
    });



    </script>
