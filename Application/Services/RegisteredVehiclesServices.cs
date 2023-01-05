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

            return null;
        }
    }
}
