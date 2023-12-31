using Domain.Entities.Departament;
using Domain.Entities.Municipality;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

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
            this.PhotosOfCirculationCards = new HashSet<PhotosOfCirculationCard>();
            this.Tenencies = new HashSet<VehicleTenency>();
        }

        public string Name { get; set; }
        public string Serial { get; set; }
        public bool IsUtilitary { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public int ModelYear { get; set; }
        public int? ChargeCapacityKwH { get; set; }
        public int? CurrentKM { get; set; }
        public int InitialKM { get; set; }
        public int CurrentChargeKwH { get; set; }
        public VehicleType VehicleType { get; set; }
        public VehicleStatus VehicleStatus { get; set; }
        public int? AdditionalInformationId { get; set; }
        public virtual AdditionalInformation? AdditionalInformation { get; set; }
        public int ServicePeriodMonths { get; set; }
        public int ServicePeriodKM { get; set; }
        public OwnershipType OwnershipType { get; set; }
        public string? OwnersName { get; set; }
        public decimal? DesiredPerformance { get; set; }
        public string VehicleQRId { get; set; }
        public string? VehicleObservation { get ; set; }
        public string? CarRegistrationPlate { get; set; }
        public bool IsClean { get; set; } 
        public int? PolicyId { get; set; }
        public virtual Policy? Policy { get; set; }
        public string? FuelCardNumber { get; set; }
        public string? VehicleResponsibleName { get; set; }
        public string? MotorSerialNumber { get; set; }
        public string? InvoiceNumber { get; set; }
        public int? PropietaryId { get; set; }
        public virtual Propietary.Propietary Propietary { get; set; }
        public int? MunicipalityId { get; set; }
        public virtual Municipalities? Municipalities { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? IVA { get; set; }
        public decimal? Total { get; set; }
        public bool? ResponsiveLetter { get; set; }
        public bool? DuplicateKey { get; set; }
        public DateTime? TenencyPaymentDate { get; set; }
        public DateTime? PlatePaymentDate { get; set;  }
        public string? InvoiceFilePath { get; set; }
        public string? InvoiceFileUrl { get; set; }
        public virtual ICollection<Policy> Policies { get; set; }
        public virtual ICollection<Departaments> AssignedDepartments { get; set; }
        public virtual ICollection<VehicleImage> VehicleImages { get; set; }
        public virtual ICollection<VehicleService> VehicleServices { get; set; }
        public virtual ICollection<Checklist> Checklists { get; set; }
        public virtual ICollection<Expenses> Expenses { get; set; }
        public virtual ICollection<VehicleMaintenance> VehicleMaintenances { get; set; }
        public virtual ICollection<VehicleReport> VehicleReports { get; set; }
        public virtual ICollection<VehicleReportUse> VehicleReportsUses { get; set; }
        public virtual ICollection<PhotosOfCirculationCard> PhotosOfCirculationCards { get; set; }
        public virtual ICollection<VehicleTenency> Tenencies { get; set; }
    }
}
