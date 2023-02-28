using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Application.Interfaces
{
    public interface IPolicyService
    {
        Task<GenericResponse<PolicyDto>> DeletePolicy(int Id);
        Task<GenericResponse<List<PolicyDto>>> GetPolicyAll();
        Task<GenericResponse<PolicyDto>> GetPolicyById(int Id);
        Task<GenericResponse<PolicyDto>> PostPolicy([FromBody] PolicyRequest policyRequest);
        Task<GenericResponse<PolicyDto>> PutPolicy(PolicyUpdateRequest request);
    }
}
