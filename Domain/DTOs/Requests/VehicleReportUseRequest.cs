using Domain.Enums;

namespace Domain.DTOs.Requests
{
    public class VehicleReportUseRequest
    {
     
        public int VehicleId { get; set; }
        public double? FinalMileage { get; set; }
        public ReportUseType StatusReportUse { get; set; }
        public string Observations { get; set; }
        public DateTime UseDate { get; set; }
        public int? UserProfileId { get; set; }
        public CurrentFuel? CurrentFuelLoad { get; set; }
        public CurrentFuel? LastFuelLoad { get; set; }
        public destinolistdto Destination { get; set; }

    }

    public class VehicleReportUseFastTravel
    {

        public int VehicleId { get; set; }
        public ReportUseType StatusReportUse { get; set; }
        public int? UserProfileId { get; set; }
        public destinolistdto Destination { get; set; }

    }

    public class destinolistdto
    {
        public string DestinationName { get; set; }
        public double? Latitud { get; set; }
        public double? Longitude { get; set; }
    }
}
