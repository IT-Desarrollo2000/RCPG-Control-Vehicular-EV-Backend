using Domain.Enums;

namespace Domain.DTOs.Requests
{
    public class VehiclesUpdateRequest
    {
        public VehiclesUpdateRequest()
        {
            DepartmentsToAdd = new List<int>();
            DepartmentsToRemove = new List<int>();
        }
        public string? Name { get; set; }
        public string? Serial { get; set; }
        public bool? IsUtilitary { get; set; }
        public string? Color { get; set; }
        public string? Brand { get; set; }
        public int? ModelYear { get; set; }
        public int? ChargeCapacityKwH { get; set; }
        public int? CurrentChargeKwH { get; set; }
        public VehicleType? VehicleType { get; set; }
        public int? ServicePeriodMonths { get; set; }
        public int? ServicePeriodKM { get; set; }
        public OwnershipType? OwnershipType { get; set; }
        public string? OwnersName { get; set; }
        public decimal? DesiredPerformance { get; set; }
        public string? VehicleObservation { get; set; }
        public string? CarRegistrationPlate { get; set; }
        public bool? IsClean { get; set; }
        public int? CurrentKM { get; set; }
        public int? InitialKM { get; set; }
        public List<int> DepartmentsToAdd { get; set; }
        public List<int> DepartmentsToRemove { get; set; }
        public string? FuelCardNumber { get; set; }
        public string? VehicleResponsibleName { get; set; }
        public string? MotorSerialNumber { get; set; }
        public string? InvoiceNumber { get; set; }
        public int? PropietaryId { get; set; }
        public int? MunicipalityId { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? IVA { get; set; }
        public decimal? Total { get; set; }
        public bool? ResponsiveLetter { get; set; }
        public bool? DuplicateKey { get; set; }
    }
}
