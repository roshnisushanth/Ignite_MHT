<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCRecordaccess.ascx.cs" Inherits="Hick.PatientLookUp.UserControls.UCRecordaccess" %>
<style>
        /*.scroll-sec{overflow-x: hidden;  height: 480px; overflow-y:scroll; }*/
        .patsearch_heading{margin-bottom:10px;}
        .ui-dialog.ui-widget.ui-widget-content.ui-corner-all.ui-front.ui-draggable{top:10px!important; height: 500px!important; width:590px!important;}
        .ui-dialog.ui-widget.ui-widget-content.ui-corner-all.ui-front.ui-draggable.del{top:10px!important; height: 230px!important;}
        #divshowadduser{height:auto!important;}
              div#divdeleteuser {
    height: auto!important;
}
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
        .record .modal-header {
            padding: 8px 15px;
        }
        .close.btn_standard {
    background-color: #FFCB05;
    opacity: 1;
    text-shadow: 0 0px 0 #fff;
}
    .popup_conter span, .popup_conter div {
        width: 64px;
    }
        div#divshare{height:auto!important;}
        .authors{overflow-x: hidden;  height: 100px; overflow-y:scroll; }
        .authoring{overflow-x: hidden;  height: 250px; overflow-y:scroll; }
        .authors th, .authors td { padding: 4px 5px!important;}
    </style>
<div class="record-acces">
<div class="patsearch_heading">
   Record Access
    
    <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popup_close" class="pull-right"
        style="cursor: pointer; margin-top: -5px;" alt="close" />
</div>
<div class="scroll-sec">
    <div class="patsearch_contents newly">
        <div class="tables-section auto">
            <div class="author">Authorized Representative For</div>
            <div class="authors">

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
                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FirstName")+" "+DataBinder.Eval(Container.DataItem,"Lastname")%>'></asp:Label>
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
                        <a><img src='<%=Page.ResolveUrl("~/Images/eye.png") %>' onclick="checkonetimepassword(<%#DataBinder.Eval(Container.DataItem,"UserId")%>,<%#DataBinder.Eval(Container.DataItem,"ReferenceId")%>,'eye')" class="onetimepswd"  id="eye" style="cursor:pointer;"/></a>
                        <a><asp:LinkButton   CommandArgument='<%#DataBinder.Eval(Container.DataItem,"UserId")%>' OnClientClick=<%#"javascript:return BindReferenceId('" + Eval("ReferenceId") + "');" %> runat="server" OnCommand="download" ID="Lnk_Btn_Download" > <img src='<%=Page.ResolveUrl("~/Images/download-org.png") %>' class="onetimepswd"  id="download"  /></asp:LinkButton></a>
<%--                        <a ><img  src='<%=Page.ResolveUrl("~/Images/download-org.png") %>' class="onetimepswd" onclick="checkonetimepassword(<%#DataBinder.Eval(Container.DataItem,"UserId")%>,'download')" id="download" /></a>--%>
                        <a ><img  src='<%=Page.ResolveUrl("~/Images/share-view_recordaccess.png") %>' class="onetimepswd" onclick="checkonetimepassword(<%#DataBinder.Eval(Container.DataItem,"UserId")%>,<%#DataBinder.Eval(Container.DataItem,"ReferenceId")%>,'share')" id="share" style="cursor:pointer;"/></a></div>
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


    <div class="patsearch_contents newly">
        <div class="tables-section auto">
            <div class="author">Your Authorized Representative </div>
            <div class="authoring">
               <asp:GridView runat="server" ID="gdautherizeduser" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" >
            <Columns>
                             <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                             </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                              <input type="checkbox" name=name />
                    
                            <asp:HiddenField ID="hdnuserid"  runat="server" Value='<%#DataBinder.Eval(Container.DataItem,"AutherizedUserId")%>' />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                             Name</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FirstName")+" "+DataBinder.Eval(Container.DataItem,"Lastname")%>'></asp:Label>
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
                            <asp:Label runat="server" ID="lblemail" Text='<%#DataBinder.Eval(Container.DataItem,"Email")%>'></asp:Label>
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
                                       <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                             Access Level</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AccessLevel")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                                       <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                            Access History</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AccessHistory")%>'></asp:Label>
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

        <div class="pull-right">
          <%--  <asp:Button runat="server" Text="down" ID="sssss" OnClick="Unnamed_Click" />--%>

     
                     <input type="button" value="Add New" name="add_allergies" class="btn_standard add-usr" id="add-usr"/>

                     <input type="button" value="Edit" name="add_allergies" class="btn_standard edit-usr" id="EditUser"/>

          <input type="button" id="delete" value="Delete User" class="btn_standard delete-usr"/>
        </div>

    </div>

</div>
    </div>

<div style="display: none;">
    <div id="divshowadduser" style="z-index: 10000; height: 451px!important;">
        <div class="edit_allergiesdiv" style="float: left; margin: 3px; width: 99%;">
            <div >
                    <div class="popup_header Content">Add New
            </div>
            <div style="float: right; margin-right: 10px;">
                <img src="../../Images/popup_close.png" id="popupclose" style="cursor: pointer; margin-top: 1px;
                    margin-right: 0px;" />
            </div>
                <iframe id="patientsearch_popup" src="../ASPX/AddEditAuthorizedUser.aspx?Action=New" style="overflow: auto;
                    position: fixed; width: 55%; height: 451px; border: none; margin-top: 0px;">
                </iframe>
            </div>
            
        </div>
    </div>

        <div id="divdeleteuser" style="z-index: 10000; height: 230px!important;">
        <div class="edit_allergiesdiv" style="float: left; margin: 3px; width: 99%;">
            <div >
                    <div class="popup_header Content" >Add New
            </div>
            <div class="cls-popup">
                <img src="../../Images/popup_close.png" id="popupclosedel" />
            </div>
                <iframe id="delete_popup" src="" style="overflow: auto;
                    position: fixed; width: 55%; height: 230px; border: none; margin-top: 0px;">
                </iframe>
            </div>
            
        </div>
    </div>


    <div id="divshare" style="z-index: 10000; height: 400px!important; width: 585px!important;">
        <div class="edit_allergiesdiv" style="float: left; margin: 3px; width: 99%;">
            <div >
                    <div class="popup_header Content" >Share
            </div>
            <div class="cls-popup">
                <img src="../../Images/popup_close.png" id="popupcloseshare" />
            </div>
                <iframe id="share_popup" src="" style="overflow: auto;
                    position: fixed; width: 56%; height: 400px; border: none; margin-top: 0px;">
                </iframe>
            </div>
            
        </div>
    </div>

    <asp:HiddenField ID="hdncurrentuser" runat="server" />
    <asp:HiddenField ID="hdnauthforuser" runat="server" />
    <asp:HiddenField  ID="hdnReferenceId" runat="server" />
</div>



<div class="modal fade bs-example-modal-sm record records" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel">
  <div class="modal-dialog modal-sm">
    <div class="modal-content">
     <div class="modal-header"> <button type="button" class="close close-rec" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button> <h4 class="modal-title" id="mySmallModalLabel">One Time Password</h4> </div>

        <p class="error" id="error" style="display:none;">Your Password is incorrect</p>


        <div class="popup_conter txtInctive" style="
                    align-items: center;">
                    <div class="form-lft">
                        <req>*</req>
                        Name</div>
                    <div class="form-rgt">
                        <input name="txt_Immunization" type="text" id="txt_pswd" class="popup_textbox">
                 
                    </div>
                </div>


        <div align="center" class="rec-butt">
        <button type="button" id="pswd_button" class="btn_standard">Ok</button>
        <button type="button" class="close btn_standard close-rec" data-dismiss="modal" aria-label="Close">Cancel</button>
        </div>
    </div>
  </div>
</div>

<script type="text/javascript">

    $(document).ready(function () {
        $("#div_patientsearch").css("display", "block");
        $("#patientsearch_leftpart").css("display", "block");
        $("#imgsearchuser").css("display", "block");



    });
    function showpopup() {
        var popuphight = window.innerHeight - 200;
        var popupwidth = window.innerWidth - 490;
        $('.Content').text("Add New");
        $("#divshowadduser").dialog(
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

    $("#EditUser").click(function () {

        if ($("input[name=name]:checked").length > 0)
        {
            var uid = "";          
            $("#gdautherizeduser input[name=name]:checked").each(function () {
                row = $(this).closest("tr");
                uid = $(row).find("input[ID=hdnuserid]").val();
            });
            $("#patientsearch_popup").attr("src", "");
            $("#patientsearch_popup").attr("src", "../ASPX/AddEditAuthorizedUser.aspx?Action=Edit&id=" + uid);
            showpopup();
            $('.Content').text("Edit User");
        }
        else {
            alert('please select any Authorized Representative')
        }
    });      
    $('input[name=name]').on('change', function () {
        $('input[name=name]').not(this).prop('checked', false);
    });
    $("#add-usr").click(function () {
        showpopup();
    });
    function showdeletepopup() {
        var popuphight = window.innerHeight - 200;
        var popupwidth = window.innerWidth - 490;

        $('.Content').text("Confirmation");
        $("#divdeleteuser").dialog(
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

    $("#delete").click(function () {


        if ($("input[name=name]:checked").length > 0) {
            var uid = "";
            var email = "";
            $("#gdautherizeduser input[name=name]:checked").each(function () {
                row = $(this).closest("tr");
                uid = $(row).find("input[ID=hdnuserid]").val();
                email = $(row).find("span[ID=lblemail]").text();
            });
            $('.Content').text("Edit");

            $("#delete_popup").attr("src", "");
            $("#delete_popup").attr("src", "../ASPX/DeleteAuthorizedUser.aspx?id=" + uid+"&email="+email);
            showdeletepopup();
        }
        else {
            alert('please select any Authorized Representative')
        }
        $('.ui-front.ui-draggable').toggleClass('del');

    });
    $("#popup_close").click(function () {
        parent.popup_close();
    });
    $("#popupclose").click(function () {
        $("#divshowadduser").dialog('close');
        $('.Content').text("Add New");
        $("#patientsearch_popup").attr("src", "");
        $("#patientsearch_popup").attr("src", "../ASPX/AddEditAuthorizedUser.aspx?Action=New");
    });

    $("#add-usr").click(function () {
        showpopup();
    });

    function checkonetimepassword(id,referenceid,type)
    {
        
        var authorizeduserid = id;
        $("#cplPatientLookUp_ucRecordaccess1_hdnauthforuser").val(id);
        $("#cplPatientLookUp_ucRecordaccess1_hdnReferenceId").val(referenceid);
        var userid = $("#cplPatientLookUp_ucRecordaccess1_hdncurrentuser").val();
      
            $.ajax({
                type: "POST",
                url: "../ASPX/Recordaccess.aspx/CheckOnetimePassword",
                data: '{"userid":"' + userid + '", "authorizeduserid":"' + authorizeduserid + '"}',
                contentType: "application/json; charset=utf-8",
                success: function (msg) {
                
                    if (msg.d == 'UnSucessful') {
                        ShowPasswordPopup()
                    }
                    else {
                        if (type == 'share') {

                            $("#share_popup").attr("src", "");
                            $("#share_popup").attr("src", "ShareMail.aspx?id=" + referenceid);
                            showsharepopup();
                            
                        }
                        if (type == 'eye') {
                            window.open('ViewPHPSummery.aspx?pageview=view&id=' + referenceid);                     

                        }                         
                    }
                },
                
            });
       
    }


    function BindReferenceId(referenceid)
    {
        $("#cplPatientLookUp_ucRecordaccess1_hdnReferenceId").val(referenceid);
    }
    function ShowPasswordPopup()
    {
        $('.records').addClass('time-pop');
    }

    $('.close-rec').click(function () {
        $('.records').removeClass('time-pop');
        $("#txt_pswd").val('');
        $("#error").hide();
    });
    $("#pswd_button").click(function () {
        var authorizeduserid = $("#cplPatientLookUp_ucRecordaccess1_hdnauthforuser").val();
        var userid = $("#cplPatientLookUp_ucRecordaccess1_hdncurrentuser").val();
        var pswd = $("#txt_pswd").val();
        $.ajax({
            type: "POST",
            url: "../ASPX/Recordaccess.aspx/insertOnetimePassword",
            data: '{"userid":"' + userid + '","pswd":"' + pswd + '", "authorizeduserid":"' + authorizeduserid + '"}',
            contentType: "application/json; charset=utf-8",
            success: function (msg) {
                if (msg.d == 'Sucessful')
                {
                    $('.records').removeClass('time-pop');
                    $("#txt_pswd").val('');
                    $("#error").hide();
                }
                else {
                    $("#error").show();
                    
                }
            },


        });
       
    });

$("#popupcloseshare").click(function () {
        $("#divshare").dialog('close');
    });

    $("#popupclosedel").click(function () {
        $("#divdeleteuser").dialog('close');
    });



</script>


