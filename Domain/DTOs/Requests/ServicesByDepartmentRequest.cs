using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class ServicesByDepartmentRequest
    {
        public ServicesByDepartmentRequest()
        {
            DepartmentId = new List<int>();
        }
        public List<int> DepartmentId { get; set; }
    }
}
