using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;

namespace Application.Interfaces
{
    public interface IExpensesServices
    {
        Task<GenericResponse<PhotosOfSpending>> AddExpenseAttachment(ExpensePhotoRequest request, int expenseId);
        Task<GenericResponse<bool>> DeleteExpenseAttachment(int expenseImageId);
        Task<GenericResponse<Expenses>> DeleteExpenses(int id);
        Task<PagedList<ExpensesDto>> GetExpenses(ExpensesFilter filter);
        Task<GenericResponse<ExpensesDto>> GetExpensesById(int id);
        Task<GenericResponse<ExpensesDto>> PolicyExpense(PolicyExpenseRequest request);
        Task<GenericResponse<ExpensesDto>> PostExpenses(ExpensesRequest expensesRequest);
        Task<GenericResponse<Expenses>> PutExpenses(ExpenseUpdateRequest expensesRequest, int id);
    }
}
