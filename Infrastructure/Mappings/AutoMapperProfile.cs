using AutoMapper;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Company;
using Domain.Entities.Departament;
using Domain.Entities.Identity;
using Domain.Entities.Profiles;
using Domain.Entities.Registered_Cars;
using Domain.Entities.User_Approvals;

namespace Infrastructure.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            ///User profile Mappings
            CreateMap<UserProfileRequest, UserProfile>().ReverseMap();
            CreateMap<UserProfile, ProfileDto>().ReverseMap();

            //UserMapping
            CreateMap<WebAdmUserRegistrationRequest, AppUser>();
            CreateMap<AppUser, WebAdmUserRegistrationRequest>();
            CreateMap<AppUserRegistrationRequest, AppUser>();
            CreateMap<AppUser, AppUserRegistrationRequest>();

            //Approval Mapping
            CreateMap<UserProfile, UserApproval>();
            CreateMap<UserApproval, UserProfile>().ForMember(a => a.Id, opt => opt.Ignore());
            CreateMap<AppUserRegistrationRequest, ApprovalCreationRequest>();
            CreateMap<ApprovalCreationRequest, UserApproval>();
            CreateMap<UserProfile, UnrelatedUserProfileDto>().ReverseMap();

            //Company
            CreateMap<Companies, CompanyRequest>().ReverseMap();
            CreateMap<CompanyRequest, CompanyDto>().ReverseMap();
            CreateMap<Companies, CompanyDto>().ReverseMap();
            CreateMap<Companies, UnrelatedCompanyDto>().ReverseMap();

            //Departament
            CreateMap<Departaments, DepartamentRequest>().ReverseMap();
            CreateMap<DepartamentRequest, DepartamentDto>().ReverseMap();
            CreateMap<Departaments, DepartamentDto>().ReverseMap();
            CreateMap<Departaments, UnrelatedDepartamentDto>().ReverseMap();



            //Vehicle
            CreateMap<Vehicle, VehicleRequest>().ReverseMap();
            CreateMap<Vehicle, VehiclesDto>().ReverseMap();
            CreateMap<VehicleRequest, VehiclesDto>().ReverseMap();
            CreateMap<Vehicle, UnrelatedVehiclesDto>().ReverseMap();
            CreateMap<VehicleImage, VehicleImageDto>().ReverseMap();
            CreateMap<Vehicle, MaintenanceSpotlightDto>()
                .ForMember(s => s.VehicleId, o => o.MapFrom(s => s.Id))
                .ForMember(s => s.VehicleName, o => o.MapFrom(s => s.Name));


            //VehicleService
            CreateMap<VehicleService, VehicleServiceRequest>().ReverseMap();
            CreateMap<VehicleServiceRequest, VehicleServiceDto>().ReverseMap();
            CreateMap<VehicleService, VehicleServiceDto>()
                .ForMember(s => s.ServiceUserName, o => o.MapFrom(s => s.ServiceUser.UserName))
                .ForMember(s => s.ServiceUserId, o => o.MapFrom(s => s.ServiceUserId));
            CreateMap<VehicleService, MaintenanceSpotlightDto>()
                .ForMember(m => m.VehicleName, o => o.MapFrom(m => m.Vehicle.Name))
                .ForMember(m => m.Type, o => o.MapFrom(m => m.TypeService));
            CreateMap<VehicleServiceUpdateRequest, VehicleService>();
            CreateMap<VehicleServiceFinishRequest, VehicleService>();
            CreateMap<VehicleServiceCanceledRequest, VehicleService>();
            CreateMap<VehicleService, MaintenanceSpotlightDto>()
                .ForMember(s => s.VehicleName, o => o.MapFrom(s => s.Vehicle.Name))
                .ForMember(s => s.Type, o => o.MapFrom(s => s.TypeService))
                .ForMember(s => s.ServicePeriodMonths, o => o.MapFrom(s => s.Vehicle.ServicePeriodMonths))
                .ForMember(s => s.ServicePeriodKM, o => o.MapFrom(s => s.Vehicle.ServicePeriodKM));

            //Checklist
            CreateMap<Checklist, ChecklistDto>().ReverseMap();
            CreateMap<ChecklistDto, CreationChecklistDto>().ReverseMap();
            CreateMap<Checklist, CreationChecklistDto>().ReverseMap();

            //Expenses
            CreateMap<Expenses, ExpensesDto>().ReverseMap();
            CreateMap<Expenses, ExpensesRequest>().ReverseMap();
            CreateMap<ExpensesRequest, ExpensesDto>().ReverseMap();
            CreateMap<Vehicle, GetExpensesDto>().ReverseMap();
            CreateMap<Expenses, GetExpensesDto>().ReverseMap();
            CreateMap<GetExpensesDtoList, Expenses>().ReverseMap();
            CreateMap<GetExpensesDtoList, Vehicle>().ReverseMap();
            CreateMap<Expenses, UnrelatedExpensesDto>()
                .ForMember(e => e.TypesOfExpensesName, o => o.MapFrom(e => e.TypesOfExpenses.Name));
            CreateMap<PhotosOfSpendingDto, PhotosOfSpending>().ReverseMap();

            //TypesOfExpenses
            CreateMap<TypesOfExpenses, TypesOfExpensesDto>().ReverseMap();
            CreateMap<TypesOfExpenses, TypesOfExpensesRequest>().ReverseMap();
            CreateMap<TypesOfExpensesDto, TypesOfExpensesRequest>().ReverseMap();
            CreateMap<GetTypesOfExpensesDto, TypesOfExpenses>().ReverseMap();

            //VehicleMaintenance
            CreateMap<VehicleMaintenance, VehicleMaintenanceRequest>().ReverseMap();
            CreateMap<VehicleMaintenanceRequest, VehicleMaintenanceDto>().ReverseMap();
            CreateMap<VehicleMaintenance, VehicleMaintenanceDto>()
                .ForMember(m => m.ApprovedByAdminName, o => o.MapFrom(m => m.ApprovedByUser.UserName));

            //MaintenanceWorkshops
            CreateMap<VehicleMaintenanceWorkshop, MaintenanceWorkshopRequest>().ReverseMap();
            CreateMap<MaintenanceWorkshopRequest, MaintenanceWorkshopDto>().ReverseMap();
            CreateMap<VehicleMaintenanceWorkshop, MaintenanceWorkshopDto>().ReverseMap();
            CreateMap<VehicleMaintenanceWorkshop, GetVehicleMaintenanceWorkshopDto>().ReverseMap();
            CreateMap<MaintenanceWorkShopSlimDto, VehicleMaintenanceWorkshop>().ReverseMap();

            //VehicleReport
            CreateMap<VehicleReport, VehicleReportRequest>().ReverseMap();
            CreateMap<VehicleReportRequest, VehicleReportDto>().ReverseMap();
            CreateMap<VehicleReport, VehicleReportDto>()
                .ForMember(x => x.AdminUserName, c => c.MapFrom(a => a.AdminUser.UserName))
                .ForMember(x => x.MobileUserName, c => c.MapFrom(m => m.MobileUser.FullName))
                .ForMember(x => x.SolvedByAdminUserName, c => c.MapFrom(ad => ad.SolvedByAdminUser.UserName));
            CreateMap<VehicleReport, VehicleReportSlimDto>()
                .ForMember(x => x.AdminUserName, c => c.MapFrom(a => a.AdminUser.UserName))
                .ForMember(x => x.MobileUserName, c => c.MapFrom(m => m.MobileUser.FullName))
                .ForMember(x => x.SolvedByAdminUserName, c => c.MapFrom(ad => ad.SolvedByAdminUser.UserName));
            CreateMap<VehicleReportImage, VehicleReportImageDto>();
            CreateMap<VehicleReport, GraphicsPerfomanceDto>()
                .ForMember(x => x.VehicleId, c => c.MapFrom(a => a.VehicleId))
                .ForMember(X => X.VehicleName, c => c.MapFrom(a => a.Vehicle.Name))
                .ForMember(x => x.CurrentKm, c => c.MapFrom(a => a.VehicleReportUses.InitialMileage))
                .ForMember(x => x.LastKm, c => c.MapFrom(a => a.VehicleReportUses.FinalMileage))
                .ForMember(x => x.GasolineLoadAmount, c => c.MapFrom(a => a.GasolineLoadAmount)).ReverseMap();
            CreateMap<VehicleReport, TotalPerfomanceDto>()
                .ForMember(X => X.VehicleName, c => c.MapFrom(a => a.Vehicle.Name))
                .ForMember(x => x.DesiredPerfomance, c => c.MapFrom(a => a.Vehicle.DesiredPerformance))
                .ReverseMap();



            //Performance
            CreateMap<PerformanceRequest, PerformanceDto>().ReverseMap();

            //DestinationOfReportUse
            CreateMap<DestinationOfReportUse, DestinationOfReportUseRequest>().ReverseMap();
            CreateMap<DestinationOfReportUseRequest, DestinationOfReportUseDto>().ReverseMap();
            CreateMap<DestinationOfReportUse, DestinationOfReportUseDto>().ReverseMap();
            CreateMap<DestinationOfReportUse, UnrelatedDestinationOfReportUseDto>().ReverseMap();
            CreateMap<DestinationRequest, DestinationOfReportUse>().ReverseMap();

            //VehicleReportUse
            CreateMap<VehicleReportUse, VehicleReportUseDto>()
                .ForMember(ur => ur.VehicleName, o => o.MapFrom(v => v.Vehicle.Name))
                .ForMember(ur => ur.ApprovedByAdminUserId, o => o.MapFrom(v => v.AppUserId))
                .ForMember(ur => ur.DriverUserId, o => o.MapFrom(v => v.UserProfileId))
                .ForMember(ur => ur.DriverName, o => o.MapFrom(v => v.UserProfile.FullName))
                .ForMember(ur => ur.ApprovedByAdminName, o => o.MapFrom(v => v.AppUser.UserName));
            CreateMap<VehicleReportUse, VehicleReportUseVerificationRequest>().ReverseMap();
            CreateMap<VehicleReportUseDto, VehicleReportUseVerificationRequest>().ReverseMap();
            CreateMap<VehicleReportUse, UnrelatedVehicleReportUseDto>().ReverseMap();
            CreateMap<VehicleReportUse, VehicleReportUseProceso>().ReverseMap();
            CreateMap<UseReportFastTravelRequest, VehicleReportUse>().ReverseMap();
            CreateMap<VehicleReportUse, GetVehicleActiveDto>()
                .ForMember(ur => ur.VehicleId, o => o.MapFrom(v => v.Vehicle.Id))
                .ForMember(ur => ur.VehicleName, o => o.MapFrom(v => v.Vehicle.Name))
                .ForMember(ur => ur.DriverUserId, o => o.MapFrom(v => v.UserProfileId))
                .ForMember(ur => ur.DriverName, o => o.MapFrom(v => v.UserProfile.FullName))
                .ForMember(ur => ur.VehicleStatus, o => o.MapFrom(v => v.Vehicle.VehicleStatus))
                .ReverseMap();
            CreateMap<UseReportAdminRequest, VehicleReportUse>();
            CreateMap<VehicleReportUse, GetUserForTravelDto>()
                .ForMember(ur => ur.VehicleName, o => o.MapFrom(v => v.Vehicle.Name))
                .ForMember(ur => ur.UserDriverId, o => o.MapFrom(v => v.UserProfile.Id))
                .ForMember(ur => ur.UserName, o => o.MapFrom(v => v.UserProfile.FullName)).ReverseMap();
            CreateMap<VehicleReportUse, VehicleUseReportsSlimDto>();

            //Graphics
            CreateMap<GraphicsDto, Vehicle>().ReverseMap();
            CreateMap<GraphicsDto, VehicleMaintenance>().ReverseMap();
            CreateMap<GraphicsDto, VehicleService>().ReverseMap();

            //Policy
            CreateMap<Policy, PolicyDto>().ReverseMap();
            CreateMap<PolicyRequest, PolicyDto>().ReverseMap();
            CreateMap<Policy, PolicyRequest>().ReverseMap();
            CreateMap<Policy, ShortPolicyDto>().ReverseMap();
            CreateMap<Policy, PolicyExpiredDto>()
                .ForMember(p => p.VehicleName, o => o.MapFrom(p => p.Vehicle.Name))
                .ForMember(p => p.VehicleId, o => o.MapFrom(p => p.Vehicle.Id))
                .ForMember(p => p.PolicyId, o => o.MapFrom(p => p.Id))
                .ForMember(p => p.PolicyNumber, o => o.MapFrom(p => p.PolicyNumber))
                .ForMember(p => p.PolicyExpirationDate, o => o.MapFrom(p => p.ExpirationDate));

            //Licence
            CreateMap<UserProfile, LicenceExpiredDto>()
                .ForMember(l => l.UserProfileId, o => o.MapFrom(l => l.Id))
                .ForMember(l => l.UserFullName, o => o.MapFrom(l => l.FullName))
                .ForMember(l => l.LicenceExpirationDate, o => o.MapFrom(l => l.LicenceExpirationDate))
                .ForMember(l => l.LicenceType, o => o.MapFrom(l => l.LicenceType));

            //MaintenanceProgress
            CreateMap<MaintenanceProgress, MaintenanceProgressDto>()
                .ForMember(m => m.AdminUserName, o => o.MapFrom(m => m.AdminUser.FullName))
                .ForMember(m => m.MobileUserName, o => o.MapFrom(m => m.MobileUser.FullName));
            CreateMap<MaintenanceProgressRequest, MaintenanceProgress>();
        }
    }
}
