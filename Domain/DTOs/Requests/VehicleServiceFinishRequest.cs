using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class VehicleServiceFinishRequest
    {
        [Required]
        public int VehicleServiceId { get; set; }
        [Required]
        public DateTime NextService { get; set; }
        [Required]
        public int NextServiceKM { get; set; }
        [Required]
        public int FinalMileage { get; set; }
        [Required]
        public CurrentFuel FinalFuel { get; set; }
        public string? Comment { get; set; }
        public int? ExpenseId { get; set; }

    }
}
