using Domain.Entities.Profiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasMany(ur => ur.VehicleReports)
                .WithOne(u => u.MobileUser)
                .HasForeignKey(ur => ur.MobileUserId).
                 OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(vu => vu.VehicleReportUses)
                    .WithOne(v => v.UserProfile)
                    .HasForeignKey(vu => vu.UserProfileId).
                    OnDelete(DeleteBehavior.Restrict);
        }

    }
}
