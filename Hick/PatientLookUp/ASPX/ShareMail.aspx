<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShareMail.aspx.cs" Inherits="Hick.PatientLookUp.ASPX.ShareMail" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/patientlookup.css" rel="stylesheet" />
    <link href="~/Content/style.css" rel="stylesheet" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>

    <script type="text/javascript" src="https://code.jquery.com/ui/1.11.1/jquery-ui.js"></script>
    <%--<script type="text/javascript" src="http://www.html.am/html-editors/ckeditor/ckeditor_4.4.1_full/ckeditor.js"></script>--%>
    <script type="text/javascript" src="//www.html.am/html-editors/ckeditor/ckeditor_4.4.1_full/ckeditor.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#cancel').click(function () {
                parent.window.location.href = parent.window.location.href;
            });

        });


        function mailvalidation() {
            var sEmail = $("#To").val();
            var filter = /^([\w-\.]+)@direct.((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
            if (filter.test(sEmail)) {
               
                return true;
            }
            else {
                alert("Email field will ONLY accept a Direct email ID Ex: exam@direct.exmail.com ");
                return false;
            }
        }

</script>        
    
 



 
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
        /*span#AttachmentResult {
    width: 100%;
    display: inline-block;
    font-size: 12px;
    color: green;
}*/
    </style>
</head>
<body>


    <form id="form1" runat="server"  style="font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; ">

        
        <div class="add-form">

                 <div class="form-group">
    <label for="inputEmail3" class="col-sm-2 control-label">To</label>
    <div class="col-sm-10">
     
       <asp:TextBox runat="server" ID="To" class="form-control"></asp:TextBox>
        <%--<span class="error">Error</span>--%>
    </div>
  </div>
                    <div class="form-group">
    <label for="inputPassword3" class="col-sm-2 control-label">Subject</label>
    <div class="col-sm-10">

               <asp:TextBox runat="server"  ID="subject" class="form-control"></asp:TextBox>
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
 <textarea class="ckeditor" id="editor1" name="editor1" runat="server" cols="35" rows="10">
</textarea>
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