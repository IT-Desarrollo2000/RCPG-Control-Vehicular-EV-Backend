using Domain.Entities.Profiles;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class ApprovalCreationRequest
    {
        public IFormFile DriversLicenceFrontFile{ get; set; }
        public IFormFile DriversLicenceBackFile { get; set; }
        public LicenceType LicenceType { get; set; }
        public int LicenceValidityYears { get; set; }
        public DateTime LicenceExpeditionDate { get; set; }
        public DateTime LicenceExpirationDate { get; set; }
        public int ProfileId { get; set; }
    }
}
