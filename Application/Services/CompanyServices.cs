﻿using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Company;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services
{
    public class CompanyServices : ICompanyServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CompanyServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        //GETALL
        public async Task<GenericResponse<List<CompanyDto>>> GetCompanyAll()
        {
            GenericResponse<List<CompanyDto>> response = new GenericResponse<List<CompanyDto>>();
            var entidades = await _unitOfWork.Companies.Get(includeProperties: "Departaments");
            //if(entidades == null) return null;
            var dtos = _mapper.Map<List<CompanyDto>>(entidades);
            response.success = true;
            response.Data = dtos;
            return response;
        }

        //GETALLBYID
        public async Task<GenericResponse<CompanyDto>> GetCompanyById(int Id)
        {
            GenericResponse<CompanyDto> response = new GenericResponse<CompanyDto>();
            //var entidad = await _unitOfWork.Companies.GetById(Id);
            var profile = await _unitOfWork.Companies.Get(filter: p => p.Id == Id, includeProperties: "Departaments");
            var result = profile.FirstOrDefault();
            var CompanyDTO = _mapper.Map<CompanyDto>(result);
            response.success = true;
            response.Data = CompanyDTO;
            return response;

        }

        //POST
        public async Task<GenericResponse<CompanyDto>> PostCompany([FromBody] CompanyRequest companyRequest)
        {
            GenericResponse<CompanyDto> response = new GenericResponse<CompanyDto>();
            var entidad = _mapper.Map<Companies>(companyRequest);
            await _unitOfWork.Companies.Add(entidad);
            await _unitOfWork.SaveChangesAsync();
            response.success = true;
            var CompanyDTO = _mapper.Map<CompanyDto>(entidad);
            response.Data = CompanyDTO;
            return response;
        }

        //PUT
        public async Task<GenericResponse<CompanyDto>> PutCompany(int id, [FromBody] CompanyRequest companyRequest)
        {
            GenericResponse<CompanyDto> response = new GenericResponse<CompanyDto>();
            var profile = await _unitOfWork.Companies.Get(p => p.Id == id);
            var result = profile.FirstOrDefault();
            if(result == null) return null; 

            result.Name = companyRequest.Name;
            result.ReasonSocial = companyRequest.ReasonSocial;

            await _unitOfWork.Companies.Update(result);
            await _unitOfWork.SaveChangesAsync();

            var CompanyDTP = _mapper.Map<CompanyDto>(result);
            response.success = true;
            response.Data = CompanyDTP;
            return response;
        }

        //DELETE
        public async Task<GenericResponse<CompanyDto>> DeleteCompany(int id)
        {
            GenericResponse<CompanyDto> response = new GenericResponse<CompanyDto>();
            //  var entidad = await _unitOfWork.Companies.GetById(id);
            var entidad = await _unitOfWork.Companies.Get(filter: p => p.Id == id);
            var result = entidad.FirstOrDefault();
            if(result == null)
            {
                return null;
            }
            var existe = await _unitOfWork.Companies.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            var CompanyDTO = _mapper.Map<CompanyDto>(entidad);
            response.success = true;
            response.Data = CompanyDTO;
            return response;
        }


    }
}