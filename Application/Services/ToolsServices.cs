using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Reponses;

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

        //GetAllVehicleStatus
        public async Task<GenericResponse<List<GetVehicleActiveDto>>> GetAllVehiclesActive()
        {
            GenericResponse<List<GetVehicleActiveDto>> response = new GenericResponse<List<GetVehicleActiveDto>>();
            var VehicleA = await _unitOfWork.VehicleReportUseRepo.Get(filter: status => status.StatusReportUse == Domain.Enums.ReportUseType.enProceso, includeProperties: "Vehicle,UserProfile,Destinations");
            if (VehicleA == null)
            {
                response.success = false;
                response.AddError("f", "f", 1);
                return response;
            }

            var dtos = _mapper.Map<List<GetVehicleActiveDto>>(VehicleA);
            response.success = true;
            response.Data = dtos;
            return response;
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
