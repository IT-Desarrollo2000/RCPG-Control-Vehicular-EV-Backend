using Application.Interfaces;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
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
        public async Task<IActionResult> GetAll()
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
        public async Task<IActionResult> Get(int id)
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
        public async Task<IActionResult> Post([FromForm] PolicyRequest policyRequest)
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
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        public async Task<IActionResult> UpdatePolicy([FromForm] PolicyUpdateRequest request)
        {
            var entidad = await _policyService.PutPolicy(request);

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
        public async Task<IActionResult> DeletePolicy(int id)
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

        //POST
       // [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PhotosOfPolicy))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("AddPolicyImage")]
        [HttpPost]
        public async Task<IActionResult> AddPolicyImage([FromForm] PolicyImagesRequest policyImagesRequest, int PolicyId)
        {
            var entidad = await _policyService.AddPolicyImage(policyImagesRequest, PolicyId);

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
       //[Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PhotosOfPolicy))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete]
        [Route("DeletePolicyImage")]
        public async Task<IActionResult> DeletePolicyImages(int PolicyId)
        {
            var existe = await _policyService.DeletePolicyImages(PolicyId);

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
