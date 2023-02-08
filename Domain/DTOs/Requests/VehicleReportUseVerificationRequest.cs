using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class VehicleReportUseVerificationRequest
    {

        [Required]
        public int AppUserId { get; set; }
        [Required]
        public bool Verification { get; set; }
    }
}
