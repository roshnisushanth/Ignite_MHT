using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGNITE_MODEL
{
    public class User
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public DateTime LastLoggedIN { get; set; }
        public string Type { get; set; }
    }

    public class Contact
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public long  ReferenceId { get; set; }
        public long ProviderId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long PhysicianId { get; set; }
    }

 
}
