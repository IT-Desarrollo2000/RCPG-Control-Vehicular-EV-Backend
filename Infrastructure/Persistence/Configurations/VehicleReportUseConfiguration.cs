using Domain.Entities.Registered_Cars;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class VehicleReportUseConfiguration : IEntityTypeConfiguration<VehicleReportUse>
    {
        public void Configure(EntityTypeBuilder<VehicleReportUse> builder)
        {
            builder.Property(v => v.FinalMileage).HasColumnType("decimal(18,2)");
            builder.HasMany(ur => ur.VehicleReport)
                    .WithOne(u => u.VehicleReportUses)
                    .HasForeignKey(ur => ur.VehicleReportUseId);

            builder.HasMany(bu => bu.Destinations)
                    .WithOne(b => b.VehicleReportUses)
                    .HasForeignKey(bu => bu.VehicleReportUseId)
                    .IsRequired(false);

            builder.HasOne(r => r.Checklist)
                    .WithMany(c => c.VehicleReportUses)
                    .HasForeignKey(r => r.ChecklistId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
