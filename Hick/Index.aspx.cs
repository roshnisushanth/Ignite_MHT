using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using IGNITE_MODEL;
using Dal.Encryption;

namespace Hick
{
    public partial class Index : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            logonPlaceholder.Controls.Clear();
            var lgc = ConfigurationManager.AppSettings["LogonControl"];
            //Hick.Logon.UP.LogonControl logCtrl = null;
            switch (lgc)
            {
                case "MHT":
                    var mhtCtrl = LoadControl("~/Logon/MHT/LogonControl.ascx") as Hick.Logon.MHT.LogonControl;
                    logonPlaceholder.Controls.Add(mhtCtrl);
                    break;
                default:
                     var upCtrl = LoadControl("~/Logon/UP/LogonControl.ascx") as Hick.Logon.UP.LogonControl;
                    logonPlaceholder.Controls.Add(upCtrl);
                    break;

            }
        }

        public void PostLoginProcess(IGNITE_MODEL.LoginViewData retObj, HttpResponse res)
        {

            if (retObj.Success)
            {
                EncryptDecryptUtil ecd = new EncryptDecryptUtil();
                Session["userid"] = retObj.UserId;
                Session["username"] = ecd.DecryptData((retObj.UserName).ToString(), ecd.GetEncryptType()); 
                Session["p_detail"] = retObj.Password;
                Session["LastLoggedIN"] = retObj.LastLoggedIN;
                Session["UserType"] = retObj.UserType;
                Session["PhysicianID"] = retObj.PhysicianID;
                Session["FirstName"] = ecd.DecryptData((retObj.FirstName).ToString(), ecd.GetEncryptType());
                Session["LastName"] = ecd.DecryptData((retObj.LastName).ToString(), ecd.GetEncryptType());
                Session["name"] = ecd.DecryptData((retObj.FirstName).ToString(), ecd.GetEncryptType()) + " " + ecd.DecryptData((retObj.LastName).ToString(), ecd.GetEncryptType());                
                Session["ReferenceID"] = retObj.ReferenceID.ToString();
                if (retObj.UserType == "AuthorizedUser")
                {
                    res.Redirect("AuthorizedUserForm.aspx");
                }
                else
                {
                    if (retObj.LastLoggedIN == null)
                    {
                        res.Redirect("TermsConditions.aspx");
                    }
                    else
                    {
                        res.Redirect("Chat.aspx");
                    }
                }


            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showmsg", "showMessage()", true);
            }
        }
    }
}
