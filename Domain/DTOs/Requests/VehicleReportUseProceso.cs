using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Requests
{
    public class VehicleReportUseProceso
    {
        public VehicleReportUseProceso()
        {
            Destinations = new List<DestinationRequest>();
        }

        [Required]
        public int VehicleId { get; set; }

        [Required]
        public DateTime UseDate { get; set; }

        [Required]
        public int UserProfileId { get; set; }

        public List<DestinationRequest> Destinations { get; set; }

        public CreationChecklistDto? InitialCheckList { get; set; }

        public bool? IsVehicleClean { get; set; }

        public int? CurrentChargeLoad { get; set; }

        public double? InitialMileage { get; set; }
        public double? InitialLatitude { get; set; }
        public double? InitialLongitude { get; set; }
    }

    public class UseReportAdminRequest
    {
        public UseReportAdminRequest()
        {
            Destinations = new List<DestinationRequest>();
        }

        [Required]
        public int VehicleId { get; set; }

        [Required]
        public DateTime UseDate { get; set; }

        [Required]
        public int AdminUserId { get; set; }

        public List<DestinationRequest> Destinations { get; set; }

        public CreationChecklistDto? InitialCheckList { get; set; }

        public bool? IsVehicleClean { get; set; }

        public int? CurrentChargeLoad { get; set; }

        public double? InitialMileage { get; set; }
        public double? InitialLatitude { get; set; }
        public double? InitialLongitude { get; set; }
    }
}
