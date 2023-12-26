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
        public int? CurrentChargeKwH { get; set; }
        public OwnershipType? OwnershipType { get; set; }
        public VehicleType? VehicleType { get; set; }
        public VehicleStatus? VehicleStatus { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
