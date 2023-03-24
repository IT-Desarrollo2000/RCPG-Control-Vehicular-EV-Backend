using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class UserInTravelDto
    {
        public int DriverId { get; set; }
        public string? VehicleQRId { get; set; }
        public bool IsInTravel { get; set; }
        public int? UseReportId { get; set; }
    }
}
