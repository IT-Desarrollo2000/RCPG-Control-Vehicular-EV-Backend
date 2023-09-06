using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMunicipalitiesServices
    {
        Task<PagedList<MunicipalitiesDto>> GetMunicipalityAll(MunicipalitiesFilter filter);
        Task<GenericResponse<MunicipalitiesDto>> GetMunicipalityById(int id);
        Task<GenericResponse<List<MunicipalitiesDto>>> GetMunicipalityByStateId(int id);
    }
}
