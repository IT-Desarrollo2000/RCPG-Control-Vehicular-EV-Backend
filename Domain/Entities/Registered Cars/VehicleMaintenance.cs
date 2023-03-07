using Domain.Entities.Identity;
using Domain.Enums;

namespace Domain.Entities.Registered_Cars
{
    public class VehicleMaintenance : BaseEntity
    {
        public VehicleMaintenance()
        {
            MaintenanceProgress = new HashSet<MaintenanceProgress>();
        }
        public string ReasonForMaintenance { get; set; }
        public string? Comment { get; set; }
        public DateTime? MaintenanceDate { get; set; }
        public VehicleServiceStatus Status { get; set; }

        public int? InitialMileage { get; set; }
        public CurrentFuel? InitialFuel { get; set; }
        public int? FinalMileage { get; set; }
        public CurrentFuel? FinalFuel { get; set; }


        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }

        public int? WorkShopId { get; set; }
        public virtual VehicleMaintenanceWorkshop? WorkShop { get; set; }

        public int? ApprovedByUserId { get; set; }
        public virtual AppUser? ApprovedByUser { get; set; }

        public int? ReportId { get; set; }
        public virtual VehicleReport Report { get; set; }

        public virtual ICollection<MaintenanceProgress> MaintenanceProgress { get; set; }
    }
}
