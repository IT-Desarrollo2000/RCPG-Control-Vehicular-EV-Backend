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

            //VehicleService
            CreateMap<VehicleService, VehicleServiceRequest>().ReverseMap();
            CreateMap<VehicleServiceRequest, VehicleServiceDto>().ReverseMap();
            CreateMap<VehicleService, VehicleServiceDto>().ReverseMap();

            //Checklist
            CreateMap<Checklist, ChecklistDto>().ReverseMap();
            CreateMap<ChecklistDto, CreationChecklistDto>().ReverseMap();
            CreateMap<Checklist, CreationChecklistDto>().ReverseMap();
            CreateMap<Checklist, ReportUseTypeRequest.checklistdto>().ReverseMap();

            //Expenses
            CreateMap<Expenses, ExpensesDto>().ReverseMap();
            CreateMap<Expenses, ExpensesRequest>().ReverseMap();
            CreateMap<ExpensesRequest, ExpensesDto>().ReverseMap();
            CreateMap<Vehicle, GetExpensesDto>().ReverseMap();
            CreateMap<Expenses, GetExpensesDto>().ReverseMap();
            CreateMap<GetExpensesDtoList, Expenses>().ReverseMap();
            CreateMap<GetExpensesDtoList, Vehicle>().ReverseMap();
            CreateMap<Expenses, UnrelatedExpensesDto>().ReverseMap();

            //TypesOfExpenses
            CreateMap<TypesOfExpenses, TypesOfExpensesDto>().ReverseMap();
            CreateMap<TypesOfExpenses, TypesOfExpensesRequest>().ReverseMap();
            CreateMap<TypesOfExpensesDto, TypesOfExpensesRequest>().ReverseMap();
            CreateMap<GetTypesOfExpensesDto, TypesOfExpenses>().ReverseMap();

            //VehicleMaintenance
            CreateMap<VehicleMaintenance, VehicleMaintenanceRequest>().ReverseMap();
            CreateMap<VehicleMaintenanceRequest, VehicleMaintenanceDto>().ReverseMap();
            CreateMap<VehicleMaintenance, VehicleMaintenanceDto>().ReverseMap();

            //MaintenanceWorkshops
            CreateMap<VehicleMaintenanceWorkshop, MaintenanceWorkshopRequest>().ReverseMap();
            CreateMap<MaintenanceWorkshopRequest, MaintenanceWorkshopDto>().ReverseMap();
            CreateMap<VehicleMaintenanceWorkshop, MaintenanceWorkshopDto>().ReverseMap();
            CreateMap<VehicleMaintenanceWorkshop, GetVehicleMaintenanceWorkshopDto>().ReverseMap();

            //VehicleReport
            CreateMap<VehicleReport, VehicleReportRequest>().ReverseMap();
            CreateMap<VehicleReportRequest, VehicleReportDto>().ReverseMap();
            CreateMap<VehicleReport, VehicleReportDto>()
                .ForMember(x => x.AdminUserName, c => c.MapFrom(a => a.AdminUser.Email))
                .ForMember(x => x.MobileUserName, c => c.MapFrom(m => m.MobileUser.FullName))
                .ForMember(x => x.SolvedByAdminUserName, c => c.MapFrom(ad => ad.SolvedByAdminUser.Email));
            CreateMap<VehicleReport, VehicleReportSlimDto>()
                .ForMember(x => x.AdminUserName, c => c.MapFrom(a => a.AdminUser.Email))
                .ForMember(x => x.MobileUserName, c => c.MapFrom(m => m.MobileUser.FullName))
                .ForMember(x => x.SolvedByAdminUserName, c => c.MapFrom(ad => ad.SolvedByAdminUser.Email));
            CreateMap<VehicleReportImage, VehicleReportImageDto>();


            //Performance
            CreateMap<PerformanceRequest, PerformanceDto>().ReverseMap();

            //DestinationOfReportUse
            CreateMap<DestinationOfReportUse, DestinationOfReportUseRequest>().ReverseMap();
            CreateMap<DestinationOfReportUseRequest, DestinationOfReportUseDto>().ReverseMap();
            CreateMap<DestinationOfReportUse, DestinationOfReportUseDto>().ReverseMap();
            CreateMap<DestinationOfReportUse, UnrelatedDestinationOfReportUseDto>().ReverseMap();
            CreateMap<DestinationOfReportUse, destinolistdto>().ReverseMap();

            //VehicleReportUse
            CreateMap<VehicleReportUse, VehicleReportUseRequest>().ReverseMap();
            CreateMap<VehicleReportUseRequest, VehicleReportUseDto>().ReverseMap();
            CreateMap<VehicleReportUse, VehicleReportUseDto>()
                .ForMember(ur => ur.VehicleName, o => o.MapFrom(v => v.Vehicle.Name))
                .ForMember(ur => ur.ApprovedByAdminUserId, o => o.MapFrom(v => v.AppUserId))
                .ForMember(ur => ur.DriverUserId, o => o.MapFrom(v => v.UserProfileId))
                .ForMember(ur => ur.DriverName, o => o.MapFrom(v => v.UserProfile.FullName))
                .ForMember(ur => ur.ApprovedByAdminName, o => o.MapFrom(v => v.AppUser.Email));
            CreateMap<VehicleReportUse, VehicleReportUseVerificationRequest>().ReverseMap();
            CreateMap<VehicleReportUseDto, VehicleReportUseVerificationRequest>().ReverseMap();
            CreateMap<VehicleReportUseDto, ReportUseTypeRequest>().ReverseMap();
            CreateMap<VehicleReportUse, UnrelatedVehicleReportUseDto>().ReverseMap();
            CreateMap<VehicleReportUse, VehicleReportUseProceso>().ReverseMap();
            CreateMap<VehicleReportUse, VehicleReportUseFastTravel>().ReverseMap();
            CreateMap<VehicleReportUse, GetVehicleActiveDto>()
                .ForMember(ur => ur.VehicleId, o => o.MapFrom(v => v.Vehicle.Id))
                .ForMember(ur => ur.VehicleName, o => o.MapFrom(v => v.Vehicle.Name))
                .ForMember(ur => ur.DriverUserId, o => o.MapFrom(v => v.UserProfileId))
                .ForMember(ur => ur.DriverName, o => o.MapFrom(v => v.UserProfile.FullName))
                .ForMember(ur => ur.VehicleStatus, o => o.MapFrom(v => v.Vehicle.VehicleStatus))
                .ReverseMap();


            //Graphics
            CreateMap<GraphicsDto, Vehicle>().ReverseMap();
            CreateMap<GraphicsDto, VehicleMaintenance>().ReverseMap();
            CreateMap<GraphicsDto, VehicleService>().ReverseMap();

            //Policy
            CreateMap<Policy, PolicyDto>().ReverseMap();
            CreateMap<PolicyRequest, PolicyDto>().ReverseMap();
            CreateMap<Policy, PolicyRequest>().ReverseMap();

        }
    }
}
