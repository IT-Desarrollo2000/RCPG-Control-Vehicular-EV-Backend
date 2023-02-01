using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Registered_Cars
{
    public class DestinationOfReportUse : BaseEntity
    {
        public string DestinationName { get; set; }
        public double? Latitud { get; set; }
        public double? Longitude { get; set; }
        public int? VehicleReportUseId { get; set; }
        public virtual VehicleReportUse VehicleReportUses { get; set; }
    }
}
