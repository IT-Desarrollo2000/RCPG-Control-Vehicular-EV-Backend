﻿using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;

namespace Application.Interfaces
{
    public interface IChecklistServices
    {
        Task<GenericResponse<Checklist>> DeleteChecklists(int id);
        Task<PagedList<Checklist>> GetChecklist(ChecklistFilter filter);
        Task<GenericResponse<ChecklistDto>> GetChecklistById(int id);
        Task<GenericResponse<ChecklistDto>> PostChecklist(int vehicleId, CreationChecklistDto creationChecklistDto);
        Task<GenericResponse<ChecklistDto>> PutChecklists(ChecklistUpdateDto creationChecklistDto, int id);
    }
}
