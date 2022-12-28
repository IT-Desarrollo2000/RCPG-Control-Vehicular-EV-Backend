using Domain.Entities.Departament;

namespace Domain.Entities.Company
{
    public class Companies : BaseEntity
    {
        public string Name { get; set; }
        public string ReasonSocial { get; set; }
        public virtual ICollection<Departaments> Departaments { get; set; }
    }
}
