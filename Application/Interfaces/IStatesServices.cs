using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IStatesServices
    {
        Task<GenericResponse<List<StatesDto>>> GetAllStates();
        Task<GenericResponse<List<StatesDto>>> GetStateByCountry(int id);
        Task<GenericResponse<StatesDto>> GetStateById(int id);
    }
}
