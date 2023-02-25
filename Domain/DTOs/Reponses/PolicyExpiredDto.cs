using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class PolicyExpiredDto
    {
        public string StatusMessage { get; set; }
        public string StatusColor { get; set; }
        public string StatusName { get; set; }
        public int PolicyId { get; set; }
        public string PolicyNumber { get; set; }
        public int? VehicleId { get; set; }
        public string? VehicleName { get; set; }
        public LicenceExpStopLight ExpirationType { get; set; }
        public DateTime PolicyExpirationDate { get; set; }
    }
}
