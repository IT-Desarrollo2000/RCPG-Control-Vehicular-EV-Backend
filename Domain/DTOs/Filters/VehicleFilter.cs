using Domain.Entities.Departament;
using Domain.Enums;

namespace Domain.DTOs.Filters
{
    public class VehicleFilter
    {
        public string? Name { get; set; }
        public string? Serial { get; set; }
        public bool? IsUtilitary { get; set; }
        public string? Color { get; set; }
        public string? Brand { get; set; }
        public int? ModelYear { get; set; }
        public int? FuelCapacity { get; set; }
        public CurrentFuel? CurrentFuel { get; set; }
        public FuelType? FuelType { get; set; }
        public VehicleType? VehicleType { get; set; }
        public VehicleStatus? VehicleStatus { get; set; }
        public int? ServicePeriodMonths { get; set; }
        public int? ServicePeriodKM { get; set; }
        public OwnershipType? OwnershipType { get; set; }
        public string? OwnersName { get; set; }
        public string? CarRegistrationPlate { get; set; }
        public bool? IsClean { get; set; }
        public string? FuelCardNumber { get; set; }
        public string? InvoiceNumber { get; set; }
        public int? PropietaryId { get; set; }
        public int? MunicipalityId { get; set; }
        public decimal? MinDesiredPerformance { get; set; }
        public decimal? MaxDesiredPerformance { get; set; }
        public DateTime? CreatedBefore { get; set; } 
        public DateTime? CreatedAfter { get; set; }
        public int? AssignedDepartmentsId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
