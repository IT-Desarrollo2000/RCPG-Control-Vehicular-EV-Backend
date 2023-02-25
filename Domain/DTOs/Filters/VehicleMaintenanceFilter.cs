namespace Domain.DTOs.Filters
{
    public class VehicleMaintenanceFilter
    {
        public string? WhereServiceMaintenance { get; set; }
        public string? CarryPerson { get; set; }
        public string? CauseServiceMaintenance { get; set; }
        public int? VehicleId { get; set; }
        public DateTime? NextServiceMaintenance { get; set; }
        public int? VehicleMaintenanceWorkshopId { get; set; }
        // public virtual Vehicle Vehicle { get; set; }
        //public virtual ICollection<VehicleMaintenanceWorkshop> VehicleMaintenanceWorkshops { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
