using IGNITE_DAL.DataObjects;
using IGNITE_MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGNITE_DAL.Interfaces
{
    public interface ILogin
    {
        LoginViewData Login(string UserName, string Password);
        LoginViewData Login(string FirstName, string LastName, DateTime DOB);
    }
}
