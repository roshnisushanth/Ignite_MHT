using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGNITE_MODEL.Encounters
{
    public class Medication
    {
        public long  Id { get; set; }
        public long  AssessmentId { get; set; }
        public string Medicine { get; set; }
        public bool IsActive { get; set; }
        public string Dosage { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }
        public string StartDateString { get; set; }
    }

    public class mWrapper
    {
        public List<Medication> Rows { get; set; }
    }
}
