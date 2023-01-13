using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Registered_Cars
{
    public class VehicleMaintenance : BaseEntity
    {
        public VehicleMaintenance()
        {
            this.VehicleMaintenanceWorkshops = new HashSet<VehicleMaintenanceWorkshop>();
        }

        public string WhereServiceMaintenance { get; set; }
        public string? CarryPerson { get; set; }
        public string CauseServiceMaintenance { get; set; }
        public int VehicleId { get; set; }
        public DateTime? NextServiceMaintenance { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual ICollection<VehicleMaintenanceWorkshop> VehicleMaintenanceWorkshops { get; set; }
    }
}
