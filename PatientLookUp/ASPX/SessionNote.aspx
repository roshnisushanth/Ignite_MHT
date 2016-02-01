<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SessionNote.aspx.cs" Inherits="Hick.PatientLookUp.ASPX.SessionNote" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/style.css" rel="stylesheet" />
    <link href="~/Content/session.css" rel="stylesheet" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="https://code.jquery.com/ui/1.11.1/jquery-ui.js"></script>
	

</head>
<body>
    <form id="form2" runat="server">
    <div>
    <div style="width:100%;">
                    <div class="search_img_label">
                        <img src="../../Images/session-note.png" alt="search_img" />
                    </div>
                    <div class="search_label" id="div_patientsearch" style="display: block;">
                        Session Notes
						<img src="../../Images/popup_close.png" id="popup_close" class="pull-right" alt="close"/>
                    </div>
                </div>	
				
	<div class="session-table">
	
	    <table cellpadding="0" cellspacing="0" width="98%" class="sess-tab" >
		   <tr class="top-ses">
		      <th width="15%">Date</th>
		      <th class="categ" width="16%">Category</th>
		      <th width="19%">Start Time</th>
		      <th width="19%">End Time</th>
		      <th width="19%">Total Time</th>
		      <th width="8%">&nbsp;</th>
		   </tr>
		   
		    <%foreach(var item in this.sessionNote) {%>
		   <tr>
		   <td colspan="6">
		     <table width="100%" class="sess-tab1">
			 <tr>
		     <td width="15%"><%:item.Date%></td>
		     <td class="categ" style="width:16%"><%:item.Category%></td>
		     <td width="19%"><%:item.StartTime%></td>
		     <td width="19%"><%:item.EndTime%></td>
		     <td width="19%"><%:item.TotalTime%></td>
		     <td class="editts"><img src="../../Images/timer_edittask.png" style="cursor:pointer" onclick="ShowSessionNote(<%:item.Id%>)"/>
			    <img src="../../Images/timer_deletetask.png" style="cursor:pointer" onclick="return deleteSessionNote(<%:item.Id%>);"/>
				</td>	
		     </tr>
			 </table>
			 <div class="table-div">
                 <div  style="text-align:left" class="div-rft"><b>Notes : </b> <%:item.Note%></div>
			 </div>
			 </td>
			 
			 </tr>
              <% }%>
		</table>
		<p hidden="hidden" class="ses-pls">Please Select the session note(s) to print or select 'Print' to 'Print all'</p>
	</div>	

      <div style="text-align:left;">                     
                       <input type="submit" hidden="hidden" name="" value="Print" class="btn_standard"/> 
                        <input type="button" hidden="hidden" value="Download" class="btn_standard" />
                    </div>
	
				
    </div>
        <input type="hidden" id="currentSessionNoteId" value="0" />
<div style="display: none;">
    <div id="diveditsessionnote" style="z-index: 10000;">
        <div class="edit_sessionnotediv">
            <div class="popup_header Content"> 
            </div>
             <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' onclick="closEditPopup()" class="pull-right close-btn"
                alt="close" />
            <div >
                 <textarea placeholder="Enter text" id="txteditsessionnote" ></textarea>
            </div>
            
            <div class="divside">

            </div>
                  <div class="center-txt">                     
                       <input type="button" id="btnSaveSession" name="" value="Save" class="btn_standard"  onclick="SessionNoteEdit()"/> 
                        <input type="button" id="btnCancelSession" value="Cancel" class="btn_standard" onclick="closEditPopup()"/>
                    </div>
        </div>
    </div>
</div>  

<input type="hidden" id="deleteSessionNoteId" value="0" />
        <div style="display: none;">
    <div id="divdelsessionnote" style="z-index: 10000;">
        <div class="edit_sessionnotediv">
            
             <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' onclick="closedeletePopup()" class="pull-right "
                alt="close" />
              <p>Are you sure want to delete this task</p>
                <div class="center-txt">                     
                       <input type="button" id="" name="" value="Yes" class="btn_standard" onclick="SessionNoteDelete()"/> 
                        <input type="button" id="" value="No" class="btn_standard" onclick="closedeletePopup()"/>
                    </div>
            
            
           
                  
        </div>
    </div>
</div> 
    </form>
	
	<script>
	$("#popup_close").click(function () {
        parent.popup_close();
    });
	$(document).ready(function(){
	    $('.sess-tab1 tr').removeClass('ses-act');

	    $('.sess-tab tr').on('click', function () {

	        $(".sess-tab tr").each(function (index) {
	            $(this).removeClass('ses-act');
	        });

	        $(this).addClass('ses-act');
	    });
	});

	function ShowSessionNote(id)
	{
	    $('#currentSessionNoteId').val(id);
	    $.ajax({
	        url: "../ASPX/AjaxSessionNoteModification.aspx?action=SelectById&id=" + id+"&note=", success: function (result) {
	            $('#txteditsessionnote').val(result);
	            $('.Content').text("Edit");

	            var popuphight = window.innerHeight - 150;
	            var popupwidth = window.innerWidth - 490;

	            $("#diveditsessionnote").dialog(
                    {
                        modal: true,
                        height: popuphight,
                        width: popupwidth,
                        resizable: false,
                        //title: "Patient Search",
                        create: function () {
                            $(".ui-dialog-titlebar").hide();
                            $(".ui-dialog-content").css("padding", "0px");
                        }
                    });

	        }
	    });
	}
	function SessionNoteEdit()
	{
	    var sessionNote = $('textarea#txteditsessionnote').val();
	    var id = $('#currentSessionNoteId').val();
	    $.ajax({
	        url: "../ASPX/AjaxSessionNoteModification.aspx?action=Edit&id=" + id + "&note=" + sessionNote, success: function (result) {
	            //parent.window.location.href = parent.window.location.href;
	            location.reload(true);
	        }
	    });
	}
	function deleteSessionNote(id) {
	        
	    $('#deleteSessionNoteId').val(id);

	        var popuphight = window.innerHeight - 150;
	        var popupwidth = window.innerWidth - 490;

	        $("#divdelsessionnote").dialog(
                {
                    modal: true,
                    height: popuphight,
                    width: popupwidth,
                    resizable: false,
                    //title: "Patient Search",
                    create: function () {
                        $(".ui-dialog-titlebar").hide();
                        $(".ui-dialog-content").css("padding", "0px");
                    }
                });
	}
	function closEditPopup()
	{
	    $("#diveditsessionnote").dialog('close');
	}

	function closedeletePopup() {
	    $("#divdelsessionnote").dialog('close');
	}
	function SessionNoteDelete() {
	    var id = $('#deleteSessionNoteId').val();
	    $.ajax({
	       url: "../ASPX/AjaxSessionNoteModification.aspx?action=Delete&id=" + id + "&note=", success: function (result) {
	    //parent.window.location.href = parent.window.location.href;
	       location.reload(true);
	     }
	     });
	}
	</script>
</body>
</html>

