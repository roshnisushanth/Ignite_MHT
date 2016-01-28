using IGNITE_DAL.DataObjects;
using IGNITE_DAL.Interfaces;
using IGNITE_MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGNITE_BLL
{
    public class AssessmentBLL
    {
        private readonly IAssessment assessment = new Assessment();

        public List<AssessmentSummary> LoadAssessmentSummary(long userId)
        {
            return assessment.LoadAssessmentSummary(userId);
        }
    }
}
