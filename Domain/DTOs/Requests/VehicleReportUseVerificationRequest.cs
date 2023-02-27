using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Requests
{
    public class VehicleReportUseVerificationRequest
    {
        [Required]
        public int UseReportId { get; set; }

        [Required]
        public int AppUserId { get; set; }
    }
}
