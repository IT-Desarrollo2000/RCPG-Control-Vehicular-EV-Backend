using Domain.Entities.Profiles;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class LicenseByDepartmentRequest
    {
        public LicenseByDepartmentRequest()
        {
            DepartmentId = new List<int>();
        }
        public List<int> DepartmentId { get; set; }
        public LicenceExpStopLight StopLight { get; set; }
    }
}
