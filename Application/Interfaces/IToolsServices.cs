using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IToolsServices
    {
        Task<GenericResponse<List<LicenceExpiredDto>>> GetLicencesExpirations(LicenseByDepartmentRequest request);
        Task<GenericResponse<List<MaintenanceSpotlightDto>>> GetMaintenanceSpotlight(ServicesByDepartmentRequest request);
        Task<GenericResponse<List<PolicyExpiredDto>>> GetPoliciesExpiration(LicenseByDepartmentRequest request);
        Task<GenericResponse<List<GetVehicleActiveDto>>> GetAllVehiclesActive(ServicesByDepartmentRequest request);
        
        
        Task<GenericResponse<List<TotalPerfomanceDto>>> GetTotalPerfomance(int VehicleId, AssignedDepartament assignedDepartament);
        Task<GenericResponse<List<GraphicsPerfomanceDto>>> GetAllPerfomance(int VehicleId, AssignedDepartament assignedDepartament);
        Task<GenericResponse<List<GetUserForTravelDto>>> GetUserForTravel(AssignedDepartament assignedDepartament);
        Task<GenericResponse<PerformanceReviewDto>> GetListTotalPerfomance(ListTotalPerfomanceDto listTotalPerfomanceDto, AssignedDepartament assignedDepartament);
        Task<GenericResponse<List<GetServicesMaintenance>>> GetServiceMaintenance(AssignedDepartament assignedDepartament);
        Task<GenericResponse<UserInTravelDto>> IsUserInTravel(int userProfileId);
    }
}
