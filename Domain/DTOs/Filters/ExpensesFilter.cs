namespace Domain.DTOs.Filters
{
    public class ExpensesFilter
    {
        public DateTime? CreatedAfterDate { get; set; }
        public DateTime? CreatedBeforeDate { get; set; }
        public int? TypesOfExpensesId { get; set; }
        public decimal? Cost { get; set; }
        public bool? Invoiced { get; set; }
        public DateTime? ExpenseDate { get; set; }
        public int? VehicleId { get; set; }
        public int? VehicleMaintenanceWorkshopId { get; set; }
        public int? DepartmentId { get; set; }
        public string? ERPFolio { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
