using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Registered_Cars
{
    public class MaintenanceProgressImages : BaseEntity
    {
        public string FilePath { get; set; }
        public string FileURL { get; set; }
        public int ProgressId { get; set; }
        public virtual MaintenanceProgress Progress { get; set; }
    }
}
