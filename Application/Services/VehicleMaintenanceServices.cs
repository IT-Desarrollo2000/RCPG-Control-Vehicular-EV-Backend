using Application.Extensions;
using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class VehicleMaintenanceServices : IVehicleMaintenanceService

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PaginationOptions _paginationOptions;

        public VehicleMaintenanceServices(IUnitOfWork unitOfWork, IMapper mapper, IOptions<PaginationOptions> options)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            _paginationOptions = options.Value;
        }

        //GetAll
        public async Task<PagedList<VehicleMaintenance>> GetVehicleMaintenanceAll(VehicleMaintenanceFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            string properties = "";
            IEnumerable<VehicleMaintenance> userApprovals = null;
            Expression<Func<VehicleMaintenance, bool>> Query = null;

            if(!string.IsNullOrEmpty(filter.WhereServiceMaintenance))
            {
                if(Query != null)
                {
                    Query = Query.And(p => p.WhereServiceMaintenance.Contains(filter.WhereServiceMaintenance));
                }
                else { Query = p => p.WhereServiceMaintenance.Contains(filter.WhereServiceMaintenance);}

            }

            if (!string.IsNullOrEmpty(filter.CarryPerson))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.CarryPerson.Contains(filter.CarryPerson));
                }
                else { Query = p => p.CarryPerson.Contains(filter.CarryPerson);}
            }

            if(!string.IsNullOrEmpty(filter.CauseServiceMaintenance))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.CauseServiceMaintenance.Contains(filter.CauseServiceMaintenance));
                }
                else { Query = p => p.CauseServiceMaintenance.Contains(filter.CauseServiceMaintenance);}
            }

            if(filter.VehicleId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.VehicleId >= filter.VehicleId.Value);
                }
                else { Query = p => p.VehicleId >= filter.VehicleId.Value; }
            }

            if (filter.NextServiceMaintenance.HasValue)
            {
                if(Query!= null)
                {
                    Query = Query.And(p => p.NextServiceMaintenance >= filter.NextServiceMaintenance.Value);
                }
                else { Query = p => p.NextServiceMaintenance >= filter.NextServiceMaintenance.Value; }
            }

            if (Query != null)
            {
                userApprovals = await _unitOfWork.VehicleMaintenanceRepo.Get(filter: Query, includeProperties: "Vehicle,VehicleMaintenanceWorkshops");
            }
            else
            {
                userApprovals = await _unitOfWork.VehicleMaintenanceRepo.Get(includeProperties: "Vehicle,VehicleMaintenanceWorkshops");
            }

            var pagedApprovals = PagedList<VehicleMaintenance>.Create(userApprovals, filter.PageNumber, filter.PageSize);

            return pagedApprovals;

        }

        //GETBYID
        public async Task<GenericResponse<VehicleMaintenanceDto>> GetVehicleMaintenanceById(int Id)
        {
            GenericResponse<VehicleMaintenanceDto> response = new GenericResponse<VehicleMaintenanceDto>();
            var profile = await _unitOfWork.VehicleMaintenanceRepo.Get(filter: p => p.Id == Id, includeProperties: "Vehicle,VehicleMaintenanceWorkshops");
            var result = profile.FirstOrDefault();
            var VehicleMaintenanceDTO = _mapper.Map<VehicleMaintenanceDto>(result);
            response.success = true;
            response.Data = VehicleMaintenanceDTO;
            return response;
        }

        //Post
        public async Task<GenericResponse<VehicleMaintenanceDto>> PostVehicleMaintenance([FromBody] VehicleMaintenanceRequest vehicleMaintenanceRequest)
        {
            GenericResponse<VehicleMaintenanceDto> response = new GenericResponse<VehicleMaintenanceDto>();
            var existeVehicleMaintenance = await _unitOfWork.VehicleRepo.Get(c => c.Id == vehicleMaintenanceRequest.VehicleId);
            var resultVehicleMaintenance = existeVehicleMaintenance.FirstOrDefault();

            if(resultVehicleMaintenance == null)
            {
                response.success = false;
                response.AddError("No existe Vehicle", $"No existe Vehiculo con el VehicleId {vehicleMaintenanceRequest.VehicleId} solicitado", 1);
                return response;
            }

            var entidad = _mapper.Map<VehicleMaintenance>(vehicleMaintenanceRequest);
            await _unitOfWork.VehicleMaintenanceRepo.Add(entidad);
            await _unitOfWork.SaveChangesAsync();
            response.success = true;
            var VehicleMaintenanceDTO = _mapper.Map<VehicleMaintenanceDto>(entidad);
            response.Data=VehicleMaintenanceDTO;
            return response;

        }

        //Pull
        public async Task<GenericResponse<VehicleMaintenanceDto>> PutVehicleMaintenance(int Id, [FromBody] VehicleMaintenanceRequest vehicleMaintenanceRequest)
        {
            GenericResponse<VehicleMaintenanceDto> response = new GenericResponse<VehicleMaintenanceDto>();
            var profile = await _unitOfWork.VehicleMaintenanceRepo.Get(p => p.Id == Id);
            var result = profile.FirstOrDefault();
            if(result == null) { return null; }

            var existeVehicleMaintenance = await _unitOfWork.VehicleRepo.Get(c => c.Id == vehicleMaintenanceRequest.VehicleId);
            var resultVehicleMaintenance = existeVehicleMaintenance.FirstOrDefault();

            if (resultVehicleMaintenance == null)
            {
                response.success = false;
                response.AddError("No existe Vehicle", $"No existe Vehiculo con el VehicleId {vehicleMaintenanceRequest.VehicleId} solicitado", 1);
                return response;
            }

            result.WhereServiceMaintenance = vehicleMaintenanceRequest.WhereServiceMaintenance;
            result.CarryPerson = vehicleMaintenanceRequest.CarryPerson;
            result.CauseServiceMaintenance = vehicleMaintenanceRequest.CauseServiceMaintenance;
            result.VehicleId = vehicleMaintenanceRequest.VehicleId;
            result.NextServiceMaintenance = vehicleMaintenanceRequest.NextServiceMaintenance;

            await _unitOfWork.VehicleMaintenanceRepo.Update(result);
            await _unitOfWork.SaveChangesAsync();

            var VehicleMaintenanceDTO = _mapper.Map<VehicleMaintenanceDto>(result);
            response.success = true;
            response.Data = VehicleMaintenanceDTO;

            return response;
        }

        //Delete
        public async Task<GenericResponse<VehicleMaintenanceDto>> DeleteVehicleManintenance(int Id)
        {
            GenericResponse<VehicleMaintenanceDto> response= new GenericResponse<VehicleMaintenanceDto>();
            var entidad = await _unitOfWork.VehicleMaintenanceRepo.Get(filter: p => p.Id == Id);
            var result = entidad.FirstOrDefault();
            if (result == null)
            {
                return null;
            }

            var existe = await _unitOfWork.VehicleMaintenanceRepo.Delete(Id);
            await _unitOfWork.SaveChangesAsync();

            var VehicleMaintenanceDTO = _mapper.Map<VehicleMaintenanceDto>(result);
            response.success = true;
            response.Data = VehicleMaintenanceDTO;

            return response;

        }
    }
}

