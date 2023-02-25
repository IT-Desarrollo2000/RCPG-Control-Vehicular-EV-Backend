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
using Microsoft.AspNetCore.Mvc;
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
        private readonly VehicleReportUseProceso _vehicleReportUseProceso;

        public VehicleReportUseServices(IUnitOfWork unitOfWork, IMapper mapper, IOptions<PaginationOptions> options, UserManager<AppUser> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            _paginationOptions = options.Value;
            _userManager = userManager;
        }

        //GetAll
        public async Task<PagedList<VehicleReportUseDto>> GetVehicleReportUseAll(VehicleReportUseFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            string properties = "Vehicle,Checklist,VehicleReport,UserProfile,AppUser,Destinations";
            IEnumerable<VehicleReportUse> useReports = null;
            Expression<Func<VehicleReportUse, bool>> Query = null;

            if (filter.VehicleId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.VehicleId >= filter.VehicleId.Value);
                }
                else { Query = p => p.VehicleId >= filter.VehicleId.Value; }
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
                    Query = Query.And(p => p.StatusReportUse >= filter.StatusReportUse.Value);
                }
                else { Query = p => p.StatusReportUse >= filter.StatusReportUse.Value; }
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
                    Query = Query.And(p => p.ChecklistId >= filter.ChecklistId.Value);
                }
                else { Query = p => p.ChecklistId >= filter.ChecklistId.Value; }
            }

            if (filter.UseDate.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.UseDate >= filter.UseDate.Value);
                }
                else { Query = p => p.UseDate >= filter.UseDate.Value; }
            }

            if (filter.UserProfileId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.UserProfileId >= filter.UserProfileId.Value);
                }
                else { Query = p => p.UserProfileId >= filter.UserProfileId.Value; }
            }

            if (filter.AppUserId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.AppUserId >= filter.AppUserId.Value);
                }
                else { Query = p => p.AppUserId >= filter.AppUserId.Value; }
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

        //GETBYID
        public async Task<GenericResponse<VehicleReportUseDto>> GetVehicleReporUseById(int Id)
        {
            GenericResponse<VehicleReportUseDto> response = new GenericResponse<VehicleReportUseDto>();
            var profile = await _unitOfWork.VehicleReportUseRepo.Get(filter: p => p.Id == Id, includeProperties: "Vehicle,Checklist,VehicleReport,UserProfile,AppUser,Destinations");
            var result = profile.FirstOrDefault();

            if (result == null)
            {
                response.success = true;
                response.AddError("No existe VehicleReportUse", $"No existe ReportUse con el Id {Id} solicitado ");
                return response;
            }


            var VehicleReportUseDto = _mapper.Map<VehicleReportUseDto>(result);
            response.success = true;
            response.Data = VehicleReportUseDto;
            return response;
        }

        //PostProceso
        public async Task<GenericResponse<VehicleReportUseDto>> PostEnProceso(VehicleReportUseProceso vehicleReportUseProceso)
        {
            GenericResponse<VehicleReportUseDto> response = new GenericResponse<VehicleReportUseDto>();
            GenericResponse<VehiclesDto> responseVehicle = new GenericResponse<VehiclesDto>();

            try
            {
                if (vehicleReportUseProceso.StatusReportUse == Domain.Enums.ReportUseType.Cancelado || vehicleReportUseProceso.StatusReportUse == Domain.Enums.ReportUseType.Finalizado)
                {
                    response.success = false;
                    response.AddError("No puedes crear reporte con la siguiente caracteristicas ->", $"No puedes crear reporte con el status {vehicleReportUseProceso.StatusReportUse} solicitado", 1);
                    return response;
                }

                var existeVehicleMaintenance = await _unitOfWork.VehicleRepo.Get(c => c.Id == vehicleReportUseProceso.VehicleId);
                var resultVehicleMaintenance = existeVehicleMaintenance.FirstOrDefault();
                if (resultVehicleMaintenance == null)
                {
                    response.success = false;
                    response.AddError("No existe Vehicle", $"No existe Vehiculo con el VehicleId {vehicleReportUseProceso.VehicleId} solicitado", 1);
                    return response;

                }


                if (vehicleReportUseProceso.VehicleId == resultVehicleMaintenance.Id)
                {
                    if (resultVehicleMaintenance.VehicleStatus == Domain.Enums.VehicleStatus.EN_USO)
                    {
                        response.success = false;
                        response.AddError("No pudes crear otro reporte de uso con este VehicleId, esta en uso actualmente", $"No puedes usar VehicleId {vehicleReportUseProceso.VehicleId} solicitado", 1);
                        return response;
                    }

                }

                if (vehicleReportUseProceso.UserProfileId.HasValue)
                {
                    var existeUserProfile = await _unitOfWork.UserProfileRepo.Get(c => c.Id == vehicleReportUseProceso.UserProfileId.Value);
                    var resultUserProfile = existeUserProfile.FirstOrDefault();

                    if (resultUserProfile == null)
                    {
                        response.success = false;
                        response.AddError("No existe UserProfile", $"No existe UserProfileId {vehicleReportUseProceso.UserProfileId} para cargar", 1);
                        return response;
                    }
                }

                if (vehicleReportUseProceso.StatusReportUse == Domain.Enums.ReportUseType.enProceso)//AQUI IBA CANCELADO ESTATUS
                {
                    vehicleReportUseProceso.StatusReportUse = Domain.Enums.ReportUseType.enProceso;
                    var existeVehicle = await _unitOfWork.VehicleRepo.Get(c => c.Id == vehicleReportUseProceso.VehicleId);
                    var resultVehicle = existeVehicle.FirstOrDefault();

                    if (vehicleReportUseProceso.StatusReportUse == Domain.Enums.ReportUseType.enProceso)
                    {
                        resultVehicle.VehicleStatus = Domain.Enums.VehicleStatus.EN_USO;
                        await _unitOfWork.VehicleRepo.Update(resultVehicle);
                        response.success = true;
                    }

                    var entidad = _mapper.Map<VehicleReportUse>(vehicleReportUseProceso);
                    entidad.InitialMileage = Convert.ToInt32(resultVehicle.CurrentKM);
                    await _unitOfWork.VehicleReportUseRepo.Add(entidad);
                    await _unitOfWork.SaveChangesAsync();


                    //Guardar destinos

                    var lastReportUse = await _unitOfWork.VehicleReportUseRepo.Get();
                    var resultLastCheck = lastReportUse.LastOrDefault();

                    var destination = new DestinationOfReportUse()
                    {
                        DestinationName = vehicleReportUseProceso.Destination.DestinationName,
                        Latitud = vehicleReportUseProceso.Destination.Latitud,
                        Longitude = vehicleReportUseProceso.Destination.Longitude,
                        VehicleReportUseId = resultLastCheck.Id
                    };

                    await _unitOfWork.DestinationOfReportUseRepo.Add(destination);
                    await _unitOfWork.SaveChangesAsync();

                   response.success = true;
                    var VehicleReportUseDTO = _mapper.Map<VehicleReportUseDto>(entidad);
                    response.Data = VehicleReportUseDTO;
                    return response;

                }
                else
                {
                    var existeVehicle = await _unitOfWork.VehicleRepo.Get(c => c.Id == vehicleReportUseProceso.VehicleId);
                    var resultVehicle = existeVehicle.FirstOrDefault();

                    var entidad = _mapper.Map<VehicleReportUse>(vehicleReportUseProceso);
                    entidad.InitialMileage = Convert.ToInt32(resultVehicleMaintenance.CurrentKM);
                    await _unitOfWork.VehicleReportUseRepo.Add(entidad);
                    await _unitOfWork.SaveChangesAsync();

                    var lastReportUse = await _unitOfWork.VehicleReportUseRepo.Get();
                    var resultLastCheck = lastReportUse.OrderByDescending(pr => pr.Id).LastOrDefault();


                    var destination = new DestinationOfReportUse()
                    {
                        DestinationName = vehicleReportUseProceso.Destination.DestinationName,
                        Latitud = vehicleReportUseProceso.Destination.Latitud,
                        Longitude = vehicleReportUseProceso.Destination.Longitude,
                        VehicleReportUseId = resultLastCheck.Id
                    };

                    await _unitOfWork.DestinationOfReportUseRepo.Add(destination);
                    await _unitOfWork.SaveChangesAsync();

                    response.success = true;
                    var VehicleReportUseDTO = _mapper.Map<VehicleReportUseDto>(entidad);
                    response.Data = VehicleReportUseDTO;
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

        //PostViajeRapido
        public async Task<GenericResponse<VehicleReportUseDto>> PostViajeRapido(VehicleReportUseFastTravel vehicleReportUseFastTravel)
        {
            GenericResponse<VehicleReportUseDto> response = new GenericResponse<VehicleReportUseDto>();
            GenericResponse<VehiclesDto> responseVehicle = new GenericResponse<VehiclesDto>();

            try
            {
                if (vehicleReportUseFastTravel.StatusReportUse == Domain.Enums.ReportUseType.Cancelado || vehicleReportUseFastTravel.StatusReportUse == Domain.Enums.ReportUseType.Finalizado)
                {
                    response.success = false;
                    response.AddError("No puedes crear reporte con la siguiente caracteristicas ->", $"No puedes crear reporte con el status {vehicleReportUseFastTravel.StatusReportUse} solicitado", 1);
                    return response;
                }


                var existeVehicleMaintenance = await _unitOfWork.VehicleRepo.Get(c => c.Id == vehicleReportUseFastTravel.VehicleId);
                var resultVehicleMaintenance = existeVehicleMaintenance.FirstOrDefault();
                if (resultVehicleMaintenance == null)
                {
                    response.success = false;
                    response.AddError("No existe Vehicle", $"No existe Vehiculo con el VehicleId {vehicleReportUseFastTravel.VehicleId} solicitado", 1);
                    return response;

                }

                if (vehicleReportUseFastTravel.VehicleId == resultVehicleMaintenance.Id)
                {
                    if (resultVehicleMaintenance.VehicleStatus == Domain.Enums.VehicleStatus.EN_USO)
                    {
                        response.success = false;
                        response.AddError("No pudes crear otro reporte de uso con este VehicleId, esta en uso actualmente", $"No puedes usar VehicleId {vehicleReportUseFastTravel.VehicleId} solicitado", 1);
                        return response;
                    }

                }

                if (vehicleReportUseFastTravel.UserProfileId.HasValue)
                {
                    var existeUserProfile = await _unitOfWork.UserProfileRepo.Get(c => c.Id == vehicleReportUseFastTravel.UserProfileId.Value);
                    var resultUserProfile = existeUserProfile.FirstOrDefault();

                    if (resultUserProfile == null)
                    {
                        response.success = false;
                        response.AddError("No existe UserProfile", $"No existe UserProfileId {vehicleReportUseFastTravel.UserProfileId} para cargar", 1);
                        return response;
                    }
                }

                if (vehicleReportUseFastTravel.StatusReportUse == Domain.Enums.ReportUseType.ViajeRapido) //AQUI IBA FINALIZADO ESTATUS
                {

                    vehicleReportUseFastTravel.StatusReportUse = Domain.Enums.ReportUseType.ViajeRapido;
                    var existeVehicle = await _unitOfWork.VehicleRepo.Get(c => c.Id == vehicleReportUseFastTravel.VehicleId);
                    var resultVehicle = existeVehicle.FirstOrDefault();

                    if (resultVehicle == null)
                    {
                        response.success = false;
                        response.AddError("No existe Vehicle, no puede ir vacio este campo para Viaje Rapido", $"No existe Vehiculo con el VehicleId {vehicleReportUseFastTravel.VehicleId} solicitado", 1);
                        return response;

                    }

                    if (vehicleReportUseFastTravel.UserProfileId == null)
                    {
                        response.success = false;
                        response.AddError("No puede ir vacio este campo, no puede ir vacio este campo para Viaje Rapido", $"Se necesita Usuario solicitado", 1);
                        return response;
                    }

                    if (vehicleReportUseFastTravel.StatusReportUse == Domain.Enums.ReportUseType.ViajeRapido)
                    {
                        resultVehicle.VehicleStatus = Domain.Enums.VehicleStatus.EN_USO;
                        await _unitOfWork.VehicleRepo.Update(resultVehicle);
                        response.success = true;
                    }

                    var entidad = _mapper.Map<VehicleReportUse>(vehicleReportUseFastTravel);
                    entidad.InitialMileage = Convert.ToInt32(resultVehicle.CurrentKM);
                    await _unitOfWork.VehicleReportUseRepo.Add(entidad);
                    await _unitOfWork.SaveChangesAsync();

                    var lastReportUse = await _unitOfWork.VehicleReportUseRepo.Get();
                    var resultLastCheck = lastReportUse.LastOrDefault();

                    var destination = new DestinationOfReportUse()
                     {
                         DestinationName = vehicleReportUseFastTravel.Destination.DestinationName,
                         Latitud = vehicleReportUseFastTravel.Destination.Latitud,
                         Longitude = vehicleReportUseFastTravel.Destination.Longitude,
                         VehicleReportUseId = resultLastCheck.Id

                     };

                     await _unitOfWork.DestinationOfReportUseRepo.Add(destination);
                     await _unitOfWork.SaveChangesAsync();

                    response.success = true;
                    var VehicleReportUseDTO = _mapper.Map<VehicleReportUseDto>(entidad);
                    response.Data = VehicleReportUseDTO;
                    return response;


                }

                else
                {
                    var existeVehicle = await _unitOfWork.VehicleRepo.Get(c => c.Id == vehicleReportUseFastTravel.VehicleId);
                    var resultVehicle = existeVehicle.FirstOrDefault();

                    var entidad = _mapper.Map<VehicleReportUse>(vehicleReportUseFastTravel);
                    entidad.InitialMileage = Convert.ToInt32(resultVehicleMaintenance.CurrentKM);
                    await _unitOfWork.VehicleReportUseRepo.Add(entidad);
                    await _unitOfWork.SaveChangesAsync();

                    var lastReportUse = await _unitOfWork.VehicleReportUseRepo.Get();
                    var resultLastCheck = lastReportUse.OrderByDescending(pr => pr.Id).LastOrDefault();

                    var destination = new DestinationOfReportUse()
                     {
                         Id = 0,
                         DestinationName = vehicleReportUseFastTravel.Destination.DestinationName,
                         Latitud = vehicleReportUseFastTravel.Destination.Latitud,
                         Longitude = vehicleReportUseFastTravel.Destination.Longitude,
                         VehicleReportUseId = resultVehicle.Id

                     };

                     await _unitOfWork.DestinationOfReportUseRepo.Add(destination);
                     await _unitOfWork.SaveChangesAsync();

                    response.success = true;
                    var VehicleReportUseDTO = _mapper.Map<VehicleReportUseDto>(entidad);
                    response.Data = VehicleReportUseDTO;
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


        public async Task<GenericResponse<VehicleReportUseDto>> PutVehicleVerification(int Id, [FromBody] VehicleReportUseVerificationRequest vehicleReportUseVerificationRequest)
        {
            GenericResponse<VehicleReportUseDto> response = new GenericResponse<VehicleReportUseDto>();
            var profile = await _unitOfWork.VehicleReportUseRepo.Get(p => p.Id == Id);
            var result = profile.FirstOrDefault();

            if (result == null)
            {
                response.success = true;
                response.AddError("No existe VehicleReportUse", $"No existe ReportUse con el Id {Id} solicitado ");
                return response;
            }

            var existeAppUser = await _userManager.Users.SingleOrDefaultAsync(c => c.Id == vehicleReportUseVerificationRequest.AppUserId);
            if (existeAppUser == null)
            {
                response.success = false;
                response.AddError("No existe AppUser", $"No existe AppUserId {vehicleReportUseVerificationRequest.AppUserId} para cargar", 1);
                return response;
            }


            result.AppUserId = vehicleReportUseVerificationRequest.AppUserId;
            result.Verification = vehicleReportUseVerificationRequest.Verification;

            var VehicleReportUseDto = _mapper.Map<VehicleReportUseDto>(result);
            await _unitOfWork.VehicleReportUseRepo.Update(result);
            response.success = true;
            response.Data = VehicleReportUseDto;
            return response;


        }

        public async Task<GenericResponse<VehicleReportUseDto>> PutVehicleStatusReport(int Id, int VehicleId, [FromBody] ReportUseTypeRequest reportUseTypeRequest)
        {
            GenericResponse<VehicleReportUseDto> response = new GenericResponse<VehicleReportUseDto>();
            GenericResponse<VehiclesDto> responseVehicle = new GenericResponse<VehiclesDto>();
            var profile = await _unitOfWork.VehicleReportUseRepo.Get(p => p.Id == Id, includeProperties: "Checklist");
            var result = profile.FirstOrDefault();

            if (result == null)
            {
                response.success = true;
                response.AddError("No existe VehicleReportUse", $"No existe ReportUse con el Id {Id} solicitado ");
                return response;
            }

            var existeVehicle = await _unitOfWork.VehicleRepo.Get(c => c.Id == VehicleId);
            var resultVehicle = existeVehicle.FirstOrDefault();

            if (resultVehicle == null)
            {
                response.success = true;
                response.AddError("Se necesita el IdVehicle, no puede ir null el campo", $"No existe IdVehicle con el Id {VehicleId} solicitado ");
                return response;

            }

            if(reportUseTypeRequest.StatusReportUse == Domain.Enums.ReportUseType.enProceso || reportUseTypeRequest.StatusReportUse == Domain.Enums.ReportUseType.ViajeRapido)
            {
                response.success = true;
                response.AddError("No puedes actualizar este campo", $"no puedes actualizar el status en proceso o finalizar, solo se puede finalizar y cancelar");
                return response;
            }

            if (reportUseTypeRequest.StatusReportUse == Domain.Enums.ReportUseType.Finalizado)
            {
                if (reportUseTypeRequest.StatusReportUse == Domain.Enums.ReportUseType.Finalizado)
                {


                    if (reportUseTypeRequest.CurrentFuelLoad == null)
                    {
                        response.success = false;
                        response.AddError("No puede ir Vacio(Null este campo)", $"Insertar Valor para CurrentFueLoad del tipo de reporte viaje rapido solicitado", 1);
                        return response;

                    }

                    var check = await _unitOfWork.VehicleReportUseRepo.Get(p => p.Id == Id);
                    var resultcheck = profile.FirstOrDefault();

                    if (reportUseTypeRequest.Checklist != null)
                    {
                        var entity = _mapper.Map<Checklist>(reportUseTypeRequest.Checklist);
                        entity.VehicleId = VehicleId;
                        await _unitOfWork.ChecklistRepo.Add(entity);
                        await _unitOfWork.SaveChangesAsync();

                        var lastCheck = await _unitOfWork.ChecklistRepo.Get(p => p.VehicleId == VehicleId);
                        var resultLastCheck = lastCheck.OrderByDescending(pr => pr.VehicleId).LastOrDefault();

                        result.ChecklistId = resultLastCheck.Id;
                        await _unitOfWork.VehicleReportUseRepo.Update(result);

                    }
                    else
                    {

                        var lastCheck = await _unitOfWork.ChecklistRepo.Get(p => p.VehicleId == VehicleId);
                        var resultLastCheck = lastCheck.OrderByDescending(pr => pr.VehicleId).LastOrDefault();


                        if (resultLastCheck == null)
                        {
                            var entity = new Checklist()
                            {
                                VehicleId = VehicleId,
                                CirculationCard = false,
                                CarInsurancePolicy = false,
                                HydraulicTires = false,
                                TireRefurmishment = false,
                                JumperCable = false,
                                SecurityDice = false,
                                Extinguisher = false,
                                CarJack = false,
                                CarJackKey = false,
                                ToolBag = false,
                                SafetyTriangle = false

                            };
                            await _unitOfWork.ChecklistRepo.Add(entity);
                            await _unitOfWork.SaveChangesAsync();

                            result.ChecklistId = entity.Id;
                            await _unitOfWork.VehicleReportUseRepo.Update(result);

                        }
                        else
                        {
                            var entity = _mapper.Map<Checklist>(resultLastCheck);
                            entity.Id = 0;
                            await _unitOfWork.ChecklistRepo.Add(entity);
                            await _unitOfWork.SaveChangesAsync();


                            result.ChecklistId = resultLastCheck.Id;
                            await _unitOfWork.VehicleReportUseRepo.Update(result);

                        }

                    }


                    result.FinalMileage = reportUseTypeRequest.FinalMileage;
                    result.CurrentFuelLoad = reportUseTypeRequest.CurrentFuelLoad;
                    await _unitOfWork.VehicleReportUseRepo.Update(result);

                }
                resultVehicle.VehicleStatus = Domain.Enums.VehicleStatus.ACTIVO;
                resultVehicle.CurrentKM = (int)reportUseTypeRequest.FinalMileage;
                await _unitOfWork.VehicleRepo.Update(resultVehicle);
                response.success = true;
            }


            result.StatusReportUse = reportUseTypeRequest.StatusReportUse;
            var VehicleReportUseDto = _mapper.Map<VehicleReportUseDto>(result);
            await _unitOfWork.VehicleReportUseRepo.Update(result);
            response.success = true;
            response.Data = VehicleReportUseDto;
            return response;


        }

        //Delete
        public async Task<GenericResponse<VehicleReportUseDto>> DeleteVehicleReportUse(int Id)
        {
            GenericResponse<VehicleReportUseDto> response = new GenericResponse<VehicleReportUseDto>();
            var entidad = await _unitOfWork.VehicleReportUseRepo.Get(filter: p => p.Id == Id);
            var result = entidad.FirstOrDefault();
            if (result == null)
            {
                return null;
            }

            var existe = await _unitOfWork.VehicleReportUseRepo.Delete(Id);
            await _unitOfWork.SaveChangesAsync();

            var VehicleReportUseDTO = _mapper.Map<VehicleReportUseDto>(result);
            response.success = true;
            response.Data = VehicleReportUseDTO;

            return response;
        }
    }
}
