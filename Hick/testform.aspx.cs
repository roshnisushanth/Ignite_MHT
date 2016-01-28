using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Dal.Encryption;

namespace Hick
{
    public partial class testform : Hick.Base.BasePage
    {
        EncryptDecryptUtil ecd = new EncryptDecryptUtil();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btn_click(object sender, EventArgs e)
        {
            int randomno = generaterandom();
            string name = Request.Form["txtusername"];
           

            string constr = ConfigurationManager.ConnectionStrings["HickConnectionString"].ConnectionString.ToString();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr; 
                    //"Server=its-ba-dc02\\MSSQL2008R2_DEV;Database=hick_poc;Trusted_Connection=false;User Id=itsdev;Password=itsdev";
                conn.Open();

                using (SqlCommand command = new SqlCommand(
        "select ID,Username from Hick_Users where Username='" + name + "' ",
		conn))
	   {		
		using (SqlDataReader reader = command.ExecuteReader())
		{
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {


                    string updateString = @"update Hick_Users set Status='1',StatusMessage='Online',LastLoggedIN='"+DateTime.UtcNow+"' where Username='" + name + "'";

                    Session["userid"] = reader["ID"];
                    Session["username"] = ecd.DecryptData((reader["Username"].ToString()), ecd.GetEncryptType());
                    SqlCommand cmd = new SqlCommand(updateString, conn);
                    SqlDataAdapter adp = new SqlDataAdapter();
                    adp.UpdateCommand = cmd;
                    adp.UpdateCommand.ExecuteNonQuery();
                    Response.Redirect("Chat.aspx");
                }
            }
            else
            {
                Response.Write("Invalid Username!!");
            }
		}
	    }
	}
            }

        private int generaterandom()
        {
            Random rnd = new Random();
            return rnd.Next(500);
        }

            //connection to database and do autentication check
           // Response.Redirect("Chat.aspx");
        }
    }
