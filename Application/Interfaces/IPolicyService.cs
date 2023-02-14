using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPolicyService
    {
        Task<GenericResponse<PolicyDto>> DeletePolicy(int Id);
        Task<GenericResponse<List<PolicyDto>>> GetPolicyAll();
        Task<GenericResponse<PolicyDto>> GetPolicyById(int Id);
        Task<GenericResponse<PolicyDto>> PostPolicy([FromBody] PolicyRequest policyRequest);
        Task<GenericResponse<PolicyDto>> PutPolicy(int Id, [FromBody] PolicyRequest policyRequest);
    }
}
