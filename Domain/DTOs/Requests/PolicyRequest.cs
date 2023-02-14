
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class PolicyRequest
    {
        public string PolicyNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int? VehicleId { get; set; }
    }
}
