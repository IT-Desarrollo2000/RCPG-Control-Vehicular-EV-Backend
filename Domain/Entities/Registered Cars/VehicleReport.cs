using Domain.Entities.Identity;
using Domain.Entities.Profiles;
using Domain.Enums;

namespace Domain.Entities.Registered_Cars
{
    public class VehicleReport : BaseEntity
    {
        public VehicleReport()
        {
            this.Expenses = new HashSet<Expenses>();
            this.VehicleReportImages = new HashSet<VehicleReportImage>();
            this.Maintenances = new HashSet<VehicleMaintenance>();
        }

        public ReportType ReportType { get; set; }
        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public string? Commentary { get; set; }
        public int? MobileUserId { get; set; }
        public virtual UserProfile? MobileUser { get; set; }
        public int? AdminUserId { get; set; }
        public virtual AppUser? AdminUser { get; set; }
        public DateTime ReportDate { get; set; }
        public bool IsResolved { get; set; }
        public double? GasolineLoadAmount { get; set; }
        public int? GasolineCurrentKM { get; set; }
        public string? ReportSolutionComment { get; set; }
        public ReportStatusType ReportStatus { get; set; }
        public int? VehicleReportUseId { get; set; }
        public virtual VehicleReportUse? VehicleReportUses { get; set; }
        public int? SolvedByAdminUserId { get; set; }
        public virtual AppUser? SolvedByAdminUser { get; set; }
        public double? AmountGasoline { get; set; }

        //Collections
        public virtual ICollection<Expenses> Expenses { get; set; }
        public virtual ICollection<VehicleMaintenance> Maintenances { get; set; }
        public virtual ICollection<VehicleReportImage> VehicleReportImages { get; set; }

    }
}
