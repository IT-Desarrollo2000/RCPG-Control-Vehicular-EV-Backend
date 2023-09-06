using Application.Interfaces;
using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace API.Controllers
{
    [Route("api/Municipality")]
    [ApiController]
    [Authorize]
    public class MunicipalitiesController : ControllerBase
    {
        private readonly IMunicipalitiesServices _municipalitiesServices;

        public MunicipalitiesController( IMunicipalitiesServices municipalitiesServices)
        {
            _municipalitiesServices = municipalitiesServices;
        }

        //GETALL
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        public async Task<IActionResult> GetMunicpalityAll([FromQuery] MunicipalitiesFilter filter)
        {
            var approval = await _municipalitiesServices.GetMunicipalityAll(filter);
            var metadata = new Metadata()
            {
                TotalCount = approval.TotalCount,
                PageSize = approval.PageSize,
                CurrentPage = approval.CurrentPage,
                TotalPages = approval.TotalPages,
                HasNextPage = approval.HasNextPage,
                HasPreviousPage = approval.HasPreviousPage
            };

            var response = new GenericResponse<IEnumerable<MunicipalitiesDto>>(approval)
            {
                Meta = metadata,
                success = true,
                Data = approval

            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            return Ok(response);
        }

        //GETBYID
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var Municipality = await _municipalitiesServices.GetMunicipalityById(id);
            if (Municipality.Data == null) { return NotFound($"No existe Municipality con este Id {id}"); }
            if (Municipality.success) { return Ok(Municipality); }
            else { return BadRequest(Municipality); }
        }

        //GETBYSTATE
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("GetMunicipalityByState/{StateId}")]
        public async Task<IActionResult> GetByState(int StateId)
        {
            var Municipality = await _municipalitiesServices.GetMunicipalityByStateId(StateId);
            if (Municipality.success) { return Ok(Municipality); }
            else { return BadRequest(Municipality); }
        }
    }
}
