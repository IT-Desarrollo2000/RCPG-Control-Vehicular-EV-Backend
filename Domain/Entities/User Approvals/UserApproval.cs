using Domain.Entities.Profiles;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.User_Approvals
{
    public class UserApproval : BaseEntity
    {
        public ApprovalStatus Status { get; set; }
        public string? Comment { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string DriversLicenceUrl { get; set; }
        public string DriversLicencePath { get; set; }
        public int LicenceValidityYears { get; set; }
        public DateTime LicenceExpeditionDate { get; set; }
        public DateTime LicenceExpirationDate { get; set; }
        public int ProfileId { get; set; }
        public UserProfile Profile { get; set; }
    }
}
