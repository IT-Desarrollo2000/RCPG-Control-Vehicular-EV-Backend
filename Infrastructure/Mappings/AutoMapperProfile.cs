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

            //VehicleService
            CreateMap<VehicleService, VehicleServiceRequest>().ReverseMap();
            CreateMap<VehicleServiceRequest, VehicleServiceDto>().ReverseMap();
            CreateMap<VehicleService, VehicleServiceDto>().ReverseMap();
            
            //Checklist
            CreateMap<Checklist, ChecklistDto>().ReverseMap();
            CreateMap<ChecklistDto, CreationChecklistDto>().ReverseMap();
            CreateMap<Checklist, CreationChecklistDto>().ReverseMap();

        }
    }
}
