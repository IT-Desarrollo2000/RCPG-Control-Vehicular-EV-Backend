using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Registered_Cars
{
    public class PhotosOfSpending: BaseEntity
    {

        public string FilePath { get; set; }
        public string FileURL { get; set; }
        public int ExpensesId { get; set; }
        public virtual Expenses Expenses { get; set; }
    }
}
