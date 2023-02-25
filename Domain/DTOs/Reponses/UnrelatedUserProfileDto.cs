using Domain.Enums;

namespace Domain.DTOs.Reponses
{
    public class UnrelatedUserProfileDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public string SurnameP { get; set; }
        public string SurnameM { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? ProfileImagePath { get; set; }
        public DateTime? ProfileImageUploadDate { get; set; }

        //Relacionados al conductor
        public string? DriversLicenceFrontUrl { get; set; }
        public string? DriversLicenceBackUrl { get; set; }
        public bool IsVerified { get; set; }
        public DateTime? LicenceExpeditionDate { get; set; }
        public DateTime? LicenceExpirationDate { get; set; }
        public int? LicenceValidityYears { get; set; }
        public string? DriversLicenceFrontPath { get; set; }
        public string? DriversLicenceBackPath { get; set; }
        public LicenceType? LicenceType { get; set; }
        public int? DepartmentId { get; set; }

    }
}
