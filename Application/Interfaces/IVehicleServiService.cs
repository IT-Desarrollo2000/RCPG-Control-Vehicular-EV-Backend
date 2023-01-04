using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
using Microsoft.AspNetCore.Mvc;

namespace Application.Interfaces
{
    public interface IVehicleServiService
    {
        Task<GenericResponse<VehicleServiceDto>> DeleteVehicleService(int Id);
        Task<PagedList<VehicleService>> GetVehicleServiceAll(VehicleServiceFilter filter);
        Task<GenericResponse<VehicleServiceDto>> GetVehicleServiceById(int Id);
        Task<GenericResponse<VehicleServiceDto>> PostVehicleService([FromBody] VehicleServiceRequest vehicleServiceRequest);
        Task<GenericResponse<VehicleServiceDto>> PutVehicleService(int Id, [FromBody] VehicleServiceRequest vehicleServiceRequest);
    }
}
