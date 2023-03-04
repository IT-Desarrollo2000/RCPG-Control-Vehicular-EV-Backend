namespace Domain.DTOs.Reponses
{
    public class UnrelatedExpensesDto
    {
        public int Id { get; set; }
        public int TypesOfExpensesId { get; set; }
        public string TypesOfExpensesName { get; set; }
        public decimal Cost { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string ERPFolio { get; set; }
        public int? VehicleMaintenanceWorkshopId { get; set; }
        public int? VehicleReportId { get; set; }
        public int invoiced { get; set; }
    }
}
