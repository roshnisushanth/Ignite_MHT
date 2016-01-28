using IGNITE_MODEL.Encounters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.Encounters.UserControls
{
    public partial class BreastFeedingBaby : System.Web.UI.UserControl
    {
        protected List<FeedingChild> bfBabyList = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            var aId = Request.QueryString["aId"];
            EncounterService service = new EncounterService();
            bfBabyList = service.GetFeedingBabies(Convert.ToInt64(aId));
            asId.Value = aId;
        }

      

    }
}