using Domain.Entities.Registered_Cars;

namespace Domain.DTOs.Reponses
{
    public class ExpensesDto
    {
        public ExpensesDto()
        {
            Vehicles = new List<VehiclesDto>();
            PhotosOfSpending= new List<PhotosOfSpendingDto>();
            //ReportImagenes = new List<ReportImagenesExpense>();
        }
        public int Id { get; set; }
        public TypesOfExpensesDto TypesOfExpenses { get; set; }
        public decimal Cost { get; set; }
        public bool Invoiced { get; set; }
        public DateTime ExpenseDate { get; set; }
        public List<VehiclesDto> Vehicles { get; set; }
        public string MechanicalWorkshop { get; set; }
        public string ERPFolio { get; set; }
        public int? VehicleMaintenanceWorkshopId { get; set; }
        public MaintenanceWorkshopDto? VehicleMaintenanceWorkshop { get; set; }
        public List<PhotosOfSpendingDto> PhotosOfSpending { get; set; }
        public VehicleReportDto VehicleReport { get; set; }
        //public List<ReportImagenesExpense> ReportImagenes { get; set; }
        public int? VehicleMaintenanceId { get; set; }
        public int? VehicleServiceId { get; set; }
        public string? Comment { get; set; }

    }

   /* public class ReportImagenesExpense
    {
        public int VehicleReportId { get; set; }
        public string FilePath { get; set; }
        public string FileUrl { get; set; }
    }*/
}
