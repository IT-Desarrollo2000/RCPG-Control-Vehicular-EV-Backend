namespace Domain.DTOs.Requests
{
    public class ApprovalManagementRequest
    {
        public int ApprovalId { get; set; }
        public bool IsApproved { get; set; }
        public string Comment { get; set; }
        public int DepartmentId { get; set; }
    }
}
