using Application.Extensions;
using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
using Domain.Entities.User_Approvals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Services
{
    public class VehicleServiServices : IVehicleServiService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PaginationOptions _paginationOptions;

        public VehicleServiServices(IUnitOfWork unitOfWork, IMapper mapper, IOptions<PaginationOptions> options)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            _paginationOptions = options.Value;
        }

        //GetALL
        public async Task<PagedList<VehicleService>> GetVehicleServiceAll(VehicleServiceFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            string properties = "";
            IEnumerable<VehicleService> userApprovals = null;
            Expression<Func<VehicleService, bool>> Query = null;

            if (!string.IsNullOrEmpty(filter.WhereService))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.WhereService.Contains(filter.WhereService));
                }
                else { Query = p => p.WhereService.Contains(filter.WhereService); }
            }

            if (!string.IsNullOrEmpty(filter.CarryPerson))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.CarryPerson.Contains(filter.CarryPerson));
                }
                else { Query = p => p.CarryPerson.Contains(filter.CarryPerson); }
            }

            if (filter.VehicleId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.VehicleId >= filter.VehicleId.Value);
                }
                else { Query = p => p.VehicleId >= filter.VehicleId.Value; }
            }

            if (filter.TypeService.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.TypeService >= filter.TypeService.Value);
                }
                else { Query = p => p.TypeService >= filter.TypeService.Value; }
            }

            if (filter.NextService.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.NextService >= filter.NextService.Value);
                }
                else { Query = p => p.NextService >= filter.NextService.Value; }
            }

            if (Query != null)
            {
                userApprovals = await _unitOfWork.VehicleServiceRepo.Get(filter: Query, includeProperties:"Vehicle");
            }
            else
            {
                userApprovals = await _unitOfWork.VehicleServiceRepo.Get(includeProperties: properties);
            }

            var pagedApprovals = PagedList<VehicleService>.Create(userApprovals, filter.PageNumber, filter.PageSize);

            return pagedApprovals;
        }

        //GETALLBYID
        public async Task<GenericResponse<VehicleServiceDto>> GetVehicleServiceById(int Id)
        {
            GenericResponse<VehicleServiceDto> response = new GenericResponse<VehicleServiceDto>();
            var profile = await _unitOfWork.VehicleServiceRepo.Get(filter: p => p.Id == Id,includeProperties: "Vehicle");
            var result = profile.FirstOrDefault();
            var VehicleServiceDTO = _mapper.Map<VehicleServiceDto>(result);
            response.success = true;
            response.Data = VehicleServiceDTO;
            return response;
        }

        //Post
        public async Task<GenericResponse<VehicleServiceDto>> PostVehicleService([FromBody] VehicleServiceRequest vehicleServiceRequest)
        {
            GenericResponse<VehicleServiceDto> response = new GenericResponse<VehicleServiceDto>();
            var existeVehicle = await _unitOfWork.VehicleRepo.Get(c => c.Id == vehicleServiceRequest.VehicleId);
            var resultVehicle = existeVehicle.FirstOrDefault();

            if (resultVehicle == null)
            {
                response.success = false;
                response.AddError("No existe Vehicle", $"No existe Vehiculo con el VehicleId {vehicleServiceRequest.VehicleId} solicitado", 1);
                return response;
            }


            var entidad = _mapper.Map<VehicleService>(vehicleServiceRequest);
            await _unitOfWork.VehicleServiceRepo.Add(entidad);
            await _unitOfWork.SaveChangesAsync();
            response.success = true;
            var VehicleServiceDTO = _mapper.Map<VehicleServiceDto>(entidad);
            response.Data = VehicleServiceDTO;
            return response;
        }

        //PUT
        public async Task<GenericResponse<VehicleServiceDto>> PutVehicleService(int Id, [FromBody] VehicleServiceRequest vehicleServiceRequest)
        {
            GenericResponse<VehicleServiceDto> response = new GenericResponse<VehicleServiceDto>();
            var profile = await _unitOfWork.VehicleServiceRepo.Get(p => p.Id == Id);
            var result = profile.FirstOrDefault();
            if (result == null) return null;

            var existeVehicle = await _unitOfWork.VehicleRepo.Get(c => c.Id == vehicleServiceRequest.VehicleId);
            var resultVehicle = existeVehicle.FirstOrDefault();

            if (resultVehicle == null)
            {
                response.success = false;
                response.AddError("No existe Vehicle", $"No existe Vehiculo con el VehicleId {vehicleServiceRequest.VehicleId} solicitado", 1);
                return response;
            }

            result.WhereService = vehicleServiceRequest.WhereService;
            result.CarryPerson = vehicleServiceRequest.CarryPerson;
            result.VehicleId = vehicleServiceRequest.VehicleId;
            result.TypeService = vehicleServiceRequest.TypeService;
            result.NextService = vehicleServiceRequest.NextService;

            await _unitOfWork.VehicleServiceRepo.Update(result);
            await _unitOfWork.SaveChangesAsync();

            var VehicleServiceDTO = _mapper.Map<VehicleServiceDto>(result);
            response.success = true;
            response.Data = VehicleServiceDTO;
            return response;

        }

        //DELETE
        public async Task<GenericResponse<VehicleServiceDto>> DeleteVehicleService(int Id)
        {
            GenericResponse<VehicleServiceDto> response = new GenericResponse<VehicleServiceDto>();
            var entidad = await _unitOfWork.VehicleServiceRepo.Get(filter: p => p.Id == Id);
            var result = entidad.FirstOrDefault();
            if (result == null)
            {
                return null;
            }
            var existe = await _unitOfWork.VehicleServiceRepo.Delete(Id);
            await _unitOfWork.SaveChangesAsync();

            var VehicleServiceDTO = _mapper.Map<VehicleServiceDto>(result);
            response.success = true;
            response.Data = VehicleServiceDTO;
            return response;
        }

    }
}
