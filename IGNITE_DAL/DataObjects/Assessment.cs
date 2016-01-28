using IGNITE_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGNITE_MODEL;

namespace IGNITE_DAL.DataObjects
{
    public class Assessment : IAssessment
    {
        public List<AssessmentSummary> LoadAssessmentSummary(long userId)
        {
            List<AssessmentSummary> summary = new List<AssessmentSummary>();

            summary.Add(new AssessmentSummary()
            {

                AssessmentBy = "temp user",
                Date = DateTime.Now
            });
            return summary;
        }
    }
}
