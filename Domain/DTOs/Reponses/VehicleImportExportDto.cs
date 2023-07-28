using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class VehicleImportExportDto
    {
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
        public int ServicePeriodMonths { get; set; }
        public int ServicePeriodKM { get; set; }
        public OwnershipType OwnershipType { get; set; }
        public string? OwnersName { get; set; }
        public decimal DesiredPerformance { get; set; }
        public int CurrentKM { get; set; }
        public string? VehicleObservation { get; set; } = "";
        public string CarRegistrationPlate { get; set; }
        public string? FuelCardNumber { get; set; }
        public string? VehicleResponsibleName { get; set; }
        public bool IsClean { get; set; }
        public List<int> DepartmentsToAssign { get; set; }
        public string? MotorSerialNumber { get; set; }
        public string? InvoiceNumber { get; set; }
        public int? PropietaryId { get; set; }

        public string? PolicyNumber { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string NameCompany { get; set; }
        public decimal? PolicyCostValue { get; set; }
    }
}
