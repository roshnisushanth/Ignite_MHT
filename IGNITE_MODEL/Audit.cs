using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGNITE_MODEL
{
    public enum ActionType
    {
        View,
        Download,
        Transmit
    }
    public class Audit
    {
        public string ActionType { get; set; }
        public string InformationType { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string Date { get; set; }
        //public string Date { get; set; }
        public string Email { get; set; }
    }
    public class AuditWrapper
    {
        public List<Audit> AuditColl { get; set; }
    }
}
