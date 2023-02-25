using Domain.Entities.Registered_Cars;

namespace Domain.DTOs.Reponses
{
    public class GraphicsDto
    {
        public virtual Vehicle Vehicle { get; set; }
        public virtual IEnumerable<VehicleMaintenance> VehicleMaintenances { get; set; }
        public virtual IEnumerable<VehicleService> VehicleServices { get; set; }
    }
}
