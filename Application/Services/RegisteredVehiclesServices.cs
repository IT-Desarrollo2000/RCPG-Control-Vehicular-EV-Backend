using Application.Extensions;
using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Departament;
using Domain.Entities.Registered_Cars;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

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

        public async Task<PagedList<VehiclesDto>> GetVehicles(VehicleFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            string properties = "VehicleImages,Checklists,AssignedDepartments,AssignedDepartments.Company,Policy,PhotosOfCirculationCards";
            IEnumerable<Vehicle> vehicles = null;
            Expression<Func<Vehicle, bool>> Query = null;
            var departament = new Departaments();

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

            if (!string.IsNullOrEmpty(filter.CarRegistrationPlate))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.CarRegistrationPlate.Contains(filter.CarRegistrationPlate));
                }
                else { Query = p => p.CarRegistrationPlate.Contains(filter.CarRegistrationPlate); }
            }

            if (filter.IsClean.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.IsClean == filter.IsClean.Value);
                }
                else { Query = p => p.IsClean == filter.IsClean.Value; }
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

            if (filter.AssignedDepartmentsId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.AssignedDepartments.Select(num => num.Id).FirstOrDefault() == filter.AssignedDepartmentsId.Value);
                }
                else{Query = p => p.AssignedDepartments.Select(num => num.Id).FirstOrDefault() == filter.AssignedDepartmentsId.Value;}
            }


            if (Query != null)
            {
                vehicles = await _unitOfWork.VehicleRepo.Get(filter: Query, includeProperties: properties);
            }
            else
            {
                vehicles = await _unitOfWork.VehicleRepo.Get(includeProperties: properties);
            }

            var dtos = _mapper.Map<IEnumerable<VehiclesDto>>(vehicles);

            var result = PagedList<VehiclesDto>.Create(dtos, filter.PageNumber, filter.PageSize);

            return result;
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
                entity.VehicleQRId = System.Guid.NewGuid().ToString();
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
                        var filePath = $"{entity.Id}/{uploadDate.Day}{uploadDate.Month}{uploadDate.Year}_{entity.Serial}{rndm.Next(1, 1000)}{FileExtn}";
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
                        response.AddError("Archivo de Imagen Invalido", "Uno o mas archivos no corresponden a un archivo de Imagen",3);

                        return response;
                    }
                }

                //Guardar Tarjeta Card
                var Ima = new List<PhotosOfCirculationCard>();
                foreach(var photo in vehicleRequest.CirculationCard)
                {
                    //Validar Imagenes y Guardar las imagenes en el blobstorage
                    if(photo.ContentType.Contains("image"))
                    {
                        //Manipular el nombre del archivo
                        var uploadDate = DateTime.Now;
                        Random rndm = new Random();
                        string FileExtn = System.IO.Path.GetExtension(photo.FileName);
                        var filePath = $"FOTOS_CIRCULATIONCARD/{uploadDate.Day}{uploadDate.Month}{uploadDate.Year}_{uploadDate.Hour}{uploadDate.Minute}{rndm.Next(1, 1000)}{FileExtn}";
                        var uploadedUrl = await _blobStorageService.UploadFileToBlobAsync(photo, _azureBlobContainers.Value.VehicleCirculationCard, filePath);

                        //agregar la imagen a la bd
                        var newImage = new PhotosOfCirculationCard()
                        {
                            FilePath = filePath,
                            FileURL = await _blobStorageService.GetFileUrl(_azureBlobContainers.Value.VehicleCirculationCard, filePath),
                            Vehicle= entity

                        };

                        await _unitOfWork.PhotosOfCirculationCardRepo.Add(newImage);
                        Ima.Add(newImage);

                    }
                    else
                    {
                        response.success = false;
                        response.AddError("Archivo de Imagen Invalido", "Uno o mas archivos no corresponden a un archivo de Imagen", 4);
                        return response;
                    }

                }

                //Guardar los cambios
                await _unitOfWork.SaveChangesAsync();

                response.success = true;
                var vDto = _mapper.Map<VehiclesDto>(entity);
                var imagesDto = _mapper.Map<List<VehicleImageDto>>(images);
                vDto.VehicleImages.AddRange(imagesDto);
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

        public async Task<GenericResponse<VehiclesDto>> GetVehicleByQRId(string qrId)
        {
            GenericResponse<VehiclesDto> response = new GenericResponse<VehiclesDto>();
            try
            {
                var entity = await _unitOfWork.VehicleRepo.Get(filter: a => a.VehicleQRId == qrId, includeProperties: "VehicleImages,Checklists,AssignedDepartments,AssignedDepartments.Company,PhotosOfCirculationCards");
                var veh = entity.FirstOrDefault();

                if(veh == null)
                {
                    response.success = false;
                    response.AddError("Vehiculo no encontrado", $"El vehiculo con Id {qrId} no se encuentra registrado", 2);
                    return response;
                }

                var map = _mapper.Map<VehiclesDto>(veh);
                response.success = true;
                response.Data = map;
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
            var entity = await _unitOfWork.VehicleRepo.Get(filter: a => a.Id == id, includeProperties: "VehicleImages,Checklists,AssignedDepartments,AssignedDepartments.Company,Policy,PhotosOfCirculationCards");

            var veh = entity.FirstOrDefault();

            var map = _mapper.Map<VehiclesDto>(veh);
            response.success = true;
            response.Data = map;
            return response;
        }

        public async Task<GenericResponse<List<VehiclesDto>>> GetVehiclesByDepartment(int departmentId)
        {
            GenericResponse<List<VehiclesDto>> response = new GenericResponse<List<VehiclesDto>>();
            try
            {
                var vehicles = await _unitOfWork.VehicleRepo.Get(v => v.AssignedDepartments.Any(d => d.Id == departmentId));
                var map = _mapper.Map<List<VehiclesDto>>(vehicles);
                response.success = true;
                response.Data = map;

                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        public async Task<GenericResponse<bool>> DeleteVehicles(int id)
        {
            GenericResponse<bool> response = new GenericResponse<bool>();
            try
            {
                var exp = await _unitOfWork.VehicleRepo.GetById(id);
                if (exp == null) return null;

                //Verificar que tenga un estatus preferible
                switch (exp.VehicleStatus)
                {
                    case VehicleStatus.EN_USO:
                    case VehicleStatus.MANTENIMIENTO:
                    case VehicleStatus.APARTADO:
                        response.success = false;
                        response.AddError("Error al eliminar vehiculo", "El estatus del vehiculo no permite su eliminación del sistema", 2);
                        return response;
                    default:
                        break;
                }
                
                //Borrar Checklists 
                var checklists = await _unitOfWork.ChecklistRepo.Get(x => x.VehicleId == id, includeProperties: "InitialCheckListForUseReport,VehicleReportUses");
                foreach (var checklist in checklists)
                {
                    //Buscar reportes de uso
                    var query = await _unitOfWork.VehicleReportUseRepo.Get(x => x.ChecklistId == checklist.Id);
                    foreach (var report in query)
                    {
                        if (checklist.InitialCheckListForUseReport.Contains(report))
                        {
                            checklist.InitialCheckListForUseReport.Remove(report);
                        }

                        if (checklist.VehicleReportUses.Contains(report))
                        {
                            checklist.VehicleReportUses.Remove(report);
                        }

                        await _unitOfWork.ChecklistRepo.Update(checklist);
                        await _unitOfWork.SaveChangesAsync();
                    }
                    await _unitOfWork.ChecklistRepo.Delete(checklist.Id);
                }

                //Borrar los reportes
                var reports = await _unitOfWork.VehicleReportRepo.Get(r => r.VehicleId == id, includeProperties: "Expenses");
                {
                    foreach(var report in reports)
                    {
                        //Vaciar los gastos
                        foreach(var expense in report.Expenses)
                        {
                            expense.VehicleReport = null;
                            await _unitOfWork.ExpensesRepo.Update(expense);
                        }
                        await _unitOfWork.VehicleReportRepo.Delete(report.Id);
                    }

                }

                //Borrar Reportes de Uso
                var useReports = await _unitOfWork.VehicleReportUseRepo.Get(r => r.VehicleId == id, includeProperties: "Destinations");
                foreach (var report in useReports)
                {
                    foreach(var destination in report.Destinations)
                    {
                        await _unitOfWork.DestinationOfReportUseRepo.Delete(destination.Id);
                    }
                    await _unitOfWork.VehicleReportUseRepo.Delete(report.Id);
                }

                //Borrar polizas
                var policies = await _unitOfWork.PolicyRepo.Get(p => p.VehicleId == id);
                foreach(var policy in policies)
                {

                    await _unitOfWork.PolicyRepo.Delete(policy.Id);
                }

                //Borrar las fotos del blob
                var photos = await _unitOfWork.VehicleImageRepo.Get(filter: v => v.VehicleId == id);
                foreach (var photo in photos)
                {
                    await _blobStorageService.DeleteFileFromBlobAsync(_azureBlobContainers.Value.RegisteredCars, photo.FilePath);
                    await _unitOfWork.VehicleImageRepo.Delete(photo.Id);
                }

                //Borrar fotos de vehiculo
                var CirculationCard = await _unitOfWork.PhotosOfCirculationCardRepo.Get(filter: vehicle => vehicle.VehicleId == id);
                foreach (var photo in CirculationCard)
                {
                    await _blobStorageService.DeleteFileFromBlobAsync(_azureBlobContainers.Value.VehicleCirculationCard, photo.FilePath);
                    await _unitOfWork.PhotosOfCirculationCardRepo.Delete(photo.Id);
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

        public async Task<GenericResponse<VehiclesDto>> PutVehicles(VehiclesUpdateRequest request, int id)
        {
            GenericResponse<VehiclesDto> response = new GenericResponse<VehiclesDto>();
            try
            {
                var result = await _unitOfWork.VehicleRepo.Get(r => r.Id == id, includeProperties: "AssignedDepartments");
                var veh = result.FirstOrDefault();
                if (veh == null) return null;

                if (!string.IsNullOrEmpty(request.Name))
                {
                    veh.Name = request.Name;
                }

                if (!string.IsNullOrEmpty(request.Serial))
                {
                    veh.Serial = request.Serial;
                }

                if (request.IsUtilitary.HasValue)
                {
                    veh.IsUtilitary = request.IsUtilitary.Value;
                }

                if (!string.IsNullOrEmpty(request.Brand))
                {
                    veh.Brand = request.Brand;
                }

                if (!string.IsNullOrEmpty(request.Color))
                {
                    veh.Color = request.Color;
                }

                if (request.ModelYear.HasValue)
                {
                    veh.ModelYear = request.ModelYear.Value;
                }

                if (request.FuelCapacity.HasValue)
                {
                    veh.FuelCapacity = request.FuelCapacity.Value;
                }

                if (request.CurrentFuel.HasValue)
                {
                    veh.CurrentFuel = request.CurrentFuel.Value;
                }

                if (request.FuelType.HasValue)
                {
                    veh.FuelType = request.FuelType.Value;
                }

                if (request.VehicleType.HasValue)
                {
                    veh.VehicleType = request.VehicleType.Value;
                }

                if (request.ServicePeriodMonths.HasValue)
                {
                    veh.ServicePeriodMonths = request.ServicePeriodMonths.Value;
                }

                if (request.ServicePeriodKM.HasValue)
                {
                    veh.ServicePeriodKM = request.ServicePeriodKM.Value;
                }

                if (request.OwnershipType.HasValue)
                {
                    veh.OwnershipType = request.OwnershipType.Value;
                }

                if (!string.IsNullOrEmpty(request.OwnersName))
                {
                    veh.OwnersName = request.OwnersName;
                }

                if (request.DesiredPerformance.HasValue)
                {
                    veh.DesiredPerformance = request.DesiredPerformance.Value;
                }

                if (!string.IsNullOrEmpty(request.VehicleObservation))
                {
                    veh.VehicleObservation = request.VehicleObservation;
                }

                if (!string.IsNullOrEmpty(request.CarRegistrationPlate))
                {
                    veh.CarRegistrationPlate = request.CarRegistrationPlate;
                }

                if (request.IsClean.HasValue)
                {
                    veh.IsClean = request.IsClean.Value;
                }

                if (request.CurrentKM.HasValue)
                {
                    veh.CurrentKM = request.CurrentKM.Value;
                }

                if (request.InitialKM.HasValue)
                {
                    veh.InitialKM = request.InitialKM.Value;
                }

                foreach (var department in request.DepartmentsToRemove)
                {
                    var exists = veh.AssignedDepartments.Where(d => d.Id == department).FirstOrDefault();

                    if (exists == null)
                    {
                        response.success = false;
                        response.AddError("Departamento no encontrado", $"El departamento a eliminar con Id {department} no se encontro", 2);
                        return response;
                    }
                    veh.AssignedDepartments.Remove(exists);
                }

                foreach (var department in request.DepartmentsToAdd)
                {
                    var exists = await _unitOfWork.Departaments.GetById(department);
                    if(exists == null)
                    {
                        response.success = false;
                        response.AddError("Departamento no encontrado", $"El departamento para agregar con Id {department} no se encontro", 3);
                        return response;
                    }
                    veh.AssignedDepartments.Add(exists);
                }

                veh.FuelCardNumber = request.FuelCardNumber ?? veh.FuelCardNumber;
                veh.VehicleResponsibleName = request.VehicleResponsibleName ?? veh.VehicleResponsibleName;

                await _unitOfWork.VehicleRepo.Update(veh);
                await _unitOfWork.SaveChangesAsync();
                response.success = true;
                var dto = _mapper.Map<VehiclesDto>(veh);
                response.Data = dto;
                return response;
            }
            catch(Exception ex)
            {
                response.AddError("Error", ex.Message, 1);
                response.success = false;
                return response;
            }

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
                    response.AddError("Archivo de Imagen Invalido", "Uno o mas archivos no corresponden a un archivo de Imagen",2);

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
                return response;

            }
        }

        public async Task<GenericResponse<PhotosOfCirculationCard>> AddCirculationCardImage( CirculationCardRequest circulationCardRequest, int vehicleId)
        {
            GenericResponse<PhotosOfCirculationCard> response = new GenericResponse<PhotosOfCirculationCard>();
            try
            {
                //VERIFICAR quee exista Vehiculo
                var vehicle = await _unitOfWork.VehicleRepo.GetById(vehicleId);
                if (vehicle == null) return null;

                var Ima = new List<PhotosOfCirculationCard>();
                foreach (var image in circulationCardRequest.ImageFile)
                {
                    //Validar Imagenes y Guardar las imagenes en el blobstorage
                    if (image.ContentType.Contains("image"))
                    {
                        //Manipular el nombre del archivo
                        var uploadDate = DateTime.Now;
                        Random rndm = new Random();
                        string FileExtn = System.IO.Path.GetExtension(image.FileName);
                        var filePath = $"FOTOS_CIRCULATIONCARD/{uploadDate.Day}{uploadDate.Month}{uploadDate.Year}_{uploadDate.Hour}{uploadDate.Minute}{rndm.Next(1, 1000)}{FileExtn}";
                        var uploadedUrl = await _blobStorageService.UploadFileToBlobAsync(image, _azureBlobContainers.Value.VehicleCirculationCard, filePath);

                        //agregar la imagen a la bd
                        var newImage = new PhotosOfCirculationCard()
                        {
                            FilePath = filePath,
                            FileURL = await _blobStorageService.GetFileUrl(_azureBlobContainers.Value.VehicleCirculationCard, filePath),
                            Vehicle = vehicle

                        };

                        await _unitOfWork.PhotosOfCirculationCardRepo.Add(newImage);
                        Ima.Add(newImage);
                        await _unitOfWork.SaveChangesAsync();

                        response.success = true;
                        response.Data = newImage;

                    }
                    else
                    {
                        response.success = false;
                        response.AddError("Archivo de Imagen Invalido", "Uno o mas archivos no corresponden a un archivo de Imagen", 2);
                        return response;
                    }


                }

                return response;

            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Archivo de Imagen Invalido", "Uno o mas archivos no corresponden a un archivo de Imagen", 3);

                return response;

            }

        }

        public async Task<GenericResponse<bool>> DeleteCirculationCardImage (int VehicleId)
        {
            GenericResponse<bool> response = new GenericResponse<bool> ();
            try
            {
                //Borrar fotos 
                var vehicle = await _unitOfWork.PhotosOfCirculationCardRepo.Get(filter: vehicle => vehicle.VehicleId == VehicleId);
                if (vehicle == null) return null;

                foreach(var photo in vehicle)
                {
                    await _blobStorageService.DeleteFileFromBlobAsync(_azureBlobContainers.Value.VehicleCirculationCard, photo.FilePath);
                    await _unitOfWork.PhotosOfCirculationCardRepo.Delete(photo.Id);
                    await _unitOfWork.SaveChangesAsync();
                }

                response.success = true;
                response.Data = true;
                return response;

            }
            catch (Exception ex)
            {
                response.success = false;
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

        public async Task<GenericResponse<VehiclesDto>> MarkVehicleAsInactive(int VehicleId)
        {
            GenericResponse<VehiclesDto> response = new GenericResponse<VehiclesDto>();
            try
            {
                var vehicle = await _unitOfWork.VehicleRepo.GetById(VehicleId);
                if (vehicle == null)
                {
                    response.success = false;
                    response.AddError("Vehiculo no encontrado", "El vehiculo especificado no existe", 2);
                    return response;
                }

                //Verificar que pueda su estatus permita su modificación
                if (vehicle.VehicleStatus != VehicleStatus.ACTIVO)
                {
                    response.success = false;
                    response.AddError("Estatus incorrecto", "El estatus del vehiculo no permite que se le de de baja", 3);
                    return response;
                }

                vehicle.VehicleStatus = VehicleStatus.INACTIVO;

                //Guardar cambios
                await _unitOfWork.VehicleRepo.Update(vehicle);
                await _unitOfWork.SaveChangesAsync();

                var map = _mapper.Map<VehiclesDto>(vehicle);
                response.success = true;
                response.Data = map;
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        public async Task<GenericResponse<VehiclesDto>> MarkVehicleAsSaved(int VehicleId)
        {
            GenericResponse<VehiclesDto> response = new GenericResponse<VehiclesDto>();
            try
            {
                var vehicle = await _unitOfWork.VehicleRepo.GetById(VehicleId);
                if (vehicle == null)
                {
                    response.success = false;
                    response.AddError("Vehiculo no encontrado", "El vehiculo especificado no existe", 2);
                    return response;
                }

                //Verificar que pueda su estatus permita su modificación
                if (vehicle.VehicleStatus != VehicleStatus.ACTIVO)
                {
                    response.success = false;
                    response.AddError("Estatus incorrecto", "El estatus del vehiculo no permite que sea apartado", 3);
                    return response;
                }

                vehicle.VehicleStatus = VehicleStatus.APARTADO;

                //Guardar cambios
                await _unitOfWork.VehicleRepo.Update(vehicle);
                await _unitOfWork.SaveChangesAsync();

                var map = _mapper.Map<VehiclesDto>(vehicle);
                response.success = true;
                response.Data = map;
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        public async Task<GenericResponse<VehiclesDto>> ReactivateVehicle(int VehicleId)
        {
            GenericResponse<VehiclesDto> response = new GenericResponse<VehiclesDto>();
            try
            {
                var vehicle = await _unitOfWork.VehicleRepo.GetById(VehicleId);
                if (vehicle == null)
                {
                    response.success = false;
                    response.AddError("Vehiculo no encontrado", "El vehiculo especificado no existe", 2);
                    return response;
                }

                //Verificar que pueda su estatus permita su modificación
                switch (vehicle.VehicleStatus)
                {
                    case VehicleStatus.ACTIVO:
                    case VehicleStatus.EN_USO:
                    case VehicleStatus.MANTENIMIENTO:
                        response.success = false;
                        response.AddError("Estatus incorrecto", "El estatus del vehiculo no permite su cambio de estatus a ACTIVO", 3);
                        return response;
                    default:
                        break;
                }

                vehicle.VehicleStatus = VehicleStatus.ACTIVO;

                //Guardar cambios
                await _unitOfWork.VehicleRepo.Update(vehicle);
                await _unitOfWork.SaveChangesAsync();

                var map = _mapper.Map<VehiclesDto>(vehicle);
                response.success = true;
                response.Data = map;
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        public async Task<GenericResponse<ServicesMaintenanceDto>> GetLatestMaintenanceDto(int vehicleId)
        {
            GenericResponse<ServicesMaintenanceDto> response = new GenericResponse<ServicesMaintenanceDto>();
            try
            {
                var serviceQuery = await _unitOfWork.VehicleServiceRepo.Get(filter: s => s.VehicleId == vehicleId && s.Status == VehicleServiceStatus.FINALIZADO);
                var maintenanceQuery = await _unitOfWork.VehicleMaintenanceRepo.Get(filter: m => m.VehicleId == vehicleId && m.Status == VehicleServiceStatus.FINALIZADO);

                var dto = new ServicesMaintenanceDto()
                {
                    VehicleId = vehicleId
                };

                var lastMan = maintenanceQuery.LastOrDefault();
                var lastServ = serviceQuery.LastOrDefault();

                if(lastMan != null)
                {
                    dto.LastMaintenanceDate = lastMan.MaintenanceDate;
                    dto.LastMaintenanceId = lastMan.Id;
                }

                if(lastServ != null)
                {
                    dto.LastServiceDate = lastServ.CreatedDate;
                    dto.LastServiceId = lastServ.Id;
                }

                response.success = true;
                response.Data = dto;

                return response;
            }
            catch(Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }
    }
}
