using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
using Microsoft.AspNetCore.Mvc;

namespace Application.Interfaces
{
    public interface IPolicyService
    {
        Task<GenericResponse<List<PhotosOfPolicy>>> AddPolicyImage(PolicyImagesRequest policyImagesRequest, int policyId);
        Task<GenericResponse<PolicyDto>> DeletePolicy(int Id);
        Task<GenericResponse<bool>> DeletePolicyImages(int PolicyId);
        Task<GenericResponse<List<PolicyDto>>> GetPolicyAll();
        Task<GenericResponse<PolicyDto>> GetPolicyById(int Id);
        Task<GenericResponse<PolicyDto>> PostPolicy(PolicyRequest policyRequest);
        Task<GenericResponse<PolicyDto>> PutPolicy(PolicyUpdateRequest request);
    }
}
