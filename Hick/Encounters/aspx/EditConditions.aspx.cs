using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hick.Models;
using System.Configuration;
using System.Web.Services;
using System.Web.Script.Services;
using IGNITE_MODEL.Encounters;
using System.Data.SqlTypes;

namespace Hick.Encounters.ASPX
{
    public partial class EditConditions : Hick.Base.BasePage
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<MasterICD9Codes> GetMasterICD9CodesAutoComplete(string pre)
        {
            EncounterService service = new EncounterService();
            return service.GetMasterICD9CodesAutoComplete(pre);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var pm = Request.QueryString["pm"];
            pageMode.Value = pm;
            EncounterService service = new EncounterService();
            if (pm == "edit")
            {
                var conId = Request.QueryString["conId"];
                var condition = service.GetCondition(Convert.ToInt64(conId)); // TODO
                CodeConditions.Text = condition.ICDCode;
                OnsetDate.Text = condition.ActiveDate.ToString(Utility.GlobalDateFormat); //TODO
                var atv = condition.IsActive;
                radioconditions.Items[0].Selected = atv;
                radioconditions.Items[1].Selected = !atv;
                inactivescience.Text = condition.InActiveDate == SqlDateTime.MinValue ? string.Empty : condition.InActiveDate.ToString(Utility.GlobalDateFormat); // TODO
                cId.Value = condition.Id.ToString();
            }
            else if (pm == "add")
            {
                var aId = Request.QueryString["aId"];
                asId.Value = aId;

            }
        }
    }
}
