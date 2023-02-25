using Domain.Entities.Registered_Cars;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class VehicleReportConfiguration : IEntityTypeConfiguration<VehicleReport>
    {
        public void Configure(EntityTypeBuilder<VehicleReport> builder)
        {

            builder.HasMany(ur => ur.Expenses)
                .WithOne(u => u.VehicleReport)
                .HasForeignKey(ur => ur.VehicleReportId)
                .IsRequired(false);

            builder.HasMany(xu => xu.VehicleReportImages)
                .WithOne(x => x.VehicleReport)
                .HasForeignKey(xu => xu.VehicleReportId)
                .IsRequired();

            builder.HasOne(au => au.SolvedByAdminUser)
                .WithMany(ad => ad.SolvedReports)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(v => v.AmountGasoline).HasColumnType("decimal(18,2)");
        }

    }
}
