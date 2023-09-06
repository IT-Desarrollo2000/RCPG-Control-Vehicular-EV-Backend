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
    public class CountriesServices : ICountriesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CountriesServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //GETALL
        public async Task<GenericResponse<List<CountriesDto>>> GetAllCountries()
        {
            GenericResponse<List<CountriesDto>> response = new GenericResponse<List<CountriesDto>>();
            try
            {
                var entidades = await _unitOfWork.CountriesRepo.Get(includeProperties: "States");
                var dtos = _mapper.Map<List<CountriesDto>>(entidades);
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
        public async Task<GenericResponse<CountriesDto>> GetCountryByID(int id)
        {
            GenericResponse<CountriesDto> response = new GenericResponse<CountriesDto>();
            try
            {
                var entidad = await _unitOfWork.CountriesRepo.Get(filter: p => p.Id == id, includeProperties: "States");
                var result = entidad.FirstOrDefault();
                var CountryDto = _mapper.Map<CountriesDto>(result);
                response.success = true;
                response.Data = CountryDto;
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
