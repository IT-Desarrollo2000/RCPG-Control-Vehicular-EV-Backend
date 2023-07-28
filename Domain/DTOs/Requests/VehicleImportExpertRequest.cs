using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class VehicleImportExpertRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Serial { get; set; }
        [Required]
        public bool IsUtilitary { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public int ModelYear { get; set; }
        
        public int? FuelCapacity { get; set; }
      
        public CurrentFuel? CurrentFuel { get; set; }
        
        public FuelType? FuelType { get; set; }
        [Required]
        public VehicleType VehicleType { get; set; }
        
        public int? ServicePeriodMonths { get; set; }
       
        public int? ServicePeriodKM { get; set; }
        
        public OwnershipType OwnershipType { get; set; }
        public string? OwnersName { get; set; }
        
        public decimal? DesiredPerformance { get; set; }
        
        public int CurrentKM { get; set; }
        public string? VehicleObservation { get; set; } = "";
        [Required]
        public string CarRegistrationPlate { get; set; }
        public string? FuelCardNumber { get; set; }
        [Required]
        public string? VehicleResponsibleName { get; set; }
        [Required]
        public bool IsClean { get; set; }
        [Required]
        public List<int> DepartmentsToAssign { get; set; }
        public string? MotorSerialNumber { get; set; }
        public string? InvoiceNumber { get; set; }
        [Required]
        public int PropietaryId { get; set; }

        public string? PolicyNumber { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string? NameCompany { get; set; }
        public decimal? PolicyCostValue { get; set; }
    }
}
