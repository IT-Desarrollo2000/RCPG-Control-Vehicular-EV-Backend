using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
