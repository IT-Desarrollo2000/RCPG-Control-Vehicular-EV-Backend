using Domain.Entities.Company;
using Domain.Entities.Departament;
using Domain.Entities.Identity;
using Domain.Entities.Profiles;
using Domain.Entities.Registered_Cars;
using Domain.Entities.User_Approvals;

namespace Application.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<RefreshToken> RefreshTokenRepo { get; }
        IRepository<UserProfile> UserProfileRepo { get; }
        IRepository<UserApproval> UserApprovalRepo { get; }
        IRepository<Companies> Companies { get; }
        IRepository<Departaments> Departaments { get; }
        IRepository<Vehicle> VehicleRepo { get; }
        IRepository<VehicleService> VehicleServiceRepo { get; }
        IRepository<Checklist> ChecklistRepo { get; }
        IRepository<Expenses> ExpensesRepo { get; }
        IRepository<TypesOfExpenses> TypesOfExpensesRepo { get; }
        IRepository<VehicleMaintenance> VehicleMaintenanceRepo { get; }
        IRepository<VehicleMaintenanceWorkshop> MaintenanceWorkshopRepo { get; }
        IRepository<VehicleImage> VehicleImageRepo { get; }
        IRepository<PhotosOfSpending> PhotosOfSpendingRepo { get; }
        IRepository<VehicleReport> VehicleReportRepo { get; }
        IRepository<DestinationOfReportUse> DestinationOfReportUseRepo { get; }
        IRepository<VehicleReportUse> VehicleReportUseRepo { get; }
        IRepository<VehicleReportImage> VehicleReportImage { get; }
        IRepository<Policy> PolicyRepo { get; }

        void Dispose();
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
