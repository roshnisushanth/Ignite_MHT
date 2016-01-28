using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.SuperUser
{
    public partial class SuperUserMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.MetaKeywords != null)
            {
                string pkey = Convert.ToString(Page.MetaKeywords).ToLower();
                hdnpagekey.Value = pkey;
            }
        }
    }
}