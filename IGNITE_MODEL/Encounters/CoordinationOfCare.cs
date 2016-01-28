using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGNITE_MODEL.Encounters
{
    public class CoordinationOfCare
    {
        public long Id { get; set; }
        public long AssessmentId { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string Notes { get; set; }

        public CoordinationOfCare()
        {
            Date = DateTime.Now;
        }
    }
}
