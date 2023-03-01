using Application.Interfaces;
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
    [Route("api/vehicleUseReport")]
    [ApiController]
    public class VehicleReportUseController : ControllerBase
    {
        private readonly IVehicleReportUseService _vehicleReportUseService;

        public VehicleReportUseController(IVehicleReportUseService vehicleReportUseService)
        {
            this._vehicleReportUseService = vehicleReportUseService;
        }

        //GETALL
        [Authorize(Roles = "Administrator, AdminUser, Supervisor, AppUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetUseReports([FromQuery] VehicleReportUseFilter filter)
        {
            var approval = await _vehicleReportUseService.GetUseReports(filter);

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
        [Authorize(Roles = "Supervisor, Administrator, AdminUser, AppUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet("GetById/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entidad = await _vehicleReportUseService.GetUseReportById(id);
            if (entidad.success)
            {
                return Ok(entidad);
            }
            else
            {
                return BadRequest(entidad);

            }

        }

        //VIAJE NORMAL
        [Authorize(Roles = "Supervisor, Administrator, AdminUser, AppUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [Route("InitiateNormalTravel")]
        public async Task<IActionResult> InitiateNormalTravel([FromBody] VehicleReportUseProceso request)
        {
            var result = await _vehicleReportUseService.UseNormalTravel(request);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }


        //VIAJE RAPIDO
        [Authorize(Roles = "Administrator, AppUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [Route("InitiateFastTravel")]
        public async Task<IActionResult> InitiateFastTravel([FromBody] UseReportFastTravelRequest request)
        {
            var result = await _vehicleReportUseService.UseFastTravel(request);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        //VIAJE NORMAL ADMIN
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [Route("InitiateAdminTravel")]
        public async Task<IActionResult> InitiateAdminTravel([FromBody] UseReportAdminRequest request)
        {
            var result = await _vehicleReportUseService.UseAdminTravel(request);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        //FINALIZAR VIAJE NORMAL
        [Authorize(Roles = "Supervisor, Administrator, AdminUser, AppUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        [Route("FinishNormalTravel")]
        public async Task<IActionResult> FinishNormalUse(UseReportFinishRequest request)
        {
            var result = await _vehicleReportUseService.MarkNormalTravelAsFinished(request);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        //FINALIZAR VIAJE RAPIDO
        [Authorize(Roles = "Supervisor, Administrator, AdminUser, AppUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        [Route("FinishFastTravel")]
        public async Task<IActionResult> FinishFastUse(UseReportFastTravelFinishRequest request)
        {
            var result = await _vehicleReportUseService.MarkFastTravelAsFinished(request);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        //VERIFICAR EL VIAJE
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        [Route("VerifyByAdmin")]
        public async Task<IActionResult> VerifyUseReport(VehicleReportUseVerificationRequest request)
        {
            var result = await _vehicleReportUseService.VerifyVehicleUse(request);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        //VERIFICAR EL VIAJE
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        [Route("Cancel")]
        public async Task<ActionResult<IActionResult>> CancelUseReport(UseReportCancelRequest request)
        {
            var result = await _vehicleReportUseService.MarkTravelAsCanceled(request);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        //ACTUALIZAR REPORT DE USO
        [Authorize(Roles = "Supervisor, Administrator, AdminUser, AppUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateUseReport(UseReportUpdateRequest request)
        {
            var result = await _vehicleReportUseService.UpdateUseReport(request);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        //ELIMINAR
        [Authorize(Roles = "Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleReportUse(int id)
        {
            var result = await _vehicleReportUseService.DeleteVehicleReportUse(id);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }
    }
}
