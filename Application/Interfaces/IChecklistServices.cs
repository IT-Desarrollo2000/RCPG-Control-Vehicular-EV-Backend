using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IChecklistServices
    {
        Task<GenericResponse<Checklist>> DeleteChecklists(int id);
        Task<PagedList<Checklist>> GetChecklist(ChecklistFilter filter);
        Task<GenericResponse<ChecklistDto>> GetChecklistById(int id);
        Task<GenericResponse<ChecklistDto>> PostChecklist(int vehicleId, CreationChecklistDto creationChecklistDto);
        Task<GenericResponse<Checklist>> PutChecklists(CreationChecklistDto creationChecklistDto, int id);
    }
}
