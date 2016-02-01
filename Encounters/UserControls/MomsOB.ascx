<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MomsOB.ascx.cs" Inherits="Hick.Encounters.UserControls.MomsOB" %>

<%

    var id = momOB.Id;
    var name = momOB.Name;
    var address1 = momOB.Address1;
    var address2 = momOB.Address2;
    var phone = momOB.Phone;
    var city = momOB.City;
    var state = momOB.State;
    var statero = momOB.State.ToLower() == "--select--" ? "" : ", " + momOB.State;
    var zip = momOB.ZipCode;
%>
<div class="patsearch_heading">
    Mom's OB
     <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popupclose" class="pull-right" style="cursor: pointer; margin-top: -5px;"
         alt="close" />
</div>

<div class="patsearch_border">

    <div class="" style="margin: 3px;">
        <div class="mom_ob_body" style="margin-top: 70px; display: none;">
            <div style="width: 180px; margin: 14px;">
                <span style="margin-left: 20px; font-weight: bold;">Physician Name</span><br />
                <p style="margin-left: 40px;"><%=name %></p>
            </div>

            <div style="width: 180px; margin: 14px;">
                <span style="margin-left: 20px; font-weight: bold;">Phone Number</span><br />
                <p style="margin-left: 40px;"><%=phone %></p>
            </div>

            <div style="width: 220px; margin: 14px;">
                <span style="margin-left: 20px; font-weight: bold;">Address</span><br />
                <p style="margin-left: 40px;"><%=address1 %> <%=address2 %> <%=city %> <%=statero %> <%=zip %></p>
            </div>
        </div>


        <div class="mom_ob_edit">
            <div style="margin: 3px;">
                <div class="mom_ob_edit" style="margin-top: 40px;">
                    <div style="margin-top: 40px;">
                        <div style="">
                            <span style="margin-left: 40px; font-weight: bold;">Physician Name</span><br />
                        </div>
                        <asp:TextBox ID="phy_name" runat="server" CssClass="popup_textbox" ClientIDMode="Static" Style="width: 300px; margin-left: 60px; margin-top: 10px;"></asp:TextBox><br />
                    </div>
                </div>
                <div style="margin-top: 20px;">
                    <div style="">
                        <span style="margin-left: 40px; font-weight: bold;">Phone Number</span><br />
                    </div>
                    <asp:TextBox ID="ph_no" runat="server" CssClass="popup_textbox" ClientIDMode="Static" Style="width: 300px; margin-left: 60px; margin-top: 10px;"></asp:TextBox><br />
                </div>

                <div style="margin-top: 20px;">
                    <div style="">
                        <span style="margin-left: 40px; font-weight: bold;">Address</span><br />
                    </div>

                </div>

                <div style="display: -webkit-box; display: inline-flex;">
                    <div style="margin-top: 10px;">

                        <asp:TextBox ID="address1" runat="server" CssClass="popup_textbox" ClientIDMode="Static" Style="width: 300px; margin-left: 60px; margin-top: 0px;"></asp:TextBox><br />
                        <div style="">
                            <span style="margin-left: 60px;">Address 1</span><br />
                        </div>

                    </div>

                    <div style="margin-top: 10px;">

                        <asp:TextBox ID="address2" runat="server" CssClass="popup_textbox" ClientIDMode="Static" Style="width: 300px; margin-left: 60px; margin-top: 0px;"></asp:TextBox><br />
                        <div style="">
                            <span style="margin-left: 60px;">Address 2</span><br />
                        </div>

                    </div>
                </div>
                <div style="display: -webkit-box; display: inline-flex;">
                    <div style="margin-top: 20px;">

                        <asp:TextBox ID="city" runat="server" CssClass="popup_textbox" ClientIDMode="Static" Style="width: 300px; margin-left: 60px; margin-top: 0px;"></asp:TextBox><br />
                        <div style="">
                            <span style="margin-left: 60px;">City</span><br />
                        </div>
                    </div>

                    <div style="margin-top: 20px;">

                        <select class="state" id="state" style="margin-left: 60px; width: 300px; height: 30px;">
                            <% foreach (var st in states)
                                {
                                    System.Globalization.TextInfo textInfo = new System.Globalization.CultureInfo("en-US", false).TextInfo;
                                    var stname = textInfo.ToTitleCase(st.State.ToLower());
                                    var stid = st.StateId;
                            %>
                             <option value="<%=stid %>" <%if (string.Compare( state,stname,true) == 0)
                            { %>
                            selected <%} %>><%=stname %></option>
                        <%} %>
                        </select>
                        <div style="">
                            <span style="margin-left: 60px;">State</span><br />
                        </div>
                    </div>
                </div>
                <div style="margin-top: 20px;">

                    <asp:TextBox ID="zip" runat="server" CssClass="popup_textbox" ClientIDMode="Static" Style="width: 300px; margin-left: 60px; margin-top: 0px;"></asp:TextBox><br />
                    <div style="">
                        <span style="margin-left: 60px;">Zipcode</span><br />
                    </div>


                </div>
                <div style="margin-left: 6%; margin-top: 20px;">
                    <input type="button" id="save_mom_ob" value="Save" name="save_mom_ob" class="btn_standard" />
                </div>
                <input type="hidden" clientidmode="Static" id="momobid" name="momobid" runat="server" />
                <input type="hidden" clientidmode="Static" id="asId" name="asId" runat="server" />
            </div>
        </div>
    </div>
</div>

<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery.maskedinput/1.4.1/jquery.maskedinput.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $("#ph_no").mask("(999) 999-9999", { placeholder: "" });
        $("#div_patientsearch").css("display", "block");
        $("#patientsearch_leftpart").css("display", "block");
        $("#imgsearchuser").css("display", "block");

        $("#popupclose").click(function () {
            $("#divshowmomob").dialog('close');
        });

        if (isreadyonly) {
            $(".mom_ob_body").show();
            $(".mom_ob_edit").hide();
        }

    });

    $('#save_mom_ob').click(function () {

        var dataOB, momid = $('#momobid').val(),
        aId = $('#asId').val(),
        pname = $('#phy_name').val(),
        phno = $('#ph_no').val(),
        address1 = $('#address1').val(),
        address2 = $('#address2').val(),
        city = $('#city').val(),
        zip = $('#zip').val(),
        state = $('#state').val(),
        method = (momid && momid != 0) ? 'EditMomOB' : 'AddMomOB';

        if (momid && momid != 0) {
            dataOB = { "momOBId": momid, "Name": pname, "Address1": address1, "Address2": address2, "Phone": phno, "City": city, "State": state, "ZipCode": zip };
        }
        else {
            dataOB = { "assessmentId": aId, "Name": pname, "Address1": address1, "Address2": address2, "Phone": phno, "City": city, "State": state, "ZipCode": zip };
        }

        $.ajax({
            type: "POST",
            url: "../Services/EncounterService.svc/" + method,
            data: JSON.stringify(dataOB),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.d == "error") {
                    showError({ text: 'Sorry an error has occured. Please contact administrator.' });
                }
                else if (method == 'AddMomOB') {
                    $('#momobid').val(result.d);
                    showSucess({ text: 'Mom OB updated successfully.' });
                }
                else {
                    showSucess({ text: 'Mom OB updated successfully.' });
                }
            },
            error: function () {
                showError({ text: 'Sorry an error has occured. Please contact administrator.' });
            }
        });
    });

    $("#popupclose").click(function () {
        parent.assesummary_close();
    });

</script>
