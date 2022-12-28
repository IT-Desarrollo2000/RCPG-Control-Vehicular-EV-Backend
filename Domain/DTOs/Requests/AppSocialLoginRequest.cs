using Domain.Enums;

namespace Domain.DTOs.Requests
{
    public class AppSocialLoginRequest
    {
        public string Email { get; set; }
        public string UID { get; set; }
        public SocialNetworkType SocialType { get; set; }
    }
}
