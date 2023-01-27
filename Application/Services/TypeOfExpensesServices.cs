using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TypeOfExpensesServices: ITypeOfExpensesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TypeOfExpensesServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<GenericResponse<TypesOfExpensesDto>> CreateTypeOfExpenses(TypesOfExpensesRequest typesOfExpensesRequest)
        {
            GenericResponse<TypesOfExpensesDto> response = new GenericResponse<TypesOfExpensesDto>();
            var entity = _mapper.Map<TypesOfExpenses>(typesOfExpensesRequest);
            await _unitOfWork.TypesOfExpensesRepo.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            response.success = true;
            var typesDto = _mapper.Map<TypesOfExpensesDto>(entity);
            response.Data = typesDto;
            return response;
        }
        public async Task<GenericResponse<List<TypesOfExpensesDto>>> GetTypesOfExpensesList()
        {
            GenericResponse<List<TypesOfExpensesDto>> response = new GenericResponse<List<TypesOfExpensesDto>>();
            var tags = await _unitOfWork.TypesOfExpensesRepo.GetAll();
            var prueba = _mapper.Map<List<TypesOfExpensesDto>>(tags);
            response.success = true;
            response.Data = prueba;
            return response;
        }

        public async Task<GenericResponse<TypesOfExpensesDto>> GetTypesOfExpensesId(int id)
        {
            GenericResponse<TypesOfExpensesDto> response = new GenericResponse<TypesOfExpensesDto>();
            var entity = await _unitOfWork.TypesOfExpensesRepo.Get(filter: a => a.Id == id);
     
            var check = entity.FirstOrDefault();
            var map = _mapper.Map<TypesOfExpensesDto>(check);
            response.success = true;
            response.Data = map;
            return response;
        }

        public async Task<GenericResponse<TypesOfExpenses>> PutTypesOfExpenses(TypesOfExpensesRequest typesOfExpensesRequest, int id)
        {

            GenericResponse<TypesOfExpenses> response = new GenericResponse<TypesOfExpenses>();
            var result = await _unitOfWork.TypesOfExpensesRepo.Get(r => r.Id == id);
            var type = result.FirstOrDefault();
            if (type == null) return null;

            type.Name = typesOfExpensesRequest.Name;
            type.Description = typesOfExpensesRequest.Description;

            await _unitOfWork.TypesOfExpensesRepo.Update(type);
            await _unitOfWork.SaveChangesAsync();
            response.success = true;
            response.Data = type;
            return response;

        }

        public async Task<GenericResponse<TypesOfExpenses>> DeleteTypeOfExpenses(int id)
        {
            GenericResponse<TypesOfExpenses> response = new GenericResponse<TypesOfExpenses>();
            var type = await _unitOfWork.TypesOfExpensesRepo.GetById(id);
            if (type == null) return null;
            var exists = await _unitOfWork.TypesOfExpensesRepo.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            var typedto = _mapper.Map<TypesOfExpenses>(type);
            response.success = true;
            response.Data = typedto;
            return response;
        }

    }
}
