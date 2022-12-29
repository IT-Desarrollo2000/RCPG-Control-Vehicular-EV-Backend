using Domain.Entities.Company;
using Domain.Entities.Departament;
using Domain.Entities.Identity;
using Domain.Entities.Profiles;
using Domain.Entities.Registered_Cars;
using Domain.Entities.User_Approvals;
using Infrastructure.Persistence.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class CVContext : IdentityDbContext<AppUser, AppRole, int,
        IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public CVContext(DbContextOptions<CVContext> options) : base(options)
        {

        }

        ///DECLARACIÖN DE ENTIDADES
        //Entidades Base
        //Security
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<AppUserSocial> AppUserSocials { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }

        //Companies
        public virtual DbSet<Companies> Companies { get; set; }
        //Departamens
        public virtual DbSet<Departaments> Departaments { get; set; }

        //User Approvals
        public virtual DbSet<UserApproval> UserApprovals { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new AppUserConfiguration());
            builder.ApplyConfiguration(new AppRoleConfiguration());
            builder.ApplyConfiguration(new UserApprovalConfiguration());
            builder.ApplyConfiguration(new CompanyDepartamentConfiguration());
        }
    }
}
