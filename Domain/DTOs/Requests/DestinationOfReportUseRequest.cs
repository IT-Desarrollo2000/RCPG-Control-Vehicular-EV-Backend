using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Requests
{
    public class DestinationOfReportUseRequest
    {
        [Required]
        public string DestinationName { get; set; }
        public double? Latitud { get; set; }
        public double? Longitude { get; set; }
        [Required]
        public int VehicleReportUseId { get; set; }
    }
}
