using Domain.CustomEntities;
using Domain.DTOs.Filters;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAdditionalInformationService
    {
        Task<GenericResponse<AdditionalInformation>> DeleteAdditionalInformation(int id);
        Task<PagedList<AdditionalInformation>> GetAdditionalInformation(AdditionalInformationFilter filter);
        Task<GenericResponse<AdditionalInformationDto>> GetAdditionalInformationById(int id);
        Task<GenericResponse<AdditionalInformationDto>> PostAdditionalInformation(AdditionalInformationRequest additionalInformationRequest);
        Task<GenericResponse<AdditionalInformationDto>> PutAditionalInformation(AdditionalInformationUpdateDto additionalInformationUpdateDto, int id);
    }
}
