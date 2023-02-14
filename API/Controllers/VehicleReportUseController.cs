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
    [Route("api/vehicleReportUse")]
    [ApiController]
    public class VehicleReportUseController : ControllerBase
    {
        private readonly IVehicleReportUseService _vehicleReportUseService;

        public VehicleReportUseController(IVehicleReportUseService vehicleReportUseService) 
        {
            this._vehicleReportUseService = vehicleReportUseService;
        }

        //GETALL
        [Authorize(Roles = "Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PagedList<VehicleReportUseDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetVehicleReport([FromQuery] VehicleReportUseFilter filter)
        {
            var approval = await _vehicleReportUseService.GetVehicleReportUseAll(filter);

            var metadata = new Metadata()
            {
                TotalCount = approval.TotalCount,
                PageSize = approval.PageSize,
                CurrentPage = approval.CurrentPage,
                TotalPages = approval.TotalPages,
                HasNextPage = approval.HasNextPage,
                HasPreviousPage = approval.HasPreviousPage
            };

            var response = new GenericResponse<IEnumerable<VehicleReportUseDto>>(approval)
            {
                Meta = metadata,
                success = true,
                Data = approval
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            return Ok(response);
        }

        //GETBYID
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(VehicleReportUseDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<VehicleReportUseDto>> Get(int id)
        {
            var entidad = await _vehicleReportUseService.GetVehicleReporUseById(id);
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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(VehicleReportUseDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<ActionResult<VehicleReportUseDto>> Post([FromBody] VehicleReportUseRequest vehicleReportUseRequest)
        {
            var entidad = await _vehicleReportUseService.PostVehicleReporUse(vehicleReportUseRequest);

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
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(VehicleReportUseDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        public async Task<ActionResult<VehicleReportUseDto>> PutVehicleReportUse(int id, [FromBody] VehicleReportUseRequest vehicleReportUseRequest)
        {

            var entidad = await _vehicleReportUseService.PutVehicleReportUse(id,vehicleReportUseRequest);

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
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(VehicleReportUseDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        [Route("Verification")]
        public async Task<ActionResult<VehicleReportUseDto>> PutVehicleReportUseVerification(int id, [FromBody] VehicleReportUseVerificationRequest vehicleReportUseVerificationRequest)
        {

            var entidad = await _vehicleReportUseService.PutVehicleVerification(id, vehicleReportUseVerificationRequest);

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
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(VehicleReportUseDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        [Route("Status")]
        public async Task<ActionResult<VehicleReportUseDto>> PutStatusVehicleReport(int id, int VehicleId ,[FromBody] ReportUseTypeRequest reportUseTypeRequest)
        {

            var entidad = await _vehicleReportUseService.PutVehicleStatusReport(id, VehicleId, reportUseTypeRequest);

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
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(VehicleReportUseDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<VehicleReportDto>> DeleteVehicleReportUse(int id)
        {
            var existe = await _vehicleReportUseService.DeleteVehicleReportUse(id);
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


    }
}
