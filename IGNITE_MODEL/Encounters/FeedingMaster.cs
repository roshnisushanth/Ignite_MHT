using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGNITE_MODEL.Encounters
{
    public enum FeedingStatus
    {
        NA,
        Confirm,
        Deny
    }
    public class FeedingMaster
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public long FeedingId { get; set; }
    }

    public class FeedingChild : FeedingMaster
    {
        public short Status { get; set; }
        public string StatusStr { get; set; }
        public FeedingStatus StatusEnum { get; set; }

        public static FeedingStatus GetStatusEnum(int status)
        {
            switch (status)
            {
                case 0:
                    return FeedingStatus.NA;
                case 1:
                    return FeedingStatus.Confirm;
                case 2:
                    return FeedingStatus.Deny;
                default:
                    return FeedingStatus.NA;
            }
        }
    }

    public class FeedingMother : FeedingChild
    {
        public string Notes { get; set; }
    }

    [Serializable]
    public class FeedingViewObject
    {
        public string AssessmentId { get; set; }
        public string FeedId { get; set; }
        public string Status { get; set; }
    }
}
