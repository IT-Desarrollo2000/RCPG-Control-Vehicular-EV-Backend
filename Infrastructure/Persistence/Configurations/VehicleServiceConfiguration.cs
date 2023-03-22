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
    public class VehicleServiceConfiguration : IEntityTypeConfiguration<VehicleService>
    {
        public void Configure(EntityTypeBuilder<VehicleService> builder)
        {
            builder.HasOne(s => s.ServiceUser)
                .WithMany()
                .HasForeignKey(s => s.ServiceUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Workshop)
                .WithMany()
                .HasForeignKey(s => s.WorkShopId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Expense)
                .WithOne(m => m.VehicleService)
                .HasForeignKey<Expenses>(e => e.VehicleServiceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
