<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCTimerLog.ascx.cs"
    Inherits="Hick.PatientLookUp.UserControls.UCTimerLog" %>
<style>
    .search_label {
    background-color: #D2D3D5;
    font-size: 16px;
    width: 100%;
    margin: 0px 2px;
    padding-top: 10px;
    padding-left: 10px;
    padding-bottom: 3px;
}
    .search_img_label {
    background-color: #D3D3D3;
    width: 50px;
    float: left;
}.patsearch_heading_timer{padding:0;     height: 37px;}
.date-time {    float: right;
    margin-right: 10px;}
</style>
<div class="patsearch_border_timer">
    <asp:HiddenField ID="hdnPatientId" runat="server" />
    <asp:HiddenField ID="hdnUserId" runat="server" />
    <div class="patsearch_heading_timer">
        <div class="search_img_label">
                        <img src="../../Images/icon_refresh.png" alt="search_img" />
                    </div>
                    <div class="search_label" id="div_patientsearch" style="display: block;">
        Timer Log
         <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popup_close" class="pull-right" style="cursor: pointer;margin-top:-8px;"
                alt="close" />
        <div class="date-time">
            <%--<span>
                <asp:Label ID="lbldays" runat="server" Text="0"></asp:Label>d </span>--%><span>
                <asp:Label ID="lblhours" runat="server" Text="0"></asp:Label>h </span><span>
                    <asp:Label ID="lblmins" runat="server" Text="0"></asp:Label>m </span><span>
                    <asp:Label ID="lblsecs" runat="server" Text="0"></asp:Label>s </span>
        </div></div>
    </div>
     <table border="0" width="100%" id="taskdetails">
                <thead>
                    <tr style="text-align: center; height: 40px;" id="headrow">
                        <th style="text-align: center;width: 14%;">
                            Date
                        </th>
                        <th style="text-align: center; width: 14%;">
                            Category
                        </th>
                        <th style="text-align: center; width: 14%;">
                            Start Time
                        </th>
                        <th style="text-align: center; width: 14%;">
                            End Time
                        </th>
                        <th style="text-align: center; width: 14%;">
                            Total Time
                        </th>
                        <th style="text-align: center; width: 6%;">
                            
                        </th>
                        <th style="text-align: center; width: 5%;">
                            
                        </th>
                    </tr>
                </thead>            
        </table>
    <div id="tdprint" style="padding: 8px; height: 400px; overflow: auto;">
        

        <asp:Repeater ID="taskdetailsrepeater" runat="server" ClientIDMode="Static"   OnItemCommand="Taskdetailsrepeater_ItemCommand" >
            <HeaderTemplate>
             <table border="0" width="100%" id="taskdetails">
                 <%--  <thead>
                    <tr style="text-align: center; height: 40px;" id="headrow">
                        <th style="text-align: center; width: 14%;">
                            Date
                        </th>
                        <th style="text-align: center; width: 14%;">
                            Category
                        </th>
                        <th style="text-align: center; width: 14%;">
                            Start Time
                        </th>
                        <th style="text-align: center; width: 14%;">
                            End Time
                        </th>
                        <th style="text-align: center; width: 14%;">
                            Total Time
                        </th>
                        <th style="text-align: center; width: 5%;">
                            
                        </th>
                        <th style="text-align: center; width: 5%;">
                            
                        </th>
                    </tr>
                </thead>            --%>
                     <%-- <div class="scrollit">--%>     
                         
            </HeaderTemplate>          
            <ItemTemplate>                         
                <tr style="height: 70px; border: 1px solid #D3D3D3; width: 100%;" class="mainrow">
                    <td colspan="11">                       
                        <table width="100%">
                            <tr>
                                <td style="text-align: center; width: 15%;">
                                    <asp:Label ID="lbldate"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Date")%>' ></asp:Label>
                                </td>
                                <td style="text-align: center; width: 15%;">
                                    <asp:Label ID="lblcategory" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Category")%>'></asp:Label>
                                </td>
                                <td style="text-align: center; width: 15%;">
                                    <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"starttime")%>'></asp:Label>
                                </td>
                                <td style="text-align: center; width: 15%;">
                                    <asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"endtime")%>'></asp:Label>
                                </td>
                                <td style="text-align: center; width: 15%;">
                                    <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"totaltime")%>'></asp:Label>
                                </td>
                                <td style="width: 5%;">
                                    <asp:LinkButton  CommandName="edit" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"taskid") + ":" + DataBinder.Eval(Container.DataItem,"tasktype")%>' Text="" runat="server">
                                        <asp:image imageurl="..\..\Images\timer_edittask.png" runat="server"/>

                                    </asp:LinkButton> 
                                </td>
                                <td style="width: 5%;">
                                    <asp:LinkButton  CommandName="delete" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"taskid") + ":" + DataBinder.Eval(Container.DataItem,"tasktype")%>' Text="" runat="server">
                                        <asp:image imageurl="..\..\Images\timer_deletetask.png" runat="server"/>

                                    </asp:LinkButton> 
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7" style="text-align: left; padding: 5px;">
                                    <span style="margin-left: 6%;">Task Details:
                                        <asp:Label ID="lbltaskdetails" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TaskDetails")%>'></asp:Label></span>
                                </td>
                            </tr>
                        </table>    
                    </td>
                </tr>                           
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <div style="padding-top: 12px;padding-right:1px; float: left; margin-left: 10px; font-weight:600;">
        <asp:Button ID="btnprinttask" runat="server" Text="Print" OnClientClick="javascript:return Print();" BackColor="#FFCB05" Width="100px" Height="30" BorderWidth="0" />        
    </div>
    <div style="padding-top: 12px;padding-right:1px; float: left; margin-left: 10px; font-weight:600;">
        <asp:Button ID="btnexporttask" runat="server" Text="Export" OnClick="btnexporttask_click" BackColor="#FFCB05" Width="100px" Height="30" BorderWidth="0"/>        
    </div>
    <div style="padding-top: 12px;padding-right:1px; float: left; margin-left: 10px; font-weight:600;">
        <asp:Button ID="btnbillingtask" Visible="false" runat="server" Text="Create Billing Report"  BackColor="#FFCB05" Width="150px" Height="30" BorderWidth="0"/>        
    </div>
    <div style="padding-top: 12px; float: right; margin-right: 40px; font-weight:600;">
        <asp:Button ID="btnaddtask" runat="server" Text="Add Task" OnClick="btnaddtask_click" BackColor="#FFCB05" Width="100px" Height="30" BorderWidth="0"/>        
    </div>
</div>
<div id="overlay">
     <div id="overlaysub">
         <div style="float: right;padding:0px; width:20px;height:20px">
           <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popupclose" style="cursor: pointer;background-position:top right;width:20px;height:20px"  alt="close" />
             </div>
          <p><asp:Label ID="lblprompt" Text="Are you sure you want to delete this task?" runat="server" /></p>
         <p><asp:Button ID="btnOk" runat="server" Text="Ok" OnClick="btnOk_click" BackColor="#FFCB05" Width="100px" Height="30" BorderWidth="0"/> <asp:Button ID="btnYes" runat="server" Text="Yes" OnClick="btnYes_click" BackColor="#FFCB05" Width="100px" Height="30" BorderWidth="0"/> &nbsp;&nbsp;<asp:Button ID="btnNo" runat="server" Text="No" OnClick="btnNo_click" BackColor="#FFCB05" Width="100px" Height="30" BorderWidth="0"/></p>
     </div>
</div>
<script type="text/javascript">
    
    $(document).ready(function () {
        $("#div_timermanagement").css("display", "block");
        $("#TimerManagement_LeftPart").css("display", "block");
        $("#timermanagement").css("display", "block");

        $("table#taskdetails tr:even").each(function () {
            if ($(this).hasClass("mainrow")) {
                $(this).css("background-color", "#EBE4E4");
            }
        });

        $("#headrow").css("background-color", "white");

        $(".patsearch_border").css("overflow-y", "hidden");
    });
     $("#popupclose").click(function () {
        window.location = "TimerLog.aspx?UserId=" + <%=Session["userid"].ToString()%> + "&PatientId=" + <%=Session["patientid"].ToString()%>;
    });
    function ShowCancelTask() {
        $("#cancelConfirm").modal('show');
    }

    function Print() {

            var printPriviewHTML = "<html><head></head><body><div> ";
            var prtContent = document.getElementById('tdPrint').innerHTML;
            var WinPrint = window.open('', '', 'left=100,top=100,toolbar=0,scrollbars=0,status=0,resizable=yes,width=700,height=700');
            
            var totalContent =  prtContent;
            printPriviewHTML += totalContent;
            printPriviewHTML += "</div></body></html>";
            WinPrint.document.write(printPriviewHTML);
            
            WinPrint.document.close();
            WinPrint.focus();
            
            WinPrint.print();
            return false;
    }

    
    $("#popup_close").click(function () {
        parent.popup_close();
    });

  </script>