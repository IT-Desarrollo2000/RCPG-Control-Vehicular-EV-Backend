using Application.Interfaces;
using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IProfileServices _profileServices;
        private readonly UserManager<AppUser> _userManager;
        private readonly IToolsServices _utilitesService;

        public UserProfileController(IProfileServices profileServices, UserManager<AppUser> userManager, IToolsServices utilitesService)
        {
            _profileServices = profileServices;
            _userManager = userManager;
            _utilitesService = utilitesService;
        }

        [Authorize(Roles = "AppUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<ProfileDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpPost]
        [Route("uploadProfileImage")]
        public async Task<IActionResult> UploadProfilePic([FromForm] ProfileImageRequest request)
        {
            var fileSize = request.ImageFile.Length;
            if ((fileSize / 1048576.0) > 10)
            {
                return BadRequest("El archivo excede los 10 Mb de limite");
            }

            var result = await _profileServices.UploadProfileImage(request);

            if (result == null)
            {
                return NotFound("El perfil de usuario no existe");
            }

            if (result.success) { return Ok(result); } else { return BadRequest(result); };
        }

        [Authorize(Roles = "AppUser,Administrator,Supervisor,AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<ProfileDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut]
        [Route("updateDriversLicence")]
        public async Task<IActionResult> UpdateLicence([FromForm] ApprovalCreationRequest request)
        {
            var result = await _profileServices.UpdateDriverLicence(request);

            if (result.success) { return Ok(result); } else { return BadRequest(result); };
        }

        //[Authorize(Roles = "Administrator, AdminUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ProfileDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("getById/{UserId:int}")]
        public async Task<IActionResult> GetUserProfile(int UserId)
        {
            var user = await _profileServices.GetUserProfile(UserId);

            return Ok(user);
        }

        //[Authorize(Roles = "AppUser, Administrator")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ProfileDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("getProfile")]
        public async Task<IActionResult> GetCurrentProfile()
        {
            var currentUser = this.User;
            var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(currentUserName);
            var profile = await _profileServices.GetCurrentProfile(user);

            return Ok(profile);
        }

        //Verificar si el usuario esta en viaje
        [Authorize(Roles = "Administrator, AppUser")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<UserInTravelDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        [Route("IsUserInTravel")]
        public async Task<IActionResult> GetServiceMaintenance(int? userProfileId)
        {
            var currentUser = this.User;
            var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(currentUserName);
            var profile = await _profileServices.GetCurrentProfile(user);

            if (profile == null && userProfileId == null)
            {
                return NotFound("El usuario especificado no cuenta con un perfil de conductor");
            }

            var users = await _utilitesService.IsUserInTravel(userProfileId ?? profile.Id);
            if (users.success)
            {
                return Ok(users);
            }
            else
            {
                return BadRequest(users);
            }
        }
    }
}
