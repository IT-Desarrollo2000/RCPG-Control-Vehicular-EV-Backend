using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class GetServicesMaintenance
    {
        public int VehicleId { get; set; }
        public string NameVehicle { get; set; }
        public int TotalService { get; set; }
        public int TotalMaintenance { get; set; }
        public string Error { get; set; }
    }
}
