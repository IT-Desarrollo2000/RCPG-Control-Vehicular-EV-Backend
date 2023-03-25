using Domain.Entities.Registered_Cars;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MimeKit.Encodings;

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

            builder.HasOne(r => r.InitialCheckList)
                .WithMany(c => c.InitialCheckListForUseReport)
                .HasForeignKey(r => r.InitialCheckListId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.FinishedByDriver)
                .WithMany(u => u.FinishedUseReports)
                .HasForeignKey(r => r.FinishedByDriverId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.FinishedByAdmin)
                .WithMany(u => u.FinishedUseReports)
                .HasForeignKey(r => r.FinishedByAdminId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
