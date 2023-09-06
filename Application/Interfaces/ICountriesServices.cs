using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICountriesServices
    {
        Task<GenericResponse<List<CountriesDto>>> GetAllCountries();
        Task<GenericResponse<CountriesDto>> GetCountryByID(int id);
    }
}
