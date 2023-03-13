using Domain.Entities.Registered_Cars;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ProgressImagesConfig : IEntityTypeConfiguration<MaintenanceProgressImages>
    {
        public void Configure(EntityTypeBuilder<MaintenanceProgressImages> builder)
        {
            builder.HasOne(i => i.Progress)
                .WithMany(m => m.ProgressImages)
                .HasForeignKey(i => i.ProgressId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
