using Domain.Entities.Identity;
using Domain.Entities.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Registered_Cars
{
    public class MaintenanceProgress : BaseEntity
    {
        public int VehicleMaintenanceId { get; set; }
        public VehicleMaintenance VehicleMaintenance { get; set; }
        public string Comment { get; set; }
        public int? MobileUserId { get; set; }
        public AppUser? AdminUser { get; set; }
        public int? AdminUserId { get; set; }
        public UserProfile? MobileUser { get; set; }
        //public string? ImagePath { get; set; }
        //public string? ImageUrl { get; set;}
    }
}
