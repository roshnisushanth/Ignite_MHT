<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditFamilyHistory.aspx.cs"
    Inherits="Hick.PatientLookUp.ASPX.AddEditFamilyHistory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/patientlookup.css" rel="stylesheet" />
    <link href="~/Content/style.css" rel="stylesheet" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="https://code.jquery.com/ui/1.11.1/jquery-ui.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtOnsetDate').datepicker({ maxDate: new Date() });
            $('#calImginactive').click(function () {
                $('#txtOnsetDate').datepicker('show');
            });


            $("#cancle_edit_family").click(function () {
                parent.window.location.href = parent.window.location.href;
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:HiddenField ID="hdndesc" runat="server" Value="test" />
            <div class="edit_conditionsdiv" style="float: left; margin: 6px; width: 100%;">
                <div id="Div1" class="popup_content" runat="server">
                    <div class="popup_conter">
                        <div class="form-lft">
                            <req>*</req>
                            Relationship
                        </div>
                        <div class="form-rgt">
                            <asp:DropDownList ID="ddlRelationShip" runat="server">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>Brother</asp:ListItem>
                                <asp:ListItem>Daughter</asp:ListItem>
                                <asp:ListItem>Father</asp:ListItem>
                                <asp:ListItem>Maternal Grandfather</asp:ListItem>
                                <asp:ListItem>Maternal Grandmother</asp:ListItem>
                                <asp:ListItem>Mother</asp:ListItem>
                                <asp:ListItem>Paternal Grandfather</asp:ListItem>
                                <asp:ListItem>Paternal Grandmother</asp:ListItem>
                                <asp:ListItem>Sister</asp:ListItem>
                                <asp:ListItem>Son</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator Display="Dynamic" ID="reqRelationship" CssClass="error"
                                ControlToValidate="ddlRelationShip" ErrorMessage="Relationship is required" runat="server"
                                InitialValue="Select" Style="text-align: left;"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="popup_conter" style="align-items: center;">
                        <div class="form-lft">
                            <req>*</req>
                            Condition
                        </div>
                        <div class="form-rgt">
                            <asp:DropDownList ID="ddlConditions" runat="server" onchange="HideOthetTextBox();"
                                Style="margin-right: 20px;">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>Acquired Immunodeficiency Syndrome</asp:ListItem>
                                <asp:ListItem>Arthritis</asp:ListItem>
                                <asp:ListItem>Asthma</asp:ListItem>
                                <asp:ListItem>Bronchitis</asp:ListItem>
                                <asp:ListItem>Cancer</asp:ListItem>
                                <asp:ListItem>Chlamydia</asp:ListItem>
                                <asp:ListItem>Chronic Heart Failure (CHF)</asp:ListItem>
                                <asp:ListItem>Chronic Obstructive Pulmonary Disease (COPD)</asp:ListItem>
                                <asp:ListItem>Coronary Artery Disease (CAD)</asp:ListItem>
                                <asp:ListItem>Diabetes Mellitus Type 1</asp:ListItem>
                                <asp:ListItem>Diabetes Mellitus Type 2</asp:ListItem>
                                <asp:ListItem>Dizziness</asp:ListItem>
                                <asp:ListItem>Emphysema</asp:ListItem>
                                <asp:ListItem>Epilepsy</asp:ListItem>
                                <asp:ListItem>Eye Problem</asp:ListItem>
                                <asp:ListItem>Fainting</asp:ListItem>
                                <asp:ListItem>Frequent or Severe Headaches</asp:ListItem>
                                <asp:ListItem>Glaucoma</asp:ListItem>
                                <asp:ListItem>Gonorrhea</asp:ListItem>
                                <asp:ListItem>Hearing Impairment</asp:ListItem>
                                <asp:ListItem>Heart Condition</asp:ListItem>
                                <asp:ListItem>Hemodialysis</asp:ListItem>
                                <asp:ListItem>Herpes</asp:ListItem>
                                <asp:ListItem>High Blood Cholesterol</asp:ListItem>
                                <asp:ListItem>High Blood Pressure</asp:ListItem>
                                <asp:ListItem>Hypertension (HTN)</asp:ListItem>
                                <asp:ListItem>Hypoglycemia</asp:ListItem>
                                <asp:ListItem>Ischemic Vascular Disease (IVD)</asp:ListItem>
                                <asp:ListItem>Jaundice</asp:ListItem>
                                <asp:ListItem>Kidney Disease</asp:ListItem>
                                <asp:ListItem>Low Blood Pressure</asp:ListItem>
                                <asp:ListItem>Mental Retardation</asp:ListItem>
                                <asp:ListItem>Pain or Pressure in Chest</asp:ListItem>
                                <asp:ListItem>Palpitations</asp:ListItem>
                                <asp:ListItem>Periods of unconsciousness</asp:ListItem>
                                <asp:ListItem>Rheumatic Fever</asp:ListItem>
                                <asp:ListItem>Rheumatism</asp:ListItem>
                                <asp:ListItem>Seizures</asp:ListItem>
                                <asp:ListItem>Shortness of Breath</asp:ListItem>
                                <asp:ListItem>Stomach Liver or Intestinal Problems</asp:ListItem>
                                <asp:ListItem>Syphilis</asp:ListItem>
                                <asp:ListItem>Tuberculosis</asp:ListItem>
                                <asp:ListItem>Tumor</asp:ListItem>
                                <asp:ListItem>Thyroid Problems</asp:ListItem>
                                <asp:ListItem>Urinary Tract Infection</asp:ListItem>
                                <asp:ListItem>Other</asp:ListItem>
                            </asp:DropDownList>
                        </div>


                    </div>

                    <div class="popup_conter txtInctive" style="align-items: center;">
                        <div class="form-lft">
                            <req>*</req>
                            Onset Date
                        </div>
                        <div class="form-rgt" style="position: relative;">
                            <asp:TextBox ID="txtOnsetDate" runat="server" CssClass="popup_textbox" ClientIDMode="Static"
                                Style="margin-left: 0px; width: 100%;" ></asp:TextBox>

                            <img src="../../Images/calendar.jpg" id="calImginactive" class="txtbox_spanimg" alt="calendar"
                                style="position: absolute; top: 2px;" />

                            <asp:RequiredFieldValidator Display="Dynamic" ID="reqOnsetDate" CssClass="error"
                                ControlToValidate="txtOnsetDate" ErrorMessage="Onset Date date required" runat="server"
                                Style="text-align: left;"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="popup_conter">
                        <asp:Button ID='btnSaveCondition' CssClass='btn_standard' runat='server' Text='Save'
                            OnClick="btnSaveCondition_Click"></asp:Button>
                        <input type="button" value="Cancel" name="cancle_edit_conditions" class="btn_standard"
                            id="cancle_edit_family" />
                    </div>
                </div>
            </div>
    </form>
</body>
</html>
