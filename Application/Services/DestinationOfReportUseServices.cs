using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services
{
    public class DestinationOfReportUseServices : IDestinationOfReportUseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DestinationOfReportUseServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        //GETALL
        public async Task<GenericResponse<List<DestinationOfReportUseDto>>> GetDestinationOfReportUseAll()
        {
            GenericResponse<List<DestinationOfReportUseDto>> response = new GenericResponse<List<DestinationOfReportUseDto>>();
            var entidades = await _unitOfWork.DestinationOfReportUseRepo.Get(includeProperties: "VehicleReportUses");
            var dtos = _mapper.Map<List<DestinationOfReportUseDto>>(entidades);
            response.success = true;
            response.Data = dtos;
            return response;
        }


        //GETBYID
        public async Task<GenericResponse<DestinationOfReportUseDto>> GetDestinationOfReportUseById(int Id)
        {
            GenericResponse<DestinationOfReportUseDto> response = new GenericResponse<DestinationOfReportUseDto>();
            var entidades = await _unitOfWork.DestinationOfReportUseRepo.Get(filter: p => p.Id == Id, includeProperties: "VehicleReportUses");
            var result = entidades.FirstOrDefault();

            if (result == null)
            {
                response.success = false;
                response.AddError("No existe DestinationOfResultReportUse", $"No existe DestinationOfResultReportUse con el Id {Id} solicitado", 1);
                return response;
            }


            var DestinationDto = _mapper.Map<DestinationOfReportUseDto>(result);
            response.success = true;
            response.Data = DestinationDto;
            return response;
        }

        //POST
        public async Task<GenericResponse<DestinationOfReportUseDto>> PostDestinationOfReportUse([FromBody] DestinationOfReportUseRequest destinationOfReportUseRequest)
        {

            GenericResponse<DestinationOfReportUseDto> response = new GenericResponse<DestinationOfReportUseDto>();

            if (destinationOfReportUseRequest.VehicleReportUseId.HasValue)
            {
                var existeVehicleReportUse = await _unitOfWork.VehicleReportUseRepo.Get(p => p.Id == destinationOfReportUseRequest.VehicleReportUseId.Value);
                var resultVehicleReportUse = existeVehicleReportUse.FirstOrDefault();

                if (resultVehicleReportUse == null)
                {
                    response.success = false;
                    response.AddError("No existe VehicleReportUse", $"No existe VehicleReportUseId {destinationOfReportUseRequest.VehicleReportUseId} para cargar", 1);
                    return response;
                }

                var entidad = _mapper.Map<DestinationOfReportUse>(destinationOfReportUseRequest);
                await _unitOfWork.DestinationOfReportUseRepo.Add(entidad);
                await _unitOfWork.SaveChangesAsync();
                response.success = true;
                var DestinatioDto = _mapper.Map<DestinationOfReportUseDto>(entidad);
                response.Data = DestinatioDto;
                return response;
            }
            else
            {

                var entidad = _mapper.Map<DestinationOfReportUse>(destinationOfReportUseRequest);
                await _unitOfWork.DestinationOfReportUseRepo.Add(entidad);
                await _unitOfWork.SaveChangesAsync();
                response.success = true;
                var DestinatioDto = _mapper.Map<DestinationOfReportUseDto>(entidad);
                response.Data = DestinatioDto;
                return response;
            }


        }

        //Put
        public async Task<GenericResponse<DestinationOfReportUseDto>> PutDestinationOfReportUse(int Id, [FromBody] DestinationOfReportUseRequest destinationOfReportUseRequest)
        {
            GenericResponse<DestinationOfReportUseDto> response = new GenericResponse<DestinationOfReportUseDto>();
            var existeDestinationOfReportUse = await _unitOfWork.DestinationOfReportUseRepo.Get(p => p.Id == Id);
            var result = existeDestinationOfReportUse.FirstOrDefault();

            if (result == null)
            {
                response.success = false;
                response.AddError("No existe registro de DestinationOfReportUse", $"No existe registro de DestinationOfReportUse con el Id {Id} solicitado", 1);
                return response;
            }

            if (destinationOfReportUseRequest.VehicleReportUseId.HasValue)
            {
                var existeVehicleReportUse = await _unitOfWork.VehicleReportUseRepo.Get(p => p.Id == destinationOfReportUseRequest.VehicleReportUseId.Value);
                var resultVehicleReportUse = existeVehicleReportUse.FirstOrDefault();

                if (resultVehicleReportUse == null)
                {
                    response.success = false;
                    response.AddError("No existe VehicleReportUse", $"No existe VehicleReportUseId {destinationOfReportUseRequest.VehicleReportUseId} para cargar", 1);
                    return response;
                }

                result.DestinationName = destinationOfReportUseRequest.DestinationName;
                result.Latitud = destinationOfReportUseRequest.Latitud;
                result.Longitude = destinationOfReportUseRequest.Longitude;
                result.VehicleReportUseId = destinationOfReportUseRequest.VehicleReportUseId;

                await _unitOfWork.DestinationOfReportUseRepo.Update(result);
                await _unitOfWork.SaveChangesAsync();

                var VehicleReportUseDTO = _mapper.Map<DestinationOfReportUseDto>(result);
                response.success = true;
                response.Data = VehicleReportUseDTO;

                return response;


            }
            else
            {

                result.Latitud = destinationOfReportUseRequest.Latitud;
                result.Longitude = destinationOfReportUseRequest.Longitude;
                result.VehicleReportUseId = destinationOfReportUseRequest.VehicleReportUseId;

                await _unitOfWork.DestinationOfReportUseRepo.Update(result);
                await _unitOfWork.SaveChangesAsync();

                var VehicleReportUseDTO = _mapper.Map<DestinationOfReportUseDto>(result);
                response.success = true;
                response.Data = VehicleReportUseDTO;

                return response;


            }

        }

        //Delete
        public async Task<GenericResponse<DestinationOfReportUseDto>> DeleteDestinationOfReportUse(int Id)
        {
            GenericResponse<DestinationOfReportUseDto> response = new GenericResponse<DestinationOfReportUseDto>();
            var entidad = await _unitOfWork.DestinationOfReportUseRepo.Get(filter: p => p.Id == Id);
            var result = entidad.FirstOrDefault();
            if (result == null)
            {
                return null;
            }

            var existe = await _unitOfWork.DestinationOfReportUseRepo.Delete(Id);
            await _unitOfWork.SaveChangesAsync();

            var DestinationDTO = _mapper.Map<DestinationOfReportUseDto>(result);
            response.success = true;
            response.Data = DestinationDTO;

            return response;
        }



    }
}
