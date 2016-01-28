using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.PatientLookUp
{
    public partial class LookUpMaster : System.Web.UI.MasterPage
    {
        protected void Page_PreRender(object sender, EventArgs e) 
        {
            if (Page.MetaKeywords != null)
            {
                string pkey = Convert.ToString(Page.MetaKeywords).ToLower();
                hdnpagekey.Value = pkey;
            }
        
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Control myUserControl = new Control();



            //if (!String.IsNullOrEmpty(Request.QueryString["pageview"]))
            //{
            //    Session["pageview"] = Request.QueryString["pageview"].ToString();
            //    if (Session["pageview"].ToString()== "record")
            //    {

            //        myUserControl = (Control)Page.LoadControl("../UserControls/UCRecordAccessMenu.ascx");
            //    }
            //    else
            //    {
            //        Session["pageview"] = null;
            //        myUserControl = (Control)Page.LoadControl("../UserControls/UCPatientMenu.ascx");
            //    }

            //}
            //else
            //{
            //    Session["pageview"] = null;
            //    myUserControl = (Control)Page.LoadControl("../UserControls/UCPatientMenu.ascx");
            //}


            //menuModule.Controls.Add(myUserControl);






            if (!String.IsNullOrEmpty(Request.QueryString["pageview"]))
            {
  
               if (Request.QueryString["pageview"].ToString()== "record")
                {
                    Session["pageview"] = Request.QueryString["pageview"].ToString();
                }
                else
                {
                    Session["pageview"] = null;
                }
            }
            else
            {
                Session["pageview"] = null;
            }

            if (!string.IsNullOrEmpty(Session["pageview"] as string))
            {
                if (Session["pageview"].ToString() == "record")
                {

                    myUserControl = (Control)Page.LoadControl("../UserControls/UCRecordAccessMenu.ascx");
                }
                else
                {
                    Session["pageview"] = null;
                    myUserControl = (Control)Page.LoadControl("../UserControls/UCPatientMenu.ascx");
                }

            }
            else
            {
                Session["pageview"] = null;
                myUserControl = (Control)Page.LoadControl("~/PatientLookUp/UserControls/UCPatientMenu.ascx");
            }


            menuModule.Controls.Add(myUserControl);














        }
    }
}