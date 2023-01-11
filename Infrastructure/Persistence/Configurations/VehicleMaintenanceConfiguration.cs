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
    public class VehicleMaintenanceConfiguration : IEntityTypeConfiguration<VehicleMaintenance>
    {
        public void Configure(EntityTypeBuilder<VehicleMaintenance> builder)
        {
            builder.HasMany(ur => ur.VehicleMaintenanceWorkshops)
                   .WithOne(u => u.VehicleMaintenance)
                   .HasForeignKey(x => x.VehicleMaintenanceId)
                   .IsRequired();

        }

    }
}
