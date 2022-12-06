using Domain.Entities.Identity;
using Domain.Entities.Profiles;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Persistence.Configurations;

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


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new AppUserConfiguration());
            builder.ApplyConfiguration(new AppRoleConfiguration());
        }
    }
}
