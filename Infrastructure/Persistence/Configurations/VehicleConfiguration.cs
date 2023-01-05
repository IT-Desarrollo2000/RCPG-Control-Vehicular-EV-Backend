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

        }
    }
}
