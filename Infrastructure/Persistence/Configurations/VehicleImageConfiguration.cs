using Domain.Entities.Registered_Cars;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class VehicleImageConfiguration : IEntityTypeConfiguration<VehicleImage>
    {
        public void Configure(EntityTypeBuilder<VehicleImage> builder)
        {
            builder.HasOne(i => i.Vehicle)
                .WithMany(v => v.VehicleImages)
                .HasForeignKey(i => i.VehicleId)
                .IsRequired();
        }
    }
}
