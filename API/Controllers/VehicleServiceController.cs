using Application.Interfaces;
using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace API.Controllers
{
    [Route("api/vehicleService")]
    [ApiController]
    public class VehicleServiceController: ControllerBase
    {
        private readonly IVehicleServiService _vehicleServiService;

        public VehicleServiceController( IVehicleServiService vehicleServiService)
        {
            this._vehicleServiService = vehicleServiService;
        }

        //GETALL
         [Authorize(Roles = "Administrator, AdminUser")]
         [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<VehicleServiceDto>))]
         [ProducesResponseType((int)HttpStatusCode.BadRequest)]
         [HttpGet]
         [Route("")]
         public async Task<IActionResult> GetVehicleService([FromQuery] VehicleServiceFilter filter)
         {
             var approval = await _vehicleServiService.GetVehicleServiceAll(filter);

             var metadata = new Metadata()
             {
                 TotalCount = approval.TotalCount,
                 PageSize = approval.PageSize,
                 CurrentPage = approval.CurrentPage,
                 TotalPages = approval.TotalPages,
                 HasNextPage = approval.HasNextPage,
                 HasPreviousPage = approval.HasPreviousPage
             };

             var response = new GenericResponse<IEnumerable<VehicleService>>(approval)
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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(VehicleServiceDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<VehicleServiceDto>> Get(int id)
        {
            var entidad = await _vehicleServiService.GetVehicleServiceById(id);
            if (entidad.Data == null)
            {
                return NotFound($"No existe VehicleService con este Id {id}");
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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(VehicleServiceDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<ActionResult<VehicleServiceDto>> Post([FromBody] VehicleServiceRequest vehicleServiceRequest)
        {
            var entidad = await _vehicleServiService.PostVehicleService(vehicleServiceRequest);
            if (entidad.Data == null)
            {
                return NotFound($"No existe Vehicle con el VehicleId {vehicleServiceRequest.VehicleId} para Actualizar VehicleService");
            }

            /* if ( == null && departamentRequest.CompanyId == null)
             {
                 return BadRequest("Los campos Name y CompanyId no pueden ir vacios");
             }*/

            if (entidad.success)
            {
                return Ok(entidad);
            }
            else
            {
                return BadRequest($"No se pudo agregar el VehicleService");
            }

        }

        //PUT
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(VehicleServiceDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        public async Task<ActionResult<VehicleServiceDto>> PutVehicleService(int id, [FromBody] VehicleServiceRequest vehicleServiceRequest)
        {

            var entidad = await _vehicleServiService.PutVehicleService(id, vehicleServiceRequest);

            if (entidad == null)
            {
                return NotFound($"No existe Vehicle con el VehicleId {id} para Actualizar VehicleService");
            }

            if (entidad.Data == null)
            {
                return NotFound($"No existe vehicle con el vehicleId {vehicleServiceRequest.VehicleId} para Actualizar VehicleService");
            }

            if (entidad.success)
            {
                return Ok(entidad);

            }
            else
            {
                return BadRequest($"No se pudo agregar el VehicleService");
            }
        }

        //Delete
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(VehicleServiceDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<VehicleServiceDto>> DeleteVehicleService(int id)
        {
            var existe = await _vehicleServiService.DeleteVehicleService(id);
            if (existe == null)
            {
                return NotFound($"No existe VehicleService con el Id {id} para borrar");
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
