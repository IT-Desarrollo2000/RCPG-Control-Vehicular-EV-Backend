using Domain.Entities.Departament;
using Domain.Entities.Registered_Cars;
using Domain.Enums;

namespace Domain.DTOs.Reponses
{
    public class VehiclesDto
    {
        public VehiclesDto()
        {
            VehicleImages = new List<VehicleImageDto>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Serial { get; set; }
        public bool IsUtilitary { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public int ModelYear { get; set; }
        public int FuelCapacity { get; set; }
        public CurrentFuel CurrentFuel { get; set; }
        public FuelType FuelType { get; set; }
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
        public List<DepartamentDto> AssignedDepartments { get; set; }
        public List<PhotosOfCirculationCardDto> PhotosOfCirculationCards { get; set; }
        public string VehicleQRId { get; set; }
        public string? VehicleObservation { get; set; }
        public ShortPolicyDto? Policy { get; set; }
        public string? CarRegistrationPlate { get; set; }
        public bool IsClean { get; set; }
        public string? FuelCardNumber { get; set; }
        public string? VehicleResponsibleName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
