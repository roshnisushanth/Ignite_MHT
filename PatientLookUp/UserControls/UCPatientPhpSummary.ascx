<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCPatientPhpSummary.ascx.cs" Inherits="Hick.PatientLookUp.UserControls.UCPatientPhpSummary" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link href="../../Content/style.css" rel="stylesheet" />
    <link href="../../Content/patientsummary.css" rel="stylesheet" />
    <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
    <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap-theme.min.css">
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>

       <script src="../../Scripts/HighCharts/highcharts.js" type="text/javascript"></script>

    <script type="text/javascript" src="//code.highcharts.com/highcharts-more.js"></script>
    <script type="text/javascript" src="//code.highcharts.com/modules/solid-gauge.js"></script> 

    <style>
        .ele_center {

            margin: 5px 2px;

        }
        .popup_textbox {
    width: 170px;
}
        #gdfamilyhistory .test-width1 {
    width: 160px!important;
}
.box-bottom table {
    position: relative;
}
.box-bottom table tr:first-child{ top:0;     width: 100%;
    background: #ffffff;}
.rw1 .col-lg-12.col-md-12.box-top {
    padding: 0 15px!important;
    height: 43px;
}.box-bottom table th div{text-align:center;}
    .col-lg-12.col-md-12.hdr.summ.head .col-lg-4.col-md-4 {
    width: 190px;
    float: left;
    padding: 0;
}
        .col-lg-1.col-md-1.boxs.pull-right.text-center {
   width: 51px;
    float: left!important;
    font-size: 12px;
}
.test-width{width:130px;}
.test-width1{width:160px;}
.clos-edit{width:85px;}
.ui-dialog.ui-widget.ui-widget-content.ui-corner-all.ui-front.ui-draggable[aria-describedby="divshowencounter"] {
    height: 400px!important;
}
.ui-dialog.ui-widget.ui-widget-content.ui-corner-all.ui-front.ui-draggable[aria-describedby="divshowencounter"] #divshowconditions{height:auto!important;}

.ui-dialog.ui-widget.ui-widget-content.ui-corner-all.ui-front.ui-draggable[aria-describedby="divshowencounter"] div#divshowencounter {
    height: auto!important;
}
.ui-dialog.ui-widget.ui-widget-content.ui-corner-all.ui-front.ui-draggable[aria-describedby="divshare"] {
    width: 600px!important;
    height: 443px!important;
}
.ui-dialog.ui-widget.ui-widget-content.ui-corner-all.ui-front.ui-draggable[aria-describedby="divshare"] #divshare{height:auto!important;}

    </style>

</head>
<body>
    
 
    
    <div class="patsearch_heading patient">
        PHP Summary
        <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popup_close" class="pull-right php-pop" alt="close" />
    </div>
    
        <div class="patsearch_border">
        <%--div for profile pic and info--%>
        <div class = "col-lg-12 col-md-12 hdr summ head" style="margin-bottom:20px;">
            <div class = "col-lg-2 col-md-2 prf usr-icon">
                <img src="../../Images/default_user.png" class="prf_pic" id="prf_pic" alt="Profile Pic" />
            </div>
             <div class="col-lg-4 col-md-4">
                 <div class="usr_name">
                         <asp:Label CssClass="nme" ID="lblpatname" runat="server"></asp:Label><br  />
                 <%--   <span class="log_time "></span>--%>
                      <asp:Label CssClass="nme" ID="lblLastLoggedin" runat="server"></asp:Label>
                </div>
                 
            </div>
            <div class="col-lg-1 col-md-1 col-sm-1" style="padding:0;"><a  href="" class="poptip"><asp:Button runat="server" OnClick="Button_PHPDownload_Click"  class="down-php"/><span>Download PHP</span></a>
               <a class="poptip"> <img src="<%=Page.ResolveUrl("~/Images/folder-sharePHP.png") %>" onclick="ShowSharePopup()"  class="down-phps"/><span>Share </span></a></div>
            <div class="col-lg-1 col-md-1 boxs  pull-right text-center php-top">
                 <span class="tp_hdr ">BP</span><br  /><br  />
                  <asp:Label CssClass="btm_summary" ID="lblBP" runat="server"></asp:Label>
            </div>
            <div class="col-lg-1 col-md-1 boxs pull-right text-center php-top">
                <span class="tp_hdr ">Age</span><br  /><br  />
                  <asp:Label CssClass="btm_summary" ID="lblAge" runat="server"></asp:Label>
            </div>
             <div class="col-lg-1 col-md-1 boxs pull-right text-center php-top">
                <span class="tp_hdr ">Gender</span><br  /><br  />
                 <asp:Label CssClass="btm_summary" ID="lblgender" runat="server"></asp:Label>
            </div>
             <div class="col-lg-1 col-md-1 boxs pull-right text-center php-top">
                <span class="tp_hdr ">Weight</span><br  /><br  />
                  <asp:Label CssClass="btm_summary" ID="lblweight" runat="server"></asp:Label>
            </div>
            <div class="col-lg-1 col-md-1 boxs pull-right text-center php-top">
                <span class="tp_hdr ">Height</span><br  /><br  />
                  <asp:Label CssClass="btm_summary" ID="lblheight" runat="server"></asp:Label>
            </div>
</div>
        <div class="col-lg-12 col-md-12 rw1 summ-mid sum-marg">
            
               
                    
                     <div class="col-lg-12 col-md-12 box-top">
                        <h5 class="pull-left">
                       <img src="../../Images/Conditions.png" class="icon-set" alt="" width="20px" height="20px" />
                       <strong>Conditions</strong>  <span class="php-arrow"></span></h5>
                  
                             <input type="button" value="Add New" name="add_conditions" class="btn_standard php-add" id="add_conditions" />
                    </div>
                     <div class="box-bottom">
    <asp:GridView runat="server" ID="grdconditions" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" HeaderStyle-BorderWidth="0" RowStyle-BorderWidth="1px" RowStyle-BorderColor="#D3D3D3">
            <Columns>
                <asp:TemplateField ItemStyle-CssClass="table_data_list" HeaderStyle-Width="165px">
                    <HeaderTemplate>
                        <div class="ele_center icd">
                            ICD 10 Code</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div  class="ele_center eval">
                            <asp:Label CssClass="lblicd9code1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ICDCode")%>'></asp:Label></div>
                        <span class="lblpatientid1" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem,"PatientID")%>
                        </span><span class="spnconditioncheck1" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem, "ConditionCheck")%>
                        </span><span class="spnhistory1" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem, "History")%>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="table_data_list" HeaderStyle-Width="190px">
                    <HeaderTemplate>
                        <div class="ele_center des test-width1">
                            Description</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div  class="ele_center test-width1">
                            <asp:Label CssClass="lbldesc1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Condition")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="table_data_list" HeaderStyle-Width="120px">
                    <HeaderTemplate>
                        <div  class="ele_center icd">
                            Active Since</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="ele_center eval">
                            <asp:Label CssClass="lbldos1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"DateOfOnset","{0:MM/dd/yyyy}")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="table_data_list" HeaderStyle-Width="187px">
                    <HeaderTemplate>&nbsp;
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="text-align: center;" class="">
                            <img style="cursor: pointer;" src="../../Images/button_edit.jpg" alt="Edit" class="edit_conditions"
                                cid='<%#DataBinder.Eval(Container.DataItem,"ConditionID")%>' />
                            <!--<img style="cursor: pointer;" src="../../Images/button_close.jpg" alt="Delete" class="delete_conditions"
                                cid='<%#DataBinder.Eval(Container.DataItem,"ConditionID")%>' />-->
                             <img style="cursor: pointer;" src="../../Images/button_close.jpg" alt="Delete" class="delete_conditions" 
                                 onclick="return deleteconNote(<%#DataBinder.Eval(Container.DataItem,"ConditionID")%>);" />
 
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
                     
            
                     <div class="col-lg-12 col-md-12 box-top">
                   <h5 class="pull-left">
                       <img src="../../Images/Medications.png" class="icon-set" alt="" width="20px" height="20px" />
                       <strong>Medications</strong> <span class="php-arrow"></span></h5>                
                    <input type="button" value="Add New" name="add_medications" class="btn_standard php-add" id="add_medications" />

                    </div>
                    <div class="box-bottom">
                     <asp:GridView runat="server" ID="gdconditions" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" HeaderStyle-BorderWidth="0" RowStyle-BorderWidth="1px" RowStyle-BorderColor="#D3D3D3">
            <Columns>
                <asp:TemplateField ItemStyle-CssClass="table_data_list" >
                    <HeaderTemplate>
                        <div class="ele_center icd test-width1">
                            Medications</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="ele_center test-width1">
                            <asp:Label ID="lblDescription" CssClass="lbldesc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Description")%>'></asp:Label></div>
                        <span class="lblpatientid" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem,"PatientID")%>
                        </span>
                          
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="table_data_list" >
                    <HeaderTemplate>
                        <div style="  class="ele_center">
                            Dosage</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style=" white-space: nowrap; class="ele_center">
                            
                            <asp:Label ID="lblDosage" CssClass="lbldos" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Dosage")%>'></asp:Label>
                            <asp:Label ID="lblDosageunits" CssClass="lbldosunits" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem,"DosageUnits")%> '></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list" >
                    <HeaderTemplate>
                        <div  class="ele_center">
                            Active Since</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style=" white-space: nowrap;" class="ele_center">
                            <asp:Label ID="lblActivedate" CssClass="lbldate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Date")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list" >
                    <HeaderTemplate>&nbsp;
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div   class="enco-butt">                      
                            <img style="cursor: pointer;" src="../../Images/button_edit.jpg" alt="Edit" class="edit_medications" cid='<%#DataBinder.Eval(Container.DataItem,"MedicationID")%>' />
                                 <img style="cursor: pointer;" src="../../Images/button_close.jpg" alt="Delete" class="delete_med" 
                                 onclick="return deletemedNote(<%#DataBinder.Eval(Container.DataItem,"MedicationID")%>);" />
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
                
           

                     <div class="col-lg-12 col-md-12  box-top">
                        <h5 class="pull-left">
                       <img src="../../Images/Immunizations.png" class="icon-set" alt="" width="20px" height="20px" />
                       <strong>Immunizations</strong> <span class="php-arrow"></span></h5>
               <input type="button" value="Add New" name="add_immunizations" class="btn_standard php-add" id="add_immunizations"/>
                    </div>
                        <div class=" box-bottom">
                                 <asp:GridView runat="server" ID="gdimmunizations" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" HeaderStyle-BorderWidth="0" RowStyle-BorderWidth="1px" RowStyle-BorderColor="#D3D3D3">
            <Columns>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div class="ele_center">
                            Immunization Name</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="ele_center">
                            <asp:Label ID="lblImmunization" CssClass="lblImmunization" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ImmunizationType")%>'></asp:Label></div>
                        <span class="lblpatientid" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem,"PatientID")%>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div class="ele_center">
                            Date Received</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style=" white-space: nowrap;" class="ele_center">
                            <asp:Label ID="lbldate" CssClass="lbldate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AdministrationDate","{0:MM/dd/yyyy}")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="enco-butt">                      
                            <img style="cursor: pointer;" src="../../Images/button_edit.jpg" alt="Edit" class="edit_Immunization" cid='<%#DataBinder.Eval(Container.DataItem,"ImmunizationID")%>' />
                                 <img style="cursor: pointer;" src="../../Images/button_close.jpg" alt="Delete" class="delete_conditions" 
                                 onclick="return deleteimmNote(<%#DataBinder.Eval(Container.DataItem,"ImmunizationID")%>);" />
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
                   
                  
                    
           
            
              
                     <div class="col-lg-12 col-md-12  box-top">
                         <h5 class="pull-left">
                       <img src="../../Images/FamilyHistory.png" class="icon-set" alt="" width="20px" height="20px" />
                       <strong>Family History</strong> <span class="php-arrow"></span></h5>
                 
                             <input type="button" value="Add New" name="add_family" class="btn_standard php-add" id="add_family" />
                    </div>
                    <div class="box-bottom">

                      <asp:GridView runat="server" ID="gdfamilyhistory" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" HeaderStyle-BorderWidth="0" RowStyle-BorderWidth="1px" RowStyle-BorderColor="#D3D3D3">
            <Columns>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div class="ele_center">
                            Relationship</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style=" white-space: nowrap;" class="ele_center">
                            <asp:Label ID="lblRelationship" CssClass="lblRelationship" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Relationship")%>'></asp:Label></div>
                        <span class="lblpatientid" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem,"PatientID")%>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div class="ele_center test-width1">
                            Condition</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="ele_center test-width1">
                            <asp:Label ID="lblCondition" CssClass="lblCondition" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ConditionName")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div class="ele_center">
                            Onset Date</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="ele_center">
                            <asp:Label ID="lbldate" CssClass="lbldate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"OnsetDate","{0:MM/dd/yyyy}")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="text-align: center;" class="">
                            <img style="cursor: pointer;" src="../../Images/button_edit.jpg" alt="Edit" class="edit_familyhistory"
                                cid='<%#DataBinder.Eval(Container.DataItem,"PatientFamilyHistoryID")%>' />
                            <img style="cursor: pointer;" src="../../Images/button_close.jpg" alt="Delete" class="delete_conditions" 
                                 onclick="return deletefamNote(<%#DataBinder.Eval(Container.DataItem,"PatientFamilyHistoryID")%>);" />
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
               
            

             
                 
                    <div class="col-lg-12 col-md-12  box-top">
                        <h5 class="pull-left">
                       <img src="../../Images/SocialHistory.png" class="icon-set" alt="" width="20px" height="20px" />
                       <strong>Social History</strong> <span class="php-arrow"></span></h5>
                    </div>
                     <div class=" box-bottom from-bottom">
                   <iframe id="socialhistory" src="../../PatientLookUp/ASPX/ViewSocialHistory.aspx" style="width:100%; height:100%; border: none;"></iframe>
                    </div>
            
                 

                 
                     <div class="col-lg-12 col-md-12 box-top">
                        <h5 class="pull-left">
                       <img src="../../Images/Results.png" class="icon-set" alt="" width="20px" height="20px" />
                       <strong>Results</strong>  <span class="php-arrow"></span></h5>
               
                    </div>
                     <div class="box-bottom" style="overflow-x:scroll;">
      <asp:GridView runat="server" ID="gdlabresult" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" HeaderStyle-BorderWidth="0" RowStyle-BorderWidth="1px" RowStyle-BorderColor="#D3D3D3">
            <Columns>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div class="ele_center">
                           Date</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style=" white-space: nowrap;" class="ele_center">
                            <asp:Label ID="lbldate" CssClass="lbldate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Date")%>'></asp:Label></div>
                        <span class="lblpatientid" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem,"PatientID")%>
                        </span><span class="lbllabId" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem, "LabImagingId")%>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div class="ele_center">
                           Test Type</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style=" white-space: nowrap;" class="ele_center">
                            <asp:Label ID="lblTestType" CssClass="lblTestType" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TestType")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div class="ele_center">
                           Results</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="ele_center">
                            <asp:Label ID="lblresult" CssClass="lblresult" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Results")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div class="ele_center">
                           Requesting Doctor</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="ele_center">
                            <asp:Label ID="lbldoctor" CssClass="lbldoctor" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"RequestingDoctor")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div class="ele_center">
                         Administered by</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="ele_center">
                            <asp:Label ID="lbladmin" CssClass="lbladmin" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AdministeredBy")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="clos-edit">
                         
                            <img style="cursor: pointer;" src="../../Images/button_edit.jpg" alt="Edit" class="edit_labresult"
                                cid='<%#DataBinder.Eval(Container.DataItem,"LabImagingId")%>' />
                                   <img style="cursor: pointer;" src="../../Images/button_close.jpg" alt="Delete" class="delete_labresult"
                                cid='<%#DataBinder.Eval(Container.DataItem,"LabImagingId")%>' /></div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="lblnorecords" Text="No records found" Font-Bold="true" runat="server"
                    Style="white-space: nowrap; padding: 10px;"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
                     </div>
         

           
                
                     <div class="col-lg-12 col-md-12  box-top">
                        <h5 class="pull-left">
                       <img src="../../Images/Icon-Demographics.png" class="icon-set" alt="" width="20px" height="20px" />
                       <strong>Demographics</strong> <span class="php-arrow"></span></h5>
                
                    </div>
                    <div class=" box-bottom from-bottom">
              <iframe src="../../PatientLookUp/ASPX/ViewDemographics.aspx" style="width:100%; height:100%; border: none;"></iframe>
                     </div>
            
                
                        
               
                     <div class="col-lg-12 col-md-12 box-top">
                        <h5 class="pull-left">
                       <img src="../../Images/Icon-Problems.png" class="icon-set" alt="" width="20px" height="20px" />
                            <strong>Allergies</strong> <span class="php-arrow"></span></h5>
                         <input type="button" value="Add New" name="add_allergies" class="btn_standard php-add" id="add_allergies" />
                    </div>
                    <div class=" box-bottom">
                      <asp:GridView runat="server" ID="gdallergy" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" HeaderStyle-BorderWidth="0" RowStyle-BorderWidth="1px" RowStyle-BorderColor="#D3D3D3">
            <Columns>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div class="ele_center">
                            Allergy</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style=" white-space: nowrap;" class="ele_center">
                            <asp:Label ID="lblAllergy" CssClass="lblallergytype" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AllergyType")%>'></asp:Label></div>
                        <span class="lblpatientid" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem,"PatientID")%>
                        </span><span class="spnismedallergy" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem, "IsMedicationAllergy")%>
                        </span><span class="spntreatment" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem, "Treatment")%>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div class="ele_center">
                            Reaction</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="ele_center">
                            <asp:Label ID="lblReaction" CssClass="lblreactions" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Reaction")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div class="ele_center">
                            Active Since</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style=" white-space: nowrap;" class="ele_center">
                            <asp:Label ID="lblactivesince" CssClass="lbldos" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"DateLastOccured")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div  class="enco-butt">
                         
                            <img style="cursor: pointer;" src="../../Images/button_edit.jpg" alt="Edit" class="edit_allergies"
                                cid='<%#DataBinder.Eval(Container.DataItem,"AllergyID")%>' />
                                   <img style="cursor: pointer;" src="../../Images/button_close.jpg" alt="Delete" class="delete_conditions" 
                                 onclick="return deletealgNote(<%#DataBinder.Eval(Container.DataItem,"AllergyID")%>);" /></div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="lblnorecords" Text="No records found" Font-Bold="true" runat="server"
                    Style="white-space: nowrap; padding: 10px;"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
                     </div>
                  
            


                
                     <div class="col-lg-12 col-md-12 box-top">
                        <h5 class="pull-left">
                       <img src="../../Images/Icon-Encounters.png" class="icon-set" alt="" width="20px" height="20px" />
                       <strong>Encounters</strong> <span class="php-arrow"></span></h5>
                         <input type="button" value="Add New" class="btn_standard php-add" id="add_encounter" />
                    </div>
                    <div class="box-bottom">

        <asp:GridView runat="server" ID="gdencounters" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" HeaderStyle-BorderWidth="0" RowStyle-BorderWidth="1px" RowStyle-BorderColor="#D3D3D3">
            <Columns>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div class="ele_center">
                            Date</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style=" white-space: nowrap;" class="ele_center">
                            <asp:Label ID="lblvisitdate" CssClass="lblvisitdate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Visitdate","{0:MM/dd/yyyy}")%>'></asp:Label></div>
                        <span class="lblpatientid" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem,"PatientID")%>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div class="ele_center">
                            Doctor</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="ele_center">
                            <asp:Label ID="lbldoctor" CssClass="lbldoctor" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"DoctorName")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div class="ele_center">
                           Reason</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="ele_center">
                            <asp:Label ID="lblvisitreason" CssClass="lblvisitreason" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"VisitReason")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div class="ele_center">
                           Diagnoses</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="ele_center">
                            <asp:Label ID="lbldiagnosis" CssClass="lbldiagnosis" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"VisitDiagnosis")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="text-align: center;"  class="enco-butt">
                            <img style="cursor: pointer;" src="../../Images/button_edit.jpg" alt="Edit" class="edit_encounter"
                                cid='<%#DataBinder.Eval(Container.DataItem,"DoctorVisitId")%>' />
                            <img style="cursor: pointer;" src="../../Images/button_close.jpg" alt="Delete" class="delete_conditions" 
                                 onclick="return deleteencNote(<%#DataBinder.Eval(Container.DataItem,"DoctorVisitId")%>);" />
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
                    
            


              
                     <div class="col-lg-12 col-md-12  box-top">
                        <h5 class="pull-left">
                       <img src="../../Images/Icon-Referrals.png" class="icon-set" alt="" width="20px" height="20px" />
                       <strong>Referrals</strong> <span class="php-arrow"></span></h5>
                        
                    </div>
                    <div class=" box-bottom" style="min-height:150px;">
            <asp:GridView runat="server" ID="gdreferral" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" HeaderStyle-BorderWidth="0" RowStyle-BorderWidth="1px" RowStyle-BorderColor="#D3D3D3" OnRowCommand="gdreferral_RowCommand">
            <Columns>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div class="ele_center">
                            Referral Requested Date</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="white-space: nowrap;" class="ele_center">
                            <asp:Label ID="lbldate" CssClass="lbldate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CreatedDate","{0:MM/dd/yyyy}") %>'></asp:Label></div>
                        <span class="lblpatientid" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem,"PatientID")%>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div class="ele_center">
                            Referred To</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="ele_center">
                            <asp:Label ID="lblreferredto" CssClass="lblreferredto" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AssignedPhysician")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div class="ele_center">
                            Ordering Physician</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="ele_center">
                            <asp:Label ID="lblordphys" CssClass="lblordphys" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ReferredBy")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div class="ele_center">
                            Appointment Date/Time</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="ele_center">
                            <asp:Label ID="lblapptdate" CssClass="lblapptdate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AppDatePref1","{0:MM/dd/yyyy}")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div  class="ele_center">
                            Status</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="float: left; white-space: nowrap;" class="ele_center">
                            <asp:Label ID="lblstatus" CssClass="lblstatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"StatusText")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="text-align: center;"  class="enco-butt">
                            <asp:Button ID="btnview" Text="View" runat="server" class="btn_standard" CommandName="Export" style="min-width:90px;"
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem, "ReferralId")%>'></asp:Button></div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="Label1" Text="No information available" Font-Bold="true" runat="server" Style="white-space: nowrap;
                    padding: 10px;"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>


                     </div>
                
            

            
                
                     <div class="col-lg-12 col-md-12  box-top">
                        <h5 class="pull-left">
                       <img src="../../Images/Icon-Tests-and-Procedures.png" class="icon-set" alt="" width="20px" height="20px" />
                       <strong>Tests and Procedures</strong> <span class="php-arrow"></span></h5>
                      <input type="button" value="Add New" name="add_testandprocedure" class="btn_standard php-add" id="add_testandprocedure"/>
                    </div>
                    <div class=" box-bottom">
    <asp:GridView runat="server" ID="gdtestandprocedure" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" HeaderStyle-BorderWidth="0" RowStyle-BorderWidth="1px" RowStyle-BorderColor="#D3D3D3">
            <Columns>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div class="ele_center test-width">
                           Procedure</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="ele_center test-width">
                            <asp:Label ID="lblprocedure" CssClass="lblprocedure" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SurgeriesProcedure")%>'></asp:Label></div>
                        <span class="lblpatientid" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem,"PatientID")%>
                        </span><span class="lblSurgeriesId" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem, "SurgeriesId")%>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div  class="ele_center test-width1">
                           Description</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div  class="ele_center test-width1">
                            <asp:Label ID="lbldiscription" CssClass="lbldiscription" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Description")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div style="" class="ele_center test-date">
                           Test Date</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style=" class="ele_center test-date">
                            <asp:Label ID="lbldate" CssClass="lbldate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Date")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="text-align: center;" class="test-action">
                         
                            <img style="cursor: pointer;" src="../../Images/button_edit.jpg" alt="Edit" class="edit_testandprocedure"
                                cid='<%#DataBinder.Eval(Container.DataItem,"SurgeriesId")%>' />
                                    <img style="cursor: pointer;" src="../../Images/button_close.jpg" alt="Delete" class="delete_conditions" 
                                 onclick="return deletetestNote(<%#DataBinder.Eval(Container.DataItem,"SurgeriesId")%>);" />
                                   </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="lblnorecords" Text="No records found" Font-Bold="true" runat="server"
                    Style="white-space: nowrap; padding: 10px;"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
                     </div>
           

                   <div class="col-lg-12 col-md-12  box-top">
                        <h5 class="pull-left">
                      <%-- <img src="../../Images/Icon-Tests-and-Procedures.png" class="icon-set" alt="" width="20px" height="20px" />--%>
                       <strong>Clinical Documents</strong> <span class="php-arrow"></span></h5>
                      <input type="button" value="Add New" name="add_clicnicaldoc" class="btn_standard php-add" id="add_clicnicaldoc"/>
                    </div>            
          <div class=" box-bottom">
              
        <asp:GridView runat="server" ID="gdclicnicaldoc" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" HeaderStyle-BorderWidth="0" RowStyle-BorderWidth="1px" RowStyle-BorderColor="#D3D3D3">
            <Columns>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div style="width: 130px; font-weight: bold;" class="ele_center" >
                           Document Name</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="text-align:center;" class="ele_center">
                            <asp:Label ID="lblclinicaldoc" CssClass="lblclinicaldoc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FileName")%>'></asp:Label></div>
                        <span class="lblpatientid" style="display: none;">
                            <%#DataBinder.Eval(Container.DataItem,"PatientID")%>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
               
                    <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list" ItemStyle-CssClass="table_data_list">
                    <HeaderTemplate>
                        <div style="width: 130px; font-weight: bold;" class="ele_center ">
                           Date Uploaded</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="text-align:center;" class="ele_center ">
                            <asp:Label ID="lblImmunization" CssClass="lblImmunization" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"UploadedDate")%>'></asp:Label></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="bold_font table_data_list clincal" ItemStyle-CssClass="table_data_list clincal">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div style="text-align:center;" class="">                      
      <img style="cursor: pointer;" src="../../Images/button_edit.jpg" alt="Edit" class="edit_clinicaldoc" mid='<%#DataBinder.Eval(Container.DataItem,"Id")%>' />
      <input type="button" value="Download" name="upload" class="btn_standard js-download" fext='<%#DataBinder.Eval(Container.DataItem,"UploadedFileName")%>'/>
        <%--<a href="../../UserFiles/ClinicalForms/<%#DataBinder.Eval(Container.DataItem,"UploadedFileName")%>" class="btn_standard"></a>                       
 --%>                            </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="Label1" Text="No records found" Font-Bold="true" runat="server" Style="white-space: nowrap;
                    padding: 10px;"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>

              </div>
 

         

           <!-- <div class="phpsumm-form">  -->
                 
                     <div class="col-lg-12 col-md-12  box-top">
                        <h5 class="pull-left">
                       <img src="../../Images/Icon-Vital-signs.png" class="icon-set" alt="" width="20px" height="20px" />
                       <strong>Vital Sign</strong> <span class="php-arrow"></span></h5>
           
                    </div>
                    <div class=" box-bottom vital">
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                    <div class="vlign"></div>
                 </div>

            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                    <div class="">
                        <p class="units">Units</p>
                        <ul class="sum-li">
                            <li>Height</li>
                            <li> <asp:TextBox ID="txt_Height" runat="server" ></asp:TextBox></li>
                            <li>
                                 <asp:DropDownList ID="ddlHeightUnits" runat="server">
                        <asp:ListItem>in</asp:ListItem>
                        <asp:ListItem>cm</asp:ListItem>
                    </asp:DropDownList>
                            </li>
                        </ul>

                        <ul class="sum-li">
                            <li>Temp</li>
                            <li> <asp:TextBox ID="txt_temp" runat="server" ></asp:TextBox></li>
                            <li>
                                <asp:DropDownList ID="ddlTemperatureUnit" runat="server">
                        <asp:ListItem>F&#176;</asp:ListItem>
                        <asp:ListItem>C&#176;</asp:ListItem>
                    </asp:DropDownList>
                            </li>
                        </ul>
                    </div>
                 </div>

         <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                    <div class="">
                        <p class="units">Units</p>
                        <ul class="sum-li">
                            <li>Weight</li>
                            <li><asp:TextBox ID="txt_weight" runat="server" ></asp:TextBox></li>
                            <li>
                                 <asp:DropDownList ID="ddlWeightUnits" runat="server">
                        <asp:ListItem>lbs</asp:ListItem>
                        <asp:ListItem>kg</asp:ListItem>
                    </asp:DropDownList>
                            </li>
                        </ul>

                        <ul class="sum-li">
                            <li>Pulse</li>
                            <li><asp:TextBox ID="txt_pulse" runat="server" ></asp:TextBox></li>
                            <li>
                                BPM
                            </li>
                        </ul>
                    </div>
                 </div>

        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                    <div class="vital-units">
                        <p class="units">Units</p>
                        <ul class="sum-li lst">
                            <li style="width:16px;">BP</li>
                            <li><asp:TextBox ID="txtBP" runat="server" ></asp:TextBox></li>
                            <li><asp:TextBox ID="txtBP2" runat="server" ></asp:TextBox></li>
                            <li style="width:42px;">MM HR</li>
                        </ul>

                        <ul class="sum-li resp">
                            <li style="width: 67px;">Respiration</li>
                            <li><asp:TextBox ID="txtRespiration" runat="server" ></asp:TextBox></li>
                            <li style="width: 20px;">BPM</li>
                        </ul>
                    </div>
                 </div>

                <div align="center"  class="clear">   <asp:Button runat="server" ID="save_vitals" 
                        CssClass="btn_standard" Text="Save" onclick="save_vitals_Click"></asp:Button> 
                </div>
                        </div>

            
            <!--<div class="historic clearfix">-->
               
                     <div class="col-lg-12 col-md-12  box-top">
                        <h5 class="pull-left">
                       <img src="../../Images/notebook.png" class="icon-set" alt="" width="20px" height="20px" />
                       <strong>Vitals - Historic View</strong> <span class="php-arrow"></span></h5>
                 
                    </div>
                    <div class=" box-bottom vit-his">

                 <div class="form-group clearfix">
    <label for="" class="col-sm-3 col-md-3 col-lg-3 control-label">Select Vital</label>
    <div class="col-sm-4 col-md-4 col-lg-4">

        

     <asp:DropDownList ID="ddlSelectVitals" runat="server" style="height:28px;">
                           
                            <asp:ListItem>Height</asp:ListItem>
                            <asp:ListItem>Weight</asp:ListItem>
                            <asp:ListItem>Temperature</asp:ListItem>
                            <asp:ListItem>Pulse</asp:ListItem>
                            <asp:ListItem>Respiration </asp:ListItem>
                            <asp:ListItem>Blood Pressure</asp:ListItem>
                        </asp:DropDownList>

                   
    </div>
  </div>

  <div class="form-group clearfix">
    <label for="inputPassword3" class="col-sm-3 col-md-3 col-lg-3 control-label">Date Range</label>
    <div class="col-sm-4">
       <p class="txt-lft"> <b> From</b> </p>

        <div style="position:relative;">
        <asp:TextBox ID="txtFromdate" runat="server" CssClass="popup_textbox" ClientIDMode="Static" style="margin-left:0;"></asp:TextBox>
               
                    <img src="../../Images/calendar.jpg" id="calendar_img" class="txtbox_spanimg" alt="calendar" style="position: absolute;
    top: 0px;    right: 20px;"/>
            </div>
    </div>
      <div class="col-sm-4 col-md-4 col-lg-4">
         <p class="txt-lft"><b> To</b></p>
      <div style="position:relative;">
           <asp:TextBox ID="txtTodate" runat="server" CssClass="popup_textbox" ClientIDMode="Static" style="margin-left:0;"></asp:TextBox>
               
                    <img src="../../Images/calendar.jpg" id="calendar_imgs" class="txtbox_spanimg" alt="calendar" style="position: absolute;
    top: 0px;    right: 20px;"/>
          </div>
    </div>

     
  </div>
  
                 <div align="center">   
                      <input type="button" id="btn_submitvitals" value="Submit" class="btn_standard" onclick="submitVital()"/> 
                     <%--<Button ID="btn_submitvitals" 
                        CssClass="btn_standard" Text="Submit" onclick="submitVital()" ></Button>
               --%>
                         </div>

                         <div>
                    <div>
                        <asp:Label ID="lblGraphResult" runat="server"></asp:Label>
                     
                        <asp:Button ID="btnBindCondition" runat="server" Text="Update Grid" OnClick="btn_submitvitals_Click" />

         <asp:ScriptManager ID="ScriptManager1" runat="server">
         </asp:ScriptManager> 
       <asp:UpdatePanel ID="UPHistoricView" runat="server" UpdateMode="Conditional">
            <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnBindCondition" EventName="Click" />                  
            </Triggers>
            <ContentTemplate> 


                       <div id="LineGraph" >
                        </div>

                </ContentTemplate>
        </asp:UpdatePanel>

                    </div>
                </div>

            </div>

        <div style="display: none;">

    <div id="divshare" style="z-index: 10000; height: 400px!important;">
        <div class="edit_allergiesdiv" style="float: left; margin: 3px; width: 99%;">
            <div >
                    <div class="popup_header Content" >Share
            </div>
                <div class="cls-popup">
                <img src="../../Images/popup_close.png" id="ClosePopupshare"  />
            </div>
                <iframe id="share_popup" src="" style="overflow: auto;
                    position: fixed; width: 56%; height: 400px; border: none; margin-top: 0px;">
                </iframe>
            </div>
            
        </div>
    </div>

</div>
               
    

          </div>

<%-- open Clinical document --%>

            <div style="display: none;">
    <div id="divshowclicnicaldoc" style="z-index: 10000;">
        <div class="edit_clicnicaldocdiv">
            <div style="float: left;">
                <iframe id="frameclicnicaldocdiv" src="" style="overflow: auto; position: fixed; width: 54%;
                    height: 346px; border: none; margin-top: 40px;"></iframe>
            </div>
            <div class="popup_header Content">
            </div>
            <div style="float: right; margin-right: 10px;">
                <img src="../../Images/popup_close.png" id="popupclose" style="cursor: pointer;
                        margin-right: -5px; margin-top: -3px; z-index:999;" />
            </div>
        </div>
    </div>
</div>

<script>

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
            showpopupclinicaldoc();
        });
        $("#add_clicnicaldoc").click(function () {
            $('.Content').text("Add New");

            $("#frameclicnicaldocdiv").attr("src", "");
            $("#frameclicnicaldocdiv").attr("src", "AddEditClinicalDocuments.aspx?Action=New");
            showpopupclinicaldoc();
        });
    });

    function showpopupclinicaldoc() {
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


    function ClosePopupshares() {
        $("#divshare").dialog('close');
    }

    $("#popup_close").click(function () {
        parent.popup_close();
     
    });


    function ClosePopupresu() {

        $("#divlabresult").dialog('close');

    }
</script>


<%-- close clinical document  --%>



<%-- Open Medication  --%>
<style>    
div.patient_search_right_frame{margin:5px 0;}
.patsearch_heading.patient{width:100%; padding-top:4px;}
.patsearch_heading.patient .btn_standard{margin:0px 50px 0 0!important; padding:5px 10px; float:right;}
.patsearch_heading.patient #popup_close{margin-right:-164px; margin-top: 0!important;}
.ui-dialog .ui-dialog-content{overflow:hidden!important;}

</style>

<input type="hidden" id="deletemedNoteId" value="0" />
        <div style="display: none;">
    <div id="divdelmednote" style="z-index: 10000;">
        <div class="edit_sessionnotediv">
            
             <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' onclick="closemedPopup()" class="pull-right "
                alt="close" />
              <p>Are you sure want to delete this task</p>
                <div class="center-txt">                     
                       <input type="button" id="" name="" value="Yes" class="btn_standard" onclick="MedNoteDelete()"/> 
                        <input type="button" id="" value="No" class="btn_standard" onclick="closemedPopup()"/>
                    </div>
            
            
           
                  
        </div>
    </div>
</div> 

<!-- Add New -->
<div style="display: none;">
    <div id="divshowmedications" class="z-ind">
        <div class="edit_medicationdiv">
            <div class="pull-left">
                <iframe id="framemedications" src="" class="framemedications"></iframe>
            </div>
            <div class="popup_header Content">
            </div>
            <div class="cls-popup">
                <img src="../../Images/popup_close.png" id="ClosePopupmedi"  />
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">

    $(document).ready(function () {

        $("#ClosePopupmedi").click(function () {

            $("#divshowmedications").dialog('close');
        });


        $("#ClosePopupshare").click(function () {

            $("#divshare").dialog('close');
        });

        

          $("#popupclosemedications").click(function () {
              parent.window.location.href = parent.window.location.href;
          });

          $(".delete_medications").click(function () {
              if (confirm('Are you sure you want to Delete?')) {
              $('.Content').text("Delete");
              var uid = $(this).attr("cid");

              var data = "{ MedicationID: '" + uid + "'}";

              $.ajax({
                  type: "POST",
                  url: "Medications.aspx/DeleteMedications",
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
              
          });

          $(".edit_medications").click(function () {
              $('.Content').text("Edit");

              var uid = $(this).attr("cid");
              var _dosage = $(this).closest("tr").find("[class~=lbldos]").html();
              var _desc = $(this).closest("tr").find("[class~=lbldesc]").html();
              var _dos = $(this).closest("tr").find("[class~=lbldate]").html();
              var _dosageunits = $(this).closest("tr").find("[class~=lbldosunits]").html();

              $.ajax({
                  type: "POST",
                  url: "Medications.aspx/CacheDetails",
                  data: '{"description":"' + _desc + '","dosage":"' + _dosage + '","activesince":"' + _dos + '","dosageunits":"' + _dosageunits + '"}',
                  contentType: "application/json; charset=utf-8",
                  success: function (msg) {

                  },
                  error: function (a) {

                  },
                  complete: function () {
                      $("#framemedications").attr("src", "");
                      $("#framemedications").attr("src", "AddEditMedications.aspx?cid=" + uid);

                      showpopupmedications();
                  }
              });
          });
          $("#add_medications").click(function () {
              $('.Content').text("Add New");

              $("#framemedications").attr("src", "");
              $("#framemedications").attr("src", "AddEditMedications.aspx");
              showpopupmedications();
          });
      });

      function showpopupmedications() {
          var popuphight = window.innerHeight - 150;
          var popupwidth = window.innerWidth - 490;

          $("#divshowmedications").dialog(
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
      function ClosePopupmedi() {

          $("#divshowmedications").dialog('close');

      }



    function MedNoteDelete() {
        var uid = $('#deletemedNoteId').val();
            $('.Content').text("Delete");
            //var uid = $(this).attr("cid");
          
            var data = "{ MedicationID: '" + uid + "'}";

            $.ajax({
                type: "POST",
                url: "Medications.aspx/DeleteMedications",
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
        function deletemedNote(id) {
            
        $('#deletemedNoteId').val(id);

            var popuphight = window.innerHeight - 150;
            var popupwidth = window.innerWidth - 490;

            $("#divdelmednote").dialog(
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
    function closemedPopup() {
        $("#divdelmednote").dialog('close');
    }




</script>
<%-- Close Medication  --%>

                <%--Open Condition  --%>

<input type="hidden" id="deleteconNoteId" value="0" />
        <div style="display: none;">
    <div id="divdelconnote" style="z-index: 10000;">
        <div class="edit_sessionnotediv">
            
             <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' onclick="closedeletePopup()" class="pull-right "
                alt="close" />
              <p>Are you sure want to delete this task</p>
                <div class="center-txt">                     
                       <input type="button" id="" name="" value="Yes" class="btn_standard" onclick="ConNoteDelete()"/> 
                        <input type="button" id="" value="No" class="btn_standard" onclick="closedeletePopup()"/>
                    </div>
            
            
           
                  
        </div>
    </div>
</div> 

<!-- Add New -->
                <div style="display: none;">
    <div id="divshowconditions" class="z-ind">
        <div class="edit_conditionsdiv">
            <div  class="pull-left">
                <iframe id="frmeditconditions_popup" src="" class="frmeditconditions_popup"></iframe>
            </div>
            <div class="popup_header Content">
            </div>
            <div class="cls-popup">
                              <img src="../../Images/popup_close.png" id="popupclosecondition"  /> 
            </div>
        </div>
    </div>
</div>
                <script type="text/javascript">

    $(document).ready(function () {
        $("#popupclosecondition").click(function () {
            $("#divshowconditions").dialog('close');
        });

        

        $(".edit_conditions").click(function () {
            $('.Content').text("Edit");

            var uid = $(this).attr("cid");
            var _desc = $(this).closest("tr").find("[class~=lbldesc1]").html();
            var _icd9 = $(this).closest("tr").find("[class~=lblicd9code1]").html();
            var _dos = $(this).closest("tr").find("[class~=lbldos1]").html();
            var _conditioncheck = $(this).closest("tr").find("[class~=spnconditioncheck1]").html();
            var _history = $(this).closest("tr").find("[class~=spnhistory1]").html();

            $.ajax({
                type: "POST",
                url: "Conditions.aspx/CacheDetails",
                data: '{"description":"' + _desc + '","icd9code":"' + _icd9 + '","activesince":"' + _dos + '","conditioncheck":"' + _conditioncheck + '","inactivesince":"' + _history + '"}',
                contentType: "application/json; charset=utf-8",
                success: function (msg) {

                },
                error: function (a) {

                },
                complete: function () {
                    $("#frmeditconditions_popup").attr("src", "");
                    $("#frmeditconditions_popup").attr("src", "EditConditions.aspx?cid=" + uid);
                    showpopup();
                }
            });
        });
        $("#add_conditions").click(function () {
            $('.Content').text("Add New");

            // $('<span>Add New</span>').appendTo('.Content');

            // $('<span>Add New</span>').appendTo('.Content');

            $(".edit_conditionsdiv").css("display", "block");

            $("#frmeditconditions_popup").attr("src", "");
            $("#frmeditconditions_popup").attr("src", "EditConditions.aspx");
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
                //title: "Patient Search",
                create: function () {
                    $(".ui-dialog-titlebar").hide();
                    $(".ui-dialog-content").css("padding", "0px");
                }
            });
    }

    function popupclosecondition() {

        $("#divshowconditions").dialog('close');

    }


    function ConNoteDelete() {
        var uid = $('#deleteconNoteId').val();
            $('.Content').text("Delete");
            //var uid = $(this).attr("cid");
          
             var data = "{ ConditionID: '" + uid + "'}";

                $.ajax({
                    type: "POST",
                    url: "Conditions.aspx/DeleteConditions",
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
        function deleteconNote(id) {
            
        $('#deleteconNoteId').val(id);

            var popuphight = window.innerHeight - 150;
            var popupwidth = window.innerWidth - 490;

            $("#divdelconnote").dialog(
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
    function closedeletePopup() {
        $("#divdelconnote").dialog('close');
    }


</script>
                <%--Close Condition  --%>

<%-- Open Family History  --%>


<input type="hidden" id="deletefamNoteId" value="0" />
        <div style="display: none;">
    <div id="divdelfamnote" style="z-index: 10000;">
        <div class="edit_sessionnotediv">
            
             <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' onclick="closefamPopup()" class="pull-right "
                alt="close" />
              <p>Are you sure want to delete this task</p>
                <div class="center-txt">                     
                       <input type="button" id="" name="" value="Yes" class="btn_standard" onclick="FamNoteDelete()"/> 
                        <input type="button" id="" value="No" class="btn_standard" onclick="closefamPopup()"/>
                    </div>
                  
        </div>
    </div>
</div> 

<div style="display: none;">
    <div id="divshowfamily" class="z-ind">
        <div class="edit_familydiv">
            <div class="pull-left">
                <asp:HiddenField runat="server" ID="currentuserId" />
                <iframe id="frmeditfamily_popup" src="" class="frmeditfamily_popup"></iframe>
            </div>
            <div class="popup_header Content">
            </div>
            <div class="cls-popup">
              <img src="../../Images/popup_close.png" id="popupclosefamily"  />
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">

    $(document).ready(function () {

        $("#popupclosefamily").click(function () {
            $("#divshowfamily").dialog('close');
        });


        $(".delete_familyhistory").click(function () {
            if (confirm('Are you sure you want to Delete?')) {
                $('.Content').text("Delete");
                var uid = $(this).attr("cid");

                var data = "{ PatientFamilyHistoryID: '" + uid + "'}";

                $.ajax({
                    type: "POST",
                    url: "FamilyHistory.aspx/DeleteFamilyHistory",
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
        });

        $(".edit_familyhistory").click(function () {
            $('.Content').text("Edit");

            var uid = $(this).attr("cid");
           
            var _relationship = $(this).closest("tr").find("[class~=lblRelationship]").html();
            var _condition = $(this).closest("tr").find("[class~=lblCondition]").html();
            var _date = $(this).closest("tr").find("[class~=lbldate]").html();


            $.ajax({
                type: "POST",
                url: "FamilyHistory.aspx/CacheDetails",
                data: '{"Relationship":"' + _relationship + '","ConditionName":"' + _condition + '","OnsetDate":"' + _date + '"}',
                contentType: "application/json; charset=utf-8",
                success: function (msg) {

                },
                error: function (a) {

                },
                complete: function () {
                    $("#frmeditfamily_popup").attr("src", "");
                    $("#frmeditfamily_popup").attr("src", "AddEditFamilyHistory.aspx?cid=" + uid);
                    showpopupfamily();
                }
            });
        });
        $("#add_family").click(function () {
            $('.Content').text("Add New");

            // $('<span>Add New</span>').appendTo('.Content');

            // $('<span>Add New</span>').appendTo('.Content');

            $(".edit_familydiv").css("display", "block");

            $("#frmeditfamily_popup").attr("src", "");
            $("#frmeditfamily_popup").attr("src", "AddEditFamilyHistory.aspx");
            showpopupfamily();
        });
    });

    function showpopupfamily() {
        var popuphight = window.innerHeight - 200;
        var popupwidth = window.innerWidth - 490;

        $("#divshowfamily").dialog(
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

    function popupclosefamily() {

        $("#divshowfamily").dialog('close');

    }




    function FamNoteDelete() {
        var uid = $('#deletefamNoteId').val();
            $('.Content').text("Delete");
            //var uid = $(this).attr("cid");
          
           var data = "{ PatientFamilyHistoryID: '" + uid + "'}";

                $.ajax({
                    type: "POST",
                    url: "FamilyHistory.aspx/DeleteFamilyHistory",
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
        function deletefamNote(id) {
            
        $('#deletefamNoteId').val(id);

            var popuphight = window.innerHeight - 150;
            var popupwidth = window.innerWidth - 490;

            $("#divdelfamnote").dialog(
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
    
function closefamPopup() {

        $("#divdelfamnote").dialog('close');

    }

</script>
 <%-- Close Family History --%>

                <%-- Open Immunization --%>

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


                 <div style="display: none;">
    <div id="divshowimmunization" class="z-ind">
        <div class="edit_immunizationdiv">
            <div style="float: left;">
                <iframe id="frameimmunizationdiv" src="" class="frameimmunizationdiv"></iframe>
            </div>
            <div class="popup_header Content">
            </div>
            <div class="cls-popup">
              <img src="../../Images/popup_close.png" id="popupcloseimmunization" />
            </div>
        </div>
    </div>
</div>             
                 <script type="text/javascript">

    $(document).ready(function () {

        $("#popupcloseimmunization").click(function () {
            $("#divshowimmunization").dialog('close');
        });
        function popupcloseimmunization() {

            $("#divshowimmunization").dialog('close');

        }
       

        $(".edit_Immunization").click(function () {
            $('.Content').text("Edit");

            var uid = $(this).attr("cid");
            var _date = $(this).closest("tr").find("[class~=lbldate]").html();
            var _immunization = $(this).closest("tr").find("[class~=lblImmunization]").html();


            $.ajax({
                type: "POST",
                url: "Immunizations.aspx/CacheDetails",
                data: '{"Immunization":"' + _immunization + '","Date":"' + _date + '"}',
                contentType: "application/json; charset=utf-8",
                success: function (msg) {

                },
                error: function (a) {

                },
                complete: function () {
                    $("#frameimmunizationdiv").attr("src", "");
                    $("#frameimmunizationdiv").attr("src", "EditImmunizations.aspx?cid=" + uid);

                    showpopupimmunization();
                }
            });
        });
        $("#add_immunizations").click(function () {
            $('.Content').text("Add New");

            $("#frameimmunizationdiv").attr("src", "");
            $("#frameimmunizationdiv").attr("src", "EditImmunizations.aspx");
            showpopupimmunization();
        });
    });

    function showpopupimmunization() {
        var popuphight = window.innerHeight - 150;
        var popupwidth = window.innerWidth - 490;

        $("#divshowimmunization").dialog(
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



    function ImmNoteDelete() {
        var uid = $('#deleteimmNoteId').val();
            $('.Content').text("Delete");
            //var uid = $(this).attr("cid");
          
             var data = "{ ImmunizationID: '" + uid + "'}";

                $.ajax({
                    type: "POST",
                    url: "Immunizations.aspx/DeleteImmunizations",
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
                <%-- Close immunization  --%>

<%-- Open Allergies  --%>




<input type="hidden" id="deletealgNoteId" value="0" />
        <div style="display: none;">
    <div id="divdelalgnote" style="z-index: 10000;">
        <div class="edit_sessionnotediv">
            
             <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' onclick="closealgPopup()" class="pull-right "
                alt="close" />
              <p>Are you sure want to delete this task</p>
                <div class="center-txt">                     
                       <input type="button" id="" name="" value="Yes" class="btn_standard" onclick="AglNoteDelete()"/> 
                        <input type="button" id="" value="No" class="btn_standard" onclick="closealgPopup()"/>
                    </div>
            
            
           
                  
        </div>
    </div>
</div> 



<div style="display: none;">
    <div id="divshowallergies" class="z-ind">
        <div class="edit_allergiesdiv" >
            <div class="pull-left">
                <iframe id="allergies_popup" src="" class="allergies_popup">
                </iframe>
            </div>
            <div class="popup_header Content">
            </div>
            <div class="cls-popup">
                <img src="../../Images/popup_close.png" id="popupcloseallergies" />
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    $(document).ready(function () {


        $("#popupcloseallergies").click(function () {
            $("#divshowallergies").dialog('close');
        });

        
        $(".edit_allergies").click(function () {
            $('.Content').text("Edit");

            var uid = $(this).attr("cid");
            var _reaction = $(this).closest("tr").find("[class~=lblreactions]").html();
            var _allergy = $(this).closest("tr").find("[class~=lblallergytype]").html();
            var _dos = $(this).closest("tr").find("[class~=lbldos]").html();
            var _ismedicalallergy = $(this).closest("tr").find("[class~=spnismedallergy]").html();
            var _treatment = $(this).closest("tr").find("[class~=spntreatment]").html();


            $.ajax({
                type: "POST",
                url: "Allergies.aspx/CacheDetails",
                data: '{"Reaction":"' + _reaction + '","Allergy":"' + _allergy + '","activesince":"' + _dos + '","IsMedicationAllergy":"' + _ismedicalallergy + '","Treatment":"' + _treatment + '"}',
                contentType: "application/json; charset=utf-8",
                success: function (msg) {

                },
                error: function (a) {

                },
                complete: function () {
                    $("#allergies_popup").attr("src", "");
                    $("#allergies_popup").attr("src", "AddEditAllergies.aspx?cid=" + uid);

                    showpopupallergies();
                }
            });
        });
        $("#add_allergies").click(function () {
            $('.Content').text("Add New");
            $("#allergies_popup").attr("src", "");
            $("#allergies_popup").attr("src", "AddEditAllergies.aspx");
            showpopupallergies();
        });
    });

    function showpopupallergies() {
        var popuphight = window.innerHeight - 225;
        var popupwidth = window.innerWidth - 490;

        $("#divshowallergies").dialog(
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

    function ClosePopupallergies() {
        $("#divshowallergies").dialog('close');

    }



    function AglNoteDelete() {
        var uid = $('#deletealgNoteId').val();
            $('.Content').text("Delete");
            //var uid = $(this).attr("cid");
          
            var data = "{ AllergyID: '" + uid + "'}";

            $.ajax({
                    type: "POST",
                    url: "Allergies.aspx/DeleteAllergies",
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
        function deletealgNote(id) {
            
        $('#deletealgNoteId').val(id);

            var popuphight = window.innerHeight - 150;
            var popupwidth = window.innerWidth - 490;

            $("#divdelalgnote").dialog(
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
    function closealgPopup() {
        $("#divdelalgnote").dialog('close');
    }


</script>
<%-- Close Allergies  --%>
                  
                     <%-- Open Encounter  --%>


<input type="hidden" id="deleteencNoteId" value="0" />
        <div style="display: none;">
    <div id="divdelencnote" style="z-index: 10000;">
        <div class="edit_sessionnotediv">
            
             <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' onclick="closeencPopup()" class="pull-right "
                alt="close" />
              <p>Are you sure want to delete this task</p>
                <div class="center-txt">                     
                       <input type="button" id="" name="" value="Yes" class="btn_standard" onclick="EncNoteDelete()"/> 
                        <input type="button" id="" value="No" class="btn_standard" onclick="closeencPopup()"/>
                    </div>
            
            
           
                  
        </div>
    </div>
</div> 


                <div style="display: none;">
    <div id="divshowencounter" class="z-ind">
        <div class="edit_encounterdiv">
            <div class="pull-left">
                <iframe id="frmeditencounter_popup" src="" class="frmeditencounter_popup" ></iframe>
            </div>
            <div class="popup_header Content">
            </div>
            <div  class="cls-popup">
                <img src="../../Images/popup_close.png" id="popupcloseencounter" />
            </div>
        </div>
    </div>
</div>
                <script type="text/javascript">

                    $(document).ready(function () {

                        $("#popupcloseencounter").click(function () {

                            $("#divshowencounter").dialog('close');
                        });

                       

                        $(".edit_encounter").click(function () {
                            $('.Content').text("Edit");

                            var uid = $(this).attr("cid");

                            var _visitdate = $(this).closest("tr").find("[class~=lblvisitdate]").html();
                            var _doctor = $(this).closest("tr").find("[class~=lbldoctor]").html();
                            var _reason = $(this).closest("tr").find("[class~=lblvisitreason]").html();
                            var _diagnosis = $(this).closest("tr").find("[class~=lbldiagnosis]").html();


                            $.ajax({
                                type: "POST",
                                url: "Encounters.aspx/CacheDetails",
                                data: '{"VisitDate":"' + _visitdate + '","DoctorName":"' + _doctor + '","VisitReason":"' + _reason + '","VisitDiagnosis":"' + _diagnosis + '"}',
                                contentType: "application/json; charset=utf-8",
                                success: function (msg) {

                                },
                                error: function (a) {

                                },
                                complete: function () {
                                    $("#frmeditencounter_popup").attr("src", "");
                                    $("#frmeditencounter_popup").attr("src", "EditEncounters.aspx?cid=" + uid);
                                    showpopupencounter();
                                }
                            });
                        });
                        $("#add_encounter").click(function () {
                            $('.Content').text("Add New");


                            $(".edit_encounterdiv").css("display", "block");

                            $("#frmeditencounter_popup").attr("src", "");
                            $("#frmeditencounter_popup").attr("src", "EditEncounters.aspx");
                            showpopupencounter();
                        });
                    });

                    function showpopupencounter() {
                        var popuphight = window.innerHeight - 200;
                        var popupwidth = window.innerWidth - 490;

                        $("#divshowencounter").dialog(
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
                    function closeencPopup() {
                        $("#divdelencnote").dialog('close');
                    }


    function EncNoteDelete() {
        var uid = $('#deleteencNoteId').val();
            $('.Content').text("Delete");
            //var uid = $(this).attr("cid");
          
            var data = "{ EncounterID: '" + uid + "'}";

                $.ajax({
                    type: "POST",
                    url: "Encounters.aspx/DeleteEncounter",
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
        function deleteencNote(id) {
            
        $('#deleteencNoteId').val(id);

            var popuphight = window.innerHeight - 150;
            var popupwidth = window.innerWidth - 490;

            $("#divdelencnote").dialog(
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
    function closeencPopup() {
        $("#divshowencounter").dialog('close');
    }



</script>
                <%-- close Encounter  --%>
<%-- Open Result --%>
<div style="display: none;">
    <div id="divlabresult" style="z-index: 10000;">
        <div class="edit_allergiesdiv" style="float: left; margin: 3px; width: 99%;">
            <div style="float: left;">
                <iframe id="labresult_popup" src="../ASPX/AddEditLabResults.aspx" style="overflow: auto;
                    position: fixed; width: 54%; height: 346px; border: none; margin-top: 40px;">
                </iframe>
            </div>
            <div class="popup_header Content">
            </div>
            <div style="float: right; margin-right: 10px;">
                <img src="../../Images/popup_close.png" id="popupcloselabresult" style="cursor: pointer; margin-top: -33px;
                    margin-right: 0px;" />
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    $(document).ready(function () {


        $("#popupcloselabresult").click(function () {
            $("#divlabresult").dialog('close');
        });

        $(".delete_labresult").click(function () {

            $('.Content').text("Delete");
            var uid = $(this).attr("cid");

            var data = "{ LabResultID: '" + uid + "'}";

            $.ajax({
                type: "POST",
                url: "LabResults.aspx/DeleteProcedure",
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
        });
        $(".edit_labresult").click(function () {
            $('.Content').text("Edit");

            var uid = $(this).attr("cid");
            var _Date = $(this).closest("tr").find("[class~=lbldate]").html();
            var _TestType = $(this).closest("tr").find("[class~=lblTestType]").html();
            var _Results = $(this).closest("tr").find("[class~=lblresult]").html();
            var _Doctor = $(this).closest("tr").find("[class~=lbldoctor]").html();
            var _Adminby = $(this).closest("tr").find("[class~=lbladmin]").html();
            $.ajax({
                type: "POST",
                url: "LabResults.aspx/CacheDetails",
                data: '{"Date":"' + _Date + '","TestType":"' + _TestType + '","Results":"' + _Results + '","Doctor":"' + _Doctor + '","Adminby":"' + _Adminby + '"}',
                contentType: "application/json; charset=utf-8",
                success: function (msg) {

                },
                error: function (a) {

                },
                complete: function () {
                    $("#labresult_popup").attr("src", "");
                    $("#labresult_popup").attr("src", "AddEditLabResults.aspx?cid=" + uid);

                    showpopuplabresult();
                }
            });
        });
        $("#add_labresult").click(function () {
            $('.Content').text("Add New");
            showpopuplabresult();
        });
    });

    function showpopuplabresult() {
        var popuphight = window.innerHeight - 225;
        var popupwidth = window.innerWidth - 490;

        $("#divlabresult").dialog(
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

    function ClosePopuptestandprocedure() {

        $("#divshowlabresult").dialog('close');

    }
    $("#testandprocedurepopup_close").click(function () {
        parent.popup_close();
    });
</script>

                <%-- Close Results --%>

                <%-- Open Test and Procedures --%>


<input type="hidden" id="deletetestNoteId" value="0" />
        <div style="display: none;">
    <div id="divdeltestnote" style="z-index: 10000;">
        <div class="edit_sessionnotediv">
            
             <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' onclick="closetestPopup()" class="pull-right "
                alt="close" />
              <p>Are you sure want to delete this task</p>
                <div class="center-txt">                     
                       <input type="button" id="" name="" value="Yes" class="btn_standard" onclick="TestNoteDelete()"/> 
                        <input type="button" id="" value="No" class="btn_standard" onclick="closetestPopup()"/>
                    </div>
            
            
           
                  
        </div>
    </div>
</div> 

                <div style="display: none;">
    <div id="divtestandprocedure" style="z-index: 10000;">
        <div class="edit_allergiesdiv" style="float: left; margin: 3px; width: 99%;">
            <div style="float: left;">
                <iframe id="testandprocedure_popup" src="../ASPX/AddEditTestAndProcedures.aspx" style="overflow: auto;
                    position: fixed; width: 54%; height: 346px; border: none; margin-top: 40px;">
                </iframe>
            </div>
            <div class="popup_header Content">
            </div>
            <div class="cls-popup">
                <img src="../../Images/popup_close.png" id="popupclosetestandprocedure"  />
            </div>
        </div>
    </div>
</div>
                
<script type="text/javascript">
    $(document).ready(function () {

        

        
        $(".edit_testandprocedure").click(function () {
            $('.Content').text("Edit");

            var uid = $(this).attr("cid");
            var _lblprocedure = $(this).closest("tr").find("[class~=lblprocedure]").html();
            var _lbldiscription = $(this).closest("tr").find("[class~=lbldiscription]").html();
            var _lbldate = $(this).closest("tr").find("[class~=lbldate]").html();

            $.ajax({
                type: "POST",
                url: "TestAndProcedures.aspx/CacheDetails",
                data: '{"Procedure":"' + _lblprocedure + '","Discription":"' + _lbldiscription + '","Date":"' + _lbldate + '"}',
                contentType: "application/json; charset=utf-8",
                success: function (msg) {

                },
                error: function (a) {

                },
                complete: function () {
                    $("#testandprocedure_popup").attr("src", "");
                    $("#testandprocedure_popup").attr("src", "AddEditTestAndProcedures.aspx?cid=" + uid);

                    showpopuptestandprocedure();
                }
            });
        });
        $("#add_testandprocedure").click(function () {
            $('.Content').text("Add New");
            showpopuptestandprocedure();
        });
    });

    function showpopuptestandprocedure() {
        var popuphight = window.innerHeight - 225;
        var popupwidth = window.innerWidth - 490;

        $("#divtestandprocedure").dialog(
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

    $("#popupclosetestandprocedure").click(function () {
        $("#divtestandprocedure").dialog('close');
    });

    function ClosePopuptestandprocedure() {

        $("#divtestandprocedure").dialog('close');

    }

    $("#testandprocedurepopup_close").click(function () {
        parent.popup_close();
    });



    function TestNoteDelete() {
        var uid = $('#deletetestNoteId').val();
            $('.Content').text("Delete");
            //var uid = $(this).attr("cid");
          
           var data = "{ ProcedureID: '" + uid + "'}";

            $.ajax({
                type: "POST",
                url: "TestAndProcedures.aspx/DeleteProcedure",
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
        function deletetestNote(id) {
            
        $('#deletetestNoteId').val(id);

            var popuphight = window.innerHeight - 150;
            var popupwidth = window.innerWidth - 490;

            $("#divdeltestnote").dialog(
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
    function closetestPopup() {
        $("#divdeltestnote").dialog('close');
    }


</script>
                <%-- Cloase Test and Procedures --%>




    <script type="text/javascript">

        $("#div_patientsearch").css("display", "block");

        $("#patientsearch_leftpart").css("display", "block");
        $("#imgsearchuser").css("display", "block");


        $("#popup_close").click(function () {
            parent.popup_close();
        });
    </script>
                             <script type="text/javascript">
                                 $(document).ready(function () {
                                     $('#txtFromdate').datepicker({ maxDate: new Date() });
                                     $('#calendar_img').click(function () {
                                         $('#txtFromdate').datepicker('show');
                                     });

                                     $('#txtTodate').datepicker({ maxDate: new Date() });
                                     $('#calendar_imgs').click(function () {
                                         $('#txtTodate').datepicker('show');
                                     });
                                 });
    </script>
    <script type="text/javascript">

                 $(document).ready(function () {
                     $('#dob').datepicker({ maxDate: new Date() });
                     $('#calendar_img').click(function () {
                         $('#dob').datepicker('show');
                     });
                     $("#popup_close").click(function () {
                         parent.popup_close();
                     });
                     
                     //ACCORDION SECTION

                     $("div.box-top").addClass("accord-Notact");

                     //ACCORDION BUTTON ACTION
                     $('div.box-top').click(function () {

                         $(this).siblings('.accord-act')
                             .removeClass('accord-act')
                             .addClass('accord-Notact');

                         if ($(this).next().is(':visible')) {
                             $('div.box-bottom').slideUp('normal');
                             $(this).addClass("accord-Notact").removeClass("accord-act");

                         } else {
                             $('div.box-bottom').slideUp('normal');
                             $(this).next().slideDown('normal');
                             $(this).removeClass("accord-Notact ").addClass("accord-act");

                         }
                     }).children('.php-add').click(function (e) {
                         return false;
                     });

                 });

                
    </script>
        <script type="text/javascript">
            function bind_barchart() {
                vitalType = $('#<%=ddlSelectVitals.ClientID%>').val().toString();
                if (vitalType == 'Height')
                    vitalType = 'Height (in.)'
                else if (vitalType == 'Weight')
                    vitalType = 'Weight (lbs.)'
                else if (vitalType == 'Temperature')
                    vitalType = 'Temperature (F)'
                else if (vitalType == 'Pulse')
                    vitalType = 'Pulse BPM'
                else if (vitalType == 'Respiration')
                    vitalType = 'Respiration BPM'
                else if (vitalType == 'Blood Pressure')
                    vitalType = 'Blood Pressure mmHG'
                $.barchart();
                xyz();
            }
            function xyz() {
                options1 = {



                    chart: {
                        renderTo: 'LineGraph',
                        type: 'line'
                        //zoomType: 'y'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: ''
                    },
                    xAxis: {
                        min: 0,
                        max: 9,
                        categories: [],
                        labels: {
                            rotation: 0,
                            y: 30,
                            x: 10,
                            enabled: false
                        },
                        title: {

                            text: ''
                        }
                    },
                    legend: {
                        //                layout: 'horizontal',
                        backgroundColor: '#FFFFFF',
                        align: 'right',
                        verticalAlign: 'top',
                        //                x: 10,
                        //                y: 10,
                        floating: true,
                        shadow: false,
                        enabled: true
                    },
                    yAxis: {
                        min: 0,
                        title:
                {

                    text: vitalType

                }
                    },

                    tooltip: {
                        formatter: function () {
                            return '' + vitalType + ': ' + this.y + '<br />Date :' + this.x;
                        }
                    },
                    scrollbar:
            {
                enabled: true
            },

                    plotOptions: {
                        column: {
                            stacking: 'normal',
                            pointPadding: 0.2,
                            borderWidth: 4,
                            minPointLength: 0,
                            shadow: false
                            //color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white'
                        },
                        series: {
                            animation: ({
                                duration: 2000,
                                easing: 'swing',
                                complete: function () {

                                }
                            })
                        }
                    },
                    credits: {
                        text: '',
                        href: ''
                    },
                    series: [],
                    exporting: {
                        enabled: true
                    }
                }

            }
            $.barchart = function () {

                $.ajax({
                    type: "POST",
                    url: "PhpSummary.aspx/BindGraphs",
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,

                    success: function (msg) {

                        //$("[id*=btnSubmit1]").click();
                        var element = msg.d.split("|");





                        var xAxisSeries = new Array();
                        options1.xAxis.categories = new Array();
                        options1.series = new Array();


                        if (msg.d.length > 0) {

                            var xaxis = element[0].split(",");
                            for (var i = 0; i < xaxis.length; i++) {

                                options1.xAxis.categories.push((xaxis[i]));
                            }


                            var addseries = false;
                            var columnSeries = {};
                            columnSeries.name = (vitalType);
                            columnSeries.data = new Array();
                            var unknown = element[1].split(",");
                            for (var i = 0; i < unknown.length; i++) {
                                if (parseInt(unknown[i]) != 0)
                                    addseries = true;
                                columnSeries.data.push(parseFloat((unknown[i])));
                            }

                            if (addseries == true)
                                options1.series.push(columnSeries);






                        }

                        chart1 = new Highcharts.Chart(options1);


                    },
                    error: function (xmlhttprequest, textstatus, errorThrown) {
                        //$("#divLoading").css('display', 'none');

                        window.parent.ShowError(request.responseText);
                    }
                });
            }

        </script>
    <script type="text/javascript">

        var chart2;
        var options2;

        function bind_linechartforbp() {

            vitalType = $('#<%=ddlSelectVitals.ClientID%>').val().toString();

            $.barchart1();
            xyz1();

        }

        function xyz1() {
            options2 = {



                chart: {
                    renderTo: 'LineGraph',
                    type: 'line',
                    zoomType: 'y'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: ''
                },
                xAxis: {
                    min: 0,
                    max: 9,
                    categories: [],
                    labels: {
                        rotation: 0,
                        y: 30,
                        x: 10,
                        enabled: false
                    },
                    title: {

                        text: 'Date'
                    }
                },
                yAxis: {
                    min: 0,
                    title:
                {

                    text: vitalType

                }
                },
                legend: {

                    backgroundColor: '#FFFFFF',
                    align: 'right',
                    verticalAlign: 'top',

                    floating: true,
                    shadow: false,
                    enabled: true
                },
                tooltip: {
                    //                  shared:true,
                    formatter: function () {


                        return '' + vitalType + ': ' + this.y + '<br />Date ' + this.x;
                    }
                },

                scrollbar:
            {
                enabled: true
            },



                plotOptions: {
                    column: {
                        stacking: 'normal',
                        pointPadding: 0.2,
                        borderWidth: 4,
                        minPointLength: 0,
                        shadow: false
                        //color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white'
                    },
                    series: {
                        animation: ({
                            duration: 2000,
                            easing: 'swing',
                            complete: function () {

                            }
                        })
                    }
                },
                credits: {
                    text: '',
                    href: ''
                },
                series: [],
                exporting: {
                    enabled: true
                }
            }

        }

        $.barchart1 = function () {

            $.ajax({
                type: "POST",
                url: "PhpSummary.aspx/BindGraphs",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,

                success: function (msg) {

                    //$("[id*=btnSubmit1]").click();
                    var element = msg.d.split("|");

                    var xAxisSeries = new Array();

                    options2.xAxis.categories = new Array();
                    options2.series = new Array();


                    if (msg.d.length > 0) {

                        var xaxis = element[0].split(",");
                        for (var i = 0; i < xaxis.length; i++) {

                            options2.xAxis.categories.push((xaxis[i]));
                        }

                        var addseries = false;
                        var columnSeries = {};
                        columnSeries.name = ('BP Systolic');
                        columnSeries.data = new Array();
                        columnSeries.color = ('#AA4643');
                        var unknown1 = element[1].split(",");
                        for (var i = 0; i < unknown1.length; i++) {
                            if (parseInt(unknown1[i]) != 0)
                                addseries = true;
                            columnSeries.data.push(parseFloat((unknown1[i])));
                        }

                        if (addseries == true)
                            options2.series.push(columnSeries);

                        var addseries = false;
                        var columnSeries = {};
                        columnSeries.name = ('BP Diastolic');
                        columnSeries.data = new Array();
                        columnSeries.color = ('#6085B1');
                        var unknown = element[2].split(",");
                        for (var i = 0; i < unknown.length; i++) {
                            if (parseInt(unknown[i]) != 0)
                                addseries = true;
                            columnSeries.data.push(parseFloat((unknown[i])));
                        }

                        if (addseries == true)
                            options2.series.push(columnSeries);



                    }

                    chart2 = new Highcharts.Chart(options2);


                },
                error: function (xmlhttprequest, textstatus, errorThrown) {
                    //$("#divLoading").css('display', 'none');

                    window.parent.ShowError(request.responseText);
                }
            });
        }

        function showsharepopupdiv() {
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

        function ShowSharePopup(){
      
            var userid = $("#cplPatientLookUp_Patientphpsummery_currentuserId").val();
     
                $("#share_popup").attr("src", "");
                $("#share_popup").attr("src", "ShareMail.aspx?id=" + userid);
                showsharepopupdiv();


        }

        function Cancelbtn_Condition() {

            $("#divshowconditions").dialog('close');

        }

        $(function HideButton() {
           document.getElementById("<%= btnBindCondition.ClientID %>").style.display = "none";
       });

        $(function () {
           $(document).on('Condition', function () {
               document.getElementById('<%= btnBindCondition.ClientID %>').click();
               bind_barchart();
           });
        });

        function submitVital() {

            document.getElementById('<%= btnBindCondition.ClientID %>').click();
               bind_barchart();

        }
    </script>
</body>   
</html>