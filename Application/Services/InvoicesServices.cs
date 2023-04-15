using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Services
{
    public class InvoicesServices: IInvoicesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IOptions<BlobContainers> _azureBlobContainers;
        private readonly IBlobStorageService _blobStorageService;

        public InvoicesServices(IUnitOfWork unitOfWork, IMapper mapper, IOptions<BlobContainers> azureBlobContainers, IBlobStorageService blobStorageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _azureBlobContainers = azureBlobContainers;
            _blobStorageService = blobStorageService;
        }

        public async Task<GenericResponse<InvoicesDto>> AddInvoices(int ExpensesId, InvoicesRequest request)
        {
            GenericResponse<InvoicesDto> response = new GenericResponse<InvoicesDto>();
            try
            {
                var entity = _mapper.Map<Invoices>(request);
                var existegasto = await _unitOfWork.ExpensesRepo.Get(v => v.Id == ExpensesId);
                var resultExpenses = existegasto.FirstOrDefault();

                if (resultExpenses == null)
                {
                    response.success = false;
                    response.AddError("No existe gasto", $"No existe gasto con el id{ExpensesId} solicitado", 2);
                    return response;
                }

                if (request.FilePath1 != null && request.FilePath1.ContentType.Contains("image"))
                {
                    var uploadDate = DateTime.UtcNow;
                    Random rndm = new Random();
                    string FileExtn = System.IO.Path.GetExtension(request.FilePath1.FileName);
                    var filePath = $"{ExpensesId}/{uploadDate.Day}{uploadDate.Month}{uploadDate.Year}_InvoiceImage1{rndm.Next(1, 1000)}{FileExtn}";
                    var uploadedUrl = await _blobStorageService.UploadFileToBlobAsync(request.FilePath1, _azureBlobContainers.Value.ExpenseInvoices, filePath);

                    entity.FilePath1 = filePath;
                    entity.FileURL1 = await _blobStorageService.GetFileUrl(_azureBlobContainers.Value.ExpenseInvoices, filePath);
                }


                if (request.FilePath2 != null && request.FilePath2.ContentType.Contains("image"))
                {
                    var uploadDate = DateTime.UtcNow;
                    Random rndm = new Random();
                    string FileExtn = System.IO.Path.GetExtension(request.FilePath2.FileName);
                    var filePath = $"{ExpensesId}/{uploadDate.Day}{uploadDate.Month}{uploadDate.Year}_InvoiceImage2{rndm.Next(1, 1000)}{FileExtn}";
                    var uploadedUrl = await _blobStorageService.UploadFileToBlobAsync(request.FilePath2, _azureBlobContainers.Value.ExpenseInvoices, filePath);

                    entity.FilePath2 = filePath;
                    entity.FileURL2 = await _blobStorageService.GetFileUrl(_azureBlobContainers.Value.ExpenseInvoices, filePath);
                }

                entity.ExpensesId = ExpensesId;
                await _unitOfWork.InvoicesRepo.Add(entity);
                await _unitOfWork.SaveChangesAsync();
                response.success = true;
                var InvoicesDto = _mapper.Map<InvoicesDto>(entity);
                response.Data = InvoicesDto;
                return response;
            }
            catch(Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
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
            try
            {
                var check = await _unitOfWork.InvoicesRepo.GetById(id);
                if (check == null) return null;

                if(check.FilePath1 != null)
                {
                    await _blobStorageService.DeleteFileFromBlobAsync(_azureBlobContainers.Value.ExpenseInvoices, check.FilePath1);
                }

                if(check.FilePath2 != null)
                {
                    await _blobStorageService.DeleteFileFromBlobAsync(_azureBlobContainers.Value.ExpenseInvoices, check.FilePath2);
                }

                var exists = await _unitOfWork.InvoicesRepo.Delete(id);
                await _unitOfWork.SaveChangesAsync();
                var invdto = _mapper.Map<Invoices>(check);
                response.success = true;
                response.Data = invdto;
                return response;
            }
            catch(Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
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
