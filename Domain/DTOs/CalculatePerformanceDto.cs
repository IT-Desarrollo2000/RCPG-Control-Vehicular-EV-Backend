namespace Domain.DTOs
{
    public class CalculatePerformanceDto
    {
        public double PreviusKm { get; set; }
        public double CurrentKm { get; set; }
        public int? GasolineLoadAmount { get; set; }
        public double MileageTraveled { get; set; }
        public double Perfomance { get; set; }
    }
}
