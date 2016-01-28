using Hick.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.PatientLookUp.ASPX
{
    public partial class AddEditTestAndProcedures : System.Web.UI.Page
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
        public long SurgeriesId
        {
            get
            {

                return Convert.ToInt64(ViewState["SurgeriesId"]);
            }
            set
            {
                ViewState["SurgeriesId"] = value;
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
                SurgeriesId = !String.IsNullOrEmpty(Request.QueryString["cid"]) ? Convert.ToInt64(Request.QueryString["cid"]) : 0;

                if (PatientID != 0 && SurgeriesId != 0)
                {
                    BindDetails();
                    buttontype.Value = "Update";
                }

            }
        }
        public void BindDetails()
        {
            Hick.Models.Surgeries obj = HttpContext.Current.Cache["EditTestAndProcedures"] as Hick.Models.Surgeries;
            if (obj != null)
            {
               testProcedure.Text = obj.SurgeriesProcedure;
               testdate.Text = obj.Date;
               details.Text= obj.Description;

            }

        }
        protected void saveProcedure(object sender, EventArgs e)
        {
            SaveDetails();
        }
        public void SaveDetails()
        {
            try
            {
                string action = "";
                if (buttontype.Value != "Update")
                {
                   action = "INSERT";
               }
               else
               {
                   action = "UPDATE";
               }

                var uri = Utility.GetServiceUrl("updatetestandsurgeries");

                IgJObject postData = new IgJObject();
                postData.Add("PatientID", PatientID);
                postData.Add("UserId", Convert.ToString(Session["PhysicianID"]));
                postData.Add("SurgeriesProcedure", testProcedure.Text);
                postData.Add("Date", testdate.Text);
                postData.Add("Description", details.Text);
                postData.Add("SurgeriesId", SurgeriesId);
                postData.Add("ActionItem", action);

                var res = Utility.PostRequestForSave(uri, postData.ToString(Formatting.None));
                ClientScript.RegisterStartupScript(GetType(), "scrpt", "parent.window.location.href=parent.window.location.href;", true);

            }
            catch (Exception exc)
            {

            }

        }
    }
}