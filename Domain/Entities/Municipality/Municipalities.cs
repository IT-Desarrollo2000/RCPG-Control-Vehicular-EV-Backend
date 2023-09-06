using Domain.Entities.Registered_Cars;
using Domain.Entities.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Municipality
{
    public class Municipalities : BaseEntity
    {
        public string Name { get; set; }
        public int StateId { get; set; }
        public virtual States States { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
