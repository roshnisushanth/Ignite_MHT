using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGNITE_MODEL
{
    public class LoginViewData
    {
        public string Message { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Success { get; set; }
        public long UserId { get; set; }
        public int PhysicianID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; } // need to understand why we need the password sent back 
        private DateTime? _lastLoggedIN = null;
        public string UserType { get; set; }
        public long ReferenceID { get; set; }
     

        public DateTime? LastLoggedIN
        {
            get
            {
                return _lastLoggedIN;
            }

            set
            {
                _lastLoggedIN = value;
            }
        }
    }
}
