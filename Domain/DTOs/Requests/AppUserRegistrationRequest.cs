using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Domain.DTOs.Requests
{
    public class AppUserRegistrationRequest
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastNameP { get; set; }
        public string LastNameM { get; set; }
        public SocialNetworkType RegistrationType { get; set; }
        public string? FirebaseUID { get; set; }

        //Para registro de conductores
        public IFormFile DriversLicenceFrontFile { get; set; }
        public IFormFile DriversLicenceBackFile { get; set; }
        public LicenceType LicenceType { get; set; }
        public int LicenceValidityYears { get; set; }
        public DateTime LicenceExpeditionDate { get; set; }
        public DateTime LicenceExpirationDate { get; set; }
    }
}
