using Application.Extensions;
using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.Entities.Registered_Cars;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ImportExportServices : IImportExportServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IOptions<BlobContainers> _azureBlobContainers;
        private readonly IBlobStorageService _blobStorageService;
        private readonly PaginationOptions _paginationOptions;

        public ImportExportServices(IUnitOfWork unitOfWork, IMapper mapper, IOptions<BlobContainers> azureBlobContainers, IBlobStorageService blobStorageService, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _azureBlobContainers = azureBlobContainers;
            _blobStorageService = blobStorageService;
            _paginationOptions = options.Value;
        }

        public async Task<PagedList<VehicleExportDto>> ExportVehiclesData(VehicleExportFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            //var vehicleData = await _unitOfWork.VehicleRepo.Get(includeProperties: "Policy,Propietary,VehicleImages,Checklists,AssignedDepartments,PhotosOfCirculationCards");
            string properties = "Policy,Propietary,VehicleImages,Checklists,AssignedDepartments,PhotosOfCirculationCards";
            IEnumerable<Vehicle> vehicles = null;
            Expression<Func<Vehicle, bool>> Query = null;

            if (filter.CurrentFuel.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.CurrentFuel == filter.CurrentFuel.Value);
                }
                else { Query = p => p.CurrentFuel == filter.CurrentFuel.Value; }
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

            if (filter.FuelType.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.FuelType == filter.FuelType.Value);
                }
                else { Query = p => p.FuelType == filter.FuelType.Value; }
            }

            if (Query != null)
            {
                vehicles = await _unitOfWork.VehicleRepo.Get(filter: Query, includeProperties: properties);
            }
            else
            {
                vehicles = await _unitOfWork.VehicleRepo.Get(includeProperties: properties);
            }

            var dtos = _mapper.Map<List<VehicleExportDto>>(vehicles);

            foreach (var dto in dtos)
            {
                var lastCheckListQ = await _unitOfWork.ChecklistRepo.Get(c => c.VehicleId == dto.Id);
                var lastCheckList = lastCheckListQ.LastOrDefault();
                dto.Checklist = _mapper.Map<ChecklistDto>(lastCheckList);
            }

            var pagedData = PagedList<VehicleExportDto>.Create(dtos, filter.PageNumber, filter.PageSize);

            return pagedData;
        }
    }
}
