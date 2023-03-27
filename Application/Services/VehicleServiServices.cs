using Application.Extensions;
using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Identity;
using Domain.Entities.Registered_Cars;
using Domain.Entities.User_Approvals;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace Application.Services
{
    public class VehicleServiServices : IVehicleServiService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PaginationOptions _paginationOptions;
        private readonly UserManager<AppUser> _userManager;

        public VehicleServiServices(IUnitOfWork unitOfWork, IMapper mapper, IOptions<PaginationOptions> options, UserManager<AppUser> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            _paginationOptions = options.Value;
            _userManager = userManager;
        }

        //GetALL
        public async Task<PagedList<VehicleServiceDto>> GetVehicleServiceAll(VehicleServiceFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            string properties = "ServiceUser,Vehicle,Expense";
            IEnumerable<VehicleService> userApprovals = null;
            Expression<Func<VehicleService, bool>> Query = null;

            if (filter.ServiceUserId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.ServiceUserId >= filter.ServiceUserId.Value);
                }
                else { Query = p => p.ServiceUserId >= filter.ServiceUserId.Value; }
            }

            if (filter.WorkShopId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.WorkShopId >= filter.WorkShopId.Value);
                }
                else { Query = p => p.WorkShopId >= filter.WorkShopId.Value; }
            }

            if (filter.VehicleId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.VehicleId >= filter.VehicleId.Value);
                }
                else { Query = p => p.VehicleId >= filter.VehicleId.Value; }
            }

            if (filter.Status.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Status == filter.Status.Value);
                }
                else { Query = p => p.Status == filter.Status.Value; }
            }

            if (filter.TypeService.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.TypeService == filter.TypeService.Value);
                }
                else { Query = p => p.TypeService == filter.TypeService.Value; }
            }

           
            if (Query != null)
            {
                userApprovals = await _unitOfWork.VehicleServiceRepo.Get(filter: Query, includeProperties: properties);
            }
            else
            {
                userApprovals = await _unitOfWork.VehicleServiceRepo.Get(includeProperties: properties);
            }

            var dtos = _mapper.Map<IEnumerable<VehicleServiceDto>>(userApprovals);

            var pagedApprovals = PagedList<VehicleServiceDto>.Create(dtos, filter.PageNumber, filter.PageSize);

            return pagedApprovals;
        }

        //GETALLBYID
        public async Task<GenericResponse<VehicleServiceDto>> GetVehicleServiceById(int Id)
        {
            GenericResponse<VehicleServiceDto> response = new GenericResponse<VehicleServiceDto>();
            var profile = await _unitOfWork.VehicleServiceRepo.Get(filter: p => p.Id == Id,includeProperties: "Vehicle,Workshop,ServiceUser,Expense");
            var result = profile.FirstOrDefault();
            var VehicleServiceDTO = _mapper.Map<VehicleServiceDto>(result);
            response.success = true;
            response.Data = VehicleServiceDTO;
            return response;
        }

        //Post
        public async Task<GenericResponse<VehicleServiceDto>> PostVehicleService(VehicleServiceRequest vehicleServiceRequest)
        {
            GenericResponse<VehicleServiceDto> response = new GenericResponse<VehicleServiceDto>();
            try
            {
                var existeVehicle = await _unitOfWork.VehicleRepo.Get(c => c.Id == vehicleServiceRequest.VehicleId);
                var resultVehicle = existeVehicle.FirstOrDefault();

                //Verificar existencia del vehiculo
                if (resultVehicle == null)
                {
                    response.success = false;
                    response.AddError("No existe Vehicle", $"No existe Vehiculo con el VehicleId {vehicleServiceRequest.VehicleId} solicitado", 2);
                    return response;
                }

                //Verificar que el vehiculo se encuentre disponible
                switch (resultVehicle.VehicleStatus)
                {
                    case VehicleStatus.INACTIVO:
                    case VehicleStatus.MANTENIMIENTO:
                    case VehicleStatus.EN_USO:
                        response.success = false;
                        response.AddError("Vehiculo no disponible", "El estatus del vehiculo no permite su uso para viajes por el momento", 3);
                        return response;
                    default:
                        break;
                }

                //Verificar existencia del taller
                var workshopExists = await _unitOfWork.MaintenanceWorkshopRepo.GetById(vehicleServiceRequest.WorkShopId);
                if (workshopExists == null)
                {
                    response.success = false;
                    response.AddError("Taller no encontrado", $"No existe un taller con el Id {vehicleServiceRequest.WorkShopId}", 4);
                    return response;
                }

                //Verificar usuario de Admin
                var user = _userManager.Users.FirstOrDefault(u => u.Id == vehicleServiceRequest.ServiceUserId);
                if(user == null)
                {
                    response.success = false;
                    response.AddError("Usuario no encontrado", "El id de usuario especificado no se encuentra", 5);
                    return response;
                }

                var entidad = _mapper.Map<VehicleService>(vehicleServiceRequest);
                entidad.InitialFuel = resultVehicle.CurrentFuel;
                entidad.InitialMileage = resultVehicle.CurrentKM;
                entidad.Status = Domain.Enums.VehicleServiceStatus.EN_CURSO;
                entidad.Workshop = workshopExists;
                entidad.Vehicle = resultVehicle;

                //Actualizar estatus del vehiculo
                resultVehicle.VehicleStatus = Domain.Enums.VehicleStatus.MANTENIMIENTO;

                await _unitOfWork.VehicleRepo.Update(resultVehicle);
                await _unitOfWork.VehicleServiceRepo.Add(entidad);
                await _unitOfWork.SaveChangesAsync();
                response.success = true;
                var VehicleServiceDTO = _mapper.Map<VehicleServiceDto>(entidad);
                response.Data = VehicleServiceDTO;
                return response;
            } catch (Exception ex) 
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
            
        }

        //PUT
        public async Task<GenericResponse<VehicleServiceDto>> PutVehicleService(VehicleServiceUpdateRequest request)
        {
            GenericResponse<VehicleServiceDto> response = new GenericResponse<VehicleServiceDto>();
            try
            {
                //Verificar existencia de la entidad
                var profile = await _unitOfWork.VehicleServiceRepo.Get(p => p.Id == request.VehicleServiceId, includeProperties: "ServiceUser");
                var result = profile.FirstOrDefault();
                if (result == null)
                {
                    response.success = false;
                    response.AddError("Orden de Servicio no encontrada", $"No se encontro la orden con Id {request.VehicleServiceId}",2);

                    return response;
                }

                //Verificar que sea editable
                if(result.Status == Domain.Enums.VehicleServiceStatus.CANCELADO)
                {
                    response.success = false;
                    response.AddError("Error al editar", "No es posible modificar el reporte puesto que se encuentra con estatus de CANCELADO",3);
                    return response;
                }

                if(request.VehicleId.HasValue)
                {
                    var existeVehicle = await _unitOfWork.VehicleRepo.Get(c => c.Id == request.VehicleId);
                    var resultVehicle = existeVehicle.FirstOrDefault();
                    if (resultVehicle == null)
                    {
                        response.success = false;
                        response.AddError("No existe Vehicle", $"No existe Vehiculo con el VehicleId {request.VehicleId} solicitado", 4);
                        return response;
                    }

                    result.Vehicle = resultVehicle;
                }

                if(request.WorkShopId.HasValue)
                {
                    var workshopExists = await _unitOfWork.MaintenanceWorkshopRepo.GetById(request.WorkShopId.Value);
                    if (workshopExists == null)
                    {
                        response.success = false;
                        response.AddError("Taller no encontrado", $"No existe un taller con el Id {request.WorkShopId}", 5);
                        return response;
                    }

                    result.Workshop = workshopExists;
                }

                if(request.ExpenseId.HasValue)
                {
                    var expense = await _unitOfWork.ExpensesRepo.GetById(request.ExpenseId.Value);
                    if(expense == null)
                    {
                        response.success = false;
                        response.AddError("Gasto no encontrado", $"El gasto con Id {request.ExpenseId.Value}", 6);

                        return response;
                    }
                    result.Expense = expense;
                }

                if(request.TypeService.HasValue) { result.TypeService = request.TypeService.Value; }

                if(request.NextService.HasValue) { result.NextService = request.NextService.Value; }

                if(request.NextServiceKM.HasValue) { result.NextServiceKM = request.NextServiceKM.Value;}

                if(request.FinalFuel.HasValue) { result.FinalFuel= request.FinalFuel.Value; }

                if(request.FinalMileage.HasValue) { result.FinalMileage = request.FinalMileage.Value; }

                if(!string.IsNullOrEmpty(request.Comment)) { result.Comment = request.Comment; }

                await _unitOfWork.VehicleServiceRepo.Update(result);
                await _unitOfWork.SaveChangesAsync();

                var VehicleServiceDTO = _mapper.Map<VehicleServiceDto>(result);
                response.success = true;
                response.Data = VehicleServiceDTO;
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        //Marcar el servicio como Finalizado
        public async Task<GenericResponse<VehicleServiceDto>> MarkAsResolved(VehicleServiceFinishRequest request)
        {
            GenericResponse<VehicleServiceDto> response = new GenericResponse<VehicleServiceDto>();
            try
            {
                //Verificar existencia de la entidad
                var serviceQuery = await _unitOfWork.VehicleServiceRepo.Get(p => p.Id == request.VehicleServiceId, includeProperties: "ServiceUser");
                var result = serviceQuery.FirstOrDefault();
                if (result == null)
                {
                    response.success = false;
                    response.AddError("Orden de Servicio no encontrada", $"No se encontro la orden con Id {request.VehicleServiceId}",2);

                    return response;
                }

                if(result.Status == Domain.Enums.VehicleServiceStatus.FINALIZADO || result.Status == Domain.Enums.VehicleServiceStatus.CANCELADO)
                {
                    response.success = false;
                    response.AddError("Estatus invalido", "El estatus de la orden de servicio no permite su edición", 3);
                    return response;
                }

                result.FinalFuel = request.FinalFuel;
                result.FinalMileage = result.FinalMileage;
                result.Status = Domain.Enums.VehicleServiceStatus.FINALIZADO;
                result.NextService = request.NextService;
                result.NextServiceKM = request.NextServiceKM;
                result.Comment = request.Comment;

                //Actualizar estatus del vehiculo
                var vehicle = await _unitOfWork.VehicleRepo.GetById(result.VehicleId);
                vehicle.VehicleStatus = Domain.Enums.VehicleStatus.ACTIVO;
                vehicle.CurrentFuel = request.FinalFuel;
                vehicle.CurrentKM = request.FinalMileage;

                //Verificar que el gasto exista
                if (request.ExpenseId.HasValue)
                {
                    var expense = await _unitOfWork.ExpensesRepo.GetById(request.ExpenseId.Value);
                    if(expense == null)
                    {
                        response.AddError("Gasto no encontrado", $"No se encontro el gasto con Id {request.ExpenseId}",4);
                        response.success=false;
                        return response;
                    }

                    result.Expense = expense;
                }

                //Guardar los cambios del vehiculo
                await _unitOfWork.VehicleRepo.Update(vehicle);
                await _unitOfWork.VehicleServiceRepo.Update(result);
                await _unitOfWork.SaveChangesAsync();
                response.success = true;
                var VehicleServiceDTO = _mapper.Map<VehicleServiceDto>(result);
                response.Data = VehicleServiceDTO;

                return response;
            }
            catch(Exception ex) 
            { 
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        //Marcar el servicio como Cancelado
        public async Task<GenericResponse<VehicleServiceDto>> MarkAsCanceled(VehicleServiceCanceledRequest request)
        {
            GenericResponse<VehicleServiceDto> response = new GenericResponse<VehicleServiceDto>();
            try
            {
                //Verificar existencia de la entidad
                var serviceQuery = await _unitOfWork.VehicleServiceRepo.Get(p => p.Id == request.VehicleServiceId, includeProperties: "ServiceUser");
                var result = serviceQuery.FirstOrDefault();
                if (result == null)
                {
                    response.success = false;
                    response.AddError("Orden de Servicio no encontrada", $"No se encontro la orden con Id {request.VehicleServiceId}",2);

                    return response;
                }

                if (result.Status == Domain.Enums.VehicleServiceStatus.FINALIZADO || result.Status == Domain.Enums.VehicleServiceStatus.CANCELADO)
                {
                    response.success = false;
                    response.AddError("Estatus invalido", "El estatus de la orden de servicio no permite su edición", 3);
                    return response;
                }

                //Modificar estatus del servicio
                result.Status = Domain.Enums.VehicleServiceStatus.CANCELADO;

                //Actualizar estatus del vehiculo
                var vehicle = await _unitOfWork.VehicleRepo.GetById(result.VehicleId);
                vehicle.VehicleStatus = Domain.Enums.VehicleStatus.ACTIVO;

                //Guardar los cambios del vehiculo
                await _unitOfWork.VehicleRepo.Update(vehicle);
                await _unitOfWork.VehicleServiceRepo.Update(result);
                await _unitOfWork.SaveChangesAsync();
                response.success = true;
                var VehicleServiceDTO = _mapper.Map<VehicleServiceDto>(result);
                response.Data = VehicleServiceDTO;

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
        public async Task<GenericResponse<VehicleServiceDto>> DeleteVehicleService(int Id)
        {
            GenericResponse<VehicleServiceDto> response = new GenericResponse<VehicleServiceDto>();
            var entidad = await _unitOfWork.VehicleServiceRepo.Get(filter: p => p.Id == Id);
            var result = entidad.FirstOrDefault();

            if(result.Status == VehicleServiceStatus.EN_CURSO)
            {
                response.success = false;
                response.AddError("Estatus invalido", "El estatus del servicio no permite su eliminación", 2);
                return response;
            }

            if (result == null)
            {
                return null;
            }
            var existe = await _unitOfWork.VehicleServiceRepo.Delete(Id);
            await _unitOfWork.SaveChangesAsync();

            response.success = true;
            return response;
        }

    }
}
