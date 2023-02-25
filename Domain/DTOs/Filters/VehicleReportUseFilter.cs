using Domain.Enums;

namespace Domain.DTOs.Filters
{
    public class VehicleReportUseFilter
    {
        public int? VehicleId { get; set; }
        public double? FinalMileage { get; set; }
        public ReportUseType? StatusReportUse { get; set; }
        public string? Observations { get; set; }
        public int? ChecklistId { get; set; }
        public DateTime? UseDate { get; set; }
        public int? UserProfileId { get; set; }
        public int? AppUserId { get; set; }
        public CurrentFuel? CurrentFuelLoad { get; set; }
        public bool? Verification { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
