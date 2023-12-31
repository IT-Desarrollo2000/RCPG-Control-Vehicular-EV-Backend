﻿using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.HasMany(u => u.Socials)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .IsRequired();

            builder.HasMany(pu => pu.VehicleReports)
                .WithOne(p => p.AdminUser)
                .HasForeignKey(pu => pu.AdminUserId);

            builder.HasMany(vu => vu.VehicleReportUses)
                    .WithOne(v => v.AppUser)
                    .HasForeignKey(vu => vu.AppUserId);
        }
    }
}
