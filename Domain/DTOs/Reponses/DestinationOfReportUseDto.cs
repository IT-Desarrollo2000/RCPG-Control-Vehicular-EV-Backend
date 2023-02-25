namespace Domain.DTOs.Reponses
{
    public class DestinationOfReportUseDto
    {
        public int Id { get; set; }
        public string DestinationName { get; set; }
        public double? Latitud { get; set; }
        public double? Longitude { get; set; }
        public int? VehicleReportUseId { get; set; }
        public virtual UnrelatedVehicleReportUseDto VehicleReportUses { get; set; }
    }
}
