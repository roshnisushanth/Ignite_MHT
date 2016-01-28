using Hick.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hick
{
    public class ConversationLog
    {
        public long Id { get; set; }
        public long ConversationId { get; set; }
        public string Conversation { get; set; }
        public string ConversationDate { get; set; }
        public int PeerID { get; set; }
        public int MessageTo { get; set; } 
        public int MessageType { get; set; }
        public int ReadStatus { get; set; } 

        public string PeerName { get; set; }
        public string PeerUserName { get; set; }
        public string Time { get; set; }
        public string ReceivedImagePath { get; set; }
        public string TotalChatDuration { get; set; }
       
    }
    public class ExportChatLog
    {
        public string Name { get; set; }
        public string Conversation { get; set; }
        public string Time { get; set; }
        public int MessageType { get; set; }
    }
}