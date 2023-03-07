using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
using Microsoft.AspNetCore.Mvc;

namespace Application.Interfaces
{
    public interface IVehicleMaintenanceService
    {
        Task<GenericResponse<MaintenanceProgressDto>> AddProgress(MaintenanceProgressRequest request);
        Task<GenericResponse<VehicleMaintenanceDto>> CancelMaintenance(CancelMaintenanceRequest request);
        Task<GenericResponse<VehicleMaintenanceDto>> DeleteVehicleManintenance(int Id);
        Task<GenericResponse<VehicleMaintenanceDto>> FinalizeMaintenance(FinalizeMaintenanceRequest request);
        Task<PagedList<VehicleMaintenanceDto>> GetVehicleMaintenanceAll(VehicleMaintenanceFilter filter);
        Task<GenericResponse<VehicleMaintenanceDto>> GetVehicleMaintenanceById(int Id);
        Task<GenericResponse<VehicleMaintenanceDto>> InitiateMaintenance(VehicleMaintenanceRequest request);
        Task<GenericResponse<VehicleMaintenanceDto>> UpdateMaintenance(MaintenanceUpdateRequest request);
    }
}
