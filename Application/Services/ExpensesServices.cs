﻿using Application.Extensions;
using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Services
{
    public class ExpensesServices : IExpensesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PaginationOptions _paginationOptions;
        private readonly IOptions<BlobContainers> _azureBlobContainers;
        private readonly IBlobStorageService _blobStorageService;

        public ExpensesServices(IUnitOfWork unitOfWork, IMapper mapper, IOptions<PaginationOptions> options, IOptions<BlobContainers> azureBlobContainers, IBlobStorageService blobStorageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paginationOptions = options.Value;
            _azureBlobContainers = azureBlobContainers;
            _blobStorageService = blobStorageService;
        }
        public async Task<GenericResponse<ExpensesDto>> PostExpenses(ExpensesRequest request)
        {
            GenericResponse<ExpensesDto> response = new GenericResponse<ExpensesDto>();
            try
            {
                var dtoexpens = new List<Vehicle>();
                foreach (var vehicleid in request.VehicleIds)
                {
                    var existevehicleid = await _unitOfWork.VehicleRepo.Get(v => v.Id == vehicleid);
                    var resultExpenses = existevehicleid.FirstOrDefault();

                    if (resultExpenses == null)
                    {
                        response.success = false;
                        response.AddError("No existe vehiculo", $"No existe vehiculo con ese id{vehicleid} solicitado", 2);
                        return response;
                    }
                    dtoexpens.Add(resultExpenses);
                }

                //Verificar que exista el tipo de gasto
                var existetypeOfExpenses = await _unitOfWork.TypesOfExpensesRepo.Get(v => v.Id == request.TypesOfExpensesId);
                var resultType = existetypeOfExpenses.FirstOrDefault();

                if (resultType == null)
                {
                    response.success = false;
                    response.AddError("No existe el tipo de gastos", $"No existe tipo de gastos con el id{request.TypesOfExpensesId} solicitado", 3);
                    return response;
                }

                //Verificar si el departamento tiene valor y existe
                if (request.DepartmentId.HasValue)
                {
                    var exists = await _unitOfWork.Departaments.GetById(request.DepartmentId.Value);
                    if(exists == null)
                    {
                        response.success = false;
                        response.AddError("No existe departamento", $"No existe departamento con el id{request.DepartmentId.Value} solicitado", 7);
                        return response;
                    }
                }

                //Mapear entidad
                var entity = _mapper.Map<Expenses>(request);
                entity.Vehicles = dtoexpens;

                //Verificar que exista el report
                if (request.VehicleReportId.HasValue)
                {
                    var report = await _unitOfWork.VehicleReportRepo.GetById(request.VehicleReportId.Value);

                    if (report == null)
                    {

                        response.success = false;
                        response.AddError("Reporte no encontrado", $"El reporte con ID {request.VehicleReportId.Value} solicitado no existe", 4);
                        return response;
                    }
                    entity.VehicleReport = report;
                }

                if (request.VehicleMaintenanceWorkshopId.HasValue)
                {
                    var existeworkshops = await _unitOfWork.MaintenanceWorkshopRepo.Get(v => v.Id == request.VehicleMaintenanceWorkshopId);
                    var resultworkshop = existeworkshops.FirstOrDefault();

                    if (resultworkshop == null)
                    {
                        response.success = false;
                        response.AddError("No existe el taller", $"No existe taller con el id{request.VehicleMaintenanceWorkshopId} solicitado", 5);
                        return response;
                    }

                    entity.VehicleMaintenanceWorkshop = resultworkshop;
                }

                //Agregar gasto a BD
                entity.ERPFolio = Guid.NewGuid().ToString();
                await _unitOfWork.ExpensesRepo.Add(entity);

                //Verificar que contenga imagenes
                //Guardar las fotos
                var images = new List<PhotosOfSpending>();

                foreach (var photo in request.Attachments)
                {
                    //Validar imagenes y Guardar las imagenes en el blobstorage
                    if (photo.ContentType.Contains("image"))
                    {
                        //Manipular el nombre de archivo
                        var uploadDate = DateTime.UtcNow;
                        Random rndm = new Random();
                        string FileExtn = System.IO.Path.GetExtension(photo.FileName);
                        var filePath = $"{entity.Id}/{uploadDate.Day}{uploadDate.Month}{uploadDate.Year}_{entity.TypesOfExpensesId}{rndm.Next(1, 1000)}{FileExtn}";
                        var uploadedUrl = await _blobStorageService.UploadFileToBlobAsync(photo, _azureBlobContainers.Value.ExpenseAttachments, filePath);

                        //Agregar la imagen en BD
                        var newImage = new PhotosOfSpending()
                        {
                            FilePath = filePath,
                            FileURL = await _blobStorageService.GetFileUrl(_azureBlobContainers.Value.ExpenseAttachments, filePath),
                            Expenses = entity
                        };

                        await _unitOfWork.PhotosOfSpendingRepo.Add(newImage);
                        images.Add(newImage);
                    }
                    else
                    {
                        response.success = false;
                        response.AddError("Archivo de Imagen Invalido", "Uno o mas archivos no corresponden a un archivo de Imagen", 6);

                        return response;
                    }
                }

                await _unitOfWork.SaveChangesAsync();
                response.success = true;
                var expensesDto = _mapper.Map<ExpensesDto>(entity);
                var imagesDto = _mapper.Map<List<PhotosOfSpendingDto>>(images);
                expensesDto.PhotosOfSpending.AddRange(imagesDto);
                response.Data = expensesDto;
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        public async Task<GenericResponse<ExpensesDto>> GetExpensesById(int id)
        {
            GenericResponse<ExpensesDto> response = new GenericResponse<ExpensesDto>();
            var entity = await _unitOfWork.ExpensesRepo.Get(filter: a => a.Id == id, includeProperties: "Vehicles,PhotosOfSpending,TypesOfExpenses,VehicleReport,VehicleMaintenanceWorkshop,Department,Invoices,Policy");
            var check = entity.FirstOrDefault();
            var map = _mapper.Map<ExpensesDto>(check);
            response.success = true;
            response.Data = map;
            return response;
        }

        public async Task<GenericResponse<Expenses>> PutExpenses(ExpenseUpdateRequest request, int id)
        {

            GenericResponse<Expenses> response = new GenericResponse<Expenses>();

            try
            {
                var result = await _unitOfWork.ExpensesRepo.Get(r => r.Id == id);
                var expenses = result.FirstOrDefault();
                if (expenses == null) return null;

                if (request.TypesOfExpensesId.HasValue)
                {
                    var typeOfExpense = await _unitOfWork.TypesOfExpensesRepo.GetById(request.TypesOfExpensesId.Value);
                    if (typeOfExpense == null)
                    {
                        response.AddError("Tipo de gasto no encontrado", $"El tipo de gasto con Id {request.TypesOfExpensesId.Value} no existe", 7);
                        response.success = false;
                        return response;
                    }
                    expenses.TypesOfExpensesId = request.TypesOfExpensesId.Value;
                }

                if (request.Cost.HasValue)
                {
                    expenses.Cost = request.Cost.Value;
                }

                if (request.Invoiced.HasValue)
                {
                    expenses.Invoiced = request.Invoiced.Value;
                }

                if (request.ExpenseDate.HasValue)
                {
                    expenses.ExpenseDate = request.ExpenseDate.Value;
                }

                if (request.VehicleMaintenanceWorkshopId.HasValue)
                {
                    var workshop = await _unitOfWork.MaintenanceWorkshopRepo.GetById(request.VehicleMaintenanceWorkshopId.Value);
                    if (workshop == null)
                    {
                        response.AddError("Taller no encontrado", $"El taller con Id {request.VehicleMaintenanceWorkshopId.Value} no existe", 8);
                        response.success = false;
                        return response;
                    }
                    expenses.VehicleMaintenanceWorkshopId = request.VehicleMaintenanceWorkshopId.Value;
                }

                if (request.VehicleReportId.HasValue)
                {
                    var report = await _unitOfWork.VehicleReportRepo.GetById(request.VehicleReportId.Value);
                    if (report == null)
                    {
                        response.AddError("Reporte no encontrado", $"El reporte con Id {request.VehicleReportId.Value} no existe", 9);
                        response.success = false;
                        return response;
                    }

                    expenses.VehicleReportId = report.Id;
                }

                if (request.DepartmentId.HasValue)
                {
                    var exists = await _unitOfWork.Departaments.GetById(request.DepartmentId.Value);
                    if (exists == null)
                    {
                        response.success = false;
                        response.AddError("No existe departamento", $"No existe departamento con el id{request.DepartmentId.Value} solicitado", 7);
                        return response;
                    }
                    expenses.DepartmentId = request.DepartmentId.Value;
                }

                expenses.Comment = request.Comment;

                await _unitOfWork.ExpensesRepo.Update(expenses);
                await _unitOfWork.SaveChangesAsync();
                response.success = true;
                response.Data = expenses;
                return response;
            }
            catch (Exception ex)
            {
                response.AddError("Error", ex.Message, 1);
                response.success = false;
                return response;
            }
        }

        public async Task<GenericResponse<Expenses>> DeleteExpenses(int id)
        {
            GenericResponse<Expenses> response = new GenericResponse<Expenses>();

            try
            {
                var exp = await _unitOfWork.ExpensesRepo.GetById(id);
                if (exp == null) return null;

                //Borrar las fotos del blob
                var photos = await _unitOfWork.PhotosOfSpendingRepo.Get(filter: v => v.ExpensesId == id);

                foreach (var photo in photos)
                {
                    await _blobStorageService.DeleteFileFromBlobAsync(_azureBlobContainers.Value.ExpenseAttachments, photo.FilePath);
                    await _unitOfWork.PhotosOfSpendingRepo.Delete(photo.Id);
                }

                //Eliminar tenencia asociada en donde aplique
                var tenencyQ = await _unitOfWork.TenencyRepo.Get(t => t.ExpenseId == id);
                var tenency = tenencyQ.FirstOrDefault();
                if(tenency != null)
                {
                    tenency.ExpenseId = null;
                    await _unitOfWork.TenencyRepo.Update(tenency);
                }

                var exists = await _unitOfWork.ExpensesRepo.Delete(id);
                await _unitOfWork.SaveChangesAsync();
                var expensesdto = _mapper.Map<Expenses>(exp);
                response.success = true;
                response.Data = expensesdto;
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);

                return response;
            }
        }

        public async Task<PagedList<ExpensesDto>> GetExpenses(ExpensesFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            string properties = "Vehicles,PhotosOfSpending,TypesOfExpenses,VehicleReport,VehicleMaintenanceWorkshop,Department,Invoices,Policy";
            IEnumerable<Expenses> expenses = null;
            Expression<Func<Expenses, bool>> Query = null;

            if (filter.CreatedAfterDate.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.CreatedDate >= filter.CreatedAfterDate.Value);
                }
                else { Query = p => p.CreatedDate >= filter.CreatedAfterDate.Value; }
            }

            if (filter.CreatedBeforeDate.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.CreatedDate <= filter.CreatedBeforeDate.Value);
                }
                else { Query = p => p.CreatedDate <= filter.CreatedBeforeDate.Value; }
            }

            if (filter.TypesOfExpensesId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.TypesOfExpensesId == filter.TypesOfExpensesId.Value);
                }
                else { Query = p => p.TypesOfExpensesId == filter.TypesOfExpensesId.Value; }
            }

            if (filter.Cost.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Cost == filter.Cost.Value);
                }
                else { Query = p => p.Cost == filter.Cost.Value; }
            }

            if (filter.Invoiced.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Invoiced == filter.Invoiced.Value);
                }
                else { Query = p => p.Invoiced == filter.Invoiced.Value; }
            }

            if (filter.ExpenseDate.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.ExpenseDate == filter.ExpenseDate.Value);
                }
                else { Query = p => p.ExpenseDate == filter.ExpenseDate.Value; }
            }

            if (filter.VehicleId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Vehicles.Any(v => v.Id == filter.VehicleId));
                }
                else { Query = p => p.Vehicles.Any(v => v.Id == filter.VehicleId); }
            }

            if (filter.InvoicesId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Invoices.Any(v => v.Id == filter.InvoicesId));
                }
                else { Query = p => p.Invoices.Any(v => v.Id == filter.InvoicesId); }
            }

            if (filter.VehicleReportId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.VehicleReportId == filter.VehicleReportId.Value);
                }
                else { Query = p => p.VehicleReportId == filter.VehicleReportId.Value; }
            }

            if (filter.VehicleMaintenanceWorkshopId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.VehicleMaintenanceWorkshopId == filter.VehicleMaintenanceWorkshopId.Value);
                }
                else { Query = p => p.VehicleMaintenanceWorkshopId == filter.VehicleMaintenanceWorkshopId.Value; }
            }

            if (filter.VehicleMaintenanceId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.VehicleMaintenanceId == filter.VehicleMaintenanceId.Value);
                }
                else { Query = p => p.VehicleMaintenanceId == filter.VehicleMaintenanceId.Value; }
            }

            if (filter.VehicleMaintenanceId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.VehicleMaintenanceId == filter.VehicleMaintenanceId.Value);
                }
                else { Query = p => p.VehicleMaintenanceId == filter.VehicleMaintenanceId.Value; }
            }

            if (!string.IsNullOrEmpty(filter.ERPFolio))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.ERPFolio.Contains(filter.ERPFolio));
                }
                else { Query = p => p.ERPFolio.Contains(filter.ERPFolio); }
            }

            if (filter.DepartmentId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.DepartmentId <= filter.DepartmentId.Value);
                }
                else { Query = p => p.DepartmentId <= filter.DepartmentId.Value; }
            }

            if (Query != null)
            {
                expenses = await _unitOfWork.ExpensesRepo.Get(filter: Query, includeProperties: properties);
            }
            else
            {
                expenses = await _unitOfWork.ExpensesRepo.Get(includeProperties: properties);
            }

            //Eliminar recursión usando DTOs
            var dtos = _mapper.Map<IEnumerable<ExpensesDto>>(expenses);

            var pagedExpenses = PagedList<ExpensesDto>.Create(dtos, filter.PageNumber, filter.PageSize);

            return pagedExpenses;
        }

        public async Task<GenericResponse<PhotosOfSpending>> AddExpenseAttachment(ExpensePhotoRequest request, int expenseId)
        {
            GenericResponse<PhotosOfSpending> response = new GenericResponse<PhotosOfSpending>();

            try
            {
                //Verificar que exista el vehiculo
                var expenseQuery = await _unitOfWork.ExpensesRepo.Get(e => e.Id == expenseId, includeProperties: "PhotosOfSpending,TypesOfExpenses,VehicleReport,VehicleMaintenanceWorkshop");
                var expense = expenseQuery.FirstOrDefault();
                if (expense == null) return null;

                if (request.ImageFile.ContentType.Contains("image"))
                {
                    //Manipular el nombre de archivo
                    var uploadDate = DateTime.UtcNow;
                    Random rndm = new Random();
                    string FileExtn = System.IO.Path.GetExtension(request.ImageFile.FileName);
                    var filePath = $"{expense.Id}/{uploadDate.Day}{uploadDate.Month}{uploadDate.Year}_{expense.TypesOfExpensesId}{rndm.Next(1, 1000)}{FileExtn}";
                    var uploadedUrl = await _blobStorageService.UploadFileToBlobAsync(request.ImageFile, _azureBlobContainers.Value.ExpenseAttachments, filePath);

                    //Agregar la imagen en BD
                    var newImage = new PhotosOfSpending()
                    {
                        FilePath = filePath,
                        FileURL = await _blobStorageService.GetFileUrl(_azureBlobContainers.Value.RegisteredCars, filePath),
                        Expenses = expense
                    };

                    await _unitOfWork.PhotosOfSpendingRepo.Add(newImage);
                    await _unitOfWork.SaveChangesAsync();

                    response.success = true;
                    response.Data = newImage;

                    return response;
                }
                else
                {
                    response.success = false;
                    response.AddError("Archivo de Imagen Invalido", "Uno o mas archivos no corresponden a un archivo de Imagen", 10);

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

        public async Task<GenericResponse<bool>> DeleteExpenseAttachment(int expenseImageId)
        {
            GenericResponse<bool> response = new GenericResponse<bool>();

            try
            {
                //Borrar las fotos del blob
                var photos = await _unitOfWork.PhotosOfSpendingRepo.GetById(expenseImageId);
                if (photos == null) return null;

                await _blobStorageService.DeleteFileFromBlobAsync(_azureBlobContainers.Value.ExpenseAttachments, photos.FilePath);
                await _unitOfWork.PhotosOfSpendingRepo.Delete(photos.Id);
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

        //CREAR POLIZA APARTIR DE UN GASTO
        public async Task<GenericResponse<ExpensesDto>> PolicyExpense(PolicyExpenseRequest request)
        {
            GenericResponse<ExpensesDto> response = new GenericResponse<ExpensesDto>();
            try
            {
                //Verificar que si exista el vehiculo especificado
                var vehicle = await _unitOfWork.VehicleRepo.GetById(request.VehicleId);
                if(vehicle == null)
                {
                    response.success=false;
                    response.AddError("Vehiculo no encontrado", $"El vehiculo con Id {request.VehicleId}", 2);
                    return response;
                }

                //Si se envio un departamento verificar su existencia
                if(request.DepartmentId.HasValue)
                {
                    var department = await _unitOfWork.Departaments.GetById(request.DepartmentId.Value);
                    if(department == null)
                    {
                        response.success = false;
                        response.AddError("Departamento no encontrado", $"El Departamento con Id {request.DepartmentId.Value}", 3);
                        return response;
                    }
                }

                //Buscar el tipo de gasto asociado a polizas
                var typeOfExp = await _unitOfWork.TypesOfExpensesRepo.Get(e => e.Name.ToUpper() == "POLIZAS");
                var exist = typeOfExp.FirstOrDefault();
                if(exist == null)
                {
                    response.success = false;
                    response.AddError("Tipo de gasto no encontrado", $"El Tipo de gasto Polizas no se encuentra en el sistema", 4);
                    return response;
                }

                //Verificar si ya existe una poliza con el vehiculo asignado
                var oldPolicyQ = await _unitOfWork.PolicyRepo.Get(p => p.CurrentVehicleId == vehicle.Id);
                var oldPolicy = oldPolicyQ.LastOrDefault();
                if(oldPolicy != null)
                {
                    oldPolicy.CurrentVehicleId = null;
                    oldPolicy.Vehicle = null;

                    await _unitOfWork.PolicyRepo.Update(oldPolicy);
                }

                //Eliminar la poliza si el coche ya cuenta con una
                vehicle.Policy = null;
                vehicle.PolicyId = null;

                await _unitOfWork.VehicleRepo.Update(vehicle);

                //Generar Poliza
                var newPolicy = new Policy
                {
                    CurrentVehicleId = vehicle.Id,
                    CurrentVehicle = vehicle,
                    VehicleId = vehicle.Id,
                    Vehicle = vehicle,
                    PolicyNumber = request.PolicyNumber,
                    ExpirationDate = request.PolicyExpirationDate,
                    NameCompany = request.PolicyCompanyName,
                    PolicyCostValue = request.Cost
                };

                //Generar el gasto
                var newExpense = new Expenses
                {
                    Invoiced = request.Invoiced,
                    ExpenseDate = request.ExpenseDate,
                    Comment = request.Comment,
                    Cost = request.Cost,
                    TypesOfExpenses = exist,
                    DepartmentId = request.DepartmentId ?? null,
                    ERPFolio = Guid.NewGuid().ToString(),
                    Policy = newPolicy
                };

                //Asignar el auto al gasto
                newExpense.Vehicles.Add(vehicle);

                //Guardar las imagenes de la poliza
                foreach (var image in request.PolicyAttachments)
                {
                    //Validar Imagenes y Guardar las imagenes en el blobstorage
                    if (image.ContentType.Contains("pdf"))
                    {
                        //Manipular el nombre del archivo
                        var uploadDate = DateTime.Now;
                        Random rndm = new Random();
                        string FileExtn = System.IO.Path.GetExtension(image.FileName);
                        var filePath = $"FOTOS_POLIZA/{uploadDate.Day}{uploadDate.Month}{uploadDate.Year}_{uploadDate.Hour}{uploadDate.Minute}{rndm.Next(1, 1000)}{FileExtn}";
                        var uploadedUrl = await _blobStorageService.UploadFileToBlobAsync(image, _azureBlobContainers.Value.PolicyImages, filePath);

                        //agregar la imagen a la bd
                        var newImage = new PhotosOfPolicy()
                        {
                            FilePath = filePath,
                            FileURL = await _blobStorageService.GetFileUrl(_azureBlobContainers.Value.PolicyImages, filePath)
                        };

                        newPolicy.PhotosOfPolicies.Add(newImage);
                    }
                    else
                    {
                        response.success = false;
                        response.AddError("Archivo de Imagen Invalido", "Uno o mas archivos no corresponden a un archivo PDF", 5);

                        return response;
                    }

                }

                //Guardar las imagenes del gasto
                foreach (var photo in request.ExpenseAttachments)
                {
                    //Validar imagenes y Guardar las imagenes en el blobstorage
                    if (photo.ContentType.Contains("image"))
                    {
                        //Manipular el nombre de archivo
                        var uploadDate = DateTime.UtcNow;
                        Random rndm = new Random();
                        string FileExtn = System.IO.Path.GetExtension(photo.FileName);
                        var filePath = $"Expense_Policy/{uploadDate.Day}{uploadDate.Month}{uploadDate.Year}_{exist.Id}{rndm.Next(1, 1000)}{FileExtn}";
                        var uploadedUrl = await _blobStorageService.UploadFileToBlobAsync(photo, _azureBlobContainers.Value.ExpenseAttachments, filePath);

                        //Agregar la imagen en BD
                        var newImage = new PhotosOfSpending()
                        {
                            FilePath = filePath,
                            FileURL = await _blobStorageService.GetFileUrl(_azureBlobContainers.Value.ExpenseAttachments, filePath)
                        };

                        newExpense.PhotosOfSpending.Add(newImage);
                    }
                    else
                    {
                        response.success = false;
                        response.AddError("Archivo de Imagen Invalido", "Uno o mas archivos no corresponden a un archivo de Imagen", 6);

                        return response;
                    }
                }

                await _unitOfWork.ExpensesRepo.Add(newExpense);
                await _unitOfWork.SaveChangesAsync();

                response.success = true;
                response.Data = _mapper.Map<ExpensesDto>(newExpense);
                return response;
            }
            catch(Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }
        
        public async Task<GenericResponse<ExpensesDto>> TenencyExpense(TenecyExpenseRequest request)
        {
            GenericResponse<ExpensesDto> response = new GenericResponse<ExpensesDto>();
            try
            {
                //Verificar que si exista el vehiculo especificado
                var vehicle = await _unitOfWork.VehicleRepo.GetById(request.VehicleId);
                if (vehicle == null)
                {
                    response.success = false;
                    response.AddError("Vehiculo no encontrado", $"El vehiculo con Id {request.VehicleId}", 2);
                    return response;
                }

                //Si se envio un departamento verificar su existencia
                if (request.DepartmentId.HasValue)
                {
                    var department = await _unitOfWork.Departaments.GetById(request.DepartmentId.Value);
                    if (department == null)
                    {
                        response.success = false;
                        response.AddError("Departamento no encontrado", $"El Departamento con Id {request.DepartmentId.Value}", 3);
                        return response;
                    }
                }

                //Buscar el tipo de gasto asociado a polizas
                var typeOfExp = await _unitOfWork.TypesOfExpensesRepo.Get(e => e.Name.ToUpper() == "TENENCIA");
                var exist = typeOfExp.FirstOrDefault();
                if (exist == null)
                {
                    response.success = false;
                    response.AddError("Tipo de gasto no encontrado", $"El Tipo de gasto Tenencia no se encuentra en el sistema", 4);
                    return response;
                }


                //Generar el gasto
                var newExpense = new Expenses
                {
                    Invoiced = request.Invoiced,
                    ExpenseDate = request.ExpenseDate,
                    Comment = request.Comment,
                    Cost = request.Cost,
                    TypesOfExpenses = exist,
                    DepartmentId = request.DepartmentId ?? null,
                    ERPFolio = Guid.NewGuid().ToString()
                };

                //Asignar el auto al gasto
                newExpense.Vehicles.Add(vehicle);

                //Agregar fecha de pago de tenencia al vehiculo
                vehicle.TenencyPaymentDate = new DateTime(request.TenencyYear,1,1);

                //Guardar el gasto
                await _unitOfWork.VehicleRepo.Update(vehicle);
                
                //Guardar las imagenes del gasto
                foreach (var photo in request.ExpenseAttachments)
                {
                    //Validar imagenes y Guardar las imagenes en el blobstorage
                    if (photo.ContentType.Contains("image"))
                    {
                        //Manipular el nombre de archivo
                        var uploadDate = DateTime.UtcNow;
                        Random rndm = new Random();
                        string FileExtn = System.IO.Path.GetExtension(photo.FileName);
                        var filePath = $"Expense_Policy/{uploadDate.Day}{uploadDate.Month}{uploadDate.Year}_{exist.Id}{rndm.Next(1, 1000)}{FileExtn}";
                        var uploadedUrl = await _blobStorageService.UploadFileToBlobAsync(photo, _azureBlobContainers.Value.ExpenseAttachments, filePath);

                        //Agregar la imagen en BD
                        var newImage = new PhotosOfSpending()
                        {
                            FilePath = filePath,
                            FileURL = await _blobStorageService.GetFileUrl(_azureBlobContainers.Value.ExpenseAttachments, filePath)
                        };

                        newExpense.PhotosOfSpending.Add(newImage);
                    }
                    else
                    {
                        response.success = false;
                        response.AddError("Archivo de Imagen Invalido", "Uno o mas archivos no corresponden a un archivo de Imagen", 6);

                        return response;
                    }
                }

                await _unitOfWork.ExpensesRepo.Add(newExpense);
                await _unitOfWork.SaveChangesAsync();

                //Crear registro de tenencia
                var newTenency = new VehicleTenency()
                {
                    ExpenseId = newExpense.Id,
                    TenencyPaymentDate = request.ExpenseDate,
                    TenencyCost = request.Cost,
                    TenencyYear = request.TenencyYear,
                    VehicleId = vehicle.Id
                };

                //Guardar la tenencia
                await _unitOfWork.TenencyRepo.Add(newTenency);
                await _unitOfWork.SaveChangesAsync();

                response.success = true;
                response.Data = _mapper.Map<ExpensesDto>(newExpense);
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        public async Task<GenericResponse<ExpensesDto>> PlatesExpense(PlateExpenseRequest request)
        {
            GenericResponse<ExpensesDto> response = new GenericResponse<ExpensesDto>();
            try
            {
                //Verificar que si exista el vehiculo especificado
                var vehicle = await _unitOfWork.VehicleRepo.GetById(request.VehicleId);
                if (vehicle == null)
                {
                    response.success = false;
                    response.AddError("Vehiculo no encontrado", $"El vehiculo con Id {request.VehicleId}", 2);
                    return response;
                }

                //Si se envio un departamento verificar su existencia
                if (request.DepartmentId.HasValue)
                {
                    var department = await _unitOfWork.Departaments.GetById(request.DepartmentId.Value);
                    if (department == null)
                    {
                        response.success = false;
                        response.AddError("Departamento no encontrado", $"El Departamento con Id {request.DepartmentId.Value}", 3);
                        return response;
                    }
                }

                //Buscar el tipo de gasto asociado a polizas
                var typeOfExp = await _unitOfWork.TypesOfExpensesRepo.Get(e => e.Name.ToUpper() == "PLACAS");
                var exist = typeOfExp.FirstOrDefault();
                if (exist == null)
                {
                    response.success = false;
                    response.AddError("Tipo de gasto no encontrado", $"El Tipo de gasto Tenencia no se encuentra en el sistema", 4);
                    return response;
                }

                //Generar el gasto
                var newExpense = new Expenses
                {
                    Invoiced = request.Invoiced,
                    ExpenseDate = request.ExpenseDate,
                    Comment = request.Comment,
                    Cost = request.Cost,
                    TypesOfExpenses = exist,
                    DepartmentId = request.DepartmentId ?? null,
                    ERPFolio = Guid.NewGuid().ToString()
                };

                //Asignar el auto al gasto
                newExpense.Vehicles.Add(vehicle);

                //Agregar fecha de pago de tenencia al vehiculo
                vehicle.PlatePaymentDate = request.ExpenseDate;
                vehicle.CarRegistrationPlate = request.PlateNumber;

                //Guardar el gasto
                await _unitOfWork.VehicleRepo.Update(vehicle);

                //Guardar las imagenes del gasto
                foreach (var photo in request.ExpenseAttachments)
                {
                    //Validar imagenes y Guardar las imagenes en el blobstorage
                    if (photo.ContentType.Contains("image"))
                    {
                        //Manipular el nombre de archivo
                        var uploadDate = DateTime.UtcNow;
                        Random rndm = new Random();
                        string FileExtn = System.IO.Path.GetExtension(photo.FileName);
                        var filePath = $"Expense_Policy/{uploadDate.Day}{uploadDate.Month}{uploadDate.Year}_{exist.Id}{rndm.Next(1, 1000)}{FileExtn}";
                        var uploadedUrl = await _blobStorageService.UploadFileToBlobAsync(photo, _azureBlobContainers.Value.ExpenseAttachments, filePath);

                        //Agregar la imagen en BD
                        var newImage = new PhotosOfSpending()
                        {
                            FilePath = filePath,
                            FileURL = await _blobStorageService.GetFileUrl(_azureBlobContainers.Value.ExpenseAttachments, filePath)
                        };

                        newExpense.PhotosOfSpending.Add(newImage);
                    }
                    else
                    {
                        response.success = false;
                        response.AddError("Archivo de Imagen Invalido", "Uno o mas archivos no corresponden a un archivo de Imagen", 6);

                        return response;
                    }
                }

                await _unitOfWork.ExpensesRepo.Add(newExpense);
                await _unitOfWork.SaveChangesAsync();

                response.success = true;
                response.Data = _mapper.Map<ExpensesDto>(newExpense);
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
