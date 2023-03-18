namespace Domain.DTOs.Reponses
{
    public class AuthResult
    {
        public AuthResult()
        {
            Messages = new List<string>();
            Errors = new List<string>();
        }
        public string Token { get; set; }
        public DateTime? TokenExpiration { get; set; } = null;
        public string RefreshToken { get; set; }
        public bool Success { get; set; }
        public int? UserProfileId { get; set; }
        public List<string> Messages { get; set; } = null;
        public List<string> Errors { get; set; } = null;
    }
}
