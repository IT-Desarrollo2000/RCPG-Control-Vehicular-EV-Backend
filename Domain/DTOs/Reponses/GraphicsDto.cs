using Domain.Entities.Registered_Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class GraphicsDto
    {
        public virtual Vehicle Vehicle { get; set; }
        public virtual IEnumerable<VehicleMaintenance> VehicleMaintenances { get; set; }
        public virtual IEnumerable<VehicleService> VehicleServices { get; set;}
    }
}
