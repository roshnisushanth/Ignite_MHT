using Hick.Models;
using IGNITE_MODEL.Encounters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.Encounters.UserControls
{
    public partial class BabyPCP : System.Web.UI.UserControl
    {
        protected BabyPCPhy babyPCP = null;
        protected List<StatesMaster> states = new List<StatesMaster>();

        protected void Page_Load(object sender, EventArgs e)
        {
            var aId = Request.QueryString["aId"];
            var pm = Request.QueryString["pm"];
            EncounterService service = new EncounterService();
            babyPCP = service.GetBabyPCP(Convert.ToInt64(aId));
            states = service.GeStatesMaster();
            momobid.Value = babyPCP.Id.ToString();
            asId.Value = aId;
            phy_name.Text = babyPCP.Name;
            ph_no.Text = babyPCP.Phone;
            address1.Text = babyPCP.Address1;
            address2.Text = babyPCP.Address2;
            city.Text = babyPCP.City;
            zip.Text = babyPCP.ZipCode;
        }
    }
}