using Application.Extensions;
using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Identity;
using Domain.Entities.Registered_Cars;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace Application.Services
{
    public class VehicleReportServices : IVehicleReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PaginationOptions _paginationOptions;
        private readonly UserManager<AppUser> _userManager;
        private readonly IOptions<BlobContainers> _azureBlobContainers;
        private readonly IBlobStorageService _blobStorageService;
        private readonly IExpensesServices _expensesServices;

        public VehicleReportServices(IUnitOfWork unitOfWork, IMapper mapper, IOptions<PaginationOptions> options, UserManager<AppUser> userManager, IOptions<BlobContainers> azureBlobContainers, IBlobStorageService blobStorageService, IExpensesServices expensesServices)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            _paginationOptions = options.Value;
            _userManager = userManager;
            _azureBlobContainers = azureBlobContainers;
            _blobStorageService = blobStorageService;
            _expensesServices = expensesServices;
        }

        //GetALL
        public async Task<PagedList<VehicleReportDto>> GetVehicleReportAll(VehicleReportFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            string properties = "Vehicle,MobileUser,AdminUser,VehicleReportImages,Expenses,VehicleReportUses,SolvedByAdminUser,Expenses.TypesOfExpenses";
            IEnumerable<VehicleReport> userApprovals = null;
            Expression<Func<VehicleReport, bool>> Query = null;

            if (filter.ReportType.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.ReportType >= filter.ReportType.Value);
                }
                else { Query = p => p.ReportType >= filter.ReportType.Value; }
            }

            if (filter.VehicleId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.VehicleId >= filter.VehicleId.Value);
                }
                else { Query = p => p.VehicleId >= filter.VehicleId.Value; }
            }


            if (!string.IsNullOrEmpty(filter.Commentary))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Commentary.Contains(filter.Commentary));
                }
                else { Query = p => p.Commentary.Contains(filter.Commentary); }
            }

            if (filter.MobileUserId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.MobileUserId >= filter.MobileUserId.Value);
                }
                else { Query = p => p.MobileUserId >= filter.MobileUserId.Value; }
            }

            if (filter.AdminUserId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.AdminUserId >= filter.AdminUserId.Value);
                }
                else { Query = p => p.AdminUserId >= filter.AdminUserId.Value; }
            }

            if (filter.ReportDate.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.ReportDate >= filter.ReportDate.Value);
                }
                else { Query = p => p.ReportDate >= filter.ReportDate.Value; }
            }

            if (filter.IsResolved.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.IsResolved == filter.IsResolved.Value);
                }
                else { Query = p => p.IsResolved == filter.IsResolved.Value; }
            }

            if (filter.GasolineLoadAmount.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.VehicleId >= filter.VehicleId.Value);
                }
                else { Query = p => p.VehicleId >= filter.VehicleId.Value; }
            }

            if (filter.GasolineCurrentKM.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.GasolineCurrentKM >= filter.GasolineCurrentKM.Value);
                }
                else { Query = p => p.GasolineCurrentKM >= filter.GasolineCurrentKM.Value; }
            }

            if (!string.IsNullOrEmpty(filter.ReportSolutionComment))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.ReportSolutionComment.Contains(filter.ReportSolutionComment));
                }
                else { Query = p => p.ReportSolutionComment.Contains(filter.ReportSolutionComment); }
            }

            if (filter.ReportStatus.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.ReportStatus >= filter.ReportStatus.Value);
                }
                else { Query = p => p.ReportStatus >= filter.ReportStatus.Value; }
            }

            if (filter.VehicleReportUseId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.VehicleReportUseId >= filter.VehicleReportUseId.Value);
                }
                else { Query = p => p.VehicleReportUseId >= filter.VehicleReportUseId.Value; }
            }

            if (filter.SolvedByAdminUserId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.SolvedByAdminUserId >= filter.SolvedByAdminUserId.Value);
                }
                else { Query = p => p.SolvedByAdminUserId >= filter.SolvedByAdminUserId.Value; }
            }

            if (filter.AmountGasoline.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.AmountGasoline >= filter.AmountGasoline.Value);
                }
                else { Query = p => p.AmountGasoline >= filter.AmountGasoline.Value; }
            }


            if (Query != null)
            {
                userApprovals = await _unitOfWork.VehicleReportRepo.Get(filter: Query, includeProperties: properties);
            }
            else
            {
                userApprovals = await _unitOfWork.VehicleReportRepo.Get(includeProperties: properties);
            }

            var dtos = _mapper.Map<IEnumerable<VehicleReportDto>>(userApprovals);

            var pagedApprovals = PagedList<VehicleReportDto>.Create(dtos, filter.PageNumber, filter.PageSize);

            return pagedApprovals;
        }

        //GETBYID
        public async Task<GenericResponse<VehicleReportDto>> GetVehicleReportById(int Id)
        {
            GenericResponse<VehicleReportDto> response = new GenericResponse<VehicleReportDto>();
            var profile = await _unitOfWork.VehicleReportRepo.Get(filter: p => p.Id == Id, includeProperties: "Vehicle,MobileUser,AdminUser,VehicleReportImages,Expenses,VehicleReportUses,SolvedByAdminUser,Expenses.TypesOfExpenses");
            var result = profile.FirstOrDefault();
            var VehicleReportDto = _mapper.Map<VehicleReportDto>(result);
            response.success = true;
            response.Data = VehicleReportDto;
            return response;
        }

        //POST 
        public async Task<GenericResponse<VehicleReportDto>> PostVehicleReport(VehicleReportRequest vehicleReportRequest)
        {
            GenericResponse<VehicleReportDto> response = new GenericResponse<VehicleReportDto>();
            try
            {
                //Verificar que contenga los campos de usuarios 
                if (!vehicleReportRequest.AdminUserId.HasValue && !vehicleReportRequest.MobileUserId.HasValue)
                {
                    response.success = false;
                    response.AddError("Usuario no especificado", $"Es necesario especificar un usuario para la creación del reporte", 4);
                    return response;
                }

                Expenses expensesRequest = null;

                //Verificar que el usuario que lo creo exista
                if (vehicleReportRequest.MobileUserId.HasValue)
                {
                    var existeUserProfile = await _unitOfWork.UserProfileRepo.Get(c => c.Id == vehicleReportRequest.MobileUserId.Value);
                    var resultUserProfile = existeUserProfile.FirstOrDefault();

                    if (resultUserProfile == null)
                    {
                        response.success = false;
                        response.AddError("No existe UserProfile", $"No existe UserProfileId {vehicleReportRequest.MobileUserId} para cargar", 1);
                        return response;
                    }

                }

                if (vehicleReportRequest.AdminUserId.HasValue)
                {
                    var existeAppUser = await _userManager.Users.SingleOrDefaultAsync(c => c.Id == vehicleReportRequest.AdminUserId.Value);
                    if (existeAppUser == null)
                    {
                        response.success = false;
                        response.AddError("No existe AppUser", $"No existe AppUserId {vehicleReportRequest.AdminUserId} para cargar", 1);
                        return response;
                    }

                }

                //Verificar que el vehiculo exista
                var vehicleExistQuery = await _unitOfWork.VehicleRepo.Get(c => c.Id == vehicleReportRequest.VehicleId);
                var vehicleExists = vehicleExistQuery.FirstOrDefault();

                if (vehicleExists == null)
                {
                    response.success = false;
                    response.AddError("Vehiculo no encontrado", $"No existe Vehiculo con el Id {vehicleReportRequest.VehicleId} solicitado", 5);
                    return response;

                }

                //Verificar si el reporte es por carga de gasolina y si contiene los campos requeridos para ello
                if (vehicleReportRequest.ReportType == Domain.Enums.ReportType.Carga_Gasolina)
                {
                    if (!vehicleReportRequest.GasolineLoadAmount.HasValue)
                    {
                        response.success = false;
                        response.AddError("Es necesario un valor existente Litros", $"Para el tipo de Carga de Gasolina, es necesario un valor para la carga de Litros", 1);
                        return response;
                    }

                    if (!vehicleReportRequest.GasolineCurrentKM.HasValue)
                    {
                        response.success = false;
                        response.AddError("Es necesario un valor existente KM", $"Para el tipo de Carga de Gasolina, es necesario un valor para el KM actual ", 2);
                        return response;
                    }

                    if (!vehicleReportRequest.AmountGasoline.HasValue)
                    {
                        response.success = false;
                        response.AddError("Es necesario el monto de carga de gasolina", $"Para el tipo de Carga de Gasolina, es necesario el monto que se gasto ", 2);
                        return response;
                    }

                    //Verificar que exista el tipo de gasto
                    var existetypeOfExpenses = await _unitOfWork.TypesOfExpensesRepo.Get(v => v.Name == "Carga_Gasolina");
                    var resultType = existetypeOfExpenses.FirstOrDefault();
                    TypesOfExpenses expenseFuel = null;

                    if (resultType == null)
                    {
                        var typesOfExpensesRequest = new TypesOfExpenses()
                        {
                            Name = "Carga_Gasolina",
                            Description = "Gasto Creado Por Reporte de Gasolina"
                        };


                        expenseFuel = _mapper.Map<TypesOfExpenses>(typesOfExpensesRequest);
                        await _unitOfWork.TypesOfExpensesRepo.Add(expenseFuel);
                    }

                    //se crea gastos
                    expensesRequest = new Expenses()
                    {
                        TypesOfExpensesId = resultType.Id,
                        Cost = (decimal)vehicleReportRequest.AmountGasoline,
                        ExpenseDate = DateTime.Now,
                        ERPFolio = Guid.NewGuid().ToString()
                    };
                    expensesRequest.Vehicles.Add(vehicleExists);
                }

                //Verificar si se agregara a un reporte de uso
                if (vehicleReportRequest.VehicleReportUseId.HasValue)
                {
                    var profileD = await _unitOfWork.VehicleReportUseRepo.Get(p => p.Id == vehicleReportRequest.VehicleReportUseId);
                    var resultD = profileD.FirstOrDefault();

                    if (resultD == null)
                    {
                        response.success = false;
                        response.AddError("No existe reporte de uso", $"No existe reporte de uso con el Id {vehicleReportRequest.VehicleReportUseId} solicitado");
                        return response;
                    }
                }

                //Mappear la entidad
                var entidadR = _mapper.Map<VehicleReport>(vehicleReportRequest);
                //Modificar el status del reporte
                entidadR.ReportStatus = Domain.Enums.ReportStatusType.Pendiente;
                entidadR.IsResolved = false;

                if (expensesRequest != null)
                {
                    entidadR.Expenses.Add(expensesRequest);
                }

                //Agregar los gastos al reporte
                foreach (var expenseId in vehicleReportRequest.Expenses)
                {
                    var expense = await _unitOfWork.ExpensesRepo.GetById(expenseId);
                    if (expense == null)
                    {
                        response.success = false;
                        response.AddError("Gasto no existe", $"El Id de gasto {expenseId} no existe");
                    }
                    entidadR.Expenses.Add(expense);
                }

                //Mappear y guardar la entidad
                await _unitOfWork.VehicleReportRepo.Add(entidadR);

                //Agregar imagenes al reporte
                //Guardar las fotos
                var images = new List<VehicleReportImage>();

                foreach (var image in vehicleReportRequest.ReportImages)
                {
                    //Validar imagenes y Guardar las imagenes en el blobstorage
                    if (image.ContentType.Contains("image"))
                    {
                        //Manipular el nombre de archivo
                        var uploadDate = DateTime.UtcNow;
                        Random rndm = new Random();
                        string FileExtn = System.IO.Path.GetExtension(image.FileName);
                        var filePath = $"{entidadR.Id}/{uploadDate.Day}{uploadDate.Month}{uploadDate.Year}_{uploadDate.Hour}{uploadDate.Minute}{rndm.Next(1, 1000)}{FileExtn}";
                        var uploadedUrl = await _blobStorageService.UploadFileToBlobAsync(image, _azureBlobContainers.Value.VehicleReports, filePath);

                        //Agregar la imagen en BD
                        var newImage = new VehicleReportImage()
                        {
                            FilePath = filePath,
                            FileUrl = await _blobStorageService.GetFileUrl(_azureBlobContainers.Value.VehicleReports, filePath),
                            VehicleReport = entidadR
                        };

                        await _unitOfWork.VehicleReportImage.Add(newImage);
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
                var VehicleReportDTOCG = _mapper.Map<VehicleReportDto>(entidadR);
                response.Data = VehicleReportDTOCG;
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 0);

                return response;
            }
        }

        //Marcar reporte como resuelto (Admin only)
        public async Task<GenericResponse<VehicleReportDto>> ManageReportStatus(SolvedReportRequest request)
        {
            GenericResponse<VehicleReportDto> response = new GenericResponse<VehicleReportDto>();
            try
            {
                //Buscar el report especificado
                var report = await _unitOfWork.VehicleReportRepo.GetById(request.ReportId);
                if (report == null)
                {
                    response.success = false;
                    response.AddError("Reporte no encontrado", $"El Id {request.ReportId} de reporte especificado no existe", 2);
                    return response;
                }

                switch (report.ReportStatus)
                {
                    case Domain.Enums.ReportStatusType.Resuelto:
                        response.success = false;
                        response.AddError("Reporte resuelto", $"El Id {request.ReportId} de reporte ya esta marcado como resuelto", 3);
                        break;
                    case Domain.Enums.ReportStatusType.Cancelado:
                        response.success = false;
                        response.AddError("Reporte cancelado", $"El Id {request.ReportId} de reporte ya esta marcado como cancelado", 4);
                        break;
                    default:
                        break;
                }

                //Buscar el usuario especificado
                var adminUserExists = await _userManager.Users.SingleOrDefaultAsync(c => c.Id == request.AdminUserId);
                if (adminUserExists == null)
                {
                    response.success = false;
                    response.AddError("No existe AppUser", $"No existe AppUserId {request.AdminUserId} para cargar", 5);
                    return response;
                }

                //Modificar el reporte y cambiar estatus
                report.ReportStatus = request.Status;
                report.SolvedByAdminUser = adminUserExists;
                report.ReportSolutionComment = request.ResolutionComment;
                
                if(request.Status == Domain.Enums.ReportStatusType.Resuelto || request.Status == Domain.Enums.ReportStatusType.Cancelado)
                {
                    report.IsResolved = true;
                }

                await _unitOfWork.VehicleReportRepo.Update(report);
                await _unitOfWork.SaveChangesAsync();

                response.success = true;
                var VehicleReportDTOCG = _mapper.Map<VehicleReportDto>(report);
                response.Data = VehicleReportDTOCG;
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Reporte no encontrado", ex.Message, 1);

                return response;
            }
        }

        //Put
        public async Task<GenericResponse<VehicleReportDto>> PutVehicleReport(VehicleReportUpdateRequest request)
        {
            GenericResponse<VehicleReportDto> response = new GenericResponse<VehicleReportDto>();
            try
            {
                //Consultar que el reporte exista
                var reportQuery = await _unitOfWork.VehicleReportRepo.Get(p => p.Id == request.ReportId);
                var report = reportQuery.FirstOrDefault();
                if (report == null)
                {
                    response.success = true;
                    response.AddError("No existe reporte", $"No existe reporte con el Id {request.ReportId} solicitado", 2);
                    return response;
                }

                //Verificar que el reporte sea modificable
                switch (report.ReportStatus)
                {
                    case Domain.Enums.ReportStatusType.Resuelto:
                        response.success = false;
                        response.AddError("Reporte resuelto", $"El Id {request.ReportId} de reporte ya esta marcado como resuelto", 3);
                        return response;
                        break;
                    case Domain.Enums.ReportStatusType.Cancelado:
                        response.success = false;
                        response.AddError("Reporte cancelado", $"El Id {request.ReportId} de reporte ya esta marcado como cancelado", 4);
                        return response;
                        break;
                    default:
                        break;
                }

                //Modificar datos dependiendo de si se enviaron los valores
                report.ReportDate = request.ReportDate != null ? request.ReportDate.Value : report.ReportDate;
                report.Commentary = !string.IsNullOrEmpty(request.Commentary) ? request.Commentary : report.Commentary;
                report.ReportType = request.ReportType.HasValue ? request.ReportType.Value : report.ReportType;
                report.GasolineLoadAmount = request.GasolineLoadAmount.HasValue ? request.GasolineLoadAmount.Value : report.GasolineLoadAmount;
                report.GasolineCurrentKM = request.GasolineCurrentKM.HasValue ? request.GasolineCurrentKM.Value : report.GasolineCurrentKM;

                //Agregar los gastos especificados
                foreach (var expenseId in request.ExpensesToAdd)
                {
                    var expense = await _unitOfWork.ExpensesRepo.GetById(expenseId);
                    if (expense == null)
                    {
                        response.success = false;
                        response.AddError("Gasto no existe", $"El Id de gasto {expenseId} no existe");
                    }
                    report.Expenses.Add(expense);
                }

                //Eliminar los gastos especificados
                foreach (var expenseId in request.ExpensesToRemove)
                {
                    var expense = await _unitOfWork.ExpensesRepo.GetById(expenseId);
                    if (expense == null)
                    {
                        response.success = false;
                        response.AddError("Gasto no existe", $"El Id de gasto {expenseId} no existe");
                    }
                    report.Expenses.Remove(expense);
                }

                await _unitOfWork.VehicleReportRepo.Update(report);
                await _unitOfWork.SaveChangesAsync();

                response.success = true;
                var VehicleReportDTOCG = _mapper.Map<VehicleReportDto>(report);
                response.Data = VehicleReportDTOCG;
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        //Delete
        public async Task<GenericResponse<bool>> DeleteVehicleReport(int Id)
        {
            GenericResponse<bool> response = new GenericResponse<bool>();
            var entidad = await _unitOfWork.VehicleReportRepo.Get(filter: p => p.Id == Id);
            var result = entidad.FirstOrDefault();
            if (result == null)
            {
                response.success = false;
                response.Data = false;
                response.AddError("Reporte no encontrado", "El reporte especificado no existe", 1);

                return response;
            }

            //Borrar las fotos del blob
            var photos = await _unitOfWork.VehicleReportImage.Get(filter: v => v.VehicleReportId == result.Id);

            foreach (var photo in photos)
            {
                await _blobStorageService.DeleteFileFromBlobAsync(_azureBlobContainers.Value.VehicleReports, photo.FilePath);
                await _unitOfWork.VehicleReportImage.Delete(photo.Id);
            }

            var existe = await _unitOfWork.VehicleReportRepo.Delete(Id);
            await _unitOfWork.SaveChangesAsync();

            response.success = true;
            response.Data = true;

            return response;
        }

        //Agregar imagen al reporte invidividualmente
        public async Task<GenericResponse<VehicleReportImage>> AddReportImage(VehicleImageRequest request, int reportId)
        {
            GenericResponse<VehicleReportImage> response = new GenericResponse<VehicleReportImage>();

            try
            {
                //Verificar que exista el vehiculo
                var report = await _unitOfWork.VehicleReportRepo.GetById(reportId);
                if (report == null) return null;

                if (request.ImageFile.ContentType.Contains("image"))
                {
                    //Manipular el nombre de archivo
                    var uploadDate = DateTime.UtcNow;
                    Random rndm = new Random();
                    string FileExtn = System.IO.Path.GetExtension(request.ImageFile.FileName);
                    var filePath = $"{report.Id}/{uploadDate.Day}{uploadDate.Month}{uploadDate.Year}_{uploadDate.Hour}{uploadDate.Minute}{rndm.Next(1, 1000)}{FileExtn}";
                    var uploadedUrl = await _blobStorageService.UploadFileToBlobAsync(request.ImageFile, _azureBlobContainers.Value.VehicleReports, filePath);

                    //Agregar la imagen en BD
                    var newImage = new VehicleReportImage()
                    {
                        FilePath = filePath,
                        FileUrl = await _blobStorageService.GetFileUrl(_azureBlobContainers.Value.VehicleReports, filePath),
                        VehicleReport = report
                    };

                    await _unitOfWork.VehicleReportImage.Add(newImage);
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

        //Eliminar imagen del reporte individualmente
        public async Task<GenericResponse<bool>> DeleteReportImage(int reportImageId)
        {
            GenericResponse<bool> response = new GenericResponse<bool>();

            try
            {
                //Borrar las fotos del blob
                var photos = await _unitOfWork.VehicleReportImage.GetById(reportImageId);
                if (photos == null) return null;

                await _blobStorageService.DeleteFileFromBlobAsync(_azureBlobContainers.Value.VehicleReports, photos.FilePath);
                await _unitOfWork.VehicleReportImage.Delete(photos.Id);
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
    }
}
