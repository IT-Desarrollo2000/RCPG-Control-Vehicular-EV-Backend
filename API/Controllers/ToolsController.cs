using Application.Interfaces;
using Domain.DTOs.Reponses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/Tools")]
    [ApiController]
    public class ToolsController : ControllerBase
    {
        private readonly IToolsServices _toolsServices;

        public ToolsController(IToolsServices toolsServices)
        {
            this._toolsServices = toolsServices;
        }

        //GETALL
        //[Authorize(Roles = "Supervisor, Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GetVehicleActiveDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("GetAllVehicleActive")]
        public async Task<ActionResult<List<GetVehicleActiveDto>>> GetAll()
        {
            var users = await _toolsServices.GetAllVehiclesActive();
            if (users.success)
            {
                return Ok(users);
            }
            else
            {
                return BadRequest(users);
            }

        }
    }
}
