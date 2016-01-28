using Dal.Encryption;
using IGNITE.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hick.PatientLookUp.ASPX
{
    public partial class Recordaccess : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid">current user</param>
        /// <param name="authorizeduserid"> autorized user for</param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public static string CheckOnetimePassword(string userid, string authorizeduserid)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
              new SqlParameter("@UserId",userid),
               new SqlParameter("@Autherizeduserid",authorizeduserid)

            };

            var obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "sp_hick_GetOneTimeAccessCodeStatus", sqlParms);
            if ((int)obj == 1)
            {
                return "Sucessful";
            }
            else
            {
                return "UnSucessful";
            }


        }


        [WebMethod(EnableSession = true)]
        public static string insertOnetimePassword(string userid, string pswd, string authorizeduserid)
        {
            EncryptDecryptUtil enc = new EncryptDecryptUtil();
            SqlParameter[] sqlParms = new SqlParameter[]{
              new SqlParameter("@UserId",userid),
               new SqlParameter("@Autherizeduserid",authorizeduserid),
                    new SqlParameter("@password",enc.EncryptData(pswd,enc.GetEncryptType()))

            };

            var obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "sp_hick_InsertOneTimeAccessCodeStatus", sqlParms);
            if ((int)obj == 1)
            {
                return "Sucessful";
            }
            else
            {
                return "UnSucessful";
            }


        }


    }
}