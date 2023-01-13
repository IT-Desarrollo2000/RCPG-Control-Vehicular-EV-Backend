using Application.Extensions;
using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
using Domain.Entities.User_Approvals;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RegisteredVehiclesServices : IRegisteredVehiclesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PaginationOptions _paginationOptions;

        public RegisteredVehiclesServices(IUnitOfWork unitOfWork, IMapper mapper, IOptions<PaginationOptions> options)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._paginationOptions = options.Value;
        }

        public async Task<PagedList<Vehicle>> GetVehicles(VehicleFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            string properties = "";
            IEnumerable<Vehicle> vehicles = null;
            Expression<Func<Vehicle, bool>> Query = null;

            if (filter.CreatedAfter.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.CreatedDate >= filter.CreatedAfter.Value);
                }
                else { Query = p => p.CreatedDate >= filter.CreatedAfter.Value; }
            }

            if (filter.CreatedBefore.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.CreatedDate <= filter.CreatedBefore.Value);
                }
                else { Query = p => p.CreatedDate <= filter.CreatedBefore.Value; }
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Name.Contains(filter.Name));
                }
                else { Query = p => p.Name.Contains(filter.Name); }
            }

            if (filter.IsUtilitary.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.IsUtilitary == filter.IsUtilitary.Value);
                }
                else { Query = p => p.IsUtilitary == filter.IsUtilitary.Value; }
            }

            if (filter.FuelType.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.FuelType == filter.FuelType.Value);
                }
                else { Query = p => p.FuelType == filter.FuelType.Value; }
            }

            if (filter.OwnershipType.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.OwnershipType == filter.OwnershipType.Value);
                }
                else { Query = p => p.OwnershipType == filter.OwnershipType.Value; }
            }

            if (filter.VehicleType.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.VehicleType == filter.VehicleType.Value);
                }
                else { Query = p => p.VehicleType == filter.VehicleType.Value; }
            }

            if (filter.VehicleStatus.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.VehicleStatus == filter.VehicleStatus.Value);
                }
                else { Query = p => p.VehicleStatus == filter.VehicleStatus.Value; }
            }

            if (!string.IsNullOrEmpty(filter.Color))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Color.Contains(filter.Color));
                }
                else { Query = p => p.Color.Contains(filter.Color); }
            }

            if (!string.IsNullOrEmpty(filter.Brand))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Color.Contains(filter.Brand));
                }
                else { Query = p => p.Color.Contains(filter.Brand); }
            }

            if (!string.IsNullOrEmpty(filter.OwnersName))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Color.Contains(filter.OwnersName));
                }
                else { Query = p => p.Color.Contains(filter.OwnersName); }
            }

            if (!string.IsNullOrEmpty(filter.Serial))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Serial == filter.Serial);
                }
                else { Query = p => p.Serial == filter.Serial; }
            }

            if (filter.MinDesiredPerformance.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.DesiredPerformance >= filter.MinDesiredPerformance.Value);
                }
                else { Query = p => p.DesiredPerformance >= filter.MinDesiredPerformance.Value; }
            }

            if (filter.MaxDesiredPerformance.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.DesiredPerformance <= filter.MaxDesiredPerformance.Value);
                }
                else { Query = p => p.DesiredPerformance <= filter.MaxDesiredPerformance.Value; }
            }

            if (Query != null)
            {
                vehicles = await _unitOfWork.VehicleRepo.Get(filter: Query, includeProperties: properties);
            }
            else
            {
                vehicles = await _unitOfWork.VehicleRepo.Get(includeProperties: properties);
            }

            var pagedApprovals = PagedList<Vehicle>.Create(vehicles, filter.PageNumber, filter.PageSize);

            return pagedApprovals;
        }

        public async Task<GenericResponse<VehiclesDto>> AddVehicles(VehicleRequest vehicleRequest)
        {
            GenericResponse<VehiclesDto> response = new GenericResponse<VehiclesDto>();
            var entity = _mapper.Map<Vehicle>(vehicleRequest);
            await _unitOfWork.VehicleRepo.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            response.success = true;
            var vDto = _mapper.Map<VehiclesDto>(entity);
            response.Data = vDto;
            return response;
        }

        public async Task<GenericResponse<VehiclesDto>> GetVehicleById(int id)
        {
            GenericResponse<VehiclesDto> response = new GenericResponse<VehiclesDto>();
            var entity = await _unitOfWork.VehicleRepo.Get(filter: a => a.Id == id);

            var veh = entity.FirstOrDefault();
            var map = _mapper.Map<VehiclesDto>(veh);
            response.success = true;
            response.Data = map;
            return response;
        }

        public async Task<GenericResponse<Vehicle>> DeleteVehicles(int id)
        {
            GenericResponse<Vehicle> response = new GenericResponse<Vehicle>();
            var exp = await _unitOfWork.VehicleRepo.GetById(id);
            if (exp == null) return null;
            var exists = await _unitOfWork.VehicleRepo.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            var vehicledto = _mapper.Map<Vehicle>(exp);
            response.success = true;
            response.Data = vehicledto;
            return response;
        }

        public async Task<GenericResponse<Vehicle>> PutVehicles(VehiclesUpdateRequest vehiclesUpdateRequest, int id)
        {

            GenericResponse<Vehicle> response = new GenericResponse<Vehicle>();
            var result = await _unitOfWork.VehicleRepo.Get(r => r.Id == id);
            var veh = result.FirstOrDefault();
            if (veh == null) return null;         

            if (!string.IsNullOrEmpty(vehiclesUpdateRequest.Name))
            {               
                veh.Name = vehiclesUpdateRequest.Name;
            }

            if (!string.IsNullOrEmpty(vehiclesUpdateRequest.Serial))
            {
                veh.Serial = vehiclesUpdateRequest.Serial;
            }

            if (vehiclesUpdateRequest.IsUtilitary.HasValue)
            {
                veh.IsUtilitary = vehiclesUpdateRequest.IsUtilitary.Value;
            }

            if (!string.IsNullOrEmpty(vehiclesUpdateRequest.Brand))
            {
                veh.Brand = vehiclesUpdateRequest.Brand;
            }


            if (!string.IsNullOrEmpty(vehiclesUpdateRequest.Color))
            {
                veh.Color = vehiclesUpdateRequest.Color;
            }

            if (vehiclesUpdateRequest.ModelYear.HasValue)
            {
                veh.ModelYear = vehiclesUpdateRequest.ModelYear.Value;
            }

            if (vehiclesUpdateRequest.FuelCapacity.HasValue)
            {
                veh.FuelCapacity = vehiclesUpdateRequest.FuelCapacity.Value;
            }

            if (vehiclesUpdateRequest.FuelType.HasValue)
            {
                veh.FuelType = vehiclesUpdateRequest.FuelType.Value;
            }

            if (vehiclesUpdateRequest.VehicleType.HasValue)
            {
                veh.VehicleType = vehiclesUpdateRequest.VehicleType.Value;
            }

            if (vehiclesUpdateRequest.VehicleStatus.HasValue)
            {
                veh.VehicleStatus = vehiclesUpdateRequest.VehicleStatus.Value;
            }

            if (vehiclesUpdateRequest.ServicePeriodMonths.HasValue)
            {
                veh.ServicePeriodMonths = vehiclesUpdateRequest.ServicePeriodMonths.Value;
            }

            if (vehiclesUpdateRequest.ServicePeriodKM.HasValue)
            {
                veh.ServicePeriodKM = vehiclesUpdateRequest.ServicePeriodKM.Value;
            }

            if (vehiclesUpdateRequest.OwnershipType.HasValue)
            {
                veh.OwnershipType = vehiclesUpdateRequest.OwnershipType.Value;
            }


            if (!string.IsNullOrEmpty(vehiclesUpdateRequest.OwnersName))
            {
                veh.OwnersName = vehiclesUpdateRequest.OwnersName;
            }

            if (vehiclesUpdateRequest.DesiredPerformance.HasValue)
            {
                veh.DesiredPerformance = vehiclesUpdateRequest.DesiredPerformance.Value;
            }

            await _unitOfWork.VehicleRepo.Update(veh);
            await _unitOfWork.SaveChangesAsync();
            response.success = true;
            response.Data = veh;
            return response;

        }
    }
}
