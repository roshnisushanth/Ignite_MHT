using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.Encounters.aspx
{
    public partial class Medications : Hick.Base.BasePage
    {
        string patientid = "";
        string userid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdnPatientId.Value = Request.QueryString["PatientId"];
                hdnUserId.Value = Request.QueryString["UserId"];

                patientid = hdnPatientId.Value;
                userid = hdnUserId.Value;
                if (!string.IsNullOrEmpty(patientid) && !string.IsNullOrEmpty(userid))
                {
                    Session["patientid"] = patientid;
                    Session["userid"] = userid;
                }
            }
        }
    }
}