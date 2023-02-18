using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ToolsServices : IToolsServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ToolsServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

       /* //GETALL
        public async Task<GenericResponse<List<VehicleReportUseDto>>> GetAll()
        {
            GenericResponse<List<PolicyDto>> response = new GenericResponse<List<PolicyDto>>();
            var entidades = await _unitOfWork.PolicyRepo.Get(includeProperties: "Vehicle");
            //if(entidades == null) return null;
            var dtos = _mapper.Map<List<PolicyDto>>(entidades);
            response.success = true;
            response.Data = dtos;
            return response;
        }*/

        //public async Task<object> GetLicencesExpirations(LicenceExpStopLight request)
        //{
        //    GenericResponse<object> response = new GenericResponse<object>();

        //    try
        //    {
        //        //Consultar la lista de licencias
        //        //
        //        var licences = await _unitOfWork.UserProfileRepo.Get(l => l.LicenceExpirationDate != null);

        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.success = true;

        //    }
        //}
    }
}
