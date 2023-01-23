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
    [Route("api/vehicleMaintenance")]
    [ApiController]
    public class VehicleMaintenanceController : ControllerBase
    {
        private readonly IVehicleMaintenanceService _vehicleMaintenanceService;

        public VehicleMaintenanceController( IVehicleMaintenanceService vehicleMaintenanceService)
        {
            this._vehicleMaintenanceService = vehicleMaintenanceService;
        }

        //GETALL
        [Authorize(Roles = "Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<VehicleMaintenanceDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetVehicleMaintenance([FromQuery] VehicleMaintenanceFilter filter)
        {
            var approval = await _vehicleMaintenanceService.GetVehicleMaintenanceAll(filter);

            var metadata = new Metadata()
            {
                TotalCount = approval.TotalCount,
                PageSize = approval.PageSize,
                CurrentPage = approval.CurrentPage,
                TotalPages = approval.TotalPages,
                HasNextPage = approval.HasNextPage,
                HasPreviousPage = approval.HasPreviousPage
            };

            var response = new GenericResponse<IEnumerable<VehicleMaintenance>>(approval)
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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(VehicleMaintenanceDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<VehicleMaintenanceDto>> Get(int id)
        {
            var entidad = await _vehicleMaintenanceService.GetVehicleMaintenanceById(id);
            if (entidad.Data == null)
            {
                return NotFound($"No existe registro del Mantenimiento con este Id {id}");
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
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(VehicleMaintenanceDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<ActionResult<VehicleMaintenanceDto>> Post([FromBody] VehicleMaintenanceRequest vehicleMaintenanceRequest)
        {
            var entidad = await _vehicleMaintenanceService.PostVehicleMaintenance(vehicleMaintenanceRequest);

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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(VehicleMaintenanceDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        public async Task<ActionResult<VehicleMaintenanceDto>> PutVehicleMaintenance(int id, [FromBody] VehicleMaintenanceRequest vehicleMaintenanceRequest)
        {

            var entidad = await _vehicleMaintenanceService.PutVehicleMaintenance(id, vehicleMaintenanceRequest);

           /* if (entidad == null)
            {
                return NotFound($"No existe Maintenance con el Id {id} para Actualizar VehicleMaintenance");
            }

            if (entidad.Data == null)
            {
                return NotFound($"No existe vehicle con el vehicleId {vehicleMaintenanceRequest.VehicleId} para Actualizar VehicleService");
            }*/

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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(VehicleMaintenanceDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<VehicleMaintenanceDto>> DeleteVehicleMaintenance(int id)
        {
            var existe = await _vehicleMaintenanceService.DeleteVehicleManintenance(id);
            if (existe == null)
            {
                return NotFound($"No existe VehicleMaintenance con el Id {id} para borrar");
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
