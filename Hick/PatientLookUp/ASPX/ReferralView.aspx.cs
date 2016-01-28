using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace Hick.PatientLookUp.ASPX
{
    public partial class ReferralView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static void CacheDetails(string ReferralID)
        {


            Hick.Models.Referral obj = new Hick.Models.Referral();
            obj.ReferralID = Convert.ToInt32(ReferralID);
            //obj.DoctorName = DoctorName;
            //obj.VisitReason = VisitReason;
            //obj.VisitDiagnosis = VisitDiagnosis;


            HttpContext.Current.Cache["EditReferrals"] = obj;
        }
    }
}