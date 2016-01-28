using IGNITE_DAL.DataObjects;
using IGNITE_DAL.Interfaces;
using IGNITE_MODEL.Encounters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.Encounters.UserControls
{
    public partial class AssessmentSummary : System.Web.UI.UserControl
    {
        protected EncounterAssessmentSummary summary;
        protected void Page_Load(object sender, EventArgs e)
        {

            var aId = Request.QueryString["aId"]; // TODO
            var userid =Convert.ToString(Session["userid"]);
            var peerid = Session["peerId"];
            EncounterService service = new EncounterService();
            summary = service.GetCompleteAssessmentSummary(Convert.ToInt64(aId), Convert.ToInt64(peerid));
            asId.Value = aId;
        }
    }
}