using Domain.Entities.Registered_Cars;

namespace Domain.DTOs.Reponses
{
    public class PerformanceDto
    {
        public Vehicle Vehicle { get; set; }
        public decimal PerformanceOfVehicle { get; set; }

    }
}
