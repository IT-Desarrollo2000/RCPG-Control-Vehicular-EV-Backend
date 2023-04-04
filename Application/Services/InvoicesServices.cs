using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class InvoicesServices: IInvoicesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InvoicesServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<GenericResponse<InvoicesDto>> AddInvoices(int ExpensesId, InvoicesRequest invoicesRequest)
        {

            GenericResponse<InvoicesDto> response = new GenericResponse<InvoicesDto>();
            var entity = _mapper.Map<Invoices>(invoicesRequest);
            var existegasto = await _unitOfWork.ExpensesRepo.Get(v => v.Id == ExpensesId);
            var resultExpenses = existegasto.FirstOrDefault();

            if (resultExpenses == null)
            {
                response.success = false;
                response.AddError("No existe gasto", $"No existe gasto con el id{ExpensesId} solicitado", 2);
                return response;
            }
            //entity.Vehicle = _unitOfWork.VehicleRepo.Get(VehicleDB => VehicleDB.Id == vehicleId);
            entity.ExpensesId = ExpensesId;
            await _unitOfWork.InvoicesRepo.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            response.success = true;
            var InvoicesDto = _mapper.Map<InvoicesDto>(entity);
            response.Data = InvoicesDto;
            return response;
        }
        public async Task<GenericResponse<InvoicesDto>> GetInvoicesById(int id)
        {
            GenericResponse<InvoicesDto> response = new GenericResponse<InvoicesDto>();
            var entity = await _unitOfWork.InvoicesRepo.Get(filter: a => a.Id == id);
            var check = entity.FirstOrDefault();
            var map = _mapper.Map<InvoicesDto>(check);
            response.success = true;
            response.Data = map;
            return response;
        }

        public async Task<GenericResponse<List<InvoicesDto>>> GetInvoicesExpensesById(int ExpensesId)
        {
            GenericResponse<List<InvoicesDto>> response = new GenericResponse<List<InvoicesDto>>();
            var entity = await _unitOfWork.InvoicesRepo.Get(filter: a => a.ExpensesId == ExpensesId);
            var check = entity;
            var map = _mapper.Map<List<InvoicesDto>>(check);
            response.success = true;
            response.Data = map;
            return response;
        }

        public async Task<GenericResponse<Invoices>> DeleteInvoices(int id)
        {
            GenericResponse<Invoices> response = new GenericResponse<Invoices>();
            var check = await _unitOfWork.InvoicesRepo.GetById(id);
            if (check == null) return null;
            var exists = await _unitOfWork.InvoicesRepo.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            var invdto = _mapper.Map<Invoices>(check);
            response.success = true;
            response.Data = invdto;
            return response;
        }

        public async Task<GenericResponse<InvoicesDto>> PutInvoices(InvoicesUpdate invoicesRequest, int id)
        {

            GenericResponse<InvoicesDto> response = new GenericResponse<InvoicesDto>();
            var result = await _unitOfWork.InvoicesRepo.Get(r => r.Id == id);
            var check = result.FirstOrDefault();
            if (check == null) return null;

            check.Folio = invoicesRequest.Folio;
            check.InvoicedDate = invoicesRequest.InvoicedDate;


            await _unitOfWork.InvoicesRepo.Update(check);
            await _unitOfWork.SaveChangesAsync();
            var map = _mapper.Map<InvoicesDto>(check);
            response.success = true;
            response.Data = map;
            return response;

        }
    }
}
