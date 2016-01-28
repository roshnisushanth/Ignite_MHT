using Hick.Authorized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.PatientLookUp.ASPX
{
    public partial class AjaxSessionNoteModification : System.Web.UI.Page
    {
        AuthorizedService service = new AuthorizedService();
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request.QueryString["action"];
            string id = Request.QueryString["id"];
            string note = (Request.QueryString["note"].ToString() != "") ? Request.QueryString["note"].ToString() : "";
            //if (action == "edit")
            //{
            //    modify(action, Convert.ToInt32(id), note);
            //}
            //else if (action == "delete")
            //{
            //    modify(action, Convert.ToInt32(id), note);
            //}
            modify(action, Convert.ToInt32(id), note);
        }

        public void modify(string action, int id, string note)
        {
            var SessionNote = service.SessionNoteModification(action, id, note);
            if (action == "SelectById")
                Response.Write(SessionNote.Note);
            else
                Response.Write("Success!");
        }
    }
}