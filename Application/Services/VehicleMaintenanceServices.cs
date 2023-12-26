using Application.Extensions;
using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Identity;
using Domain.Entities.Registered_Cars;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace Application.Services
{
    public class VehicleMaintenanceServices : IVehicleMaintenanceService

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PaginationOptions _paginationOptions;
        private readonly UserManager<AppUser> _userManager;
        private readonly IOptions<BlobContainers> _azureBlobContainers;
        private readonly IBlobStorageService _blobStorageService;

        public VehicleMaintenanceServices(IUnitOfWork unitOfWork, IMapper mapper, IOptions<PaginationOptions> options, UserManager<AppUser> userManager, IOptions<BlobContainers> azureBlobContainers, IBlobStorageService blobStorageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paginationOptions = options.Value;
            _userManager = userManager;
            _azureBlobContainers = azureBlobContainers;
            _blobStorageService = blobStorageService;
        }

        //GetAll
        public async Task<PagedList<VehicleMaintenanceDto>> GetVehicleMaintenanceAll(VehicleMaintenanceFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            string properties = "Expenses,Expenses.PhotosOfSpending,Vehicle,WorkShop,Report,ApprovedByUser,MaintenanceProgress,MaintenanceProgress.ProgressImages,MaintenanceProgress.AdminUser,MaintenanceProgress.MobileUser,Vehicle.AssignedDepartments";
            IEnumerable<VehicleMaintenance> maintenances = null;
            Expression<Func<VehicleMaintenance, bool>> Query = null;

            if (filter.VehicleId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.VehicleId == filter.VehicleId.Value);
                }
                else { Query = p => p.VehicleId == filter.VehicleId.Value; }
            }

            if (filter.AdminId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.ApprovedByUserId == filter.AdminId.Value);
                }
                else { Query = p => p.ApprovedByUserId == filter.AdminId.Value; }
            }

            if (filter.WorkshopId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.WorkShopId == filter.WorkshopId.Value);
                }
                else { Query = p => p.WorkShopId == filter.WorkshopId.Value; }
            }

            if (filter.ReportId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.ReportId == filter.ReportId.Value);
                }
                else { Query = p => p.ReportId == filter.ReportId.Value; }
            }

            if (filter.Status.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Status == filter.Status.Value);
                }
                else { Query = p => p.Status == filter.Status.Value; }
            }

            if (filter.MaintenanceDate.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.MaintenanceDate.Value.Date == filter.MaintenanceDate.Value.Date);
                }
                else { Query = p => p.MaintenanceDate.Value.Date == filter.MaintenanceDate.Value.Date; }
            }

            if (Query != null)
            {
                maintenances = await _unitOfWork.VehicleMaintenanceRepo.Get(filter: Query, includeProperties: properties);
            }
            else
            {
                maintenances = await _unitOfWork.VehicleMaintenanceRepo.Get(includeProperties: properties);
            }

            var dtos = _mapper.Map<IEnumerable<VehicleMaintenanceDto>>(maintenances);
            var pagedApprovals = PagedList<VehicleMaintenanceDto>.Create(dtos, filter.PageNumber, filter.PageSize);

            return pagedApprovals;

        }

        //Obtener resumen de gastos
        public async Task<GenericResponse<MaintenanceExpenseSummaryDto>> GetMaintenanceExpenseSummary(int MaintenanceId)
        {
            GenericResponse<MaintenanceExpenseSummaryDto> response = new GenericResponse<MaintenanceExpenseSummaryDto>();
            try
            {
                //Respuesta 
                var summary = new MaintenanceExpenseSummaryDto();

                //Obtener todos los gastos
                var expenses = await _unitOfWork.ExpensesRepo.Get(e => e.VehicleMaintenanceId == MaintenanceId);

                //Realizar sumatoria
                decimal expenseTotal = 0;
                foreach(var expense in expenses)
                {
                    expenseTotal += expense.Cost;
                }

                //Asignar los datos
                summary.MaintenanceId = MaintenanceId;
                summary.ExpenseTotal = expenseTotal;
                summary.ExpensesSummary = _mapper.Map<List<ExpenseSummary>>(expenses);

                response.success = true;
                response.Data = summary;

                return response;

            } 
            catch(Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        //GETBYID
        public async Task<GenericResponse<VehicleMaintenanceDto>> GetVehicleMaintenanceById(int Id)
        {
            var Expen = new VehicleMaintenanceDto();
            var Vehicle = new UnrelatedVehiclesDto();
            var workshop = new MaintenanceWorkShopSlimDto();
            var report = new VehicleReportSlimDto();
            var main = new List<MaintenanceProgressDto>();
            var exp =  new List<ExpensesForMaintenanceDto>();

            GenericResponse<VehicleMaintenanceDto> response = new GenericResponse<VehicleMaintenanceDto>();
            var profile = await _unitOfWork.VehicleMaintenanceRepo.Get(filter: p => p.Id == Id, includeProperties: "Expenses,Expenses.PhotosOfSpending,Vehicle,WorkShop,Report,ApprovedByUser,MaintenanceProgress,MaintenanceProgress.ProgressImages,MaintenanceProgress.AdminUser,MaintenanceProgress.MobileUser");
            var result = profile.FirstOrDefault();
            if(result == null)
            {
                response.success = false;
                response.AddError("Mantenimiento no encontrado", $"El mantenimiento con ID {Id} no existe", 2);
                return response;
            }


            var expense = await _unitOfWork.ExpensesRepo.Get(filter: expense => expense.VehicleMaintenanceId == Id);
            var resultexpense = expense.FirstOrDefault();

            decimal suma = 0;
            foreach(var sum in expense )
            {
                suma += sum.Cost;
            }

            var Main = new VehicleMaintenanceDto()
            {
                Id = Id,
                ReasonForMaintenance = result.ReasonForMaintenance,
                MaintenanceDate = result.MaintenanceDate,
                Status = result.Status,
                Comment = result.Comment,
                InitialMileage = result.InitialMileage,
                InitialCharge = result.InitialCharge,
                FinalMileage = result.FinalMileage,
                FinalCharge = result.FinalCharge,
                VehicleId = result.VehicleId,
                Vehicle = Vehicle,
                WorkShopId = result.WorkShopId,
                WorkShop = workshop,
                ApprovedByUserId = result.ApprovedByUserId,
                ApprovedByAdminName = result.ApprovedByUser.FullName,
                ReportId = result.ReportId,
                Report = report,
                MaintenanceProgress = main,
                Expenses = exp,


            };

            //var VehicleMaintenanceDTO = _mapper.Map<VehicleMaintenanceDto>(result);
            response.success = true;
            response.Data = Main;
            return response;
        }

        //INICIAR MTTO

        public async Task<GenericResponse<VehicleMaintenanceDto>> InitiateMaintenance(VehicleMaintenanceRequest request)
        {
           GenericResponse<VehicleMaintenanceDto> response = new GenericResponse<VehicleMaintenanceDto>();
           try
            {
                //Verificar  que el vehiculo exista
                var vehicleExists = await _unitOfWork.VehicleRepo.GetById(request.VehicleId);
                if (vehicleExists == null)
                {
                    response.success = false;
                    response.AddError("No existe Vehicle", $"No existe Vehiculo con el VehicleId {request.VehicleId} solicitado", 2);
                    return response;
                }

                //Verificar que el vehiculo se encuentre disponible
                switch (vehicleExists.VehicleStatus)
                {
                    case VehicleStatus.INACTIVO:
                    case VehicleStatus.MANTENIMIENTO:
                    case VehicleStatus.EN_USO:
                        response.success = false;
                        response.AddError("Vehiculo no disponible", "El estatus del vehiculo no permite su mantenimiento por el momento", 3);
                        return response;
                    default:
                        break;
                }

                //Verificar que exista el taller
                if (request.WorkShopId.HasValue)
                {
                    var workshop = await _unitOfWork.MaintenanceWorkshopRepo.GetById(request.WorkShopId.Value);
                    if (workshop == null)
                    {
                        response.success = false;
                        response.AddError("No existe taller", $"No existe taller con el Id {request.WorkShopId} solicitado", 4);
                        return response;
                    }
                }

                //Verificar que exista el taller
                if (request.AdminUserId.HasValue)
                {
                    var adminUser = await _userManager.Users.Where(u => u.Id == request.AdminUserId.Value).SingleOrDefaultAsync();
                    if (adminUser == null)
                    {
                        response.success = false;
                        response.AddError("No existe usuario", $"No existe usuario con el Id {request.AdminUserId} solicitado", 5);
                        return response;
                    }
                }

                //Verificar que exista el taller
                if (request.ReportId.HasValue)
                {
                    var workshop = await _unitOfWork.VehicleReportRepo.GetById(request.ReportId.Value);
                    if (workshop == null)
                    {
                        response.success = false;
                        response.AddError("No existe reporte", $"No existe reporte con el Id {request.ReportId} solicitado", 6);
                        return response;
                    }
                }

                //Mapear Elementos
                var entidad = _mapper.Map<VehicleMaintenance>(request);
                entidad.ApprovedByUserId = request.AdminUserId;

                //Asignar los datos relacionados del vehiculo
                entidad.InitialCharge = request.InitialCharge ?? vehicleExists.CurrentChargeKwH;
                entidad.InitialMileage = request.InitialMileage ?? vehicleExists.CurrentKM;
                entidad.MaintenanceDate = request.MaintenanceDate ?? DateTime.UtcNow;

                //Modificar Status
                entidad.Status = VehicleServiceStatus.EN_CURSO;
                vehicleExists.VehicleStatus = VehicleStatus.MANTENIMIENTO;

                //GuardarCambios
                await _unitOfWork.VehicleRepo.Update(vehicleExists);
                await _unitOfWork.VehicleMaintenanceRepo.Add(entidad);
                await _unitOfWork.SaveChangesAsync();

                response.success = true;
                var VehicleMaintenanceDTO = _mapper.Map<VehicleMaintenanceDto>(entidad);
                response.Data = VehicleMaintenanceDTO;
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        //FINALIZAR MTTO
        public async Task<GenericResponse<VehicleMaintenanceDto>> FinalizeMaintenance(FinalizeMaintenanceRequest request)
        {
            GenericResponse<VehicleMaintenanceDto> response = new GenericResponse<VehicleMaintenanceDto>();
            try
            {
                //Verificar que el mtto exista 
                var maintenance = await _unitOfWork.VehicleMaintenanceRepo.GetById(request.MaintenanceId);
                if(maintenance == null)
                {
                    response.success = false;
                    response.AddError("Mantenimiento no encontrado", $"El mantenimiento con Id {request.MaintenanceId} no existe", 2);
                    return response;
                }

                //Verificar que sea valido para su modificación
                if(maintenance.Status != VehicleServiceStatus.EN_CURSO)
                {
                    response.success = false;
                    response.AddError("Estatus invalido", "El estatus del mantenmiento no permite su modificación", 3);
                    return response;
                }

                //Mapear Elementos
                maintenance.Status = VehicleServiceStatus.FINALIZADO;
                maintenance.FinalCharge = request.FinalChargeKwH;
                maintenance.FinalMileage = request.FinalMileage;
                maintenance.Comment = request.Comment ?? "N/A";

                //Asignar los datos relacionados del vehiculo
                var vehicle = await _unitOfWork.VehicleRepo.GetById(maintenance.VehicleId);
                vehicle.CurrentChargeKwH = request.FinalChargeKwH;
                vehicle.CurrentKM = request.FinalMileage;
                vehicle.VehicleStatus = VehicleStatus.ACTIVO;

                //GuardarCambios
                await _unitOfWork.VehicleRepo.Update(vehicle);
                await _unitOfWork.VehicleMaintenanceRepo.Update(maintenance);
                await _unitOfWork.SaveChangesAsync();

                response.success = true;
                var dto = _mapper.Map<VehicleMaintenanceDto>(maintenance);
                response.Data = dto;
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        //CANCELAR MTTO
        public async Task<GenericResponse<VehicleMaintenanceDto>> CancelMaintenance(CancelMaintenanceRequest request)
        {
            GenericResponse<VehicleMaintenanceDto> response = new GenericResponse<VehicleMaintenanceDto>();
            try
            {
                //Verificar que el mtto exista 
                var maintenance = await _unitOfWork.VehicleMaintenanceRepo.GetById(request.MaintenanceId);
                if (maintenance == null)
                {
                    response.success = false;
                    response.AddError("Mantenimiento no encontrado", $"El mantenimiento con Id {request.MaintenanceId} no existe", 2);
                    return response;
                }

                //Verificar que sea valido para su modificación
                if (maintenance.Status != VehicleServiceStatus.EN_CURSO)
                {
                    response.success = false;
                    response.AddError("Estatus invalido", "El estatus del mantenmiento no permite su modificación", 3);
                    return response;
                }

                //Mapear Elementos
                maintenance.Status = VehicleServiceStatus.CANCELADO;
                maintenance.Comment = request.Comment ?? "N/A";

                //Asignar los datos relacionados del vehiculo
                var vehicle = await _unitOfWork.VehicleRepo.GetById(maintenance.VehicleId);
                vehicle.VehicleStatus = VehicleStatus.ACTIVO;

                //GuardarCambios
                await _unitOfWork.VehicleRepo.Update(vehicle);
                await _unitOfWork.VehicleMaintenanceRepo.Update(maintenance);
                await _unitOfWork.SaveChangesAsync();

                response.success = true;
                var dto = _mapper.Map<VehicleMaintenanceDto>(maintenance);
                response.Data = dto;
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        //ACTUALIZAR MTTO
        public async Task<GenericResponse<VehicleMaintenanceDto>> UpdateMaintenance(MaintenanceUpdateRequest request)
        {
            GenericResponse<VehicleMaintenanceDto> response = new GenericResponse<VehicleMaintenanceDto>();
            try
            {
                //Verificar que el mtto exista 
                var maintenance = await _unitOfWork.VehicleMaintenanceRepo.GetById(request.MaintenanceId);
                if (maintenance == null)
                {
                    response.success = false;
                    response.AddError("Mantenimiento no encontrado", $"El mantenimiento con Id {request.MaintenanceId} no existe", 2);
                    return response;
                }

                //Verificar que sea valido para su modificación
                if (maintenance.Status == VehicleServiceStatus.CANCELADO)
                {
                    response.success = false;
                    response.AddError("Estatus invalido", "El estatus del mantenmiento no permite su modificación", 3);
                    return response;
                }

                //Verificar el gasto
                if (request.ExpenseId.HasValue)
                {
                    var expense = await _unitOfWork.ExpensesRepo.GetById(request.ExpenseId.Value);
                    if (expense == null)
                    {
                        response.success = false;
                        response.AddError("Gasto invalido", "El gasto especificado no existe", 4);
                        return response;
                    }

                   // maintenance.ExpenseId = expense.Id;
                }

                //Aplicar cambios
                maintenance.ReasonForMaintenance = request.ReasonForMaintenance ?? maintenance.ReasonForMaintenance;
                maintenance.Comment = request.Comment ?? maintenance.Comment;
                maintenance.MaintenanceDate = request.MaintenanceDate ?? maintenance.MaintenanceDate;

                //Verificar si cambio el taller
                if (request.WorkShopId.HasValue)
                {
                    var workshop = await _unitOfWork.MaintenanceWorkshopRepo.GetById(request.WorkShopId.Value);
                    if( workshop == null)
                    {
                        response.success = false;
                        response.AddError("Taller no existe", "El taller especificado no existe", 5);
                        return response;
                    }
                    maintenance.WorkShopId = workshop.Id;
                }

                //Verificar si cambio el reporte
                if (request.ReportId.HasValue)
                {
                    var report = await _unitOfWork.VehicleReportRepo.GetById(request.ReportId.Value);
                    if (report == null)
                    {
                        response.success = false;
                        response.AddError("Reporte no existe", "El reporte especificado no existe", 6);
                        return response;
                    }
                    maintenance.ReportId = report.Id;
                }

                //Guardar Cambios
                await _unitOfWork.VehicleMaintenanceRepo.Update(maintenance);
                await _unitOfWork.SaveChangesAsync();

                response.success = true;
                var dto = _mapper.Map<VehicleMaintenanceDto>(maintenance);
                response.Data = dto;
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        //DELETE
        public async Task<GenericResponse<VehicleMaintenanceDto>> DeleteVehicleManintenance(int Id)
        {
            GenericResponse<VehicleMaintenanceDto> response = new GenericResponse<VehicleMaintenanceDto>();
            try
            {
                var entidad = await _unitOfWork.VehicleMaintenanceRepo.Get(filter: p => p.Id == Id);
                var result = entidad.FirstOrDefault();

                if(result.Status == VehicleServiceStatus.EN_CURSO)
                {
                    response.success = false;
                    response.AddError("Estatus invalido", "El estatus del mantenimiento no permite su eliminación", 3);
                    return response;
                }

                if (result == null)
                {
                    response.success = false;
                    response.AddError("MTTO no existe", "El mtto no se encontro", 2);
                    return response;
                }

                var existe = await _unitOfWork.VehicleMaintenanceRepo.Delete(Id);
                await _unitOfWork.SaveChangesAsync();

                var VehicleMaintenanceDTO = _mapper.Map<VehicleMaintenanceDto>(result);
                response.success = true;
                response.Data = VehicleMaintenanceDTO;

                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        //AGREGAR PROGRESO
        public async Task<GenericResponse<MaintenanceProgressDto>> AddProgress(MaintenanceProgressRequest request)
        {
            GenericResponse<MaintenanceProgressDto> response = new GenericResponse<MaintenanceProgressDto>();
            try
            {
                //Verificar que exista el mantenimiento y tenga el estatus correcto para su modificación
                var maintenance = await _unitOfWork.VehicleMaintenanceRepo.GetById(request.VehicleMaintenanceId);
                if(maintenance == null)
                {
                    response.success = false;
                    response.AddError("Mantenimiento no encontrado", $"El mantenimiento especificado {request.VehicleMaintenanceId} no existe", 2);
                    return response;
                }

                if(maintenance.Status == VehicleServiceStatus.CANCELADO)
                {
                    response.success = false;
                    response.AddError("Estatus invalido", "El estatus del mantenmiento especificado no permite su modificación", 3);
                    return response;
                }

                //Verificar que ambos usuarios no sean nulos
                if(!request.AdminUserId.HasValue && !request.MobileUserId.HasValue)
                {
                    response.success = false;
                    response.AddError("Usuario no especificado", "Debe especificar un usuario de Admin o un conductor", 4);
                    return response;
                }

                foreach ( var ex in request.ExpenseId)
                {
                    //verificar el gasto
                    if (ex.HasValue)
                    {
                        var expense = await _unitOfWork.ExpensesRepo.GetById(ex.Value);
                        if (expense == null)
                        {
                            response.success = false;
                            response.AddError("Gasto invalido", "El gasto especificado no existe", 5);
                            return response;
                        }

                        var consultE = await _unitOfWork.ExpensesRepo.Get(filter: x => x.Id == ex.Value);
                        var resultE = consultE.FirstOrDefault();
                        foreach (var co in consultE)
                        {
                            co.VehicleMaintenanceId = maintenance.Id;
                            await _unitOfWork.ExpensesRepo.Update(co);
                            await _unitOfWork.SaveChangesAsync();

                        }
                    }
                }

                //Mapear los datos
                var newProgress = _mapper.Map<MaintenanceProgress>(request);

                //Agregar imagenes al reporte
                //Guardar las fotos
                var images = new List<MaintenanceProgressImages>();

                foreach (var image in request.Images)
                {
                    //Validar imagenes y Guardar las imagenes en el blobstorage
                    if (image.ContentType.Contains("image"))
                    {
                        //Manipular el nombre de archivo
                        var uploadDate = DateTime.UtcNow;
                        Random rndm = new Random();
                        string FileExtn = System.IO.Path.GetExtension(image.FileName);
                        var filePath = $"{newProgress.VehicleMaintenanceId}/{uploadDate.Day}{uploadDate.Month}{uploadDate.Year}_{uploadDate.Hour}{uploadDate.Minute}{rndm.Next(1, 1000)}{FileExtn}";
                        var uploadedUrl = await _blobStorageService.UploadFileToBlobAsync(image, _azureBlobContainers.Value.MaintenanceProgressImages, filePath);

                        //Agregar la imagen en BD
                        var newImage = new MaintenanceProgressImages()
                        {
                            FilePath = filePath,
                            FileURL = await _blobStorageService.GetFileUrl(_azureBlobContainers.Value.MaintenanceProgressImages, filePath),
                            Progress = newProgress
                        };

                        await _unitOfWork.MaintenanceProgressImageRepot.Add(newImage);  
                        images.Add(newImage);
                    }
                    else
                    {
                        response.success = false;
                        response.AddError("Archivo de Imagen Invalido", "Uno o mas archivos no corresponden a un archivo de Imagen",6);

                        return response;
                    }
                }


                //Guardar los datos
                await _unitOfWork.MaintenanceProgressRepo.Add(newProgress);
                await _unitOfWork.SaveChangesAsync();

                var dto = _mapper.Map<MaintenanceProgressDto>(newProgress);
                response.Data = dto;
                response.success = true;
                return response;
            }
            catch(Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        public async Task<GenericResponse<bool>> DeleteProgress(int ProgressId)
        {
            GenericResponse<bool> response= new GenericResponse<bool>();
            try
            {
                var progress = await _unitOfWork.MaintenanceProgressRepo.GetById(ProgressId);
                if(progress == null)
                {
                    response.success = false;
                    response.AddError("Not found", "El comentario de progreso de mantenimiento no existe", 2);
                    return response;
                }

                //Borrar las imagenes del blob
                var images = await _unitOfWork.MaintenanceProgressImageRepot.Get(filter: i => i.ProgressId== ProgressId);
                foreach(var image in images)
                {
                    await _blobStorageService.DeleteFileFromBlobAsync(_azureBlobContainers.Value.MaintenanceProgressImages, image.FilePath);
                    await _unitOfWork.MaintenanceProgressImageRepot.Delete(image.Id);
                }

                var delete = await _unitOfWork.MaintenanceProgressRepo.Delete(ProgressId);
                await _unitOfWork.SaveChangesAsync();

                response.success = true;
                response.Data = delete;
                return response;
            }
            catch(Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        //Obtener reportes por departamento
        public async Task<GenericResponse<List<VehicleMaintenanceDto>>> GetMaintenanceByDepartment(int departmentId)
        {
            GenericResponse<List<VehicleMaintenanceDto>> response = new GenericResponse<List<VehicleMaintenanceDto>>();
            try
            {
                var reports = await _unitOfWork.VehicleMaintenanceRepo.Get(r => r.Vehicle.AssignedDepartments.Any(d => d.Id == departmentId), includeProperties: "Expenses,Expenses.PhotosOfSpending,Vehicle,WorkShop,Report,ApprovedByUser,MaintenanceProgress,MaintenanceProgress.ProgressImages,MaintenanceProgress.AdminUser,MaintenanceProgress.MobileUser,Vehicle.AssignedDepartments");

                var dto = _mapper.Map<List<VehicleMaintenanceDto>>(reports);
                response.success = true;
                response.Data = dto;
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

