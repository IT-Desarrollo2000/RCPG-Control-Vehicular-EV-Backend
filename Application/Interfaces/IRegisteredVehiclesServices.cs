using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRegisteredVehiclesServices
    {
        Task<GenericResponse<VehicleImage>> AddVehicleImage(VehicleImageRequest request, int vehicleId);
        Task<GenericResponse<VehiclesDto>> AddVehicles(VehicleRequest vehicleRequest);
        Task<GenericResponse<bool>> DeleteVehicleImage(int VehicleImageId);
        Task<GenericResponse<bool>> DeleteVehicles(int id);
        Task<GenericResponse<List<GetExpensesDto>>> GetExpenses(int VehicleId);
        Task<GenericResponse<List<GraphicsDto>>> GetServicesAndMaintenanceList(List<int> VehicleId);
        Task<GenericResponse<GraphicsDto>> GetServicesAndWorkshop(int VehicleId);
        Task<GenericResponse<VehiclesDto>> GetVehicleById(int id);
        Task<PagedList<Vehicle>> GetVehicles(VehicleFilter filter);
        Task<GenericResponse<PerformanceDto>> Performance(PerformanceRequest performanceRequest);
        Task<GenericResponse<List<PerformanceDto>>> PerformanceList(List<PerformanceRequest> performanceRequests);
        Task<GenericResponse<Vehicle>> PutVehicles(VehiclesUpdateRequest vehiclesUpdateRequest, int id);
    }
}
