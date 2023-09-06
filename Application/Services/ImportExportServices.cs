using Application.Extensions;
using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
using Domain.Enums;
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

            string properties = "Policy,Propietary,AssignedDepartments,Municipalities";
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
                vehicles = await _unitOfWork.VehicleRepo.Get(filter: Query, includeProperties: properties, orderBy: v => v.OrderBy(x => x.ModelYear));
            }
            else
            {
                vehicles = await _unitOfWork.VehicleRepo.Get(includeProperties: properties, orderBy: v => v.OrderBy(x => x.ModelYear));
            }

            var dtos = _mapper.Map<List<VehicleExportDto>>(vehicles);

            foreach (var dto in dtos)
            {
                //Agregar ultima fecha de servicio
                var lastServiceDate = await _unitOfWork.VehicleServiceRepo.Get(x => x.VehicleId == dto.Id);
                if(lastServiceDate.Count() > 0 && lastServiceDate.LastOrDefault().CreatedDate.HasValue)
                {
                    dto.LastServiceDate = DateOnly.FromDateTime(lastServiceDate.LastOrDefault().CreatedDate.Value);
                }
                
                var nextServiceDate = await _unitOfWork.VehicleServiceRepo.Get(x => x.VehicleId == dto.Id);
                
                if (lastServiceDate.Count() > 0 && lastServiceDate.LastOrDefault().NextService.HasValue)
                {
                    dto.NextServiceDate = DateOnly.FromDateTime(lastServiceDate.LastOrDefault().NextService.Value);
                }

                //Agregar departamentos
                var deparments = await _unitOfWork.Departaments.Get(x => x.AssignedVehicles.Any(x => x.Id == dto.Id), includeProperties: "Company");
                if(deparments != null)
                {
                    foreach(var department in deparments)
                    {
                        dto.Departments += $"{department.Company.Name} - {department.Name},";
                    }
                }

                var municipalities = await _unitOfWork.MunicipalitiesRepo.Get(m => m.Vehicles.Any(v => v.Id == dto.Id), includeProperties: "Vehicles,States");
                var municipality = municipalities.LastOrDefault();
                if (municipality != null)
                {
                    dto.Location = $"{municipality.Name}, {municipality.States.Name}";
                }
            }

            var pagedData = PagedList<VehicleExportDto>.Create(dtos, filter.PageNumber, filter.PageSize);

            return pagedData;
        }

        public async Task<PagedList<PolicyExportDto>> ExportVehiclePolicyData(PolicyExportFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            string properties = "Policy,Propietary,VehicleImages,Checklists,AssignedDepartments,PhotosOfCirculationCards";
            IEnumerable<Vehicle> vehicles = null;
            Expression<Func<Vehicle, bool>> Query = null;

            
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

            
            if (Query != null)
            {
                Query = Query.And(p => p.Policy != null);
                vehicles = await _unitOfWork.VehicleRepo.Get(filter: Query, includeProperties: properties);
            }
            else
            {
                vehicles = await _unitOfWork.VehicleRepo.Get(filter: v => v.Policy != null, includeProperties: properties);
            }

            var dtos = new List<PolicyExportDto>();

            switch (filter.StopLight)
            {
                case LicenceExpStopLight.EXPIRADOS:
                    vehicles = vehicles.Where(u => u.Policy.ExpirationDate <= DateTime.UtcNow);
                    break;
                case LicenceExpStopLight.TRES_MESES:
                    vehicles = vehicles.Where(u => (u.Policy.ExpirationDate - DateTime.UtcNow).TotalDays <= 90 && (u.Policy.ExpirationDate - DateTime.UtcNow).TotalDays > 0);
                    break;
                case LicenceExpStopLight.SEIS_MESES:
                    vehicles = vehicles.Where(u => (u.Policy.ExpirationDate - DateTime.UtcNow).TotalDays <= 180 && (u.Policy.ExpirationDate - DateTime.UtcNow).TotalDays > 90);
                    break;
                case LicenceExpStopLight.DOCE_MESES:
                    vehicles = vehicles.Where(u => (u.Policy.ExpirationDate - DateTime.UtcNow).TotalDays > 180);
                    break;
                default:
                    break;
            }

            dtos = _mapper.Map<List<PolicyExportDto>>(vehicles);

            var pagedData = PagedList<PolicyExportDto>.Create(dtos, filter.PageNumber, filter.PageSize);

            return pagedData;
        }

        public async Task<GenericResponse<VehicleImportExportDto>> ImportVehicles(VehicleImportExpertRequest vehicleImportExpertRequest)
        {

            GenericResponse<VehicleImportExportDto> response = new GenericResponse<VehicleImportExportDto>();
            try
            {
                var entity = _mapper.Map<Vehicle>(vehicleImportExpertRequest);
                entity.FuelCapacity = vehicleImportExpertRequest.FuelCapacity ?? 30;
                entity.CurrentFuel = vehicleImportExpertRequest.CurrentFuel ?? CurrentFuel.MEDIUM;
                entity.FuelType = vehicleImportExpertRequest.FuelType ?? FuelType.GASOLINA_REGULAR;
                entity.ServicePeriodMonths = vehicleImportExpertRequest.ServicePeriodMonths ?? 6;
                entity.ServicePeriodKM = vehicleImportExpertRequest.ServicePeriodKM ?? 10000;
                entity.DesiredPerformance = vehicleImportExpertRequest.DesiredPerformance ?? 8;


                var propietary = await _unitOfWork.PropietaryRepo.GetById(vehicleImportExpertRequest.PropietaryId);
                if (propietary == null)
                {
                    response.success = false;
                    response.AddError("Not Found", $"No se encontro el propietario con Id {vehicleImportExpertRequest.PropietaryId}", 2);

                    return response;
                }
                entity.Propietary = propietary;
                

                foreach (var id in vehicleImportExpertRequest.DepartmentsToAssign)
                {
                    var department = await _unitOfWork.Departaments.GetById(id);
                    if (department == null)
                    {
                        response.success = false;
                        response.AddError("Not Found", $"No se encontro el departamento con Id {id}", 3);

                        return response;
                    }
                    entity.AssignedDepartments.Add(department);
                }

                if (!string.IsNullOrEmpty(vehicleImportExpertRequest.PolicyNumber))
                {
                    var newpolicy = _mapper.Map<Policy>(vehicleImportExpertRequest);
                    entity.Policy = newpolicy;
                }

                Checklist newch = new Checklist
                {
                    CirculationCard = true,
                    CarInsurancePolicy = true,
                    HydraulicTires = true,
                    TireRefurmishment = true,
                    JumperCable = true,
                    SecurityDice = true,
                    Extinguisher = true,
                    CarJack = true,
                    CarJackKey = true,
                    ToolBag = true,
                    SafetyTriangle = true

                };
                //Generar ID de QR
                entity.VehicleQRId = System.Guid.NewGuid().ToString();
                entity.InitialKM = vehicleImportExpertRequest.CurrentKM;

                entity.Checklists.Add(newch);
                await _unitOfWork.VehicleRepo.Add(entity);
                await _unitOfWork.SaveChangesAsync();
                response.success = true;
                var VehicleDto = _mapper.Map<VehicleImportExportDto>(entity);
                response.Data = VehicleDto;
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
