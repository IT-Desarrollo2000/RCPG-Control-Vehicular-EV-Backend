using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Identity;

namespace Application.Interfaces
{
    public interface IProfileServices
    {
        Task<ProfileDto> GetCurrentProfile(AppUser user);
        Task<ProfileDto> GetUserProfile(int UserId);
        Task<GenericResponse<ProfileDto>> UploadProfileImage(ProfileImageRequest request);
    }
}
