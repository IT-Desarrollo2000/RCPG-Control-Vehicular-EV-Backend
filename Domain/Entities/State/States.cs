using Domain.Entities.Country;
using Domain.Entities.Municipality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.State
{
    public class States : BaseEntity
    {
        public string Name { get; set; }
        public int CountryId { get; set; }
        public virtual Countries Countries { get; set; }
        public virtual ICollection<Municipalities> Municipalities { get; set; }
    }
}
