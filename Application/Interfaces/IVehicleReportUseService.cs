using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Application.Interfaces
{
    public interface IVehicleReportUseService
    {
        Task<GenericResponse<VehicleReportUseDto>> DeleteVehicleReportUse(int Id);
        Task<PagedList<VehicleReportUseDto>> GetVehicleReportUseAll(VehicleReportUseFilter filter);
        Task<GenericResponse<VehicleReportUseDto>> GetVehicleReporUseById(int Id);
        Task<GenericResponse<VehicleReportUseDto>> PostEnProceso(VehicleReportUseProceso vehicleReportUseProceso);
        Task<GenericResponse<VehicleReportUseDto>> PostViajeRapido(VehicleReportUseFastTravel vehicleReportUseFastTravel);
        Task<GenericResponse<VehicleReportUseDto>> PutVehicleStatusReport(int Id, int VehicleId, [FromBody] ReportUseTypeRequest reportUseTypeRequest);
        Task<GenericResponse<VehicleReportUseDto>> PutVehicleVerification(int Id, [FromBody] VehicleReportUseVerificationRequest vehicleReportUseVerificationRequest);
    }
}
