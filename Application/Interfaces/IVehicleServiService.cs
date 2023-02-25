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
        Task<PagedList<VehicleServiceDto>> GetVehicleServiceAll(VehicleServiceFilter filter);
        Task<GenericResponse<VehicleServiceDto>> GetVehicleServiceById(int Id);
        Task<GenericResponse<VehicleServiceDto>> MarkAsCanceled(VehicleServiceCanceledRequest request);
        Task<GenericResponse<VehicleServiceDto>> MarkAsResolved(VehicleServiceFinishRequest request);
        Task<GenericResponse<VehicleServiceDto>> PostVehicleService(VehicleServiceRequest vehicleServiceRequest);
        Task<GenericResponse<VehicleServiceDto>> PutVehicleService(VehicleServiceUpdateRequest vehicleServiceRequest);
    }
}
