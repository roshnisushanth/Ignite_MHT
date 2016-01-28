using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hick.Models
{
    public class VideoChatSettings
    {
        public bool EnableVideoChat { get; set; }
        public string FlashServerType { get; set; }
        public string FlashMediaServer { get; set; }
        public string BroadcastVideoWidth { get; set; }
        public string BroadcastVideoheight { get; set; }
        public string BroadcastVideoGuid { get; set; }
        public string ReceiveVideoGuid { get; set; }
        public string ParentVideoId { get; set; }
    }

    public class VideoChatLog
    {
       
        public long ConversationId { get; set; }
        public string VideoId { get; set; }
        public string ParentVideoId { get; set; }
        public string ConversationDate { get; set; }
        public int PeerID { get; set; }      
        public int MessageType { get; set; }
        public int Status { get; set; }
        public string ConversationEndTime { get; set; }

        public string PeerName { get; set; }
        public string Duration { get; set; }
        public string Time { get; set; }
     
    }
}