using Hick.Authorized;
using IGNITE_MODEL.AutherizedUser;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.PatientLookUp.ASPX
{
    public partial class AddEditAuthorizedUser : System.Web.UI.Page
    {
        AuthorizedService service = new AuthorizedService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string action = Request.QueryString["Action"].ToString();

                if (action == "Edit")
                {
                    int userid = Convert.ToInt32(Request.QueryString["id"].ToString());
                    save_User.Text = "Update";
                    FirstName.Enabled = false;
                    FirstName.CssClass = "form-control";
                    LastName.Enabled = false;
                    LastName.CssClass = "form-control";
                    DOB.Enabled = false;
                    DOB.CssClass = "popup_textbox";
                    Email.Enabled = false;
                    Email.CssClass = "form-control";
                    hdnaction.Value = action;
                    GetUserValueOnID(userid);
                }
                else
                {
                    save_User.Text = "Save";
                    Revoke.Visible =false;
                }
            }
        }

        protected void save_User_Click(object sender, EventArgs e)
        {
            string action = "Insert";
            int authorizedUser = 0;
            if (save_User.Text == "Update")
            {
                action = "Update";
                authorizedUser = Convert.ToInt32(hdnusr_id.Value);
            }
            string firstname = FirstName.Text;
            string lastname = LastName.Text;
            string relationship = Relationship.Value;
            string otherrelationship = OtherRelationship.Text;
            string email = Email.Text;
            string password = Passcode.Text;
            int userId = Convert.ToInt32(Session["userid"].ToString());
             
            DateTime date = DateTime.ParseExact(DOB.Text, "MM/dd/yyyy", null);
            string dob = date.ToString("MM/dd/yyyy");
            string result = service.AddAutherizedUsers(action, userId, authorizedUser, firstname, lastname, dob, relationship, otherrelationship, email, password);
            if (save_User.Text == "Save")
            {

                string firstName = Session["FirstName"].ToString();
                string lastName = Session["LastName"].ToString();
                if (result == "Exists")
                {

                    string subject = "";
                    string body = firstName + " " + lastName + " has requested you to be an Authorized Representative to access to their patient record. Please login to your existing Ignite account to view <Insert Patient’s First Name>’s record, under “Record Access”. You will need the access code provided by " + firstName + " for any record access .";


                    ////SentMail(email, body, subject);
                }
                else if (result == "Not Exists")
                {
                    string body = firstName + " " + lastName + " has requested you to be an Authorized Representative to access to their patient record. Please click the link below to login to Ignite. Your username will be your email ID and your password will be the access code as provided to you by" + firstName + " .";
                    string subject = "";
                    //SentMail(email, body, subject);
                }
                else if(result== "Already Exists")
                {
                    Response.Write("<script>alert('This user is already registered as an authorized user')</script>");
                }
            }

            ClientScript.RegisterStartupScript(GetType(), "scrpt", "parent.window.location.href=parent.window.location.href;", true);
        }

        protected void GetUserValueOnID(int id)
        {
            AutherizedUsersList AutherizedUsers = new AutherizedUsersList();
            AutherizedUsers = service.GetAutherizedUsers("SelectOnId", Convert.ToInt32(Session["userid"].ToString()), id);
            FirstName.Text = AutherizedUsers.AutherizedUsers.Select(m => m.FirstName).FirstOrDefault();
            LastName.Text = AutherizedUsers.AutherizedUsers.Select(m => m.LastName).FirstOrDefault();
            DOB.Text = AutherizedUsers.AutherizedUsers.Select(m => m.DOB).FirstOrDefault();
            Relationship.Value = AutherizedUsers.AutherizedUsers.Select(m => m.Relationship).FirstOrDefault();
            OtherRelationship.Text = AutherizedUsers.AutherizedUsers.Select(m => m.RelationshipOther).FirstOrDefault();
            Email.Text = AutherizedUsers.AutherizedUsers.Select(m => m.Email).FirstOrDefault();
            Passcode.Text = AutherizedUsers.AutherizedUsers.Select(m => m.Passcode).FirstOrDefault();
            hdnusr_id.Value = (AutherizedUsers.AutherizedUsers.Select(m => m.AutherizedUserId).FirstOrDefault()).ToString();
        }



        protected void Revoke_Click(object sender, EventArgs e)
        {
            int result = service.RevokeUsers("Revoke", Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(hdnusr_id.Value));
            ClientScript.RegisterStartupScript(GetType(), "scrpt", "parent.window.location.href=parent.window.location.href;", true);

            if(result==1)
            {
                string firstName = Session["FirstName"].ToString();
                string lastName = Session["LastName"].ToString();
                string body = firstName + " " + lastName + " has discontinued access to their patient record, your access is no longer valid.";
                string subject = "";
                SentMail(Email.Text, body, subject);
            }

        }

        // sending mail to given email id
        public void SentMail(string Email, string MailBody, string Subject)
        {
            MailMessage mail = new MailMessage();
            string MailServer = ConfigurationManager.AppSettings["smtphost"].ToString();
            int PortNumber = Convert.ToInt32(ConfigurationManager.AppSettings["portNumber"].ToString());
            string smtpUserName = ConfigurationManager.AppSettings["smtpUserName"].ToString();
            string smtpPassword = ConfigurationManager.AppSettings["smtpPassword"].ToString();

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