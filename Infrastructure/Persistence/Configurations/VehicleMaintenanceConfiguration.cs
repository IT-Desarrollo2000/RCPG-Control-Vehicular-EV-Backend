﻿using Domain.Entities.Registered_Cars;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class VehicleMaintenanceConfiguration : IEntityTypeConfiguration<VehicleMaintenanceWorkshop>
    {
        public void Configure(EntityTypeBuilder<VehicleMaintenanceWorkshop> builder)
        {
            builder.HasMany(ur => ur.VehicleMaintenances)
                   .WithOne(u => u.WorkShop)
                   .HasForeignKey(x => x.WorkShopId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
