using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class SupervisorRemovalRequest
    {
        public SupervisorRemovalRequest()
        {
            DepartmentsToRemove = new List<int>();
        }

        [Required]
        public int AppUserId { get; set; }

        public List<int> DepartmentsToRemove{ get; set; }
    }
}
