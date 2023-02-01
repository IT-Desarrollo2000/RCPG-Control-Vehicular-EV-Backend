using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class DestinationOfReportUseRequest
    {
        public string DestinationName { get; set; }
        public double? Latitud { get; set; }
        public double? Longitude { get; set; }
        public int? VehicleReportUseId { get; set; }
    }
}
