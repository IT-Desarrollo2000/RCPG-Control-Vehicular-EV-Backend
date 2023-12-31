using Domain.Entities.Departament;
using Domain.Entities.Municipality;
using Domain.Entities.Registered_Cars;
using Domain.Enums;
using System.ComponentModel;

namespace Domain.DTOs.Reponses
{
    public class VehiclesDto
    {
        public VehiclesDto()
        {
            VehicleImages = new List<VehicleImageDto>();
            Policies = new List<ShortPolicyDto>();
            AssignedDepartments = new List<ShortDepartmentDto>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Serial { get; set; }
        public bool IsUtilitary { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public int ModelYear { get; set; }
        public int? ChargeCapacityKwH { get; set; }
        public int CurrentChargeKwH { get; set; }
        public VehicleType VehicleType { get; set; }
        public VehicleStatus VehicleStatus { get; set; }
        public int ServicePeriodMonths { get; set; }
        public int ServicePeriodKM { get; set; }
        public OwnershipType OwnershipType { get; set; }
        public string? OwnersName { get; set; }
        public decimal DesiredPerformance { get; set; }
        public int? CurrentKM { get; set; }
        public int? InitialKM { get; set; }
        public List<VehicleImageDto> VehicleImages { get; set; }
        public List<ChecklistDto> Checklists { get; set; }
        public List<ShortDepartmentDto> AssignedDepartments { get; set; }
        public List<PhotosOfCirculationCardDto> PhotosOfCirculationCards { get; set; }
        public List<PhotosOfPolicyDto> PhotosOfPolicies { get; set; }
        public string VehicleQRId { get; set; }
        public string? VehicleObservation { get; set; }
        public ShortPolicyDto? Policy { get; set; }
        public string? CarRegistrationPlate { get; set; }
        public bool IsClean { get; set; }
        public string? FuelCardNumber { get; set; }
        public string? VehicleResponsibleName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<ShortPolicyDto> Policies { get; set; }
        public string? MotorSerialNumber { get; set; }
        public string? InvoiceNumber { get; set; }
        public int? PropietaryId { get; set; }
        public PropietaryDto? Propietary { get; set; }
        public int? MunicipalityId { get; set; }
        public virtual MunicipalitiesDto? Municipalities { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? IVA { get; set; }
        public decimal? Total { get; set; }
        public bool? ResponsiveLetter { get; set; }
        public bool? DuplicateKey { get; set; }
        [DisplayName("TenencyValidity")]
        public DateTime? TenencyPaymentDate { get; set; }
        public DateTime? PlatePaymentDate { get; set; }
        public string? InvoiceFileUrl { get; set; }
        public bool IsTenencyPaid
        {
            get
            {
                return TenencyPaymentDate.HasValue ? TenencyPaymentDate.Value.Year == DateTime.UtcNow.Year : false;
            }
        }
        public bool IsPlatePaid
        {
            get
            {
                return PlatePaymentDate.HasValue;
            }
        }
    }

}


