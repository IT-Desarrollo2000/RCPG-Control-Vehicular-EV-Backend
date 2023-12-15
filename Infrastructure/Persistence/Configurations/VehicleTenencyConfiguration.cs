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
    public class VehicleTenencyConfiguration : IEntityTypeConfiguration<VehicleTenency>
    {
        public void Configure(EntityTypeBuilder<VehicleTenency> builder)
        {
            builder.HasOne(x => x.Vehicle)
                .WithMany(v => v.Tenencies)
                .HasForeignKey(x => x.VehicleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(x => x.Expense)
                .WithOne()
                .HasForeignKey<VehicleTenency>(t => t.ExpenseId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(v => v.TenencyCost).HasColumnType("decimal(18,2)");
        }
    }
}
