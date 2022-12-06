using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class AuthResult
    {
        public string Token { get; set; }
        public DateTime? TokenExpiration { get; set; } = null;
        public string RefreshToken { get; set; }
        public bool Success { get; set; }
        public List<string> Messages { get; set; } = null;
        public List<string> Errors { get; set; } = null;
    }
}
