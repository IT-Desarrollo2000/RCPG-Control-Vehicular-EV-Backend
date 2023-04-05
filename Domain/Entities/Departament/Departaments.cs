using Domain.Entities.Company;
using Domain.Entities.Identity;
using Domain.Entities.Registered_Cars;

namespace Domain.Entities.Departament
{
    public class Departaments : BaseEntity
    {
        public Departaments()
        {
            AssignedVehicles = new HashSet<Vehicle>();
            Supervisors = new HashSet<AppUser>();
            Expenses = new HashSet<Expenses>();
        }

        public string Name { get; set; }
        public int CompanyId { get; set; }
        public virtual Companies Company { get; set; }
        public virtual ICollection<Vehicle> AssignedVehicles { get; set; }
        public virtual ICollection<AppUser> Supervisors { get; set; }
        public virtual ICollection<Expenses> Expenses { get; set; }
    }
}
