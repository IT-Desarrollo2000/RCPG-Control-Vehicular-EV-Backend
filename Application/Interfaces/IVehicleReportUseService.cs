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
        Task<GenericResponse<bool>> DeleteVehicleReportUse(int Id);
        Task<GenericResponse<List<VehicleReportUseDto>>> GetUseReportByDepartment(int departmentId);
        Task<GenericResponse<VehicleReportUseDto>> GetUseReportById(int Id);
        Task<PagedList<VehicleReportUseDto>> GetUseReports(VehicleReportUseFilter filter);
        Task<PagedList<VehicleUseReportsSlimDto>> GetUseReportsMobile(VehicleReportUseFilter filter);
        Task<GenericResponse<VehicleReportUseDto>> GetVehicleCurrentUse(int VehicleId);
        Task<GenericResponse<VehicleReportUseDto>> MarkFastTravelAsFinished(UseReportFastTravelFinishRequest request);
        Task<GenericResponse<VehicleReportUseDto>> MarkNormalTravelAsFinished(UseReportFinishRequest request);
        Task<GenericResponse<VehicleReportUseDto>> MarkTravelAsCanceled(UseReportCancelRequest request);
        Task<GenericResponse<VehicleReportUseDto>> UpdateUseReport(UseReportUpdateRequest request);
        Task<GenericResponse<VehicleReportUseDto>> UseAdminTravel(UseReportAdminRequest request);
        Task<GenericResponse<VehicleReportUseDto>> UseFastTravel(UseReportFastTravelRequest request);
        Task<GenericResponse<VehicleReportUseDto>> UseNormalTravel(VehicleReportUseProceso request);
        Task<GenericResponse<VehicleReportUseDto>> VerifyVehicleUse(VehicleReportUseVerificationRequest request);
    }
}
