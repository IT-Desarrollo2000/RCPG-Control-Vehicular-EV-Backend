using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IVehicleMaintenanceService
    {
        Task<GenericResponse<VehicleMaintenanceDto>> DeleteVehicleManintenance(int Id);
        Task<PagedList<VehicleMaintenance>> GetVehicleMaintenanceAll(VehicleMaintenanceFilter filter);
        Task<GenericResponse<VehicleMaintenanceDto>> GetVehicleMaintenanceById(int Id);
        Task<GenericResponse<VehicleMaintenanceDto>> PostVehicleMaintenance([FromBody] VehicleMaintenanceRequest vehicleMaintenanceRequest);
        Task<GenericResponse<VehicleMaintenanceDto>> PutVehicleMaintenance(int Id, [FromBody] VehicleMaintenanceRequest vehicleMaintenanceRequest);
    }
}
