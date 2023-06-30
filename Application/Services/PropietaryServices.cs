using Application.Extensions;
using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Departament;
using Domain.Entities.Propietary;
using Domain.Entities.Registered_Cars;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PropietaryServices : IPropietaryServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PaginationOptions _paginationOptions;
        public PropietaryServices(IUnitOfWork unitOfWork, IMapper mapper, IOptions<PaginationOptions> options)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._paginationOptions = options.Value;
        }

        public async Task<GenericResponse<PropietaryDto>> PostPropietary(PropietaryRequest propietaryRequest )
        {
            GenericResponse<PropietaryDto> response = new GenericResponse<PropietaryDto>();
            try
            {
                var entity = _mapper.Map<Propietary>(propietaryRequest);
                await _unitOfWork.PropietaryRepo.Add(entity);
                await _unitOfWork.SaveChangesAsync();
                response.success = true;
                var tagsdto = _mapper.Map<PropietaryDto>(entity);
                response.Data = tagsdto;

                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        public async Task<PagedList<PropietaryDto>> GetPropietaries(PropietaryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            string properties = "";
            IEnumerable<Propietary> propietaries = null;
            Expression<Func<Propietary, bool>> Query = null;

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

            if (!string.IsNullOrEmpty(filter.DisplayName))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.DisplayName.Contains(filter.DisplayName));
                }
                else { Query = p => p.DisplayName.Contains(filter.DisplayName); }
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Name.Contains(filter.Name));
                }
                else { Query = p => p.Name.Contains(filter.Name); }
            }

            if (!string.IsNullOrEmpty(filter.SurnameP))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.SurnameP.Contains(filter.SurnameP));
                }
                else { Query = p => p.SurnameP.Contains(filter.SurnameP); }
            }

            if (!string.IsNullOrEmpty(filter.SurnameM))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.SurnameM.Contains(filter.SurnameM));
                }
                else { Query = p => p.SurnameM.Contains(filter.SurnameM); }
            }

            if (!string.IsNullOrEmpty(filter.SurnameM))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.SurnameM.Contains(filter.SurnameM));
                }
                else { Query = p => p.SurnameM.Contains(filter.SurnameM); }
            }

            if (!string.IsNullOrEmpty(filter.CompanyName))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.CompanyName.Contains(filter.CompanyName));
                }
                else { Query = p => p.CompanyName.Contains(filter.CompanyName); }
            }

            if (filter.IsMoralPerson.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.IsMoralPerson == filter.IsMoralPerson.Value);
                }
                else { Query = p => p.IsMoralPerson == filter.IsMoralPerson.Value; }
            }


            if (Query != null)
            {
                propietaries = await _unitOfWork.PropietaryRepo.Get(filter: Query, includeProperties: properties);
            }
            else
            {
                propietaries = await _unitOfWork.PropietaryRepo.Get(includeProperties: properties);
            }

            var dtos = _mapper.Map<IEnumerable<PropietaryDto>>(propietaries);

            var result = PagedList<PropietaryDto>.Create(dtos, filter.PageNumber, filter.PageSize);

            return result;
        }
        public async Task<GenericResponse<PropietaryDto>> GetPropietaryById(int id)
        {
            GenericResponse<PropietaryDto> response = new GenericResponse<PropietaryDto>();
            var entity = await _unitOfWork.PropietaryRepo.Get(filter: a => a.Id == id);
            var check = entity.FirstOrDefault();
            var map = _mapper.Map<PropietaryDto>(check);
            response.success = true;
            response.Data = map;
            return response;
        }

        public async Task<GenericResponse<PropietaryDto>> PutPropietary(PropietaryUpdateDto propietaryUpdateDto, int id)
        {

            GenericResponse<PropietaryDto> response = new GenericResponse<PropietaryDto>();
            try
            {
                var result = await _unitOfWork.PropietaryRepo.Get(r => r.Id == id);
                var check = result.FirstOrDefault();
                if (check == null) return null;

                check.DisplayName = propietaryUpdateDto.DisplayName ?? check.DisplayName;
                check.Name = propietaryUpdateDto.Name ?? check.Name;
                check.SurnameP = propietaryUpdateDto.SurnameP ?? check.SurnameP;
                check.SurnameM = propietaryUpdateDto.SurnameM ?? check.SurnameM;
                check.CompanyName = propietaryUpdateDto.CompanyName ?? check.CompanyName;
                check.IsMoralPerson = propietaryUpdateDto.IsMoralPerson ?? check.IsMoralPerson;


                await _unitOfWork.PropietaryRepo.Update(check);
                await _unitOfWork.SaveChangesAsync();
                var map = _mapper.Map<PropietaryDto>(check);
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

        public async Task<GenericResponse<Propietary>> DeletePropietary(int id)
        {
            GenericResponse<Propietary> response = new GenericResponse<Propietary>();

            try
            {
                var check = await _unitOfWork.PropietaryRepo.GetById(id);
                if (check == null) return null;
                var exists = await _unitOfWork.PropietaryRepo.Delete(id);
                await _unitOfWork.SaveChangesAsync();
                var checkdto = _mapper.Map<Propietary>(check);
                response.success = true;
                response.Data = checkdto;
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
