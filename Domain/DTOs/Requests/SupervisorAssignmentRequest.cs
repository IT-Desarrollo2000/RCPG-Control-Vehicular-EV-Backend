using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Requests
{
    public class SupervisorAssignmentRequest
    {
        public SupervisorAssignmentRequest()
        {
            DepartmentsToAssign = new List<int>();
        }

        [Required]
        public int AppUserId { get; set; }

        public List<int> DepartmentsToAssign { get; set; }
    }
}
