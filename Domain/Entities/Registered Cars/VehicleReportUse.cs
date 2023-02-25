using Domain.Entities.Identity;
using Domain.Entities.Profiles;
using Domain.Enums;

namespace Domain.Entities.Registered_Cars
{
    public class VehicleReportUse : BaseEntity
    {
        public VehicleReportUse()
        {
            this.VehicleReport = new HashSet<VehicleReport>();
            this.Destinations = new HashSet<DestinationOfReportUse>();
        }

        public int VehicleId { get; set; }
        public double? InitialMileage { get; set; }
        public double? FinalMileage { get; set; }
        public ReportUseType StatusReportUse { get; set; }
        public string? Observations { get; set; }
        public int? ChecklistId { get; set; }
        public DateTime? UseDate { get; set; }
        public int? UserProfileId { get; set; }
        public int? AppUserId { get; set; }
        public CurrentFuel? CurrentFuelLoad { get; set; }
        public CurrentFuel? LastFuelLoad { get; set; }
        public bool? Verification { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual Checklist? Checklist { get; set; }
        public ICollection<VehicleReport?> VehicleReport { get; set; }
        public virtual UserProfile? UserProfile { get; set; }
        public virtual AppUser? AppUser { get; set; }
        public ICollection<DestinationOfReportUse?> Destinations { get; set; } //ojo
    }
}
