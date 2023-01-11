using Application.Extensions;
using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ExpensesServices: IExpensesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PaginationOptions _paginationOptions;

        public ExpensesServices(IUnitOfWork unitOfWork, IMapper mapper, IOptions<PaginationOptions> options)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._paginationOptions = options.Value;
        }
        public async Task<GenericResponse<ExpensesDto>> PostExpenses([FromBody] ExpensesRequest expensesRequest)
        {
            GenericResponse<ExpensesDto> response = new GenericResponse<ExpensesDto>();
            var existevehicleid = await _unitOfWork.VehicleRepo.Get(v => v.Id == expensesRequest.VehicleId);
            var resultExpenses = existevehicleid.FirstOrDefault();

            var existetypeOfExpenses = await _unitOfWork.TypesOfExpensesRepo.Get(v => v.Id == expensesRequest.TypesOfExpensesId);
            var resultType = existetypeOfExpenses.FirstOrDefault();

            if (resultExpenses == null)
            {
                response.success = false;
                response.AddError("No existe vehiculo", $"No existe vehiculo con ese id{expensesRequest.VehicleId} solicitado", 1);
                return response;
            }

            if (resultType == null)
            {
                response.success = false;
                response.AddError("No existe el tipo de gastos", $"No existe tipo de gastos con el id{expensesRequest.TypesOfExpensesId} solicitado", 1);
                return response;
            } 

            var entity = _mapper.Map<Expenses>(expensesRequest);
            await _unitOfWork.ExpensesRepo.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            response.success = true;
            var expensesDto = _mapper.Map<ExpensesDto>(entity);
            response.Data = expensesDto;
            return response;
        }

        public async Task<GenericResponse<ExpensesDto>> GetExpensesById(int id)
        {
            GenericResponse<ExpensesDto> response = new GenericResponse<ExpensesDto>();
            var entity = await _unitOfWork.ExpensesRepo.Get(filter: a => a.Id == id, includeProperties: "Vehicle,TypesOfExpenses");
            var check = entity.FirstOrDefault();
            var map = _mapper.Map<ExpensesDto>(check);
            response.success = true;
            response.Data = map;
            return response;
        }

        public async Task<GenericResponse<Expenses>> PutExpenses(ExpensesRequest expensesRequest, int id)
        {

            GenericResponse<Expenses> response = new GenericResponse<Expenses>();
            var result = await _unitOfWork.ExpensesRepo.Get(r => r.Id == id);
            var expenses = result.FirstOrDefault();
            if (expenses == null) return null;
            expenses.Cost = expensesRequest.Cost;
            expenses.ExpenseDate = expensesRequest.ExpenseDate;           
            expenses.MechanicalWorkshop = expensesRequest.MechanicalWorkshop;
            expenses.ERPFolio = expensesRequest.ERPFolio;
 


            await _unitOfWork.ExpensesRepo.Update(expenses);
            await _unitOfWork.SaveChangesAsync();
            response.success = true;
            response.Data = expenses;
            return response;

        }

        public async Task<GenericResponse<Expenses>> DeleteExpenses(int id)
        {
            GenericResponse<Expenses> response = new GenericResponse<Expenses>();
            var exp = await _unitOfWork.ExpensesRepo.GetById(id);
            if (exp == null) return null;
            var exists = await _unitOfWork.ExpensesRepo.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            var expensesdto = _mapper.Map<Expenses>(exp);
            response.success = true;
            response.Data = expensesdto;
            return response;
        }

        public async Task<PagedList<Expenses>> GetExpenses(ExpensesFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            string properties = "";
            IEnumerable<Expenses> expenses = null;
            Expression<Func<Expenses, bool>> Query = null;

            if (filter.CreatedAfterDate.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.CreatedDate >= filter.CreatedAfterDate.Value);
                }
                else { Query = p => p.CreatedDate >= filter.CreatedAfterDate.Value; }
            }

            if (filter.CreatedBeforeDate.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.CreatedDate <= filter.CreatedBeforeDate.Value);
                }
                else { Query = p => p.CreatedDate <= filter.CreatedBeforeDate.Value; }
            }

            if (filter.TypesOfExpensesId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.TypesOfExpensesId == filter.TypesOfExpensesId.Value);
                }
                else { Query = p => p.TypesOfExpensesId == filter.TypesOfExpensesId.Value; }
            }

            if (filter.Cost.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Cost == filter.Cost.Value);
                }
                else { Query = p => p.Cost == filter.Cost.Value; }
            }

            if (filter.ExpenseDate.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.ExpenseDate == filter.ExpenseDate.Value);
                }
                else { Query = p => p.ExpenseDate == filter.ExpenseDate.Value; }
            }

            if (filter.VehicleId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.VehicleId <= filter.VehicleId.Value);
                }
                else { Query = p => p.VehicleId <= filter.VehicleId.Value; }
            }        

            if (!string.IsNullOrEmpty(filter.MechanicalWorkshop))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.MechanicalWorkshop.Contains(filter.MechanicalWorkshop));
                }
                else { Query = p => p.MechanicalWorkshop.Contains(filter.MechanicalWorkshop); }
            }

            if (!string.IsNullOrEmpty(filter.ERPFolio))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.ERPFolio.Contains(filter.ERPFolio));
                }
                else { Query = p => p.ERPFolio.Contains(filter.ERPFolio); }
            }


            if (Query != null)
            {
                expenses = await _unitOfWork.ExpensesRepo.Get(filter: Query, includeProperties: properties);
            }
            else
            {
                expenses = await _unitOfWork.ExpensesRepo.Get(includeProperties: properties);
            }

            var pagedExpenses = PagedList<Expenses>.Create(expenses, filter.PageNumber, filter.PageSize);

            return pagedExpenses;
        }
        
    }   
}
