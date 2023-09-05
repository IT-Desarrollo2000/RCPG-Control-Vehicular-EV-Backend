using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class PolicyExportDto
    {
        public int PolicyId { get; set; }
        public int VehicleId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public string Serial { get; set; }
        public string? CarRegistrationPlate { get; set; }
        public string? MotorSerialNumber { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string NameCompany { get; set; }
        public decimal? PolicyCostValue { get; set; }
    }
}
