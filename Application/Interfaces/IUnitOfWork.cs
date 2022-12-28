using Domain.Entities.Company;
using Domain.Entities.Departament;
using Domain.Entities.Identity;
using Domain.Entities.Profiles;
using Domain.Entities.User_Approvals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<RefreshToken> RefreshTokenRepo { get; }
        IRepository<UserProfile> UserProfileRepo { get; }
        IRepository<UserApproval> UserApprovalRepo { get; }
        IRepository<Companies> Companies { get; }
        IRepository<Departaments> Departaments { get; }
        
        void Dispose();
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
