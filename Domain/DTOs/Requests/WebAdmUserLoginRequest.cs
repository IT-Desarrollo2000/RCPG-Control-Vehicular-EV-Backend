using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Requests
{
    public class WebAdmUserLoginRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
