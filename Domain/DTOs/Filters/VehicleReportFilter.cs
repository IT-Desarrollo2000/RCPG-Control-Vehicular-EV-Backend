using Domain.Entities.Identity;
using Domain.Entities.Profiles;
using Domain.Entities.Registered_Cars;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Filters
{
    public class VehicleReportFilter
    {
        public ReportType? ReportType { get; set; }
        public int? VehicleId { get; set; }
        public int? MobileUserId { get; set; }
        public int? AdminUserId { get; set; }
        public DateTime? ReportDate { get; set; }
        public bool? IsResolved { get; set; }
        public ReportStatusType? ReportStatus { get; set; }
        public int? VehicleReportUseId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
