using Application.Interfaces;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{

    [Route("api/Policy")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService _policyService;

        public PolicyController(IPolicyService policyService)
        {
            this._policyService = policyService;
        }

        //GETALL
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PolicyDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        public async Task<ActionResult<List<PolicyDto>>> GetAll()
        {
            var users = await _policyService.GetPolicyAll();
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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PolicyDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PolicyDto>> Get(int id)
        {
            var entidad = await _policyService.GetPolicyById(id);
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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PolicyDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<ActionResult<PolicyDto>> Post([FromBody] PolicyRequest policyRequest)
        {
            var entidad = await _policyService.PostPolicy(policyRequest);

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
        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PolicyDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        public async Task<ActionResult<PolicyDto>> PutDestinationOfResultUse(int id, [FromBody] PolicyRequest policyRequest)
        {

            var entidad = await _policyService.PutPolicy(id, policyRequest);

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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PolicyDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<PolicyDto>> DeletePolicy(int id)
        {
            var existe = await _policyService.DeletePolicy(id);

            if (existe.success)
            {
                return Ok(existe);
            }
            else
            {
                return NotFound(existe);
            }

        }
    }
}
