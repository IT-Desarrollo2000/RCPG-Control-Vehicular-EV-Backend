using Domain.Enums;

namespace Domain.DTOs.Filters
{
    public class VehicleServiceFilter
    {
        public string? WhereService { get; set; }
        public string? CarryPerson { get; set; }
        public int? VehicleId { get; set; }
        public VehicleServiceType? TypeService { get; set; }
        public DateTime? NextService { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
