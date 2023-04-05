namespace Domain.DTOs.Reponses
{
    public class GetExpensesDto
    {
        public int Id { get; set; }
        public GetTypesOfExpensesDto TypesOfExpenses { get; set; }
        public decimal Cost { get; set; }
        public DateTime ExpenseDate { get; set; }
        public GetVehicleMaintenanceWorkshopDto VehicleMaintenanceWorkshop { get; set; }
        public string ERPFolio { get; set; }
        public int? DepartmentId { get; set; }
        //public List<PhotosOfSpending> PhotosOfSpending { get; set; }
    }
}
