using Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        //Unique
        //public string SharingKey { get; set; }
        //public ICollection<UserContact> Contacts { get; set; }
    }
}
