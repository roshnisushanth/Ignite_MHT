using IGNITE_DAL.DataObjects;
using IGNITE_DAL.Interfaces;
using IGNITE_MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGNITE_BLL
{
    public class UserBLL
    {
        readonly IUserAuth authUser = new UserAuth();

        public User GetUserAuthByID(long userId)
        {
            return authUser.GetUserAuthByID(userId);
        }

    }
}
