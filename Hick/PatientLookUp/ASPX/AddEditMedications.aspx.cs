using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hick.Models;
using System.Configuration;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace Hick.PatientLookUp.ASPX
{
    public partial class AddEditMedications : Hick.Base.BasePage
    {
        public long PatientID
        {
            get
            {

                return Convert.ToInt64(ViewState["PatientID"]);
            }
            set
            {
                ViewState["PatientID"] = value;
            }
        }
        public long MedicationID
        {
            get
            {

                return Convert.ToInt64(ViewState["MedicationID"]);
            }
            set
            {
                ViewState["MedicationID"] = value;
            }
        }
        public string PIN
        {
            get
            {

                return ViewState["PIN"].ToString();
            }
            set
            {
                ViewState["PIN"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PatientID = Session["PatientID"] != null ? Convert.ToInt64(Session["PatientID"]) : 0;
                PIN = ConfigurationManager.AppSettings["Bridge_Base"];
                MedicationID = !String.IsNullOrEmpty(Request.QueryString["cid"]) ? Convert.ToInt64(Request.QueryString["cid"]) : 0;

                if (PatientID != 0 && MedicationID != 0)
                {
                    BindDetails();
                }

            }
        }

        public void BindDetails()
        {
            Hick.Models.Medications obj = HttpContext.Current.Cache["EditMedications"] as Hick.Models.Medications;
            if (obj != null)
            {
                medications.Text = obj.Description;
                dosage.Text = obj.Dosage;
                txtdate.Text = obj.Date;
                if (obj.DosageUnits != "")
                {
                    drp_dosageunits.Items.FindByText(obj.DosageUnits).Selected = true;
                }
                
                // drp_dosageunits.Text = 
                // hdndesc.Value = obj.MedicationID;
            }

        }
        protected void savemedications(object sender, EventArgs e)
        {
//            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) from users where user_name like @username AND password like @password", sqlConnection))
//            {
//                SqlConnection sqlconnection = new SqlConnection()
//                sqlConnection.Open();
//                sqlCommand.Parameters.AddWithValue("@username", userName);
//                sqlCommand.Parameters.AddWithValue("@password", passWord);
//                int userCount = (int)sqlCommand.ExecuteScalar();
   
//}

            SaveDetails();
        }

        public void SaveDetails()
        {
            try
            {
                var uri = Utility.GetServiceUrl("updatemedications");

                IgJObject postData = new IgJObject();
                postData.Add("PatientID", PatientID);
                postData.Add("Description", medications.Text);
                postData.Add("MedicationCheck", radioMedications.SelectedValue);
                postData.Add("MedicationID", MedicationID);
                postData.Add("Date", txtdate.Text);
                postData.Add("Dosage", dosage.Text);
                postData.Add("DosageUnits", drp_dosageunits.SelectedItem.Text);

                var res = Utility.PostRequestForSave(uri, postData.ToString(Formatting.None));

                ClientScript.RegisterStartupScript(GetType(), "scrpt", "parent.window.location.href=parent.window.location.href;", true);
            }
            catch (Exception exc)
            {

            }

        }
    }
}