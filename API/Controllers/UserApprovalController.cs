using Application.Interfaces;
using Application.Services;
using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Identity;
using Domain.Entities.User_Approvals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Text.Json;

namespace API.Controllers
{
    [Route("api/userApproval")]
    [ApiController]
    public class UserApprovalController : ControllerBase
    {
        private readonly IUserApprovalServices _approvalServices;

        public UserApprovalController(IUserApprovalServices userApprovalServices)
        {
            _approvalServices = userApprovalServices;
        }

        [Authorize(Roles = "Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<UserApproval>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetApprovals(UserApprovalFilter filter)
        {
            var approval = await _approvalServices.GetApprovals(filter);

            return Ok(approval);
        }

        [Authorize(Roles = "Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PagedList<UserApproval>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetUserProfile([FromQuery]UserApprovalFilter filter)
        {
            var approval = await _approvalServices.GetApprovals(filter);

            var metadata = new Metadata()
            {
                TotalCount = approval.TotalCount,
                PageSize = approval.PageSize,
                CurrentPage = approval.CurrentPage,
                TotalPages = approval.TotalPages,
                HasNextPage = approval.HasNextPage,
                HasPreviousPage = approval.HasPreviousPage
            };

            var response = new GenericResponse<IEnumerable<UserApproval>>(approval)
            {
                Meta = metadata,
                success = true,
                Data = approval
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            return Ok(response);
        }

        [Authorize(Roles = "Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<UserApproval>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateApproval([FromForm]ApprovalCreationRequest request)
        {
            var result = await _approvalServices.CreateApproval(request);

            if (result == null) { return NotFound("No se encontro la solicitud proporcionada"); }

            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        [Authorize(Roles = "Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<UserApproval>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        [Route("Manage")]
        public async Task<IActionResult> ManageApproval([FromBody]ApprovalManagementRequest request)
        {
            var result = await _approvalServices.ManageApproval(request);

            if (result == null) { return NotFound("No se encontro la solicitud proporcionada"); }

            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        [Authorize(Roles = "Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteApproval(int ApprovalId)
        {
            var result = await _approvalServices.DeleteApproval(ApprovalId);

            if(result.success) { return Ok(result); } else { return BadRequest(result); }
        }
    }
}
