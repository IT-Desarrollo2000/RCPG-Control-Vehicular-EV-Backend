using Application.Interfaces;
using AutoMapper;
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
    [Route("api/checklist")]
    [ApiController]
    public class ChecklistController : ControllerBase
    {
        private readonly IChecklistServices _checklistServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ChecklistController(IChecklistServices checklistServices, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._checklistServices = checklistServices;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PagedList<Checklist>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetChecklist([FromQuery] ChecklistFilter filter)
        {
            var checklists = await _checklistServices.GetChecklist(filter);

            var metadata = new Metadata()
            {
                TotalCount = checklists.TotalCount,
                PageSize = checklists.PageSize,
                CurrentPage = checklists.CurrentPage,
                TotalPages = checklists.TotalPages,
                HasNextPage = checklists.HasNextPage,
                HasPreviousPage = checklists.HasPreviousPage
            };

            var response = new GenericResponse<IEnumerable<Checklist>>(checklists)
            {
                Meta = metadata,
                success = true,
                Data = checklists
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            return Ok(response);
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<ChecklistDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("GetChecklistById/{id}")]
        public async Task<IActionResult> GetChecklistById(int id)
        {
            var result = await _checklistServices.GetChecklistById(id);
            if (result.Data == null) { return NotFound($"No existe checklist con el Id {id}"); }
            if (result.success) { return Ok(result); } else { return NotFound(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<ChecklistDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [Route("PostChecklist")]
        public async Task<IActionResult> PostChecklist(int vehicleId, CreationChecklistDto creationChecklistDto)
        {
            var result = await _checklistServices.PostChecklist(vehicleId, creationChecklistDto);
            if (result.Data == null) { return NotFound($"No existe vehiculo con el Id {vehicleId}"); }
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<Checklist>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        [Route("PutChecklists")]
        public async Task<IActionResult> PutChecklists(CreationChecklistDto creationChecklistDto, int id)
        {
            var result = await _checklistServices.PutChecklists(creationChecklistDto, id);
            if (result.Data == null) { return NotFound($"No existe checklist con el Id {id}"); }
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete]
        [Route("DeleteChecklists")]
        public async Task<IActionResult> DeleteChecklists(int id)
        {
            var result = await _checklistServices.DeleteChecklists(id);
            if (result.Data == null) { return NotFound($"No existe checklist con el Id {id}"); }
            if (result.success) { return Ok(result); }
            else
            {
                return BadRequest(result);
            }
        }

    }
}
