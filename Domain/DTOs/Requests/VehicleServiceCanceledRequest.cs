using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class VehicleServiceCanceledRequest
    {
        [Required]
        public int VehicleServiceId { get; set; }
        public string? Comment { get; set; }
    }
}
