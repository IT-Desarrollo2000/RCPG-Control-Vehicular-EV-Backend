using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Departament;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services
{
    public class DepartamentServices : IDepartamentServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartamentServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        //GETALL
        public async Task<GenericResponse<List<DepartamentDto>>> GetDepartamentALL()
        {
            GenericResponse<List<DepartamentDto>> response = new GenericResponse<List<DepartamentDto>>();
            var entidades = await _unitOfWork.Departaments.Get(includeProperties: "Company");
            var dtos = _mapper.Map<List<DepartamentDto>>(entidades);
            response.success = true;
            response.Data = dtos;
            return response;
        }

        //GETBYID
        public async Task<GenericResponse<DepartamentDto>> GetDepartamentById(int Id)
        {
            GenericResponse<DepartamentDto> response = new GenericResponse<DepartamentDto>();
            var entidad = await _unitOfWork.Departaments.Get(p => p.Id == Id, includeProperties: "Company");
            var result = entidad.FirstOrDefault();
            var DepartamentDTO = _mapper.Map<DepartamentDto>(result);
            response.success = true;
            response.Data = DepartamentDTO;
            return response;
        }

        //POST
        public async Task<GenericResponse<DepartamentDto>> PostDepartament([FromBody] DepartamentRequest departamentRequest)
        {
            GenericResponse<DepartamentDto> response = new GenericResponse<DepartamentDto>();
            //Validacion si existe compañia
            var existeCompany = await _unitOfWork.Companies.Get(c => c.Id == departamentRequest.CompanyId);
            var resultCompany = existeCompany.FirstOrDefault();
            if (resultCompany == null)
            {
                response.success = false;
                response.AddError("No existe Company", $"No existe Company con el CompanyId {departamentRequest.CompanyId} solicitado", 2);

                return response;
            }

            var entidad = _mapper.Map<Departaments>(departamentRequest);
            await _unitOfWork.Departaments.Add(entidad);
            await _unitOfWork.SaveChangesAsync();
            response.success = true;
            var DepartamentDTO = _mapper.Map<DepartamentDto>(entidad);
            response.Data = DepartamentDTO;
            return response;
        }

        //Put
        public async Task<GenericResponse<DepartamentDto>> PutDepartament(int id, [FromBody] DepartamentRequest departamentRequest)
        {
            GenericResponse<DepartamentDto> response = new GenericResponse<DepartamentDto>();
            var profile = await _unitOfWork.Departaments.Get(p => p.Id == id);
            var result = profile.FirstOrDefault();
            if (result == null) return null;


            var existeCompany = await _unitOfWork.Companies.Get(c => c.Id == departamentRequest.CompanyId);
            var resultCompany = existeCompany.FirstOrDefault();
            if (resultCompany == null)
            {
                response.success = false;
                response.AddError("No existe Company", $"No existe Company con el CompanyId {departamentRequest.CompanyId} solicitado", 2);

                return response;
            }


            result.Name = departamentRequest.name;
            result.CompanyId = departamentRequest.CompanyId;

            await _unitOfWork.Departaments.Update(result);
            await _unitOfWork.SaveChangesAsync();
            var DepartamentDTP = _mapper.Map<DepartamentDto>(result);
            response.success = true;
            response.Data = DepartamentDTP;
            return response;
        }

        //Delete
        public async Task<GenericResponse<DepartamentDto>> DeleteDepartament(int id)
        {
            GenericResponse<DepartamentDto> response = new GenericResponse<DepartamentDto>();
            var entidad = await _unitOfWork.Departaments.GetById(id);
            if (entidad == null)
            {
                return null;
            }
            var existe = await _unitOfWork.Departaments.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            var DepartamentDTO = _mapper.Map<DepartamentDto>(entidad);
            response.success = true;
            response.Data = DepartamentDTO;
            return response;
        }

    }
}
