using Domain.Enums;

namespace Domain.DTOs.Filters
{
    public class UserApprovalFilter
    {
        public DateTime? ApprovalAfterDate { get; set; }
        public DateTime? ApprovalBeforeDate { get; set; }
        public ApprovalStatus? Status { get; set; }
        public LicenceType? LicenceType { get; set; }
        public int? LicenceValidityYears { get; set; }
        public DateTime? LicenceExpeditionAfterDate { get; set; }
        public DateTime? LicenceExpeditionBeforeDate { get; set; }
        public DateTime? LicenceExpirationAfterDate { get; set; }
        public DateTime? LicenceExpirationBeforeDate { get; set; }
        public DateTime? CreatedAfterDate { get; set; }
        public DateTime? CreatedBeforeDate { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
