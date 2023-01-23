using Application.Extensions;
using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
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
    public class MaintenanceWorkshopServices : IMaintenanceWorkshopService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PaginationOptions _paginationOptions;

        public MaintenanceWorkshopServices(IUnitOfWork unitOfWork, IMapper mapper, IOptions<PaginationOptions> options)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            _paginationOptions = options.Value;
        }

        //GETALL
        public async Task<PagedList<VehicleMaintenanceWorkshop>> GetMaintenanceWorkshopAll( MaintenanceWorkshopFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            string properties = "";
            IEnumerable<VehicleMaintenanceWorkshop> userApprovals = null;
            Expression<Func<VehicleMaintenanceWorkshop, bool>> Query = null;

            if(!string.IsNullOrEmpty(filter.Name))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Name.Contains(filter.Name));
                }
                else { Query = p => p.Name.Contains(filter.Name); }
            }

            if (!string.IsNullOrEmpty(filter.Ubication))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Ubication.Contains(filter.Ubication));
                }
                else { Query = p => p.Ubication.Contains(filter.Ubication); }
            }

            if(filter.Latitude.HasValue)
            {
                if(Query != null)
                {
                    Query = Query.And(p => p.Latitude >= filter.Latitude.Value);
                }
                else { Query = p => p.Latitude >= filter.Latitude.Value; }
            }

            if (filter.Longitude.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Longitude >= filter.Longitude.Value);
                }
                else { Query = p => p.Longitude >= filter.Longitude.Value; }
            }

            if (!string.IsNullOrEmpty(filter.Telephone))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Telephone.Contains(filter.Telephone));
                }
                else { Query = p => p.Telephone.Contains(filter.Telephone); }
            }

            if (Query != null)
            {
                userApprovals = await _unitOfWork.MaintenanceWorkshopRepo.Get(filter: Query, includeProperties: "VehicleMaintenances,Expenses");
            }
            else
            {
                userApprovals = await _unitOfWork.MaintenanceWorkshopRepo.Get(includeProperties: "VehicleMaintenances,Expenses");
            }

            var pagedApprovals = PagedList<VehicleMaintenanceWorkshop>.Create(userApprovals, filter.PageNumber, filter.PageSize);

            return pagedApprovals;

        }

        //GETBYID
        public async Task<GenericResponse<MaintenanceWorkshopDto>> GetMaintenanceWorkshopById(int Id)
        {
            GenericResponse<MaintenanceWorkshopDto> response = new GenericResponse<MaintenanceWorkshopDto>();
            var profile = await _unitOfWork.MaintenanceWorkshopRepo.Get(filter: p => p.Id == Id, includeProperties:"VehicleMaintenances,Expenses");
            var result = profile.FirstOrDefault();
            var VehicleMaintenanceDTO = _mapper.Map<MaintenanceWorkshopDto>(result);
            response.success = true;
            response.Data = VehicleMaintenanceDTO;
            return response;
        }

        //Post
        public async Task<GenericResponse<MaintenanceWorkshopDto>> PostMaintenanceWorkshop([FromBody] MaintenanceWorkshopRequest maintenanceWorkshopRequest)
        {
            GenericResponse<MaintenanceWorkshopDto> response = new GenericResponse<MaintenanceWorkshopDto>();
            var entidad = _mapper.Map<VehicleMaintenanceWorkshop>(maintenanceWorkshopRequest);
            await _unitOfWork.MaintenanceWorkshopRepo.Add(entidad);
            await _unitOfWork.SaveChangesAsync();
            response.success = true;
            var MaintenanceWorkshopDTO = _mapper.Map<MaintenanceWorkshopDto>(entidad);
            response.Data = MaintenanceWorkshopDTO;
            return response;

        }

        //Pull
        public async Task<GenericResponse<MaintenanceWorkshopDto>> PutMaintenanceWorkshop(int Id, [FromBody] MaintenanceWorkshopRequest maintenanceWorkshopRequest)
        {
            GenericResponse<MaintenanceWorkshopDto> response = new GenericResponse<MaintenanceWorkshopDto>();
            var profile = await _unitOfWork.MaintenanceWorkshopRepo.Get(p => p.Id == Id);
            var result = profile.FirstOrDefault();
            if (result == null) { return null; }
            result.Name = maintenanceWorkshopRequest.Name;
            result.Ubication = maintenanceWorkshopRequest.Ubication;
            result.Latitude = maintenanceWorkshopRequest.Latitude;
            result.Longitude = maintenanceWorkshopRequest.Longitude;
            result.Telephone = maintenanceWorkshopRequest.Telephone;

            await _unitOfWork.MaintenanceWorkshopRepo.Update(result);
            await _unitOfWork.SaveChangesAsync();
            var MaintenanceWorkshopDTO = _mapper.Map<MaintenanceWorkshopDto>(result);
            response.success = true;
            response.Data = MaintenanceWorkshopDTO;
            return response;

        }

        //Delete
        public async Task<GenericResponse<MaintenanceWorkshopDto>> DeleteMaintenanceWorkshop(int Id)
        {
            GenericResponse<MaintenanceWorkshopDto> response = new GenericResponse<MaintenanceWorkshopDto>();
            var entidad = await _unitOfWork.MaintenanceWorkshopRepo.Get(filter: p => p.Id == Id);
            var result = entidad.FirstOrDefault();
            if (result == null)
            {
                return null;
            }

            var existe = await _unitOfWork.MaintenanceWorkshopRepo.Delete(Id);
            await _unitOfWork.SaveChangesAsync();

            var MaintenanceWorkshopDTO = _mapper.Map<MaintenanceWorkshopDto>(result);
            response.success = true;
            response.Data = MaintenanceWorkshopDTO;

            return response;

        }




    }
}
