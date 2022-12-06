using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Identity
{
    public class AppUserSocial : BaseEntity
    {
        public int UserId { get; set; }
        public virtual AppUser User { get; set; }
        public string FirebaseUID { get; set; }
        public SocialNetworkType NetworkType { get; set; }
    }
}
