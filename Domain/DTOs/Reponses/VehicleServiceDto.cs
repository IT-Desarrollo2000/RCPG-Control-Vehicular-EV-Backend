using Domain.Entities.Registered_Cars;
using Domain.Enums;

namespace Domain.DTOs.Reponses
{
    public class VehicleServiceDto
    {
        public int Id { get; set; }
        public int ServiceUserId { get; set; }
        public string ServiceUserName { get; set; }
        public int WorkshopId { get; set; }
        public VehicleServiceType TypeService { get; set; }
        public VehicleServiceStatus Status { get; set; }
        public DateTime NextService { get; set; }
        public int NextServiceKM { get; set; }
        public int InitialMileage { get; set; }
        public CurrentFuel InitialFuel { get; set; }
        public int FinalMileage { get; set; }
        public CurrentFuel FinalFuel { get; set; }
        public int VehicleId { get; set; }
        public VehiclesDto Vehicle { get; set; }
        public string? Comment { get; set; }
        public int? ExpenseId { get; set; }
        public ExpensesDto Expense { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
