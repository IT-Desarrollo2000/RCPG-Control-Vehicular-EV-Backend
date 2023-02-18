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
    public interface IVehicleReportService
    {
        Task<GenericResponse<VehicleReportImage>> AddReportImage(VehicleImageRequest request, int reportId);
        Task<GenericResponse<bool>> DeleteReportImage(int reportImageId);
        Task<GenericResponse<bool>> DeleteVehicleReport(int Id);
        Task<PagedList<VehicleReportDto>> GetVehicleReportAll(VehicleReportFilter filter);
        Task<GenericResponse<VehicleReportDto>> GetVehicleReportById(int Id);
        Task<GenericResponse<VehicleReportDto>> ManageReportStatus(SolvedReportRequest request);
        Task<GenericResponse<VehicleReportDto>> PostVehicleReport(VehicleReportRequest vehicleReportRequest);
        Task<GenericResponse<VehicleReportDto>> PutVehicleReport(VehicleReportUpdateRequest vehicleReportRequest);
    }
}
