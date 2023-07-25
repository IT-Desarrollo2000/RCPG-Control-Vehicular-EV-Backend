using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Filters
{
    public class PolicyExportFilter
    {
        public OwnershipType? OwnershipType { get; set; }
        public VehicleType? VehicleType { get; set; }
        public VehicleStatus? VehicleStatus { get; set; }
        public LicenceExpStopLight StopLight { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
