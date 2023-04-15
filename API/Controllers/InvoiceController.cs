using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace API.Controllers
{
    [Route("api/invoices")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoicesServices _invoicesServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InvoiceController(IInvoicesServices invoicesServices, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._invoicesServices = invoicesServices;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }


        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<InvoicesDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("GetInvoicesById/{id}")]
        public async Task<IActionResult> GetInvoicesById(int id)
        {
            var result = await _invoicesServices.GetInvoicesById(id);
            if (result.Data == null) { return NotFound($"No existe factura con el Id {id}"); }
            if (result.success) { return Ok(result); } else { return NotFound(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<InvoicesDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("GetInvoicesExpensesById")]
        public async Task<IActionResult> GetInvoicesExpensesById(int expensesId)
        {
            var result = await _invoicesServices.GetInvoicesExpensesById(expensesId);
            if (result.Data == null) { return NotFound($"No existe gasto con el Id {expensesId}"); }
            if (result.success) { return Ok(result); } else { return NotFound(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<InvoicesDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        [Route("AddInvoices")]
        public async Task<IActionResult> AddInvoices(int expensesId, [FromForm] InvoicesRequest invoicesRequest)
        {
            var result = await _invoicesServices.AddInvoices(expensesId, invoicesRequest);

            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete]
        [Route("DeleteInvoices")]
        public async Task<IActionResult> DeleteInvoices(int id)
        {
            var result = await _invoicesServices.DeleteInvoices(id);
            if (result.Data == null) { return NotFound($"No existe factura con el Id {id}"); }
            if (result.success) { return Ok(result); }
            else
            {
                return BadRequest(result);
            }
        }

        [Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<InvoicesDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        [Route("PutInvoices")]
        public async Task<IActionResult> PutInvoices(InvoicesUpdate invoicesRequest, int id)
        {
            var result = await _invoicesServices.PutInvoices(invoicesRequest, id);
            if (result.Data == null) { return NotFound($"No existe factura con el Id {id}"); }
            if (result.success) { return Ok(result); } else { return BadRequest(result); }
        }
    }
}
