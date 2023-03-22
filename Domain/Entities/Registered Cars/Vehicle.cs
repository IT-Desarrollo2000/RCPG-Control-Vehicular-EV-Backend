using Domain.Entities.Departament;
using Domain.Enums;

namespace Domain.Entities.Registered_Cars
{
    public class Vehicle : BaseEntity
    {
        public Vehicle()
        {
            this.VehicleServices = new HashSet<VehicleService>();
            this.VehicleImages = new HashSet<VehicleImage>();
            this.AssignedDepartments = new HashSet<Departaments>();
            this.Checklists = new HashSet<Checklist>();
            this.Expenses = new HashSet<Expenses>();
            this.VehicleMaintenances = new HashSet<VehicleMaintenance>();
            this.VehicleReports = new HashSet<VehicleReport>();
            this.VehicleReportsUses = new HashSet<VehicleReportUse>();
        }
        public string Name { get; set; }
        public string Serial { get; set; }
        public bool IsUtilitary { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public int ModelYear { get; set; }
        public int FuelCapacity { get; set; }
        public int CurrentKM { get; set; }
        public int InitialKM { get; set; }
        public CurrentFuel CurrentFuel { get; set; }
        public FuelType FuelType { get; set; }
        public VehicleType VehicleType { get; set; }
        public VehicleStatus VehicleStatus { get; set; }
        public int ServicePeriodMonths { get; set; }
        public int ServicePeriodKM { get; set; }
        public OwnershipType OwnershipType { get; set; }
        public string? OwnersName { get; set; }
        public decimal DesiredPerformance { get; set; }
        public string VehicleQRId { get; set; }
        public string? VehicleObservation { get ; set; }
        public string? CarRegistrationPlate { get; set; }
        public bool IsClean { get; set; } 
        public virtual Policy? Policy { get; set; }
        public string? FuelCardNumber { get; set; }
        public string? VehicleResponsibleName { get; set; }

        public virtual ICollection<Departaments> AssignedDepartments { get; set; }
        public virtual ICollection<VehicleImage> VehicleImages { get; set; }
        public virtual ICollection<VehicleService> VehicleServices { get; set; }
        public virtual ICollection<Checklist> Checklists { get; set; }
        public virtual ICollection<Expenses> Expenses { get; set; }
        public virtual ICollection<VehicleMaintenance> VehicleMaintenances { get; set; }
        public virtual ICollection<VehicleReport> VehicleReports { get; set; }
        public virtual ICollection<VehicleReportUse> VehicleReportsUses { get; set; }
    }
}
