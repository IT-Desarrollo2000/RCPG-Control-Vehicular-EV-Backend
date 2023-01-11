using Domain.Entities.Registered_Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class VehicleMaintenanceDto
    {
        public int Id { get; set; }
        public string WhereServiceMaintenance { get; set; }
        public string? CarryPerson { get; set; }
        public string CauseServiceMaintenance { get; set; }
        public int VehicleId { get; set; }
        public DateTime? NextServiceMaintenance { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual ICollection<VehicleMaintenanceWorkshop> VehicleMaintenanceWorkshops { get; set; }
    }
}
