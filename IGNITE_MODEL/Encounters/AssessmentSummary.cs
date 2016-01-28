using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGNITE_MODEL.Encounters
{
    public class AssessmentSummary
    {
        public long AssessmentId { get; set; }
        public DateTime Date { get; set; }
        public string AssesmentBy { get; set; }
    }

    public class SummaryWrapper
    {
        public List<AssessmentSummary> Rows { get; set; }
    }
}
