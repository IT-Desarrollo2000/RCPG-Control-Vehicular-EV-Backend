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
    public class MaintenanceConfiguration : IEntityTypeConfiguration<VehicleMaintenance>
    {
        public void Configure(EntityTypeBuilder<VehicleMaintenance> builder)
        {
            builder.HasOne(m => m.ApprovedByUser)
                .WithMany()
                .HasForeignKey(m => m.ApprovedByUserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Report)
                .WithMany(r => r.Maintenances)
                .HasForeignKey(m => m.ReportId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

          /* builder.HasMany(m => m.Expenses)
                .WithOne( d => d.VehicleMaintenance)
                .HasForeignKey(m => m.)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);*/
        }
    }
}
