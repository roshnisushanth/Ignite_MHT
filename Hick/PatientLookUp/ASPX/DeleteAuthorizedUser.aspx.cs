using Hick.Authorized;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.PatientLookUp.ASPX
{
    public partial class DeleteAuthorizedUser : System.Web.UI.Page
    {
        AuthorizedService service = new AuthorizedService();
        string MailServer = ConfigurationManager.AppSettings["smtphost"].ToString();
        int PortNumber = Convert.ToInt32(ConfigurationManager.AppSettings["portNumber"].ToString());
        string smtpUserName = ConfigurationManager.AppSettings["smtpUserName"].ToString();
        string smtpPassword = ConfigurationManager.AppSettings["smtpPassword"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hdnuserid.Value= Request.QueryString["id"].ToString();
                hdnemail.Value = Request.QueryString["email"].ToString();
            }
        }

        protected void Revoke_User_Click(object sender, EventArgs e)
        {
            int result = service.RevokeUsers("Delete", Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(hdnuserid.Value));
            ClientScript.RegisterStartupScript(GetType(), "scrpt", "parent.window.location.href=parent.window.location.href;", true);
            if (result == 1)
            {
                string firstName = Session["FirstName"].ToString();
                string lastName = Session["LastName"].ToString();
                string body = firstName + " " + lastName + " has discontinued access to their patient record, your access is no longer valid.";
                string subject = "";
                SentMail(hdnemail.Value, body, subject);
            }
        }

        protected void canclel_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "scrpt", "parent.window.location.href=parent.window.location.href;", true);
        }
        // sending mail to given email id
        public void SentMail(string Email, string MailBody, string Subject)
        {
            MailMessage mail = new MailMessage();
            try
            {
                string FromEmail = ConfigurationManager.AppSettings["FromEmail"].ToString();
                //mail.SendEmail(FromEmail,"",EmailID,MailSubject,Mailbodycontent);
                mail.From = new MailAddress(FromEmail);
                mail.To.Add(Email);
                mail.Subject = Subject;
                mail.Body = MailBody;
                mail.IsBodyHtml = true;
                System.Net.Mail.SmtpClient smtp = new SmtpClient(MailServer, PortNumber);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(smtpUserName, smtpPassword);
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enablessl"].ToString());
                smtp.Send(mail);
            }
            catch (Exception exp)
            {

            }
        }
    }
}