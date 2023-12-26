using Domain.DTOs.Reponses;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Requests
{
    public class VehicleMaintenanceRequest
    {
        [Required]
        public string ReasonForMaintenance { get; set; }
        [Required]
        public int VehicleId { get; set; }

        public DateTime? MaintenanceDate { get; set; }
        public int? InitialMileage { get; set; }
        public int? InitialCharge { get; set; }

        public int? WorkShopId { get; set; }
        public int? AdminUserId { get; set; }
        public int? ReportId { get; set; }
    }
}
