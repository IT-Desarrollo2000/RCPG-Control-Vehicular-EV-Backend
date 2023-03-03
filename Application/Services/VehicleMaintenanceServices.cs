﻿using Application.Extensions;
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

        public VehicleMaintenanceServices(IUnitOfWork unitOfWork, IMapper mapper, IOptions<PaginationOptions> options, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paginationOptions = options.Value;
            _userManager = userManager;
        }

        //GetAll
        public async Task<PagedList<VehicleMaintenanceDto>> GetVehicleMaintenanceAll(VehicleMaintenanceFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            string properties = "Vehicle,WorkShop,Report,ApprovedByUser";
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

        //GETBYID
        public async Task<GenericResponse<VehicleMaintenanceDto>> GetVehicleMaintenanceById(int Id)
        {
            GenericResponse<VehicleMaintenanceDto> response = new GenericResponse<VehicleMaintenanceDto>();
            var profile = await _unitOfWork.VehicleMaintenanceRepo.Get(filter: p => p.Id == Id, includeProperties: "Vehicle,WorkShop,Report,ApprovedByUser");
            var result = profile.FirstOrDefault();
            if(result == null)
            {
                response.success = false;
                response.AddError("Mantenimiento no encontrado", $"El mantenimiento con ID {Id} no existe", 1);
                return response;
            }

            var VehicleMaintenanceDTO = _mapper.Map<VehicleMaintenanceDto>(result);
            response.success = true;
            response.Data = VehicleMaintenanceDTO;
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
                        response.AddError("Vehiculo no disponible", "El estatus del vehiculo no permite su mantenimiento por el momento", 4);
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
                        response.AddError("No existe taller", $"No existe taller con el Id {request.WorkShopId} solicitado", 3);
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
                        response.AddError("No existe usuario", $"No existe usuario con el Id {request.AdminUserId} solicitado", 4);
                        return response;
                    }
                }

                //Verificar que exista el taller
                if (request.ReportId.HasValue)
                {
                    var workshop = await _unitOfWork.MaintenanceWorkshopRepo.GetById(request.ReportId.Value);
                    if (workshop == null)
                    {
                        response.success = false;
                        response.AddError("No existe reporte", $"No existe reporte con el Id {request.ReportId} solicitado", 5);
                        return response;
                    }
                }

                //Mapear Elementos
                var entidad = _mapper.Map<VehicleMaintenance>(request);
                entidad.ApprovedByUserId = request.AdminUserId;

                //Asignar los datos relacionados del vehiculo
                entidad.InitialFuel = request.InitialFuel ?? vehicleExists.CurrentFuel;
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
                maintenance.FinalFuel = request.FinalFuel;
                maintenance.FinalMileage = request.FinalMileage;
                maintenance.Comment = request.Comment ?? "N/A";

                //Asignar los datos relacionados del vehiculo
                var vehicle = await _unitOfWork.VehicleRepo.GetById(maintenance.VehicleId);
                vehicle.CurrentFuel = request.FinalFuel;
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
                        response.AddError("Taller no existe", "El taller especificado no existe", 4);
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
                        response.AddError("Reporte no existe", "El reporte especificado no existe", 4);
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
    }
}

