using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Registered_Cars
{
    public class VehicleImage : BaseEntity
    {
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public string FilePath { get; set; }
        public string FileURL { get; set; }
    }
}
