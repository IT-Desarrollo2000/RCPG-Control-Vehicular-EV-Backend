using Application.Extensions;
using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.Entities.Registered_Cars;
using Domain.Entities.User_Approvals;
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

        public RegisteredVehiclesServices(IUnitOfWork unitOfWork, IMapper mapper, PaginationOptions paginationOptions)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paginationOptions = paginationOptions;
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

            if(filter.FuelType.HasValue)
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

    }
}
