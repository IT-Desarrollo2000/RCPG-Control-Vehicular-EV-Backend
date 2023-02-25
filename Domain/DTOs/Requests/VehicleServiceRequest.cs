using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Requests
{
    public class VehicleServiceRequest
    {
        [Required]
        public int WorkShopId { get; set; }
        [Required]
        public int ServiceUserId { get; set; }
        [Required]
        public int VehicleId { get; set; }
        [Required]
        public VehicleServiceType TypeService { get; set; }
    }
}
