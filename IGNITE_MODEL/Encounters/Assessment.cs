using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGNITE_MODEL.Encounters
{
    public class Assessment
    {
        public long Id { get; set; }
        public long AssessmentId { get; set; }
        public DateTime  Date { get; set; }
        public string Time { get; set; }
        public string Notes { get; set; }

        public Assessment()
        {
            Date = DateTime.Now;
        }
    }
}
