using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class UseReportUpdateRequest
    {
        public UseReportUpdateRequest()
        {
            DestinationsToAdd = new List<DestinationRequest>();
            DestinationsToRemove = new List<int>();
        }

        [Required]
        public int UseReportId { get; set; }
        public double? InitialMileage { get; set; }
        public double? FinalMileage { get; set; }
        public string? Observations { get; set; }
        public DateTime? UseDate { get; set; }
        public int? CurrentChargeLoad { get; set; }
        public int? LastChargeLoad { get; set; }

        public List<int> DestinationsToRemove { get; set; }
        public List<DestinationRequest> DestinationsToAdd { get; set; }
    }
}
