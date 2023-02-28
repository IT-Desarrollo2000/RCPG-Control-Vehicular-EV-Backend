using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IToolsServices
    {
        Task<GenericResponse<List<LicenceExpiredDto>>> GetLicencesExpirations(LicenceExpStopLight request);
        Task<GenericResponse<List<MaintenanceSpotlightDto>>> GetMaintenanceSpotlight();
        Task<GenericResponse<List<PolicyExpiredDto>>> GetPoliciesExpiration(LicenceExpStopLight request);
        Task<GenericResponse<List<GetVehicleActiveDto>>> GetAllVehiclesActive();
        Task<GenericResponse<TotalPerfomanceDto>> GetTotalPerfomance(int VehicleId);
        Task<GenericResponse<List<GraphicsPerfomanceDto>>> GetAllPerfomance(int VehicleId);
        Task<GenericResponse<List<ListTotalPerfomanceDto>>> GetListTotalPerfomance(int VehicleId);
    }
}
