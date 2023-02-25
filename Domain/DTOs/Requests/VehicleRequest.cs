using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Requests
{
    public class VehicleRequest
    {
        public VehicleRequest()
        {
            DepartmentsToAssign = new List<int>();
            Images = new List<IFormFile>();
        }

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
        [Required]
        public int FuelCapacity { get; set; }
        [Required]
        public CurrentFuel CurrentFuel { get; set; }
        [Required]
        public FuelType FuelType { get; set; }
        [Required]
        public VehicleType VehicleType { get; set; }
        [Required]
        public int ServicePeriodMonths { get; set; }
        [Required]
        public int ServicePeriodKM { get; set; }
        [Required]
        public OwnershipType OwnershipType { get; set; }
        public string? OwnersName { get; set; }
        [Required]
        public decimal DesiredPerformance { get; set; }
        [Required]
        public int CurrentKM { get; set; }
        public string? VehicleObservation { get; set; } = "";
        public string? CarRegistrationPlate { get; set; }
        [Required]
        public bool IsClean { get; set; } 
        public List<int> DepartmentsToAssign { get; set; }
        public List<IFormFile> Images { get; set; }

    }

    public class VehicleImageRequest
    {
        [Required]
        public IFormFile ImageFile { get; set; }
    }
}
