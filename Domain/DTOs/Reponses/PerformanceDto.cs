using Domain.Entities.Registered_Cars;

namespace Domain.DTOs.Reponses
{
    public class PerformanceDto
    {
        public int VehicleId { get; set; }
        public double CurrentKm { get; set; }
        public double LastKm { get; set; }
        public double GasolineLoadAmount { get; set; }

    }
}
