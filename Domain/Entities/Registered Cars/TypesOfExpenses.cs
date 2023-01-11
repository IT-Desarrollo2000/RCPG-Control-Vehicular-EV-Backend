using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Registered_Cars
{
    public class TypesOfExpenses: BaseEntity
    {
       
        public string Name { get; set; }
        public virtual ICollection<Expenses> Expenses { get; set; }

    }
}
