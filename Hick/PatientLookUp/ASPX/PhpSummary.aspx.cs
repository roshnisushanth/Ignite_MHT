using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace Hick.PatientLookUp.ASPX
{
    public partial class PhpSummary : Hick.Base.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string BindGraphs()
        {


            string xml = HttpContext.Current.Session["groupedData"] == null ? "" : HttpContext.Current.Session["groupedData"].ToString();


            return xml;

        }
    }
}