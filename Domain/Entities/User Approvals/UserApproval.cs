using Domain.Entities.Profiles;
using Domain.Enums;

namespace Domain.Entities.User_Approvals
{
    public class UserApproval : BaseEntity
    {
        public ApprovalStatus Status { get; set; }
        public string? Comment { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string DriversLicenceFrontUrl { get; set; }
        public string DriversLicenceBackUrl { get; set; }
        public string DriversLicenceFrontPath { get; set; }
        public string DriversLicenceBackPath { get; set; }
        public LicenceType LicenceType { get; set; }
        public int LicenceValidityYears { get; set; }
        public DateTime LicenceExpeditionDate { get; set; }
        public DateTime LicenceExpirationDate { get; set; }
        public int ProfileId { get; set; }
        public virtual UserProfile Profile { get; set; }
    }
}
