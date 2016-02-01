<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCClinicalDocuments.ascx.cs" Inherits="Hick.PatientLookUp.UserControls.UCClinicalDocuments" %>
<style>    
div.patient_search_right_frame{margin:5px 0;}
.patsearch_heading.patient{width:100%; padding-top:4px;}
.patsearch_heading.patient .btn_standard{margin:0px 50px 0 0!important; padding:5px 10px; float:right;}
.patsearch_heading.patient #popup_close{margin-right:-164px; margin-top: 0!important;}
#popupclose { position: absolute;  top: 5px;  right: 5px; width:30px; height:30px;}
.pop-img {
    cursor: pointer;
    margin-right: 0px;
    margin-top: -35px;
    width: 32px!important;
    position: inherit!important;
    left: 0;
    float: right;
}
</style>
<div class="patsearch_heading patient">
        Clinical Documents
         <input type="button" value="Add New" name="add_clicnicaldoc" class="btn_standard" id="add_clicnicaldoc"
        style=" margin-left: 508px;margin-top:-28px;" />
        <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popup_close" class="pull-right" style="cursor: pointer;margin-top:-42px;"
                alt="close" />
    </div>
    <div class="patsearch_border med">
    <div class="conditions_head" style="margin: 3px;">

        <asp:GridView runat="server" ID="gdclicnicaldoc" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" HeaderStyle-BorderWidth="0" RowStyle-BorderWidth="1px" RowStyle-BorderColor="#D3D3D3">
            <Columns>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div style="width: 130px; font-weight: bold;text-align:left;" class="ele_center">
                           Document Name</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="width: 110px;text-align:left;" class="ele_center">
                            <asp:Label ID="lblclinicaldoc" CssClass="lblclinicaldoc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FileName")%>'></asp:Label></div>
                        <span class="lblpatientid" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem,"PatientID")%>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
               
                    <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div style="width: 130px; font-weight: bold;text-align:left;" class="ele_center">
                           Date Uploaded</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="width: 110px;text-align:left;" class="ele_center">
                            <asp:Label ID="lblImmunization" CssClass="lblImmunization" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"UploadedDate")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list clincal" ItemStyle-CssClass="table_data_list clincal">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="text-align:center;" class="">                      
                            <input type="button" value="Download" name="upload" class="btn_standard js-download" fext='<%#DataBinder.Eval(Container.DataItem,"UploadedFileName")%>'/>
      <img style="cursor: pointer;" src="../../Images/button_edit.jpg" alt="Edit" class="edit_clinicaldoc" mid='<%#DataBinder.Eval(Container.DataItem,"Id")%>' />      
        <%--<a href="../../UserFiles/ClinicalForms/<%#DataBinder.Eval(Container.DataItem,"UploadedFileName")%>" class="btn_standard"></a>                       
 --%>                            
                             <img style="cursor: pointer;" src="../../Images/button_close.jpg" alt="Delete" class="delete_conditions" 
                                 onclick="return deleteimmNote(<%#DataBinder.Eval(Container.DataItem,"Id")%>);" />
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
<div style="display: none;">
    <div id="divshowclicnicaldoc" style="z-index: 10000;">
        <div class="edit_clicnicaldocdiv">
            <div style="float: left;">
                <iframe id="frameclicnicaldocdiv" src="" style="overflow: auto; position: fixed; width: 54%;
                    height: 346px; border: none; margin-top: 40px;"></iframe>
            </div>
            <div class="popup_header Content">
            </div>
            <div class="pop-new">
                <img src="../../Images/popup_close.png" id="popupclose" class="pop-img" />
            </div>
        </div>
    </div>
</div>

<input type="hidden" id="deleteimmNoteId" value="0" />
        <div style="display: none;">
    <div id="divdelimmnote" style="z-index: 10000;">
        <div class="edit_sessionnotediv">
            
             <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' onclick="closeimmPopup()" class="pull-right "
                alt="close" />
              <p>Are you sure want to delete this task</p>
                <div class="center-txt">                     
                       <input type="button" id="" name="" value="Yes" class="btn_standard" onclick="ImmNoteDelete()"/> 
                        <input type="button" id="" value="No" class="btn_standard" onclick="closeimmPopup()"/>
                    </div>
            
            
           
                  
        </div>
    </div>
</div> 

<script>

    $("#div_patientsearch").css("display", "block");
    $("#patientsearch_leftpart").css("display", "block");
    $("#imgsearchuser").css("display", "block");


    (function ($) {
        $('.js-download').click(function () {
            var fext = $(this).attr('fext');
            var url = '../../UserFiles/ClinicalForms/' + fext;
   
            var nVer = navigator.appVersion;
            var nAgt = navigator.userAgent;
            var browserName = navigator.appName;
            var fullVersion = '' + parseFloat(navigator.appVersion);
            var majorVersion = parseInt(navigator.appVersion, 10);
            var nameOffset, verOffset, ix;

            // In Opera 15+, the true version is after "OPR/" 
            if ((verOffset = nAgt.indexOf("OPR/")) != -1) {
                browserName = "Opera";
                fullVersion = nAgt.substring(verOffset + 4);
            }
                // In older Opera, the true version is after "Opera" or after "Version"
            else if ((verOffset = nAgt.indexOf("Opera")) != -1) {
                browserName = "Opera";
                fullVersion = nAgt.substring(verOffset + 6);
                if ((verOffset = nAgt.indexOf("Version")) != -1)
                    fullVersion = nAgt.substring(verOffset + 8);
            }
                // In MSIE, the true version is after "MSIE" in userAgent
            else if ((verOffset = nAgt.indexOf("MSIE")) != -1) {
                browserName = "Microsoft Internet Explorer";
                fullVersion = nAgt.substring(verOffset + 5);
            }
                // In Chrome, the true version is after "Chrome" 
            else if ((verOffset = nAgt.indexOf("Chrome")) != -1) {
                browserName = "Chrome";
                fullVersion = nAgt.substring(verOffset + 7);
            }
                // In Safari, the true version is after "Safari" or after "Version" 
            else if ((verOffset = nAgt.indexOf("Safari")) != -1) {
                browserName = "Safari";
                fullVersion = nAgt.substring(verOffset + 7);
                if ((verOffset = nAgt.indexOf("Version")) != -1)
                    fullVersion = nAgt.substring(verOffset + 8);
            }
                // In Firefox, the true version is after "Firefox" 
            else if ((verOffset = nAgt.indexOf("Firefox")) != -1) {
                browserName = "Firefox";
                fullVersion = nAgt.substring(verOffset + 8);
            }
                // In most other browsers, "name/version" is at the end of userAgent 
            else if ((nameOffset = nAgt.lastIndexOf(' ') + 1) <
                      (verOffset = nAgt.lastIndexOf('/'))) {
                browserName = nAgt.substring(nameOffset, verOffset);
                fullVersion = nAgt.substring(verOffset + 1);
                if (browserName.toLowerCase() == browserName.toUpperCase()) {
                    browserName = navigator.appName;
                }
            }
            // trim the fullVersion string at semicolon/space if present
            if ((ix = fullVersion.indexOf(";")) != -1)
                fullVersion = fullVersion.substring(0, ix);
            if ((ix = fullVersion.indexOf(" ")) != -1)
                fullVersion = fullVersion.substring(0, ix);

            majorVersion = parseInt('' + fullVersion, 10);
            if (isNaN(majorVersion)) {
                fullVersion = '' + parseFloat(navigator.appVersion);
                majorVersion = parseInt(navigator.appVersion, 10);
            }
            SaveToDisk(url, fext, browserName)



        });
    })(jQuery)

    function SaveToDisk(fileURL, fileName, browsername) {
        // for non-IE
        if (browsername == "Chrome") {
            if (!window.ActiveXObject) {
                var save = document.createElement('a');
                save.href = fileURL;
                save.target = '_blank';
                save.download = fileName || 'unknown';

                var event = document.createEvent('Event');
                event.initEvent('click', true, true);
                save.dispatchEvent(event);
                (window.URL || window.webkitURL).revokeObjectURL(save.href);
            }

                // for IE
            else if (!!window.ActiveXObject && document.execCommand) {
                var _window = window.open(fileURL, '_blank');
                _window.document.close();
                _window.document.execCommand('SaveAs', true, fileName || fileURL)
                _window.close();
            }
        }
        else {
            window.open(fileURL);
        }
    }

    $(document).ready(function () {

        $("#popupclose").click(function () {
            $("#divshowclicnicaldoc").dialog('close');
            location.reload();
        });

       

        $(".edit_clinicaldoc").click(function () {
            $('.Content').text("Edit");

            var uid = $(this).attr("mid");
            $("#frameclicnicaldocdiv").attr("src", "");
            $("#frameclicnicaldocdiv").attr("src", "AddEditClinicalDocuments.aspx?Action=Edit&id=" + uid);
            showpopup();
        });
        $("#add_clicnicaldoc").click(function () {
            $('.Content').text("Add New");

            $("#frameclicnicaldocdiv").attr("src", "");
            $("#frameclicnicaldocdiv").attr("src", "AddEditClinicalDocuments.aspx?Action=New");
            showpopup();
        });
    });

    function showpopup() {
        var popuphight = window.innerHeight - 150;
        var popupwidth = window.innerWidth - 490;

        $("#divshowclicnicaldoc").dialog(
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

    function ClosePopup() {

        $("#divshowclicnicaldoc").dialog('close');

    }

    $("#popup_close").click(function () {
        parent.popup_close();
     
    });

    function ImmNoteDelete() {
        var uid = $('#deleteimmNoteId').val();
        $('.Content').text("Delete");
        //var uid = $(this).attr("cid");

        var data = "{ Id: '" + uid + "'}";

        $.ajax({
            type: "POST",
            url: "ClinicalDocuments.aspx/DeleteClinicalDocuments",
            data: data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            mode: "queue",
            success: function (msg) {
                location.reload();
            },
            error: function (xmlhttprequest, textstatus, errorThrown) {
                alert(xmlhttprequest.responseText);
            }
        });
    }
    function deleteimmNote(id) {

        $('#deleteimmNoteId').val(id);

        var popuphight = window.innerHeight - 150;
        var popupwidth = window.innerWidth - 490;

        $("#divdelimmnote").dialog(
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

    function closeimmPopup() {
        $("#divdelimmnote").dialog('close');
    }
</script>


