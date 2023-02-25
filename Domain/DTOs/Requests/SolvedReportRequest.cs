using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Requests
{
    public class SolvedReportRequest
    {
        [Required]
        public int ReportId { get; set; }

        [Required]
        public int AdminUserId { get; set; }

        [Required]
        public string ResolutionComment { get; set; }

        [Required]
        public ReportStatusType Status { get; set; }

    }
}
