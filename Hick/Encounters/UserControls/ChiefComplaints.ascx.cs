using IGNITE_MODEL.Encounters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.Encounters.UserControls
{
    public partial class ChiefComplaints : System.Web.UI.UserControl
    {
        protected List<Complaints> babies = null;
        protected List<Complaints> mothes = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            var aId = Request.QueryString["aId"]; // TODO
            EncounterService service = new EncounterService();
            babies = service.GetChiefComplaints(Convert.ToInt64(aId),true);
            mothes = service.GetChiefComplaints(Convert.ToInt64(aId),false);
            asId.Value = aId;
        }
    }
}