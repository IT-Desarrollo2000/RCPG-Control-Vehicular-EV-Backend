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
    public interface IVehicleReportUseService
    {
        Task<GenericResponse<VehicleReportUseDto>> DeleteVehicleReportUse(int Id);
        Task<PagedList<VehicleReportUse>> GetVehicleReportUseAll(VehicleReportUseFilter filter);
        Task<GenericResponse<VehicleReportUseDto>> GetVehicleReporUseById(int Id);
        Task<GenericResponse<VehicleReportUseDto>> PostVehicleReporUse([FromBody] VehicleReportUseRequest vehicleReportUseRequest);
        Task<GenericResponse<VehicleReportUseDto>> PutVehicleReportUse(int Id, [FromBody] VehicleReportUseRequest vehicleReportUseRequest);
    }
}
