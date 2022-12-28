using Domain.DTOs.Reponses;
using Domain.DTOs.Requests;
using Domain.Entities.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Application.Interfaces
{
    public interface ITokenService
    {
        Task<AuthResult> CreateToken(AppUser user);
        Task<AuthResult> VerifyToken(TokenRequest tokenRequest, TokenValidationParameters tokenValidationParameters);
        Task<AuthResult> RevokeToken(TokenRequest tokenRequest, TokenValidationParameters tokenValidationParameters);
    }
}
