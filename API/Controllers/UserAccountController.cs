using Application.Interfaces;
using AutoMapper;
using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Identity;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace API.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public UserAccountController(UserManager<AppUser> userManager,
            ITokenService tokenService,
            IMapper mapper,
            TokenValidationParameters tokenValidationParameters,
            IIdentityService identityService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _tokenValidationParameters = tokenValidationParameters;
            _identityService = identityService;
        }

        #region ..::Registro y Autenticación::..
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("WebAdm/Register")]
        public async Task<ActionResult> RegisterWebAdmUser([FromBody] WebAdmUserRegistrationRequest user)
        {
            var existingUser = await _userManager.FindByNameAsync(user.UserName);

            if (existingUser != null)
            {
                var response = new GenericResponse<AppUser>();
                response.success = false;
                response.AddError("User Exists", "Ya existe un usuario registrado con las mismas credenciales", 1);
                return BadRequest(response);
            }

            var isCreated = await _identityService.CreateWebAdmUserAsync(user);
            if (isCreated.Succeeded)
            {
                existingUser = await _userManager.FindByNameAsync(user.UserName);

                return Ok(await _tokenService.CreateToken(existingUser));
            }
            else
            {
                var identityErrors = isCreated.Errors.ToList();
                List<string> errorList = new List<string>();

                foreach (var error in identityErrors)
                {
                    errorList.Add(error.Description);
                }

                var errorResponse = new GenericResponse<AuthResult>();
                errorResponse.success = false;

                foreach (var error in errorList)
                {
                    errorResponse.AddError("Error", error, 0);
                }

                return BadRequest(errorResponse);
            }

        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("WebAdm/UserRoles/Remove")]
        public async Task<ActionResult> RemoveUserFromRoles([FromBody] WebAdmRolesRequest user)
        {
            if (!ModelState.IsValid) BadRequest(ModelState);
            var request = await _identityService.RemoveUserFromRoles(user);

            if (request != null)
            {
                return Ok(request);
            }
            else
            {
                ModelState.AddModelError("Error", "No fue posible completar la operación");
                return BadRequest(ModelState);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("WebAdm/UserRoles/Add")]
        public async Task<ActionResult> AddUserToRoles([FromBody] WebAdmRolesRequest user)
        {
            if (!ModelState.IsValid) BadRequest(ModelState);
            var request = await _identityService.AddUserToRoles(user);

            if (request != null)
            {
                return Ok(request);
            }
            else
            {
                ModelState.AddModelError("Error", "No fue posible completar la operación");
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [Route("WebAdm/Login")]
        public async Task<ActionResult> LoginWebAdmUser(WebAdmUserLoginRequest logindDto)
        {
            var existingUser = await _userManager.FindByNameAsync(logindDto.Username);
            if (existingUser == null)
            {
                var errorResponse = new GenericResponse<AuthResult>();
                errorResponse.success = false;

                errorResponse.AddError("Invalid credentials", "Credenciales invalidas", 1);

                return BadRequest(errorResponse);
            }

            var isCorrect = await _identityService.LoginWebAdmUser(existingUser, logindDto.Password);
            if (isCorrect.Success)
            {
                return Ok(await _tokenService.CreateToken(existingUser));
            }
            else
            {
                return BadRequest(isCorrect);
            }
        }

        [HttpPost]
        [Route("App/Register")]
        public async Task<ActionResult> RegisterAppUser([FromBody] AppUserRegistrationRequest user)
        {
            var existingUser = await _identityService.GetAppUserByPhoneEmail(user.Email);
            if (existingUser != null)
            {
                var errorResponse = new GenericResponse<AuthResult>();
                errorResponse.success = false;

                errorResponse.AddError("Existing Email", "Ya existe un usuario registrado bajo el mismo correo", 1);

                return BadRequest(errorResponse);
            }

            var isCreated = await _identityService.CreateAppUserAsync(user);

            if (isCreated.Result.Succeeded)
            {
                return Ok(isCreated.Profile);
            }
            else
            {
                var errorResponse = new GenericResponse<AuthResult>();
                errorResponse.success = false;

                foreach (var error in isCreated.Errors)
                {
                    errorResponse.AddError("Error", error, 0);
                }

                return BadRequest(errorResponse);
            }
        }

        [HttpPost]
        [Route("App/Login")]
        public async Task<ActionResult> LoginAppUser(WebAdmUserLoginRequest logindDto)
        {
            var existingUser = await _identityService.GetAppUserByPhoneEmail(logindDto.Username);
            if (existingUser == null)
            {
                var errorResponse = new GenericResponse<AuthResult>();
                errorResponse.success = false;

                errorResponse.AddError("Invalid Request", "Solicitud de autenticación invalida", 1);

                return BadRequest(errorResponse);
            }

            var isCorrect = await _identityService.LoginAppUser(logindDto.Username, logindDto.Password);
            if (isCorrect.Success)
            {
                return Ok(await _tokenService.CreateToken(existingUser));
            }
            else
            {
                return BadRequest(isCorrect);
            }
        }

        [HttpPost]
        [Route("App/SocialLogin")]
        public async Task<ActionResult> LoginAppUserSocial(AppSocialLoginRequest loginDto)
        {
            var existingUser = await _identityService.GetAppUserBySocial(loginDto.UID, loginDto.Email, loginDto.SocialType);
            if (existingUser == null)
            {
                var errorResponse = new GenericResponse<AuthResult>();
                errorResponse.success = false;

                errorResponse.AddError("Error", "Error no se pudo crear el usuario", 1);

                return BadRequest(errorResponse);
            }

            var isCorrect = await _identityService.LoginSocialAppUser(loginDto.UID);
            if (isCorrect.Success)
            {
                return Ok(await _tokenService.CreateToken(existingUser));
            }
            else
            {
                return BadRequest(isCorrect);
            }
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<ActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
        {
            var res = await _tokenService.VerifyToken(tokenRequest, _tokenValidationParameters);
            if (res == null)
            {
                var errorResponse = new GenericResponse<AuthResult>();
                errorResponse.success = false;

                errorResponse.AddError("Error", "Tokens invalidos", 1);

                return BadRequest(errorResponse);
            }

            return Ok(res);
        }

        [Authorize]
        [HttpPost]
        [Route("Logout")]
        public async Task<ActionResult> UserLogout([FromBody] TokenRequest tokenRequest)
        {
            var res = await _tokenService.VerifyToken(tokenRequest, _tokenValidationParameters);
            if (res == null)
            {
                var errorResponse = new GenericResponse<AuthResult>();
                errorResponse.success = false;

                errorResponse.AddError("Error", "Tokens invalidos", 1);

                return BadRequest(errorResponse);
            }

            return Ok(res);
        }
        #endregion

        #region ..::Administración de usuarios AdminWeb::..
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("WebAdm/GetAdminUsers")]
        public async Task<ActionResult> GetWebAdmUsers(AdminRoleType? roleType = null)
        {
            var users = await _identityService.GetUsersWithRoles(roleType);
            return Ok(users);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("WebAdm/Roles")]
        public async Task<ActionResult> GetRoleList()
        {
            var roles = await _identityService.GetRoleList();
            return Ok(roles);
        }

        [Authorize(Roles = "Administrator, AdminUser")]
        [HttpGet]
        [Route("WebAdm/AppUsers")]
        public async Task<ActionResult> GetCustomers()
        {
            var users = await _identityService.GetCustomers();

            return Ok(users);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("Dummy/Register")]
        public async Task<ActionResult> RegisterDummyAppUser([FromBody] AppUserRegistrationRequest user)
        {
            var existingUser = await _identityService.GetAppUserByPhoneEmail(user.Email);
            if (existingUser != null)
            {
                var errorResponse = new GenericResponse<AuthResult>();
                errorResponse.success = false;

                errorResponse.AddError("Existing Email", "Ya existe un usuario registrado bajo el mismo correo", 1);

                return BadRequest(errorResponse);
            }

            var isCreated = await _identityService.CreateDummyAppUser(user);

            if (isCreated.Result.Succeeded)
            {
                var response = new GenericResponse<AuthResult>();
                response.success = true;

                response.AddError("Task Failed Succesfully", "La operación fallo con exito <:^)", 1);

                return Ok(response);
            }
            else
            {
                var response = new GenericResponse<AuthResult>();
                response.success = false;

                response.AddError("Task Failed", "La operación fallo con exito", 1);

                return BadRequest(response);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        [Route("Dummy/{userId:int}")]
        public async Task<ActionResult> DeleteDummyUser(int userId)
        {
            var isCreated = await _identityService.DeleteUserAsync(userId);

            if (isCreated)
            {
                var response = new GenericResponse<AuthResult>();
                response.success = true;

                response.AddError("Task Failed Succesfully", "La operación fallo con exito <:^)", 1);

                return Ok(response);
            }
            else
            {
                var response = new GenericResponse<AuthResult>();
                response.success = false;

                response.AddError("Task Failed", "Ocurrio un error al borrar el usuario", 1);

                return BadRequest(response);
            }
        }
        #endregion
    }
}
