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
    public interface IInvoicesServices
    {
        Task<GenericResponse<InvoicesDto>> AddInvoices(int expensesId, InvoicesRequest invoicesRequest);
        Task<GenericResponse<Invoices>> DeleteInvoices(int id);
        Task<GenericResponse<InvoicesDto>> GetInvoicesById(int id);
        Task<GenericResponse<List<InvoicesDto>>> GetInvoicesExpensesById(int expensesId);
        Task<GenericResponse<InvoicesDto>> PutInvoices(InvoicesUpdate invoicesRequest, int id);
    }
}
