using Domain.Enums;

namespace Domain.DTOs.Reponses
{
    public class UnrelatedVehiclesDto
    {
        public UnrelatedVehiclesDto()
        {
            AssignedDepartments = new List<ShortDepartmentDto>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Serial { get; set; }
        public bool IsUtilitary { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public int ModelYear { get; set; }
        public int FuelCapacity { get; set; }
        public List<ShortDepartmentDto> AssignedDepartments { get; set; }
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
        public string? VehicleObservation { get; set; }
        public string? FuelCardNumber { get; set; }
        public string? VehicleResponsibleName { get; set; }
        public string? CarRegistrationPlate { get; set; }
    }
}
