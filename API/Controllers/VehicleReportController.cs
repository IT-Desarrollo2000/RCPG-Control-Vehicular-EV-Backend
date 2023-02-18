using Application.Interfaces;
using Application.Services;
using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Text.Json;

namespace API.Controllers
{

    [Route("api/vehicleReport")]
    [ApiController]
    public class VehicleReportController : ControllerBase
    {
        private readonly IVehicleReportService _vehicleReportService;

        public VehicleReportController(IVehicleReportService vehicleReportService) 
        {
            this._vehicleReportService = vehicleReportService;
        }

        //GETALL
        //[Authorize(Roles = "Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<VehicleReportDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetVehicleReport([FromQuery] VehicleReportFilter filter)
        {
            var approval = await _vehicleReportService.GetVehicleReportAll(filter);

            var metadata = new Metadata()
            {
                TotalCount = approval.TotalCount,
                PageSize = approval.PageSize,
                CurrentPage = approval.CurrentPage,
                TotalPages = approval.TotalPages,
                HasNextPage = approval.HasNextPage,
                HasPreviousPage = approval.HasPreviousPage
            };

            var response = new GenericResponse<IEnumerable<VehicleReportDto>>(approval)
            {
                Meta = metadata,
                success = true,
                Data = approval
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            return Ok(response);
        }

        //GETBYID
        //[Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(VehicleReportDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<VehicleReportDto>> Get(int id)
        {
            var entidad = await _vehicleReportService.GetVehicleReportById(id);
            if (entidad.Data == null)
            {
                return NotFound($"No existe registro del Report con este Id {id}");
            }
            if (entidad.success)
            {
                return Ok(entidad);
            }
            else
            {
                return NotFound(entidad);

            }

        }

        //POST
        //[Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(VehicleReportDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("Create")]
        [HttpPost]
        public async Task<ActionResult<VehicleReportDto>> Post([FromForm] VehicleReportRequest vehicleReportRequest)
        {
            var entidad = await _vehicleReportService.PostVehicleReport(vehicleReportRequest);

            if (entidad.success)
            {
                return Ok(entidad);
            }
            else
            {
                return BadRequest(entidad);
            }

        }

        //PUT
        //[Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(VehicleReportDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("Update")]
        [HttpPut]
        public async Task<ActionResult<VehicleReportDto>> PutVehicleReport([FromBody] VehicleReportUpdateRequest vehicleReportRequest)
        {

            var entidad = await _vehicleReportService.PutVehicleReport(vehicleReportRequest);

            if (entidad.success)
            {
                return Ok(entidad);

            }
            else
            {
                return BadRequest(entidad);
            }
        }

        /// <summary>
        /// Permite la administración del reporte de uso, solo se usa para marcar el reporte con otros estatus(Cancelado/En proceso/Resuelto) por parte de un Admin
        /// </summary>
        /// <param name="vehicleReportRequest"></param>
        /// <returns>Devuelve el reporte</returns>
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(VehicleReportDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("ManageReport")]
        [HttpPut]
        public async Task<ActionResult<VehicleReportDto>> ManageReport([FromBody] SolvedReportRequest vehicleReportRequest)
        {

            var entidad = await _vehicleReportService.ManageReportStatus(vehicleReportRequest);

            if (entidad.success)
            {
                return Ok(entidad);

            }
            else
            {
                return BadRequest(entidad);
            }
        }

        //Delete
       // [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(VehicleReportDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<VehicleReportDto>> DeleteVehicleReport(int id)
        {
            var existe = await _vehicleReportService.DeleteVehicleReport(id);
            if (existe == null)
            {
                return NotFound($"No existe VehicleReport con el Id {id} para borrar");
            }

            if (existe.success)
            {
                return Ok(existe);
            }
            else
            {
                return NotFound();
            }

        }


        [Authorize(Roles = "Supervisor, Administrator, AdminUser, AppUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<VehicleReportImage>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [Route("{reportId:int}/AddImage")]
        public async Task<IActionResult> AddReportImage(int reportId, [FromForm] VehicleImageRequest request)
        {
            var result = await _vehicleReportService.AddReportImage(request, reportId);
            if (result == null) return NotFound("No se encontro el reporte especificado");
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser, AppUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete]
        [Route("{imageId:int}/DeleteImage")]
        public async Task<IActionResult> DeleteReportImage(int imageId)
        {
            var result = await _vehicleReportService.DeleteReportImage(imageId);
            if (result == null) { return NotFound($"No existe imagen con el Id {imageId}"); }
            if (result.success) { return Ok(result); }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
