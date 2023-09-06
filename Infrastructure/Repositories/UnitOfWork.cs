using Application.Interfaces;
using Domain.Entities.Company;
using Domain.Entities.Country;
using Domain.Entities.Departament;
using Domain.Entities.Identity;
using Domain.Entities.Municipality;
using Domain.Entities.Profiles;
using Domain.Entities.Propietary;
using Domain.Entities.Registered_Cars;
using Domain.Entities.State;
using Domain.Entities.User_Approvals;
using Infrastructure.Persistence;

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
        private readonly IRepository<Invoices> _InvoicesRepo;
        private readonly IRepository<PhotosOfSpending> _PhotosOfSpendingRepo;
        private readonly IRepository<VehicleMaintenance> _VehicleMaintenanceRepo;
        private readonly IRepository<VehicleMaintenanceWorkshop> _MaintenanceWorkshopRepo;
        private readonly IRepository<VehicleReport> _VehicleReportRepo;
        private readonly IRepository<VehicleReportImage> _VehicleReportImageRepo;
        private readonly IRepository<VehicleReportUse> _VehicleReportUseRepo;
        private readonly IRepository<DestinationOfReportUse> _DestinationOfReportUseRepo;
        private readonly IRepository<Policy> _PolicyRepo;
        private readonly IRepository<MaintenanceProgress> _MaintenanceProgressRepo;
        private readonly IRepository<MaintenanceProgressImages> _MaintenanceProgressImagesRepo;
        private readonly IRepository<PhotosOfPolicy> _PhotosOfPolicyRepo;
        private readonly IRepository<PhotosOfCirculationCard> _PhotosOfCirculationCardRepo;
        private readonly IRepository<Propietary> _propietaryRepo;
        private readonly IRepository<AdditionalInformation> _AdditionalInformationRepo;
        private readonly IRepository<Countries> _CountriesRepo;
        private readonly IRepository<States> _StatesRepo;
        private readonly IRepository<Municipalities> _MunicipalitiesRepo; 

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
        public IRepository<Invoices> InvoicesRepo => _InvoicesRepo ?? new BaseRepository<Invoices>(_context);
        public IRepository<PhotosOfSpending> PhotosOfSpendingRepo => _PhotosOfSpendingRepo ?? new BaseRepository<PhotosOfSpending>(_context);
        public IRepository<VehicleMaintenance> VehicleMaintenanceRepo => _VehicleMaintenanceRepo ?? new BaseRepository<VehicleMaintenance>(_context);
        public IRepository<VehicleMaintenanceWorkshop> MaintenanceWorkshopRepo => _MaintenanceWorkshopRepo ?? new BaseRepository<VehicleMaintenanceWorkshop>(_context);
        public IRepository<VehicleReport> VehicleReportRepo => _VehicleReportRepo ?? new BaseRepository<VehicleReport>(_context);
        public IRepository<VehicleReportImage> VehicleReportImage => _VehicleReportImageRepo ?? new BaseRepository<VehicleReportImage>(_context);
        public IRepository<VehicleReportUse> VehicleReportUseRepo => _VehicleReportUseRepo ?? new BaseRepository<VehicleReportUse>(_context);
        public IRepository<DestinationOfReportUse> DestinationOfReportUseRepo => _DestinationOfReportUseRepo ?? new BaseRepository<DestinationOfReportUse>(_context);
        public IRepository<Policy> PolicyRepo => _PolicyRepo ?? new BaseRepository<Policy>(_context);
        public IRepository<MaintenanceProgress> MaintenanceProgressRepo => _MaintenanceProgressRepo ?? new BaseRepository<MaintenanceProgress>(_context);
        public IRepository<MaintenanceProgressImages> MaintenanceProgressImageRepot => _MaintenanceProgressImagesRepo ?? new BaseRepository<MaintenanceProgressImages>(_context);
        public IRepository<PhotosOfPolicy> PhotosOfPolicyRepo => _PhotosOfPolicyRepo ?? new BaseRepository<PhotosOfPolicy>(_context);
        public IRepository<PhotosOfCirculationCard> PhotosOfCirculationCardRepo => _PhotosOfCirculationCardRepo ?? new BaseRepository<PhotosOfCirculationCard>(_context);
        public IRepository<Propietary> PropietaryRepo => _propietaryRepo ?? new BaseRepository<Propietary>(_context);
        public IRepository<AdditionalInformation> AdditionalInformatioRepo => _AdditionalInformationRepo ?? new BaseRepository<AdditionalInformation>(_context);
        public IRepository<Countries> CountriesRepo => _CountriesRepo ?? new BaseRepository<Countries>(_context);
        public IRepository<States> StatesRepo => _StatesRepo ?? new BaseRepository<States>(_context);
        public IRepository<Municipalities> MunicipalitiesRepo => _MunicipalitiesRepo ?? new BaseRepository<Municipalities>(_context);

        //FUNCIONES DEL SERVICIO
        public void Dispose()
        {
            if (_context != null)
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
