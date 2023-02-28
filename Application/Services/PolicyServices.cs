using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services
{
    public class PolicyServices : IPolicyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PolicyServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        //GETALL
        public async Task<GenericResponse<List<PolicyDto>>> GetPolicyAll()
        {
            GenericResponse<List<PolicyDto>> response = new GenericResponse<List<PolicyDto>>();
            var entidades = await _unitOfWork.PolicyRepo.Get(includeProperties: "Vehicle");
            //if(entidades == null) return null;
            var dtos = _mapper.Map<List<PolicyDto>>(entidades);
            response.success = true;
            response.Data = dtos;
            return response;
        }

        //GETALLBYID
        public async Task<GenericResponse<PolicyDto>> GetPolicyById(int Id)
        {
            GenericResponse<PolicyDto> response = new GenericResponse<PolicyDto>();
            var profile = await _unitOfWork.PolicyRepo.Get(filter: p => p.Id == Id, includeProperties: "Vehicle");
            var result = profile.FirstOrDefault();

            if (result == null)
            {
                response.success = false;
                response.AddError("No existe Policy", $"No existe Policy con el Id {Id} solicitado", 1);
                return response;
            }


            var PolicyDTO = _mapper.Map<PolicyDto>(result);
            response.success = true;
            response.Data = PolicyDTO;
            return response;

        }

        //POST
        public async Task<GenericResponse<PolicyDto>> PostPolicy([FromBody] PolicyRequest policyRequest)
        {

            GenericResponse<PolicyDto> response = new GenericResponse<PolicyDto>();

            if (policyRequest.VehicleId.HasValue)
            {
                var existeVehicle = await _unitOfWork.VehicleRepo.Get(p => p.Id == policyRequest.VehicleId);
                var resultVehicle = existeVehicle.FirstOrDefault();

                if (resultVehicle == null)
                {
                    response.success = false;
                    response.AddError("No existe Vehicle", $"No existe Vehicle con el Id {policyRequest.VehicleId} para cargar", 1);
                    return response;
                }



                var existePolicyVehicle = await _unitOfWork.PolicyRepo.Get(filter: p => p.VehicleId == policyRequest.VehicleId);
                var resultPolicyVehicle = existePolicyVehicle.FirstOrDefault();

                if (resultPolicyVehicle == null)
                {
                    var entidad = _mapper.Map<Policy>(policyRequest);
                    await _unitOfWork.PolicyRepo.Add(entidad);
                    await _unitOfWork.SaveChangesAsync();
                    response.success = true;
                    var PolicyDto = _mapper.Map<PolicyDto>(entidad);
                    response.Data = PolicyDto;
                    return response;
                }
                else
                {

                    response.success = false;
                    response.AddError("No se puede asignar la misma poliza a otro vehiculo", $"No se puede poner {policyRequest.VehicleId} para cargar", 1);
                    return response;
                }

            }
            else
            {

                var entidad = _mapper.Map<Policy>(policyRequest);
                await _unitOfWork.PolicyRepo.Add(entidad);
                await _unitOfWork.SaveChangesAsync();
                response.success = true;
                var PolicyDto = _mapper.Map<PolicyDto>(entidad);
                response.Data = PolicyDto;
                return response;
            }


        }

        //Put
        public async Task<GenericResponse<PolicyDto>> PutPolicy(PolicyUpdateRequest request)
        {
            GenericResponse<PolicyDto> response = new GenericResponse<PolicyDto>();
            try
            {
                var policy = await _unitOfWork.PolicyRepo.GetById(request.PolicyId);
                //Verificar que la poliza existe
                if (policy == null)
                {
                    response.success = false;
                    response.AddError("No existe registro de Policy", $"No existe registro de Policy con el Id {request.PolicyId} solicitado", 1);
                    return response;
                }

                if (request.VehicleId.HasValue)
                {
                    var vehicle = await _unitOfWork.VehicleRepo.GetById(request.VehicleId.Value);
                    if(vehicle == null)
                    {
                        response.success = false;
                        response.AddError("Error", "El vehiculo especificado no existe", 2);
                        return response;
                    }
                    policy.VehicleId = request.VehicleId.Value;
                }

                policy.PolicyNumber = request.PolicyNumber ?? policy.PolicyNumber;
                policy.ExpirationDate = request.ExpirationDate ?? policy.ExpirationDate;

                //Guardar cambios
                await _unitOfWork.PolicyRepo.Update(policy);
                await _unitOfWork.SaveChangesAsync();

                response.success = true;
                var dto = _mapper.Map<PolicyDto>(policy);
                response.Data = dto;
                return response;
            }
            catch(Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message,1);
                return response;
            }
        }

        //Delete
        public async Task<GenericResponse<PolicyDto>> DeletePolicy(int Id)
        {
            GenericResponse<PolicyDto> response = new GenericResponse<PolicyDto>();
            var entidad = await _unitOfWork.PolicyRepo.Get(filter: p => p.Id == Id);
            var result = entidad.FirstOrDefault();
            if (result == null)
            {
                response.success = false;
                response.AddError("No existe registro de Policy", $"No existe registro de Policy con el Id {Id} solicitado", 1);
                return response;
            }

            var existe = await _unitOfWork.PolicyRepo.Delete(Id);
            await _unitOfWork.SaveChangesAsync();

            var PolicyDTO = _mapper.Map<PolicyDto>(result);
            response.success = true;
            response.Data = PolicyDTO;

            return response;
        }



    }
}
