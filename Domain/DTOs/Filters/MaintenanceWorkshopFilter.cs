using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Filters
{
    public class MaintenanceWorkshopFilter
    {
        public string? Name { get; set; }
        public string? Ubication { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? Telephone { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
