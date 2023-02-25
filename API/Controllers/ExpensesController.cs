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
    [Route("api/expenses")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IExpensesServices _expensesServices;

        public ExpensesController(IMapper mapper, IExpensesServices expensesServices)
        {
            this._mapper = mapper;
            this._expensesServices = expensesServices;
        }

        //[Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PagedList<ExpensesDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetExpenses([FromQuery] ExpensesFilter filter)
        {
            var expenses = await _expensesServices.GetExpenses(filter);

            var metadata = new Metadata()
            {
                TotalCount = expenses.TotalCount,
                PageSize = expenses.PageSize,
                CurrentPage = expenses.CurrentPage,
                TotalPages = expenses.TotalPages,
                HasNextPage = expenses.HasNextPage,
                HasPreviousPage = expenses.HasPreviousPage
            };

            var response = new GenericResponse<IEnumerable<ExpensesDto>>(expenses)
            {
                Meta = metadata,
                success = true,
                Data = expenses
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            return Ok(response);
        }

        //[Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<ExpensesDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("GetExpensesById/{id}")]
        public async Task<IActionResult> GetExpensesById(int id)
        {
            var result = await _expensesServices.GetExpensesById(id);
            if (result.Data == null) { return NotFound($"No existe gasto con el Id {id}"); }
            if (result.success) { return Ok(result); } else { return NotFound(result); }
        }

        //[Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<ExpensesDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [Route("PostExpenses")]
        public async Task<IActionResult> PostExpenses([FromForm] ExpensesRequest expensesRequest) 
        {
            var result = await _expensesServices.PostExpenses(expensesRequest);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        //[Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<Expenses>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        [Route("PutExpenses")]
        public async Task<IActionResult> PutExpenses([FromBody] ExpenseUpdateRequest expensesRequest, int id)
        {
            var result = await _expensesServices.PutExpenses(expensesRequest, id);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        //[Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete]
        [Route("DeleteExpenses")]
        public async Task<IActionResult> DeleteExpenses(int id)
        {
            var result = await _expensesServices.DeleteExpenses(id);
            if (result == null) { return NotFound($"No existe gasto con el Id {id}"); }
            if (result.success) { return Ok(result); }
            else
            {
                return BadRequest(result);
            }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<PhotosOfSpending>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [Route("{expenseId:int}/AddAttachment")]
        public async Task<IActionResult> AddExpenseAttachment(int expenseId, [FromForm] ExpensePhotoRequest request)
        {
            var result = await _expensesServices.AddExpenseAttachment(request, expenseId);
            if (result == null) return NotFound("No se encontro el vehiculo especificado");
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete]
        [Route("DeleteAttachment")]
        public async Task<IActionResult> DeleteExpenseAttachment(int attachmentId)
        {
            var result = await _expensesServices.DeleteExpenseAttachment(attachmentId);
            if (result == null) { return NotFound($"No existe imagen con el Id {attachmentId}"); }
            if (result.success) { return Ok(result); }
            else { return BadRequest(result); }
        }

    }

}
