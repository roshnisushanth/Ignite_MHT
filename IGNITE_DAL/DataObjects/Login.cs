using IGNITE.DBUtility;
using IGNITE_DAL.Interfaces;
using IGNITE_MODEL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
//using Dal.Encryption;

namespace IGNITE_DAL.DataObjects
{
    public class Login : ILogin
    {
        //EncryptDecryptUtil ecd = new EncryptDecryptUtil();
        LoginViewData ILogin.Login(string UserName, string Password)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("UserName",UserName),
                new SqlParameter("Password",Password)
            };

            return LoginToApplication(sqlParms, "sp_hickFetchUserForLogin");
        }

        LoginViewData ILogin.Login(string FirstName, string LastName, DateTime DOB)
        {
            throw new NotImplementedException();
        }

        LoginViewData LoginToApplication(SqlParameter[] sqlParms, string spname)
        {
            using (SqlDataReader r = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, spname, sqlParms))
            {
                while (r.Read())
                {
                    var loginViewData = new LoginViewData();
                    loginViewData.UserId = DBHelper.getInt64(r, "ID");
                    loginViewData.UserName = DBHelper.getString(r, "Username");
                    loginViewData.FirstName = DBHelper.getString(r, "Firstname");
                    loginViewData.LastName= DBHelper.getString(r, "Lastname");
                    loginViewData.Password = DBHelper.getString(r, "Password");
                    loginViewData.UserType = DBHelper.getString(r, "user_type");
                    loginViewData.PhysicianID = DBHelper.getInt(r, "PhysicianID");
                    loginViewData.ReferenceID = DBHelper.getInt64(r, "ReferenceId");
                    loginViewData.Success = true;

                    if (!string.IsNullOrEmpty(Convert.ToString(r["LastLoggedIN"])))
                    {
                        loginViewData.LastLoggedIN = DBHelper.getDateTime(r, "LastLoggedIN");
                        sqlParms = new SqlParameter[]{
                                new SqlParameter("UserId",DBHelper.getInt64(r, "ID")),
                                new SqlParameter("LastLoggedIN",DateTime.UtcNow),
                                new SqlParameter("TcAcceptDate",string.Empty)
                            };

                        SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.StoredProcedure, "sp_hickUpdateUserStatusMessageOnline", sqlParms);
                    }
                    return loginViewData;
                }
            }

            return new LoginViewData()
            {
                Message = "Login Failed",
                Success = false
            };
        }
    }
}