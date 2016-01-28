using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGNITE_MODEL.Encounters
{
    public enum FeedingObservationStatus
    {
        NA,
        Normal,
        Abnormal
    }

    public class FeedingObservations
    {
        public long Id { get; set; }
        public bool IsObserved { get; set; }
        public long ObservationId { get; set; }
        public FeedingObservations()
        {
            ChildObservations = new List<FeedingOvbservationChild>();
            IsObserved = false;
        }
        public List<FeedingOvbservationChild> ChildObservations { get; set; }
    }

    public class FeedingOvbservationChild
    {
        public long Id { get; set; }
        public short Status { get; set; }
        public string StatusStr { get; set; }
        public FeedingObservationStatus StatusEnum { get; set; }
        public string Notes { get; set; }
        public string Description { get; set; }
        public long FeedingId { get; set; }
        public long ObservationParentId { get; set; }
        public static FeedingObservationStatus GetStatusEnum(int status)
        {
            switch (status)
            {
                case 0:
                    return FeedingObservationStatus.NA;
                case 1:
                    return FeedingObservationStatus.Normal;
                case 2:
                    return FeedingObservationStatus.Abnormal;
                default:
                    return FeedingObservationStatus.NA;
            }
        }
    }
}
