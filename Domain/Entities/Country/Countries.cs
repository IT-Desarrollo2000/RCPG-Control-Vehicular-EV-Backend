using Domain.Entities.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Country
{
    public class Countries : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<States> States { get; set; }

    }
}
