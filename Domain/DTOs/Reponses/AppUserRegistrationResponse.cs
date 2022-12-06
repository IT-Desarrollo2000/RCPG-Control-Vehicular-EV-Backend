using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class AppUserRegistrationResponse
    {
        public AppUserRegistrationResponse()
        {
            this.Errors = new List<string>();
        }

        public IdentityResult Result { get; set; }
        public ProfileDto? Profile { get; set; } = null;
        public List<string> Errors { get; set; }
    }
}
