using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGNITE_MODEL.AutherizedUser
{
    public class AutherizedUser
    {
        // Current User Id
        public long UserId { get; set; }

        // Current User Reference Id
        public long ReferenceId { get; set; }

        // Autherized User Id
        public long AutherizedUserId { get; set;}

        //First Name
        public string FirstName { get; set; }

        // Last Name
        public string LastName { get; set; }

        // Date of birth
        public string DOB { get; set; }

        // Relationship
        public string Relationship { get; set; }

        // Other relationship
        public string RelationshipOther { get; set; }

        // Email 
        public string Email { get; set; }

        // Passcode 
        public string Passcode { get; set; }

        // Access Level
        public string AccessLevel { get; set; }

        // User Last Logged date and time
        public DateTime LastLoggedIn { get; set; }

        // User Active status
        public string ActiveStatus { get; set; }

        // Authorized user last login
        public string AccessHistory { get; set; }
    }

    public class AutherizedUsersList
    {

        public List<AutherizedUser> AutherizedUsers { get; set; }
    }

    public class Activitylog
    {
        // Action Type ie, Download,View
        public string Actiontype { get; set; }

        // Information Type
        public string InformationType { get; set; }

        // Action Date
        public DateTime ActionDate { get; set; }

        // Email
        public string Email { get; set; }
    }
}
