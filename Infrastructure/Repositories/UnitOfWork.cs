using Application.Interfaces;
using Domain.Entities.Company;
using Domain.Entities.Departament;
using Domain.Entities.Identity;
using Domain.Entities.Profiles;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CVContext _context;
        private readonly IRepository<RefreshToken> _RefreshTokenRepo;
        private readonly IRepository<UserProfile> _UserProfileRepo;
        private readonly IRepository<Companies> _Companies;
        private readonly IRepository<Departaments> _Departaments;
        public UnitOfWork(CVContext context)
        {
            _context = context;
        }

        //DECLARAR REPOSITORIOS
        public IRepository<RefreshToken> RefreshTokenRepo => _RefreshTokenRepo ?? new BaseRepository<RefreshToken>(_context);
        public IRepository<UserProfile> UserProfileRepo => _UserProfileRepo ?? new BaseRepository<UserProfile>(_context);
        public IRepository<Companies> Companies => _Companies ?? new BaseRepository<Companies>(_context);
        public IRepository<Departaments> Departaments => _Departaments ?? new BaseRepository<Departaments>(_context);

        //FUNCIONES DEL SERVICIO
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
