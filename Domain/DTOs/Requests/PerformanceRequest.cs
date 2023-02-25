namespace Domain.DTOs.Requests
{
    public class PerformanceRequest
    {
        public int VehicleId { get; set; }
        public decimal CurrentKm { get; set; }
        public decimal PreviousKm { get; set; }


    }
}
