using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;

namespace Application.Interfaces
{
    public interface ITypeOfExpensesServices
    {
        Task<GenericResponse<TypesOfExpensesDto>> CreateTypeOfExpenses(TypesOfExpensesRequest typesOfExpensesRequest);
        Task<GenericResponse<TypesOfExpenses>> DeleteTypeOfExpenses(int id);
        Task<GenericResponse<TypesOfExpensesDto>> GetTypesOfExpensesId(int id);
        Task<GenericResponse<List<TypesOfExpensesDto>>> GetTypesOfExpensesList();
        Task<GenericResponse<TypesOfExpenses>> PutTypesOfExpenses(TypesOfExpensesRequest typesOfExpensesRequest, int id);
    }
}
