using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Registered_Cars
{
    public class VehicleService : BaseEntity
    {
        public string WhereService { get; set; }
        public string? CarryPerson  { get; set; }
        public int  VehicleId { get; set; }
        public VehicleServiceType TypeService { get; set; }
        public DateTime NextService { get; set; }
        public virtual Vehicle Vehicle { get; set; }

    }
}
