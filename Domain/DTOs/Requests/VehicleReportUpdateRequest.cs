using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Requests
{
    public class VehicleReportUpdateRequest
    {
        public VehicleReportUpdateRequest()
        {

        }

        [Required]
        public int ReportId { get; set; }

        public ReportType? ReportType { get; set; }
        public string? Commentary { get; set; }
        public DateTime? ReportDate { get; set; }
        public int? GasolineLoadAmount { get; set; }
        public int? GasolineCurrentKM { get; set; }
        public string? ReportSolutionComment { get; set; }

        public List<int> ExpensesToAdd { get; set; }
        public List<int> ExpensesToRemove { get; set; }
    }
}
