using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hick.PHP;
using IGNITE_MODEL.PHPView;
namespace Hick.PatientLookUp.UserControls
{
    public partial class UCClinicalDocuments : System.Web.UI.UserControl
    {
        PHPService PHPService = new PHPService();
        protected void Page_Load(object sender, EventArgs e)
        {
            ClinicalDocumentList clinicaldoc = new ClinicalDocumentList();
            clinicaldoc = PHPService.GetClinicalDoc("selectAll",0,Convert.ToInt32(Session["PatientID"].ToString()));
            gdclicnicaldoc.DataSource = clinicaldoc.ClinicalDocumentLists;
            gdclicnicaldoc.DataBind();
        }



    }
}