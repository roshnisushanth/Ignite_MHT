using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hick.Models
{
    public class HickGroups
    {
        public long Id { get; set; }
        public long CreatedBy { get; set; }
        public long CreatedDate { get; set; }
        public IList<HickGroupUsers> GroupUsersColl { get; set; }

        public bool IncomingCall { get; set; }
        public bool IsUnreadMessage { get; set; }
    }
    public class HickGroupUsers
    {
        public long GroupId { get; set; }
        public long UserId { get; set; }

        public string FullName { get; set; }
    }
}