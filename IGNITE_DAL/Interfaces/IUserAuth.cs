using IGNITE_MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGNITE_DAL.Interfaces
{
    public interface IUserAuth
    {
        User GetUserAuthByID(long userId);
    }
}
