using Domain.Entities.Identity;
using Domain.Enums;

namespace Domain.Entities.Registered_Cars
{
    public class VehicleService : BaseEntity
    {
        public int? WorkShopId { get; set; }
        public virtual VehicleMaintenanceWorkshop? Workshop { get; set; }
        public int? ServiceUserId { get; set; }
        public virtual AppUser? ServiceUser { get; set; }
        public VehicleServiceType TypeService { get; set; }
        public VehicleServiceStatus Status { get; set; }
        public DateTime? NextService { get; set; }
        public int? NextServiceKM { get; set; }
        public int? InitialMileage { get; set; }
        public CurrentFuel? InitialFuel { get; set; }
        public int? FinalMileage { get; set; }
        public CurrentFuel? FinalFuel { get; set; }
        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public string? Comment { get; set; }
        public int? ExpenseId { get; set; }
        public virtual Expenses? Expense { get; set;}
    }
}
