using Hick.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hick.Base
{
    public class BasePage : System.Web.UI.Page
    {
        override protected void OnInit(EventArgs e)
        {
            //InitializeComponent();
            base.OnInit(e);
            if (Session["userid"] == null)
            {
                //Response.Redirect(Utility.LogOutUrl);
                ClientScript.RegisterStartupScript(GetType(), "Load", "<script type='text/javascript'>window.parent.location.href = '"+ Utility.LogOutUrl + "'; </script>");
                //window.top.location.href = "http://example.com";
            }
        }
    }
}