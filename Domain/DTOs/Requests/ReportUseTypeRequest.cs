using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class ReportUseTypeRequest
    {
        [Required]
        public ReportUseType StatusReportUse { get; set; }
        public double? FinalMileage { get; set; }
        public DateTime UseDate { get; set; }
        public CurrentFuel? CurrentFuelLoad { get; set; }
    }
}
