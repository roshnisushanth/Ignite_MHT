using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.Encounters
{
    public partial class EncounterMaster : System.Web.UI.MasterPage
    {
        protected bool isreadonly = false;
        protected void Page_PreRender(object sender, EventArgs e) 
        {
            if (Page.MetaKeywords != null)
            {
                string pkey = Convert.ToString(Page.MetaKeywords).ToLower();
                hdnpagekey.Value = pkey;
            }
        
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var userType = Session["UserType"];
            isreadonly = Convert.ToString(userType).Equals("Patient");

            pageMode.Value = Convert.ToString(Request.QueryString["pm"]);
            assessmentid.Value = Convert.ToString(Request.QueryString["aId"]);
        }
    }
}