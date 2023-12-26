using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class FinalizeMaintenanceRequest
    {
        [Required]
        public int MaintenanceId { get; set; }

        [Required]
        public int FinalMileage { get; set; }

        [Required]
        public int FinalChargeKwH { get; set; }

        public string? Comment { get; set; }
    }

    public class CancelMaintenanceRequest
    {

        [Required]
        public int MaintenanceId { get; set; }
        public string? Comment { get; set; }
    }
}
