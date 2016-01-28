using IGNITE_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGNITE_MODEL;
using System.Data.SqlClient;
using IGNITE.DBUtility;
using System.Data;

namespace IGNITE_DAL.DataObjects
{
    public class UserAuth : IUserAuth
    {
        
        public User GetUserAuthByID(long userId)
        {
            SqlParameter[] sqlParms = new SqlParameter[] { new SqlParameter("UserId", userId) };
            User authUser = new User();
            using (SqlDataReader sqlGetUser = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, "sp_hick_FetchUserByID", sqlParms))
            {
                while (sqlGetUser.Read())
                {
                    authUser.Id = DBHelper.getInt64(sqlGetUser, "Id");
                    authUser.UserName = DBHelper.getString(sqlGetUser, "UserName");
                    authUser.LastLoggedIN = DBHelper.getDateTime(sqlGetUser, "LastLoggedIN");
                    authUser.Type = DBHelper.getString(sqlGetUser, "User_Type");

                }
            }
            return authUser;
        }
    }
}
