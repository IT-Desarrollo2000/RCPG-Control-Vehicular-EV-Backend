using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
