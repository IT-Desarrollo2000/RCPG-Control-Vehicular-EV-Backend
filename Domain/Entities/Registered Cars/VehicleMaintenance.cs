using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Registered_Cars
{
    public class VehicleMaintenance : BaseEntity
    {
    

        public string WhereServiceMaintenance { get; set; }
        public string? CarryPerson { get; set; }
        public string CauseServiceMaintenance { get; set; }
        public int VehicleId { get; set; }
        public DateTime? NextServiceMaintenance { get; set; }
        public int? VehicleMaintenanceWorkshopId { get; set; }
        public virtual VehicleMaintenanceWorkshop? VehicleMaintenanceWorkshop { get; set; }
        public virtual Vehicle Vehicle { get; set; }


    }
}
