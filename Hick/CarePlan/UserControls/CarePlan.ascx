<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CarePlan.ascx.cs" Inherits="Hick.CarePlan.UserControls.CarePlan" %>

<style>
</style>
<div class="patsearch_heading patient">
        Care Plan
         <input type="button" value="APPLY" name="apply" class="btn_standard" id="apply" style=" margin-left: 434px;margin-top:-28px;" />
        <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popup_close" class="pull-right" style="cursor: pointer;margin-top:-5px;"
                alt="close" />
    </div>
<div class="patsearch_border care">
    <p>Please select care plans and docuements that would like to add to the patent's library.</p>
    <from id="planfrom">
    <div class="col-md-6 col-sm-6 col-lg-6">
        <h1>Care Plans</h1>
        <ul class="care-ul">
            <li><a><input type="checkbox" value="Arrhythma / Stroke" id="1"/> Arrhythma / Stroke</a></li>
            <li><a><input type="checkbox" value="CHF" id="2"/> CHF</a></li>
            <li><a><input type="checkbox" value="Gastrontestnal" id="3"/> Gastrontestnal</a></li>
            <li><a><input type="checkbox" value="HEENT" id="4"/> HEENT</a></li>
        </ul>
    </div>

    <div class="col-md-6 col-sm-6 col-lg-6">
        <h1>Bed Lights / Misc</h1>
        <ul class="care-ul">
            <li><a><input type="checkbox" value="Red Flag Heart Disease" id="5"/> Red Flag Heart Disease</a></li>
            <li><a><input type="checkbox" value="Red Flag Heart Failure" id="6"/> Red Flag Heart Failure</a></li>
            <li><a><input type="checkbox" value="Red Flag Wounds" id="7"/> Red Flag Wounds</a></li>
            <li><a><input type="checkbox" value="Red Flag Afib" id="8"/> Red Flag Afib</a></li>
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



    var url = 'http://localhost:57520/CarePlan/UserControls/datas.json';
    $.ajax({
        type: 'GET',
        url: url,
        async: false,
        contentType: "application/json",
        dataType: 'json',
        success: function (data) {
            alert('success');
            //console.log(data);
            var obj = JSON.stringify(data);
            var main = data;
            alert(obj);
            alert(main);
        },
        error: function (e) {
            alert('error');
            console.log(e);
        }
    });
   
    $('input[type="checkbox"]').on('click', function () {
        var value = $(this).attr('id');
        alert(value);
    });

    //Test
    
    //var carevalue = [];

    //function questionCount() {

    //    $.each($('input[type="checkbox"]:checked'), function () {
    //        var value = $(this).val();
    //        carevalue.push(value);
    //    });           

    //    var json = JSON.stringify(carevalue);
    //    var obj = JSON.parse(json);

    //    while (obj.length > 0) {
    //        $(".new-lists").append("<li><a href=''>" + obj.pop() + "</a></li>");
    //    }            
    //        }

    //$(document).ready(function () {
    //    $('#apply').click(function () {
    //        questionCount();
    //    });
    //});


   
</script>