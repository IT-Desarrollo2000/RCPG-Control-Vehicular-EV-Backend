using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/Country")]
    [ApiController]
    [Authorize]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesServices _countriesServices;

        public CountriesController(ICountriesServices countriesServices)
        {
            _countriesServices = countriesServices;
        }

        //GETALL
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Country = await _countriesServices.GetAllCountries();
            if (Country.success)
            {
                return Ok(Country);
            }
            else
            {
                return BadRequest(Country);
            }

        }

        //GETBYID
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var Country = await _countriesServices.GetCountryByID(id);
            if (Country.Data == null) { return NotFound($"No existe Country con este Id {id}"); }
            if (Country.success) { return Ok(Country); }
            else { return BadRequest(Country); }
        }
    }
}
