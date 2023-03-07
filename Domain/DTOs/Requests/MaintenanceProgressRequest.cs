using Domain.Entities.Identity;
using Domain.Entities.Profiles;
using Domain.Entities.Registered_Cars;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class MaintenanceProgressRequest
    {
        [Required]
        public int VehicleMaintenanceId { get; set; }
        [Required]
        public string Comment { get; set; }
        public int? MobileUserId { get; set; }
        public int? AdminUserId { get; set; }
    }
}
