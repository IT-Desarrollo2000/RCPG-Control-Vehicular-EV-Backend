using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Identity;
using Domain.Entities.Profiles;
using Domain.Entities.User_Approvals;
using Microsoft.Extensions.Options;
using System.Runtime.Intrinsics.Arm;

namespace Application.Services
{
    public class ProfileServices : IProfileServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOptions<BlobContainers> _azureBlobContainers;
        private readonly IBlobStorageService _blobStorageService;
        private readonly IMapper _mapper;

        public ProfileServices(IUnitOfWork unitOfWork, IOptions<BlobContainers> azureBlobContainers, IBlobStorageService blobStorageService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _azureBlobContainers = azureBlobContainers;
            _blobStorageService = blobStorageService;
            _mapper = mapper;
        }

        //Obtener perfil de usuario por Id
        public async Task<ProfileDto> GetUserProfile(int UserId)
        {
            var profile = await _unitOfWork.UserProfileRepo.Get(filter: p => p.UserId == UserId, includeProperties: "Department");
            var result = profile.FirstOrDefault();
            var profileDto = _mapper.Map<ProfileDto>(result);

            var profileImageUrl = await _blobStorageService.GetFileUrl(_azureBlobContainers.Value.UserProfiles, result.ProfileImagePath);
            profileDto.ProfileImageUrl = profileImageUrl;

            return profileDto;
        }

        public async Task<ProfileDto> GetCurrentProfile(AppUser user)
        {
            var profile = await _unitOfWork.UserProfileRepo.Get(filter: p => p.UserId == user.Id, includeProperties: "Department");
            var result = profile.FirstOrDefault();
            var profileDto = _mapper.Map<ProfileDto>(result);

            var profileImageUrl = await _blobStorageService.GetFileUrl(_azureBlobContainers.Value.UserProfiles, result.ProfileImagePath);
            profileDto.ProfileImageUrl = profileImageUrl;

            return profileDto;
        }

        public async Task<GenericResponse<ProfileDto>> UploadProfileImage(ProfileImageRequest request)
        {
            GenericResponse<ProfileDto> response = new GenericResponse<ProfileDto>();
            try
            {
                var profile = await _unitOfWork.UserProfileRepo.GetById(request.UserProfileId);
                if (profile == null) return null;

                if (profile.ProfileImagePath != null)
                {
                    await _blobStorageService.DeleteFileFromBlobAsync(_azureBlobContainers.Value.UserProfiles, profile.ProfileImagePath);
                }

                if (request.ImageFile.ContentType.Contains("image"))
                {
                    var uploadDate = DateTime.UtcNow;
                    string FileExtn = System.IO.Path.GetExtension(request.ImageFile.FileName);
                    var filePath = $"{profile.Id}/{uploadDate.Day}{uploadDate.Month}{uploadDate.Year}_ProfileImage{FileExtn}";
                    var uploadedUrl = await _blobStorageService.UploadFileToBlobAsync(request.ImageFile, _azureBlobContainers.Value.UserProfiles, filePath);

                    profile.ProfileImagePath = filePath;
                    profile.ProfileImageUrl = await _blobStorageService.GetFileUrl(_azureBlobContainers.Value.UserProfiles, filePath);

                    await _unitOfWork.UserProfileRepo.Update(profile);
                    await _unitOfWork.SaveChangesAsync();

                    var profileDto = _mapper.Map<ProfileDto>(profile);
                    profileDto.ProfileImageUrl = uploadedUrl;

                    response.success = true;
                    response.Data = profileDto;

                    return response;
                }
                else
                {
                    response.success = false;
                    response.AddError("Invalid File Type", "El archivo no corresponde a un tipo de imagen");

                    return response;
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        public async Task<GenericResponse<ProfileDto>> UpdateDriverLicence(ApprovalCreationRequest request)
        {
            GenericResponse<ProfileDto> response = new GenericResponse<ProfileDto>();
            try
            {
                //Validar de que el perfil exista
                var userProfile = await _unitOfWork.UserProfileRepo.GetById(request.ProfileId);
                if (userProfile == null)
                {
                    response.success = false;
                    response.AddError("Profile not found", "No se pudo identificar el perfil de usuario solicitado", 2);

                    return response;
                }

                //Modificar el elemento
                userProfile.LicenceExpeditionDate = request.LicenceExpeditionDate;
                userProfile.LicenceExpirationDate = request.LicenceExpirationDate;
                userProfile.LicenceType = request.LicenceType;
                userProfile.LicenceValidityYears = request.LicenceValidityYears;
                    
                //Validar imagenes y Guardar las imagenes en el blobstorage
                if (request.DriversLicenceFrontFile.ContentType.Contains("image") && request.DriversLicenceBackFile.ContentType.Contains("image"))
                {


                    //Manipular el nombre de archivo
                    Random rndm = new Random();
                    var uploadDate = DateTime.UtcNow;
                    string FileExtnFront = System.IO.Path.GetExtension(request.DriversLicenceFrontFile.FileName);
                    string FileExtnBack = System.IO.Path.GetExtension(request.DriversLicenceBackFile.FileName);
                    var filePathFront = $"{request.ProfileId}/{uploadDate.Day}{uploadDate.Month}{uploadDate.Year}_LicenceFront{rndm.Next(1, 1000)}{FileExtnFront}";
                    var filePathBack = $"{request.ProfileId}/{uploadDate.Day}{uploadDate.Month}{uploadDate.Year}_LicenceBack{rndm.Next(1, 1000)}{FileExtnBack}";
                    var uploadedUrlFront = await _blobStorageService.UploadFileToBlobAsync(request.DriversLicenceFrontFile, _azureBlobContainers.Value.DriverLicences, filePathFront);
                    var uploadedUrlBack = await _blobStorageService.UploadFileToBlobAsync(request.DriversLicenceBackFile, _azureBlobContainers.Value.DriverLicences, filePathBack);

                    userProfile.DriversLicenceFrontPath = filePathFront;
                    userProfile.DriversLicenceBackPath = filePathBack;
                    userProfile.DriversLicenceFrontUrl = await _blobStorageService.GetFileUrl(_azureBlobContainers.Value.DriverLicences, filePathFront);
                    userProfile.DriversLicenceBackUrl = await _blobStorageService.GetFileUrl(_azureBlobContainers.Value.DriverLicences, filePathBack);

                    await _unitOfWork.UserProfileRepo.Update(userProfile);
                    await _unitOfWork.SaveChangesAsync();

                    response.success = true;
                    var dto = _mapper.Map<ProfileDto>(userProfile);
                    response.Data = dto;

                    return response;
                }
                else
                {
                    response.success = false;
                    response.AddError("Archivo de Imagen Invalido", "Uno o mas archivos no corresponden a un archivo de Imagen");

                    return response;
                }
            }
            catch(Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

    }
}
