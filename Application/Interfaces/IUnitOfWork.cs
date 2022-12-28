using Domain.Entities.Company;
using Domain.Entities.Departament;
using Domain.Entities.Identity;
using Domain.Entities.Profiles;

namespace Application.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<RefreshToken> RefreshTokenRepo { get; }
        IRepository<UserProfile> UserProfileRepo { get; }
        IRepository<Companies> Companies { get; }
        IRepository<Departaments> Departaments { get; }

        void Dispose();
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
