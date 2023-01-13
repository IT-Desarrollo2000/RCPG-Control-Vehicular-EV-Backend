﻿using Application.Interfaces;
using Application.Services;
using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace API.Controllers
{
    [Route("api/typesOfExpenses")]
    [ApiController]
    public class TypesOfExpensesController:ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITypeOfExpensesServices _typeOfExpensesServices;

        public TypesOfExpensesController(IUnitOfWork unitOfWork, ITypeOfExpensesServices typeOfExpensesServices)
        {
            this._unitOfWork = unitOfWork;
            this._typeOfExpensesServices = typeOfExpensesServices;
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<ExpensesDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetTypesOfExpensesList()
        {
            var result = await _typeOfExpensesServices.GetTypesOfExpensesList();
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<ExpensesDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route ("GetTypesOfExpensesId/{id}")]
        public async Task<IActionResult> GetTypesOfExpensesId(int id)
        {
            var result = await _typeOfExpensesServices.GetTypesOfExpensesId(id);
            if (result.Data == null) { return NotFound($"No existe tipo de gasto con el Id {id}"); }
            if (result.success) { return Ok(result); } else { return NotFound(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<ExpensesDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [Route("CreateTypeOfExpenses")]
        public async Task<IActionResult> CreateTypeOfExpenses(TypesOfExpensesRequest typesOfExpensesRequest)
        {
            var result = await _typeOfExpensesServices.CreateTypeOfExpenses(typesOfExpensesRequest);
            if (result.success) { return Ok(result); } else { return BadRequest(result); }

        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<Expenses>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        [Route("PutTypesOfExpenses")]
        public async Task<IActionResult> PutTypesOfExpenses(TypesOfExpensesRequest typesOfExpensesRequest, int id)
        {
            var result = await _typeOfExpensesServices.PutTypesOfExpenses(typesOfExpensesRequest, id);
            if (result.Data == null) { return NotFound($"No existe tipo de gasto con el Id {id}"); }
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete]
        [Route("DeleteTypeOfExpenses")]
        public async Task<IActionResult> DeleteTypeOfExpenses(int id)
        {
            {
                var result = await _typeOfExpensesServices.DeleteTypeOfExpenses(id);
                if (result == null) { return NotFound($"No existe tipo de gasto con el Id {id}"); }
                if (result.success) { return Ok(result); }
                else
                {
                    return BadRequest(result);
                }
            }
        }
    }
}
