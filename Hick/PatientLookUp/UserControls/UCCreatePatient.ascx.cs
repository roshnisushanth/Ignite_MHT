using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hick.Models;
using System.Net;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;
using System.Configuration;
using Newtonsoft.Json;

namespace Hick.PatientLookUp.UserControls
{
    public partial class UCCreatePatient : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_lookup_Click(object sender, EventArgs e)
        {
            try
            {
                List<CreatePatient> objColl = null;
                //var random = "no";
                var randomnumber = 0;
                //do
                //{
                randomnumber = GetRandomNumber();
                //} while (random == "no");
                string PinValue = ConfigurationManager.AppSettings["Bridge_Base"];
                var uri = Utility.GetServiceUrl("createpatient");

                IgJObject postData = new IgJObject();
                postData.Add("Pin", PinValue);
                postData.Add("PatientID", randomnumber);
                postData.Add("LastName", lastname.Text);
                postData.Add("FirstName", firstname.Text);
                postData.Add("MiddleName", middlename.Text);
                postData.Add("MaidenName", maidenname.Text);
                postData.Add("Last4SSN", lastssn.Text);
                postData.Add("DOB", dob.Text);
                postData.Add("Gender", rbGender.SelectedValue);
                postData.Add("Race", ddlRace.SelectedValue);
                postData.Add("Maritalstatus", ddlMarital.SelectedValue);
                postData.Add("Address1", address1.Text);
                postData.Add("Address2", address2.Text);
                postData.Add("City", city.Text);
                postData.Add("State", ddlState.SelectedValue);
                postData.Add("Zip", zip.Text);
                postData.Add("AlterAddress1", alteraddress1.Text);
                postData.Add("AlterAddress2", alteraddress2.Text);
                postData.Add("AlterCity", altercity.Text);
                postData.Add("AlterState", ddlalternatestate.SelectedValue);
                postData.Add("AlterZip", alterzip.Text);
                postData.Add("PhoneNumber", homephone.Text);
                postData.Add("WorkPhone", workphone.Text);
                postData.Add("CellPhone", cellphone.Text);
                postData.Add("Email", email.Text);
                postData.Add("Height", heightft.Text + "." + heightin.Text);
                postData.Add("Weight", weight.Text);
                postData.Add("EyeColor", eyecolor.Text);
                postData.Add("HairColor", haircolor.Text);
                postData.Add("BloodType", ddlBlood.SelectedValue);
                postData.Add("Birthmark", birthmark.Text);
                postData.Add("ReligiousPreferences", religiouspreferences.Text);
                postData.Add("Specialconditions", specialconditions.Text);
                postData.Add("PrimaryFinancialClass", financialclass.Text);
                postData.Add("CompanyName", companyname.Text);
                postData.Add("PrimaryPolicyNumber", policynumber.Text);
                postData.Add("Ins1GroupNo", groupnumber.Text);
                postData.Add("SecondaryFinancialClass", secondaryfinancialclass.Text);
                postData.Add("SecondryCompanyName", secondarycompanyname.Text);
                postData.Add("SecondaryPolicyNumber", secondarypolicynumber.Text);
                postData.Add("Ins2GroupNo", secondarygroupnumber.Text);

                var res = Utility.PostRequestForSave(uri, postData.ToString(Formatting.None));

                //var uri = ConfigurationManager.AppSettings["serviceURL"] + "/createpatient"; 

                //var postData = "{\"Pin\":\"1111\",\"PatientID\":\"" + randomnumber + "\",\"LastName\":\"" + lastname.Text + "\",\"FirstName\":\"" + firstname.Text +
                //    "\",\" MiddleName\":\"" + middlename.Text + "\",\"MaidenName\":\"" + maidenname.Text + "\",\"Last4SSN\":\"" + lastssn.Text +
                //    "\",\"DOB\":\"" + dob.Text + "\",\"Gender\":\"" + rbGender.SelectedValue + "\",\"Race\":\"" + ddlRace.SelectedValue +
                //    "\",\"Maritalstatus\":\"" + ddlMarital.SelectedValue + "\",\"Address1\":\"" + address1.Text + "\",\"Address2\":\"" + address2.Text +
                //    "\",\"City\":\"" + city.Text + "\",\"State\":\"" + ddlState.SelectedValue + "\",\"Zip\":\"" + zip.Text +
                //    "\",\" AlterAddress1\":\"" + alteraddress1.Text + "\",\"AlterAddress2\":\"" + alteraddress2.Text + "\",\"AlterCity\":\"" + altercity.Text +
                //    "\",\"AlterState\":\"" + ddlalternatestate.SelectedValue + "\",\"AlterZip\":\"" + alterzip.Text + "\",\"PhoneNumber\":\"" + homephone.Text +
                //    "\",\"WorkPhone\":\"" + workphone.Text + "\",\"CellPhone\":\"" + cellphone.Text + "\",\"Email\":\"" + email.Text +
                //    "\",\"Height\":\"" + heightft.Text + "." + heightin.Text + "\",\"Weight\":\"" + weight.Text + "\",\"EyeColor\":\"" + eyecolor.Text +
                //    "\",\" HairColor\":\"" + haircolor.Text + "\",\"BloodType\":\"" + ddlBlood.SelectedValue + "\",\"Birthmark\":\"" + birthmark.Text +
                //    "\",\"ReligiousPreferences\":\"" + religiouspreferences.Text + "\",\"Specialconditions\":\"" + specialconditions.Text + "\",\"PrimaryFinancialClass\":\"" + financialclass.Text +
                //   "\",\"CompanyName\":\"" + companyname.Text + "\",\"PrimaryPolicyNumber\":\"" + policynumber.Text + "\",\"Ins1GroupNo\":\"" + groupnumber.Text +
                    
                //   "\",\"SecondaryFinancialClass\":\"" + secondaryfinancialclass.Text + "\",\"SecondryCompanyName\":\"" + secondarycompanyname.Text + "\",\"SecondaryPolicyNumber\":\"" + secondarypolicynumber.Text +
                //    "\",\"Ins2GroupNo\":\"" + secondarygroupnumber.Text + "\"}";
                //var res = Utility.PostRequestForSave(uri, postData);

                if (Convert.ToInt32(res) > 0)
                {
                    Response.Redirect("~/PatientLookUp/ASPX/PhpSummary.aspx?ptid=" + res + "", false);
                }

            }
            catch (Exception exc)
            {

            }
        }

        public string GenerateStrongPassword(int length)
        {

            string numerics = "123456789";

            //create another string which is a concatenation of all above
            string allChars = numerics;
            Random r = new Random();
            String generatedPassword = "";

            int pNumber;
            string posArray = "123456789";
            if (length < posArray.Length)
                posArray = posArray.Substring(0, length);
            string randomChar = posArray.ToCharArray()[(int)Math.Floor(r.NextDouble() * posArray.Length)].ToString();
            randomChar = posArray.ToCharArray()[(int)Math.Floor(r.NextDouble() * posArray.Length)].ToString();
            randomChar = posArray.ToCharArray()[(int)Math.Floor(r.NextDouble() * posArray.Length)].ToString();
            pNumber = int.Parse(randomChar); posArray = posArray.Replace(randomChar, "");
            randomChar = posArray.ToCharArray()[(int)Math.Floor(r.NextDouble() * posArray.Length)].ToString();
            for (int i = 0; i < length; i++)
            {
                double rand = r.NextDouble();
                if (i == pNumber)
                    generatedPassword += numerics.ToCharArray()[(int)Math.Floor(rand * numerics.Length)];
                else
                    generatedPassword += allChars.ToCharArray()[(int)Math.Floor(rand * allChars.Length)];
            }
            return generatedPassword;
        }
        public int GetRandomNumber()
        {
            var b = GenerateStrongPassword(5);
            return Convert.ToInt32(b);
        }
    
    }
}