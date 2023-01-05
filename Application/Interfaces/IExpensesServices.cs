using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IExpensesServices
    {
        Task<GenericResponse<ExpensesDto>> PostExpenses(int vehicleId, ExpensesRequest expensesRequest);
    }
}
