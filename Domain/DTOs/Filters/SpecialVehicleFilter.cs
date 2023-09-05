using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Filters
{
    public class SpecialVehicleFilter
    {
        public string? VehicleResponsibleName { get; set; }
        public string? Brand { get; set; }
        public int? DepartmentId { get; set; }
        public int? PropietaryId { get; set; }
        public string? SerialNumber { get; set; }
        public string? ModelName { get; set; }
        public int? ModelYear { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
