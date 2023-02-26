using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Application.Interfaces
{
    public interface IVehicleReportUseService
    {
        Task<GenericResponse<bool>> DeleteVehicleReportUse(int Id);
        Task<GenericResponse<VehicleReportUseDto>> GetUseReportById(int Id);
        Task<PagedList<VehicleReportUseDto>> GetUseReports(VehicleReportUseFilter filter);
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
