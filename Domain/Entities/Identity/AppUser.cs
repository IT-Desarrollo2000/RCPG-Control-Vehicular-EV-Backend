using Domain.Entities.Profiles;
using Domain.Entities.Registered_Cars;
using Domain.Entities.Departament;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public AppUser() 
        {
            this.UserRoles = new HashSet<AppUserRole>();
            this.Socials = new HashSet<AppUserSocial>();
            this.VehicleReports = new HashSet<VehicleReport>();
            this.VehicleReportUses = new HashSet<VehicleReportUse>();
        
        }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        public ICollection<AppUserRole> UserRoles { get; set; }
        public virtual UserProfile Profile { get; set; }
        public virtual ICollection<AppUserSocial> Socials { get; set; }
        public virtual ICollection<VehicleReport> VehicleReports { get; set; }
        public ICollection<VehicleReportUse> VehicleReportUses { get; set; }
        public virtual ICollection<Departaments> AssignedDepartments { get; set; }
    }
}
