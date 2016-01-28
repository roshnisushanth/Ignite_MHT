using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.CommandCenter.UserControls
{
    public partial class ConsentReport : System.Web.UI.UserControl
    {
        protected string PhyName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if(!IsPostBack)
            {
                if (Session["userid"] != null)
                {
                    HickChatEngine ce = new HickChatEngine();
                    uid.Value = Convert.ToString(Session["userid"]);
                    uname.Value = Convert.ToString(Session["username"]);
                    long userid = Convert.ToInt64(Session["userid"]);
                    string usertype = Convert.ToString(Session["UserType"]);
                    //PhyName = Convert.ToString(Session["username"]);
                    PhyName = Convert.ToString(Session["name"]);
                    List<ConsentedUsers> list = ce.GetConsentedUsers(userid, 0, 1, 200, usertype);

                    gdconditions.DataSource = list;
                    gdconditions.DataBind();
                }
            }
        }
    }
}