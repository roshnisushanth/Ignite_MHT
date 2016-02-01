<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuthorizedUserForm.aspx.cs" Inherits="Hick.AuthorizedUserForm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>


    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="https://code.jquery.com/ui/1.11.2/jquery-ui.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.1/js/bootstrap.min.js"></script>
    <script src="https://rawgit.com/wenzhixin/bootstrap-table/master/src/bootstrap-table.js"></script>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css"
        rel="stylesheet">
    <script type="text/javascript">
        $(function () {
            $("#signout").click('click', function () {
                $.ajax({
                    type: "POST",
                    url: "AuthorizedUserForm.aspx/Signout",
                    data: '{"userid":"' + $('#hdncurrentuser').val() + '"}',
                    contentType: "application/json; charset=utf-8",
                    success: function (val) {
                    },
                    complete: function () {
                        window.location = "Index.aspx";
                    },
                    error: function (err) {
                       
                    }
                });
            });
        });
     
    </script>
        <link href="~/Content/patientlookup.css" rel="stylesheet" />
    <link href="~/Content/style.css" rel="stylesheet" />
    <style type="text/css">
        .add-form {
    padding-top: 5px;
    overflow-y: hidden;
    overflow-x: hidden;
    height: 416px;
}
        .popup_conter span, .popup_conter div {
        width: 64px;
    }
       
    </style>
</head>
<body>
    <form id="form1" runat="server">
<style>
        .scroll-sec{overflow: hidden;  height: 416px; }
        .patsearch_heading{margin-bottom:10px;}
        .ui-dialog.ui-widget.ui-widget-content.ui-corner-all.ui-front.ui-draggable{top:10px!important; height: 477px!important;}
        .ui-dialog.ui-widget.ui-widget-content.ui-corner-all.ui-front.ui-draggable.del{top:10px!important; height: 230px!important;}
        #divshowadduser{height:auto!important;}
              div#divdeleteuser {
    height: auto!important;
}
*{}
.authorized .author {
    background: #f8f8f8;
}
.authorized table th {
    background-color: #F2E8E7!important;
}
.authorized table th, .authorized table td {
    border: 1px #ccc solid!important;
    padding: 5px!important;
}
.popup-text{
    font-size:12px; color:#000; text-align:center; padding: 10px;
}
.rec-butt{padding-bottom: 15px;}
 .btn-mod {
    background: transparent;
    border: 0;
}
        .modal-header {
            padding: 8px 15px;
        }
        .close.btn_standard {
    background-color: #FFCB05;
    opacity: 1;
    text-shadow: 0 0px 0 #fff;
}
        div#divshare{height:auto!important;}
        div#cke_editor1{    width: 97%;}
        div#cke_1_contents {
    height: 120px!important;
}
    </style>
<div class="authorized">
<div class="patsearch_heading">
   Record Access
    
 <a id="signout" style="float: right;margin-right: 10px;" href="AuthorizedUserForm.aspx/SignOut" class="btn btn-default btn-link listTabletOff" runat="server" role="button">Sign out</a>
</div>
<div class="scroll-sec">
    <div class="patsearch_contents newly">
        <div class="tables-section auto">
            <div class="author">Authorized Representative For</div>


<asp:HiddenField ID="hdncurrentuser" runat="server" />
    <asp:HiddenField ID="hdnauthforuser" runat="server" />
   <asp:GridView runat="server" ID="gdautherizeduserfor" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" >
            <Columns>
             
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                             Name</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FirstName")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                     <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                             Email ID</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Email")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                       <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                             Relationship</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Relationship")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                                <asp:TemplateField >
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="text-align: center;" class="">
                        <a><img src='<%=Page.ResolveUrl("~/Images/eye.png") %>' onclick="ShowPopup(<%#DataBinder.Eval(Container.DataItem,"ReferenceId")%>,'eye')" class="onetimepswd"  id="eye" style="cursor:pointer;"/></a>
                        <a><asp:LinkButton   CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ReferenceId")%>' runat="server" OnCommand="download" > <img src='<%=Page.ResolveUrl("~/Images/download-org.png") %>' class="onetimepswd"  id="download" /></asp:LinkButton></a>

                        <a ><img  src='<%=Page.ResolveUrl("~/Images/share-view_recordaccess.png") %>' class="onetimepswd" onclick="ShowPopup(<%#DataBinder.Eval(Container.DataItem,"ReferenceId")%>,'share')" id="share" style="cursor:pointer;"/></a></div>


</div>
</div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="Label1" Text="No records found" Font-Bold="true" runat="server" Style="white-space: nowrap;
                    padding: 10px;"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>




        </div>
    </div>




</div>

        </div>
        <div style="display: none;">

    <div id="divshare" style="z-index: 10000; height: 400px!important;">
        <div class="edit_allergiesdiv" style="float: left; margin: 3px; width: 99%;">
            <div >
                    <div class="popup_header Content" >Share
            </div>
            </div>
            <div  class="pop-new">
                <img src="Images/popup_close.png" id="popupclose"  class="pop-img" />
            </div>
                <iframe id="share_popup" src="" style="overflow: auto;
                    position: fixed; width: 56%; height: 400px; border: none; margin-top: 0px;">
                </iframe>
            </div>
            
        </div>
    </div>

</div>
        </form>
    <script type="text/javascript">
        function showsharepopup() {
            var popuphight = window.innerHeight - 200;
            var popupwidth = window.innerWidth - 490;

            $("#divshare").dialog(
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
        


        function ShowPopup(id, type) {
            var authorizeduserid = id;
            var userid= $("#hdncurrentuser").val();
                        if (type == 'share') {
                            $("#share_popup").attr("src", "");
                            $("#share_popup").attr("src", "PatientLookUp/ASPX/ShareMail.aspx?id=" + id);
                            showsharepopup();
                        }
                        if (type == 'eye') {

                            window.open('PatientLookUp/ASPX/ViewPHPSummery.aspx?pageview=view&id=' + id);

                        }
        }

        $("#popupclose").click(function () {
            $("#divshare").dialog('close');
            location.reload();
        });
		
		function ClosePopupshares() {
        $("#divshare").dialog('close');
    }
        </script>
</body>
</html>
