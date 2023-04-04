using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Registered_Cars;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Application.Services
{
    public class PolicyServices : IPolicyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IOptions<BlobContainers> _azureBlobContainers;
        private readonly IBlobStorageService _blobStorageService;

        public PolicyServices(IUnitOfWork unitOfWork, IMapper mapper, IOptions<BlobContainers> azureBlobContainers, IBlobStorageService blobStorageService)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            _azureBlobContainers = azureBlobContainers;
            _blobStorageService = blobStorageService;
        }

        //GETALL
        public async Task<GenericResponse<List<PolicyDto>>> GetPolicyAll()
        {
            GenericResponse<List<PolicyDto>> response = new GenericResponse<List<PolicyDto>>();
            var entidades = await _unitOfWork.PolicyRepo.Get(includeProperties: "Vehicle,PhotosOfPolicies");
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
            var profile = await _unitOfWork.PolicyRepo.Get(filter: p => p.Id == Id, includeProperties: "Vehicle,PhotosOfPolicies");
            var result = profile.FirstOrDefault();

            if (result == null)
            {
                response.success = false;
                response.AddError("No existe Policy", $"No existe Policy con el Id {Id} solicitado", 2);
                return response;
            }


            var PolicyDTO = _mapper.Map<PolicyDto>(result);
            response.success = true;
            response.Data = PolicyDTO;
            return response;

        }

        //POST
        public async Task<GenericResponse<PolicyDto>> PostPolicy([FromForm] PolicyRequest policyRequest)
        {

            GenericResponse<PolicyDto> response = new GenericResponse<PolicyDto>();

            var Policy = _mapper.Map<Policy>(policyRequest);
            
            if (policyRequest.VehicleId.HasValue)
            {
                var existeVehicle = await _unitOfWork.VehicleRepo.Get(p => p.Id == policyRequest.VehicleId);
                var resultVehicle = existeVehicle.FirstOrDefault();

                if (resultVehicle == null)
                {
                    response.success = false;
                    response.AddError("No existe Vehicle", $"No existe Vehicle con el Id {policyRequest.VehicleId} para cargar", 3);
                    return response;
                }



                var existePolicyVehicle = await _unitOfWork.PolicyRepo.Get(filter: p => p.VehicleId == policyRequest.VehicleId);
                var resultPolicyVehicle = existePolicyVehicle.FirstOrDefault();

                if (resultPolicyVehicle == null)
                {
                    /*
                    await _unitOfWork.PolicyRepo.Add(entidad);
                    await _unitOfWork.SaveChangesAsync();
                    response.success = true;
                    var PolicyDto = _mapper.Map<PolicyDto>(entidad);
                    response.Data = PolicyDto;
                    return response;*/
                    Policy.PolicyNumber = policyRequest.PolicyNumber;
                    Policy.ExpirationDate = (DateTime)policyRequest.ExpirationDate;
                    Policy.NameCompany = policyRequest.NameCompany;
                    Policy.VehicleId = policyRequest.VehicleId;
                    await _unitOfWork.PolicyRepo.Add(Policy);
                }
                else
                {

                    response.success = false;
                    response.AddError("No se puede asignar la misma poliza a otro vehiculo", $"No se puede poner {policyRequest.VehicleId} para cargar", 4);
                    return response;
                }

                var Ima = new List<PhotosOfPolicy>();
                foreach (var image in policyRequest.Images)
                {
                    //Validar Imagenes y Guardar las imagenes en el blobstorage
                    if (image.ContentType.Contains("image"))
                    {
                        //Manipular el nombre del archivo
                        var uploadDate = DateTime.Now;
                        Random rndm = new Random();
                        string FileExtn = System.IO.Path.GetExtension(image.FileName);
                        var filePath = $"FOTOS_POLIZA/{uploadDate.Day}{uploadDate.Month}{uploadDate.Year}_{uploadDate.Hour}{uploadDate.Minute}{rndm.Next(1, 1000)}{FileExtn}";
                        var uploadedUrl = await _blobStorageService.UploadFileToBlobAsync(image, _azureBlobContainers.Value.PolicyImages, filePath);

                        //agregar la imagen a la bd
                        var newImage = new PhotosOfPolicy()
                        {
                            FilePath = filePath,
                            FileURL = await _blobStorageService.GetFileUrl(_azureBlobContainers.Value.PolicyImages, filePath),
                            Policy = Policy
                        };

                        await _unitOfWork.PhotosOfPolicyRepo.Add(newImage);
                        Ima.Add(newImage);
                    }
                    else
                    {
                        response.success = false;
                        response.AddError("Archivo de Imagen Invalido", "Uno o mas archivos no corresponden a un archivo de Imagen", 5);

                        return response;
                    }

                }

                //Guardar Cambios
                await _unitOfWork.SaveChangesAsync();
                response.success = true;
                var PolicyDto = _mapper.Map<PolicyDto>(policyRequest);
                response.Data= PolicyDto;
                return response;

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
                    response.AddError("No existe registro de Policy", $"No existe registro de Policy con el Id {request.PolicyId} solicitado", 5);
                    return response;
                }

                if (request.VehicleId.HasValue)
                {
                    var vehicle = await _unitOfWork.VehicleRepo.GetById(request.VehicleId.Value);
                    if(vehicle == null)
                    {
                        response.success = false;
                        response.AddError("Error", "El vehiculo especificado no existe", 6);
                        return response;
                    }
                    policy.VehicleId = request.VehicleId.Value;
                }

                policy.PolicyNumber = request.PolicyNumber ?? policy.PolicyNumber;
                policy.ExpirationDate = request.ExpirationDate ?? policy.ExpirationDate;
                policy.NameCompany = request.NameCompany ?? policy.NameCompany;
                //await _unitOfWork.PolicyRepo.Update(policy);

                var policyIma = await _unitOfWork.PhotosOfPolicyRepo.Get(filter: policy => policy.PolicyId == request.PolicyId);    
                foreach ( var photo in policyIma)
                {
                    await _blobStorageService.DeleteFileFromBlobAsync(_azureBlobContainers.Value.PolicyImages, photo.FilePath);
                    await _unitOfWork.PhotosOfPolicyRepo.Delete(photo.Id);
                }

                var Ima = new List<PhotosOfPolicy>();
                foreach (var image in request.Images)
                {
                    //Validar Imagenes y Guardar las imagenes en el blobstorage
                    if (image.ContentType.Contains("image"))
                    {
                        //Manipular el nombre del archivo
                        var uploadDate = DateTime.Now;
                        Random rndm = new Random();
                        string FileExtn = System.IO.Path.GetExtension(image.FileName);
                        var filePath = $"FOTOS_POLIZA/{uploadDate.Day}{uploadDate.Month}{uploadDate.Year}_{uploadDate.Hour}{uploadDate.Minute}{rndm.Next(1, 1000)}{FileExtn}";
                        var uploadedUrl = await _blobStorageService.UploadFileToBlobAsync(image, _azureBlobContainers.Value.PolicyImages, filePath);

                        //agregar la imagen a la bd
                        var newImage = new PhotosOfPolicy()
                        {
                            FilePath = filePath,
                            FileURL = await _blobStorageService.GetFileUrl(_azureBlobContainers.Value.PolicyImages, filePath),
                            Policy = policy
                        };

                        await _unitOfWork.PhotosOfPolicyRepo.Add(newImage);
                        Ima.Add(newImage);
                    }
                    else
                    {
                        response.success = false;
                        response.AddError("Archivo de Imagen Invalido", "Uno o mas archivos no corresponden a un archivo de Imagen", 5);

                        return response;
                    }

                }

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
                response.AddError("No existe registro de Policy", $"No existe registro de Policy con el Id {Id} solicitado", 7);
                return response;
            }

            //Borrar Fotos de la poliza
            var policyIma = await _unitOfWork.PhotosOfPolicyRepo.Get(filter: policy => policy.PolicyId == Id);
            foreach (var photo in policyIma)
            {
                await _blobStorageService.DeleteFileFromBlobAsync(_azureBlobContainers.Value.PolicyImages, photo.FilePath);
                await _unitOfWork.PhotosOfPolicyRepo.Delete(photo.Id);
            }

            var existe = await _unitOfWork.PolicyRepo.Delete(Id);
            await _unitOfWork.SaveChangesAsync();

            var PolicyDTO = _mapper.Map<PolicyDto>(result);
            response.success = true;
            response.Data = PolicyDTO;

            return response;
        }

        //AddImageIndivual
        public async Task<GenericResponse<PhotosOfPolicy>> AddPolicyImage (PolicyImagesRequest policyImagesRequest, int policyId)
        {
            GenericResponse<PhotosOfPolicy> response = new GenericResponse<PhotosOfPolicy>();

            try
            {
                //VERIFICAR QUE exista la poliza
                var Policy = await _unitOfWork.PolicyRepo.GetById(policyId);
                if (Policy == null) return null;



                var Ima = new List<PhotosOfPolicy>();
                foreach (var image in policyImagesRequest.Images)
                {
                    //Validar Imagenes y Guardar las imagenes en el blobstorage
                    if (image.ContentType.Contains("image"))
                    {
                        //Manipular el nombre del archivo
                        var uploadDate = DateTime.Now;
                        Random rndm = new Random();
                        string FileExtn = System.IO.Path.GetExtension(image.FileName);
                        var filePath = $"FOTOS_POLIZA/{uploadDate.Day}{uploadDate.Month}{uploadDate.Year}_{uploadDate.Hour}{uploadDate.Minute}{rndm.Next(1, 1000)}{FileExtn}";
                        var uploadedUrl = await _blobStorageService.UploadFileToBlobAsync(image, _azureBlobContainers.Value.PolicyImages, filePath);

                        //agregar la imagen a la bd
                        var newImage = new PhotosOfPolicy()
                        {
                            FilePath = filePath,
                            FileURL = await _blobStorageService.GetFileUrl(_azureBlobContainers.Value.PolicyImages, filePath),
                            Policy = Policy
                        };

                        await _unitOfWork.PhotosOfPolicyRepo.Add(newImage);
                        Ima.Add(newImage);
                        await _unitOfWork.SaveChangesAsync();

                        response.success = true;
                        response.Data = newImage;

                    }
                    else
                    {
                        response.success = false;
                        response.AddError("Archivo de Imagen Invalido", "Uno o mas archivos no corresponden a un archivo de Imagen", 5);

                        return response;
                    }

                }

                return response;

            }

            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }

        }

        //DeleteImageIndividual
        public async Task<GenericResponse<bool>> DeletePolicyImages (int PolicyId)
        {
            GenericResponse<bool> response = new GenericResponse<bool>();
            try
            {
                //Borrar Fotos de la poliza
                var policyIma = await _unitOfWork.PhotosOfPolicyRepo.Get(filter: policy => policy.PolicyId == PolicyId);
                if (policyIma == null) return null; 
                foreach (var photo in policyIma)
                {
                    await _blobStorageService.DeleteFileFromBlobAsync(_azureBlobContainers.Value.PolicyImages, photo.FilePath);
                    await _unitOfWork.PhotosOfPolicyRepo.Delete(photo.Id);
                    await _unitOfWork.SaveChangesAsync();
                }

                response.success = true;
                response.Data = true;
                return response;

            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;

            }
        }



    }
}
