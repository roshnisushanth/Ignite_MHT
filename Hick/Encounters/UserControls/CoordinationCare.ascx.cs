using Hick.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.Encounters.UserControls
{
    public partial class CoordinationCare : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var aId = Request.QueryString["aId"]; // TODO
            EncounterService service = new EncounterService();
            var coordinationOfCare = service.GetCoordicationOfCare(Convert.ToInt64(aId));
            dob.Text = coordinationOfCare.Date.ToString(Utility.GlobalDateFormat);
            time.Text = coordinationOfCare.Time;
            txtNotes.Text = coordinationOfCare.Notes;
            objid.Value = Convert.ToString(coordinationOfCare.Id);
            asId.Value = aId;
        }
    }
}