using Domain.CustomEntities;
using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Identity;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthResult> LoginAppUser(string userEmailPhone, string password);
        Task<AuthResult> LoginSocialAppUser(string UID);
        Task<AuthResult> LoginWebAdmUser(AppUser user, string password);
        Task<object> GetUsersWithRoles(AdminRoleType? roleType = null);
        Task<AppUser> GetAppUserBydIdAsync(int UserId);
        Task<AppUser> GetAppUserBySocial(string UID, String Email, SocialNetworkType NetworkType);
        Task<AppUser> GetAppUserByPhoneEmail(string identifier);
        Task<string> GetUserNameAsync(int userId);
        Task<bool> IsInRoleAsync(int userId, string role);
        Task<bool> AuthorizeAsync(int userId, string policyName);
        Task<IdentityResult> CreateWebAdmUserAsync(WebAdmUserRegistrationRequest user);
        Task<ICollection<AppUserRole>> RemoveUserFromRoles(WebAdmRolesRequest request);
        Task<ICollection<AppUserRole>> AddUserToRoles(WebAdmRolesRequest request);
        Task<AppUserRegistrationResponse> CreateAppUserAsync(AppUserRegistrationRequest user);
        Task<IdentityResult> VerifyAppUser(int userId, string phoneToken);
        Task<string> CreateUserPhoneToken(int userId, string phoneNumber);
        Task<bool> DeleteUserAsync(int userId);
        Task<List<string>> GetUserRoles(AppUser user);
        Task<List<string>> GetRoleList();
        Task<object> GetCustomers();
        Task<AppUserRegistrationResponse> CreateDummyAppUser(AppUserRegistrationRequest user);
        Task<GenericResponse<AppUser>> AssignDepartmentSupervisor(SupervisorAssignmentRequest request);
        Task<GenericResponse<AppUser>> RemoveDepartmentSupervisor(SupervisorRemovalRequest request);
        Task<GenericResponse<string>> ResetPassword(string userEmail);
    }
}
