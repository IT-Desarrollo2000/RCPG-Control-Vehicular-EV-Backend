using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
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
            Approvals = new HashSet<UserApproval>();
            VehicleReports = new HashSet<VehicleReport>();
            VehicleReportUses = new HashSet<VehicleReportUse>();
            FinishedUseReports = new HashSet<VehicleReportUse>();
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
        public bool? CanDriveInHighway { get; set; }
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
        public virtual ICollection<VehicleReport> VehicleReports { get; set; }
        public virtual ICollection<VehicleReportUse> VehicleReportUses { get; set; }
        public virtual ICollection<VehicleReportUse> FinishedUseReports { get; set; }
    }
}
