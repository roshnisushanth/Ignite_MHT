using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGNITE_MODEL.Encounters
{
    public class EncounterAssessmentSummary
    {
        public int AssessmentId { get; set; }
        public string PatientName { get; set; }
        public DateTime DOB { get; set; }
        public int Age { get; set; }
        public DateTime AssessmentDate { get; set; }
        public string AssessmentBy { get; set; }
        public string AssessmentNotes { get; set; }
        public string HPINotes { get; set; }
        public string COCNotes { get; set; }
        public List<FeedingMother> BreastFeedingBabies { get; set; }
        public List<FeedingMother> BreastFeedingMothers { get; set; }
        public List<FeedingOvbservationChild> Observations { get; set; }
        public List<ChiefComplaints> ChiefComplaintList { get; set; }

        public EncounterAssessmentSummary()
        {
            BreastFeedingBabies = new List<FeedingMother>();
            BreastFeedingMothers = new List<FeedingMother>();
            Observations = new List<FeedingOvbservationChild>();
            ChiefComplaintList = new List<ChiefComplaints>();
        }

        public class ChiefComplaints
        {
            public string Notes { get; set; }
            public List<ComplaintsPlan> ComplaintsPlans { get; set; }

            public ChiefComplaints()
            {
                ComplaintsPlans = new List<ComplaintsPlan>();
            }
            public class ComplaintsPlan
            {
                public string Plan { get; set; }
            }
        }
    }
}
