using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class PasswordResetConfirmationRequest
    {
        [Required]
        public string userEmail { get; set; }
        [Required]
        public string ResetToken { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string NewPasswordConfirmation { get; set; }
    }
}
