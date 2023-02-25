using Domain.Entities.Registered_Cars;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class DestinationOfReportUseConfiguration : IEntityTypeConfiguration<DestinationOfReportUse>
    {
        public void Configure(EntityTypeBuilder<DestinationOfReportUse> builder)
        {
            builder.Property(v => v.Latitud).HasColumnType("decimal(18,2)");
            builder.Property(w => w.Longitude).HasColumnType("decimal(18,2)");

        }
    }
}
