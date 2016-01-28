<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCReferralReceipt.ascx.cs" Inherits="Hick.PatientLookUp.UserControls.UCReferralReceipt" %>
<style>
div.patient_search_right_frame{margin:5px 0;}
.patsearch_heading.patient{width:100%; padding-top:4px;background:#E3E4E6;}
.patsearch_heading.patient .btn_standard{margin:0px 50px 0 0!important; padding:5px 10px; float:right;}
.patsearch_heading.patient #popup_close{ margin-top: 0!important;}
.form-group.dob label {
    width: 100%;
}
.patsearch_heading {
    background-color: #E3E4E6;
    font-size: 18px;
    height: 39px;
   /* padding-top: 7px;*/
    padding-left: 10px;
    border-bottom: 2px solid #dfdfdf;
    /* width: 100%; */
}
</style>
<div class="container">
    <div class="row">
    <div class="patsearch_heading patient">
        Referral Details
        <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popup_close" class="pull-right" style="cursor: pointer;margin-top:-42px;"
                alt="close" />
    </div>
    <span>&nbsp;</span>
    </div>
  
        <div class="row" style="overflow-y: auto;">
            <div class="col-sm-6 col-md-6 col-lg-6">
                <div class="row t1b">
                  <table class=" table table-condensed">
                          <tr>
                            <th class="tg-yw4l thhead" colspan="4">Patient Information</th>
                          </tr>
                          <tr>
                            <td class="tg-yw4l1" colspan="2">Last Name</td>
                            <td class="tg-yw4l" colspan="2"><asp:Label runat="server" ID="lblLastName"></asp:Label></td>
                          </tr>
                          <tr>
                            <td class="tg-yw4l1" colspan="2">First Name</td>
                            <td class="tg-yw4l" colspan="2"><asp:Label runat="server" ID="lblFirstName"></asp:Label></td>
                          </tr>
                          <tr>
                            <td class="tg-yw4l1" colspan="2">DOB</td>
                            <td class="tg-yw4l" colspan="2"><asp:Label runat="server" ID="lblDOB"></asp:Label></td>
                          </tr>
                          <tr>
                            <td class="tg-yw4l1" colspan="2">Phone Number</td>
                            <td class="tg-yw4l" colspan="2"><asp:Label runat="server" ID="lblPhone"></asp:Label></td>
                          </tr>
                          <tr>
                            <td class="tg-yw4l1" colspan="2">Email</td>
                            <td class="tg-yw4l" colspan="2"><asp:Label runat="server" ID="lblEmail"></asp:Label></td>
                          </tr>
                          <tr>
                            <td class="tg-yw4l1" colspan="2">Last 4 digits of SSn</td>
                            <td class="tg-yw4l" colspan="2"><asp:Label runat="server" ID="lblssn"></asp:Label></td>
                          </tr>
                          <tr>
                            <td class="tg-yw4l1" colspan="2">Address</td>
                            <td class="tg-yw4l" colspan="2"><asp:Label runat="server" ID="lblAddress"></asp:Label></td>
                          </tr>
                           <tr>
                            <td class="tg-yw4l1" colspan="2">City</td>
                            <td class="tg-yw4l" colspan="2"><asp:Label runat="server" ID="lblcity"></asp:Label></td>
                          </tr>
                           <tr>
                            <td class="tg-yw4l1" colspan="2">State</td>
                            <td class="tg-yw4l" colspan="2"><asp:Label runat="server" ID="lblState"></asp:Label></td>
                          </tr>
                           <tr>
                            <td class="tg-yw4l1" colspan="2">Zip</td>
                            <td class="tg-yw4l" colspan="2"><asp:Label runat="server" ID="lblZip"></asp:Label></td>
                          </tr>
                           <tr>
                            <td class="tg-yw4l1" colspan="2">Insurance Type</td>
                            <td class="tg-yw4l" colspan="2"><asp:Label runat="server" ID="lblInsurance"></asp:Label></td>
                          </tr>
                           <tr>
                            <td class="tg-yw4l1" colspan="2">Company Name</td>
                            <td class="tg-yw4l" colspan="2"><asp:Label runat="server" ID="lblCompanyname"></asp:Label></td>
                          </tr>
                           <tr>
                            <td class="tg-yw4l1" colspan="2">Policy Name</td>
                            <td class="tg-yw4l" colspan="2"><asp:Label runat="server" ID="lblPolicyName"></asp:Label></td>
                          </tr>
                        </table>
                </div>
            </div>
            <div class="col-sm-1 col-md-1 col-lg-1"></div>
            <div class="col-sm-5 col-md-5 col-lg-5">
                <div class="row t1b">
                        <table class=" table table-condensed">
                          <tr>
                            <th class="tg-yw4l thhead" colspan="4">Referral Information</th>
                          </tr>
                          <tr>
                            <td class="tg-yw4l1" colspan="2">Referral ID</td>
                            <td class="tg-yw4l" colspan="2"><asp:Label runat="server" ID="lblReferralID"></asp:Label></td>
                          </tr>
                          <tr>
                            <td class="tg-yw4l1" colspan="2">Provider Name</td>
                            <td class="tg-yw4l" colspan="2"><asp:Label runat="server" ID="lblAssignedPhysician"></asp:Label></td>
                          </tr>
                          <tr>
                            <td class="tg-yw4l1" colspan="2">Provider Address</td>
                            <td class="tg-yw4l" colspan="2"><asp:Label runat="server" ID="lblProviderAdd"></asp:Label></td>
                          </tr>
                          <tr>
                            <td class="tg-yw4l1" colspan="2">Provider Phone Number</td>
                            <td class="tg-yw4l" colspan="2"><asp:Label runat="server" ID="lblPhoneNum"></asp:Label></td>
                          </tr>
                          <tr>
                            <td class="tg-yw4l1" colspan="2">Provider Fax Number</td>
                            <td class="tg-yw4l" colspan="2"><asp:Label runat="server" ID="ui_lblFaxNum"></asp:Label></td>
                          </tr>
                          <tr>
                            <td class="tg-yw4l1" colspan="2">Requested By</td>
                            <td class="tg-yw4l" colspan="2"><asp:Label runat="server" ID="lblRequestedBy"></asp:Label></td>
                          </tr>
                          <tr>
                            <td class="tg-yw4l1" colspan="2">Ordering Physician Name</td>
                            <td class="tg-yw4l" colspan="2"><asp:Label runat="server" ID="lblOdrPhysician"></asp:Label></td>
                          </tr>
                        </table>
                </div>
                <span>&nbsp;</span>
                <div class="row t1b">
                   
                    <table class=" table table-condensed">
                          <tr>
                            <th class="tg-yw4l thhead" colspan="4">Appointment Preferences</th>
                          </tr>
                          <tr>
                            <td class="tg-yw4l1" colspan="2">Appointement Date1</td>
                            <td class="tg-yw4l" colspan="2"><asp:Label runat="server" ID="lblAppointmentDate"></asp:Label></td>
                          </tr>
                          <tr>
                            <td class="tg-yw4l1" colspan="2">Appointement Time1</td>
                            <td class="tg-yw4l" colspan="2"><asp:Label runat="server" ID="lblAppointmentTime"></asp:Label></td>
                          </tr>
                          <tr>
                            <td class="tg-yw4l1" colspan="2">&nbsp;</td>
                            <td class="tg-yw4l" colspan="2">&nbsp;</td>
                          </tr>
                          <tr>
                            <td class="tg-yw4l1" colspan="2">Additional Appointment Notes</td>
                            <td class="tg-yw4l" colspan="2"><asp:Label runat="server" ID="lblAddAppointementNotes"></asp:Label></td>
                          </tr>
                        </table>
                </div>

            </div>
        </div>
        
    </div>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#div_patientsearch").css("display", "block");
            $("#patientsearch_leftpart").css("display", "block");
            $("#imgsearchuser").css("display", "block");
            $("#popupclose").click(function () {

                $("#divshowconditions").dialog('close');
            });




            $("#add_conditions").click(function () {
                $('.Content').text("Add New");

                // $('<span>Add New</span>').appendTo('.Content');

                // $('<span>Add New</span>').appendTo('.Content');

                $(".edit_conditionsdiv").css("display", "block");

                $("#frmeditconditions_popup").attr("src", "");
                $("#frmeditconditions_popup").attr("src", "EditEncounters.aspx");
                showpopup();
            });
        });

        function showpopup() {
            var popuphight = window.innerHeight - 200;
            var popupwidth = window.innerWidth - 490;

            $("#divshowconditions").dialog(
            {
                modal: true,
                height: popuphight,
                width: popupwidth,
                resizable: false,
                title: "Referral",
                create: function () {
                    $(".ui-dialog-titlebar").hide();
                    $(".ui-dialog-content").css("padding", "0px");
                }
            });
        }
        function ClosePopup() {

            $("#divshowconditions").dialog('close');

        }
        $("#popup_close").click(function () {
            parent.popup_close();
        });
</script>