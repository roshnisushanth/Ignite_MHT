<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShareMail.aspx.cs" Inherits="Hick.PatientLookUp.ASPX.ShareMail" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/patientlookup.css" rel="stylesheet" />
    <link href="~/Content/style.css" rel="stylesheet" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
  
    
 
    <style>
        .add-form {
    padding: 5px 15px;
    width: 97%;
    overflow-y: hidden;
    overflow-x: hidden;
    height: 416px;
}
        span#cke_34, span#cke_70, #cke_86,  #cke_83, #cke_80 {
    display: none!important;
}
        a.cke_button {
            padding: 1px 2px!important;
        }
        #cke_34 + span.cke_toolbar_break {
    clear: inherit!important;
}
        div#cke_editor1 {
    height: 235px;
}
        div#cke_1_contents{height:92px!important;}
        .cke_toolgroup {
            margin: 0 4px 2px 0!important;}
        .cke_combo_button {
    margin: 0 6px 2px 0!important;}
        .cke_combo_text {
    line-height: 20px!important;}
        .form-control {
    display: block;
    width: 93%;
    height: 20px;
    padding: 2px 12px;
    font-size: 14px;
    line-height: 1.42857143;
    color: #555;
    background-color: #fff;
    background-image: none;
    border: 1px solid #ccc;
    border-radius: 4px;
    -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
    box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
    -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
    -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
    transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
}div.popup_conter{margin:0;}
        .btn_standard {
            height: 23px;
            line-height: 14px;
            min-width: 85px;
        }
        .add-form .form-group {
            margin-bottom: 5px;
        }

         .nicEdit-main{height:100px; overflow-x:hidden!important; overflow-y:scroll!important; font-size:12px; margin: 0!important;
    width: 100%!important;  outline: none;    padding: 2px 0 2px 2px;}
        .nicEdit-main.nicEdit-selected{height:100px; overflow-x:hidden!important; overflow-y:scroll!important;font-size:12px;}
        .new-width {
    width: 335px;
}
        
      
    </style>
</head>
<body>


    <form id="form1" runat="server"  style="font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; ">

        
        <div class="add-form">

                 <div class="form-group">
    <label for="inputEmail3" class="col-sm-2 control-label">To</label>
    <div class="col-sm-10">
     
       <asp:TextBox runat="server" ID="To" class="form-control new-width"></asp:TextBox>
        <%--<span class="error">Error</span>--%>
    </div>
  </div>
                    <div class="form-group">
    <label for="inputPassword3" class="col-sm-2 control-label">Subject</label>
    <div class="col-sm-10">

               <asp:TextBox runat="server"  ID="subject" class="form-control new-width"></asp:TextBox>
    </div>
  </div>
                 <div class="form-group">
    <label for="inputPassword3" class="col-sm-2 control-label">Attachment</label>
    <div class="col-sm-10">
                <asp:HiddenField runat="server" ID="hdnuser" />

               <asp:Button runat="server" ID="PHP"  CssClass="btn_standard" Text="PHP" OnClick="PHP_Click" ></asp:Button>
               <asp:Button runat="server" ID="CCDA"  CssClass="btn_standard" Text="CCDA" OnClick="CCDA_Click"></asp:Button>
    
    </div>
  </div>


                 <div class="form-group">
    <label for="inputPassword3" class="col-sm-2 control-label">Message</label>
    <div class="col-sm-10">     


        <div id="sample">
	<div id="myArea1" style="width: 360px; height: 100px; border: 1px solid #000;" runat="server"></div>

    </div>
  </div>
                <div class="popup_conter">
               <asp:Button runat="server" ID="sentmail"  CssClass="btn_standard" Text="Send" OnClientClick="return mailvalidation()" OnClick="sentmail_Click"></asp:Button>
               <input type="button" id="cancel"  class="btn_standard" value="Cancel"  onclick="parent.ClosePopupshares();" />
           </div>  
  


        </div>
                
             


     
    </form>
</body>
</html>

<script src="../../Scripts/nicEdit.js" type="text/javascript"></script>
<script>
var area1, area2;

function toggleArea1() {
	if(!area1) {
		area1 = new nicEditor({fullPanel : true}).panelInstance('myArea1',{hasPanel : true});
	} else {
		area1.removeInstance('myArea1');
		area1 = null;
	}
}

function addArea2() {
	area2 = new nicEditor({fullPanel : true}).panelInstance('myArea2');
}
function removeArea2() {
	area2.removeInstance('myArea2');
}

bkLib.onDomLoaded(function() { toggleArea1(); });
</script>