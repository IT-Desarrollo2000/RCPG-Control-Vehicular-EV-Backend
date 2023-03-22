namespace Domain.Entities.Registered_Cars
{
    public class Expenses : BaseEntity
    {
        public Expenses()
        {
            Vehicles = new HashSet<Vehicle>();
        }
        public int TypesOfExpensesId { get; set; }
        public virtual TypesOfExpenses TypesOfExpenses { get; set; }
        public decimal Cost { get; set; }
        public bool Invoiced { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string ERPFolio { get; set; }

        public int? VehicleReportId { get; set; }
        public int? VehicleMaintenanceWorkshopId { get; set; }
        public int? VehicleMaintenanceId { get; set; }
        public int? VehicleServiceId { get; set; }
        public string? Comment { get; set; }
        public virtual VehicleMaintenance? VehicleMaintenance { get; set; }
        public virtual VehicleService? VehicleService { get; set; }
        public virtual VehicleReport? VehicleReport { get; set; }
        public virtual VehicleMaintenanceWorkshop? VehicleMaintenanceWorkshop { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public virtual ICollection<PhotosOfSpending> PhotosOfSpending { get; set; }
    }
}
