<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCMUReport.ascx.cs" Inherits="Hick.SuperUser.UserControls.UCMUReport" %>

<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap-theme.min.css">
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#cplSuperUser_MUReports_fromdate').datepicker({ maxDate: new Date() });
            $('#calendar_img').click(function () {
                $('#cplSuperUser_MUReports_fromdate').datepicker('show');
            });

            $('#cplSuperUser_MUReports_todate').datepicker({ maxDate: new Date() });
            $('#calendar_imgs').click(function () {
                $('#cplSuperUser_MUReports_todate').datepicker('show');
            });
        });
    </script>
	<script type="text/javascript">
	    function PrintGridData() {
	        document.getElementById('<%=printMUReport.ClientID %>').style.display = 'block';
	        var reportingperiod = document.getElementById('<%=reportingperiodValue.ClientID %>').innerText;
	        var data = "";
	        if (reportingperiod == null || reportingperiod == "")
	        {
	            document.getElementById('<%=customdateprint.ClientID %>').style.display = 'block';
                document.getElementById('<%=reportingprd.ClientID %>').style.display = 'none';
	        }
	        else {
                 document.getElementById('<%=customdateprint.ClientID %>').style.display = 'none';
                document.getElementById('<%=reportingprd.ClientID %>').style.display = 'block';
	        }
            var printGrid = document.getElementById('<%=printMUReport.ClientID %>');
	        data = "<html><head><title></title></head><body >" + printGrid.outerHTML
                + "</body></html>";
	        var printwin = window.open('', 'PrintGridView', 'left=100,top=100,width=400,height=400,tollbar=0,scrollbars=1,status=0,resizable=1');

	    printwin.document.write(data);
        printwin.document.close();
        printwin.focus();
        printwin.print();
        printwin.close();
    }
   </script> 

            <style>
      .col-lg-4 ,.col-md-4{
           padding-right: 5px;
           padding-left: 5px;
         }
     .patsearch_border.meaning .popup_textbox{margin-left:5px; width: 120px; padding:0 5px;}
     .sect-rom .col-lg-5.col-md-5.col-sm-5.col-xs-12 { padding-left: 0;}
     .sect-rom .col-lg-6.col-md-6.col-sm-6.col-xs-12 { padding-left: 0;}
     .sect-rom select{height: 30px;}
     .txtbox_spanimg {  margin-left: 5px;}.mes-no th, .mes-no td{padding:3px;}
     .manis .col-lg-3.col-md-3.col-sm-3.col-xs-12.font-bold {width: 85px;}
     .manis .col-lg-3.col-md-3.col-sm-3.col-xs-12 {width: 24%; font-weight: bold; display: table-row;}
     .manis .col-lg-3.col-md-3.col-sm-3.col-xs-12 span{display: table-cell;}
     .manis .col-lg-3.col-md-3.col-sm-3.col-xs-12 label{display: table-cell; font-size: 12px;}
     .patsearch_border.meaning .col-lg-12.col-md-12.hdr.summ.head{overflow-y: scroll;  height: 476px;   width: 74%;}
     .btn_standard.btn-stan{ height: 25px;  min-width: auto; font-size:12px;}
     .modal-content { border-radius: 0;}/*.chec-main{min-height:18px;}*/
     .view-pop-det{height: 300px; overflow-y: scroll; overflow-x: hidden;}
     .ui-dialog.ui-widget.ui-widget-content.ui-corner-all.ui-front.ui-draggable{    width: 870px!important;
    top: 100px;    left: 52px!important;
}ul.calend-check.clearfix.firs {
    /*display: block!important;*/
}
#MURDView { cursor: pointer;}
.patsearch_border.meaning .col-lg-12.col-md-12.hdr.summ.head{position:inherit; width:100%;}
.patsearch_border{overflow:hidden;}
.report-period.sect-rom { padding-top: 15px;}
.patsearch_heading.patient img.pull-right{margin-top: -5px;}
.col-lg-3.col-md-3.col-sm-3.col-xs-12.rpt-top{width:155px;}
input[type=checkbox]{margin-right:3px;}
.froms-lft{width:222px;}
#popup_close{cursor:pointer; position:absolute; right:0; z-index:99;}
.use-report{display:inline-block; width:80%;}
    </style>
<script>

            $(document).ready(function () {

                $("#popup_close").click(function (e) {
                    parent.popup_close();
                });

            });
    </script>
 
    
    <div class="patsearch_heading patient">
        <span class="use-report">Meaningful Use Reports</span>
        <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popup_close" class="pull-right" alt="close" />
    </div>
    
        <div class="patsearch_border meaning">
        <%--div for profile pic and info--%>
        <div class = "col-lg-12 col-md-12 hdr summ head" style="margin-bottom:20px;">
		    <div id="printdata" runat="server">
		  <div class="report-period sect-rom">
              <div class="show-error">
                  <div class="main-error">
                    Please select a reporting period
                    </div>
                  <div class="main-errors">
                    Please select a duration
                    </div>
              </div>
		  <div class="clearfix sect-rom">
		    <div class = "col-lg-3 col-md-3 col-sm-3 col-xs-12 font-bold">
			    Reporting Period
		    </div>
			
			<div class = "col-lg-3 col-md-3 col-sm-3 col-xs-12 rpt-top">
                
			<div class="calend clearfix">
                <asp:RadioButton runat="server" ID="calenderyear" name="radCal" class="chartIndex" onchange="hideerror()"/> Calender Year
				</div>
			<div class="calend clearfix">
			   <asp:RadioButton runat="server" ID="Federalyear" name="radCal" class="chartIndex1" onchange="hideerror()"/> Federal Fiscal Year
				</div>
		    </div>
			
			
		    <div class = "col-lg-6 col-md-6 col-sm-6 col-xs-12">

            <div class="chec-main">
			<div class="calend clearfix">
			    <ul class="calend-check clearfix firs">
				  <li><asp:CheckBox  runat="server" ID="CYQ1" Text="CY Q1" class="chartcheck" /></li>
				  <li><asp:CheckBox runat="server" ID="CYQ2" Text="CY Q2" class="chartcheck" /></li>
				  <li><asp:CheckBox runat="server" ID="CYQ3" Text="CY Q3" class="chartcheck" /></li>
				  <li><asp:CheckBox runat="server" ID="CYQ4" Text="CY Q4" class="chartcheck" /></li>
                    
				</ul>
				</div>
                </div>


		   <div class="chec-main">
			<div class="calend clearfix">
			    <ul class="calend-checks clearfix secon">
				  <li><asp:CheckBox runat="server" ID="FFQ1" Text="CY Q1" /></li>
				  <li><asp:CheckBox runat="server" ID="FFQ2" Text="CY Q2" /></li>
				  <li><asp:CheckBox runat="server" ID="FFQ3" Text="CY Q3" /></li>
				  <li><asp:CheckBox runat="server" ID="FFQ4" Text="CY Q4" /></li>
				</ul>
				</div>
                </div>

		    </div>
			</div>
			
			<div class="clearfix sect-rom">
			<div class = "col-lg-3 col-md-3 col-sm-3 col-xs-12 font-bold">
			  <asp:RadioButton runat="server" ID="customdate" name="radCal" class="chartIndex2" onchange="hideerror()"/> Custom Date
		    </div>
			
			<div class = "col-lg-5 col-md-5 col-sm-5 col-xs-12 froms-lft">
			    <b>From </b>      

  <asp:TextBox runat="server" ID="fromdate"  CssClass="popup_textbox" placeholder="MM/DD/YYYY" ></asp:TextBox>

                    <img src="../../Images/calendar.jpg" id="calendar_img" class="txtbox_spanimg cald" alt="calendar" style="margin-top: -3px;"/>
                                         
            </div>
			
			
			<div class = "col-lg-4 col-md-4 col-sm-4 col-xs-12 froms-rgt">
			    <b>To</b>        

                 <asp:TextBox runat="server" ID="todate"  CssClass="popup_textbox"  placeholder="MM/DD/YYYY"></asp:TextBox>


                    
                    <img src="../../Images/calendar.jpg" id="calendar_imgs" class="txtbox_spanimg cald" alt="calendar" style="margin-top: -3px;"/>
                               
		    </div>
			</div>
			
			<div class="clearfix sect-rom">
			<div class = "col-lg-3 col-md-3 col-sm-3 col-xs-12 font-bold">
			    Provider  
		    </div>
			
			<div class = "col-lg-6 col-md-6 col-sm-6 col-xs-12">
             <asp:DropDownList runat="server" ID="dropprovider">
               
             </asp:DropDownList>
			</div>	
			
			</div>
			
			
			<div class="clearfix sect-rom manis">
			<div class = "col-lg-3 col-md-3 col-sm-3 col-xs-12 font-bold">
			    Measures
		    </div>
			
			<div class = "col-lg-3 col-md-3 col-sm-3 col-xs-12">
		   <span><asp:CheckBox runat="server" ID="measure1" />	</span> <label>Patient Electronic Access Measure 1 Stage 1</label>
		    </div>
			
			<div class = "col-lg-3 col-md-3 col-sm-3 col-xs-12">
			 <span>    <asp:CheckBox runat="server" ID="measure2" /> </span> <label> Patient Electronic Access Measure 1 Stage 2 </label>
		    </div>
			
			<div class = "col-lg-3 col-md-3 col-sm-3 col-xs-12">
			 <span>  <asp:CheckBox runat="server" ID="measure3" /> </span> <label> Patient Electronic Access Measure 2 Stage 2 </label>
		    </div>
			
			<div class = "col-lg-3 col-md-3 col-sm-3 col-xs-12 manis-btn">
			   <asp:Button runat="server" ID="Submit"  class="btn_standard new-submit" Text="Submit" OnClick="Submit_Click" OnClientClick="javascript:return CheckValidation();"/>
		    </div>

			</div>
			
			
			<div class="clearfix sect-rom">

          <asp:GridView runat="server" ID="gdmureport" ClientIDMode="Static" AutoGenerateColumns="false" class="mes-no" ShowHeaderWhenEmpty="true"
            GridLines="Both" OnRowDataBound="gdmureport_RowDataBound" EmptyDataText="No Records Found" Width="100%">
            <Columns>
        <asp:BoundField DataField="MeasureNumber" HeaderText="Measure Number" />
        <asp:BoundField DataField="Denominator_Count" HeaderText="Total Population" />
        <asp:BoundField DataField="Numerator_Count" HeaderText="Eligible Population"/>
        <asp:BoundField DataField="Percentage" HeaderText="Performance %" />
        <asp:BoundField DataField="Meet" HeaderText="Meet Criteria" />

                                     <asp:TemplateField HeaderText="Details">
                            <ItemTemplate>

                           <asp:Label ID="MURDView" runat="server" class="btn_standard btn-stan"  Text="View" onclick=<%# string.Format("MURDetailsPopup('{0}');", Eval("MeasureNumber")) %>></asp:Label>
                                 </ItemTemplate>
                        </asp:TemplateField>
            </Columns>
        </asp:GridView>
			
			</div>
			</div>
			</div>
            <div align="center">
                <asp:Button runat="server" ID="XMLEXPORT" OnClick="XMLEXPORT_Click" Text="CREATE XML" class="btn_standard" />
                <asp:Button runat="server" OnClick="ExportExcel" Text="EXPORT" class="btn_standard" />
                <asp:Button ID="btnPrint" runat="server" OnClientClick="PrintGridData();" Text="PRINT" class="btn_standard" />	
                
          <%--   <input type="submit" value="CREATE XML" class="btn_standard" >
             <input type="submit" value="EXPORT" class="btn_standard" >
             <input type="submit" value="SUBMIT" class="btn_standard" >--%>

            </div>
			
		  </div>
            
                 
            </div>
            
<div  style="display: none;">
    <div id="divshowadduser" style="z-index: 10000; height: 485px!important;">
        <div class="edit_allergiesdiv" style="float: left; margin: 3px; width: 99%;">
            <div >
                    <div class="popup_header Content">View Details
            </div>
            <div style="float: right; margin-right: 10px;">
                <img src="../../Images/popup_close.png" id="popupclose" style="cursor: pointer; margin-top: 1px;
                    margin-right: 0px;" />
            </div>
                <iframe id="patientsearch_popup" src="" style="overflow-y: scroll; overflow-x:hidden;
                     width: 100%;     height: 267px;  border: none; margin-top: 0px;">
                </iframe>
            </div>
            
        </div>
    </div>
    </div>

<%--for printing MU report--%>
<div id="printMUReport" style="display: none;" runat="server">
    <div id="reportingprd" runat="server">
    <label style="font:bold">Reporting Period</label>
       <p> <asp:Label ID="reportingperiod" runat="server" Text="" style="font:bold"></asp:Label>
           <span> <asp:Label ID="reportingperiodValue" runat="server" Text=""></asp:Label></span>
       </p>
        </div>
    <div id="customdateprint" style="display: none;" runat="server">
        <p><label style="font:bold">Custom Date : </label>
        <span><asp:Label ID="customfromdate" runat="server" Text=""></asp:Label> - 
           <span> <asp:Label ID="customtodate" runat="server" Text=""></asp:Label></span></span>
       </p>
    </div>
    <div>
       <p> <label  style="font:bold">Provider : </label>
       <asp:Label ID="provider" runat="server" Text=""></asp:Label>
       </p>
    </div>
    <div>
        <p><label style="font:bold">Measures : </label>
       <asp:Label ID="lblmeasures" runat="server" Text=""></asp:Label>
       </p>
    </div>

     <asp:GridView runat="server" ID="grdprintreport" ClientIDMode="Static" AutoGenerateColumns="false" class="mes-no" ShowHeaderWhenEmpty="true"
            GridLines="Both" OnRowDataBound="gdmureport_RowDataBound" EmptyDataText="No Records Found" Width="100%">
            <Columns>
        <asp:BoundField DataField="MeasureNumber" HeaderText="Measure Number" />
        <asp:BoundField DataField="Denominator_Count" HeaderText="Total Population" />
        <asp:BoundField DataField="Numerator_Count" HeaderText="Eligible Population"/>
        <asp:BoundField DataField="Percentage" HeaderText="Performance %" />
        <asp:BoundField DataField="Meet" HeaderText="Meet Criteria" />

                                   <%--  <asp:TemplateField HeaderText="Details">
                            <ItemTemplate>

                           <asp:Label ID="MURDView" runat="server" class="btn_standard btn-stan"  Text="View" onclick=<%# string.Format("MURDetailsPopup('{0}');", Eval("MeasureNumber")) %>></asp:Label>
                                 </ItemTemplate>
                        </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>
    </div>

        <script>

            $(document).ready(function () {

             

                $("#div_patientsearch").css("display", "block");
                $("#patientsearch_leftpart").css("display", "block");
                $("#imgsearchuser").css("display", "block");



            });
            function MURDetailsPopup(measureNumber) {
                var measure = 0;
                if (measureNumber == "Patient Electronic Access Measure1 stage1" || measureNumber == "Patient Electronic Access Measure1 stage2") {
                    measure = 1
                }
                else {
                    measure = 2
                }

                $("#patientsearch_popup").attr("src", "");
                $("#patientsearch_popup").attr("src", "../ASPX/AjaxMUReportDetails.aspx?measure=" + measure);
                showpopup()

            }

            function showpopup() {
                var popuphight = window.innerHeight - 200;
                var popupwidth = window.innerWidth - 490;
                $('.Content').text("View Details");
                $("#divshowadduser").dialog(
                    {

                        modal: true,
                        height: popuphight,
                        width: popupwidth,
                        resizable: false,
                      
                        create: function () {
                            $(".ui-dialog-titlebar").hide();
                            $(".ui-dialog-content").css("padding", "0px");
                        }
                    });
            }
     </script>   
<script type="text/javascript">

    $(document).ready(function () {


       
        $("#popupclose").click(function () {
            $("#divshowadduser").dialog('close');
        });




    });

    //$('input[type=radio]').prop('checked', false);
    //$('input[type=radio]:first').prop('checked', true);
    //$('.secon').hide(); //$('.firs').hide();

    $('input[type=radio]').click(function (event) {
        $('input[type=radio]').prop('checked', false);
        $(this).prop('checked', true);

        //event.preventDefault();
    });
    

    function CheckValidation() {
        if (document.getElementById('<%=calenderyear.ClientID %>').checked || document.getElementById('<%=Federalyear.ClientID %>').checked || document.getElementById('<%=customdate.ClientID %>').checked) {

            if (document.getElementById('<%=calenderyear.ClientID %>').checked)
                {
                    if ($('.calend-check input[type=checkbox]').is(':checked')) {
                        return true;
                    }
                    else {
                        $('.main-error').hide();
                        $('.main-errors').show();
                        return false;
                    }
            }
            else if(document.getElementById('<%=Federalyear.ClientID %>').checked)
            {
                if ($('. calend-checks input[type=checkbox]').is(':checked')) {
                    return true;
                }
                else {
                    $('.main-error').hide();
                    $('.main-errors').show();
                    return false;
                }
            }
            else {
                var fromdate=document.getElementById('<%=fromdate.ClientID %>').value;
                var todate=document.getElementById('<%=todate.ClientID %>').value;
                if(fromdate=="" || todate=="")
                {
                    $('.main-error').hide();
                    $('.main-errors').show();
                    return false;
                   
                }
                else {
                    return true;
                }
            }
        }
        else {
            $('.main-error').show();
            $('.main-errors').hide();
            return false;
        }
    }

    function hideerror() {
        if (document.getElementById('<%=calenderyear.ClientID %>').checked) {
            $('.secon input[type=checkbox]').removeAttr('checked');
            $('#cplSuperUser_MUReports_fromdate').val('');
            $('#cplSuperUser_MUReports_tomdate').val('');
        }
        else if (document.getElementById('<%=Federalyear.ClientID %>').checked) {
            $('#cplSuperUser_MUReports_fromdate').val('');
            $('#cplSuperUser_MUReports_tomdate').val('');
            $('.firs input[type=checkbox]').removeAttr('checked');
        }
        else {
            $('.secon input[type=checkbox]').removeAttr('checked');
            $('.firs input[type=checkbox]').removeAttr('checked');
        }
        $('.main-error').hide();
        $('.main-errors').hide();
    }

    //$('.new-submit').click(function (event) {
    //    if ($('.calend-check input[type=checkbox]').is(':checked')) {
    //        $('.main-error').hide();
    //    }
    //    else {
    //        $('.main-error').show();
    //        $('#lblerror').text = "Reporting Period is mandatory";
    //    }
    //    if($('.manis input[type=checkbox]').is(':checked')){
    //        $('.main-errors').hide();
    //    }
    //    else {
    //        $('.main-errors').show();
    //    }
    //});

    $('.firs input[type=checkbox]').click(function() {
        $('.secon input[type=checkbox]').removeAttr('checked');
    });
    $('.secon input[type=checkbox]').click(function () {
        $('.firs input[type=checkbox]').removeAttr('checked');
    });


    </script>