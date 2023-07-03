using Application.Interfaces;
using Application.Services;
using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Propietary;
using Domain.Entities.Registered_Cars;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Text.Json;

namespace API.Controllers
{
    [Route("api/propietaries")]
    [ApiController]
    public class PropietaryController: ControllerBase
    {
        private readonly IPropietaryServices _propietaryServices;

        public PropietaryController(IPropietaryServices propietaryServices )
        {
            this._propietaryServices = propietaryServices;
        }

        //[Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PagedList<PropietaryDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetPropietaries([FromQuery] PropietaryFilter filter)
        {
            var propietaries = await _propietaryServices.GetPropietaries(filter);

            var metadata = new Metadata()
            {
                TotalCount = propietaries.TotalCount,
                PageSize = propietaries.PageSize,
                CurrentPage = propietaries.CurrentPage,
                TotalPages = propietaries.TotalPages,
                HasNextPage = propietaries.HasNextPage,
                HasPreviousPage = propietaries.HasPreviousPage
            };

            var response = new GenericResponse<IEnumerable<PropietaryDto>>(propietaries)
            {
                Meta = metadata,
                success = true,
                Data = propietaries
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            return Ok(response);
        }


        //[Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<PropietaryDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("GetPropietaryById/{id}")]
        public async Task<IActionResult> GetPropietaryById(int id)
        {
            var result = await _propietaryServices.GetPropietaryById(id);
            if (result.Data == null) { return NotFound($"No existe propietario con el Id {id}"); }
            if (result.success) { return Ok(result); } else { return NotFound(result); }
        }

        //[Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<PropietaryDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [Route("PostPropietary")]
        public async Task<IActionResult> PostPropietary([FromForm] PropietaryRequest propietaryRequest)
        {
            var result = await _propietaryServices.PostPropietary(propietaryRequest);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        //[Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<Propietary>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        [Route("PutPropietary")]
        public async Task<IActionResult> PutPropietary(PropietaryUpdateDto propietaryUpdateDto, int id)
        {
            var result = await _propietaryServices.PutPropietary(propietaryUpdateDto, id);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        //[Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete]
        [Route("DeletePropietary")]
        public async Task<IActionResult> DeletePropietary(int id)
        {
            var result = await _propietaryServices.DeletePropietary(id);
            if (result == null) { return NotFound($"No existe propietario con el Id {id}"); }
            if (result.success) { return Ok(result); }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
