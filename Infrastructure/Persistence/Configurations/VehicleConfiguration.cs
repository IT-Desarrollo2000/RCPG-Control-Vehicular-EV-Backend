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
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure (EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasMany(ur => ur.VehicleServices)
                    .WithOne(u => u.Vehicle)
                    .HasForeignKey(ur => ur.VehicleId)
                    .IsRequired();

            builder.HasMany(x => x.Checklists)
                .WithOne(xr => xr.Vehicle)
                .HasForeignKey(x => x.VehicleId)
                .IsRequired();

        }


    }
}
