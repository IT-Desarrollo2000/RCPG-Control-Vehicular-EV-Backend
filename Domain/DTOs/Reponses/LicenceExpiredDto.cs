using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class LicenceExpiredDto
    {
        public string StatusMessage { get; set; }
        public string StatusColor { get; set; }
        public string StatusName { get; set; }
        public int UserProfileId { get; set; }
        public string UserFullName { get; set; }
        public string? DriversLicenceFrontUrl { get; set; }
        public string? DriversLicenceBackUrl { get; set; }
        public LicenceType LicenceType { get; set; }
        public LicenceExpStopLight ExpirationType { get; set; }
        public DateTime LicenceExpirationDate { get; set; }
        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set;}
    }
}
