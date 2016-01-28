using IGNITE_BLL;
using IGNITE_MODEL.Encounters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.Encounters.ASPX
{
    public partial class Assessments : Hick.Base.BasePage
    {
        protected SummaryWrapper summaries = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            userId.Value = Convert.ToString(Session["userid"]);
            peerId.Value = Request.QueryString["peerId"];
            Session["peerId"] = peerId.Value;
            //var patientId = Convert.ToString(Session["patientid"]);
            //var userId = Convert.ToString(Session["userid"]);

            EncounterService service = new EncounterService();
            summaries = service.GetAssessmentSummaries(Convert.ToInt64(peerId.Value));
        }
    }
}