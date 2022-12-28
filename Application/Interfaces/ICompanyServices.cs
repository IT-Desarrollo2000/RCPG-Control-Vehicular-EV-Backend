using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Application.Interfaces
{
    public interface ICompanyServices
    {
        Task<GenericResponse<CompanyDto>> DeleteCompany(int id);
        Task<GenericResponse<List<CompanyDto>>> GetCompanyAll();
        Task<GenericResponse<CompanyDto>> GetCompanyById(int Id);
        Task<GenericResponse<CompanyDto>> PostCompany([FromBody] CompanyRequest companyRequest);
        Task<GenericResponse<CompanyDto>> PutCompany(int id, [FromBody] CompanyRequest companyRequest);
    }
}
