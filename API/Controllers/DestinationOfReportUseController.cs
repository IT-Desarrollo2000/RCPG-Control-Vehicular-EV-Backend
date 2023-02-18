using Application.Interfaces;
using Application.Services;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace API.Controllers
{
    [Route("api/destinationOfReportUse")]
    [ApiController]
    public class DestinationOfReportUseController : ControllerBase
    {
        private readonly IDestinationOfReportUseService _destinationOfReportUseService;

        public DestinationOfReportUseController(IDestinationOfReportUseService destinationOfReportUseService) 
        {
            this._destinationOfReportUseService = destinationOfReportUseService;
        }


        //GETALL
        //[Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(DestinationOfReportUseDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        public async Task<ActionResult<List<DestinationOfReportUseDto>>> GetAll()
        {
            var users = await _destinationOfReportUseService.GetDestinationOfReportUseAll();
            if (users.success)
            {
                return Ok(users);
            }
            else
            {
                return BadRequest(users);
            }

        }

        //GETBYID
        //[Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(DestinationOfReportUseDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<DestinationOfReportUseDto>> Get(int id)
        {
            var entidad = await _destinationOfReportUseService.GetDestinationOfReportUseById(id);
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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(DestinationOfReportUseDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<ActionResult<DestinationOfReportUseDto>> Post([FromBody] DestinationOfReportUseRequest destinationOfReportUseRequest)
        {
            var entidad = await _destinationOfReportUseService.PostDestinationOfReportUse(destinationOfReportUseRequest);

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

        //PUT
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(DestinationOfReportUseDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        public async Task<ActionResult<DestinationOfReportUseDto>> PutDestinationOfResultUse(int id, [FromBody] DestinationOfReportUseRequest destinationOfReportUseRequest)
        {

            var entidad = await _destinationOfReportUseService.PutDestinationOfReportUse(id, destinationOfReportUseRequest);

            if (entidad.success)
            {
                return Ok(entidad);

            }
            else
            {
                return BadRequest(entidad);
            }
        }

        //Delete
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(DestinationOfReportUseDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<DestinationOfReportUseDto>> DeleteDestinationOfResultUse(int id)
        {
            var existe = await _destinationOfReportUseService.DeleteDestinationOfReportUse(id);
            if (existe == null)
            {
                return NotFound($"No existe DestinationOfReportUse con el Id {id} para borrar");
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
