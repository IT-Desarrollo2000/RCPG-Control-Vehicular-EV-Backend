using Domain.Enums;

namespace Domain.DTOs.Filters
{
    public class VehicleMaintenanceFilter
    {
        public string? ReasonForMaintenance { get; set; }
        public string? Comment { get; set; }
        public int? VehicleId { get; set; }
        public DateTime? MaintenanceDate { get; set; }
        public int? AdminId { get; set; }
        public int? ReportId { get; set; }
        public int? WorkshopId { get; set; }
        public VehicleServiceStatus? Status { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
