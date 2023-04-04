using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Registered_Cars
{
    public class PhotosOfPolicy : BaseEntity
    {
        public string FilePath { get; set; }
        public string FileURL { get; set; }
        public int PolicyId { get; set; }
        public virtual Policy Policy { get; set; }
    }
}
