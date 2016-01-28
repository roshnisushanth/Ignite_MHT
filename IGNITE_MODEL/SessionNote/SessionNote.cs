using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGNITE_MODEL.SessionNote
{
    public class SessionNote
    {
       public long Id { get; set; }
       public string Date { get; set; }
       public string Category { get; set; }
       public string StartTime { get; set; }
       public string EndTime { get; set; }
       public string TotalTime { get; set; }
       public string Note { get; set; }
    }
}
