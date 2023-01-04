﻿using Application.Extensions;
using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
using Domain.Entities.User_Approvals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ChecklistServices : IChecklistServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PaginationOptions _paginationOptions;

        public ChecklistServices(IUnitOfWork unitOfWork, IMapper mapper, IOptions<PaginationOptions> options)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._paginationOptions = options.Value;
        }


        public async Task<GenericResponse<ChecklistDto>> PostChecklist(int vehicleId, CreationChecklistDto creationChecklistDto)
        {
            GenericResponse<ChecklistDto> response = new GenericResponse<ChecklistDto>();
            var entity = _mapper.Map<Checklist>(creationChecklistDto);
            //entity.Vehicle = _unitOfWork.VehicleRepo.Get(VehicleDB => VehicleDB.Id == vehicleId);
            entity.VehicleId = vehicleId;
            
            await _unitOfWork.ChecklistRepo.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            response.success = true;
            var ChecklisDto = _mapper.Map<ChecklistDto>(entity);
            response.Data = ChecklisDto;
            return response;
        }

        public async Task<PagedList<Checklist>> GetChecklist(ChecklistFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            string properties = "";
            IEnumerable<Checklist> checklists = null;
            Expression<Func<Checklist, bool>> Query = null;

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

            if(filter.VehicleId.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.VehicleId <= filter.VehicleId.Value);
                }
                else { Query = p => p.VehicleId <= filter.VehicleId.Value; }
            }

            if (filter.CirculationCard.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.CirculationCard == filter.CirculationCard.Value);
                }
                else { Query = p => p.CirculationCard == filter.CirculationCard.Value; }
            }

            if (filter.CarInsurancePolicy.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.CarInsurancePolicy == filter.CarInsurancePolicy.Value);
                }
                else { Query = p => p.CarInsurancePolicy == filter.CarInsurancePolicy.Value; }
            }

            if (filter.HydraulicTires.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.HydraulicTires== filter.HydraulicTires.Value);
                }
                else { Query = p => p.HydraulicTires == filter.HydraulicTires.Value; }
            }

            if (filter.TireRefurmishment.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.TireRefurmishment == filter.TireRefurmishment.Value);
                }
                else { Query = p => p.TireRefurmishment == filter.TireRefurmishment.Value; }
            }

            if (filter.JumperCable.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.JumperCable == filter.JumperCable.Value);
                }
                else { Query = p => p.JumperCable == filter.JumperCable.Value; }
            }

            if (filter.SecurityDice.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.SecurityDice == filter.SecurityDice.Value);
                }
                else { Query = p => p.SecurityDice == filter.SecurityDice.Value; }
            }

            if (filter.Extinguisher.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.Extinguisher == filter.Extinguisher.Value);
                }
                else { Query = p => p.Extinguisher == filter.Extinguisher.Value; }
            }

            if (filter.CarJack.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.CarJack == filter.CarJack.Value);
                }
                else { Query = p => p.CarJack == filter.CarJack.Value; }
            }

            if (filter.CarJackKey.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.CarJackKey == filter.CarJackKey.Value);
                }
                else { Query = p => p.CarJackKey == filter.CarJackKey.Value; }
            }

            if (filter.ToolBag.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.ToolBag == filter.ToolBag.Value);
                }
                else { Query = p => p.ToolBag == filter.ToolBag.Value; }
            }

            if (filter.SafetyTriangle.HasValue)
            {
                if (Query != null)
                {
                    Query = Query.And(p => p.SafetyTriangle== filter.SafetyTriangle.Value);
                }
                else { Query = p => p.SafetyTriangle == filter.SafetyTriangle.Value; }
            }

            if (Query != null)
            {
                checklists = await _unitOfWork.ChecklistRepo.Get(filter: Query, includeProperties: properties);
            }
            else
            {
                checklists = await _unitOfWork.ChecklistRepo.Get(includeProperties: properties);
            }

            var pagedChecklist = PagedList<Checklist>.Create(checklists, filter.PageNumber, filter.PageSize);

            return pagedChecklist;
        }

        public async Task<GenericResponse<ChecklistDto>> GetChecklistById(int id)
        {
            GenericResponse<ChecklistDto> response = new GenericResponse<ChecklistDto>();
            var entity = await _unitOfWork.ChecklistRepo.Get(filter: a => a.Id == id);
            var check = entity.FirstOrDefault();
            var map = _mapper.Map<ChecklistDto>(check);
            response.success = true;
            response.Data = map;
            return response;
        }

        public async Task<GenericResponse<Checklist>> PutChecklists(CreationChecklistDto creationChecklistDto, int id)
        {

            GenericResponse<Checklist> response = new GenericResponse<Checklist>();
            var result = await _unitOfWork.ChecklistRepo.Get(r => r.Id == id);
            var check = result.FirstOrDefault();
            if (check == null) return null;

            check.CirculationCard = creationChecklistDto.CirculationCard;
            check.CarInsurancePolicy = creationChecklistDto.CarInsurancePolicy;
            check.HydraulicTires = creationChecklistDto.HydraulicTires;
            check.TireRefurmishment = creationChecklistDto.TireRefurmishment;
            check.JumperCable = creationChecklistDto.JumperCable;
            check.SecurityDice = creationChecklistDto.SecurityDice;
            check.Extinguisher = creationChecklistDto.Extinguisher;
            check.CarJack = creationChecklistDto.CarJack;
            check.CarJackKey = creationChecklistDto.CarJackKey;
            check.ToolBag = creationChecklistDto.ToolBag;
            check.SafetyTriangle = creationChecklistDto.SafetyTriangle;


            await _unitOfWork.ChecklistRepo.Update(check);
            await _unitOfWork.SaveChangesAsync();
            response.success = true;
            response.Data = check;
            return response;

        }
        public async Task<GenericResponse<Checklist>> DeleteChecklists(int id)
        {
            GenericResponse<Checklist> response = new GenericResponse<Checklist>();
            var check = await _unitOfWork.ChecklistRepo.GetById(id);
            if (check == null) return null;
            var exists = await _unitOfWork.ChecklistRepo.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            var checkdto = _mapper.Map<Checklist>(check);
            response.success = true;
            response.Data = checkdto;
            return response;
        }

    }
}