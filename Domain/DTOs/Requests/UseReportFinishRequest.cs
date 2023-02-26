using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class UseReportFinishRequest
    {
        [Required]
        public int UseReportId { get; set; }

        [Required]
        public CurrentFuel FinalFuelLoad { get; set; }

        [Required]
        public double FinalMileage { get; set; }

        public bool? IsVehicleClean { get; set; }
        public string? Observations { get; set; }
        public CreationChecklistDto? FinalCheckList { get; set; }
    }

    public class UseReportFastTravelFinishRequest
    {
        public UseReportFastTravelFinishRequest()
        {
            Destinations = new List<DestinationRequest>();
        }

        [Required]
        public int UseReportId { get; set; }

        [Required]
        public CurrentFuel FinalFuelLoad { get; set; }

        [Required]
        public double FinalMileage { get; set; }

        public bool? IsVehicleClean { get; set; }
        public string? Observations { get; set; }
        public List<DestinationRequest> Destinations { get; set; }
        public CreationChecklistDto? FinalCheckList { get; set; }
    }

    public class UseReportCancelRequest
    {
        [Required]
        public int UseReportId { get; set; }
        public string? Observations { get; set; }
    }
}
