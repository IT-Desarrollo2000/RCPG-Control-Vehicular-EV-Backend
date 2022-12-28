using Domain.Entities.Company;

namespace Domain.Entities.Departament
{
    public class Departaments : BaseEntity
    {
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public virtual Companies Company { get; set; }
    }
}
