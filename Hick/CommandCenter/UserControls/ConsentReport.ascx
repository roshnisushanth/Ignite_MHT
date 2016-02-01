<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConsentReport.ascx.cs" Inherits="Hick.CommandCenter.UserControls.ConsentReport" %>
<style>
    .conditions_head.consents #gdconditions {
        border-left: 0;
    }
</style>
<script src="../../Scripts/jquery.ajaxfileupload.js"></script>

<div class="patsearch_heading">
    Consent Report
        <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popup_close" class="pull-right" style="cursor: pointer; margin-top: -5px;"
            alt="close" />
</div>

<div class="patsearch_border">
    <%--<div id="assess_head" class="conditions_div" style="display: inline-flex; display: -webkit-flex; width: 100%">
        <div style="width: 140px; font-weight: bold; margin: 15px 0;" class="text-center">Last Name</div>
        <div style="width: 140px; font-weight: bold; margin: 15px 0;" class="text-center">First Name</div>
        <div style="width: 140px; font-weight: bold; margin: 15px 0;" class="text-center">Date of Birth</div>
        <div style="width: 140px; font-weight: bold; margin: 15px 0;" class="text-center">Phone Number</div>
        <div style="width: 140px; font-weight: bold; margin: 15px 0;" class="text-center">Physician</div>
        <div style="width: 140px; font-weight: bold; margin: 15px 0;" class="text-center"></div>

    </div>--%>
    <div class="conditions_head consents" style="margin: 3px 3px 3px 0;">

        <asp:GridView runat="server" ID="gdconditions" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" HeaderStyle-BorderWidth="0">
            <Columns>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>Last Name</HeaderTemplate>
                    <ItemTemplate>
                        <div style="width: 140px; margin: 14px 0;" class="text-center ">
                            <asp:Label CssClass="lbldesc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Lastname")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>First Name</HeaderTemplate>
                    <ItemTemplate>
                        <div style="width: 140px; margin: 14px 0;" class="text-center">
                            <asp:Label CssClass="lbldesc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Firstname")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>Date of Birth</HeaderTemplate>
                    <ItemTemplate>
                        <div style="width: 140px; margin: 14px 0;" class="text-center">
                            <asp:Label CssClass="lbldesc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"DateOfBirth","{0:MM/DD/YYYY}")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>Phone Number</HeaderTemplate>
                    <ItemTemplate>
                        <div style="width: 140px; margin: 14px 0;" class="text-center">
                            <asp:Label CssClass="lbldesc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PhoneNumber")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>Physician</HeaderTemplate>
                    <ItemTemplate>
                        <div style="width: 140px; margin: 14px 0;" class="text-center">
                            <asp:Label CssClass="lbldesc" runat="server" Text='<%#PhyName%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <ItemTemplate>
                        <div style="width: 140px;" class="text-center">
                            <input type="button" value="Upload" name="upload" class="btn_standard js-upload" fext='<%#DataBinder.Eval(Container.DataItem,"FileExt")%>' pid='<%#DataBinder.Eval(Container.DataItem,"ID")%>' refpatid='<%#DataBinder.Eval(Container.DataItem,"ReferenceID")%>' style="display: <%#DataBinder.Eval(Container.DataItem,"showConsentButton")%>;" />
                            <input type="button" value="Download" name="upload" class="btn_standard js-download" fext='<%#DataBinder.Eval(Container.DataItem,"FileExt")%>' pid='<%#DataBinder.Eval(Container.DataItem,"ID")%>' refpatid='<%#DataBinder.Eval(Container.DataItem,"ReferenceID")%>' style="display: <%#DataBinder.Eval(Container.DataItem,"showDownloadButton")%>;" />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="Label1" Text="No records found" Font-Bold="true" runat="server"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>

    <input type="hidden" clientidmode="Static" id="uid" name="uid" runat="server" />
    <input type="hidden" clientidmode="Static" id="uname" name="uname" runat="server" />

    <div style="display: none;">
        <input type="file" id="fileUpload" name="fileUpload" />
        <button id="btnuploadfiles" onclick="uploadfiles()"></button>
    </div>
    <a id="adownload"></a>
</div>

<script type="text/javascript">
    $("#div_command").css("display", "block");
    $("#command_leftpart").css("display", "block");

    $("#commandcenter").css("display", "block");


    $("#popup_close").click(function () {
        parent.popup_close();
    });
    var cancelFileUpload = false;
    var gpatid;
    var refgpatid;

    function downloadURI(uri, name) {
        //alert(uri);
        var link = document.createElement("a");
        link.download = name;
        link.href = uri;
        link.click();
    }

    (function ($) {
        $('.js-download').click(function () {
            gpatid = '';
            gpatid = $(this).attr('pid');
            var fext = $(this).attr('fext');
            var url = '../../userfiles/consentforms/' + gpatid + '/' + gpatid + fext;
            console.log(url);

            downloadURI(url, gpatid + fext);
        });
    })(jQuery);


    $('.js-upload').click(function () {
        gpatid = '';
        refgpatid = '';
        var that = this,
            isImage = false,
            filctrl = null;
        gpatid = $(that).attr('pid');
        refgpatid = $(that).attr('refpatid');
        filctrl = $("#fileUpload");
        filctrl.replaceWith(filctrl = filctrl.clone(true));
        $(filctrl).unbind("change");
        $(filctrl).bind("change", function () {
            //$("#btnuploadfiles").trigger("click");
            //return false;
            sendFile(this.files[0]);
        });
        $(filctrl).trigger("click");
    });

    function hideButton(fName) {
        var ext;
        if (fName != '') {
            ext = fName.substring(fName.lastIndexOf('.'));
        }
        $('.js-download').each(function (idx, item) {
            var pid = $(item).attr('pid');
            if (pid == gpatid) {
                $(item).attr('fext', ext)
                $(item).show();
            }
        });

        $('.js-upload').each(function (idx, item) {
            var pid = $(item).attr('pid');
            if (pid == gpatid) {
                $(item).hide();
            }
        });
    }

    function sendFile(file) {
        var sendurl = "UploadConsentForm.ashx?userid=" + $('#uid').val() + "&username=" + $('#uname').val() + "&patid=" + gpatid + "&refpatid=" + refgpatid;
        var fd = new FormData();
        fd.append(file.name, file);
        $.ajax({
            type: 'post',
            url: sendurl,
            data: fd,
            success: function (jdata, status) {
                if (status == 'success') {
                    hideButton(file.name);
                    showSucess({ text: 'File uploaded successfully.' });
                } else {
                    showError('Sorry an error has occured. Please contact administrator');
                }
                /*
                var data = JSON.parse(jdata);
                if (typeof (data.error) != 'undefined') {
                    var ary = data.error.split('|');
                    if (ary[0] != 'success') {  
                        alert(data.error);
                    } else {
                        //showSucess({ text: 'File "' + ary[1] + '" uploaded successfully.' });
                        //hideButton();
                        //window.document.refre
                        //location.reload(true);
                        alert(data.error);
                    }
                }*/
            },
            error: function (data, status, e) {
                isFileUploading = false;
                showError('Sorry an error has occured. Please contact administrator');
            },
            cache: false,
            contentType: false,
            processData: false
        });

    }

    function uploadfiles() {
        var sendurl = "UploadConsentForm.ashx?userid=" + $('#uid').val() + "&username=" + $('#uname').val() + "&patid=" + gpatid;
        $.ajaxFileUpload({
            url: sendurl,
            secureuri: false,
            fileElementId: 'fileUpload',
            dataType: 'json',
            success: function (data, status) {
                if (typeof (data.error) != 'undefined') {
                    var ary = data.error.split('|');
                    if (ary[0] != 'success') {
                        alert(data.error);
                    } else {
                        //showSucess({ text: 'File "' + ary[1] + '" uploaded successfully.' });
                        //hideButton();
                        //window.document.refre
                        location.reload(true);
                    }
                }
            },
            error: function (data, status, e) {
                isFileUploading = false;
                showError('Sorry an error has occured. Please contact administrator');
            },
            complete: function () {
                isFileUploading = false;
                messageCount = 0;
            }
        });


    }

</script>
