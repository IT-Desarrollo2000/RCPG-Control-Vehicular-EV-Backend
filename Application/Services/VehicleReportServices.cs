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

namespace Application.Services
{
    public class VehicleReportServices : IVehicleReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PaginationOptions _paginationOptions;
        private readonly UserManager<AppUser> _userManager;

        public VehicleReportServices( IUnitOfWork unitOfWork, IMapper mapper, IOptions<PaginationOptions> options, UserManager<AppUser> userManager) 
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            _paginationOptions = options.Value;
            _userManager = userManager;
        }

        //GetALL
        public async Task<PagedList<VehicleReport>> GetVehicleReportAll(VehicleReportFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            string properties = "";
            IEnumerable<VehicleReport> userApprovals = null;
            Expression<Func<VehicleReport, bool>> Query = null;

            if(filter.ReportType.HasValue)
            {
                if(Query != null )
                {
                    Query = Query.And(p => p.ReportType >= filter.ReportType.Value);
                }
                else { Query = p => p.ReportType >= filter.ReportType.Value;}
            }

            if (filter.VehicleId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.VehicleId >= filter.VehicleId.Value);
                }
                else { Query = p => p.VehicleId >= filter.VehicleId.Value; }

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
                else { Query = p => p.AppUserId == filter.AppUserId.Value; }
            }

            if (filter.GasolineLoad.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.GasolineLoad >= filter.GasolineLoad.Value);
                }
                else { Query = p => p.GasolineLoad >= filter.GasolineLoad.Value; }
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
                if(Query != null)
                {
                    Query = Query.And(p => p.VehicleReportUseId >= filter.VehicleReportUseId.Value);
                }
                else { Query = p => p.VehicleReportUseId >= filter.VehicleReportUseId.Value; }
            }


            if (Query != null)
            {
                userApprovals = await _unitOfWork.VehicleReportRepo.Get(filter: Query, includeProperties: "Vehicle,UserProfile,AppUser,Expenses,VehicleReportImages");
            }
            else
            {
                userApprovals = await _unitOfWork.VehicleReportRepo.Get(includeProperties: "Vehicle,UserProfile,AppUser,Expenses,VehicleReportImages");
            }

            var pagedApprovals = PagedList<VehicleReport>.Create(userApprovals, filter.PageNumber, filter.PageSize);

            return pagedApprovals;
        }

        //GETBYID
        public async Task<GenericResponse<VehicleReportDto>> GetVehicleReportById(int Id)
        {
            GenericResponse<VehicleReportDto> response = new GenericResponse<VehicleReportDto>();
            var profile = await _unitOfWork.VehicleReportRepo.Get(filter: p => p.Id == Id, includeProperties: "Vehicle,UserProfile,AppUser,Expenses,VehicleReportImages");
            var result = profile.FirstOrDefault();
            var VehicleReportDto = _mapper.Map<VehicleReportDto>(result);
            response.success = true;
            response.Data = VehicleReportDto;
            return response;
        }

        //Post 
        public async Task<GenericResponse<VehicleReportDto>> PostVehicleReport([FromBody] VehicleReportRequest vehicleReportRequest)
        {
            GenericResponse<VehicleReportDto> response = new GenericResponse<VehicleReportDto>();
            if (vehicleReportRequest.ReportType == Domain.Enums.ReportType.Carga_Gasolina)
            {
                if (vehicleReportRequest.GasolineLoad == null)
                {
                    response.success = false;
                    response.AddError("Es necesario un valor existente, Kilometraje o Litros", $"Para el tipo de Carga de Gasolina, es necesario un valor existe de la carga de gasolina {vehicleReportRequest.GasolineLoad} solicitado", 1);
                    return response;
                }

                else if (vehicleReportRequest.GasolineLoad == Domain.Enums.GasolineLoadType.Kilometraje || vehicleReportRequest.GasolineLoad == Domain.Enums.GasolineLoadType.Litros)
                {
                    var entidad = _mapper.Map<VehicleReport>(vehicleReportRequest);
                    await _unitOfWork.VehicleReportRepo.Add(entidad);
                    await _unitOfWork.SaveChangesAsync();
                    response.success = true;
                    var VehicleReportDTO = _mapper.Map<VehicleReportDto>(entidad);
                    response.Data = VehicleReportDTO;
                    return response;

                }
                else
                {
                    response.success = false;
                    response.AddError("No existe GasoLineLoad", $"No existe Gasolinead con el GasoLinead {vehicleReportRequest.GasolineLoad} solicitado", 1);
                    return response;
                }

            }
            else { vehicleReportRequest.GasolineLoad = null; }


            var existeVehicleMaintenance = await _unitOfWork.VehicleRepo.Get(c => c.Id == vehicleReportRequest.VehicleId);
            var resultVehicleMaintenance = existeVehicleMaintenance.FirstOrDefault();
            if (resultVehicleMaintenance == null)
            {
                response.success = false;
                response.AddError("No existe Vehicle", $"No existe Vehiculo con el VehicleId {vehicleReportRequest.VehicleId} solicitado", 1);
                return response;

            }

            if (vehicleReportRequest.UserProfileId.HasValue)
            {
                var existeUserProfile = await _unitOfWork.UserProfileRepo.Get(c => c.Id == vehicleReportRequest.UserProfileId.Value);
                var resultUserProfile = existeUserProfile.FirstOrDefault();

                if (resultUserProfile == null)
                {
                    response.success = false;
                    response.AddError("No existe UserProfile", $"No existe UserProfileId {vehicleReportRequest.UserProfileId} para cargar", 1);
                    return response;
                }

            } 
            if (vehicleReportRequest.AppUserId.HasValue)
            {
                var existeAppUser = await _userManager.Users.SingleOrDefaultAsync(c => c.Id == vehicleReportRequest.AppUserId.Value);
                if(existeAppUser == null)
                {
                    response.success = false;
                    response.AddError("No existe AppUser", $"No existe AppUserId {vehicleReportRequest.AppUserId} para cargar", 1);
                    return response;
                }
            
            }

           if ( vehicleReportRequest.VehicleReportUseId.HasValue)
            {
                var profileD = await _unitOfWork.VehicleReportUseRepo.Get(p => p.Id == vehicleReportRequest.VehicleReportUseId);
                var resultD = profileD.FirstOrDefault();

                if (resultD == null)
                {
                    response.success = true;
                    response.AddError("No existe VehicleReportUse", $"No existe ReportUse con el Id {vehicleReportRequest.VehicleReportUseId} solicitado ");
                    return response;
                }


                var entidad = _mapper.Map<VehicleReport>(vehicleReportRequest);
                await _unitOfWork.VehicleReportRepo.Add(entidad);
                await _unitOfWork.SaveChangesAsync();
                response.success = true;
                var VehicleReportDTO = _mapper.Map<VehicleReportDto>(entidad);
                response.Data = VehicleReportDTO;
                return response;

            }
            else
            {
                var entidad = _mapper.Map<VehicleReport>(vehicleReportRequest);
                await _unitOfWork.VehicleReportRepo.Add(entidad);
                await _unitOfWork.SaveChangesAsync();
                response.success = true;
                var VehicleReportDTO = _mapper.Map<VehicleReportDto>(entidad);
                response.Data = VehicleReportDTO;
                return response;
            }

        }

        //Put
        public async Task<GenericResponse<VehicleReportDto>> PutVehicleReport(int Id, [FromBody] VehicleReportRequest vehicleReportRequest)
        {
            GenericResponse<VehicleReportDto> response = new GenericResponse<VehicleReportDto>();
            var profile = await _unitOfWork.VehicleReportRepo.Get(p => p.Id == Id);
            var result = profile.FirstOrDefault();

            if (result == null)
            {
                response.success = true;
                response.AddError("No existe VehicleReport",$"No existe Report con el Id { Id } solicitado ");
                return response;
            }

            if (vehicleReportRequest.ReportType == Domain.Enums.ReportType.Carga_Gasolina)
            {
                if (vehicleReportRequest.GasolineLoad == null)
                {
                    response.success = false;
                    response.AddError("Es necesario un valor existente, Kilometraje o Litros", $"Para el tipo de Carga de Gasolina, es necesario un valor existe de la carga de gasolina {vehicleReportRequest.GasolineLoad} solicitado", 1);
                    return response;
                }

                else if (vehicleReportRequest.GasolineLoad == Domain.Enums.GasolineLoadType.Kilometraje || vehicleReportRequest.GasolineLoad == Domain.Enums.GasolineLoadType.Litros)
                {
                    result.ReportType = vehicleReportRequest.ReportType;
                    result.VehicleId = vehicleReportRequest.VehicleId;
                    result.Commentary = vehicleReportRequest.Commentary;
                    result.UserProfileId = vehicleReportRequest.UserProfileId;
                    result.AppUserId = vehicleReportRequest.AppUserId;
                    result.ReportDate = vehicleReportRequest.ReportDate;
                    result.IsResolved = vehicleReportRequest.IsResolved;
                    result.GasolineLoad = vehicleReportRequest.GasolineLoad;
                    result.ReportSolutionComment = vehicleReportRequest.ReportSolutionComment;
                    result.ReportStatus = vehicleReportRequest.ReportStatus;
                    result.VehicleReportUseId = vehicleReportRequest.VehicleReportUseId;

                    await _unitOfWork.VehicleReportRepo.Update(result);
                    await _unitOfWork.SaveChangesAsync();

                    var VehicleReportDto = _mapper.Map<VehicleReportDto>(result);
                    response.success = true;
                    response.Data = VehicleReportDto;

                    return response;


                }
                else
                {
                    response.success = false;
                    response.AddError("No existe GasoLineLoad", $"No existe Gasolinead con el GasoLinead {vehicleReportRequest.GasolineLoad} solicitado", 1);
                    return response;
                }

            }
            else { vehicleReportRequest.GasolineLoad = null; }

            var existeVehicleMaintenance = await _unitOfWork.VehicleRepo.Get(c => c.Id == vehicleReportRequest.VehicleId);
            var resultVehicleMaintenance = existeVehicleMaintenance.FirstOrDefault();
            if (resultVehicleMaintenance == null)
            {
                response.success = false;
                response.AddError("No existe Vehicle", $"No existe Vehiculo con el VehicleId {vehicleReportRequest.VehicleId} solicitado", 1);
                return response;

            }

            if (vehicleReportRequest.UserProfileId.HasValue)
            {
                var existeUserProfile = await _unitOfWork.UserProfileRepo.Get(c => c.Id == vehicleReportRequest.UserProfileId.Value);
                var resultUserProfile = existeUserProfile.FirstOrDefault();

                if (resultUserProfile == null)
                {
                    response.success = false;
                    response.AddError("No existe UserProfile", $"No existe UserProfileId {vehicleReportRequest.UserProfileId} para cargar", 1);
                    return response;
                }

            }
            if (vehicleReportRequest.AppUserId.HasValue)
            {
                var existeAppUser = await _userManager.Users.SingleOrDefaultAsync(c => c.Id == vehicleReportRequest.AppUserId.Value);
                if (existeAppUser == null)
                {
                    response.success = false;
                    response.AddError("No existe AppUser", $"No existe AppUserId {vehicleReportRequest.UserProfileId} para cargar", 1);
                    return response;
                }

            }
            if (vehicleReportRequest.VehicleReportUseId.HasValue)
            {
                var profileD = await _unitOfWork.VehicleReportUseRepo.Get(p => p.Id == vehicleReportRequest.VehicleReportUseId);
                var resultD = profileD.FirstOrDefault();

                if (resultD == null)
                {
                    response.success = true;
                    response.AddError("No existe VehicleReportUse", $"No existe ReportUse con el Id {vehicleReportRequest.VehicleReportUseId} solicitado ");
                    return response;
                }


                result.ReportType = vehicleReportRequest.ReportType;
                result.VehicleId = vehicleReportRequest.VehicleId;
                result.Commentary = vehicleReportRequest.Commentary;
                result.UserProfileId = vehicleReportRequest.UserProfileId;
                result.AppUserId = vehicleReportRequest.AppUserId;
                result.ReportDate = vehicleReportRequest.ReportDate;
                result.IsResolved = vehicleReportRequest.IsResolved;
                result.GasolineLoad = vehicleReportRequest.GasolineLoad;
                result.ReportSolutionComment = vehicleReportRequest.ReportSolutionComment;
                result.ReportStatus = vehicleReportRequest.ReportStatus;
                result.VehicleReportUseId = vehicleReportRequest.VehicleReportUseId;

                await _unitOfWork.VehicleReportRepo.Update(result);
                await _unitOfWork.SaveChangesAsync();

                var VehicleReportDto = _mapper.Map<VehicleReportDto>(result);
                response.success = true;
                response.Data = VehicleReportDto;

                return response;

            }
            else
            {
                result.ReportType = vehicleReportRequest.ReportType;
                result.VehicleId = vehicleReportRequest.VehicleId;
                result.Commentary = vehicleReportRequest.Commentary;
                result.UserProfileId = vehicleReportRequest.UserProfileId;
                result.AppUserId = vehicleReportRequest.AppUserId;
                result.ReportDate = vehicleReportRequest.ReportDate;
                result.IsResolved = vehicleReportRequest.IsResolved;
                result.GasolineLoad = vehicleReportRequest.GasolineLoad;
                result.ReportSolutionComment = vehicleReportRequest.ReportSolutionComment;
                result.ReportStatus = vehicleReportRequest.ReportStatus;
                result.VehicleReportUseId = vehicleReportRequest.VehicleReportUseId;

                await _unitOfWork.VehicleReportRepo.Update(result);
                await _unitOfWork.SaveChangesAsync();

                var VehicleReportDto = _mapper.Map<VehicleReportDto>(result);
                response.success = true;
                response.Data = VehicleReportDto;

                return response;
            }

        }

        //Delete
        public async Task<GenericResponse<VehicleReportDto>> DeleteVehicleReport(int Id)
        {
            GenericResponse<VehicleReportDto> response = new GenericResponse<VehicleReportDto>();
            var entidad = await _unitOfWork.VehicleReportRepo.Get(filter: p => p.Id == Id);
            var result = entidad.FirstOrDefault();
            if (result == null)
            {
                return null;
            }

            var existe = await _unitOfWork.VehicleReportRepo.Delete(Id);
            await _unitOfWork.SaveChangesAsync();

            var VehicleReportDTO = _mapper.Map<VehicleReportDto>(result);
            response.success = true;
            response.Data = VehicleReportDTO;

            return response;
        }

    }
}
