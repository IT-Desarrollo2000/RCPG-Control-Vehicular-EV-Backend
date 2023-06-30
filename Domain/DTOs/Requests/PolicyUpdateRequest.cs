using Microsoft.AspNetCore.Http;
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
        public PolicyUpdateRequest()
        {
            Images = new List<IFormFile>();
        }

        [Required]
        public int PolicyId { get; set; }
        public string? PolicyNumber { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string NameCompany { get; set; }
        public int? VehicleId { get; set; }
        public List<IFormFile> Images { get; set; }
        public decimal? PolicyCostValue { get; set; }
    }
}
