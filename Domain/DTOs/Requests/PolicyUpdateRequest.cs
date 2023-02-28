using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class PolicyUpdateRequest
    {
        [Required]
        public int PolicyId { get; set; }
        public string? PolicyNumber { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? VehicleId { get; set; }
    }
}
