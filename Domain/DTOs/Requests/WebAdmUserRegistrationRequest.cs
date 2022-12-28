﻿using Domain.Enums;

namespace Domain.DTOs.Requests
{
    public class WebAdmUserRegistrationRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastNameP { get; set; }
        public string LastNameM { get; set; }
        public List<AdminRoleType> Roles { get; set; }
        public DateTime BirthDate { get; set; } = DateTime.UtcNow;
    }
}
