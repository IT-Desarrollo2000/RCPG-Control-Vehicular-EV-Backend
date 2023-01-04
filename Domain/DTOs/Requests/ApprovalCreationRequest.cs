using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Domain.DTOs.Requests
{
    public class ApprovalCreationRequest
    {
        public IFormFile DriversLicenceFrontFile { get; set; }
        public IFormFile DriversLicenceBackFile { get; set; }
        public LicenceType LicenceType { get; set; }
        public int LicenceValidityYears { get; set; }
        public DateTime LicenceExpeditionDate { get; set; }
        public DateTime LicenceExpirationDate { get; set; }
        public int ProfileId { get; set; }
    }
}
