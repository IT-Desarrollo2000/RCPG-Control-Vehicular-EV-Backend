using Application.Interfaces;
using AutoMapper;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Identity;
using Domain.Entities.Profiles;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
                }
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
                        }
                    };

                    return responseP;
                }
                else
                {
                    if(!profile.IsVerified)
                    {
                        var responseV = new AuthResult
                        {
                            Success = false,
                            Errors = new List<string>()
                        {
                            "El usuario no ha sido verificado por la administración"
                        }
                        };

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
                            }
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

            var response = new AuthResult
            {
                Success = true,
                Errors = new List<string>()
                {
                    "Login social identificado"
                }
            };

            return response;
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
            if(!user.DriversLicenceFrontFile.ContentType.Contains("image") || user.DriversLicenceBackFile.ContentType.Contains("image"))
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
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == UserId);

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
                .Include(r => r.UserRoles)
                .ThenInclude(r => r.Role)
                .Where(r => r.UserRoles
                .Any(x => x.Role != superAdmin && x.Role != appUser))
                .Select(u => new
                {
                    u.Id,
                    Username = u.UserName,
                    Email = u.Email,
                    Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
                })
                .ToListAsync();

                return users;
            }
            else
            {
                var users = await _userManager.Users
                .Include(r => r.UserRoles)
                .ThenInclude(r => r.Role)
                .Where(r => r.UserRoles
                .Any(x => x.Role == adminRole))
                .Select(u => new
                {
                    u.Id,
                    Username = u.UserName,
                    Email = u.Email,
                    Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
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
        #endregion
    }
}
