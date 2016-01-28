<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditClinicalDocuments.aspx.cs" Inherits="Hick.CommandCenter.ASPX.AddEditClinicalDocuments" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/patientlookup.css" rel="stylesheet" />
    <link href="~/Content/style.css" rel="stylesheet" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="https://code.jquery.com/ui/1.11.1/jquery-ui.js"></script>

    <style>
        .form-rgt input{    width: 100%;     margin-left: 0!important;}
        .form-lft {
    padding-right: 17px;
}span.error {
    text-align: left;
}
        .popup_conter {
            margin-top: 6px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="edit_conditionsdiv" style="float: left; margin: 6px; width: 100%;">
            <div>
                <div class="popup_conter txtInctive" style="
                    align-items: center;">
                    <div class="form-lft">
                        <req>*</req>
                      Document Name</div>
                    <div class="form-rgt">
                        <asp:TextBox ID="txt_clinicaldoc" runat="server" CssClass="popup_textbox"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="hdnEvent" />
                        <asp:HiddenField runat="server" ID="hdnClinicalDocId" />
                        <asp:HiddenField runat="server" ID="hdnUploadedFileName" />
                         <asp:HiddenField runat="server" ID="hdnFileName" />
                        <asp:HiddenField runat="server" ID="hdnFileExt" />
                        <asp:HiddenField runat="server" ID="hdnFileSize" />
                        <asp:RequiredFieldValidator Display="Dynamic" ID="reqImmunization" CssClass="error"
                    ControlToValidate="txt_clinicaldoc" ErrorMessage="Document name is required" runat="server" ></asp:RequiredFieldValidator>
                    </div>
                </div>
                
            </div>
            <div id="Div1" class="popup_content" runat="server">
                <div class="popup_conter" style="align-items: center; ">
                    <div class="form-lft">
                        <req>*</req>
                      Upload</div>
                    <div class="form-rgt" style="position:relative;">
                        
              <asp:FileUpload runat="server" ID="fileupload" />

                        <asp:Label ID="StatusLabel" runat="server"></asp:Label>
           <asp:Label ID="StatusLabelsucess" runat="server"></asp:Label>

                   </div>
                    </div>
                </div>
                <div>
                   </div>
            </div>
        
            <div class="popup_conter">
                <asp:Button ID='btnUpload' CssClass='btn_standard' runat='server' Text='Upload' OnClick="btnUpload_Click" >
                </asp:Button>
                <input type="button" value="Cancel" class="btn_standard"
                    id="cancle_edit_immunzation"  />
            </div>
        </div>
 
    </form>
</body>
</html>
<script type="text/javascript">

    $("#cancle_edit_immunzation").click(function () {
        parent.window.location.href = parent.window.location.href;
    });

</script>