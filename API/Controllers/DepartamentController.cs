using Application.Interfaces;
using Application.Services;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/v0/departament")]
    [ApiController]
    [AllowAnonymous]
    public class DepartamentController : ControllerBase
    {
        private readonly IDepartamentServices _departamentServices;

        public DepartamentController( IDepartamentServices departamentServices) 
        {
            this._departamentServices = departamentServices;
        }

        //GETALL
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(DepartamentDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<DepartamentDto>>> GetAll()
        {
            var users = await _departamentServices.GetDepartamentALL();
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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(DepartamentDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet("{id:int}", Name = "obtenerDepartament")]
        public async Task<ActionResult<DepartamentDto>> Get(int id)
        {
            var entidad = await _departamentServices.GetDepartamentById(id);
            if (entidad.Data == null)
            {
                return NotFound("No existe Departamento con este Id");
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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(DepartamentDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<ActionResult<DepartamentDto>> Post([FromBody] DepartamentRequest departamentRequest)
        {
            var entidad = await _departamentServices.PostDepartament(departamentRequest);
            if (entidad.success)
            {
                return Ok(entidad);
            }
            else
            {
                return BadRequest($"No se pudo agregar el departamento");
            }

        }

        //PUT
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(DepartamentDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        public async Task<ActionResult<DepartamentDto>> PutCompany(int id, [FromBody] DepartamentRequest departamentRequest)
        {

            var entidad = await _departamentServices.PutDepartament(id, departamentRequest);

            if(entidad == null)
            {
                return NotFound($"No existe Departamento con el DepartamentoId {id} para Actualizar");
            }

            if(departamentRequest.CompanyId == null)
            {
                return NotFound();
            }

            if (entidad.success)
            {
                return Ok(entidad);

            }
            else
            {
                return BadRequest();
            }
        }

        //Delete
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(DepartamentDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<DepartamentDto>> DeleteCompany(int id)
        {
            var existe = await _departamentServices.DeleteDepartament(id);
            if (existe == null)
            {
                return NotFound($"No existe Departamento con el Id {id} para borrar");
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
