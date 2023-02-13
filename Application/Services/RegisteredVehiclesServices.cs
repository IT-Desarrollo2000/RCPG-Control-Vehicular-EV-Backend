using Application.Extensions;
using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Services
{
    public class RegisteredVehiclesServices : IRegisteredVehiclesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PaginationOptions _paginationOptions;
        private readonly IOptions<BlobContainers> _azureBlobContainers;
        private readonly IBlobStorageService _blobStorageService;

        public RegisteredVehiclesServices(IUnitOfWork unitOfWork, IMapper mapper, IOptions<PaginationOptions> options, IOptions<BlobContainers> azureBlobContainers, IBlobStorageService blobStorageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paginationOptions = options.Value;
            _azureBlobContainers = azureBlobContainers;
            _blobStorageService = blobStorageService;
        }

        public async Task<PagedList<Vehicle>> GetVehicles(VehicleFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            string properties = "VehicleImages,Checklists,AssignedDepartments";
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

            if (filter.FuelCapacity.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.FuelCapacity == filter.FuelCapacity.Value);
                }
                else { Query = p => p.FuelCapacity == filter.FuelCapacity.Value; }
            }

            if (filter.CurrentFuel.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.CurrentFuel == filter.CurrentFuel.Value);
                }
                else { Query = p => p.CurrentFuel == filter.CurrentFuel.Value; }
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

            try
            {
                //Mapear request
                var entity = _mapper.Map<Vehicle>(vehicleRequest);
                entity.VehicleStatus = Domain.Enums.VehicleStatus.ACTIVO;

                //Revisar que existan los departamentos asociados
                foreach (var id in vehicleRequest.DepartmentsToAssign)
                {
                    var department = await _unitOfWork.Departaments.GetById(id);
                    if (department == null)
                    {
                        response.success = false;
                        response.AddError("Not Found", $"No se encontro el departamento con Id {id}", 2);

                        return response;
                    }
                    entity.AssignedDepartments.Add(department);
                }

                //Generar ID de QR
                entity.VehicleQRId = new Guid().ToString() + $"-{entity.Id}";
                entity.InitialKM = vehicleRequest.CurrentKM;

                //Guardar el Vehiculo 
                await _unitOfWork.VehicleRepo.Add(entity);

                //Guardar las fotos
                var images = new List<VehicleImage>();

                foreach (var image in vehicleRequest.Images)
                {
                    //Validar imagenes y Guardar las imagenes en el blobstorage
                    if (image.ContentType.Contains("image"))
                    {
                        //Manipular el nombre de archivo
                        var uploadDate = DateTime.UtcNow;
                        Random rndm = new Random();
                        string FileExtn = System.IO.Path.GetExtension(image.FileName);
                        var filePath = $"{entity.Id}/{uploadDate.Day}{uploadDate.Month}{uploadDate.Year}_{entity.Serial}{rndm.Next(1,1000)}{FileExtn}";
                        var uploadedUrl = await _blobStorageService.UploadFileToBlobAsync(image, _azureBlobContainers.Value.RegisteredCars, filePath);

                        //Agregar la imagen en BD
                        var newImage = new VehicleImage()
                        {
                            FilePath = filePath,
                            FileURL = await _blobStorageService.GetFileUrl(_azureBlobContainers.Value.RegisteredCars, filePath),
                            Vehicle = entity
                        };

                        await _unitOfWork.VehicleImageRepo.Add(newImage);
                        images.Add(newImage);
                    }
                    else
                    {
                        response.success = false;
                        response.AddError("Archivo de Imagen Invalido", "Uno o mas archivos no corresponden a un archivo de Imagen");

                        return response;
                    }
                }

                //Guardar los cambios
                await _unitOfWork.SaveChangesAsync();

                response.success = true;
                var vDto = _mapper.Map<VehiclesDto>(entity);
                vDto.Images.AddRange(images);
                response.Data = vDto;
                return response;

            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);

                return response;
            }

        }

        public async Task<GenericResponse<VehiclesDto>> GetVehicleById(int id)
        {
            GenericResponse<VehiclesDto> response = new GenericResponse<VehiclesDto>();
            var entity = await _unitOfWork.VehicleRepo.Get(filter: a => a.Id == id, includeProperties: "VehicleImages,Checklists,AssignedDepartments");

            var veh = entity.FirstOrDefault();

            var map = _mapper.Map<VehiclesDto>(veh);
            response.success = true;
            response.Data = map;
            return response;
        }

        public async Task<GenericResponse<bool>> DeleteVehicles(int id)
        {
            GenericResponse<bool> response = new GenericResponse<bool>();
            try
            {
                var exp = await _unitOfWork.VehicleRepo.GetById(id);
                if (exp == null) return null;

                //Borrar las fotos del blob
                var photos = await _unitOfWork.VehicleImageRepo.Get(filter: v => v.VehicleId == id);

                foreach (var photo in photos)
                {
                    await _blobStorageService.DeleteFileFromBlobAsync(_azureBlobContainers.Value.RegisteredCars, photo.FilePath);
                    await _unitOfWork.VehicleImageRepo.Delete(photo.Id);
                }

                //Borrar Checklists 
                var checklists = await _unitOfWork.ChecklistRepo.Get(x => x.VehicleId == id);
                foreach (var checklist in checklists)
                {
                    //Buscar reportes de uso
                    var query = await _unitOfWork.VehicleReportUseRepo.Get(x => x.ChecklistId == checklist.Id, includeProperties: "Checklist");
                    
                    foreach(var report in query)
                    {
                        report.Checklist = null;
                        await _unitOfWork.VehicleReportUseRepo.Update(report);
                    }

                    await _unitOfWork.ChecklistRepo.Delete(checklist.Id);
                }

                await _unitOfWork.VehicleRepo.Delete(id);
                await _unitOfWork.SaveChangesAsync();
                response.success = true;
                response.Data = true;
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.Data = false;
                response.AddError("Error", ex.Message, 1);

                return response;
            }

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

            if (vehiclesUpdateRequest.CurrentFuel.HasValue)
            {
                veh.CurrentFuel = vehiclesUpdateRequest.CurrentFuel.Value;
            }

            if (vehiclesUpdateRequest.FuelType.HasValue)
            {
                veh.FuelType = vehiclesUpdateRequest.FuelType.Value;
            }

            if (vehiclesUpdateRequest.VehicleType.HasValue)
            {
                veh.VehicleType = vehiclesUpdateRequest.VehicleType.Value;
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

            if(!string.IsNullOrEmpty(vehiclesUpdateRequest.VehicleObservation))
            {
                veh.VehicleObservation = vehiclesUpdateRequest.VehicleObservation;
            }

            if (vehiclesUpdateRequest.CurrentKM.HasValue)
            {
                veh.CurrentKM = vehiclesUpdateRequest.CurrentKM.Value;
            }

            if (vehiclesUpdateRequest.InitialKM.HasValue)
            {
                veh.InitialKM = vehiclesUpdateRequest.InitialKM.Value;
            }

            await _unitOfWork.VehicleRepo.Update(veh);
            await _unitOfWork.SaveChangesAsync();
            response.success = true;
            response.Data = veh;
            return response;

        }

        public async Task<GenericResponse<VehicleImage>> AddVehicleImage(VehicleImageRequest request, int vehicleId)
        {
            GenericResponse<VehicleImage> response = new GenericResponse<VehicleImage>();

            try
            {
                //Verificar que exista el vehiculo
                var vehicle = await _unitOfWork.VehicleRepo.GetById(vehicleId);
                if (vehicle == null) return null;

                if (request.ImageFile.ContentType.Contains("image"))
                {
                    //Manipular el nombre de archivo
                    var uploadDate = DateTime.UtcNow;
                    Random rndm = new Random();
                    string FileExtn = System.IO.Path.GetExtension(request.ImageFile.FileName);
                    var filePath = $"{vehicleId}/{uploadDate.Day}{uploadDate.Month}{uploadDate.Year}_{vehicle.Serial}{rndm.Next(1, 1000)}{FileExtn}";
                    var uploadedUrl = await _blobStorageService.UploadFileToBlobAsync(request.ImageFile, _azureBlobContainers.Value.RegisteredCars, filePath);

                    //Agregar la imagen en BD
                    var newImage = new VehicleImage()
                    {
                        FilePath = filePath,
                        FileURL = await _blobStorageService.GetFileUrl(_azureBlobContainers.Value.RegisteredCars, filePath),
                        Vehicle = vehicle
                    };

                    await _unitOfWork.VehicleImageRepo.Add(newImage);
                    await _unitOfWork.SaveChangesAsync();

                    response.success = true;
                    response.Data = newImage;

                    return response;
                }
                else
                {
                    response.success = false;
                    response.AddError("Archivo de Imagen Invalido", "Uno o mas archivos no corresponden a un archivo de Imagen");

                    return response;
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);

                return response;
            }

        }

        public async Task<GenericResponse<bool>> DeleteVehicleImage(int VehicleImageId)
        {
            GenericResponse<bool> response = new GenericResponse<bool>();

            try
            {
                //Borrar las fotos del blob
                var photos = await _unitOfWork.VehicleImageRepo.GetById(VehicleImageId);
                if (photos == null) return null;

                await _blobStorageService.DeleteFileFromBlobAsync(_azureBlobContainers.Value.RegisteredCars, photos.FilePath);
                await _unitOfWork.VehicleImageRepo.Delete(photos.Id);
                await _unitOfWork.SaveChangesAsync();

                response.success = true;
                response.Data = true;
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);

                response.AddError("Error", ex.Message, 1);
                return response;

            }
        }

        public async Task<GenericResponse<PerformanceDto>> Performance(PerformanceRequest performanceRequest)
        {
            GenericResponse<PerformanceDto> response = new GenericResponse<PerformanceDto>();
            var entity = await _unitOfWork.VehicleRepo.GetById(performanceRequest.VehicleId);
            if (entity == null)
            {
                response.success = false;
                response.AddError("Not Found", $"No se encontro el vehiculo con el id {performanceRequest.VehicleId}", 2);

                return response;
            }
            decimal fuelCapacity = Convert.ToDecimal(entity.FuelCapacity);

            decimal PerformanceOfVehicle = (performanceRequest.CurrentKm - performanceRequest.PreviousKm) / entity.FuelCapacity;

            PerformanceDto performance = new PerformanceDto();
            performance.PerformanceOfVehicle = PerformanceOfVehicle;
            performance.Vehicle = entity;

            response.Data = performance;
            response.success = true;
            return response;
        }

        public async Task<GenericResponse<List<PerformanceDto>>> PerformanceList(List<PerformanceRequest> performanceRequests)
        {
            GenericResponse<List<PerformanceDto>> response = new GenericResponse<List<PerformanceDto>>();

            var dtolist = new List<PerformanceDto>();
            foreach (var element in performanceRequests)
            {
                var entity = await _unitOfWork.VehicleRepo.GetById(element.VehicleId);
                if (entity == null)
                {
                    response.success = false;
                    response.AddError("Not Found", $"No se encontro el vehiculo con el id {element.VehicleId}", 2);

                    return response;
                }

                decimal fuelCapacity = Convert.ToDecimal(entity.FuelCapacity);

                decimal PerformanceOfVehicle = (element.CurrentKm - element.PreviousKm) / entity.FuelCapacity;

                PerformanceDto performance = new PerformanceDto();
                performance.PerformanceOfVehicle = PerformanceOfVehicle;
                performance.Vehicle = entity;
                dtolist.Add(performance);
            }
            response.Data = dtolist;
            response.success = true;
            return response;
        }
        public async Task<GenericResponse<GraphicsDto>> GetServicesAndWorkshop(int VehicleId)
        {
            GenericResponse<GraphicsDto> response = new GenericResponse<GraphicsDto>();
            var vehicle = await _unitOfWork.VehicleRepo.Get(filter: x => x.Id == VehicleId, includeProperties: "VehicleMaintenances,VehicleServices"); 
            var vehicleresult = vehicle.FirstOrDefault();
            if (vehicle == null)
            {
                response.success = false;
                response.AddError("Not Found", $"No se encontro el vehiculo con id {VehicleId}", 2);

                return response;
            }

            var map = _mapper.Map<GraphicsDto>(vehicleresult);
            response.success = true;
            response.Data = map;
            return response;
        }

        public async Task<GenericResponse<List<GraphicsDto>>> GetServicesAndMaintenanceList(List<int> VehicleId)
        {
            GenericResponse<List<GraphicsDto>> response = new GenericResponse<List<GraphicsDto>>();
            var dtograph = new List<GraphicsDto>();

            foreach (var element in VehicleId)
            {
                var services = await _unitOfWork.VehicleRepo.Get(filter: x => x.Id == element, includeProperties: "VehicleMaintenances,VehicleServices");
                var servicesresult = services.FirstOrDefault();
                if (servicesresult == null)
                {
                    response.success = false;
                    response.AddError("Not Found", $"No se encontro el vehiculo con el id {element}", 2);

                    return response;

                }
                GraphicsDto graphics = new GraphicsDto();
                graphics.Vehicle = servicesresult;
                graphics.VehicleMaintenances = servicesresult.VehicleMaintenances;
                graphics.VehicleServices = servicesresult.VehicleServices;
                dtograph.Add(graphics);     
            }
            
            response.success = true;
            response.Data = dtograph;

            return response;
        }

        public async Task<GenericResponse<List<GetExpensesDto>>> GetExpenses(int VehicleId)
        {
            GenericResponse<List<GetExpensesDto>> response = new GenericResponse<List<GetExpensesDto>>();

            var vehicle = await _unitOfWork.VehicleRepo.Get(filter: x => x.Id == VehicleId, includeProperties: "Expenses,Expenses.TypesOfExpenses,Expenses.VehicleMaintenanceWorkshop"); ///*,Expenses.TypesOfExpenses,Expenses.VehicleMaintenanceWorkshop*/
            var vehicleresult = vehicle.FirstOrDefault();
            if (vehicleresult == null)
            {
                response.success = false;
                response.AddError("Not Found", $"No se encontro el vehiculo con id {VehicleId}", 2);

                return response;
            }

            var map = _mapper.Map<List<GetExpensesDto>>(vehicleresult.Expenses);
            response.success = true;
            response.Data = map;
            return response;
        }

        public async Task<GenericResponse<List<GetExpensesDtoList>>> GetExpensesByCar(List<int> VehicleId)
        {
            GenericResponse<List<GetExpensesDtoList>> response = new GenericResponse<List<GetExpensesDtoList>>();

            var dtolist = new List<GetExpensesDtoList>();
            foreach (var element in VehicleId)
            {
                var vehicle = await _unitOfWork.VehicleRepo.Get(filter: x => x.Id == element, includeProperties: "Expenses,Expenses.TypesOfExpenses,Expenses.VehicleMaintenanceWorkshop");
                var vehicleresult = vehicle.FirstOrDefault();
                if (vehicleresult == null)
                {
                    response.success = false;
                    response.AddError("Not Found", $"No se encontro el vehiculo con id {element}", 2);

                    return response;
                }
                GetExpensesDtoList getExpensesDtoList = new GetExpensesDtoList();
                getExpensesDtoList.Id = vehicleresult.Id;

                var map = _mapper.Map<List<GetExpensesDto>>(vehicleresult.Expenses);
                getExpensesDtoList.expensesDtos = map;


                dtolist.Add(getExpensesDtoList);
            }
            response.success = true;
            response.Data = dtolist;
            return response;
        }
    }
}
