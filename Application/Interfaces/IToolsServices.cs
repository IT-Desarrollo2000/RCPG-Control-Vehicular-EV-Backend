using Domain.CustomEntities;
using Domain.DTOs.Reponses;

namespace Application.Interfaces
{
    public interface IToolsServices
    {
        Task<GenericResponse<List<GetVehicleActiveDto>>> GetAllVehiclesActive();
    }
}
