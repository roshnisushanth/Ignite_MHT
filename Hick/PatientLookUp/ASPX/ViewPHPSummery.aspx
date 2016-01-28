<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewPHPSummery.aspx.cs" Inherits="Hick.PatientLookUp.ASPX.ViewPHPSummery" %>

<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en"><head><meta http-equiv="Content-Type" content="text/html; charset=UTF-8"><title>
	PHP
</title>
    <style type="text/css">
        .divspace
        {
            padding-bottom: 30px;
        }
        body
        {
            margin: 0px;
        }
        
        #wrapper
        {
            width: 830px;
            height: auto;
            padding: 20px;
            margin: 0 auto;
        }
        
        .headings
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 18px;
            color: #000;
            font-weight: bold;
            padding-top: 5px;
        }
        .SubHead
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 16px;
            color: #000;
            padding-top: 5px;
            line-height: 19px;
        }
        .span
        {
            font-size: 11px;
        }
        .allergies
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 18px;
            color: #000;
            font-weight: bold;
            padding-top: 10px;
            padding-bottom: 5px;
            border-bottom: 2px solid #000;
        }
        .allergies1
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 13px;
            color: #000;
            font-weight: normal;
            padding-top: 10px;
            padding-bottom: 5px;
        }
        .tablehead
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 14px;
            color: #000;
            text-align: left;
            padding-top: 2px;
            height: 28px;
        }
        .tablehead th
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 14px;
            color: #000;
            font-weight: bold;
            text-align: left;
            padding-top: 2px;
            height: 28px;
            width: 5%;
            border-bottom: 1px solid #000;
            border-spacing: 0;
            border-collapse: separate;
        }
    </style>
</head>
<body>
    <form method="post" runat="server" id="form1">
<div class="aspNetHidden">
<input type="hidden" name="__VIEWSTATE" id="__VIEWSTATE" value="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aGxvcm9weWxsPC90ZD48dGQ+PC90ZD48dGQ+U3dhbGxvdzwvdGQ+PHRkPjAxLzAxLzIwMTU8L3RkPjx0ZD5BY3RpdmU8L3RkPjx0ZD4xIFRzcCA8L3RkPjwvdHI+DQo8dHI+PHRkPk1hYWxveDwvdGQ+PHRkPjwvdGQ+PHRkPlh5ejwvdGQ+PHRkPjEwLzAxLzIwMTU8L3RkPjx0ZD5BY3RpdmU8L3RkPjx0ZD4xMCBNTCA8L3RkPjwvdHI+DQo8dHI+PHRkPlR5bGVub2wgZXh0cmEgc3RyZW5ndGg8L3RkPjx0ZD48L3RkPjx0ZD5UYWtlIGl0IHVudGlsIHRoZSBwYWluIHN1YnNpZGVzPC90ZD48dGQ+MTAvMDEvMjAxNTwvdGQ+PHRkPkFjdGl2ZTwvdGQ+PHRkPjIwIG1nIE1nIDwvdGQ+PC90cj4NCjx0cj48dGQ+Vml0YW1pbiBCPC90ZD48dGQ+PC90ZD48dGQ+VGFrZSBldmVyeSBtb3JuaW5nPC90ZD48dGQ+MDEvMDUvMjAxNTwvdGQ+PHRkPkFjdGl2ZTwvdGQ+PHRkPjEwIE1nIDwvdGQ+PC90cj4NCjx0cj48dGQ+Vml0YW1pbiBEPC90ZD48dGQ+PC90ZD48dGQ+VGFrZSBldmVyeSBtb3JuaW5nPC90ZD48dGQ+MDYvMzAvMjAxMzwvdGQ+PHRkPkFjdGl2ZTwvdGQ+PHRkPjMwMDAgSW50bCBVbml0cyAoSVUpIDwvdGQ+PC90cj4NCjx0cj48dGQ+WmFmaXJsdWthc3Q8L3RkPjx0ZD48L3RkPjx0ZD5FdmVyeSBkYXkgaW4gdGhlIGV2ZW5pbmc8L3RkPjx0ZD4wMS8wNS8yMDE1PC90ZD48dGQ+SW5hY3RpdmU8L3RkPjx0ZD4xMCBNZyA8L3RkPjwvdHI+DQo8L3Rib2R5PjwvdGFibGU+DQpkAiEPFgIfAQXlATx0YWJsZT48dGhlYWQ+PHRyPjx0aD5Qcm9ibGVtIFR5cGU8L3RoPjx0aD5TdGF0dXM8L3RoPjx0aD5PbnNldCBEYXRlPC90aD48dGg+RGF0ZSBFbmRlZDwvdGg+PHRoPkRldGFpbHM8L3RoPjwvdHI+PC90aGVhZD48dGJvZHk+DQo8dHI+PHRkPk1pZ3JhaW5lczwvdGQ+PHRkPkFjdGl2ZTwvdGQ+PHRkPjA2LzI0LzIwMTU8L3RkPjx0ZD48L3RkPjx0ZD48L3RkPjwvdHI+DQo8L3Rib2R5PjwvdGFibGU+DQpkAiMPFgIfAQWvATx0YWJsZT48dGhlYWQ+PHRyPjx0aD5UZXN0IE5hbWU8L3RoPjx0aD5UZXN0IERhdGU8L3RoPjx0aD5TdGF0dXM8L3RoPjwvdHI+PC90aGVhZD48dGJvZHk+DQo8dHI+PHRkPkRxcHJ2TnZ6S0kwPTwvdGQ+PHRkPjA4LzAxLzIwMTU8L3RkPjx0ZD5BY3RpdmU8L3RkPjwvdHI+DQo8L3Rib2R5PjwvdGFibGU+DQpkAiUPFgIfAQXnBzx0YWJsZT48dGhlYWQ+PHRyPjx0aD5Tb2NpYWwgSGlzdG9yeTwvdGg+PHRoPlJlc3BvbnNlPC90aD48L3RyPjwvdGhlYWQ+PHRib2R5Pg0KPHRyPjx0ZD5JbiBnZW5lcmFsLCBob3cgc3Ryb25nIGFyZSB5b3VyIHNvY2lhbCB0aWVzIHdpdGggeW91ciBmYW1pbHkgYW5kL29yIGZyaWVuZHM/PC90ZD48dGQ+PC90ZD48L3RyPg0KPHRyPjx0ZD5Ib3cgbWFueSByZWxhdGl2ZXMgb3IgZnJpZW5kcyBkbyB5b3UgZmVlbCBjbG9zZSB0bywgdGhhdCB5b3UgY291bGQgY2FsbCBvbiBmb3IgaGVscCwgaW5jbHVkaW5nIGFzc2lzdGluZyB5b3Ugd2l0aCB5b3VyIGhlYWx0aCBuZWVkcz88L3RkPjx0ZD48L3RkPjwvdHI+DQo8dHI+PHRkPkhvdyBtYW55IHJlbGF0aXZlcyBvciBmcmllbmRzIGRvIHlvdSBmZWVsIGF0IGVhc2Ugd2l0aCx3aG8geW91IGNhbiBkaXNjdXNzIHByaXZhdGUgbWF0dGVycywgaW5jbHVkaW5nIHlvdXIgaGVhbHRoPzwvdGQ+PHRkPjwvdGQ+PC90cj4NCjx0cj48dGQ+SG93IG1hbnkgcmVsYXRpdmVzIG9yIGZyaWVuZHMgZG8geW91IHNlZSBvciBoZWFyIGZyb20gYXQgbGVhc3Qgb25jZSBhIG1vbnRoPzwvdGQ+PHRkPjwvdGQ+PC90cj4NCjx0cj48dGQ+V2l0aCB3aG9tIGRvIHlvdSBsaXZlPzwvdGQ+PHRkPjwvdGQ+PC90cj4NCjx0cj48dGQ+SG93IG1hbnkgdGltZXMgcGVyIHdlZWs/PC90ZD48dGQ+MzwvdGQ+PC90cj4NCjx0cj48dGQ+RG8geW91IGV4ZXJjaXNlPzwvdGQ+PHRkPlllczwvdGQ+PC90cj4NCjx0cj48dGQ+UGxlYXNlIGRlc2NyaWJlIHlvdXIgc21va2luZyBoYWJpdHM8L3RkPjx0ZD5DdXJyZW50IGV2ZXJ5IGRheSBzbW9rZXI8L3RkPjwvdHI+DQo8dHI+PHRkPkRvIHlvdSBzbW9rZSBvciBoYXZlIHlvdSBzbW9rZWQgaW4geW91ciBsaWZldGltZT88L3RkPjx0ZD5ZZXM8L3RkPjwvdHI+DQo8dHI+PHRkPkRvIHlvdSBkcmluayBhbGNvaG9saWMgYmV2ZXJhZ2VzPzwvdGQ+PHRkPlllczwvdGQ+PC90cj4NCjwvdGJvZHk+PC90YWJsZT4NCmQCJw8PFgIfAAUGMTY2IGluZGQCKQ8PFgIfAAUHMTc1IGxic2RkAisPDxYCHwAFAzQuNWRkAi0PDxYCHwAFBDk5IEZkZAIvDw8WAh8ABQY3MiBCUE1kZAIxDw8WAh8ABQgxMzkgbW1IZ2RkAjMPDxYCHwAFBzYwIG1tSGdkZAI1Dw8WAh8ABQYxNyBCUE1kZGTb5bbvMfeNN14S8yp3UotkiX9gOv/zIQd/TxTJJXkRfw==">
</div>

<div class="aspNetHidden">

	<input type="hidden" name="__VIEWSTATEGENERATOR" id="__VIEWSTATEGENERATOR" value="2A73485C">
</div>
    <div id="wrapper">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tbody><tr>
                <td>
                 <a href="http://garage.extreme-cloud.com/PMAPortal/PatientLogin.aspx">
                  <%--  <img src="../PHP_files/PMA-logo.png" alt="PMA" style="border: 0px;">--%></a>
                </td>
            </tr>
            <tr>
                <td class="headings">
                    <span id="PatientNameLabel"><%= this.patientName %></span>
                </td>
            </tr>
            <tr>
                <td class="SubHead">
                    Selections from Personal Health Record<br>
                    <span class="span">Created
                        <span id="CreateddateLabel"><%=this.dateTime %></span></span>
                </td>
            </tr>
            <tr>
                <td style="padding-top: 20px;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tbody><tr>
                            <td class="allergies" style="padding-top: 5px;">
                                Demographics
                            </td>
                        </tr>
                        <tr>
                            <td class="allergies1">
                                Patient Name : <span id="PatientLabel"><%= this.patientName %></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="allergies1">
                                Gender :
                                <span id="GenderLabel"><%=this.gender %></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="allergies1">
                                DOB :
                                <span id="DOBLabel"><%=this.dob %></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="allergies1">
                                Race :
                                <span id="RaceLabel"><%=this.race %></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="allergies1">
                                Ethnicity :
                                <span id="EthnicityLabel"><%=this.ethnicity%></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="allergies1">
                                Preferred Language :
                                <span id="LanguageLabel"><%=this.preflanguage %></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </tbody></table>
                </td>
            </tr>
            <tr>
                <td style="padding-top: 20px;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tbody><tr>
                            <td class="allergies" style="padding-top: 5px;">
                                Allergies
                            </td>
                        </tr>
                        <tr>
                            <td>
             <asp:GridView runat="server" ID="gdallergies" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" OnSelectedIndexChanged="gdallergies_SelectedIndexChanged" >
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                             Name</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AllergyType")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                     <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                            Reaction</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                             <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Reaction")%>'></asp:Label>
                      </div>
                    </ItemTemplate>
                </asp:TemplateField>
                       <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                             Date observed</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"DateObserved")%>'></asp:Label>
                       </div>
                    </ItemTemplate>
                </asp:TemplateField>
                     <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                             Status</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                          <%--   <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"")%>'></asp:Label>--%>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                     <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                          Date Ended</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                           <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"DateLastOccured")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </tbody></table>
                </td>
            </tr>
            
            <tr>
                <td style="padding-top: 20px;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tbody><tr>
                            <td class="allergies" style="padding-top: 5px;">
                                Discharge Instructions
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tbody><tr>
                                        <td width="25%" class="tablehead">
                                            <div id="dischargeDiv"><table><thead><tr><th>Description</th></tr></thead><tbody>
<tr><td style="padding-top: 3px; border-bottom: #c0c0c0 1px solid" valign="top"><table><tbody><tr><td style="padding-top: 10px" valign="top"><b>Date:</b>&nbsp;&nbsp;</td><td valign="top" style="padding-top: 10px"><%=this.discharge_date %></td></tr><tr><td style="padding-top: 10px" valign="top"><b>Notes:</b>&nbsp;&nbsp;</td><td valign="top" style="padding-top: 10px"><%=this.discharge_notes %></td></tr><tr><td style="padding-top: 10px" valign="top"><b>Goals:</b>&nbsp;&nbsp;</td><td valign="top" style="padding-top: 10px"><%=this.discharge_goals %></td></tr><tr><td style="padding-top: 10px" valign="top"><b>Instructions:</b>&nbsp;&nbsp;</td><td valign="top" style="padding-top: 10px"><%=this.discharge_Instructions%></td></tr></tbody></table>
</td></tr>
</tbody></table>
</div>
                                        </td>
                                    </tr>
                                </tbody></table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </tbody></table>
                </td>
            </tr>
            <tr>
                <td style="padding-top: 20px;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tbody><tr>
                            <td class="allergies" style="padding-top: 5px;">
                                Visits
                            </td>
                        </tr>
                        <tr>
                            <td>
              <asp:GridView runat="server" ID="gdvisits" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" >
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                             Location</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                     <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                           Visit Date

                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Visitdate")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                       <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                             Visit Reason</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"VisitReason")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                   
            </Columns>
        </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </tbody></table>
                </td>
            </tr>
            <tr>
                <td style="padding-top: 20px;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tbody><tr>
                            <td class="allergies" style="padding-top: 5px;">
                                Care Team
                            </td>
                        </tr>
                        <tr>
                            <td>
        <asp:GridView runat="server" ID="gdcare" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" >
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                             Visit Date</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AllergyType")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                     <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                         	Primary Care Physician
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Reaction")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                       <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                             Referring Physician
                        </div>
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
                            Attending Physician
                        </div>
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
                            Admitting Physician
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Relationship")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>   
            </Columns>
        </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </tbody></table>
                </td>
            </tr>
            <tr>
                <td style="padding-top: 20px;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tbody><tr>
                            <td class="allergies" style="padding-top: 5px;">
                                Family History
                            </td>
                        </tr>
                        <tr>
                            <td>
        <asp:GridView runat="server" ID="gdfamily" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" >
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                             Condition</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ConditionName")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                     <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                         	Onset date
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"OnsetDate","{0:MM/dd/yyyy}")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                       <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                             Resolution
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                          <%--  <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"")%>'></asp:Label>--%>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                                       <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                           	Relationship
                        </div>
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
                            	Details
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
      <%--                      <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"")%>'></asp:Label>--%>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>   
            </Columns>
        </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </tbody></table>
                </td>
            </tr>
            <tr>
                <td style="padding-top: 20px;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tbody><tr>
                            <td class="allergies" style="padding-top: 5px;">
                                Immunizations
                            </td>
                        </tr>
                        <tr>
                            <td>
 <asp:GridView runat="server" ID="gdimmunization" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" >
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                            Name</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ImmunizationType")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                     <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                         Administration Date
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AdministrationDate")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                       <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                            Sequence
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
               <%--             <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Relationship")%>'></asp:Label>--%>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                                       <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                           Remarks
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Notes")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>   
        
            </Columns>
        </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </tbody></table>
                </td>
            </tr>
            <tr>
                <td style="padding-top: 20px;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tbody><tr>
                            <td class="allergies" style="padding-top: 5px;">
                                Lab Results
                            </td>
                        </tr>
                        <tr>
                            <td>
 <asp:GridView runat="server" ID="gdLabResults" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" >
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                           Result Section

                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Results")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                     <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                         Discharge Date
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Date")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                       <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                           	Transmitted On
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                          <%--  <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"")%>'></asp:Label>--%>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
        
            </Columns>
        </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </tbody></table>
                </td>
            </tr>
            <tr>
                <td style="padding-top: 20px;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tbody><tr>
                            <td class="allergies" style="padding-top: 5px;">
                                Medications
                            </td>
                        </tr>
                        <tr>
                            <td>
          <asp:GridView runat="server" ID="gdmedication" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" >
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                       Medication Name

                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Description")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                     <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                        Other Name
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Description")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                       <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                           	Instructions
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Units")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                               <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                          Date Started
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Date")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                                       <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                          Status
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Status")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                   <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                       Dosage
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Dosage")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </tbody></table>
                </td>
            </tr>
            <tr>
                <td style="padding-top: 20px;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tbody><tr>
                            <td class="allergies" style="padding-top: 5px;">
                                Problems
                            </td>
                        </tr>
                        <tr>
                            <td>
      <asp:GridView runat="server" ID="gdproblem" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" >
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                        Problem Type
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                       <%--     <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"")%>'></asp:Label>--%>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                     <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                   Status
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                        <%--    <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"")%>'></asp:Label>--%>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                       <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                         	Onset Date
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                        <%--    <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"")%>'></asp:Label>--%>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                               <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                         	Date Ended
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                         <%--   <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"")%>'></asp:Label>--%>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                                       <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                          	Details
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                            <%--<asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"")%>'></asp:Label>--%>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </tbody></table>
                </td>
            </tr>
            <tr>
                <td style="padding-top: 20px;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tbody><tr>
                            <td class="allergies" style="padding-top: 5px;">
                                Tests and Procedures
                            </td>
                        </tr>
                        <tr>
                            <td>
         <asp:GridView runat="server" ID="gdtest" ClientIDMode="Static" AutoGenerateColumns="false"
            GridLines="None" >
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                Test Name

                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SurgeriesProcedure")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                     <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                    Test Date
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Date")%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                       <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                     Status
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div >
                         <%--   <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"")%>'></asp:Label>--%>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </tbody></table>
                </td>
            </tr>
            
            <tr>
                <td style="padding-top: 20px;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tbody><tr>
                            <td class="allergies" style="padding-top: 5px;">
                                Social History
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tbody><tr>
                                        <td width="25%" class="tablehead">
                                           <div id="SocialDiv"><table><thead><tr><th>Social History</th><th>Response</th></tr></thead><tbody>
<tr><td>In general, how strong are your social ties with your family and/or friends?</td>
    <td><%=this.Social_Ties %></td>

</tr>
<tr><td>How many relatives or friends do you feel close to, that you could call on for help, including assisting you with your health needs?</td>
    <td><%=this.Relatives %></td>

</tr>
<tr><td>How many relatives or friends do you feel at ease with,who you can discuss private matters, including your health?</td>
    <td><%=this.Relatives_Discuss_Private_Matters %></td>

</tr>
<tr><td>How many relatives or friends do you see or hear from at least once a month?</td>
    <td><%=this.Relatives_See_Monthly %></td>

</tr>
<tr><td>With whom do you live?</td>
    <td><%=this.Living_With %></td>

</tr>
<tr><td>How many times per week?</td>
    <td><%=this.Per_Week %></td>

</tr>
<tr><td>Do you exercise?</td>
    <td><%=this.Excercise %></td>

</tr>
<tr><td>Please describe your smoking habits</td
    ><td><%=this.Smoking_Habits %></td>

</tr>
<tr><td>Do you smoke or have you smoked in your lifetime?</td>
    <td><%=this.Smoke %></td>

</tr>
<tr><td>Do you drink alcoholic beverages?</td>
    <td><%=this.Alcohol %></td>

</tr>
</tbody></table>
</div>
                                        </td>
                                    </tr>
                                </tbody></table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </tbody></table>
                </td>
            </tr>
            <tr>
                <td style="padding-top: 20px;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tbody><tr>
                            <td class="allergies" style="padding-top: 5px;">
                                Vitals
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tbody><tr>
                                        <td width="25%" class="allergies">
                                            Height
                                        </td>
                                        <td width="25%" class="allergies">
                                            Weight
                                        </td>
                                        <td width="25%" class="allergies">
                                            BMI
                                        </td>
                                        <td width="25%" class="allergies">
                                            Temperature
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tablehead" style="border: 0px;">
                                            <span id="HeightTextbox"><%=this.Vital_Height %> <%=this.Vital_Height_Unit %></span>
                                        </td>
                                        <td class="tablehead" style="border: 0px;">
                                            <span id="WeightTextbox"><%=this.Vital_Weight %> <%=this.Vital_Weight_Unit %></span>
                                        </td>
                                        <td class="tablehead" style="border: 0px;">
                                            <span id="Bmilabel"><%=this.Vital_BMI %></span>
                                        </td>
                                        <td class="tablehead" style="border: 0px;">
                                            <span id="TemperatureTextbox"><%=this.Vital_Temperature %> <%=this.Vital_Temperature_Unit %></span>
                                        </td>
                                    </tr>
                                </tbody></table>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-bottom: 2px solid #000;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tbody><tr>
                                        <td width="25%" class="allergies">
                                            PULSE
                                        </td>
                                        <td width="25%" class="allergies">
                                            BP
                                        </td>
                                        <td width="25%" class="allergies">
                                            Respiration
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tablehead" style="border: 0px;">
                                            <span id="PMATextbox"><%=this.Vital_Pulse %></span>
                                        </td>
                                        <td class="tablehead" style="border: 0px;">
                                            <span id="BPsystolicTextbox"><%=this.Vital_BP %></span>
                                        </td>
                                        <td class="tablehead" style="border: 0px;">
                                            <span id="RespirationTextbox"><%=this.Vital_Respiration %></span>
                                        </td>
                                    </tr>
                                </tbody></table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </tbody></table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </tbody></table>
    </div>
    </form>


</body></html>


