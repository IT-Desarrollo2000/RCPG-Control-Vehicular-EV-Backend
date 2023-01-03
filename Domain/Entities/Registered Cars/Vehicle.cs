using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Registered_Cars
{
    public class Vehicle : BaseEntity
    {
        public string Name { get; set; }
        public string Serial { get; set; }
        public bool IsUtilitary { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public virtual ICollection<VehicleService> VehicleServices { get; set; }
        public virtual ICollection<Checklist> Checklists { get; set; }
    }
}
