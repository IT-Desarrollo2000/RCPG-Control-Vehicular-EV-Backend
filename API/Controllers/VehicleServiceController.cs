using Application.Interfaces;
using Application.Services;
using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace API.Controllers
{
    [Route("api/vehicleService")]
    [ApiController]
    public class VehicleServiceController : ControllerBase
    {
        private readonly IVehicleServiService _vehicleServiService;

        public VehicleServiceController(IVehicleServiService vehicleServiService)
        {
            this._vehicleServiService = vehicleServiService;
        }

        //GETALL
        [Authorize(Roles = "Administrator, AdminUser, Supervisor")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PagedList<VehicleServiceDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("GetAll")]
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

             var response = new GenericResponse<IEnumerable<VehicleServiceDto>>(approval)
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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<VehicleServiceDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet("GetById/{id:int}")]
        public async Task<IActionResult> GetById(int id)
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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<VehicleServiceDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("CreateService")]
        [HttpPost]
        public async Task<ActionResult<VehicleServiceDto>> Post([FromBody] VehicleServiceRequest vehicleServiceRequest)
        {
            var entidad = await _vehicleServiService.PostVehicleService(vehicleServiceRequest);

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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<VehicleServiceDto>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("UpdateService")]
        [HttpPut]
        public async Task<ActionResult<VehicleServiceDto>> PutVehicleService([FromBody] VehicleServiceUpdateRequest vehicleServiceRequest)
        {

            var entidad = await _vehicleServiService.PutVehicleService(vehicleServiceRequest);

            if (entidad.success)
            {
                return Ok(entidad);
            }
            else
            {
                return BadRequest($"No se pudo agregar el VehicleService");
            }
        }

        //FINISHED
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<VehicleServiceDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("MarkAsFinished")]
        [HttpPut]
        public async Task<ActionResult<VehicleServiceDto>> MarkAsFinished([FromBody] VehicleServiceFinishRequest vehicleServiceRequest)
        {

            var entidad = await _vehicleServiService.MarkAsResolved(vehicleServiceRequest);

            if (entidad.success)
            {
                return Ok(entidad);
            }
            else
            {
                return BadRequest(entidad);
            }
        }

        //CANCELED
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<VehicleServiceDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("MarkAsCanceled")]
        [HttpPut]
        public async Task<ActionResult<VehicleServiceDto>> MarkAsCanceled([FromBody] VehicleServiceCanceledRequest vehicleServiceRequest)
        {

            var entidad = await _vehicleServiService.MarkAsCanceled(vehicleServiceRequest);

            if (entidad.success)
            {
                return Ok(entidad);
            }
            else
            {
                return BadRequest(entidad);
            }
        }

        //DELETE
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<VehicleServiceDto>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
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
                return BadRequest(existe);
            }

        }

        //GET BY DEPARTMENTID
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet("GetByDepartment")]
        public async Task<IActionResult> GetByDepartment(int departmentId)
        {
            var entidad = await _vehicleServiService.GetServicesByDepartment(departmentId);
            if (entidad.success)
            {
                return Ok(entidad);
            }
            else
            {
                return BadRequest(entidad);

            }

        }
    }
}
