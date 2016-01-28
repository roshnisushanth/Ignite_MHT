using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGNITE_MODEL.Encounters
{
    public class Complaints
    {
        public long Id { get; set; }
        public string DiagnosisCode { get; set; }
        public string Description { get; set; }
        public long ComplaintId { get; set; }
        public bool IsChild { get; set; }
    }
 }
