using Application.Interfaces;
using Application.Services;
using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Identity;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/utilities")]
    [ApiController]
    public class ToolsController : ControllerBase
    {
        private readonly IToolsServices _utilitesService;

        public ToolsController(IToolsServices toolsServices)
        {
            _utilitesService = toolsServices;
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<List<LicenceExpiredDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("LicenceExpirations")]
        public async Task<IActionResult> GetLicencesExpirations([FromQuery] LicenceExpStopLight request)
        {
            var result = await _utilitesService.GetLicencesExpirations(request);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        //[Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<List<PolicyExpiredDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("PolicyExpirations")]
        public async Task<IActionResult> GetPoliciesExpiration([FromQuery] LicenceExpStopLight request)
        {
            var result = await _utilitesService.GetPoliciesExpiration(request);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        //[Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<List<MaintenanceSpotlightDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("ServicesStoplight")]
        public async Task<IActionResult> GetServices()
        {
            var result = await _utilitesService.GetMaintenanceSpotlight();
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }
        
        //GETALL
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GetVehicleActiveDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("GetAllVehicleActive")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _utilitesService.GetAllVehiclesActive();
            if (users.success)
            {
                return Ok(users);
            }
            else
            {
                return BadRequest(users);
            }
        }

        //GETALL
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GraphicsPerfomanceDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("GetAllPerfomance")]
        public async Task<IActionResult> GetAllPerfomance(int VehicleId)
        {
            var users = await _utilitesService.GetAllPerfomance(VehicleId);
            if (users.success)
            {
                return Ok(users);
            }
            else
            {
                return BadRequest(users);
            }
        }

        //GETALL
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TotalPerfomanceDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("GetAllTotalPerfomance")]
        public async Task<IActionResult> GetAllTotalPerfomance(int VehicleId)
        {
            var users = await _utilitesService.GetTotalPerfomance(VehicleId);
            if (users.success)
            {
                return Ok(users);
            }
            else
            {
                return BadRequest(users);
            }
        }

        //GETpOST
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ListTotalPerfomanceDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [Route("ListTotalPerfomance")]
        public async Task<IActionResult> GetlistTotalPerfomance([FromForm] ListTotalPerfomanceDto listTotalPerfomanceDto)
        {
            var users = await _utilitesService.GetListTotalPerfomance(listTotalPerfomanceDto);
            if (users.success)
            {
                return Ok(users);
            }
            else
            {
                return BadRequest(users);
            }
        }

        //GETpOST
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GetUserForTravelDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("GetUserTravel")]
        public async Task<ActionResult<List<GetUserForTravelDto>>> GetUserTravel()
        {
            var users = await _utilitesService.GetUserForTravel();
            if (users.success)
            {
                return Ok(users);
            }
            else
            {
                return BadRequest(users);
            }
        }

        //GETpOST
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GetServicesMaintenance))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("GetTotalServiceMaintenance")]
        public async Task<IActionResult> GetServiceMaintenance()
        {
            var users = await _utilitesService.GetServiceMaintenance();
            if (users.success)
            {
                return Ok(users);
            }
            else
            {
                return BadRequest(users);
            }
        }
    }
}
