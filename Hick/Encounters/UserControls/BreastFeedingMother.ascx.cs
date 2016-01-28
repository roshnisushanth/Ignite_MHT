using IGNITE_MODEL.Encounters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.Encounters.UserControls
{
    public partial class BreastFeedingMother : System.Web.UI.UserControl
    {
        protected List<FeedingMother> bfMotherList = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            var aId = Request.QueryString["aId"];
            EncounterService service = new EncounterService();
            bfMotherList = service.GetFeedingMothers(Convert.ToInt64(aId));
            asId.Value = aId;
        }
    }
}