using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Text.Json;

namespace API.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/expenses")]
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

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PagedList<Expenses>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("")]
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

            var response = new GenericResponse<IEnumerable<Expenses>>(expenses)
            {
                Meta = metadata,
                success = true,
                Data = expenses
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            return Ok(response);
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<ExpensesDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("GetTypesOfExpensesId/{id}")]
        public async Task<IActionResult> GetExpensesById(int id)
        {
            var result = await _expensesServices.GetExpensesById(id);
            if(result.Data== null) { return NotFound($"No existe gasto con el Id {id}"); }
            if (result.success) { return Ok(result); } else { return NotFound(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<ExpensesDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("PostExpenses")]
        public async Task<IActionResult> PostExpenses([FromBody] ExpensesRequest expensesRequest)
        {
            var result = await _expensesServices.PostExpenses( expensesRequest);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<Expenses>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        [Microsoft.AspNetCore.Mvc.Route("PutExpenses")]
        public async Task<IActionResult> PutExpenses(ExpensesRequest expensesRequest, int id)
        {
            var result = await _expensesServices.PutExpenses(expensesRequest, id);
            if (result.Data == null) { return NotFound($"No existe gasto con el Id {id}"); }
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete]
        [Microsoft.AspNetCore.Mvc.Route("DeleteExpenses")]
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
    }

}
