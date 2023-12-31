﻿using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;

namespace Application.Services
{
    public class TypeOfExpensesServices : ITypeOfExpensesServices
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
            try
            {
                switch (typesOfExpensesRequest.Name.ToUpper())
                {
                    case "CARGA GASOLINA":
                        response.AddError("Acción Invalida", "Este tipo de gasto es generado por el sistema y no se puede generar", 2);
                        response.success = false;
                        return response;
                    case "MANTENIMIENTO CORRECTIVO":
                        response.AddError("Acción Invalida", "Este tipo de gasto es generado por el sistema y no se puede generar", 2);
                        response.success = false;
                        return response;
                    case "MANTENIMIENTO PREVENTIVO":
                        response.AddError("Acción Invalida", "Este tipo de gasto es generado por el sistema y no se puede generar", 2);
                        response.success = false;
                        return response;
                    case "POLIZAS":
                        response.AddError("Acción Invalida", "Este tipo de gasto es generado por el sistema y no se puede generar", 2);
                        response.success = false;
                        return response;
                    case "PLACAS":
                        response.AddError("Acción Invalida", "Este tipo de gasto es generado por el sistema y no se puede generar", 2);
                        response.success = false;
                        return response;
                    case "TENENCIA":
                        response.AddError("Acción Invalida", "Este tipo de gasto es generado por el sistema y no se puede generar", 2);
                        response.success = false;
                        return response;
                    default:
                        break;
                }

                var entity = _mapper.Map<TypesOfExpenses>(typesOfExpensesRequest);
                await _unitOfWork.TypesOfExpensesRepo.Add(entity);
                await _unitOfWork.SaveChangesAsync();
                response.success = true;
                var typesDto = _mapper.Map<TypesOfExpensesDto>(entity);
                response.Data = typesDto;
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }
        public async Task<GenericResponse<List<TypesOfExpensesDto>>> GetTypesOfExpensesList()
        {
            GenericResponse<List<TypesOfExpensesDto>> response = new GenericResponse<List<TypesOfExpensesDto>>();
            try
            {
                var tags = await _unitOfWork.TypesOfExpensesRepo.Get(orderBy: v => v.OrderBy(x => x.Name));
                var prueba = _mapper.Map<List<TypesOfExpensesDto>>(tags);
                response.success = true;
                response.Data = prueba;
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        public async Task<GenericResponse<TypesOfExpensesDto>> GetTypesOfExpensesId(int id)
        {
            GenericResponse<TypesOfExpensesDto> response = new GenericResponse<TypesOfExpensesDto>();
            try
            {
                var entity = await _unitOfWork.TypesOfExpensesRepo.Get(filter: a => a.Id == id);

                var check = entity.FirstOrDefault();
                var map = _mapper.Map<TypesOfExpensesDto>(check);
                response.success = true;
                response.Data = map;
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        public async Task<GenericResponse<TypesOfExpenses>> PutTypesOfExpenses(TypesOfExpensesRequest typesOfExpensesRequest, int id)
        {

            GenericResponse<TypesOfExpenses> response = new GenericResponse<TypesOfExpenses>();
            try
            {
                var result = await _unitOfWork.TypesOfExpensesRepo.Get(r => r.Id == id);
                var type = result.FirstOrDefault();
                if (type == null) return null;

                switch (type.Name.ToUpper())
                {
                    case "CARGA GASOLINA":
                        response.AddError("Acción Invalida", "Este tipo de gasto es generado por el sistema y no se puede modificar", 2);
                        response.success = false;
                        return response;
                    case "MANTENIMIENTO CORRECTIVO":
                        response.AddError("Acción Invalida", "Este tipo de gasto es generado por el sistema y no se puede modificar", 2);
                        response.success = false;
                        return response;
                    case "MANTENIMIENTO PREVENTIVO":
                        response.AddError("Acción Invalida", "Este tipo de gasto es generado por el sistema y no se puede modificar", 2);
                        response.success = false;
                        return response;
                    case "POLIZAS":
                        response.AddError("Acción Invalida", "Este tipo de gasto es generado por el sistema y no se puede modificar", 2);
                        response.success = false;
                        return response;
                    case "PLACAS":
                        response.AddError("Acción Invalida", "Este tipo de gasto es generado por el sistema y no se puede generar", 2);
                        response.success = false;
                        return response;
                    case "TENENCIA":
                        response.AddError("Acción Invalida", "Este tipo de gasto es generado por el sistema y no se puede generar", 2);
                        response.success = false;
                        return response;
                    default:
                        break;
                }

                type.Name = typesOfExpensesRequest.Name;
                type.Description = typesOfExpensesRequest.Description;

                await _unitOfWork.TypesOfExpensesRepo.Update(type);
                await _unitOfWork.SaveChangesAsync();
                response.success = true;
                response.Data = type;
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        public async Task<GenericResponse<TypesOfExpenses>> DeleteTypeOfExpenses(int id)
        {
            GenericResponse<TypesOfExpenses> response = new GenericResponse<TypesOfExpenses>();
            try
            {
                var type = await _unitOfWork.TypesOfExpensesRepo.GetById(id);
                if (type == null) return null;

                switch (type.Name.ToUpper())
                {
                    case "CARGA GASOLINA":
                        response.AddError("Acción Invalida", "Este tipo de gasto es generado por el sistema y no se puede eliminar", 2);
                        response.success = false;
                        return response;
                    case "MANTENIMIENTO CORRECTIVO":
                        response.AddError("Acción Invalida", "Este tipo de gasto es generado por el sistema y no se puede eliminar", 2);
                        response.success = false;
                        return response;
                    case "MANTENIMIENTO PREVENTIVO":
                        response.AddError("Acción Invalida", "Este tipo de gasto es generado por el sistema y no se puede eliminar", 2);
                        response.success = false;
                        return response;
                    case "POLIZAS":
                        response.AddError("Acción Invalida", "Este tipo de gasto es generado por el sistema y no se puede eliminar", 2);
                        response.success = false;
                        return response;
                    case "PLACAS":
                        response.AddError("Acción Invalida", "Este tipo de gasto es generado por el sistema y no se puede generar", 2);
                        response.success = false;
                        return response;
                    case "TENENCIA":
                        response.AddError("Acción Invalida", "Este tipo de gasto es generado por el sistema y no se puede generar", 2);
                        response.success = false;
                        return response;
                    default:
                        break;
                }

                var exists = await _unitOfWork.TypesOfExpensesRepo.Delete(id);
                await _unitOfWork.SaveChangesAsync();
                var typedto = _mapper.Map<TypesOfExpenses>(type);
                response.success = true;
                response.Data = typedto;
                return response;
            } 
            catch(Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }
    }
}
