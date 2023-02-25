using System.ComponentModel.DataAnnotations;

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

        public List<int> DepartmentsToRemove { get; set; }
    }
}
