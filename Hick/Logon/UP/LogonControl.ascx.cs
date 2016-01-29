using IGNITE_BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dal.Encryption;

namespace Hick.Logon.UP
{
    // Create Delegate to Handle Click event in Default page
    //public delegate void btnLogon_Click(object sender, System.EventArgs e);

    public partial class LogonControl : System.Web.UI.UserControl
    {
        EncryptDecryptUtil enc = new EncryptDecryptUtil();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Logon_Click(object sender, EventArgs e)
        {
            
            // var retObj = new LoginBLL().UserLogin(txtusername.Value, txtpassword.Value);

            var retObj = new LoginBLL().UserLogin(enc.EncryptData(txtusername.Value, enc.GetEncryptType()), enc.EncryptData(txtpassword.Value, enc.GetEncryptType()));
            new Index().PostLoginProcess(retObj, Response);
            
            if (retObj.Success == false)
            {
                lblerrormsg.Text = "Invalid Username or Password";
            }
           
        }
    }
}