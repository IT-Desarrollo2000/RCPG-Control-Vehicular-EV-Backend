using AutoMapper;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Company;
using Domain.Entities.Departament;
using Domain.Entities.Identity;
using Domain.Entities.Profiles;
using Domain.Entities.Registered_Cars;
using Domain.Entities.User_Approvals;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;

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
            CreateMap<UserApproval, UserProfile>().ReverseMap();
            CreateMap<AppUserRegistrationRequest, ApprovalCreationRequest>();
            CreateMap<ApprovalCreationRequest, UserApproval>();
            
            //Company
            CreateMap<Companies, CompanyRequest>();
            CreateMap<CompanyRequest, Companies>();
            CreateMap<CompanyRequest, CompanyDto>().ReverseMap();
            CreateMap<Companies, CompanyDto>().ReverseMap();

            //Departament
            CreateMap<Departaments, DepartamentRequest>();
            CreateMap<DepartamentRequest, Departaments>();
            CreateMap<DepartamentRequest, DepartamentDto>().ReverseMap();
            CreateMap<Departaments, DepartamentDto>().ReverseMap();

            //Vehicle
            CreateMap<Vehicle, VehicleRequest>().ReverseMap();
            CreateMap<Vehicle, VehiclesDto>().ReverseMap();
            CreateMap<VehicleRequest, VehiclesDto>().ReverseMap();

            //VehicleService
            CreateMap<VehicleService, VehicleServiceRequest>().ReverseMap();
            CreateMap<VehicleServiceRequest, VehicleServiceDto>().ReverseMap();
            CreateMap<VehicleService, VehicleServiceDto>().ReverseMap();
            
            //Checklist
            CreateMap<Checklist, ChecklistDto>().ReverseMap();
            CreateMap<ChecklistDto, CreationChecklistDto>().ReverseMap();
            CreateMap<Checklist, CreationChecklistDto>().ReverseMap();
            
            //Expenses
            CreateMap<Expenses, ExpensesDto>().ReverseMap();
            CreateMap<Expenses, ExpensesRequest>().ReverseMap();
            CreateMap<ExpensesRequest, ExpensesDto>().ReverseMap();

            //TypesOfExpenses
            CreateMap<TypesOfExpenses, TypesOfExpensesDto>().ReverseMap();
            CreateMap<TypesOfExpenses, TypesOfExpensesRequest>().ReverseMap();
            CreateMap<TypesOfExpensesDto, TypesOfExpensesRequest>().ReverseMap();
            
            //VehicleMaintenance
            CreateMap<VehicleMaintenance, VehicleMaintenanceRequest>().ReverseMap();
            CreateMap<VehicleMaintenanceRequest, VehicleMaintenanceDto>().ReverseMap();
            CreateMap<VehicleMaintenance, VehicleMaintenanceDto>().ReverseMap();

            //MaintenanceWorkshops
            CreateMap<VehicleMaintenanceWorkshop, MaintenanceWorkshopRequest>().ReverseMap();
            CreateMap<MaintenanceWorkshopRequest, MaintenanceWorkshopDto>().ReverseMap();
            CreateMap<VehicleMaintenanceWorkshop, MaintenanceWorkshopDto>().ReverseMap();

            //Performance
            CreateMap<PerformanceRequest, PerformanceDto>().ReverseMap();

        }
    }
}
