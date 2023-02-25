using Domain.Enums;

namespace Domain.DTOs.Filters
{
    public class VehicleReportFilter
    {
        public ReportType? ReportType { get; set; }
        public int? VehicleId { get; set; }
        public string? Commentary { get; set; }
        public int? MobileUserId { get; set; }
        public int? AdminUserId { get; set; }
        public DateTime? ReportDate { get; set; }
        public bool? IsResolved { get; set; }
        public int? GasolineLoadAmount { get; set; }
        public int? GasolineCurrentKM { get; set; }
        public string? ReportSolutionComment { get; set; }
        public ReportStatusType? ReportStatus { get; set; }
        public int? VehicleReportUseId { get; set; }
        public int? SolvedByAdminUserId { get; set; }
        public double? AmountGasoline { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
