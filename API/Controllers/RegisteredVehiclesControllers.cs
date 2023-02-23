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
    [Route("api/registeredVehicles")]
    [ApiController]
    public class RegisteredVehiclesControllers : ControllerBase
    {
        private readonly IRegisteredVehiclesServices _registeredVehiclesServices;

        public RegisteredVehiclesControllers(IRegisteredVehiclesServices registeredVehiclesServices)
        {
            this._registeredVehiclesServices = registeredVehiclesServices;
        }

        //[Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PagedList<Vehicle>))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetVehicles([FromQuery] VehicleFilter filter)
        {
            var vehicles = await _registeredVehiclesServices.GetVehicles(filter);

            var metadata = new Metadata()
            {
                TotalCount = vehicles.TotalCount,
                PageSize = vehicles.PageSize,
                CurrentPage = vehicles.CurrentPage,
                TotalPages = vehicles.TotalPages,
                HasNextPage = vehicles.HasNextPage,
                HasPreviousPage = vehicles.HasPreviousPage
            };

            var response = new GenericResponse<IEnumerable<Vehicle>>(vehicles)
            {
                Meta = metadata,
                success = true,
                Data = vehicles
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            return Ok(response);
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<VehiclesDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("GetVehicleById/{id}")]
        public async Task<IActionResult> GetVehicleById(int id)
        {
            var result = await _registeredVehiclesServices.GetVehicleById(id);
            if (result.Data == null) { return NotFound($"No existe vehiculo con el Id {id}"); }
            if (result.success) { return Ok(result); } else { return NotFound(result); }
        }

        //[Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<VehiclesDto>))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [Route("AddVehicles")]
        public async Task<IActionResult> AddVehicles([FromForm] VehicleRequest vehicleRequest)
        {
            var result = await _registeredVehiclesServices.AddVehicles(vehicleRequest);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<PerformanceDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [Route("Performance")]
        public async Task<IActionResult> Performance(PerformanceRequest performanceRequest)
        {
            var result = await _registeredVehiclesServices.Performance(performanceRequest);
            if (result.Data == null) { return NotFound($"No existe ese vehiculo"); }
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<PerformanceDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [Route("PerformanceList")]
        public async Task<IActionResult> PerformanceList(List<PerformanceRequest> performanceRequests)
        {
            var result = await _registeredVehiclesServices.PerformanceList(performanceRequests);
            if (result.Data == null) { return NotFound($"No existe ese vehiculo"); }
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        //[Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<Vehicle>))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        [Route("PutVehicles")]
        public async Task<IActionResult> PutVehicles(VehiclesUpdateRequest vehiclesUpdateRequest, int id)
        {
            var result = await _registeredVehiclesServices.PutVehicles(vehiclesUpdateRequest, id);
            if (result.Data == null) { return NotFound($"No existe vehiculo con el Id {id}"); }
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        //[Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<bool>))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete]
        [Route("DeleteVehicles")]
        public async Task<IActionResult> DeleteVehicles(int id)
        {
            var result = await _registeredVehiclesServices.DeleteVehicles(id);
            if (result == null) { return NotFound($"No existe vehiculo con el Id {id}"); }
            if (result.success) { return Ok(result); }
            else
            {
                return BadRequest(result);
            }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<VehicleImage>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [Route("Vehicle/{vehicleId:int}/AddImage")]
        public async Task<IActionResult> AddVehicleImage(int vehicleId, [FromForm] VehicleImageRequest vehicleRequest)
        {
            var result = await _registeredVehiclesServices.AddVehicleImage(vehicleRequest, vehicleId);
            if (result == null) return NotFound("No se encontro el vehiculo especificado");
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete]
        [Route("Vehicle/{imageId:int}/DeleteImage")]
        public async Task<IActionResult> DeleteVehicleImage(int imageId)
        {
            var result = await _registeredVehiclesServices.DeleteVehicleImage(imageId);
            if (result == null) { return NotFound($"No existe imagen con el Id {imageId}"); }
            if (result.success) { return Ok(result); }
            else
            {
                return BadRequest(result);
            }
        }

    }
}
