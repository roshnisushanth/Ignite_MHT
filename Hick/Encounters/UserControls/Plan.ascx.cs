using Hick.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.Encounters.UserControls
{
    public partial class Plan : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var aId = Request.QueryString["aId"]; // TODO
            EncounterService service = new EncounterService();
            var plan = service.GetPlan(Convert.ToInt64(aId));
            dob.Text = plan.Date.ToString(Utility.GlobalDateFormat);
            time.Text = plan.Time;
            txtNotes.Text = plan.Notes;
            objid.Value = Convert.ToString(plan.Id);
            asId.Value = aId;
        }
    }
}