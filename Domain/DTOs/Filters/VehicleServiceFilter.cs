using Domain.Enums;

namespace Domain.DTOs.Filters
{
    public class VehicleServiceFilter
    {
        public int? ServiceUserId { get; set; }
        public int? WorkShopId { get; set; }
        public int? VehicleId { get; set; }
        public VehicleServiceStatus? Status { get; set; }
        public VehicleServiceType? TypeService { get; set; }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
