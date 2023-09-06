using Application.Extensions;
using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.Entities.Municipality;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MunicipalitiesServices : IMunicipalitiesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PaginationOptions _paginationOptions;

        public MunicipalitiesServices(IUnitOfWork unitOfWork, IMapper mapper, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paginationOptions = options.Value;
        }
        //GETALL
        public async Task<PagedList<MunicipalitiesDto>> GetMunicipalityAll(MunicipalitiesFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            string properties = "States";
            IEnumerable<Municipalities> userApprovals = null;
            Expression<Func<Municipalities, bool>> Query = null;

            if (filter.Id.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Id == filter.Id.Value);
                }
                else
                {
                    Query = p => p.Id == filter.Id.Value;
                }
            }

            if (!string.IsNullOrEmpty(filter.name))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Name.Contains(filter.name));
                }
                else
                {
                    Query = p => p.Name.Contains(filter.name);
                }
            }

            if (filter.StateId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.StateId == filter.StateId.Value);
                }
                else
                {
                    Query = p => p.StateId == filter.StateId.Value;
                }
            }

            if (Query != null)
            {
                userApprovals = await _unitOfWork.MunicipalitiesRepo.Get(filter: Query, includeProperties: properties);
            }
            else
            {
                userApprovals = await _unitOfWork.MunicipalitiesRepo.Get(includeProperties: properties);
            }

            var dtos = _mapper.Map<IEnumerable<MunicipalitiesDto>>(userApprovals);

            var pagedApprovals = PagedList<MunicipalitiesDto>.Create(dtos, filter.PageNumber, filter.PageSize);

            return pagedApprovals;

        }

        //GETBYID
        public async Task<GenericResponse<MunicipalitiesDto>> GetMunicipalityById(int id)
        {
            GenericResponse<MunicipalitiesDto> response = new GenericResponse<MunicipalitiesDto>();
            try
            {
                var entidad = await _unitOfWork.MunicipalitiesRepo.Get(filter: p => p.Id == id, includeProperties: "States,Vehicles");
                var result = entidad.FirstOrDefault();
                var MunicipalityDto = _mapper.Map<MunicipalitiesDto>(result);
                response.success = true;
                response.Data = MunicipalityDto;
                return response;

            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        //GETBYIDState
        public async Task<GenericResponse<List<MunicipalitiesDto>>> GetMunicipalityByStateId(int id)
        {
            GenericResponse<List<MunicipalitiesDto>> response = new GenericResponse<List<MunicipalitiesDto>>();
            try
            {
                var entidad = await _unitOfWork.MunicipalitiesRepo.Get(filter: b => b.States.Id == id, includeProperties: "States");
                if (entidad.Count() == 0)
                {
                    response.success = false;
                    response.AddError("No existe estado con ese Id", $"No existe estado con el {id} solicitado");
                    return response;
                }

                var MunicipalityDto = _mapper.Map<List<MunicipalitiesDto>>(entidad);
                response.success = true;
                response.Data = MunicipalityDto;
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
