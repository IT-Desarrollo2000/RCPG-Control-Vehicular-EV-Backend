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
    [Route("api/vehicleMaintenance")]
    [ApiController]
    public class VehicleMaintenanceController : ControllerBase
    {
        private readonly IVehicleMaintenanceService _maintenanceService;

        public VehicleMaintenanceController(IVehicleMaintenanceService vehicleMaintenanceService)
        {
            this._maintenanceService = vehicleMaintenanceService;
        }

        //GETALL
        [Authorize(Roles = "Administrator, AdminUser, Supervisor")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetVehicleMaintenance([FromQuery] VehicleMaintenanceFilter filter)
        {
            var approval = await _maintenanceService.GetVehicleMaintenanceAll(filter);

            var metadata = new Metadata()
            {
                TotalCount = approval.TotalCount,
                PageSize = approval.PageSize,
                CurrentPage = approval.CurrentPage,
                TotalPages = approval.TotalPages,
                HasNextPage = approval.HasNextPage,
                HasPreviousPage = approval.HasPreviousPage
            };

            var response = new GenericResponse<IEnumerable<VehicleMaintenanceDto>>(approval)
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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(VehicleMaintenanceDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("GetById/{id:int}")]
        public async Task<ActionResult<VehicleMaintenanceDto>> Get(int id)
        {
            var entidad = await _maintenanceService.GetVehicleMaintenanceById(id);
            if (entidad.success) { return Ok(entidad); } else { return BadRequest(entidad); }
        }

        //POST
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [Route("InitiateMaintenance")]
        public async Task<IActionResult> InitiateMaintenance(VehicleMaintenanceRequest request)
        {
            var entity = await _maintenanceService.InitiateMaintenance(request);
            if (entity.success) { return Ok(entity); } else { return BadRequest(entity); }
        }

        //FINALIZE
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        [Route("FinalizeMaintenance")]
        public async Task<IActionResult> FinalizeMaintenance(FinalizeMaintenanceRequest request)
        {
            var entity = await _maintenanceService.FinalizeMaintenance(request);
            if (entity.success) { return Ok(entity); } else { return BadRequest(entity); }
        }

        //CANCEL
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        [Route("CancelMaintenance")]
        public async Task<IActionResult> CancelMaintenance(CancelMaintenanceRequest request)
        {
            var entity = await _maintenanceService.CancelMaintenance(request);
            if (entity.success) { return Ok(entity); } else { return BadRequest(entity); }
        }

        //UPDATE
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateMaintenance(MaintenanceUpdateRequest request)
        {
            var entity = await _maintenanceService.UpdateMaintenance(request);
            if (entity.success) { return Ok(entity); } else { return BadRequest(entity); }
        }

        //DELETE
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteMaintenance(int Id)
        {
            var entity = await _maintenanceService.DeleteVehicleManintenance(Id);
            if (entity.success) { return Ok(entity); } else { return BadRequest(entity); }
        }

        //ADD PROGRESS
        [Authorize(Roles = "Supervisor, Administrator, AdminUser, AppUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(MaintenanceProgressDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        [Route("AddProgress")]
        public async Task<IActionResult> AddMaintenanceProgress([FromForm]MaintenanceProgressRequest request)
        {
            var entity = await _maintenanceService.AddProgress(request);
            if (entity.success) { return Ok(entity); } else { return BadRequest(entity); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete]
        [Route("DeleteProgress")]
        public async Task<IActionResult> DeleteProgressImage(int progressId)
        {
            var entity = await _maintenanceService.DeleteProgress(progressId);
            if (entity.success) { return Ok(entity); } else { return BadRequest(entity); }
        }
    }
}
