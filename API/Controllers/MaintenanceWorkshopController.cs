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
    [Route("api/maintenanceWorkshop")]
    [ApiController]
    public class MaintenanceWorkshopController : ControllerBase
    {
        private readonly IMaintenanceWorkshopService _maintenanceWorkshopService;

        public MaintenanceWorkshopController(IMaintenanceWorkshopService maintenanceWorkshopService)
        {
            this._maintenanceWorkshopService = maintenanceWorkshopService;
        }

        //GETALL
        [Authorize(Roles = "Administrator, AdminUser, Supervisor")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<MaintenanceWorkshopDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetMaintenanceWorkshop([FromQuery] MaintenanceWorkshopFilter filter)
        {
            var approval = await _maintenanceWorkshopService.GetMaintenanceWorkshopAll(filter);

            var metadata = new Metadata()
            {
                TotalCount = approval.TotalCount,
                PageSize = approval.PageSize,
                CurrentPage = approval.CurrentPage,
                TotalPages = approval.TotalPages,
                HasNextPage = approval.HasNextPage,
                HasPreviousPage = approval.HasPreviousPage
            };

            var response = new GenericResponse<IEnumerable<VehicleMaintenanceWorkshop>>(approval)
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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(MaintenanceWorkshopDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MaintenanceWorkshopDto>> Get(int id)
        {
            var entidad = await _maintenanceWorkshopService.GetMaintenanceWorkshopById(id);
            if (entidad.Data == null)
            {
                return NotFound($"No existe registro del Taller de Mantenimiento con este Id {id}");
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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(MaintenanceWorkshopDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<ActionResult<MaintenanceWorkshopDto>> Post([FromBody] MaintenanceWorkshopRequest maintenanceWorkshopRequest)
        {
            var entidad = await _maintenanceWorkshopService.PostMaintenanceWorkshop(maintenanceWorkshopRequest);

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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(MaintenanceWorkshopDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        public async Task<ActionResult<MaintenanceWorkshopDto>> PutMaintenanceWorkshop(int id, [FromBody] MaintenanceWorkshopRequest maintenanceWorkshopRequest)
        {

            var entidad = await _maintenanceWorkshopService.PutMaintenanceWorkshop(id, maintenanceWorkshopRequest);

            if (entidad == null)
            {
                return NotFound($"No existe MaintenanceWorkshop con el Id {id} para Actualizar MaintenanceWorkshop");
            }


            if (entidad.success)
            {
                return Ok(entidad);

            }
            else
            {
                return BadRequest($"No se pudo agregar el VehicleMaintenance");
            }
        }

        //Delete
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(MaintenanceWorkshopDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<MaintenanceWorkshopDto>> DeleteMaintenanceWorkshop(int id)
        {
            var existe = await _maintenanceWorkshopService.DeleteMaintenanceWorkshop(id);
            if (existe == null)
            {
                return NotFound($"No existe MaintenanceWorkshop con el Id {id} para borrar");
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
