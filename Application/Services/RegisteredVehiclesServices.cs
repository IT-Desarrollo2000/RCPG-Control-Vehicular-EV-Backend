using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using System;
using System.Collections.Generic;
using System.Linq;
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

        
    }
}
