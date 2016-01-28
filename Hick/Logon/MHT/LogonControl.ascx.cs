using Hick.Models;
using IGNITE_BLL;
using IGNITE_MODEL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.Logon.MHT
{
    // Create Delegate to Handle Click event in Default page
    //public delegate void btnLogon_Click(object sender, System.EventArgs e);

    public partial class LogonControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Logon_Click(object sender, EventArgs e)
        {
            var retObj = new LoginBLL().UserLogin(txtusername.Value, txtpassword.Value);
            new Index().PostLoginProcess(retObj, Response);
           // JObject postData = new JObject();
           // postData.Add("Pin", Utility.PinValue);
           // postData.Add("UserName", txtusername.Value.Trim());
           // postData.Add("Password", txtpassword.Value);

           //// var res = Utility.PostRequest<UserDetails>(Utility.GetServiceUrl("validateuserlogin"), postData.ToString(Formatting.None));
           // LoginViewData retObj = new LoginViewData();
           // new Index().PostLoginProcess(retObj, Response);
        }
    }
}