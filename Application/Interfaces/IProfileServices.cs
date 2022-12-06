using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProfileServices
    {
        Task<ProfileDto> GetCurrentProfile(AppUser user);
        Task<ProfileDto> GetUserProfile(int UserId);
        Task<GenericResponse<ProfileDto>> UploadProfileImage(ProfileImageRequest request);
    }
}
