using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Profiles;
using Microsoft.AspNetCore.Mvc;

namespace Application.Interfaces
{
    public interface IDepartamentServices
    {
        Task<GenericResponse<DepartamentDto>> DeleteDepartament(int id);
        Task<GenericResponse<List<DepartamentDto>>> DepartmentsBySupervisor(int supervisorId);
        Task<GenericResponse<List<UserProfile>>> GetDeparmentUsers(int id);
        Task<GenericResponse<List<DepartamentDto>>> GetDepartamentALL();
        Task<GenericResponse<DepartamentDto>> GetDepartamentById(int Id);
        Task<GenericResponse<DepartamentDto>> PostDepartament([FromBody] DepartamentRequest departamentRequest);
        Task<GenericResponse<DepartamentDto>> PutDepartament(int id, [FromBody] DepartamentRequest departamentRequest);
    }
}
