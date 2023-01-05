using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ExpensesServices: IExpensesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ExpensesServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<GenericResponse<ExpensesDto>> PostExpenses(int vehicleId, ExpensesRequest expensesRequest)
        {
            GenericResponse<ExpensesDto> response = new GenericResponse<ExpensesDto>();
            var entity = _mapper.Map<Expenses>(expensesRequest);
            entity.VehicleId = vehicleId;

            await _unitOfWork.ExpensesRepo.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            response.success = true;
            var expensesDto = _mapper.Map<ExpensesDto>(entity);
            response.Data = expensesDto;
            return response;
        }


    }
}
