using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Requests
{
    public class ApprovalManagementRequest
    {
        [Required]
        public int ApprovalId { get; set; }
        [Required]
        public bool IsApproved { get; set; }
        public bool? CanDriveInHighway { get; set; }
        [Required]
        public string Comment { get; set; }
        public int? DepartmentId { get; set; }
    }
}
