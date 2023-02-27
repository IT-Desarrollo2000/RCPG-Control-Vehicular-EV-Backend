using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class VehicleServiceUpdateRequest
    {
        [Required]
        public int VehicleServiceId { get; set; }
        public int? WorkShopId { get; set; }
        public int? VehicleId { get; set; }
        public VehicleServiceType? TypeService { get; set; }
        public DateTime? NextService { get; set; }
        public int? NextServiceKM { get; set; }
        public int? FinalMileage { get; set; }
        public CurrentFuel? FinalFuel { get; set; }
        public string? Comment { get; set; }
        public int? ExpenseId { get; set; }
    }
}
