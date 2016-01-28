using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hick.Models
{
    public class ClearLog
    {
        public long Id { get; set; }
        public long ConversationId { get; set; }
        public long UserId { get; set; }
        public long ClearedDate { get; set; }
    }
}