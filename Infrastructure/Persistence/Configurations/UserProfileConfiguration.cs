using Domain.Entities.Profiles;
using Domain.Entities.Registered_Cars;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasMany(ur => ur.VehicleReports)
                .WithOne(u => u.UserProfile)
                .HasForeignKey(ur => ur.UserProfileId).
                 OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(vu => vu.VehicleReportUses)
                    .WithOne(v => v.UserProfile)
                    .HasForeignKey(vu => vu.UserProfileId).
                    OnDelete(DeleteBehavior.Restrict);
        }

    }
}
