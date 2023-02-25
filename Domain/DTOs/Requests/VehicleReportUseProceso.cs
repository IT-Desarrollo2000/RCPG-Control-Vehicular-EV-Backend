using Domain.Enums;

namespace Domain.DTOs.Requests
{
    public class VehicleReportUseProceso
    {

        public int VehicleId { get; set; }
        public ReportUseType StatusReportUse { get; set; }
        public string Observations { get; set; }
        public DateTime UseDate { get; set; }
        public int? UserProfileId { get; set; }
        public CurrentFuel? CurrentFuelLoad { get; set; }
        public virtual destinolistdtoo Destination { get; set; }
    }

    public class destinolistdtoo
    {
        public string DestinationName { get; set; }
        public double? Latitud { get; set; }
        public double? Longitude { get; set; }
    }

}
