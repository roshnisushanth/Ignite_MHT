using IGNITE_MODEL.Encounters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.Encounters.UserControls
{
    public partial class MomsOB : System.Web.UI.UserControl
    {
        protected MomOB momOB = null;
        protected List<StatesMaster> states = new List<StatesMaster>();
        protected void Page_Load(object sender, EventArgs e)
        {
            var aId = Request.QueryString["aId"];
            var pm = Request.QueryString["pm"];
            EncounterService service = new EncounterService();
            momOB = service.GetMomOB(Convert.ToInt64(aId));
            states = service.GeStatesMaster();
            momobid.Value = momOB.Id.ToString();
            asId.Value = aId;
            phy_name.Text = momOB.Name;
            ph_no.Text = momOB.Phone;
            address1.Text = momOB.Address1;
            address2.Text = momOB.Address2;
            city.Text = momOB.City;
            zip.Text = momOB.ZipCode;
        }
    }
}