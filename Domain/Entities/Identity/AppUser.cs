using Domain.Entities.Departament;
using Domain.Entities.Profiles;
using Domain.Entities.Registered_Cars;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public AppUser()
        {
            UserRoles = new HashSet<AppUserRole>();
            Socials = new HashSet<AppUserSocial>();
            VehicleReports = new HashSet<VehicleReport>();
            VehicleReportUses = new HashSet<VehicleReportUse>();
            FinishedUseReports = new HashSet<VehicleReportUse>();
        }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        public ICollection<AppUserRole> UserRoles { get; set; }
        public virtual UserProfile Profile { get; set; }
        public string? Name { get; set; }
        public string? LastNameP { get; set; }
        public string? LastNameM { get; set; }
        public string? FullName { get; set; }
        public virtual ICollection<AppUserSocial> Socials { get; set; }
        public virtual ICollection<VehicleReport> VehicleReports { get; set; }
        public virtual ICollection<VehicleReportUse> VehicleReportUses { get; set; }
        public virtual ICollection<Departaments> AssignedDepartments { get; set; }
        public virtual ICollection<VehicleReport> SolvedReports { get; set; }
        public virtual ICollection<VehicleReportUse> FinishedUseReports { get; set; }
    }
}
