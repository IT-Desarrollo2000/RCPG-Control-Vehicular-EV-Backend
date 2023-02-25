namespace Domain.DTOs.Reponses
{
    public class UnrelatedDestinationOfReportUseDto
    {
        public int Id { get; set; }
        public string DestinationName { get; set; }
        public double? Latitud { get; set; }
        public double? Longitude { get; set; }
        public int? VehicleReportUseId { get; set; }
    }
}
