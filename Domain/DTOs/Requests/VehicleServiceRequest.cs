using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Requests
{
    public class VehicleServiceRequest
    {
        [Required]
        public string WhereService { get; set; }
        public string CarryPerson { get; set; }
        [Required]
        public int VehicleId { get; set; }
        [Required]
        public VehicleServiceType TypeService { get; set; }
        [Required]
        public DateTime NextService { get; set; }
    }
}
