using Application.Interfaces;
using Domain.Entities.Company;
using Domain.Entities.Departament;
using Domain.Entities.Identity;
using Domain.Entities.Profiles;
using Domain.Entities.Registered_Cars;
using Domain.Entities.User_Approvals;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CVContext _context;
        private readonly IRepository<RefreshToken> _RefreshTokenRepo;
        private readonly IRepository<UserProfile> _UserProfileRepo;
        private readonly IRepository<UserApproval> _UserApprovalRepo;
        private readonly IRepository<Companies> _Companies;
        private readonly IRepository<Departaments> _Departaments;
        private readonly IRepository<Vehicle> _VehicleRepo;
        private readonly IRepository<VehicleService> _VehicleServiceRepo;
        private readonly IRepository<VehicleImage> _VehicleImageRepo;
        private readonly IRepository<Checklist> _ChecklistRepo;
        private readonly IRepository<Expenses> _ExpensesRepo;
        private readonly IRepository<TypesOfExpenses> _TypesOfExpensesRepo;
        private readonly IRepository<PhotosOfSpending> _PhotosOfSpendingRepo;
        private readonly IRepository<VehicleMaintenance> _VehicleMaintenanceRepo;
        private readonly IRepository<VehicleMaintenanceWorkshop> _MaintenanceWorkshopRepo;
        
        public UnitOfWork(CVContext context)
        {
            _context = context;
        }

        //DECLARAR REPOSITORIOS
        public IRepository<RefreshToken> RefreshTokenRepo => _RefreshTokenRepo ?? new BaseRepository<RefreshToken>(_context);
        public IRepository<UserProfile> UserProfileRepo => _UserProfileRepo ?? new BaseRepository<UserProfile>(_context);
        public IRepository<UserApproval> UserApprovalRepo => _UserApprovalRepo ?? new BaseRepository<UserApproval>(_context);
        public IRepository<Companies> Companies => _Companies ?? new BaseRepository<Companies>(_context);
        public IRepository<Departaments> Departaments => _Departaments ?? new BaseRepository<Departaments>(_context);
        public IRepository<Vehicle> VehicleRepo => _VehicleRepo ?? new BaseRepository<Vehicle>(_context);
        public IRepository<VehicleService> VehicleServiceRepo => _VehicleServiceRepo ?? new BaseRepository<VehicleService>(_context);
        public IRepository<VehicleImage> VehicleImageRepo => _VehicleImageRepo ?? new BaseRepository<VehicleImage>(_context);
        public IRepository<Checklist> ChecklistRepo => _ChecklistRepo ?? new BaseRepository<Checklist>(_context);
        public IRepository<Expenses> ExpensesRepo => _ExpensesRepo ?? new BaseRepository<Expenses>(_context);
        public IRepository<TypesOfExpenses> TypesOfExpensesRepo => _TypesOfExpensesRepo ?? new BaseRepository<TypesOfExpenses>(_context);
        public IRepository<PhotosOfSpending> PhotosOfSpendingRepo => _PhotosOfSpendingRepo ?? new BaseRepository<PhotosOfSpending>(_context);
        public IRepository<VehicleMaintenance> VehicleMaintenanceRepo => _VehicleMaintenanceRepo ?? new BaseRepository<VehicleMaintenance>(_context);
        public IRepository<VehicleMaintenanceWorkshop> MaintenanceWorkshopRepo => _MaintenanceWorkshopRepo ?? new BaseRepository<VehicleMaintenanceWorkshop>(_context); 
        
        //FUNCIONES DEL SERVICIO
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
