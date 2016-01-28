using Hick.Authorized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IGNITE_MODEL.SessionNote;
using System.Globalization;

namespace Hick.PatientLookUp.ASPX
{
    public partial class SessionNote : Hick.Base.BasePage
    {
        public List<IGNITE_MODEL.SessionNote.SessionNote> sessionNote= new List<IGNITE_MODEL.SessionNote.SessionNote>();
        AuthorizedService service = new AuthorizedService();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            string userid = Session["userid"].ToString();
            string peerId = Request.QueryString["peerId"];
            sessionNote = (from session in service.GetSessionNote(userid, Convert.ToInt32(peerId), 0, 1)
                           select new IGNITE_MODEL.SessionNote.SessionNote() {
                               Id=session.Id,
                               Date=session.Date,
                               Category=session.Category,
                               Note=session.Note,
                               StartTime= session.StartTime,
                               EndTime =(session.EndTime== "00:00") ? session.StartTime: session.EndTime,
                               TotalTime=session.TotalTime
                           }).ToList();
           
        }
    }
}