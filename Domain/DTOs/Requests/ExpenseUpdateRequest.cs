namespace Domain.DTOs.Requests
{
    public class ExpenseUpdateRequest
    {
        public decimal? Cost { get; set; }
        public bool? Invoiced { get; set; }
        public int? VehicleId { get; set; }
        public int? TypesOfExpensesId { get; set; }
        public int? VehicleMaintenanceWorkshopId { get; set; }
        public DateTime? ExpenseDate { get; set; }
        public string? ERPFolio { get; set; }
        public int? VehicleReportId { get; set; }
        public string? Comment { get; set; }
    }
}
