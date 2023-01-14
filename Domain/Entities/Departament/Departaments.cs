using Domain.Entities.Company;
using Domain.Entities.Registered_Cars;

namespace Domain.Entities.Departament
{
    public class Departaments : BaseEntity
    {
        public Departaments()
        {
            this.AssignedVehicles = new HashSet<Vehicle>();
        }

        public string Name { get; set; }
        public int CompanyId { get; set; }
        public virtual Companies Company { get; set; }
        public virtual ICollection<Vehicle> AssignedVehicles { get; set; }
    }
}
