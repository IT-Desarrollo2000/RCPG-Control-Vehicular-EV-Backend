using Domain.Entities.Identity;
using Domain.Entities.Registered_Cars;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class MaintenanceUpdateRequest
    {
        [Required]
        public int MaintenanceId { get; set; }
        public string? ReasonForMaintenance { get; set; }
        public string? Comment { get; set; }
        public DateTime? MaintenanceDate { get; set; }
        public int? WorkShopId { get; set; }
        public int? ReportId { get; set; }
        public int? ExpenseId { get; set; }
    }
}
