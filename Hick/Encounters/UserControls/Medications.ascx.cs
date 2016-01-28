using IGNITE_MODEL.Encounters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.Encounters.UserControls
{
    public partial class Medications : System.Web.UI.UserControl
    {
        protected mWrapper medications = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            var aId = Request.QueryString["aId"];
            EncounterService service = new EncounterService();
            medications = service.GetMedications(Convert.ToInt64(aId));
            asId.Value = aId;
        }
    }
}