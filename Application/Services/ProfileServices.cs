using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Identity;
using Microsoft.Extensions.Options;

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
            var profile = await _unitOfWork.UserProfileRepo.Get(filter: p => p.UserId == UserId);
            var result = profile.FirstOrDefault();
            var profileDto = _mapper.Map<ProfileDto>(result);

            var profileImageUrl = await _blobStorageService.GetFileUrl(_azureBlobContainers.Value.UserProfiles, result.ProfileImagePath);
            profileDto.ProfileImageUrl = profileImageUrl;

            return profileDto;
        }

        public async Task<ProfileDto> GetCurrentProfile(AppUser user)
        {
            var profile = await _unitOfWork.UserProfileRepo.Get(filter: p => p.UserId == user.Id);
            var result = profile.FirstOrDefault();
            var profileDto = _mapper.Map<ProfileDto>(result);

            var profileImageUrl = await _blobStorageService.GetFileUrl(_azureBlobContainers.Value.UserProfiles, result.ProfileImagePath);
            profileDto.ProfileImageUrl = profileImageUrl;

            return profileDto;
        }

        public async Task<GenericResponse<ProfileDto>> UploadProfileImage(ProfileImageRequest request)
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

                GenericResponse<ProfileDto> response = new GenericResponse<ProfileDto>(profileDto);

                return response;
            }
            else
            {
                var profileDto = _mapper.Map<ProfileDto>(profile);
                GenericResponse<ProfileDto> response = new GenericResponse<ProfileDto>(profileDto);
                response.AddError("Invalid File Type", "El archivo no corresponde a un tipo de imagen");

                return response;
            }
        }

    }
}
