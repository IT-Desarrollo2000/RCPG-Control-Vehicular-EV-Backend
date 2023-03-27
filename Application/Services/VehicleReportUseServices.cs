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
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using System.Reflection;

namespace Application.Services
{
    public class VehicleReportUseServices : IVehicleReportUseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PaginationOptions _paginationOptions;
        private readonly UserManager<AppUser> _userManager;

        public VehicleReportUseServices(IUnitOfWork unitOfWork, IMapper mapper, IOptions<PaginationOptions> options, UserManager<AppUser> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            _paginationOptions = options.Value;
            _userManager = userManager;
        }

        //GetAll
        public async Task<PagedList<VehicleReportUseDto>> GetUseReports(VehicleReportUseFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            string properties = "Vehicle,Checklist,VehicleReport,UserProfile,AppUser,Destinations,FinishedByDriver,FinishedByAdmin";
            IEnumerable<VehicleReportUse> useReports = null;
            Expression<Func<VehicleReportUse, bool>> Query = null;

            if (filter.VehicleId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.VehicleId == filter.VehicleId.Value);
                }
                else { Query = p => p.VehicleId == filter.VehicleId.Value; }
            }

            if (filter.FinalMileage.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.FinalMileage >= filter.FinalMileage.Value);
                }
                else { Query = p => p.FinalMileage >= filter.FinalMileage.Value; }
            }

            if (filter.StatusReportUse.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.StatusReportUse == filter.StatusReportUse.Value);
                }
                else { Query = p => p.StatusReportUse == filter.StatusReportUse.Value; }
            }

            if (!string.IsNullOrEmpty(filter.Observations))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Observations.Contains(filter.Observations));
                }
                else { Query = p => p.Observations.Contains(filter.Observations); }
            }

            if (filter.ChecklistId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.ChecklistId == filter.ChecklistId.Value);
                }
                else { Query = p => p.ChecklistId == filter.ChecklistId.Value; }
            }

            if (filter.UseDate.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.UseDate == filter.UseDate.Value);
                }
                else { Query = p => p.UseDate == filter.UseDate.Value; }
            }

            if (filter.UserProfileId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.UserProfileId == filter.UserProfileId.Value);
                }
                else { Query = p => p.UserProfileId == filter.UserProfileId.Value; }
            }

            if (filter.AppUserId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.AppUserId == filter.AppUserId.Value);
                }
                else { Query = p => p.AppUserId == filter.AppUserId.Value; }
            }

            if (filter.CurrentFuelLoad.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.CurrentFuelLoad >= filter.CurrentFuelLoad.Value);
                }
                else { Query = p => p.CurrentFuelLoad >= filter.CurrentFuelLoad.Value; }
            }

            if (filter.Verification.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Verification == filter.Verification.Value);
                }
                else { Query = p => p.Verification == filter.Verification.Value; }
            }


            if (Query != null)
            {
                useReports = await _unitOfWork.VehicleReportUseRepo.Get(filter: Query, includeProperties: properties);
            }
            else
            {
                useReports = await _unitOfWork.VehicleReportUseRepo.Get(includeProperties: properties);
            }

            //Eliminar recursion usando DTOs
            var dtos = _mapper.Map<IEnumerable<VehicleReportUseDto>>(useReports);

            var pagedApprovals = PagedList<VehicleReportUseDto>.Create(dtos, filter.PageNumber, filter.PageSize);

            return pagedApprovals;

        }

        //GetAllMobile
        public async Task<PagedList<VehicleUseReportsSlimDto>> GetUseReportsMobile(VehicleReportUseFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            string properties = "Vehicle,Checklist,VehicleReport,UserProfile,AppUser,Destinations,FinishedByDriver,FinishedByAdmin";
            IEnumerable<VehicleReportUse> useReports = null;
            Expression<Func<VehicleReportUse, bool>> Query = null;

            if (filter.VehicleId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.VehicleId == filter.VehicleId.Value);
                }
                else { Query = p => p.VehicleId == filter.VehicleId.Value; }
            }

            if (filter.FinalMileage.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.FinalMileage >= filter.FinalMileage.Value);
                }
                else { Query = p => p.FinalMileage >= filter.FinalMileage.Value; }
            }

            if (filter.StatusReportUse.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.StatusReportUse == filter.StatusReportUse.Value);
                }
                else { Query = p => p.StatusReportUse == filter.StatusReportUse.Value; }
            }

            if (!string.IsNullOrEmpty(filter.Observations))
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Observations.Contains(filter.Observations));
                }
                else { Query = p => p.Observations.Contains(filter.Observations); }
            }

            if (filter.ChecklistId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.ChecklistId == filter.ChecklistId.Value);
                }
                else { Query = p => p.ChecklistId == filter.ChecklistId.Value; }
            }

            if (filter.UseDate.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.UseDate == filter.UseDate.Value);
                }
                else { Query = p => p.UseDate == filter.UseDate.Value; }
            }

            if (filter.UserProfileId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.UserProfileId == filter.UserProfileId.Value);
                }
                else { Query = p => p.UserProfileId == filter.UserProfileId.Value; }
            }

            if (filter.AppUserId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.AppUserId == filter.AppUserId.Value);
                }
                else { Query = p => p.AppUserId == filter.AppUserId.Value; }
            }

            if (filter.CurrentFuelLoad.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.CurrentFuelLoad >= filter.CurrentFuelLoad.Value);
                }
                else { Query = p => p.CurrentFuelLoad >= filter.CurrentFuelLoad.Value; }
            }

            if (filter.Verification.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Verification == filter.Verification.Value);
                }
                else { Query = p => p.Verification == filter.Verification.Value; }
            }


            if (Query != null)
            {
                useReports = await _unitOfWork.VehicleReportUseRepo.Get(filter: Query, includeProperties: properties);
            }
            else
            {
                useReports = await _unitOfWork.VehicleReportUseRepo.Get(includeProperties: properties);
            }

            //Eliminar recursion usando DTOs
            var dtos = _mapper.Map<IEnumerable<VehicleUseReportsSlimDto>>(useReports);

            var pagedApprovals = PagedList<VehicleUseReportsSlimDto>.Create(dtos, filter.PageNumber, filter.PageSize);

            return pagedApprovals;

        }

        //GETByCarId
        public async Task<GenericResponse<VehicleReportUseDto>> GetVehicleCurrentUse(int VehicleId)
        {
            GenericResponse<VehicleReportUseDto> response = new GenericResponse<VehicleReportUseDto>();
            try
            {
                var useReport = await _unitOfWork.VehicleReportUseRepo.Get(filter: p => p.VehicleId == VehicleId && (p.StatusReportUse == ReportUseType.ViajeNormal || p.StatusReportUse == ReportUseType.ViajeRapido), includeProperties: "Vehicle,Checklist,VehicleReport,UserProfile,AppUser,Destinations,FinishedByDriver,FinishedByAdmin");
                var result = useReport.LastOrDefault();

                if (result == null)
                {
                    response.success = true;
                    response.AddError("Sin viaje en curso", $"El vehiculo especificado {VehicleId} no se encuentra en viaje", 2);
                    return response;
                }

                var VehicleReportUseDto = _mapper.Map<VehicleReportUseDto>(result);
                response.success = true;
                response.Data = VehicleReportUseDto;
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        //GETBYID
        public async Task<GenericResponse<VehicleReportUseDto>> GetUseReportById(int Id)
        {
            GenericResponse<VehicleReportUseDto> response = new GenericResponse<VehicleReportUseDto>();
            var profile = await _unitOfWork.VehicleReportUseRepo.Get(filter: p => p.Id == Id, includeProperties: "Vehicle,Checklist,VehicleReport,UserProfile,AppUser,Destinations,FinishedByDriver,FinishedByAdmin");
            var result = profile.FirstOrDefault();

            if (result == null)
            {
                response.success = true;
                response.AddError("No existe VehicleReportUse", $"No existe ReportUse con el Id {Id} solicitado ",2);
                return response;
            }


            var VehicleReportUseDto = _mapper.Map<VehicleReportUseDto>(result);
            response.success = true;
            response.Data = VehicleReportUseDto;
            return response;
        }

        //PostViajeNormal
        public async Task<GenericResponse<VehicleReportUseDto>> UseNormalTravel(VehicleReportUseProceso request)
        {
            GenericResponse<VehicleReportUseDto> response = new GenericResponse<VehicleReportUseDto>();

            try
            {
                //Verificar que el vehiculo exista
                var vehicleExists = await _unitOfWork.VehicleRepo.GetById(request.VehicleId);
                if (vehicleExists == null)
                {
                    response.success = false;
                    response.AddError("Vehiculo no encontrado", $"No existe Vehiculo con el Id {request.VehicleId} solicitado", 2);
                    return response;
                }

                //Obtener el usuario que realiza el viaje
                var userExists = await _unitOfWork.UserProfileRepo.GetById(request.UserProfileId);
                if (userExists == null)
                {
                    response.success = false;
                    response.AddError("Usuario Incorrecto", $"No existe usuario con el Id {request.UserProfileId}", 3);
                    return response;
                }

                //Verificar que el vehiculo se encuentre disponible
                switch (vehicleExists.VehicleStatus)
                {
                    case VehicleStatus.INACTIVO:
                    case VehicleStatus.MANTENIMIENTO:
                    case VehicleStatus.EN_USO:
                    case VehicleStatus.APARTADO:
                        response.success = false;
                        response.AddError("Vehiculo no disponible", "El estatus del vehiculo no permite su uso para viajes por el momento", 4);
                        return response;
                    default:
                        break;
                }

                //Verificar que el conductor cuente con una licencia valida
                if(userExists.LicenceExpirationDate <= DateTime.UtcNow)
                {
                    response.success = false;
                    response.AddError("Licencia Expirada", "La licencia del usuario se encuentra expirada", 5);
                    return response;
                }

                //Verificar que el tipo de licencia sea la adecuada para el vehiculo
                var canDrive = CanDriveVehicle(userExists.LicenceType ?? LicenceType.AUTOMOVILISTA, vehicleExists.VehicleType);
                if (!canDrive)
                {
                    response.success = false;
                    response.AddError("Licencia Invalida", $"La licencia del usuario {userExists.LicenceType} no permite el manejo de este tipo de vehiculo {vehicleExists.VehicleType}", 6);
                    return response;
                }

                //Asignar los datos del vehiculo
                var newUseReport = _mapper.Map<VehicleReportUse>(request);
                newUseReport.StatusReportUse = ReportUseType.ViajeNormal;
                newUseReport.InitialMileage = request.InitialMileage ?? Convert.ToInt32(vehicleExists.CurrentKM);
                newUseReport.CurrentFuelLoad = request.CurrentFuelLoad ?? vehicleExists.CurrentFuel;
                newUseReport.UserProfile = userExists;
                newUseReport.UseDate = request.UseDate;
                newUseReport.Verification = false;

                //Modificar el estatus del vehiculo
                vehicleExists.VehicleStatus = VehicleStatus.EN_USO;
                vehicleExists.IsClean = request.IsVehicleClean ?? vehicleExists.IsClean;

                //Agregar el checklist al vehiculo
                if(request.InitialCheckList != null)
                {
                    newUseReport.InitialCheckList.VehicleId = vehicleExists.Id;
                } else
                {
                    //Consultar el checklist del ultimo reporte de uso disponible
                    var lasUseReportquery = await _unitOfWork.VehicleReportUseRepo.Get(r => r.StatusReportUse == ReportUseType.Finalizado, includeProperties: "Checklist");
                    var lastUseReport = lasUseReportquery.LastOrDefault();

                    if (lastUseReport != null)
                    {
                        //Obtener el checklist final del ultimo reporte de uso
                        newUseReport.InitialCheckList = lastUseReport.Checklist;
                    } else
                    {
                        //Obtener el ultimo checklist del vehiculo disponible
                        var lastCheckListQuery = await _unitOfWork.ChecklistRepo.Get(c => c.VehicleId == vehicleExists.Id);
                        var lastCheckList = lastCheckListQuery.LastOrDefault();

                        if(lastCheckList != null)
                        {
                            newUseReport.InitialCheckList = lastCheckList;
                        } else
                        {
                            var newCheckList = new Checklist();
                            newCheckList.Vehicle = vehicleExists;
                            newUseReport.InitialCheckList = newCheckList;
                        }
                    }
                }

                //Guardar Cambios
                await _unitOfWork.VehicleRepo.Update(vehicleExists);
                await _unitOfWork.VehicleReportUseRepo.Add(newUseReport);
                await _unitOfWork.SaveChangesAsync();

                response.success = true;
                var dto = _mapper.Map<VehicleReportUseDto>(newUseReport);
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

        //PostViajeRapido
        public async Task<GenericResponse<VehicleReportUseDto>> UseFastTravel(UseReportFastTravelRequest request)
        {
            GenericResponse<VehicleReportUseDto> response = new GenericResponse<VehicleReportUseDto>();
            try
            {
                //Verificar que el vehiculo exista
                var vehicleExists = await _unitOfWork.VehicleRepo.GetById(request.VehicleId);
                if (vehicleExists == null)
                {
                    response.success = false;
                    response.AddError("Vehiculo no encontrado", $"No existe Vehiculo con el Id {request.VehicleId} solicitado", 2);
                    return response;
                }

                //Obtener el usuario que realiza el viaje
                var userExists = await _unitOfWork.UserProfileRepo.GetById(request.UserProfileId);
                if (userExists == null)
                {
                    response.success = false;
                    response.AddError("Usuario Incorrecto", $"No existe usuario con el Id {request.UserProfileId}", 3);
                    return response;
                }

                //Verificar que el vehiculo se encuentre disponible
                switch (vehicleExists.VehicleStatus)
                {
                    case VehicleStatus.INACTIVO:
                    case VehicleStatus.MANTENIMIENTO:
                    case VehicleStatus.EN_USO:
                    case VehicleStatus.APARTADO:
                        response.success = false;
                        response.AddError("Vehiculo no disponible", "El estatus del vehiculo no permite su uso para viajes por el momento", 4);
                        return response;
                    default:
                        break;
                }

                //Verificar que el conductor cuente con una licencia valida
                if (userExists.LicenceExpirationDate <= DateTime.UtcNow)
                {
                    response.success = false;
                    response.AddError("Licencia Expirada", "La licencia del usuario se encuentra expirada", 5);
                    return response;
                }

                //Verificar que el tipo de licencia sea la adecuada para el vehiculo
                var canDrive = CanDriveVehicle(userExists.LicenceType ?? LicenceType.AUTOMOVILISTA, vehicleExists.VehicleType);
                if (!canDrive)
                {
                    response.success = false;
                    response.AddError("Licencia Invalida", $"La licencia del usuario {userExists.LicenceType} no permite el manejo de este tipo de vehiculo {vehicleExists.VehicleType}", 6);
                    return response;
                }

                //Asignar los datos del vehiculo
                var newUseReport = _mapper.Map<VehicleReportUse>(request);
                newUseReport.StatusReportUse = ReportUseType.ViajeRapido;
                newUseReport.InitialMileage = Convert.ToInt32(vehicleExists.CurrentKM);
                newUseReport.CurrentFuelLoad = vehicleExists.CurrentFuel;
                newUseReport.UserProfile = userExists;
                newUseReport.UseDate = DateTime.UtcNow;
                newUseReport.Verification = false;

                //Modificar el estatus del vehiculo
                vehicleExists.VehicleStatus = VehicleStatus.EN_USO;
                vehicleExists.IsClean = vehicleExists.IsClean;

                //Agregar el checklist al vehiculo
                //Consultar el checklist del ultimo reporte de uso disponible
                var lasUseReportquery = await _unitOfWork.VehicleReportUseRepo.Get(r => r.StatusReportUse == ReportUseType.Finalizado, includeProperties: "Checklist");
                var lastUseReport = lasUseReportquery.LastOrDefault();

                if (lastUseReport != null)
                {
                    //Obtener el checklist final del ultimo reporte de uso
                    newUseReport.InitialCheckList = lastUseReport.Checklist;
                }
                else
                {
                    //Obtener el ultimo checklist del vehiculo disponible
                    var lastCheckListQuery = await _unitOfWork.ChecklistRepo.Get(c => c.VehicleId == vehicleExists.Id);
                    var lastCheckList = lastCheckListQuery.LastOrDefault();

                    if (lastCheckList != null)
                    {
                        newUseReport.InitialCheckList = lastCheckList;
                    }
                    else
                    {
                        var newCheckList = new Checklist();
                        newCheckList.Vehicle = vehicleExists;
                        newUseReport.InitialCheckList = newCheckList;
                    }
                }

                //Guardar Cambios
                await _unitOfWork.VehicleRepo.Update(vehicleExists);
                await _unitOfWork.VehicleReportUseRepo.Add(newUseReport);
                await _unitOfWork.SaveChangesAsync();

                response.success = true;
                var dto = _mapper.Map<VehicleReportUseDto>(newUseReport);
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

        //Viaje creado por Administración
        public async Task<GenericResponse<VehicleReportUseDto>> UseAdminTravel(UseReportAdminRequest request)
        {
            GenericResponse<VehicleReportUseDto> response = new GenericResponse<VehicleReportUseDto>();

            try
            {
                //Verificar que el vehiculo exista
                var vehicleExists = await _unitOfWork.VehicleRepo.GetById(request.VehicleId);
                if (vehicleExists == null)
                {
                    response.success = false;
                    response.AddError("Vehiculo no encontrado", $"No existe Vehiculo con el Id {request.VehicleId} solicitado", 2);
                    return response;
                }

                //Obtener el usuario que realiza el viaje
                var userExists = await _userManager.Users.Where(u => u.Id == request.AdminUserId).FirstOrDefaultAsync();
                if (userExists == null)
                {
                    response.success = false;
                    response.AddError("Admin Incorrecto", $"No existe usuario de Admin con el Id {request.AdminUserId}", 3);
                    return response;
                }

                //Verificar que el vehiculo se encuentre disponible
                switch (vehicleExists.VehicleStatus)
                {
                    case VehicleStatus.INACTIVO:
                    case VehicleStatus.MANTENIMIENTO:
                    case VehicleStatus.EN_USO:
                    case VehicleStatus.APARTADO:
                        response.success = false;
                        response.AddError("Vehiculo no disponible", "El estatus del vehiculo no permite su uso para viajes por el momento", 4);
                        return response;
                    default:
                        break;
                }

                //Asignar los datos del vehiculo
                var newUseReport = _mapper.Map<VehicleReportUse>(request);
                newUseReport.StatusReportUse = ReportUseType.ViajeNormal;
                newUseReport.InitialMileage = request.InitialMileage ?? Convert.ToInt32(vehicleExists.CurrentKM);
                newUseReport.CurrentFuelLoad = request.CurrentFuelLoad ?? vehicleExists.CurrentFuel;
                newUseReport.AppUser = userExists;
                newUseReport.UseDate = request.UseDate;
                newUseReport.Verification = true;

                //Modificar el estatus del vehiculo
                vehicleExists.VehicleStatus = VehicleStatus.EN_USO;
                vehicleExists.IsClean = request.IsVehicleClean ?? vehicleExists.IsClean;

                //Agregar el checklist al vehiculo
                if (request.InitialCheckList != null)
                {
                    newUseReport.InitialCheckList.VehicleId = vehicleExists.Id;
                }
                else
                {
                    //Consultar el checklist del ultimo reporte de uso disponible
                    var lasUseReportquery = await _unitOfWork.VehicleReportUseRepo.Get(r => r.StatusReportUse == ReportUseType.Finalizado, includeProperties: "Checklist");
                    var lastUseReport = lasUseReportquery.LastOrDefault();

                    if (lastUseReport != null)
                    {
                        //Obtener el checklist final del ultimo reporte de uso
                        newUseReport.InitialCheckList = lastUseReport.Checklist;
                    }
                    else
                    {
                        //Obtener el ultimo checklist del vehiculo disponible
                        var lastCheckListQuery = await _unitOfWork.ChecklistRepo.Get(c => c.VehicleId == vehicleExists.Id);
                        var lastCheckList = lastCheckListQuery.LastOrDefault();

                        if (lastCheckList != null)
                        {
                            newUseReport.InitialCheckList = lastCheckList;
                        }
                        else
                        {
                            var newCheckList = new Checklist();
                            newCheckList.Vehicle = vehicleExists;
                            newUseReport.InitialCheckList = newCheckList;
                        }
                    }
                }

                //Guardar Cambios
                await _unitOfWork.VehicleRepo.Update(vehicleExists);
                await _unitOfWork.VehicleReportUseRepo.Add(newUseReport);
                await _unitOfWork.SaveChangesAsync();

                response.success = true;
                var dto = _mapper.Map<VehicleReportUseDto>(newUseReport);
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

        //Confirmación de viaje por Admin
        public async Task<GenericResponse<VehicleReportUseDto>> VerifyVehicleUse(VehicleReportUseVerificationRequest request)
        {
            GenericResponse<VehicleReportUseDto> response = new GenericResponse<VehicleReportUseDto>();
            try
            {
                //Verificar que el reporte de uso existe
                var reportQuery = await _unitOfWork.VehicleReportUseRepo.Get(p => p.Id == request.UseReportId);
                var useReport = reportQuery.FirstOrDefault();

                if (useReport == null)
                {
                    response.success = true;
                    response.AddError("No existe VehicleReportUse", $"No existe ReportUse con el Id {request.UseReportId}", 2);
                    return response;
                }

                //Confirmar que el reporte sea valido para su verificación
                if(useReport.StatusReportUse != ReportUseType.Finalizado)
                {
                    response.success = false;
                    response.AddError("Estatus invalido", "El reporte de uso no puede ser verificado si no esta FINALIZADO", 3);
                    return response;
                }

                if(useReport.AppUserId != null)
                {
                    response.success = false;
                    response.AddError("Reporte de uso verificado", "El reporte de uso ya ha sido verificado por otro usuario", 4);
                    return response;
                }

                //Verificar que el usuario de Admin Existe
                var existeAppUser = await _userManager.Users.SingleOrDefaultAsync(c => c.Id == request.AppUserId);
                if (existeAppUser == null)
                {
                    response.success = false;
                    response.AddError("No existe AppUser", $"No existe AppUserId {request.AppUserId} para cargar", 5);
                    return response;
                }



                //Modificar el reporte
                useReport.AppUserId = request.AppUserId;
                useReport.Verification = true;

                //Guardar los cambios
                await _unitOfWork.VehicleReportUseRepo.Update(useReport);
                await _unitOfWork.SaveChangesAsync();

                var dto = _mapper.Map<VehicleReportUseDto>(useReport);
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

        //Finalización del viaje
        public async Task<GenericResponse<VehicleReportUseDto>> MarkNormalTravelAsFinished(UseReportFinishRequest request)
        {
            GenericResponse<VehicleReportUseDto> response = new GenericResponse<VehicleReportUseDto>();
            try
            {
                //Verificar que el reporte de uso existe
                var reportQuery = await _unitOfWork.VehicleReportUseRepo.Get(p => p.Id == request.UseReportId, includeProperties: "InitialCheckList");
                var useReport = reportQuery.FirstOrDefault();

                if (useReport == null)
                {
                    response.success = true;
                    response.AddError("No existe VehicleReportUse", $"No existe ReportUse con el Id {request.UseReportId}", 2);
                    return response;
                }

                //Verificar que minimo contenga un usuario para finalizarlo
                if(!request.FinishedByAdminId.HasValue && !request.FinishedByDriverId.HasValue)
                {
                    response.success = false;
                    response.AddError("Usuario no especificado", "Debe especificar un usuario para finalizar el viaje", 3);
                    return response;
                }

                //Verificar que cuenta con el estatus apropiado
                switch(useReport.StatusReportUse)
                {
                    case ReportUseType.Cancelado:
                    case ReportUseType.Finalizado:
                    case ReportUseType.ViajeRapido:
                        response.success = false;
                        response.AddError("Estatus invalido", "El estatus del reporte de uso no permite su finalización", 4);
                        return response;
                    default:
                        break;
                }

                //Obtener el vehiculo para su modificación
                var vehicle = await _unitOfWork.VehicleRepo.GetById(useReport.VehicleId);

                //Asignar el checklist final
                //Agregar el checklist al vehiculo si hubo cambios
                if (request.FinalCheckList != null)
                {
                    var newCheckList = _mapper.Map<Checklist>(request.FinalCheckList);
                    newCheckList.VehicleId = useReport.VehicleId;
                    useReport.Checklist = newCheckList;
                }
                else
                {
                    //De lo contrario copiar el inicial y agregarlo como final
                    var checklist = new Checklist
                    {
                        VehicleId = useReport.VehicleId,
                        CarInsurancePolicy = useReport.InitialCheckList.CarInsurancePolicy,
                        CarJack = useReport.InitialCheckList.CarJack,
                        Extinguisher = useReport.InitialCheckList.Extinguisher,
                        HydraulicTires = useReport.InitialCheckList.HydraulicTires,
                        CarJackKey = useReport.InitialCheckList.CarJackKey,
                        CirculationCard = useReport.InitialCheckList.CirculationCard,
                        JumperCable = useReport.InitialCheckList.JumperCable,
                        SafetyTriangle = useReport.InitialCheckList.SafetyTriangle,
                        ToolBag = useReport.InitialCheckList.ToolBag,
                        TireRefurmishment = useReport.InitialCheckList.TireRefurmishment,
                        SecurityDice = useReport.InitialCheckList.SecurityDice
                    };

                    useReport.Checklist = checklist;
                }

                //Modificar datos del reporte
                useReport.FinalMileage = request.FinalMileage;
                useReport.LastFuelLoad = request.FinalFuelLoad;
                useReport.StatusReportUse = ReportUseType.Finalizado;
                useReport.Observations = request.Observations ?? "N/A";
                useReport.FinishedByAdminId = request.FinishedByAdminId ?? null;
                useReport.FinishedByDriverId = request.FinishedByDriverId ?? null;

                //Modificar el vehiculo
                vehicle.VehicleStatus = VehicleStatus.ACTIVO;
                vehicle.IsClean = request.IsVehicleClean ?? vehicle.IsClean;
                vehicle.CurrentKM = Convert.ToInt32(request.FinalMileage);
                vehicle.CurrentFuel = request.FinalFuelLoad;

                //Guardar los cambios
                await _unitOfWork.VehicleRepo.Update(vehicle);
                await _unitOfWork.VehicleReportUseRepo.Update(useReport);
                await _unitOfWork.SaveChangesAsync();

                response.success = true;
                var dto = _mapper.Map<VehicleReportUseDto>(useReport);
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

        //Finalización del viaje rapido
        public async Task<GenericResponse<VehicleReportUseDto>> MarkFastTravelAsFinished(UseReportFastTravelFinishRequest request)
        {
            GenericResponse<VehicleReportUseDto> response = new GenericResponse<VehicleReportUseDto>();
            try
            {
                //Verificar que el reporte de uso existe
                var reportQuery = await _unitOfWork.VehicleReportUseRepo.Get(p => p.Id == request.UseReportId, includeProperties: "InitialCheckList");
                var useReport = reportQuery.FirstOrDefault();

                if (useReport == null)
                {
                    response.success = true;
                    response.AddError("No existe VehicleReportUse", $"No existe ReportUse con el Id {request.UseReportId}", 2);
                    return response;
                }

                switch (useReport.StatusReportUse)
                {
                    case ReportUseType.Cancelado:
                    case ReportUseType.Finalizado:
                    case ReportUseType.ViajeNormal:
                        response.success = false;
                        response.AddError("Estatus invalido", "El estatus del reporte de uso no permite su cancelación", 3);
                        return response;
                    default:
                        break;
                }

                //Verificar que minimo contenga un usuario para finalizarlo
                if (!request.FinishedByAdminId.HasValue && !request.FinishedByDriverId.HasValue)
                {
                    response.success = false;
                    response.AddError("Usuario no especificado", "Debe especificar un usuario para finalizar el viaje", 4);
                    return response;
                }

                //Obtener el vehiculo para su modificación
                var vehicle = await _unitOfWork.VehicleRepo.GetById(useReport.VehicleId);

                //Asignar el checklist final
                //Agregar el checklist al vehiculo si hubo cambios
                if (request.FinalCheckList != null)
                {
                    var newCheckList = _mapper.Map<Checklist>(request.FinalCheckList);
                    newCheckList.VehicleId = useReport.VehicleId;
                    useReport.Checklist = newCheckList;
                }
                else
                {
                    //De lo contrario copiar el inicial y agregarlo como final
                    var checklist = new Checklist
                    {
                        VehicleId = useReport.VehicleId,
                        CarInsurancePolicy = useReport.InitialCheckList.CarInsurancePolicy,
                        CarJack = useReport.InitialCheckList.CarJack,
                        Extinguisher = useReport.InitialCheckList.Extinguisher,
                        HydraulicTires = useReport.InitialCheckList.HydraulicTires,
                        CarJackKey = useReport.InitialCheckList.CarJackKey,
                        CirculationCard = useReport.InitialCheckList.CirculationCard,
                        JumperCable = useReport.InitialCheckList.JumperCable,
                        SafetyTriangle = useReport.InitialCheckList.SafetyTriangle,
                        ToolBag = useReport.InitialCheckList.ToolBag,
                        TireRefurmishment = useReport.InitialCheckList.TireRefurmishment,
                        SecurityDice = useReport.InitialCheckList.SecurityDice
                    };

                    useReport.Checklist = checklist;
                }

                //Modificar datos del reporte
                useReport.FinalMileage = request.FinalMileage;
                useReport.LastFuelLoad = request.FinalFuelLoad;
                useReport.StatusReportUse = ReportUseType.Finalizado;
                useReport.Observations = request.Observations ?? "N/A";
                useReport.FinishedByAdminId = request.FinishedByAdminId ?? null;
                useReport.FinishedByDriverId = request.FinishedByDriverId ?? null;

                //Asignar los destinos
                //Asignar los destinos
                foreach (var destination in request.Destinations)
                {
                    var newDestination = _mapper.Map<DestinationOfReportUse>(destination);
                    useReport.Destinations.Add(newDestination);
                }

                //Modificar el vehiculo
                vehicle.VehicleStatus = VehicleStatus.ACTIVO;
                vehicle.IsClean = request.IsVehicleClean ?? vehicle.IsClean;
                vehicle.CurrentKM = Convert.ToInt32(request.FinalMileage);
                vehicle.CurrentFuel = request.FinalFuelLoad;

                //Guardar los cambios
                await _unitOfWork.VehicleRepo.Update(vehicle);
                await _unitOfWork.VehicleReportUseRepo.Update(useReport);
                await _unitOfWork.SaveChangesAsync();

                response.success = true;
                var dto = _mapper.Map<VehicleReportUseDto>(useReport);
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

        //Cancelar viaje
        public async Task<GenericResponse<VehicleReportUseDto>> MarkTravelAsCanceled(UseReportCancelRequest request)
        {
            GenericResponse<VehicleReportUseDto> response = new GenericResponse<VehicleReportUseDto>();
            try
            {
                //Verificar que el reporte de uso existe
                var reportQuery = await _unitOfWork.VehicleReportUseRepo.Get(p => p.Id == request.UseReportId, includeProperties: "InitialCheckList");
                var useReport = reportQuery.FirstOrDefault();

                if (useReport == null)
                {
                    response.success = true;
                    response.AddError("No existe VehicleReportUse", $"No existe ReportUse con el Id {request.UseReportId}", 2);
                    return response;
                }

                //Verificar que minimo contenga un usuario para finalizarlo
                if (!request.FinishedByAdminId.HasValue && !request.FinishedByDriverId.HasValue)
                {
                    response.success = false;
                    response.AddError("Usuario no especificado", "Debe especificar un usuario para finalizar el viaje", 3);
                    return response;
                }

                switch (useReport.StatusReportUse)
                {
                    case ReportUseType.Cancelado:
                    case ReportUseType.Finalizado:
                        response.success = false;
                        response.AddError("Estatus invalido", "El estatus del reporte de uso no permite su cancelación", 4);
                        return response;
                    default:
                        break;
                }

                //Obtener el vehiculo para su modificación
                var vehicle = await _unitOfWork.VehicleRepo.GetById(useReport.VehicleId);


                //Modificar datos del reporte
                useReport.StatusReportUse = ReportUseType.Cancelado;
                useReport.Observations = request.Observations ?? "N/A";
                useReport.FinishedByAdminId = request.FinishedByAdminId ?? null;
                useReport.FinishedByDriverId = request.FinishedByDriverId ?? null;

                //Modificar el vehiculo
                vehicle.VehicleStatus = VehicleStatus.ACTIVO;

                //Guardar los cambios
                await _unitOfWork.VehicleRepo.Update(vehicle);
                await _unitOfWork.VehicleReportUseRepo.Update(useReport);
                await _unitOfWork.SaveChangesAsync();

                response.success = true;
                var dto = _mapper.Map<VehicleReportUseDto>(useReport);
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

        //Modificar el reporte de uso(Solo si no esta cancelado)
        public async Task<GenericResponse<VehicleReportUseDto>> UpdateUseReport(UseReportUpdateRequest request)
        {
            GenericResponse<VehicleReportUseDto> response = new GenericResponse<VehicleReportUseDto>();
            try
            {
                //Verificar que el reporte de uso existe
                var reportQuery = await _unitOfWork.VehicleReportUseRepo.Get(p => p.Id == request.UseReportId);
                var useReport = reportQuery.FirstOrDefault();

                if (useReport == null)
                {
                    response.success = true;
                    response.AddError("No existe VehicleReportUse", $"No existe ReportUse con el Id {request.UseReportId}", 2);
                    return response;
                }

                //Verificar que cuenta con el estatus apropiado
                if (useReport.StatusReportUse == ReportUseType.Cancelado)
                {
                    response.success = false;
                    response.AddError("Estatus invalido", "El estatus del reporte de uso no permite su modificación", 3);
                    return response;
                }

                //Modificar los datos
                useReport.InitialMileage = request.InitialMileage ?? useReport.InitialMileage;
                useReport.FinalMileage = request.FinalMileage ?? useReport.FinalMileage;
                useReport.CurrentFuelLoad = request.CurrentFuelLoad ?? useReport.CurrentFuelLoad;
                useReport.LastFuelLoad = request.LastFuelLoad ?? useReport.LastFuelLoad;
                useReport.UseDate = request.UseDate ?? useReport.UseDate;
                useReport.Observations = request.Observations ?? useReport.Observations;

                //Agregar destinos adicionales
                //Asignar los destinos
                foreach (var destination in request.DestinationsToAdd)
                {
                    var newDestination = _mapper.Map<DestinationOfReportUse>(destination);
                    useReport.Destinations.Add(newDestination);
                }

                //Eliminar los destinos solicitados
                foreach (var destination in request.DestinationsToRemove)
                {
                    //Confirmar la existencia
                    var dest = await _unitOfWork.DestinationOfReportUseRepo.GetById(destination);
                    if (dest == null)
                    {
                        response.success = false;
                        response.AddError("Destino no encontrado", $"No se encontro el destino con Id {destination}", 4);
                        return response;
                    }
                    await _unitOfWork.DestinationOfReportUseRepo.Delete(destination);
                }

                //Guardar los cambios
                await _unitOfWork.VehicleReportUseRepo.Update(useReport);
                await _unitOfWork.SaveChangesAsync();

                response.success = true;
                var dto = _mapper.Map<VehicleReportUseDto>(useReport);
                response.Data = dto;

                return response;

            } catch (Exception ex)
            {
                response.success= false;
                response.AddError("Error",ex.Message, 1);
                return response;
            }
        }

        //Delete
        public async Task<GenericResponse<bool>> DeleteVehicleReportUse(int Id)
        {
            GenericResponse<bool> response = new GenericResponse<bool>();
            var entidad = await _unitOfWork.VehicleReportUseRepo.Get(filter: p => p.Id == Id);
            var result = entidad.FirstOrDefault();
            if (result == null)
            {
                return null;
            }

            await _unitOfWork.VehicleReportUseRepo.Delete(Id);
            await _unitOfWork.SaveChangesAsync();

            response.success = true;
            response.Data = true;

            return response;
        }

        //Función que indica si X licencia puede manejar Y coche
        private bool CanDriveVehicle(LicenceType licenceType, VehicleType vehicleType) 
        {
            switch (licenceType)
            {
                case LicenceType.AUTOMOVILISTA:
                    switch (vehicleType)
                    {
                        case VehicleType.VAN:
                        case VehicleType.CAMION:
                        case VehicleType.MULTIPROPOSITO:
                            return false;
                        default:
                            return true;
                    }
                case LicenceType.MOTOCICLISTA:
                    if (vehicleType == VehicleType.MOTOCICLETA) { return true; } else { return false; }
                default:
                    return true;
            }
        }
    }
}
