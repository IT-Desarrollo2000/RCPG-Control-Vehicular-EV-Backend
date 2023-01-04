using Domain.Entities.Registered_Cars;
using Domain.Enums;

namespace Domain.DTOs.Reponses
{
    public class VehicleServiceDto
    {
        public int Id { get; set; }
        public string WhereService { get; set; }
        public string? CarryPerson { get; set; }
        public int VehicleId { get; set; }
        public VehicleServiceType TypeService { get; set; }
        public DateTime NextService { get; set; }
        public virtual Vehicle Vehicle { get; set; }

    }
}
