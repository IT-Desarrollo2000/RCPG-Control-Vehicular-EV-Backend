using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Propietary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPropietaryServices
    {
        Task<GenericResponse<Propietary>> DeletePropietary(int id);
        Task<PagedList<PropietaryDto>> GetPropietaries(PropietaryFilter filter);
        Task<GenericResponse<PropietaryDto>> GetPropietaryById(int id);
        Task<GenericResponse<PropietaryDto>> PostPropietary(PropietaryRequest propietaryRequest);
        Task<GenericResponse<PropietaryDto>> PutPropietary(PropietaryUpdateDto propietaryUpdateDto, int id);
    }
}
