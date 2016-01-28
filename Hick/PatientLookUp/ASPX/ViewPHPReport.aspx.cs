using Hick.Authorized;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.PatientLookUp.ASPX
{
    public partial class ViewPHPReport : System.Web.UI.Page
    {
        AuthorizedService service = new AuthorizedService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["pageview"]))
            {
                if(Request.QueryString["pageview"].ToString()=="view")
                {
                    var auditresult = service.AddAuditLog(Convert.ToInt32(Session["userid"].ToString()),0, "PHP", "Not Applicable", "Not Applicable", DateTime.Now);

                }
            }
        }


    }
}