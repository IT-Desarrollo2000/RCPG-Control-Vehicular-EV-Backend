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
        public FinalizeMaintenanceRequest () 
        {
            ExpenseId = new List<int?>();
        }

        [Required]
        public int MaintenanceId { get; set; }

        [Required]
        public int FinalMileage { get; set; }

        [Required]
        public CurrentFuel FinalFuel { get; set; }

        public List<int?> ExpenseId { get; set; }

        public string? Comment { get; set; }
    }

    public class CancelMaintenanceRequest
    {

        [Required]
        public int MaintenanceId { get; set; }
        public string? Comment { get; set; }
    }
}
