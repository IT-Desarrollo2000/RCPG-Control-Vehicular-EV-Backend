using Domain.Entities.Departament;
using Domain.Entities.Identity;
using Domain.Entities.Registered_Cars;
using Domain.Entities.User_Approvals;
using Domain.Enums;

namespace Domain.Entities.Profiles
{
    public class UserProfile : BaseEntity
    {
        public UserProfile()
        {
            this.Approvals = new HashSet<UserApproval>();
            this.VehicleReports = new HashSet<VehicleReport>();
            this.VehicleReportUses = new HashSet<VehicleReportUse>();
        }

        public int UserId { get; set; }
        public virtual AppUser User { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public string SurnameP { get; set; }
        public string SurnameM { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? ProfileImagePath { get; set; }
        public DateTime? ProfileImageUploadDate { get; set; }

        //Relacionados al conductor
        public virtual ICollection<UserApproval> Approvals { get; set; }
        public bool IsVerified { get; set; }
        public string? DriversLicenceFrontUrl { get; set; }
        public string? DriversLicenceBackUrl { get; set; }
        public string? DriversLicenceFrontPath { get; set; }
        public string? DriversLicenceBackPath { get; set; }
        public int? LicenceValidityYears { get; set; }
        public LicenceType? LicenceType { get; set; }
        public DateTime? LicenceExpeditionDate { get; set; }
        public DateTime? LicenceExpirationDate { get; set; }
        public int? DepartmentId { get; set; }
        public virtual Departaments? Department { get; set; }
        public ICollection<VehicleReport> VehicleReports { get; set; }
        public ICollection<VehicleReportUse> VehicleReportUses { get; set; }


        //Unique
        //public string SharingKey { get; set; }
        //public ICollection<UserContact> Contacts { get; set; }
    }
}
