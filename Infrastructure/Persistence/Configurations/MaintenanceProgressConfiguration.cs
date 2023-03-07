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
    public class MaintenanceProgressConfiguration : IEntityTypeConfiguration<MaintenanceProgress>
    {
        public void Configure(EntityTypeBuilder<MaintenanceProgress> builder)
        {
            builder.HasOne(m => m.VehicleMaintenance)
                .WithMany(e => e.MaintenanceProgress)
                .HasForeignKey(m => m.VehicleMaintenanceId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(m => m.MobileUser)
                .WithMany()
                .HasForeignKey(m => m.MobileUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.HasOne(m => m.AdminUser)
                .WithMany()
                .HasForeignKey(m => m.AdminUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        }
    }
}
