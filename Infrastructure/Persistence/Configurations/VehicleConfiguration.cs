using Domain.Entities.Registered_Cars;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasMany(ur => ur.VehicleServices)
                    .WithOne(u => u.Vehicle)
                    .HasForeignKey(ur => ur.VehicleId)
                    .IsRequired();

            builder.HasMany(x => x.Checklists)
                .WithOne(xr => xr.Vehicle)
                .HasForeignKey(x => x.VehicleId)
                .IsRequired();

            builder.Property(v => v.DesiredPerformance).HasColumnType("decimal(18,2)");
            builder.HasMany(pu => pu.VehicleMaintenances)
                   .WithOne(p => p.Vehicle)
                   .HasForeignKey(pu => pu.VehicleId)
                   .IsRequired();

            builder.HasMany(m => m.VehicleReports)
                .WithOne(mu => mu.Vehicle)
                .HasForeignKey(m => m.VehicleId)
                .IsRequired();

            builder.HasMany(v => v.AssignedDepartments)
                .WithMany(d => d.AssignedVehicles);

            builder.HasMany(bu => bu.VehicleReportsUses)
                   .WithOne(b => b.Vehicle)
                   .HasForeignKey(bu => bu.VehicleId);

            builder.HasOne(ru => ru.Policy)
                    .WithOne(r => r.Vehicle)
                    .HasForeignKey<Policy>(r => r.VehicleId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
