using Domain.Entities.Identity;

namespace Domain.Entities.Profiles
{
    public class UserProfile : BaseEntity
    {
        //public userprofile()
        //{
        //    this.contacts = new hashset<usercontact>();
        //}

        public int UserId { get; set; }
        public virtual AppUser User { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public string SurnameP { get; set; }
        public string SurnameM { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? ProfileImagePath { get; set; }
        public DateTime? ProfileImageUploadDate { get; set; }

        //Relacionados al conductor
        public bool IsVerified { get; set; }
        public string? DriversLicenceUrl { get; set; }
        public string? DriversLicencePath { get; set; }
        public int? LicenceValidityYears { get; set; }
        public DateTime? LicenceExpeditionDate { get; set; }
        public DateTime? LicenceExpirationDate { get; set; }

        //Unique
        //public string SharingKey { get; set; }
        //public ICollection<UserContact> Contacts { get; set; }
    }
}
