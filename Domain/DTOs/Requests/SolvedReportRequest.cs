using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
