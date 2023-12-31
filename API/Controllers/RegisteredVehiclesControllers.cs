﻿using Application.Interfaces;
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
    [Route("api/registeredVehicles")]
    [ApiController]
    public class RegisteredVehiclesControllers : ControllerBase
    {
        private readonly IRegisteredVehiclesServices _registeredVehiclesServices;

        public RegisteredVehiclesControllers(IRegisteredVehiclesServices registeredVehiclesServices)
        {
            this._registeredVehiclesServices = registeredVehiclesServices;
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
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

            var response = new GenericResponse<IEnumerable<VehiclesDto>>(vehicles)
            {
                Meta = metadata,
                success = true,
                Data = vehicles
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            return Ok(response);
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser, AppUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("GetVehicleById/{id}")]
        public async Task<IActionResult> GetVehicleById(int id)
        {
            var result = await _registeredVehiclesServices.GetVehicleById(id);
            if (result.Data == null) { return NotFound($"No existe vehiculo con el Id {id}"); }
            if (result.success) { return Ok(result); } else { return NotFound(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser, AppUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("GetByQR/{QRId}")]
        public async Task<IActionResult> GetVehicleByQRId(string QRId)
        {
            var result = await _registeredVehiclesServices.GetVehicleByQRId(QRId);
            if (result.success) { return Ok(result); } else { return NotFound(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [Route("AddVehicles")]
        public async Task<IActionResult> AddVehicles([FromForm] VehicleRequest vehicleRequest)
        {
            var result = await _registeredVehiclesServices.AddVehicles(vehicleRequest);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
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
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [Route("PerformanceList")]
        public async Task<IActionResult> PerformanceList(List<PerformanceRequest> performanceRequests)
        {
            var result = await _registeredVehiclesServices.PerformanceList(performanceRequests);
            if (result.Data == null) { return NotFound($"No existe ese vehiculo"); }
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        [Route("PutVehicles")]
        public async Task<IActionResult> PutVehicles(VehiclesUpdateRequest vehiclesUpdateRequest, int id)
        {
            var result = await _registeredVehiclesServices.PutVehicles(vehiclesUpdateRequest, id);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
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
        [ProducesResponseType((int)HttpStatusCode.OK)]
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

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        [Route("MarkAsInactive")]
        public async Task<IActionResult> MarkAsInactive(int VehicleId)
        {
            var result = await _registeredVehiclesServices.MarkVehicleAsInactive(VehicleId);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        [Route("MarkAsSaved")]
        public async Task<IActionResult> MarkAsSaved(int VehicleId)
        {
            var result = await _registeredVehiclesServices.MarkVehicleAsSaved(VehicleId);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        [Route("Reactivate")]
        public async Task<IActionResult> ReactivateVehicle(int VehicleId)
        {
            var result = await _registeredVehiclesServices.ReactivateVehicle(VehicleId);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser, AppUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("Maintenances/{VehicleId:int}")]
        public async Task<IActionResult> GetLastMaintenances(int VehicleId)
        {
            var result = await _registeredVehiclesServices.GetLatestMaintenanceDto(VehicleId);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [Route("Vehicle/{vehicleId:int}/AddCirculationCardImage")]
        public async Task<IActionResult> AddCirculationCard([FromForm] CirculationCardRequest circulationCardRequest, int vehicleId)
        {
            var result = await _registeredVehiclesServices.AddCirculationCardImage(circulationCardRequest, vehicleId);
            if (result == null) return NotFound("No se encontro el vehiculo especificado");
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [Route("Vehicle/AddInvoiceFile")]
        public async Task<IActionResult> AddInvoiceFile([FromForm] InvoiceFileRequest request)
        {
            var result = await _registeredVehiclesServices.AddVehicleInvoiceFile(request.InvoiceFile, request.VehicleId);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete]
        [Route("Vehicle/CirculationCardImage")]
        public async Task<IActionResult> DeleteCirculationCard(int VehicleId)
        {
            var result = await _registeredVehiclesServices.DeleteCirculationCardImage(VehicleId);
            if (result == null) { return NotFound($"No existe vehiculo con el Id {VehicleId}"); }
            if (result.success) { return Ok(result); }
            else
            {
                return BadRequest(result);
            }
        }
        
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("GetDepartmentVehicles")]
        public async Task<IActionResult> GetDepartmentVehicles(int departmentId)
        {
            var result = await _registeredVehiclesServices.GetVehiclesByDepartment(departmentId);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PagedList<SpecialVehicleDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("GetVehicleInfo")]
        public async Task<IActionResult> GetFilteredVehicles([FromQuery] SpecialVehicleFilter filter)
        {
            var vehicles = await _registeredVehiclesServices.GetFilteredVehicles(filter);

            var metadata = new Metadata()
            {
                TotalCount = vehicles.TotalCount,
                PageSize = vehicles.PageSize,
                CurrentPage = vehicles.CurrentPage,
                TotalPages = vehicles.TotalPages,
                HasNextPage = vehicles.HasNextPage,
                HasPreviousPage = vehicles.HasPreviousPage
            };

            var response = new GenericResponse<IEnumerable<SpecialVehicleDto>>(vehicles)
            {
                Meta = metadata,
                success = true,
                Data = vehicles
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            return Ok(response);
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("GetResponsibleNames")]
        public async Task<IActionResult> GetResponsibleNames()
        {
            var result = await _registeredVehiclesServices.GetResponsibleNames();
            if (result.success) { return Ok(result); } else { return NotFound(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("GetBrandNames")]
        public async Task<IActionResult> GetBrandNames()
        {
            var result = await _registeredVehiclesServices.GetBrandNames();
            if (result.success) { return Ok(result); } else { return NotFound(result); }
        }
    }
}
