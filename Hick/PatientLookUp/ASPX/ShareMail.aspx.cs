﻿using Hick.Authorized;
using Hick.CCDGenerator;
using Hick.Models;
using Hick.PHP;
using IGNITE_MODEL.PHPView;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.PatientLookUp.ASPX
{
    public partial class ShareMail : System.Web.UI.Page
    {
        AuthorizedService service = new AuthorizedService();
      CCDGeneratorService ccdService = new CCDGeneratorService();
        protected void Page_Load(object sender, EventArgs e)
        {
            hdnuser.Value = Request.QueryString["id"].ToString();
        }
        

        // sending mail to given email id
        public void SentMail(string Email, string MailBody, string Subject,string Path)
        {
           DirectAPI.DAPI dapi = new DirectAPI.DAPI();
           dapi.SendMessage(Email, "", "", Subject, MailBody, Path);

        }

        protected void sentmail_Click(object sender, EventArgs e)
        {
            string urlPath = "";
            if (!String.IsNullOrEmpty(ViewState["newFile"].ToString()))
            {
                urlPath = ViewState["newFile"].ToString();
            }
            SentMail(To.Text, Server.HtmlDecode(myArea1.InnerText), subject.Text, urlPath);
            var auditresult = service.AddAuditLog(Convert.ToInt32(Session["userid"].ToString()), 2, "PHP", "Not Applicable", "Not Applicable", DateTime.Now);
            ClientScript.RegisterStartupScript(GetType(), "scrpt", "parent.window.location.href=parent.window.location.href;", true);
        }

        protected void PHP_Click(object sender, EventArgs e)
        {
            int iPatientid = Convert.ToInt32(hdnuser.Value);
            GeneratePDFFile(iPatientid);
        }

        protected void CCDA_Click(object sender, EventArgs e)
        {
            try
            {
                string xmlResult = ccdService.GenerateCCD(Convert.ToInt32(hdnuser.Value), Session["FirstName"].ToString(), Session["LastName"].ToString());
            string filename_set = DateTime.Now.ToString("MMddyyyyHHmmss");
            var strPath = @"" + Server.MapPath("~\\Uploads\\")+ "CCDA" + filename_set + ".xml";  
                System.IO.File.WriteAllText(strPath, xmlResult);
                WebClient myClient = new WebClient();
                ViewState["newFile"] = strPath;

            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
                //var m = ex.ToString();
            }

        }

        public void GeneratePDFFile(int iPatientid)
        {
            WebClient myClient = new WebClient();
            myClient.UseDefaultCredentials = true;
            string MyUrl = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port;
            MyUrl = MyUrl + Request.Url.AbsolutePath.Replace("ShareMail.aspx", "");
            string currentPageUrl = MyUrl + "ViewPHPSummery.aspx?id=" + iPatientid;
            byte[] data = myClient.DownloadData(currentPageUrl);
            string filename_set = DateTime.Now.ToString("MMddyyyyHHmmss");
            File.WriteAllBytes(@"" + Server.MapPath("~\\Uploads\\") + "" + "PHPView_" + filename_set + ".pdf", data);
            ViewState["newFile"] = Server.MapPath("~\\Uploads\\") + "PHPViewReport_" + filename_set + ".pdf";

        }
        

    }
}