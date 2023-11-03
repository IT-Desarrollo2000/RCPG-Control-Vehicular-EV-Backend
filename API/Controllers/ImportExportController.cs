using Application.Extensions;
using Application.Interfaces;
using Application.Services;
using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace API.Controllers
{
    [Route("api/DataExport")]
    [ApiController]
    public class ImportExportController : ControllerBase
    {
        private readonly IImportExportServices _importExportServices;

        public ImportExportController(IImportExportServices importExportServices)
        {
            _importExportServices = importExportServices;
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("ExportVehicleData")]
        public async Task<IActionResult> GetVehicleExportData([FromQuery]VehicleExportFilter filter)
        {
            var exportData = await _importExportServices.ExportVehiclesData(filter);

            var metadata = new Metadata()
            {
                TotalCount = exportData.TotalCount,
                PageSize = exportData.PageSize,
                CurrentPage = exportData.CurrentPage,
                TotalPages = exportData.TotalPages,
                HasNextPage = exportData.HasNextPage,
                HasPreviousPage = exportData.HasPreviousPage
            };

            var response = new GenericResponse<IEnumerable<VehicleExportDto>>(exportData)
            {
                Meta = metadata,
                success = true,
                Data = exportData
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            return Ok(response);
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("ExportVehicleDataExcel")]
        public async Task<IActionResult> ExportVehicleDataExcel([FromQuery] VehicleExportFilter filter)
        {
            try
            {
                var exportData = await _importExportServices.ExportVehiclesData(filter);
                if(exportData.Count() == 0 || exportData == null)
                {
                    return NotFound("No hay información para mostrar");
                }

                byte[] fileContents = ExcelExporter.ExportToExcel(exportData);

                return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "RCPG_VEHICULAR_VEHICLE_EXPORT.xlsx");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("ExportPolicyData")]
        public async Task<IActionResult> ExportPolicyData([FromQuery] PolicyExportFilter filter)
        {
            var exportData = await _importExportServices.ExportVehiclePolicyData(filter);

            var metadata = new Metadata()
            {
                TotalCount = exportData.TotalCount,
                PageSize = exportData.PageSize,
                CurrentPage = exportData.CurrentPage,
                TotalPages = exportData.TotalPages,
                HasNextPage = exportData.HasNextPage,
                HasPreviousPage = exportData.HasPreviousPage
            };

            var response = new GenericResponse<IEnumerable<PolicyExportDto>>(exportData)
            {
                Meta = metadata,
                success = true,
                Data = exportData
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            return Ok(response);
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("ExportPolicyDataExcel")]
        public async Task<IActionResult> ExportPolicyDataExcel([FromQuery] PolicyExportFilter filter)
        {
            try
            {
                var exportData = await _importExportServices.ExportVehiclePolicyData(filter);
                if (exportData.Count() == 0 || exportData == null)
                {
                    return NotFound("No hay información para mostrar");
                }
                byte[] fileContents = ExcelExporter.ExportToExcel(exportData);

                return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "RCPG_VEHICULAR_POLICY_EXPORT.xlsx");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [Route("ImportVehicles")]
        public async Task<IActionResult> ImportVehicles([FromForm] VehicleImportExpertRequest vehicleImportExpertRequest)
        {
            var result = await _importExportServices.ImportVehicles(vehicleImportExpertRequest);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }
    }
}
