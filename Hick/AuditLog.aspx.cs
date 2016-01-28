using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick
{
    public partial class AuditLog : Hick.Base.BasePage
    {
        string currentuser;
        protected void Page_Load(object sender, EventArgs e)
        {
            //string sessionid = Convert.ToString(Request.QueryString["ssid"]);
            string peerid = (Request.QueryString["peerid"] != null) ? Request.QueryString["peerid"].ToString() : "0";
            ////if (!String.IsNullOrEmpty(sessionid))
            //{
            //    //AuthenticateBridgeUser(sessionid);
            //}

            /* For Query string as username(for demo purpose)*/

            //string name = Convert.ToString(Request.QueryString["username"]);            
            //if (!String.IsNullOrEmpty(name))
            //{
            //    AuthenticateBridgeUser(name.ToLower());               
            //}

            if (Session["username"] != null)
            {
                currentuser = Session["username"].ToString();
                string userid = Session["userid"].ToString();
                Userid.InnerHtml = userid;
                loginname.InnerHtml = Session["username"].ToString();
                hdnusertype.InnerHtml = Convert.ToString(Session["UserType"]).ToLower().Trim();
                hdnpeerid.InnerHtml = peerid;
                //ShowTermsPopUp(Convert.ToString(Session["LastLoggedIN"]), Convert.ToInt32(userid),false);

                //DateTime dt = String.IsNullOrEmpty(Convert.ToString(Session["LastLoggedIN"])) ? DateTime.Now : Convert.ToDateTime(Session["LastLoggedIN"]);
                //datetimepicker.InnerHtml = dt.ToString("hh:mm tt");

            }
            else
            {
                //Response.Redirect(Utility.LogOutUrl);
            }
        }
        /*
        public void AuthenticateBridgeUser(string sessionid)
        {

            try
            {
                Session.Clear();
                long _userid = long.Parse(Utility.DecryptText(sessionid.Split('-')[0].Trim()));
                string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = constr;
                    conn.Open();

                    using (SqlCommand command = new SqlCommand("select * from Hick_Users where ID='" + _userid + "'", conn))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Session["userid"] = reader["ID"];
                                    Session["username"] = reader["Username"];
                                    Session["LastLoggedIN"] = Convert.ToString(reader["LastLoggedIN"]);
                                    Session["UserType"] = Convert.ToString(reader["user_type"]);

                                    if (!string.IsNullOrEmpty(Convert.ToString(reader["LastLoggedIN"])))
                                    {
                                        string constring = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
                                        using (SqlConnection connection = new SqlConnection())
                                        {
                                            connection.ConnectionString = constring;
                                            connection.Open();

                                            string updateString = @"update Hick_Users set Status='1',StatusMessage='Online', LastLoggedIN='" + DateTime.UtcNow + "' where ID='" + _userid + "'";
                                            SqlCommand cmd = new SqlCommand(updateString, connection);
                                            SqlDataAdapter adp = new SqlDataAdapter();
                                            adp.UpdateCommand = cmd;
                                            adp.UpdateCommand.ExecuteNonQuery();
                                        }
                                    }
                                    else
                                    {
                                        Response.Redirect("TermsConditions.aspx", false);
                                    }
                                }
                            }
                            else
                            {
                                Response.Redirect(Utility.LogOutUrl);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Response.Redirect(Utility.LogOutUrl);
            }
        }
        */
    }
}