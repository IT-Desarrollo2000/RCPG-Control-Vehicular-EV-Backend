using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class UseReportFastTravelRequest
    {
        [Required]
        public int VehicleId { get; set; }

        [Required]
        public int UserProfileId { get; set; }
    }
}
