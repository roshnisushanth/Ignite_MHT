using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hick.Models;

namespace Hick
{
    public class Users
    {       
        public int ID { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public int Status { get; set; }
        public string Lastname { get; set; }
        public string StatusMessage { get; set; }
        public string Lastloggedin { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public int Favorites { get; set; }
        public string PhoneNumber { get; set; }
        public string DateOfBirth { get; set; }
        public string Physician { get; set; }
        public long ReferenceID { get; set; }

        public bool UnReadMessages { get; set; }
        public bool IncomingCall { get; set; }
        public string VideoCallDuration { get; set; }
        public string TotalChatDuration { get; set; }

        public string Completed { get; set; }
        public string NotCompleted { get; set; }
        public string NoTimerLog { get; set; }
        public string CompletedPercentage { get; set; }
        public bool ConsentFormUploaded { get; set; }
        


    }

    public class ConsentedUsers : Users
    {
        public long  PatientId { get; set; }
        public string showConsentButton { get; set; }
        public string showDownloadButton { get; set; }
        public string FileExt { get; set; }
    }

    public class Wraper
    {
        public List<Users> UsersColl { get; set; }
        public List<HickGroups> GroupsColl { get; set; }
    }
}