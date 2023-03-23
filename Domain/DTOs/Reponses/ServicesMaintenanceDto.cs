using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class ServicesMaintenanceDto
    {
        public int VehicleId { get; set; }
        public int? LastServiceId { get; set; }
        public DateTime? LastServiceDate { get; set; }
        public int? LastMaintenanceId { get; set; }
        public DateTime? LastMaintenanceDate { get; set; }
    }
}
