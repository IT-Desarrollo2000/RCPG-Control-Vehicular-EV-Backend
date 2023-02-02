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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class VehicleReportUseServices : IVehicleReportUseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PaginationOptions _paginationOptions;
        private readonly UserManager<AppUser> _userManager;

        public VehicleReportUseServices( IUnitOfWork unitOfWork, IMapper mapper, IOptions<PaginationOptions> options, UserManager<AppUser> userManager) 
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            _paginationOptions = options.Value;
            _userManager = userManager;
        }

        //GetAll
        public async Task<PagedList<VehicleReportUse>> GetVehicleReportUseAll(VehicleReportUseFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            string properties = "";
            IEnumerable<VehicleReportUse> userApprovals = null;
            Expression<Func<VehicleReportUse, bool>> Query = null;

            if(filter.VehicleId.HasValue)
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

            if(filter.StatusReportUse.HasValue)
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


            if (Query != null)
            {
                userApprovals = await _unitOfWork.VehicleReportUseRepo.Get(filter: Query, includeProperties: "Vehicle,Checklist,VehicleReport,UserProfile,AppUser,Destinations");
            }
            else
            {
                userApprovals = await _unitOfWork.VehicleReportUseRepo.Get(includeProperties: "Checklist,VehicleReport,UserProfile,AppUser,Destinations");
            }

            var pagedApprovals = PagedList<VehicleReportUse>.Create(userApprovals, filter.PageNumber, filter.PageSize);

            return pagedApprovals;

        }

        //GETBYID
        public async Task<GenericResponse<VehicleReportUseDto>> GetVehicleReporUseById(int Id)
        {
            GenericResponse<VehicleReportUseDto> response = new GenericResponse<VehicleReportUseDto>();
            var profile = await _unitOfWork.VehicleReportUseRepo.Get(filter: p => p.Id == Id, includeProperties:"Checklist,VehicleReport,UserProfile,AppUser,Destinations");
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

        //Post 
        public async Task<GenericResponse<VehicleReportUseDto>> PostVehicleReporUse([FromBody] VehicleReportUseRequest vehicleReportUseRequest)
        {
            GenericResponse<VehicleReportUseDto> response = new GenericResponse<VehicleReportUseDto>();
        

            if(vehicleReportUseRequest.StatusReportUse == Domain.Enums.ReportUseType.ViajeRapido)
            {
                if(vehicleReportUseRequest.VehicleId == null)
                {
                    response.success = false;
                    response.AddError("No puede ir vacio el campo IdVehiculo", "Se solicita el campo IdVehiculo", 1);
                    return response;
                }

                if(vehicleReportUseRequest.UseDate == null)
                {
                    response.success = false;
                    response.AddError("No puede ir vacio el campo UseDate", "Se solicita el campo UseDate", 1);
                    return response;
                }
                if(vehicleReportUseRequest.FinalMileage == null)
                {
                    response.success = false;
                    response.AddError("No puede ir vacio el campo FinalMileage", "Se solicita el campo Mileage", 1);
                    return response;
                }

                var entidad = _mapper.Map<VehicleReportUse>(vehicleReportUseRequest);
                await _unitOfWork.VehicleReportUseRepo.Add(entidad);
                await _unitOfWork.SaveChangesAsync();
                response.success = true;
                var VehicleReportUseDTO = _mapper.Map<VehicleReportUseDto>(entidad);
                response.Data = VehicleReportUseDTO;
                return response;

            }

            var existeVehicleMaintenance = await _unitOfWork.VehicleRepo.Get(c => c.Id == vehicleReportUseRequest.VehicleId);
            var resultVehicleMaintenance = existeVehicleMaintenance.FirstOrDefault();
            if (resultVehicleMaintenance == null)
            {
                response.success = false;
                response.AddError("No existe Vehicle", $"No existe Vehiculo con el VehicleId {vehicleReportUseRequest.VehicleId} solicitado", 1);
                return response;

            }

            if (vehicleReportUseRequest.ChecklistId.HasValue)
            {
                var existeCheckList = await _unitOfWork.ChecklistRepo.Get(c => c.Id == vehicleReportUseRequest.ChecklistId.Value);
                var resultCheckList = existeCheckList.FirstOrDefault();

                if(resultCheckList == null)
                {
                    response.success = false;
                    response.AddError("No existe CheckList", $"No existe CheckListId {vehicleReportUseRequest.ChecklistId} para cargar", 1);
                    return response;
                }

                var entidad = _mapper.Map<VehicleReportUse>(vehicleReportUseRequest);
                await _unitOfWork.VehicleReportUseRepo.Add(entidad);
                await _unitOfWork.SaveChangesAsync();
                response.success = true;
                var VehicleReportUseDTO = _mapper.Map<VehicleReportUseDto>(entidad);
                response.Data = VehicleReportUseDTO;
                return response;
            }
            else if (vehicleReportUseRequest.UserProfileId.HasValue)
            {
                var existeUserProfile = await _unitOfWork.UserProfileRepo.Get(c => c.Id == vehicleReportUseRequest.UserProfileId.Value);
                var resultUserProfile = existeUserProfile.FirstOrDefault();

                if (resultUserProfile == null)
                {
                    response.success = false;
                    response.AddError("No existe UserProfile", $"No existe UserProfileId {vehicleReportUseRequest.ChecklistId} para cargar", 1);
                    return response;
                }

                var entidad = _mapper.Map<VehicleReportUse>(vehicleReportUseRequest);
                await _unitOfWork.VehicleReportUseRepo.Add(entidad);
                await _unitOfWork.SaveChangesAsync();
                response.success = true;
                var VehicleReportUseDTO = _mapper.Map<VehicleReportUseDto>(entidad);
                response.Data = VehicleReportUseDTO;
                return response;
            }
            else if (vehicleReportUseRequest.AppUserId.HasValue)
            {
                var existeAppUser = await _userManager.Users.SingleOrDefaultAsync(c => c.Id == vehicleReportUseRequest.AppUserId.Value);
                if (existeAppUser == null)
                {
                    response.success = false;
                    response.AddError("No existe AppUser", $"No existe AppUserId {vehicleReportUseRequest.UserProfileId} para cargar", 1);
                    return response;
                }

                var entidad = _mapper.Map<VehicleReportUse>(vehicleReportUseRequest);
                await _unitOfWork.VehicleReportUseRepo.Add(entidad);
                await _unitOfWork.SaveChangesAsync();
                response.success = true;
                var VehicleReportUseDTO = _mapper.Map<VehicleReportUseDto>(entidad);
                response.Data = VehicleReportUseDTO;
                return response;

            }
            else
            {
                var entidad = _mapper.Map<VehicleReportUse>(vehicleReportUseRequest);
                await _unitOfWork.VehicleReportUseRepo.Add(entidad);
                await _unitOfWork.SaveChangesAsync();
                response.success = true;
                var VehicleReportUseDTO = _mapper.Map<VehicleReportUseDto>(entidad);
                response.Data = VehicleReportUseDTO;
                return response;
            }
        }

        //Put
        public async Task<GenericResponse<VehicleReportUseDto>> PutVehicleReportUse (int Id, [FromBody] VehicleReportUseRequest vehicleReportUseRequest)
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

            var existeVehicleMaintenance = await _unitOfWork.VehicleRepo.Get(c => c.Id == vehicleReportUseRequest.VehicleId);
            var resultVehicleMaintenance = existeVehicleMaintenance.FirstOrDefault();
            if (resultVehicleMaintenance == null)
            {
                response.success = false;
                response.AddError("No existe Vehicle", $"No existe Vehiculo con el VehicleId {vehicleReportUseRequest.VehicleId} solicitado", 1);
                return response;

            }

            if (vehicleReportUseRequest.ChecklistId.HasValue)
            {
                var existeCheckList = await _unitOfWork.ChecklistRepo.Get(c => c.Id == vehicleReportUseRequest.ChecklistId.Value);
                var resultCheckList = existeCheckList.FirstOrDefault();

                if (resultCheckList == null)
                {
                    response.success = false;
                    response.AddError("No existe CheckList", $"No existe CheckListId {vehicleReportUseRequest.ChecklistId} para cargar", 1);
                    return response;
                }

                result.VehicleId = vehicleReportUseRequest.VehicleId;
                result.FinalMileage = vehicleReportUseRequest.FinalMileage;
                result.StatusReportUse = vehicleReportUseRequest.StatusReportUse;
                result.Observations = vehicleReportUseRequest.Observations;
                result.ChecklistId = vehicleReportUseRequest.ChecklistId;
                result.UseDate = vehicleReportUseRequest.UseDate;
                result.UserProfileId = vehicleReportUseRequest.UserProfileId;
                result.AppUserId = vehicleReportUseRequest.AppUserId;

                var VehicleReportUseDto = _mapper.Map<VehicleReportUseDto>(result);
                await _unitOfWork.VehicleReportUseRepo.Update(result);
                response.success = true;
                response.Data = VehicleReportUseDto;
                return response;

            }
            else if (vehicleReportUseRequest.UserProfileId.HasValue)
            {
                var existeUserProfile = await _unitOfWork.UserProfileRepo.Get(c => c.Id == vehicleReportUseRequest.UserProfileId.Value);
                var resultUserProfile = existeUserProfile.FirstOrDefault();

                if (resultUserProfile == null)
                {
                    response.success = false;
                    response.AddError("No existe UserProfile", $"No existe UserProfileId {vehicleReportUseRequest.ChecklistId} para cargar", 1);
                    return response;
                }

                result.VehicleId = vehicleReportUseRequest.VehicleId;
                result.FinalMileage = vehicleReportUseRequest.FinalMileage;
                result.StatusReportUse = vehicleReportUseRequest.StatusReportUse;
                result.Observations = vehicleReportUseRequest.Observations;
                result.ChecklistId = vehicleReportUseRequest.ChecklistId;
                result.UseDate = vehicleReportUseRequest.UseDate;
                result.UserProfileId = vehicleReportUseRequest.UserProfileId;
                result.AppUserId = vehicleReportUseRequest.AppUserId;

                var VehicleReportUseDto = _mapper.Map<VehicleReportUseDto>(result);
                await _unitOfWork.VehicleReportUseRepo.Update(result);
                response.success = true;
                response.Data = VehicleReportUseDto;
                return response;
            }
            else if (vehicleReportUseRequest.AppUserId.HasValue)
            {
                var existeAppUser = await _userManager.Users.SingleOrDefaultAsync(c => c.Id == vehicleReportUseRequest.AppUserId.Value);
                if (existeAppUser == null)
                {
                    response.success = false;
                    response.AddError("No existe AppUser", $"No existe AppUserId {vehicleReportUseRequest.UserProfileId} para cargar", 1);
                    return response;
                }

                result.VehicleId = vehicleReportUseRequest.VehicleId;
                result.FinalMileage = vehicleReportUseRequest.FinalMileage;
                result.StatusReportUse = vehicleReportUseRequest.StatusReportUse;
                result.Observations = vehicleReportUseRequest.Observations;
                result.ChecklistId = vehicleReportUseRequest.ChecklistId;
                result.UseDate = vehicleReportUseRequest.UseDate;
                result.UserProfileId = vehicleReportUseRequest.UserProfileId;
                result.AppUserId = vehicleReportUseRequest.AppUserId;

                var VehicleReportUseDto = _mapper.Map<VehicleReportUseDto>(result);
                await _unitOfWork.VehicleReportUseRepo.Update(result);
                response.success = true;
                response.Data = VehicleReportUseDto;
                return response;

            }
            else
            {
                result.VehicleId = vehicleReportUseRequest.VehicleId;
                result.FinalMileage = vehicleReportUseRequest.FinalMileage;
                result.StatusReportUse = vehicleReportUseRequest.StatusReportUse;
                result.Observations = vehicleReportUseRequest.Observations;
                result.ChecklistId = vehicleReportUseRequest.ChecklistId;
                result.UseDate = vehicleReportUseRequest.UseDate;
                result.UserProfileId = vehicleReportUseRequest.UserProfileId;
                result.AppUserId = vehicleReportUseRequest.AppUserId;

                var VehicleReportUseDto = _mapper.Map<VehicleReportUseDto>(result);
                await _unitOfWork.VehicleReportUseRepo.Update(result);
                response.success = true;
                response.Data = VehicleReportUseDto;
                return response;
            }

        }

        //Delete
        public async Task<GenericResponse<VehicleReportUseDto>> DeleteVehicleReportUse (int Id)
        {
            GenericResponse<VehicleReportUseDto> response = new GenericResponse<VehicleReportUseDto>();
            var entidad = await _unitOfWork.VehicleReportUseRepo.Get(filter: p => p.Id == Id);
            var result = entidad.FirstOrDefault();
            if(result == null)
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
