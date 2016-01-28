using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGNITE_MODEL.Encounters
{
    public class Condition
    {
        public long Id { get; set; }
        public long AssessmentId { get; set; }
        public string ICDCode { get; set; }
        public DateTime ActiveDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime InActiveDate { get; set; }
        public string Description { get; set; }
        public string ActiveDateString { get; set; }
    }

    public class cWrapper
    {
        public List<Condition> Rows { get; set; }
    }
}
