namespace Domain.DTOs.Reponses
{
    public class PolicyDto
    {
        public int Id { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int? VehicleId { get; set; }
        public UnrelatedVehiclesDto Vehicle { get; set; }
    }
}
