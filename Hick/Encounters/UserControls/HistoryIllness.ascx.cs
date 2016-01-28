using Hick.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.Encounters.UserControls
{
    public partial class HistoryIllness : System.Web.UI.UserControl
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            var aId = Request.QueryString["aId"]; // TODO
            EncounterService service = new EncounterService();
            var presentIllness = service.GetPresentIllness(Convert.ToInt64(aId));
            dob.Text = presentIllness.Date.ToString(Utility.GlobalDateFormat);
            time.Text = presentIllness.Time;
            txtNotes.Text = presentIllness.Notes;
            objid.Value = Convert.ToString(presentIllness.Id);
            asId.Value = aId;
        }
    }
}