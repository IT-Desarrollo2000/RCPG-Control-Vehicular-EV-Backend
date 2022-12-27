using Application.Interfaces;
using Domain.Entities.Identity;
using Domain.Entities.Profiles;
using Domain.Entities.User_Approvals;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CVContext _context;
        private readonly IRepository<RefreshToken> _RefreshTokenRepo;
        private readonly IRepository<UserProfile> _UserProfileRepo;
        private readonly IRepository<UserApproval> _UserApprovalRepo;

        public UnitOfWork(CVContext context)
        {
            _context = context;
        }

        //DECLARAR REPOSITORIOS
        public IRepository<RefreshToken> RefreshTokenRepo => _RefreshTokenRepo ?? new BaseRepository<RefreshToken>(_context);
        public IRepository<UserProfile> UserProfileRepo => _UserProfileRepo ?? new BaseRepository<UserProfile>(_context);
        public IRepository<UserApproval> UserApprovalRepo => _UserApprovalRepo ?? new BaseRepository<UserApproval>(_context);


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
