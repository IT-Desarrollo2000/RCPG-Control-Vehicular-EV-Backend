using Domain.Entities.Propietary;
using Domain.Entities.Registered_Cars;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class VehicleExportDto
    {
        public VehicleExportDto() 
        { 
            AssignedDepartments = new List<ShortDepartmentDto>();
            VehicleImages = new List<VehicleImageDto>();
            PhotosOfCirculationCards = new List<PhotosOfCirculationCardDto>();
        }
        public int Id { get; set; }
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
        public string? VehicleObservation { get; set; }
        public string? CarRegistrationPlate { get; set; }
        public bool IsClean { get; set; }
        public int? PolicyId { get; set; }
        public string? FuelCardNumber { get; set; }
        public string? VehicleResponsibleName { get; set; }
        public string? MotorSerialNumber { get; set; }
        public string? InvoiceNumber { get; set; }
        public PropietaryDto? Propietary { get; set; }
        public ExportPolicyDto? Policy { get; set; }
        public ChecklistDto Checklist { get; set; }
        public List<ShortDepartmentDto> AssignedDepartments { get; set;}
        public List<VehicleImageDto> VehicleImages { get; set;}
        public List<PhotosOfCirculationCardDto> PhotosOfCirculationCards { get; set;}

    }

    public class ExportPolicyDto
    {
        public int Id { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string NameCompany { get; set; }
        public List<PhotosOfPolicyDto> PhotosOfPolicies { get; set; }
        public decimal? PolicyCostValue { get; set; }
    }
}
