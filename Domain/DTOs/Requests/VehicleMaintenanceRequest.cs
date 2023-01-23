using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class VehicleMaintenanceRequest
    {
        public string WhereServiceMaintenance { get; set; }
        public string? CarryPerson { get; set; }
        public string CauseServiceMaintenance { get; set; }
        public int VehicleId { get; set; }
        public DateTime? NextServiceMaintenance { get; set; }
        public int? VehicleMaintenanceWorkshopId { get; set; }
    }
}
