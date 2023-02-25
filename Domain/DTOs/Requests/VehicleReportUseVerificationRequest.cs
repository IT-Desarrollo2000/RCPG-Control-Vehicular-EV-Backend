using System.ComponentModel.DataAnnotations;

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
