<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCCreatePatient.ascx.cs"
    Inherits="Hick.PatientLookUp.UserControls.UCCreatePatient" %>
<link href="../../Content/patientlookup.css" rel="stylesheet" type="text/css" />

    <div class="patsearch_heading">
        Create New Patient
         <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popup_close" class="pull-right" style="cursor: pointer;margin-top:-5px;"
                alt="close" />
    </div>
   <div class="patsearch_border">
    <div class="patsearch_contents" style="margin-left: 15px; margin-top: 20px;">
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Last Name</div>
            <asp:TextBox ID="lastname" runat="server" CssClass="standard_textbox" ClientIDMode="Static"></asp:TextBox>
            <asp:RequiredFieldValidator SetFocusOnError="true" ForeColor="Red" Style="float: left;
                margin-left: 5px; margin-top:10px;" ID="rqvLastName" runat="server" ControlToValidate="lastname"
                ErrorMessage="Last Name is Required." Display="Dynamic" CssClass="error"
                ValidationGroup="group1"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ForeColor="Red" Style="float: left; margin-left: 5px; margin-top:10px;"
                ID="rqvLN" runat="server" SetFocusOnError="true" ErrorMessage="Only Alphabets are allowed in the Last Name."
                ControlToValidate="lastname" CssClass="error" ValidationExpression="^[a-zA-Z ]+$"
                Display="Dynamic" ValidationGroup="group1"></asp:RegularExpressionValidator>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                First Name</div>
            <asp:TextBox ID="firstname" runat="server" CssClass="standard_textbox" ClientIDMode="Static"></asp:TextBox>
            <asp:RequiredFieldValidator ForeColor="Red" SetFocusOnError="true" Style="float: left;
                margin-left: 5px; margin-top:10px;" ID="rqvFirstName" runat="server" ControlToValidate="firstname"
                ErrorMessage="First Name is Required." Display="Dynamic" CssClass="error"
                ValidationGroup="group1"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ForeColor="Red" Style="float: left; margin-left: 5px; margin-top:10px;"
                ID="rqvFN" runat="server" SetFocusOnError="true" ErrorMessage="Only Alphabets are allowed in the First Name."
                ControlToValidate="firstname" CssClass="error" ValidationExpression="^[a-zA-Z ]+$"
                Display="Dynamic" ValidationGroup="group1"></asp:RegularExpressionValidator>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Middle Name</div>
            <asp:TextBox ID="middlename" runat="server" CssClass="standard_textbox" ClientIDMode="Static"></asp:TextBox>
            <asp:RegularExpressionValidator ForeColor="Red" SetFocusOnError="true" Style="float: left;
                margin-left: 5px; margin-top:10px;" ID="rqvMN" runat="server" ErrorMessage="Only Alphabets are allowed in the Middle Name."
                ControlToValidate="middlename" CssClass="error" ValidationExpression="^[a-zA-Z ]+$"
                Display="Dynamic" ValidationGroup="group1"></asp:RegularExpressionValidator>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Maiden Name</div>
            <asp:TextBox ID="maidenname" runat="server" CssClass="standard_textbox" ClientIDMode="Static"></asp:TextBox>
            <asp:RegularExpressionValidator SetFocusOnError="true" Style="float: left; margin-left: 5px; margin-top:10px;"
                ID="RegularExpressionValidator1" ForeColor="Red" runat="server" ErrorMessage="Only Alphabets are allowed in the Maiden Name."
                ControlToValidate="middlename" CssClass="error" ValidationExpression="^[a-zA-Z ]+$"
                Display="Dynamic" ValidationGroup="group1"></asp:RegularExpressionValidator>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Last 4 digits of SSN</div>
            <asp:TextBox ID="lastssn" runat="server" MaxLength="4" CssClass="standard_textbox"
                ClientIDMode="Static"></asp:TextBox>
            <asp:RegularExpressionValidator ForeColor="Red" CssClass="error" SetFocusOnError="true"
                Style="float: left;margin-left: 5px; margin-top:10px;" ID="rqvSSN" runat="server" ErrorMessage="Please enter last 4 digits of SSN"
                ControlToValidate="lastssn" ValidationExpression="^[0-9]{4}$" Display="Dynamic"
                ValidationGroup="group1"></asp:RegularExpressionValidator>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Date of Birth</div>
            <asp:TextBox ID="dob" runat="server" CssClass="standard_textbox" ClientIDMode="Static"></asp:TextBox>
            <asp:RequiredFieldValidator ForeColor="Red" SetFocusOnError="true" ValidationGroup="group1"
                ID="rqvDateOfBirth" Style="float: left; margin-left: 5px; margin-top:10px;" runat="server" ErrorMessage="Date of Birth is Required."
                ControlToValidate="dob" CssClass="error" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ForeColor="Red" SetFocusOnError="true" Style="float: left;
                margin-left: 5px; margin-top:10px;" ID="RegularExpressionValidator3" runat="server" ValidationGroup="group1"
                ControlToValidate="dob" ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$"
                ErrorMessage="Please make sure date of birth is valid." CssClass="error" Display="Dynamic"></asp:RegularExpressionValidator>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Gender</div>
            <asp:RadioButtonList ID="rbGender" runat="server" RepeatDirection="Horizontal" Font-Bold="false" >
                <asp:ListItem Selected="False" style="font-weight: normal;">Male</asp:ListItem>
                <asp:ListItem Selected="False" style="font-weight: normal;">Female</asp:ListItem>
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator ForeColor="Red" SetFocusOnError="true" ValidationGroup="group1"
                ID="RequiredFieldValidator1" Style="float: left; margin-left: 0px;margin-top:5px;" runat="server"
                ErrorMessage="Gender is Required." ControlToValidate="rbGender" CssClass="error"
                Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Race</div>
            <asp:DropDownList ID="ddlRace" CssClass="standard_textbox" runat="server">
                <asp:ListItem>Select</asp:ListItem>
                <asp:ListItem>Caucasian</asp:ListItem>
                <asp:ListItem>Hispanic</asp:ListItem>
                <asp:ListItem>Black</asp:ListItem>
                <asp:ListItem>Asian</asp:ListItem>
                <asp:ListItem>American Indian or Alaska Native</asp:ListItem>
                <asp:ListItem>Native Hawaiians or Other Pacific Islander</asp:ListItem>
                <asp:ListItem>Other</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Marital Status</div>
            <asp:DropDownList ID="ddlMarital" CssClass="standard_textbox" runat="server">
                <asp:ListItem>Select</asp:ListItem>
                <asp:ListItem>Married</asp:ListItem>
                <asp:ListItem>Single</asp:ListItem>
                <asp:ListItem>Separated</asp:ListItem>
                <asp:ListItem>Divorced</asp:ListItem>
                <asp:ListItem>Living With Partner</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Primary Address</div>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Address 1</div>
            <%--<asp:TextBox ID="address1" runat="server" CssClass="standard_textbox" Rows="1" TextMode="MultiLine"
                ClientIDMode="Static"></asp:TextBox>--%>
            <asp:TextBox ID="address1" runat="server" CssClass="standard_textbox" ClientIDMode="Static"></asp:TextBox>
            <asp:RequiredFieldValidator ForeColor="Red" SetFocusOnError="true" Style="float: left;
                margin-left: 5px; margin-top:10px;" ID="rqvAddress1" runat="server" CssClass="error" ControlToValidate="address1"
                ErrorMessage="Address is Required." Display="Dynamic" ValidationGroup="group1"></asp:RequiredFieldValidator>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Address 2</div>
           <%-- <asp:TextBox ID="address2" runat="server" CssClass="standard_textbox" Rows="1" TextMode="MultiLine"
                ClientIDMode="Static"></asp:TextBox>--%>
            <asp:TextBox ID="address2" runat="server" CssClass="standard_textbox" ClientIDMode="Static"></asp:TextBox>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                City</div>
            <asp:TextBox ID="city" runat="server" CssClass="standard_textbox" ClientIDMode="Static"></asp:TextBox>
            <asp:RequiredFieldValidator ForeColor="Red" SetFocusOnError="true" Style="float: left;
                margin-left: 5px; margin-top:10px;" ID="rqc" runat="server" ControlToValidate="city" ErrorMessage="City is Required."
                Display="Dynamic" CssClass="error" ValidationGroup="group1"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ForeColor="Red" SetFocusOnError="true" Style="float: left;
                margin-left: 5px; margin-top:10px;;" ID="rcity" runat="server" ErrorMessage="Only Alphabets are allowed in the City."
                ControlToValidate="city" ValidationExpression="^[a-zA-Z ]+$" Display="Dynamic"
                ValidationGroup="group1" CssClass="error"></asp:RegularExpressionValidator>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                State</div>
            <asp:DropDownList ID="ddlState" runat="server" CssClass="standard_textbox">
                <asp:ListItem Selected="True">Select</asp:ListItem>
                <asp:ListItem Value="Alabama">Alabama</asp:ListItem>
                <asp:ListItem Value="Alaska">Alaska</asp:ListItem>
                <asp:ListItem Value="Arizona">Arizona</asp:ListItem>
                <asp:ListItem Value="Arkansas">Arkansas</asp:ListItem>
                <asp:ListItem Value="California">California</asp:ListItem>
                <asp:ListItem Value="Colorado">Colorado</asp:ListItem>
                <asp:ListItem Value="Connecticut">Connecticut</asp:ListItem>
                <asp:ListItem Value="Delaware">Delaware</asp:ListItem>
                <asp:ListItem Value="District of Columbia">District of Columbia</asp:ListItem>
                <asp:ListItem Value="Florida">Florida</asp:ListItem>
                <asp:ListItem Value="Georgia">Georgia</asp:ListItem>
                <asp:ListItem Value="Hawaii">Hawaii</asp:ListItem>
                <asp:ListItem Value="Idaho">Idaho</asp:ListItem>
                <asp:ListItem Value="Illinois">Illinois</asp:ListItem>
                <asp:ListItem Value="Indiana">Indiana</asp:ListItem>
                <asp:ListItem Value="Iowa">Iowa</asp:ListItem>
                <asp:ListItem Value="Kansas">Kansas</asp:ListItem>
                <asp:ListItem Value="Kentucky">Kentucky</asp:ListItem>
                <asp:ListItem Value="Louisiana">Louisiana</asp:ListItem>
                <asp:ListItem Value="Maine">Maine</asp:ListItem>
                <asp:ListItem Value="Maryland">Maryland</asp:ListItem>
                <asp:ListItem Value="Massachusetts">Massachusetts</asp:ListItem>
                <asp:ListItem Value="Michigan">Michigan</asp:ListItem>
                <asp:ListItem Value="Minnesota">Minnesota</asp:ListItem>
                <asp:ListItem Value="Mississippi">Mississippi</asp:ListItem>
                <asp:ListItem Value="Missouri">Missouri</asp:ListItem>
                <asp:ListItem Value="Montana">Montana</asp:ListItem>
                <asp:ListItem Value="Nebraska">Nebraska</asp:ListItem>
                <asp:ListItem Value="Nevada">Nevada</asp:ListItem>
                <asp:ListItem Value="New Hampshire">New Hampshire</asp:ListItem>
                <asp:ListItem Value="New Jersey">New Jersey</asp:ListItem>
                <asp:ListItem Value="New Mexico">New Mexico</asp:ListItem>
                <asp:ListItem Value="New York">New York</asp:ListItem>
                <asp:ListItem Value="North Carolina">North Carolina</asp:ListItem>
                <asp:ListItem Value="North Dakota">North Dakota</asp:ListItem>
                <asp:ListItem Value="Ohio">Ohio</asp:ListItem>
                <asp:ListItem Value="Oklahoma">Oklahoma</asp:ListItem>
                <asp:ListItem Value="Oregon">Oregon</asp:ListItem>
                <asp:ListItem Value="Puerto Rico">Puerto Rico</asp:ListItem>
                <asp:ListItem Value="Pennsylvania">Pennsylvania</asp:ListItem>
                <asp:ListItem Value="Rhode Island">Rhode Island</asp:ListItem>
                <asp:ListItem Value="South Carolina">South Carolina</asp:ListItem>
                <asp:ListItem Value="South Dakota">South Dakota</asp:ListItem>
                <asp:ListItem Value="Tennessee">Tennessee</asp:ListItem>
                <asp:ListItem Value="Texas">Texas</asp:ListItem>
                <asp:ListItem Value="Utah">Utah</asp:ListItem>
                <asp:ListItem Value="Vermont">Vermont</asp:ListItem>
                <asp:ListItem Value="Virginia">Virginia</asp:ListItem>
                <asp:ListItem Value="Washington">Washington</asp:ListItem>
                <asp:ListItem Value="West Virginia">West Virginia</asp:ListItem>
                <asp:ListItem Value="Wisconsin">Wisconsin</asp:ListItem>
                <asp:ListItem Value="Wyoming">Wyoming</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ForeColor="Red" SetFocusOnError="true" Style="float: left;
                margin-left: 5px; margin-top:10px;" ID="rState" runat="server" ControlToValidate="ddlState" InitialValue="Select"
                ErrorMessage="State is Required." CssClass="error" Display="Dynamic" ValidationGroup="group1"></asp:RequiredFieldValidator>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Zip</div>
            <asp:TextBox ID="zip" MaxLength="5" runat="server" CssClass="standard_textbox" ClientIDMode="Static"></asp:TextBox>
            <asp:RequiredFieldValidator ForeColor="Red" SetFocusOnError="true" Style="float: left;
                margin-left: 5px; margin-top:10px;" ID="RequiredFieldValidator2" runat="server" ControlToValidate="zip"
                ErrorMessage="Zip code is Required." Display="dynamic" CssClass="error" ValidationGroup="group1"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ForeColor="Red" CssClass="error" SetFocusOnError="true"
                Style="float: left; margin-left: 5px; margin-top:10px;" ID="Zipr" runat="server" ErrorMessage="Please make sure Zip Code is valid."
                ControlToValidate="zip" ValidationExpression="\d{5}(-\d{4})?" Display="Dynamic"
                ValidationGroup="group1"></asp:RegularExpressionValidator>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Do you have an alternate address?</div>
            <asp:DropDownList ID="ddlalternateaddress" runat="server" Height="32px" CssClass="standard_textbox"
                onChange="onddlChange(this.value)">
                <asp:ListItem Value="Select">Select</asp:ListItem>
                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                <asp:ListItem Value="No">No</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div id="divAlternate" runat="server" style="display: none;">
            <div class="patsearch_formcontrol">
                <div class="standard_label1">
                    Alternate Address</div>
            </div>
            <div class="patsearch_formcontrol">
                <div class="standard_label1">
                    Address 1</div>
                <asp:TextBox ID="alteraddress1" runat="server" CssClass="standard_textbox" ClientIDMode="Static"></asp:TextBox>
            </div>
            <div class="patsearch_formcontrol">
                <div class="standard_label1">
                    Address 2</div>
                <asp:TextBox ID="alteraddress2" runat="server" CssClass="standard_textbox" ClientIDMode="Static"></asp:TextBox>
            </div>
            <div class="patsearch_formcontrol">
                <div class="standard_label1">
                    City</div>
                <asp:TextBox ID="altercity" runat="server" CssClass="standard_textbox" ClientIDMode="Static"></asp:TextBox>
                <asp:RegularExpressionValidator ForeColor="Red" SetFocusOnError="true" Style="float: left;
                    margin-left: 5px; margin-top:10px;" ID="RegularExpressionValidator9" runat="server" ErrorMessage="Only Alphabets are allowed in the City."
                    ControlToValidate="altercity" CssClass="error" ValidationExpression="^[a-zA-Z ]+$"
                    Display="Dynamic" ValidationGroup="group1"></asp:RegularExpressionValidator>
            </div>
            <div class="patsearch_formcontrol">
                <div class="standard_label1">
                    State</div>
                <asp:DropDownList ID="ddlalternatestate" runat="server" CssClass="standard_textbox">
                    <asp:ListItem Selected="True">Select</asp:ListItem>
                    <asp:ListItem Value="Alabama">Alabama</asp:ListItem>
                    <asp:ListItem Value="Alaska">Alaska</asp:ListItem>
                    <asp:ListItem Value="Arizona">Arizona</asp:ListItem>
                    <asp:ListItem Value="Arkansas">Arkansas</asp:ListItem>
                    <asp:ListItem Value="California">California</asp:ListItem>
                    <asp:ListItem Value="Colorado">Colorado</asp:ListItem>
                    <asp:ListItem Value="Connecticut">Connecticut</asp:ListItem>
                    <asp:ListItem Value="Delaware">Delaware</asp:ListItem>
                    <asp:ListItem Value="District of Columbia">District of Columbia</asp:ListItem>
                    <asp:ListItem Value="Florida">Florida</asp:ListItem>
                    <asp:ListItem Value="Georgia">Georgia</asp:ListItem>
                    <asp:ListItem Value="Hawaii">Hawaii</asp:ListItem>
                    <asp:ListItem Value="Idaho">Idaho</asp:ListItem>
                    <asp:ListItem Value="Illinois">Illinois</asp:ListItem>
                    <asp:ListItem Value="Indiana">Indiana</asp:ListItem>
                    <asp:ListItem Value="Iowa">Iowa</asp:ListItem>
                    <asp:ListItem Value="Kansas">Kansas</asp:ListItem>
                    <asp:ListItem Value="Kentucky">Kentucky</asp:ListItem>
                    <asp:ListItem Value="Louisiana">Louisiana</asp:ListItem>
                    <asp:ListItem Value="Maine">Maine</asp:ListItem>
                    <asp:ListItem Value="Maryland">Maryland</asp:ListItem>
                    <asp:ListItem Value="Massachusetts">Massachusetts</asp:ListItem>
                    <asp:ListItem Value="Michigan">Michigan</asp:ListItem>
                    <asp:ListItem Value="Minnesota">Minnesota</asp:ListItem>
                    <asp:ListItem Value="Mississippi">Mississippi</asp:ListItem>
                    <asp:ListItem Value="Missouri">Missouri</asp:ListItem>
                    <asp:ListItem Value="Montana">Montana</asp:ListItem>
                    <asp:ListItem Value="Nebraska">Nebraska</asp:ListItem>
                    <asp:ListItem Value="Nevada">Nevada</asp:ListItem>
                    <asp:ListItem Value="New Hampshire">New Hampshire</asp:ListItem>
                    <asp:ListItem Value="New Jersey">New Jersey</asp:ListItem>
                    <asp:ListItem Value="New Mexico">New Mexico</asp:ListItem>
                    <asp:ListItem Value="New York">New York</asp:ListItem>
                    <asp:ListItem Value="North Carolina">North Carolina</asp:ListItem>
                    <asp:ListItem Value="North Dakota">North Dakota</asp:ListItem>
                    <asp:ListItem Value="Ohio">Ohio</asp:ListItem>
                    <asp:ListItem Value="Oklahoma">Oklahoma</asp:ListItem>
                    <asp:ListItem Value="Oregon">Oregon</asp:ListItem>
                    <asp:ListItem Value="Puerto Rico">Puerto Rico</asp:ListItem>
                    <asp:ListItem Value="Pennsylvania">Pennsylvania</asp:ListItem>
                    <asp:ListItem Value="Rhode Island">Rhode Island</asp:ListItem>
                    <asp:ListItem Value="South Carolina">South Carolina</asp:ListItem>
                    <asp:ListItem Value="South Dakota">South Dakota</asp:ListItem>
                    <asp:ListItem Value="Tennessee">Tennessee</asp:ListItem>
                    <asp:ListItem Value="Texas">Texas</asp:ListItem>
                    <asp:ListItem Value="Utah">Utah</asp:ListItem>
                    <asp:ListItem Value="Vermont">Vermont</asp:ListItem>
                    <asp:ListItem Value="Virginia">Virginia</asp:ListItem>
                    <asp:ListItem Value="Washington">Washington</asp:ListItem>
                    <asp:ListItem Value="West Virginia">West Virginia</asp:ListItem>
                    <asp:ListItem Value="Wisconsin">Wisconsin</asp:ListItem>
                    <asp:ListItem Value="Wyoming">Wyoming</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="patsearch_formcontrol">
                <div class="standard_label1">
                    Zip</div>
                <asp:TextBox ID="alterzip" runat="server" CssClass="standard_textbox" MaxLength="5"
                    ClientIDMode="Static"></asp:TextBox>
                <asp:RegularExpressionValidator SetFocusOnError="true" Style="float: left; margin-left: 5px; margin-top:10px;"
                    ID="RegularExpressionValidator8" runat="server" ErrorMessage="Please make sure Alternate Zip Code is valid."
                    ControlToValidate="alterzip" CssClass="error" ValidationExpression="\d{5}(-\d{4})?"
                    Display="Dynamic" ValidationGroup="group1"></asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Home Phone</div>
            <asp:TextBox ID="homephone" MaxLength="10" runat="server" CssClass="standard_textbox"
                ClientIDMode="Static"></asp:TextBox>
            <asp:RequiredFieldValidator ForeColor="Red" SetFocusOnError="true" Style="float: left;
                margin-left: 5px; margin-top:10px;" ID="RequiredFieldValidator3" runat="server" ControlToValidate="homephone"
                ErrorMessage="Home Phone is Required." Display="dynamic" CssClass="error" ValidationGroup="group1"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ForeColor="Red" SetFocusOnError="true" ID="RegularExpressionValidator7"
                Style="float: left; margin-left: 5px; margin-top:10px;" CssClass="error" ControlToValidate="homephone"
                ValidationExpression="[0-9]{10}" Display="Dynamic" ValidationGroup="group1"
                ErrorMessage="Please enter 10 digits." runat="server" />
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Work Phone</div>
            <asp:TextBox ID="workphone" MaxLength="10" runat="server" CssClass="standard_textbox"
                ClientIDMode="Static"></asp:TextBox>
            <asp:RegularExpressionValidator ForeColor="Red" SetFocusOnError="true" Style="float: left;
                margin-left: 5px; margin-top:10px;" ID="rqvtxtworkphone" runat="server" CssClass="error" ErrorMessage="Please enter 10 digits."
                ControlToValidate="workphone" ValidationExpression="^\([0-9]{3}\)[0-9]{3}-[0-9]{4}$"
                Display="Dynamic" ValidationGroup="group1"></asp:RegularExpressionValidator>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Cell Phone</div>
            <asp:TextBox ID="cellphone" MaxLength="10" runat="server" CssClass="standard_textbox"
                ClientIDMode="Static"></asp:TextBox>
            <asp:RegularExpressionValidator ForeColor="Red" SetFocusOnError="true" Style="float: left;
                margin-left: 5px; margin-top:10px;" ID="rqvtxtcellphone" runat="server" CssClass="error" ErrorMessage="Please enter 10 digits."
                ControlToValidate="cellphone" ValidationExpression="^\([0-9]{3}\)[0-9]{3}-[0-9]{4}$"
                Display="Dynamic" ValidationGroup="group1"></asp:RegularExpressionValidator>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                E-mail
            </div>
            <asp:TextBox ID="email" runat="server" CssClass="standard_textbox" ClientIDMode="Static"></asp:TextBox>
            <asp:RegularExpressionValidator ForeColor="Red" SetFocusOnError="true" Style="float: left;
                margin-left: 5px; margin-top:10px;" ID="rqvHN" runat="server" ErrorMessage="Please make sure email id is valid."
                ControlToValidate="email" CssClass="error" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                Display="Dynamic" ValidationGroup="group1"></asp:RegularExpressionValidator>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Height
            </div>
            <asp:TextBox ID="heightft" runat="server" SkinID="skinTxt" CssClass="standard_textbox"
                ForeColor="Black" Height="25px" Width="50px" MaxLength="2"></asp:TextBox>
            &nbsp; &nbsp;<span style="margin-top: 3px;">ft </span>&nbsp; &nbsp;
            <asp:TextBox ID="heightin" runat="server" SkinID="skinTxt" CssClass="standard_textbox"
                ForeColor="Black" Height="25px" Width="50px" MaxLength="2"></asp:TextBox><span style="margin-top: 3px;margin-left:8px;">in
                </span>
            <asp:RegularExpressionValidator CssClass="error" Style="float: left;" ID="RegularExpressionValidator4"
                runat="server" ErrorMessage="Only Integers allowed." ControlToValidate="heightft"
                ValidationExpression="^[0-9]+$" Display="Static" ValidationGroup="group1"></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" CssClass="error"
                ErrorMessage="Only Integers allowed." ControlToValidate="heightin" ValidationExpression="^[0-9]+$"
                Display="Static" ValidationGroup="group1"></asp:RegularExpressionValidator>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Weight</div>
            <asp:TextBox ID="weight" runat="server" CssClass="standard_textbox" ClientIDMode="Static"></asp:TextBox>&nbsp;lbs
            <asp:RegularExpressionValidator SetFocusOnError="true" Style="float: left; margin-left: 0px;"
                ID="rqWeight" runat="server" CssClass="error" ErrorMessage="Please input a valid weight value."
                ControlToValidate="weight" ValidationExpression="^\d{1,3}(\.\d{0,2})?$" Display="Dynamic"
                ValidationGroup="group1"></asp:RegularExpressionValidator>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Eye Color</div>
            <asp:TextBox ID="eyecolor" runat="server" CssClass="standard_textbox" ClientIDMode="Static"></asp:TextBox>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Hair Color</div>
            <asp:TextBox ID="haircolor" runat="server" CssClass="standard_textbox" ClientIDMode="Static"></asp:TextBox>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Blood / RH Type</div>
            <asp:DropDownList ID="ddlBlood" runat="server" CssClass="standard_textbox">
                <asp:ListItem>Select</asp:ListItem>
                <asp:ListItem>O+</asp:ListItem>
                <asp:ListItem>A+</asp:ListItem>
                <asp:ListItem>B+</asp:ListItem>
                <asp:ListItem>AB+</asp:ListItem>
                <asp:ListItem>O-</asp:ListItem>
                <asp:ListItem>A-</asp:ListItem>
                <asp:ListItem>B-</asp:ListItem>
                <asp:ListItem>AB-</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Birthmark / Scars</div>
            <asp:TextBox ID="birthmark" runat="server" CssClass="standard_textbox" ClientIDMode="Static"></asp:TextBox>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Religious Preferences</div>
            <asp:TextBox ID="religiouspreferences" runat="server" CssClass="standard_textbox"
                ClientIDMode="Static"></asp:TextBox>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Special conditions</div>
            <asp:TextBox ID="specialconditions" runat="server" CssClass="standard_textbox" ClientIDMode="Static"></asp:TextBox>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Insurance Information</div>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Primary Insurance</div>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Financial Class</div>
            <asp:TextBox ID="financialclass" runat="server" CssClass="standard_textbox" ClientIDMode="Static"></asp:TextBox>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Company Name</div>
            <asp:TextBox ID="companyname" runat="server" CssClass="standard_textbox" ClientIDMode="Static"></asp:TextBox>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Policy Number</div>
            <asp:TextBox ID="policynumber" runat="server" CssClass="standard_textbox" ClientIDMode="Static"></asp:TextBox>
            <asp:RegularExpressionValidator Style="float: left; margin-left: 5px; margin-top:10px;" ID="revtxtpolicyno"
                runat="server" ErrorMessage="Please enter alphanumeric values." CssClass="error"
                ControlToValidate="policynumber" ValidationExpression="^[0-9a-zA-Z]+$" Display="Dynamic"
                ValidationGroup="group1"></asp:RegularExpressionValidator>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Group Number</div>
            <asp:TextBox ID="groupnumber" runat="server" CssClass="standard_textbox" ClientIDMode="Static"></asp:TextBox>
            <asp:RegularExpressionValidator SetFocusOnError="true" Style="float: left; margin-left: 5px; margin-top:10px;"
                ID="revtxtGroupNum1" runat="server" ErrorMessage="Please enter alphanumeric values."
                ControlToValidate="groupnumber" CssClass="error" ValidationExpression="^[0-9a-zA-Z]+$"
                Display="Dynamic" ValidationGroup="group1"></asp:RegularExpressionValidator>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Secondary Insurance</div>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Financial Class</div>
            <asp:TextBox ID="secondaryfinancialclass" runat="server" CssClass="standard_textbox"
                ClientIDMode="Static"></asp:TextBox>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Company Name</div>
            <asp:TextBox ID="secondarycompanyname" runat="server" CssClass="standard_textbox"
                ClientIDMode="Static"></asp:TextBox>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Policy Number</div>
            <asp:TextBox ID="secondarypolicynumber" runat="server" CssClass="standard_textbox"
                ClientIDMode="Static"></asp:TextBox>
            <asp:RegularExpressionValidator SetFocusOnError="true" Style="float: left; margin-left: 5px; margin-top:10px;"
                ID="revtxtpolicyno1" runat="server" ErrorMessage="Please enter alphanumeric values."
                ControlToValidate="secondarypolicynumber" CssClass="error" ValidationExpression="^[0-9a-zA-Z]+$"
                Display="Dynamic" ValidationGroup="group1"></asp:RegularExpressionValidator>
        </div>
        <div class="patsearch_formcontrol">
            <div class="standard_label1">
                Group Number</div>
            <asp:TextBox ID="secondarygroupnumber" runat="server" CssClass="standard_textbox"
                ClientIDMode="Static"></asp:TextBox>
            <asp:RegularExpressionValidator SetFocusOnError="true" Style="float: left; margin-left: 5px; margin-top:10px;"
                ID="revtxtGroupNum2" runat="server" ErrorMessage="Please enter alphanumeric values."
                ControlToValidate="secondarygroupnumber" CssClass="error" ValidationExpression="^[0-9a-zA-Z]+$"
                Display="Dynamic" ValidationGroup="group1"></asp:RegularExpressionValidator>
        </div>
        <div id="btn_groups">
            <div class="standard_label" style="margin-right: -10px;">
            </div>
            <%--   <input type="button" value="LOOKUP" id="btn_lookup" class="btn_standard" runat="server" onclick="btn_lookup_Click" />--%>
            <div id="divbtns">
                <asp:Button Text="Save" ValidationGroup="group1" ID="btn_lookup" CssClass="btn_standard"
                    runat="server" OnClick="btn_lookup_Click" />
            </div>
            <span id="imgsubmitprg" style="display: none; padding-left: 10px;">&nbsp;Processing
                please wait....<br />
                <img src="../../Images/fileupload-loader.gif" alt="" /></span>
        </div>
    </div>
</div>
<script type="text/javascript">
    $('#dob').datepicker();
    $('#calendar_img').click(function () {
        $('#dob').datepicker('show');
    });
    function showProgress() {
        $("#divbtns").hide();
        $("#imgsubmitprg").show();
    }

    $("#div_patientsearch").css("display", "block");

    $("#patientsearch_leftpart").css("display", "block");
    $("#imgsearchuser").css("display", "block");
    function onddlChange(value) {

        if (value == "Yes") {
            document.getElementById('cplPatientLookUp_uccreatepatient_divAlternate').style.display = "";
        }
        else {
            document.getElementById('cplPatientLookUp_uccreatepatient_divAlternate').style.display = "none";
            $("#cplPatientLookUp_uccreatepatient_txtalterAddress1").val("");
            $("#cplPatientLookUp_uccreatepatient_txtalterAddress1").val("");
            $("#cplPatientLookUp_uccreatepatient_txtalterAddress2").val("");
            $("#cplPatientLookUp_uccreatepatient_txtaltercity").val("");
            $("#cplPatientLookUp_uccreatepatient_txtalterzip").val("");
            $("#cplPatientLookUp_uccreatepatient_ddlalternatestate").val('Select');
        }
    }

    $("#popup_close").click(function () {
        parent.popup_close();
    });
</script>
<style type="text/css">
    .standard_label1
    {
        font-size: 14px;
        font-weight: 600;
        margin: 5px;
        padding-top: 1px;
        width: 245px;
    }
</style>
