using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Application.Interfaces
{
    public interface IDestinationOfReportUseService
    {
        Task<GenericResponse<DestinationOfReportUseDto>> DeleteDestinationOfReportUse(int Id);
        Task<GenericResponse<List<DestinationOfReportUseDto>>> GetDestinationOfReportUseAll();
        Task<GenericResponse<DestinationOfReportUseDto>> GetDestinationOfReportUseById(int Id);
        Task<GenericResponse<DestinationOfReportUseDto>> PostDestinationOfReportUse([FromBody] DestinationOfReportUseRequest destinationOfReportUseRequest);
        Task<GenericResponse<DestinationOfReportUseDto>> PutDestinationOfReportUse(int Id, [FromBody] DestinationOfReportUseRequest destinationOfReportUseRequest);
    }
}
