﻿using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public OwnershipType? OwnershipType { get; set; }
        public string? OwnersName { get; set; }
        public string? CarRegistrationPlate { get; set; }
        public bool? IsClean { get; set; }
        public decimal? MinDesiredPerformance { get; set; }
        public decimal? MaxDesiredPerformance { get; set; }
        public DateTime? CreatedBefore { get; set; }
        public DateTime? CreatedAfter { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
