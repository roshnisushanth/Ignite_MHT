using Hick.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.Encounters.UserControls
{
    public partial class Assessmnt : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var aId = Request.QueryString["aId"]; // TODO
            EncounterService service = new EncounterService();
            var assessment = service.GetAssessment(Convert.ToInt64(aId));
            dob.Text = assessment.Date.ToString(Utility.GlobalDateFormat);
            time.Text = assessment.Time;
            txtNotes.Text = assessment.Notes;
            objid.Value = Convert.ToString(assessment.Id);
            asId.Value = aId;
        }
    }
}