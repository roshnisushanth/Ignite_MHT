using IGNITE_MODEL.Encounters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.Encounters.UserControls
{
    public partial class Conditions : System.Web.UI.UserControl
    {
        protected cWrapper conditions = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            var aId = Request.QueryString["aId"];
            EncounterService service = new EncounterService();
            conditions = service.GetConditions(Convert.ToInt64(aId));
            asId.Value = aId;
        }
    }
}