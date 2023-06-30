using Domain.Entities.Registered_Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Propietary
{
    public class Propietary : BaseEntity
    {
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public string? SurnameP { get; set; }
        public string? SurnameM { get; set; }
        public string? CompanyName { get; set; }
        public bool IsMoralPerson { get; set; } 
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
