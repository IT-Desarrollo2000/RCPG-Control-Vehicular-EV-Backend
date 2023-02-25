using Application.Interfaces;
using AutoMapper;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICompanyServices _companyServices;

        public CompanyController(IMapper _mapper, ICompanyServices companyServices)
        {
            mapper = _mapper;
            this._companyServices = companyServices;
        }

        //GETALL
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CompanyDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        public async Task<ActionResult<List<CompanyDto>>> GetAll()
        {
            var users = await _companyServices.GetCompanyAll();
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
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CompanyDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet("{id:int}", Name = "obtenerGenero")]
        public async Task<ActionResult<CompanyDto>> Get(int id)
        {
            var entidad = await _companyServices.GetCompanyById(id);

            if (entidad.Data == null)
            {
                return NotFound($"No existe compañia con este Id {id}");
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
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CompanyDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<ActionResult<CompanyDto>> Post([FromBody] CompanyRequest companyRequest)
        {
            var entidad = await _companyServices.PostCompany(companyRequest);
            if (entidad.success)
            {
                return Ok(entidad);
            }
            else
            {
                return BadRequest($"No se pudo agregar la empresa");
            }

        }

        //PUT
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CompanyDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        public async Task<ActionResult<CompanyDto>> PutCompany(int id, [FromBody] CompanyRequest companyRequest)
        {
            var entidad = await _companyServices.PutCompany(id, companyRequest);
            if (entidad == null)
            {
                return NotFound($"No existe company con el Id {id} para Actualizar");
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
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CompanyDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<CompanyDto>> DeleteCompany(int id)
        {
            var existe = await _companyServices.DeleteCompany(id);

            if (existe.success)
            {
                return Ok(existe);
            }
            else
            {
                return BadRequest(existe);
            }
        }
    }
}
