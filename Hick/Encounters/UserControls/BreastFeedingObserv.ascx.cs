using IGNITE_MODEL.Encounters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.Encounters.UserControls
{
    public partial class BreastFeedingObserv : System.Web.UI.UserControl
    {
        protected FeedingObservations observation = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            var aId = Request.QueryString["aId"]; // TODO
            EncounterService service = new EncounterService();
            observation = service.GetFeedingObservations(Convert.ToInt64(aId));
            if(observation.IsObserved)
            {
                radioconditions.Items[0].Selected = true;

            }
            else
            {
                radioconditions.Items[1].Selected = true;
            }
            asId.Value = aId;
        }
    }
}