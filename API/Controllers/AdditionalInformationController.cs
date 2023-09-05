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
    [Route("api/AdditionalInformation")]
    [ApiController]
    public class AdditionalInformationController:ControllerBase
    {
        private readonly IAdditionalInformationService _additionalInformationService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdditionalInformationController(IAdditionalInformationService additionalInformationService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._additionalInformationService = additionalInformationService;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        //[Authorize(Roles = "")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PagedList<AdditionalInformation>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAdditionalInformation([FromQuery] AdditionalInformationFilter filter)
        {
            var additionalInformation = await _additionalInformationService.GetAdditionalInformation(filter);

            var metadata = new Metadata()
            {
                TotalCount = additionalInformation.TotalCount,
                PageSize = additionalInformation.PageSize,
                CurrentPage = additionalInformation.CurrentPage,
                TotalPages = additionalInformation.TotalPages,
                HasNextPage = additionalInformation.HasNextPage,
                HasPreviousPage = additionalInformation.HasPreviousPage
            };

            var response = new GenericResponse<IEnumerable<AdditionalInformation>>(additionalInformation)
            {
                Meta = metadata,
                success = true,
                Data = additionalInformation   };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            return Ok(response);
        }

        //[Authorize(Roles = "")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<AdditionalInformationDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("GetAdditionalInformationById/{id}")]
        public async Task<IActionResult> GetAdditionalInformationById(int id)
        {
            var result = await _additionalInformationService.GetAdditionalInformationById(id);
            if (result.Data == null) { return NotFound($"No existe informacion adicional con el Id {id}"); }
            if (result.success) { return Ok(result); } else { return NotFound(result); }
        }

        //[Authorize(Roles = "")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<AdditionalInformationDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [Route("PostAdditionalInformation")]
        public async Task<IActionResult> PostAdditionalInformation([FromForm] AdditionalInformationRequest additionalInformation )
        {
            var result = await _additionalInformationService.PostAdditionalInformation(additionalInformation);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        //[Authorize(Roles = "")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<AdditionalInformationDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        [Route("PutAditionalInformation")]
        public async Task<IActionResult> PutAditionalInformation([FromForm] AdditionalInformationUpdateDto additionalInformationUpdateDto, int id)
        {
            var result = await _additionalInformationService.PutAditionalInformation(additionalInformationUpdateDto, id);
            if (result.Data == null) { return NotFound($"No existe informacion adicional con el Id {id}"); }
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        //[Authorize(Roles = "")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete]
        [Route("DeleteAdditionalInformation")]
        public async Task<IActionResult> DeleteAdditionalInformation(int id)
        {
            var result = await _additionalInformationService.DeleteAdditionalInformation(id);
            if (result.Data == null) { return NotFound($"No existe informacion adicional con el Id {id}"); }
            if (result.success) { return Ok(result); }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
