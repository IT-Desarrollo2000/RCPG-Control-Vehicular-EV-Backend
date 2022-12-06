using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class AppSocialLoginRequest
    {
        public string Email { get; set; }
        public string UID { get; set; }
        public SocialNetworkType SocialType { get; set; }
    }
}
