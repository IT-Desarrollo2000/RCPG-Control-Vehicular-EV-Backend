using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class StatesServices : IStatesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StatesServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //GETALL
        public async Task<GenericResponse<List<StatesDto>>> GetAllStates()
        {
            GenericResponse<List<StatesDto>> response = new GenericResponse<List<StatesDto>>();
            try
            {
                var entidades = await _unitOfWork.StatesRepo.Get(includeProperties: "Countries,Municipalities");
                var dtos = _mapper.Map<List<StatesDto>>(entidades);
                response.success = true;
                response.Data = dtos;
                return response;

            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        //GETBYID
        public async Task<GenericResponse<StatesDto>> GetStateById(int id)
        {
            GenericResponse<StatesDto> response = new GenericResponse<StatesDto>();
            try
            {
                var entidad = await _unitOfWork.StatesRepo.Get(filter: p => p.Id == id, includeProperties: "Countries,Municipalities");
                var result = entidad.FirstOrDefault();
                var StateDto = _mapper.Map<StatesDto>(result);
                response.success = true;
                response.Data = StateDto;
                return response;

            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        //GetByCountry
        public async Task<GenericResponse<List<StatesDto>>> GetStateByCountry(int id)
        {
            GenericResponse<List<StatesDto>> response = new GenericResponse<List<StatesDto>>();
            try
            {
                var entidad = await _unitOfWork.StatesRepo.Get(filter: p => p.Countries.Id == id, includeProperties: "Countries");
                if (entidad.Count() == 0)
                {
                    response.success = false;
                    response.AddError("No existe Pais con ese Id", $"No existe Pais con el {id} solicitado");
                    return response;
                }

                var StateDto = _mapper.Map<List<StatesDto>>(entidad);
                response.success = true;
                response.Data = StateDto;
                return response;


            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }

        }
    }
}
