using Application.Interfaces;
using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace API.Controllers
{
    [Route("api/graphics")]
    [ApiController]
    public class GraphicsController: ControllerBase
    {
        private readonly IRegisteredVehiclesServices _registeredVehiclesServices;

        public GraphicsController(IRegisteredVehiclesServices registeredVehiclesServices)
        {
            this._registeredVehiclesServices = registeredVehiclesServices;
        }

        //[Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<PerformanceDto>))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [Route("Performance")]
        public async Task<IActionResult> Performance(PerformanceRequest performanceRequest)
        {
            var result = await _registeredVehiclesServices.Performance(performanceRequest);
            if (result.Data == null) { return NotFound($"No existe ese vehiculo"); }
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        //[Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<PerformanceDto>))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [Route("PerformanceList")]
        public async Task<IActionResult> PerformanceList(List<PerformanceRequest> performanceRequests)
        {
            var result = await _registeredVehiclesServices.PerformanceList(performanceRequests);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }


        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<GraphicsDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("GetServicesAndWorkshop/{VehicleId}")]
        public async Task<IActionResult> GetServicesAndWorkshop(int VehicleId)
        {
            var result = await _registeredVehiclesServices.GetServicesAndWorkshop(VehicleId);
            if (result.Data == null) { return NotFound($"No existe vehiculo con el id {VehicleId}"); }
            if (result.success) { return Ok(result); } else { return NotFound(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<GraphicsDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [Route("GetServicesAndMaintenanceList")]
        public async Task<IActionResult> GetServicesAndMaintenanceList([FromForm] List<int> VehicleId)
        {
            var result = await _registeredVehiclesServices.GetServicesAndMaintenanceList(VehicleId);
            if (result.success) { return Ok(result); } else { return NotFound(result); }
        }

        [HttpGet]
        public async Task<IActionResult> GetExpenses(int VehicleId)
        {
            var result = await _registeredVehiclesServices.GetExpenses(VehicleId);
            if (result.success) { return Ok(result); } else { return NotFound(result); }
        }
    }
}
