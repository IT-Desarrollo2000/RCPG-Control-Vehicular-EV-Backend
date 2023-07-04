using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Filters
{
    public class VehicleExportFilter
    {
        public CurrentFuel? CurrentFuel { get; set; }
        public OwnershipType? OwnershipType { get; set; }
        public VehicleType? VehicleType { get; set; }
        public VehicleStatus? VehicleStatus { get; set; }
        public FuelType? FuelType { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
