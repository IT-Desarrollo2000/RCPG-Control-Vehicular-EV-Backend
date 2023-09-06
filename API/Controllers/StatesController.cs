using Application.Interfaces;
using Domain.DTOs.Reponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/State")]
    [ApiController]
    [Authorize]
    public class StatesController : ControllerBase
    {
        private readonly IStatesServices _statesServices;

        public StatesController( IStatesServices statesServices)
        {
            _statesServices = statesServices;
        }


        //GETALL
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var State = await _statesServices.GetAllStates();
            if (State.success)
            {
                return Ok(State);
            }
            else
            {
                return BadRequest(State);
            }

        }

        //GETBYID
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var State = await _statesServices.GetStateById(id);
            if (State.Data == null) { return NotFound($"No existe State con este Id {id}"); }
            if (State.success) { return Ok(State); }
            else { return BadRequest(State); }

        }

        //GETBYCOUNTRY
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("GetStateByCountry/{CountryId}")]
        public async Task<IActionResult> GetByCountry(int CountryId)
        {
            var Country = await _statesServices.GetStateByCountry(CountryId);
            if (Country.success) { return Ok(Country); }
            else { return BadRequest(Country); }
        }
    }
}
