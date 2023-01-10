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
    public interface IMaintenanceWorkshopService
    {
        Task<GenericResponse<MaintenanceWorkshopDto>> DeleteMaintenanceWorkshop(int Id);
        Task<PagedList<VehicleMaintenanceWorkshop>> GetMaintenanceWorkshopAll(MaintenanceWorkshopFilter filter);
        Task<GenericResponse<MaintenanceWorkshopDto>> GetMaintenanceWorkshopById(int Id);
        Task<GenericResponse<MaintenanceWorkshopDto>> PostMaintenanceWorkshop([FromBody] MaintenanceWorkshopRequest maintenanceWorkshopRequest);
        Task<GenericResponse<MaintenanceWorkshopDto>> PutMaintenanceWorkshop(int Id, [FromBody] MaintenanceWorkshopRequest maintenanceWorkshopRequest);
    }
}
