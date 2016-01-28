using Hick.Models;
using IGNITE_MODEL.Encounters;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.Encounters.ASPX
{
    public partial class AddEditMedications : Hick.Base.BasePage
    {
        protected Medication medication = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            var pm = Request.QueryString["pm"];
            pageMode.Value = pm;
            EncounterService service = new EncounterService();
            if (pm == "edit")
            {
                var medId = Request.QueryString["medId"];
                mId.Value = medId;
                medication = service.GetMedication(Convert.ToInt64(medId)); 
                medications.Text = medication.Medicine;
                dosage.Text = medication.Dosage;
                var atv = medication.IsActive;
                radioMedications.Items[0].Selected = atv;
                radioMedications.Items[1].Selected = !atv;

                startDate.Text = medication.StartDate == SqlDateTime.MinValue ? string.Empty : medication.StartDate.ToString(Utility.GlobalDateFormat);  
                medicationStop.Text = medication.StopDate == SqlDateTime.MinValue ? string.Empty : medication.StartDate.ToString(Utility.GlobalDateFormat);
            }
            else if (pm == "add")
            {
                var aId = Request.QueryString["aId"];
                asId.Value = aId;

            }
        }
    }
}