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
    public class LoginBLL
    {
        private readonly ILogin login = new Login();

        public LoginViewData UserLogin(string UserName, string Password)
        {
            return login.Login(UserName, Password);
        }
        public LoginViewData UserLogin(string FirstName, string LastName, DateTime DOB)
        {
            return login.Login(FirstName, LastName, DOB);
        }
    }
}
