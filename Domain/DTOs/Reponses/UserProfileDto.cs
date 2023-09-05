using Domain.Entities.Departament;
using Domain.Entities.Identity;
using Domain.Entities.Registered_Cars;
using Domain.Entities.User_Approvals;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Reponses
{
    public class UserProfileDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string SurnameP { get; set; }
        public string SurnameM { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? ProfileImagePath { get; set; }
        public DateTime? ProfileImageUploadDate { get; set; }

        //Relacionados al conductor
        public bool IsVerified { get; set; }
        public bool? CanDriveInHighway { get; set; }
        public string? DriversLicenceFrontUrl { get; set; }
        public string? DriversLicenceBackUrl { get; set; }
        public string? DriversLicenceFrontPath { get; set; }
        public string? DriversLicenceBackPath { get; set; }
        public int? LicenceValidityYears { get; set; }
        public LicenceType? LicenceType { get; set; }
        public DateTime? LicenceExpeditionDate { get; set; }
        public DateTime? LicenceExpirationDate { get; set; }
        public int? DepartmentId { get; set; }
    }
}
