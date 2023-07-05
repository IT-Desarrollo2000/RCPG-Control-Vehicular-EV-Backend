using Application.Extensions;
using Application.Interfaces;
using Application.Services;
using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
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
        public async Task<IActionResult> GetVehicleExportDataExcel([FromQuery] VehicleExportFilter filter)
        { 
            try
            {
                var exportData = await _importExportServices.ExportVehiclesData(filter);

                byte[] fileContents = ExcelExporter.ExportToExcel(exportData);

                return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "RCPG_VEHICULAR_EXPORT.xlsx");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
