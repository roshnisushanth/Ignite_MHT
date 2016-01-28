using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hick
{
    public class UserConversation
    {
        public long Id { get; set; }
        public long Initiator { get; set; }
        public long Answerer { get; set; }
        public string Date { get; set; }
        public long GroupId { get; set; }
        public int IsChatOn { get; set; }
        public TimeSpan StartTime { get; set; }
        public string Total_Txtchat_Dauration { get; set; }

        public string TimeZone { get; set; }
    }
}