using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
using Domain.Entities.User_Approvals;
using Microsoft.AspNetCore.Mvc;
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


        [HttpGet]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChecklistById(int id)
        {
            var result = await _checklistServices.GetChecklistById(id);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        [HttpPost]
        public async Task<IActionResult> PostChecklist(int vehicleId, CreationChecklistDto creationChecklistDto)
        {
            var result = await _checklistServices.PostChecklist(vehicleId, creationChecklistDto);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        //[HttpPut] 
        //public async Task<IActionResult> PutChecklists(CreationChecklistDto creationChecklistDto, int id)
        //{

        //}

        [HttpDelete]
        public async Task<IActionResult> DeleteChecklists(int id)
        {
            var result = await _checklistServices.DeleteChecklists(id);
            if (result == null) { return NotFound(); }
            if (result.success) { return Ok(result); }
            else
            {
                return BadRequest(result);
            }
        }

    }
}
