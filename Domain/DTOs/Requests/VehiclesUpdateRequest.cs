using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class VehiclesUpdateRequest
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
        public int? ServicePeriodMonths { get; set; }
        public int? ServicePeriodKM { get; set; }
        public OwnershipType? OwnershipType { get; set; }
        public string? OwnersName { get; set; }
        public decimal? DesiredPerformance { get; set; }
        public string? VehicleObservation { get; set; }
        public string? CarRegistrationPlate { get; set; }
        public int? CurrentKM { get; set; }
        public int? InitialKM { get; set; }
    }
}
