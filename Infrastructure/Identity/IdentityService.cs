using Application.Interfaces;
using AutoMapper;
using MailKit;
using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Identity;
using Domain.Entities.Profiles;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MailKit.Net.Smtp;
using MimeKit;
using Azure;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IUserClaimsPrincipalFactory<AppUser> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        private readonly IUserApprovalServices _userApprovalServices;
        private readonly SmtpClient _mailClient;

        public IdentityService(
            UserManager<AppUser> userManager, RoleManager<AppRole> roleManager,
            IUserClaimsPrincipalFactory<AppUser> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUserApprovalServices userApprovalServices)
        {
            _userManager = userManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
            _mapper = mapper;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _userApprovalServices = userApprovalServices;
            _mailClient = new SmtpClient();
        }
        #region ..::Registro y Autenticación::..
        public async Task<AuthResult> LoginWebAdmUser(AppUser user, string password)
        {
            var signedIn = await _userManager.CheckPasswordAsync(user, password);

            if (signedIn)
            {
                var response = new AuthResult
                {
                    Success = signedIn,
                    Messages = new List<string>()
                    {
                        "Inicio de sesión correcto"
                    }
                };
                return response;
            }
            else
            {
                var response = new AuthResult
                {
                    Success = signedIn,
                    Errors = new List<string>()
                    {
                        "Usuario y/o contraseña invalidos"
                    }
                };

                return response;
            }
        }

        public async Task<AuthResult> LoginAppUser(string Email, string password)
        {
            var user = await _userManager.Users.Where(u => u.Email == Email).FirstOrDefaultAsync();

            if (user == null)
            {
                var response = new AuthResult
                {
                    Success = false,
                    Errors = new List<string>()
                    {
                        "Usuario y/o contraseña incorrectos"
                    },
                    ErrorCode = 1
                };

                return response;
            }
            else
            {

                //Revisar que el usuario este verificado
                var query = await _unitOfWork.UserProfileRepo.Get(p => p.UserId == user.Id);
                var profile = query.FirstOrDefault();

                if (profile == null)
                {
                    var responseP = new AuthResult
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            "El usuario no cuenta con un perfil"
                        },
                        ErrorCode = 2
                    };

                    return responseP;
                }
                else
                {
                    if (!profile.IsVerified)
                    {
                        var errors = new List<string>();

                        //Verificar el estatus de su ultima solicitud
                        var request = await _unitOfWork.UserApprovalRepo.Get(a => a.ProfileId == profile.Id && a.Status != ApprovalStatus.APROVADO);
                        var lastRequest = request.LastOrDefault();

                        //Devolver respuesta
                        var responseV = new AuthResult
                        {
                            Success = false,
                            UserProfileId = profile.Id
                        };

                        if (lastRequest == null) errors.Add("No se encontro ninguna solicitud de aprobación del usuario");
                        else
                        {
                            switch(lastRequest.Status)
                            {
                                case ApprovalStatus.PENDIENTE:
                                    errors.Add("El usuario esta pendiente de aprobación por la administración");
                                    responseV.ErrorCode = 3;
                                    break;
                                case ApprovalStatus.RECHAZADO:
                                    errors.Add("El usuario fue rechazado por la administración");
                                    errors.Add($"Motivo: {lastRequest.Comment}");
                                    responseV.ErrorCode = 4;
                                    break;
                                default:
                                    break;
                            }
                        }

                        responseV.Errors.AddRange(errors);

                        return responseV;
                    }
                    else
                    {
                        var signedIn = await _userManager.CheckPasswordAsync(user, password);
                        
                        var response = new AuthResult
                        {
                            Success = signedIn,
                            Errors = new List<string>()
                            {
                                "Usuario y/o contraseña incorrectos"
                            },
                            ErrorCode = 5
                        };

                        return response;
                    }
                }
            }
        }

        public async Task<AuthResult> LoginSocialAppUser(string UID)
        {
            var user = await _userManager.Users
                .Include(u => u.Socials)
                .Where(u => u.Socials
                .Any(s => s.FirebaseUID == UID)).FirstOrDefaultAsync();


            if (user == null)
            {
                var errorresponse = new AuthResult
                {
                    Success = false,
                    Errors = new List<string>()
                    {
                    "El usuario no cuenta con un registro en la aplicación"
                    }
                };

                return errorresponse;
            } 
            else
            {
                //Revisar que el usuario este verificado
                var query = await _unitOfWork.UserProfileRepo.Get(p => p.UserId == user.Id);
                var profile = query.FirstOrDefault();

                if (profile == null)
                {
                    var responseP = new AuthResult
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            "El usuario no cuenta con un perfil"
                        }
                    };

                    return responseP;
                }
                else
                {
                    if (!profile.IsVerified)
                    {
                        var errors = new List<string>();

                        //Verificar el estatus de su ultima solicitud
                        var request = await _unitOfWork.UserApprovalRepo.Get(a => a.ProfileId == profile.Id && a.Status != ApprovalStatus.APROVADO);
                        var lastRequest = request.LastOrDefault();

                        if (lastRequest == null) errors.Add("No se encontro ninguna solicitud de aprobación del usuario");
                        else
                        {
                            switch (lastRequest.Status)
                            {
                                case ApprovalStatus.PENDIENTE:
                                    errors.Add("El usuario esta pendiente de aprobación por la administración");
                                    break;
                                case ApprovalStatus.RECHAZADO:
                                    errors.Add("El usuario fue rechazado por la administración");
                                    errors.Add($"Motivo: {lastRequest.Comment}");
                                    break;
                                default:
                                    break;
                            }
                        }

                        //Devolver respuesta
                        var responseV = new AuthResult
                        {
                            Success = false,
                            Errors = errors,
                            UserProfileId = profile.Id
                        };

                        return responseV;
                    }
                    else
                    {

                        var response = new AuthResult
                        {
                            Success = true
                        };
                        response.Messages.Add("Login social identificado");

                        return response;
                    }
                }
            }
        }

        public async Task<bool> AuthorizeAsync(int userId, string policyName)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

            var result = await _authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }

        public async Task<AppUserRegistrationResponse> CreateAppUserAsync(AppUserRegistrationRequest user)
        {
            //Validar archivos
            if (!user.DriversLicenceFrontFile.ContentType.Contains("image") || !user.DriversLicenceBackFile.ContentType.Contains("image"))
            {
                return null;
            }

            var newUser = _mapper.Map<AppUser>(user);

            newUser.UserName = $"{Guid.NewGuid()}-{DateTime.UtcNow.Day}{DateTime.UtcNow.Month}{DateTime.UtcNow.Year}";
            newUser.NormalizedEmail = newUser.UserName.ToLower();
            newUser.NormalizedUserName = newUser.UserName.ToLower();
            newUser.LockoutEnabled = true;

            if (user.RegistrationType != SocialNetworkType.Native)
            {
                AppUserSocial newSocial = new AppUserSocial()
                {
                    FirebaseUID = user.FirebaseUID,
                    NetworkType = user.RegistrationType
                };

                newUser.Socials.Add(newSocial);
            }

            var result = await _userManager.CreateAsync(newUser, user.Password);

            if (!result.Succeeded)
            {
                return new AppUserRegistrationResponse()
                {
                    Result = result,
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }

            var appUser = await _userManager.FindByNameAsync(newUser.UserName);
            await _userManager.AddToRoleAsync(appUser, "AppUser");


            //Crear perfil nuevo
            var newProfile = new UserProfile()
            {
                User = appUser,
                FullName = $"{user.FirstName} {user.LastNameP} {user.LastNameM}",
                Name = user.FirstName,
                SurnameP = user.LastNameP,
                SurnameM = user.LastNameM,
                IsVerified = false
            };

            await _unitOfWork.UserProfileRepo.Add(newProfile);
            await _unitOfWork.SaveChangesAsync();

            //Crear solicitud de aprobación
            var request = _mapper.Map<ApprovalCreationRequest>(user);
            request.ProfileId = newProfile.Id;

            var approval = await _userApprovalServices.CreateApproval(request);

            if (!approval.success)
            {
                return null;
            }

            //Mapear resultado
            var dto = _mapper.Map<ProfileDto>(newProfile);
            return new AppUserRegistrationResponse()
            {
                Result = result,
                Profile = dto
            };
        }

        public async Task<IdentityResult> VerifyAppUser(int userId, string phoneToken)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
            var verified = await _userManager.ChangePhoneNumberAsync(user, user.PhoneNumber, phoneToken);

            return verified;
        }

        public async Task<string> CreateUserPhoneToken(int userId, string phoneNumber)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
            var verified = await _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);
            user.PhoneNumber = phoneNumber;

            return verified;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user != null)
            {
                var result = await DeleteUserAsync(user);
                return result.Succeeded;
            }
            else
            {
                return false;
            }
        }

        public async Task<IdentityResult> DeleteUserAsync(AppUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result;
        }

        public async Task<AppUser> GetAppUserBydIdAsync(int UserId)
        {
            var user = await _userManager.Users.Include(x => x.AssignedDepartments).Include(r => r.UserRoles).ThenInclude(r => r.Role).Where(u => u.Id == UserId).SingleOrDefaultAsync();

            return user;
        }

        public async Task<AppUser> GetAppUserBySocial(string UID, String Email, SocialNetworkType NetworkType)
        {
            var user = await _userManager.Users
                .Include(u => u.Socials)
                .Where(u => u.Socials
                .Any(s => s.FirebaseUID == UID)).FirstOrDefaultAsync();

            var emailUser = await _userManager.Users.Where(u => u.Email == Email).FirstOrDefaultAsync();

            if (user == null && emailUser == null)
            {
                return null;
            }

            if (user == null && emailUser != null)
            {
                AppUserSocial newSocial = new AppUserSocial()
                {
                    FirebaseUID = UID,
                    NetworkType = NetworkType
                };

                emailUser.Socials.Add(newSocial);
            }

            return user;
        }

        public async Task<AppUser> GetAppUserByPhoneEmail(string identifier)
        {
            var user = await _userManager.Users.Where(u => u.Email == identifier).FirstOrDefaultAsync();

            return user;
        }

        public async Task<string> GetUserNameAsync(int userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        public async Task<bool> IsInRoleAsync(int userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<GenericResponse<AppUser>> UpdateAdmUser(AdmUserUpdateRequest request)
        {
            GenericResponse<AppUser> response = new GenericResponse<AppUser>();
            try
            {
                var user = await _userManager.Users.Where(u => u.Id == request.AppUserId).SingleOrDefaultAsync();
                if(user == null)
                {
                    response.success = false;
                    response.AddError("Usuario inexistente", "Nope no existe <:^) ", 2);
                    return response;
                }

                user.Name = request.Name ?? user.Name;
                user.LastNameP = request.LastNameP ?? user.LastNameP;
                user.LastNameM = request.LastNameM ?? user.LastNameM;
                user.FullName = $"{user.Name} {user.LastNameP} {user.LastNameM}";

                var update = await _userManager.UpdateAsync(user);

                if (!update.Succeeded)
                {
                    response.success = false;
                    response.AddError("Error", "Nope no se actualizo <:^) ", 3);
                    return response;
                }

                response.success = true;
                response.Data = user;

                return response;
            }
            catch(Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        //Password related stuff
        public async Task<GenericResponse<string>> ResetPassword(string userEmail)
        {
            var response = new GenericResponse<string>();
            try
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                if(user == null)
                {
                    response.AddError("Usuario no encontrado", "No se encontro un usuario bajo la dirección de correo", 2);
                    response.success = false;
                    return response;
                }

                //Generar token para reinicio de contraseña
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                //Enviar el token al correo
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("password-recovery", "rcpg.no-reply@gmail.com"));
                message.To.Add(new MailboxAddress("Calamardo", $"{ userEmail }"));
                message.Subject = "Reinicio de contraseña";
                message.Body = new TextPart("html")
                {
                    Text = @$"Por favor reinicie su contraseña haciendo click en el siguiente link: <a href='https://rcpg-controlvehicular-api.azurewebsites.net/PRecovery/{token}/ '>Reinicio de Contraseña</a>"
                };

                await _mailClient.ConnectAsync("smtp.gmail.com", 465);
                await _mailClient.AuthenticateAsync("rcpg.controlvehicular@gmail.com", "jaoiyivnqwdhaezz");
                await _mailClient.SendAsync(message);
                await _mailClient.DisconnectAsync(true);

                response.success = true;
                response.Data = token;
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        public async Task<GenericResponse<IdentityResult>> PasswordResetConfirmation(PasswordResetConfirmationRequest request)
        {
            GenericResponse<IdentityResult> response = new GenericResponse<IdentityResult>();
            try
            {
                //Generar token para reinicio de contraseña
                var user = await _userManager.FindByEmailAsync(request.userEmail);
                if (user == null)
                {
                    response.AddError("Usuario no encontrado", "No se encontro un usuario bajo la dirección de correo", 2);
                    response.success = false;
                    return response;
                }

                var reset = await _userManager.ResetPasswordAsync(user, request.ResetToken, request.NewPassword);

                response.success = reset.Succeeded;
                response.Data = reset;
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }

        public async Task<GenericResponse<IdentityResult>> PasswordChange(PasswordChangeRequest request)
        {
            GenericResponse<IdentityResult> response = new GenericResponse<IdentityResult>();
            try 
            {
                //Encontrar el usuario
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    response.AddError("Usuario no encontrado", "No se encontro un usuario bajo la dirección de correo", 2);
                    response.success = false;
                    return response;
                }

                if(request.newPassword != request.newPasswordConfirmation)
                {
                    response.AddError("Confirmación Incorrecta", "La confirmación de contraseña es incorrecta", 3);
                    response.success = false;
                    return response;
                }

                var change = await _userManager.ChangePasswordAsync(user, request.oldPassword, request.newPassword);

                response.success = change.Succeeded;
                response.Data = change;
                return response;
            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);
                return response;
            }
        }
        #endregion

        #region ..::Administración AdminWeb::..
        public async Task<IdentityResult> CreateWebAdmUserAsync(WebAdmUserRegistrationRequest user)
        {
            var newUser = _mapper.Map<AppUser>(user);

            newUser.NormalizedEmail = newUser.UserName.ToLower();
            newUser.NormalizedUserName = newUser.UserName.ToLower();
            newUser.LockoutEnabled = true;
            newUser.EmailConfirmed = true;
            newUser.PhoneNumberConfirmed = true;
            newUser.Name = user.FirstName;
            newUser.LastNameP = user.LastNameP;
            newUser.LastNameM = user.LastNameM;
            newUser.FullName = $"{user.FirstName} {user.LastNameP} {user.LastNameM}"; 

            var result = await _userManager.CreateAsync(newUser, user.Password);

            if (result.Errors.Count() > 0)
            {
                return result;
            }

            var adminUser = await _userManager.FindByNameAsync(user.UserName);

            foreach (AdminRoleType role in user.Roles)
            {
                switch (role)
                {
                    case AdminRoleType.WebAdm:
                        await _userManager.AddToRoleAsync(adminUser, "AdminUser");
                        break;
                    case AdminRoleType.Supervisor:
                        await _userManager.AddToRoleAsync(adminUser, "Supervisor");

                        //Agregar a los departamentos como supervisor
                        //Buscar los departamentos y asignarlos al usuario
                        foreach (var id in user.SupervisingDepartments)
                        {
                            var department = await _unitOfWork.Departaments.GetById(id);
                            if (department != null)
                            {
                                department.Supervisors.Add(adminUser);
                                await _unitOfWork.Departaments.Update(department);
                            }
                        }

                        await _unitOfWork.SaveChangesAsync();
                        break;
                    default:
                        break;
                }
            }

            return result;
        }

        public async Task<ICollection<AppUserRole>> RemoveUserFromRoles(WebAdmRolesRequest request)
        {
            var user = await GetAppUserBydIdAsync(request.UserId);

            foreach (AdminRoleType role in request.Roles)
            {
                switch (role)
                {
                    case AdminRoleType.WebAdm:
                        await _userManager.RemoveFromRoleAsync(user, "AdminUser");
                        break;
                    case AdminRoleType.Supervisor:
                        await _userManager.RemoveFromRoleAsync(user, "Supervisor");
                        break;
                    default:
                        break;
                }
            }

            return user.UserRoles;
        }

        public async Task<ICollection<AppUserRole>> AddUserToRoles(WebAdmRolesRequest request)
        {
            var user = await GetAppUserBydIdAsync(request.UserId);

            foreach (AdminRoleType role in request.Roles)
            {
                switch (role)
                {
                    case AdminRoleType.WebAdm:
                        await _userManager.AddToRoleAsync(user, "AdminUser");
                        break;
                    case AdminRoleType.Supervisor:
                        await _userManager.AddToRoleAsync(user, "Supervisor");
                        break;
                    default:
                        break;
                }
            }

            return user.UserRoles;
        }

        public async Task<List<string>> GetUserRoles(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            return roles.ToList();
        }

        public async Task<List<string>> GetRoleList()
        {
            var roles = await _roleManager.Roles.Select(r => r.NormalizedName).ToListAsync();

            return roles;
        }

        public async Task<object> GetUsersWithRoles(AdminRoleType? roleType = null)
        {
            AppRole adminRole = null;
            AppRole superAdmin = await _roleManager.FindByNameAsync("Administrator");
            AppRole appUser = await _roleManager.FindByNameAsync("AppUser");

            switch (roleType)
            {
                case AdminRoleType.WebAdm:
                    adminRole = await _roleManager.FindByNameAsync("AdminUser");
                    break;
                case AdminRoleType.Supervisor:
                    adminRole = await _roleManager.FindByNameAsync("Supervisor");
                    break;
                default:
                    break;
            }

            if (adminRole == null)
            {
                var users = await _userManager.Users
                    .Include(u => u.AssignedDepartments)
                    .ThenInclude(d => d.Company)
                    .Include(r => r.UserRoles)
                    .ThenInclude(r => r.Role)
                    .Where(r => r.UserRoles
                    .Any(x => x.Role != superAdmin && x.Role != appUser))
                    .Select(u => new
                    {
                        u.Id,
                        Username = u.UserName,
                        FullName = u.FullName,
                        Name = u.Name,
                        LastNameP = u.LastNameP,
                        LastNameM = u.LastNameM,
                        Email = u.Email,
                        Roles = u.UserRoles.Select(r => r.Role.Name).ToList(),
                        SupervisingDepartments = u.AssignedDepartments
                    })
                    .ToListAsync();

                return users;
            }
            else
            {
                var users = await _userManager.Users
                    .Include(u => u.AssignedDepartments)
                    .ThenInclude(d => d.Company)
                    .Include(r => r.UserRoles)
                    .ThenInclude(r => r.Role)
                    .Where(r => r.UserRoles
                    .Any(x => x.Role == adminRole))
                    .Select(u => new
                    {
                        u.Id,
                        Username = u.UserName,
                        FullName = u.FullName,
                        Name = u.Name,
                        LastNameP = u.LastNameP,
                        LastNameM = u.LastNameM,
                        Email = u.Email,
                        Roles = u.UserRoles.Select(r => r.Role.Name).ToList(),
                        SupervisingDepartments = u.AssignedDepartments
                    })
                    .ToListAsync();

                return users;
            }

        }

        public async Task<object> GetCustomers()
        {
            var adminRole = await _roleManager.FindByNameAsync("AppUser");

            var users = await _userManager.Users
                .Include(r => r.UserRoles)
                .ThenInclude(r => r.Role)
                .Where(r => r.UserRoles
                .Any(x => x.Role == adminRole))
                .Include(u => u.Profile)
                .ToListAsync();

            return users;
        }

        public async Task<IdentityResult> LockOutUser(int UserId)
        {
            var user = await GetAppUserBydIdAsync(UserId);

            var locked = await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddMinutes(5));

            return locked;
        }

        public async Task<IdentityResult> DeleteUser(int UserId)
        {
            var user = await GetAppUserBydIdAsync(UserId);
            if (user.UserName == "CGAdmin") return null;

            var result = await _userManager.DeleteAsync(user);

            return result;
        }

        public async Task<IdentityResult> ModifyUser(int UserId)
        {
            var user = await GetAppUserBydIdAsync(UserId);

            var result = await _userManager.UpdateAsync(user);

            return result;
        }

        public async Task<AppUserRegistrationResponse> CreateDummyAppUser(AppUserRegistrationRequest user)
        {
            var newUser = _mapper.Map<AppUser>(user);

            newUser.UserName = $"{Guid.NewGuid()}";
            newUser.NormalizedEmail = newUser.UserName.ToLower();
            newUser.NormalizedUserName = newUser.UserName.ToLower();
            newUser.LockoutEnabled = true;
            newUser.PhoneNumberConfirmed = true;
            newUser.EmailConfirmed = true;

            if (user.RegistrationType != SocialNetworkType.Native)
            {
                AppUserSocial newSocial = new AppUserSocial()
                {
                    FirebaseUID = user.FirebaseUID,
                    NetworkType = user.RegistrationType
                };

                newUser.Socials.Add(newSocial);
            }

            var result = await _userManager.CreateAsync(newUser, user.Password);

            if (!result.Succeeded)
            {
                return new AppUserRegistrationResponse()
                {
                    Result = result,
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }

            var appUser = await _userManager.FindByNameAsync(newUser.UserName);
            //var phoneToken = await _userManager.GenerateChangePhoneNumberTokenAsync(appUser, appUser.PhoneNumber);
            await _userManager.AddToRoleAsync(appUser, "AppUser");

            var newProfile = new UserProfile()
            {
                User = appUser,
                FullName = user.FirstName,
                SurnameP = user.LastNameP,
                SurnameM = user.LastNameM
            };

            await _unitOfWork.UserProfileRepo.Add(newProfile);
            await _unitOfWork.SaveChangesAsync();

            return new AppUserRegistrationResponse()
            {
                Result = result
            };
        }

        public async Task<GenericResponse<AppUser>> AssignDepartmentSupervisor(SupervisorAssignmentRequest request)
        {
            GenericResponse<AppUser> response = new GenericResponse<AppUser>();

            try
            {
                //Revisar que el usuario exista y contenga el rol de supervisor
                var user = await GetAppUserBydIdAsync(request.AppUserId);
                var roles = await GetUserRoles(user);

                if (user == null) return null;

                if (!roles.Contains("Supervisor"))
                {
                    response.success = false;
                    response.AddError("Unauthorized", "El usuario especificado no cuenta con el rol para ser asignado a los departamentos");

                    return response;
                }

                //Buscar los departamentos y asignarlos al usuario
                foreach (var id in request.DepartmentsToAssign)
                {
                    var department = await _unitOfWork.Departaments.GetById(id);
                    if (department == null)
                    {
                        response.success = false;
                        response.AddError("Not Found", $"El departamento con Id {id} no existe", 2);

                        return response;
                    }

                    department.Supervisors.Add(user);

                    await _unitOfWork.Departaments.Update(department);
                }

                await _unitOfWork.SaveChangesAsync();

                response.success = true;
                response.Data = user;

                return response;

            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);

                return response;
            }

        }

        public async Task<GenericResponse<AppUser>> RemoveDepartmentSupervisor(SupervisorRemovalRequest request)
        {
            GenericResponse<AppUser> response = new GenericResponse<AppUser>();

            try
            {
                //Obtener los departamentos que contengan al usuario
                var departments = await _unitOfWork.Departaments.Get(d => d.Supervisors.Any(x => x.Id == request.AppUserId), includeProperties: "Supervisors");
                var user = await GetAppUserBydIdAsync(request.AppUserId);
                if (user == null) return null;

                //Buscar los departamentos y asignarlos al usuario
                foreach (var department in request.DepartmentsToRemove)
                {
                    var exists = departments.Where(d => d.Id == department).FirstOrDefault();
                    if (exists == null)
                    {
                        response.AddError("Not Found", $"No existe el departamento con Id {department}", 2);
                        response.success = false;

                        return response;
                    }

                    user.AssignedDepartments.Remove(exists);
                }

                await _userManager.UpdateAsync(user);

                response.success = true;
                response.Data = user;

                return response;

            }
            catch (Exception ex)
            {
                response.success = false;
                response.AddError("Error", ex.Message, 1);

                return response;
            }
        }
        #endregion
    }
}
